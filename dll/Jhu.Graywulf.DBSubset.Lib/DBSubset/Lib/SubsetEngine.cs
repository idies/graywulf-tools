using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using smo = Microsoft.SqlServer.Management.Smo;

namespace Jhu.Graywulf.DBSubset.Lib
{
    public class SubsetEngine
    {
        private TextWriter log;
        private SubsetDefinition subsetDefinition;

        public SubsetEngine(SubsetDefinition subsetDefinition)
        {
            InitializeMembers();

            this.subsetDefinition = subsetDefinition;
        }

        private void InitializeMembers()
        {
        }

        public void Execute()
        {
            foreach (Table t in subsetDefinition.Tables.Values.OrderBy(t => t.Order))
            {
                SubsetTable(t);
            }
        }

        public void GenerateScript(TextWriter writer)
        {
            foreach (Table t in subsetDefinition.Tables.Values.OrderBy(t => t.Order))
            {
                if (t.SamplingMethod != SamplingMethod.None)
                {
                    writer.WriteLine("-- SUBSAMPLING TABLE '{0}' ---", t.Name);
                    writer.WriteLine();
                    writer.WriteLine(GetTableCopyQuery(t));
                    writer.WriteLine();
                    writer.WriteLine("GO");
                    writer.WriteLine();
                }
            }
        }

        private void SubsetTable(Table table)
        {
            string sql = GetTableCopyQuery(table);

            using (SqlConnection cn = new SqlConnection(subsetDefinition.DestinationDatabaseConnectionString.ConnectionString))
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cmd.CommandTimeout = 1500;

                    int numrows = cmd.ExecuteNonQuery();
                }
            }
        }

        private string GetCommaSeparatedList(List<string> list, string alias)
        {
            string res = null;

            foreach (string value in list)
            {
                if (res == null)
                {
                    res = "";
                }
                else
                {
                    res += ", ";
                }

                if (alias != null)
                {
                    res += String.Format("{0}.[{1}]", alias, value);
                }
                else
                {
                    res += String.Format("[{1}]", alias, value);
                }
            }

            return res;
        }

        private string GetTableCopyQuery(Table table)
        {
            StringBuilder sql = new StringBuilder();

            if (table.HasIdentityProperty)
            {
                sql.Append(ScriptTemplates.SetIdentityInsertQuery.Replace("[$IdentityInsertState]", "ON"));
            }

            if (table.SamplingMethod == SamplingMethod.Random)
            {
                sql.Append(ScriptTemplates.RandomSubsetQuery);
            }
            else
            {
                sql.Append(ScriptTemplates.FullTableQuery);
            }

            if (table.HasIdentityProperty)
            {
                sql.Append(ScriptTemplates.SetIdentityInsertQuery.Replace("[$IdentityInsertState]", "OFF"));
            }

            // --- Temporary table columns

            string tempcol = "";

            for (int i = 0; i < table.PrimaryKeyColumns.Count; i++)
            {
                if (i != 0)
                {
                    tempcol += ", ";
                }

                tempcol += String.Format("[{0}] {1}", 
                                         table.PrimaryKeyColumns[i],
                                         table.PrimaryKeyColumnTypes[i]);
            }

            sql.Replace("[$TempTableColumns]", tempcol);

            // --- Primary key columns

            sql.Replace("[$PrimaryKeyColumns]",
                        GetCommaSeparatedList(table.PrimaryKeyColumns, null));
            sql.Replace("[$PrimaryKeyColumnsAlias]",
                        GetCommaSeparatedList(table.PrimaryKeyColumns, "sourcetablealias"));
            // --- Source table name 

            sql.Replace("[$SourceTable]",
                        String.Format("{0}[{1}].[{2}].[{3}]",
                                      GetLinkedServerName(),
                                      subsetDefinition.SourceDatabaseConnectionString.InitialCatalog,
                                      table.Schema,
                                      table.Name));

            sql.Replace("[$InsertColumnList]", GetCommaSeparatedList(table.Columns, null));
            sql.Replace("[$SelectColumnList]", GetCommaSeparatedList(table.Columns, "sourcetablealias"));

            // --- Foreign key constraints

            string join = "";

            if (table.ForeignKeys.Count > 0)
            {
                int q = 0;
                foreach (ForeignKey fk in table.ForeignKeys)
                {
                    string on = "";
                    for (int c = 0; c < fk.ReferencingColumnNames.Count; c++)
                    {
                        on += String.Format(" AND [foreigntable_{0}].[{1}] = sourcetablealias.[{2}] ",
                                            q,
                                            fk.ReferencedColumnNames[c],
                                            fk.ReferencingColumnNames[c]);
                    }

                    join += String.Format(@"INNER JOIN [{1}].[{2}].[{3}] foreigntable_{0} ON {4}",
                         q,
                         subsetDefinition.DestinationDatabaseConnectionString.InitialCatalog,
                         fk.ReferencedTable.Schema,
                         fk.ReferencedTable.Name,
                         on.Substring(5));

                    q++;
                }
            }

            sql.Replace("[$SourceTableJoins]", join);

            // --- Sampling factor

            sql.Replace("[$SamplingFactor]", table.SamplingFactor.ToString());

            sql.Replace("[$DestinationTable]",
                        String.Format("[{0}].[{1}].[{2}]",
                                      subsetDefinition.DestinationDatabaseConnectionString.InitialCatalog,
                                      table.Schema,
                                      table.Name));

            // --- Primary key join condition
            if (table.PrimaryKeyColumns.Count > 0)
            {
                string primarykeyjoincondition = "";

                foreach (string pk in table.PrimaryKeyColumns)
                {
                    primarykeyjoincondition +=
                        String.Format(" AND ##temporaryidlist.[{0}] = sourcetablealias.[{0}]", pk);
                }

                sql.Replace("[$PrimaryKeyJoinCondition]", primarykeyjoincondition.Substring(5));
            }

            return sql.ToString();
        }

        /*
        private string GetRandomSubsetCopyQuery(Table table)
        {
            string sql = string.Format(
@"INSERT [{0}].[{1}].[{2}] WITH (TABLOCKX)
  SELECT t.*
  FROM   {3}[{4}].[{5}].[{6}] t WITH (NOLOCK)
  CROSS APPLY master.dbo.Random(t.[{7}]) r
  WHERE r.Random < {8}",
                                    subsetDefinition.DestinationDatabaseConnectionString.InitialCatalog,
                                    table.Schema,
                                    table.Name,
                                    GetLinkedServerName(),
                                    subsetDefinition.SourceDatabaseConnectionString.InitialCatalog,
                                    table.Schema,
                                    table.Name,
                                    table.PrimaryKeyColumns[0],     //****
                                    table.SamplingFactor.ToString());

            return sql;
        }
         * */

        /*
        private string GetForeignKeyCopyQuery(Table table)
        {

            string sql = String.Format(
@"INSERT [{0}].[{1}].[{2}] WITH (TABLOCKX)
  SELECT t.*
  FROM   {3}[{4}].[{5}].[{6}] t WITH (NOLOCK)",
                                     subsetDefinition.DestinationDatabaseConnectionString.InitialCatalog,
                                     table.Schema,
                                     table.Name,
                                     GetLinkedServerName(),
                                     subsetDefinition.SourceDatabaseConnectionString.InitialCatalog,
                                     table.Schema,
                                     table.Name);

            int q = 0;
            foreach (ForeignKey fk in table.ForeignKeys)
            {
                string on = "";
                for (int c = 0; c < fk.ReferencingColumnNames.Count; c ++)
                {
                    on += String.Format(" AND f{0}.{1} = t.{2} ",
                                        q,
                                        fk.ReferencedColumnNames[c],
                                        fk.ReferencingColumnNames[c]);
                }

                sql += String.Format(@"INNER JOIN {1}.{2}.{3} f{0} ON {4}",
                     q,
                     subsetDefinition.DestinationDatabaseConnectionString.InitialCatalog,
                     fk.ReferencedTable.Schema,
                     fk.ReferencedTable.Name,
                     on.Substring(5));

                q++;
            }

            return sql;
        }
         * */

        private string GetLinkedServerName()
        {
            /*
            string linkedserver = "";

            if (String.Compare(subsetDefinition.SourceDatabaseConnectionString.DataSource,
                               subsetDefinition.DestinationDatabaseConnectionString.DataSource,
                               true) != 0)
            {
                linkedserver = "[" + subsetDefinition.SourceDatabaseConnectionString.DataSource + "]";
            }

            return linkedserver;
             * */

            return "";
        }
    }
}
