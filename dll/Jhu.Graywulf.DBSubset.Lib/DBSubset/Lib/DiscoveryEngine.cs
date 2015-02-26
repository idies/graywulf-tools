using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Microsoft.SqlServer.Management.Common;
using smo = Microsoft.SqlServer.Management.Smo;

namespace Jhu.Graywulf.DBSubset.Lib
{
    public class DiscoveryEngine
    {
        private SqlConnectionStringBuilder sourceDatabaseConnectionString;

        public SqlConnectionStringBuilder SourceDatabaseConnectionString
        {
            get { return sourceDatabaseConnectionString; }
        }

        public DiscoveryEngine()
        {
            InitializeMembers();
        }

        private void InitializeMembers()
        {
            this.sourceDatabaseConnectionString = new SqlConnectionStringBuilder();
        }

        public SubsetDefinition Execute()
        {
            SubsetDefinition sdef = new SubsetDefinition();
            sdef.SourceDatabaseConnectionString.ConnectionString = this.sourceDatabaseConnectionString.ConnectionString;

            smo::Server server = GetSmoServerObject();
            smo::Database db = server.Databases[sourceDatabaseConnectionString.InitialCatalog];
            db.PrefetchObjects();

            CollectTables(server, db, sdef);
            FindReferences(sdef);
            OrderTables(sdef);

            return sdef;
        }

        private void CollectTables(smo::Server server, smo::Database db, SubsetDefinition subsetDefinition)
        {
            smo::Scripter src = new smo::Scripter(server);
            
            // Add all tables
            List<smo::SqlSmoObject> tables = new List<smo.SqlSmoObject>();
            tables.AddRange(db.Tables.Cast<smo::SqlSmoObject>());

            smo::DependencyTree tree = src.DiscoverDependencies(tables.ToArray(), true);
            smo::DependencyWalker depwalker = new smo::DependencyWalker();
            smo::DependencyCollection depcoll = depwalker.WalkDependencies(tree);

            int q = 0;
            foreach (smo::DependencyCollectionNode dep in depcoll)
            {
                //Console.WriteLine(dep.Urn.GetAttribute("Name"));
                //Console.WriteLine(dep.Urn.ToString());
                smo::Table t = db.Tables[dep.Urn.GetAttribute("Name"), dep.Urn.GetAttribute("Schema")];

                Table nt = new Table(t, subsetDefinition);

                nt.Order = q;
                subsetDefinition.Tables.Add(nt.FullyQualifiedName, nt);

                q++;
            }
        }

        private void FindReferences(SubsetDefinition subsetDefinition)
        {
            foreach (Table t in subsetDefinition.Tables.Values)
            {
                t.ReferencedBy.Clear();
            }

            foreach (Table t in subsetDefinition.Tables.Values)
            {
                foreach (ForeignKey fk in t.ForeignKeys)
                {
                    fk.ReferencedTable.ReferencedBy.Add(t);
                }
            }
        }

        public static List<Table> FindAllReferencedTables(Table table)
        {
            List<Table> references = new List<Table>();
            references.AddRange(table.ForeignKeys.Select(t => t.ReferencedTable));

            foreach (Table t in table.ForeignKeys.Select(t => t.ReferencedTable))
            {
                references.AddRange(FindAllReferencedTables(t));
            }

            return references;
        }

        public static List<Table> FindAllReferencingTables(Table table)
        {
            List<Table> referencing = new List<Table>();
            referencing.AddRange(table.ReferencedBy);

            foreach (Table t in table.ReferencedBy)
            {
                referencing.AddRange(FindAllReferencingTables(t));
            }

            return referencing;
        }

        private void OrderTables(SubsetDefinition subsetDefinition)
        {
            Table[] tables = subsetDefinition.Tables.Values.ToArray();

            for (int i = 0; i < subsetDefinition.Tables.Count - 1; i++)
            {
                for (int j = subsetDefinition.Tables.Count - 1; j > i; j--)
                {
                    // Compare two tables
                    Table t1 = tables[j - 1];
                    Table t2 = tables[j];

                    foreach (ForeignKey fk in t1.ForeignKeys)
                    {
                        if (fk.ReferencedTable == t2)
                        {
                            tables[j - 1] = t2;
                            tables[j] = t1;
                            break;
                        }
                    }
                }
            }

            for (int i = 0; i < tables.Length; i++)
            {
                tables[i].Order = i;
            }
        }

        private smo::Server GetSmoServerObject()
        {
            // Create connection and add credentials if sql server login method is specified for the server instance
            using (SqlConnection cn = new SqlConnection(sourceDatabaseConnectionString.ConnectionString))
            {
                ServerConnection scn = new ServerConnection(cn);

                return new smo::Server(scn);
            }
        }
    }
}
