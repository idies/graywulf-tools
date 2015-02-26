using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Data.SqlClient;
using System.IO;

namespace Jhu.Graywulf.DBSubset.Lib
{
    public class SubsetDefinition
    {
        private SqlConnectionStringBuilder sourceDatabaseConnectionString;
        private SqlConnectionStringBuilder destinationDatabaseConnectionString;
        private Dictionary<string, Table> tables;

        private List<Table> tempTables;

        [XmlIgnore]
        public SqlConnectionStringBuilder SourceDatabaseConnectionString
        {
            get { return sourceDatabaseConnectionString; }
        }

        [XmlIgnore]
        public SqlConnectionStringBuilder DestinationDatabaseConnectionString
        {
            get { return destinationDatabaseConnectionString; }
        }

        [XmlIgnore]
        public Dictionary<string, Table> Tables
        {
            get { return tables; }
        }

        #region Properties for XML serializations

        [XmlElement("SourceDatabaseConnectionString")]
        public string SourceDatabaseConnectionString_ForXml
        {
            get { return sourceDatabaseConnectionString.ConnectionString; }
            set { sourceDatabaseConnectionString.ConnectionString = value; }
        }

        [XmlElement("DestinationDatabaseConnectionString")]
        public string DestinationDatabaseConnectionString_ForXml
        {
            get { return destinationDatabaseConnectionString.ConnectionString; }
            set { destinationDatabaseConnectionString.ConnectionString = value; }
        }

        [XmlArrayItem("Table")]
        public List<Table> Tables_ForXml
        {
            get
            {
                return tempTables;
            }
        }

        #endregion

        public SubsetDefinition()
        {
            InitializeMembers();
        }

        private void InitializeMembers()
        {
            this.sourceDatabaseConnectionString = new SqlConnectionStringBuilder();
            this.destinationDatabaseConnectionString = new SqlConnectionStringBuilder();
            this.tables = new Dictionary<string, Table>();

            this.tempTables = new List<Table>();
        }

        public void Save(TextWriter writer)
        {
            this.tempTables.Clear();
            this.tempTables.AddRange(this.tables.Values);

            XmlSerializer ser = new XmlSerializer(typeof(SubsetDefinition));
            ser.Serialize(writer, this);
        }

        public static SubsetDefinition Load(TextReader reader)
        {
            XmlSerializer ser = new XmlSerializer(typeof(SubsetDefinition));
            SubsetDefinition sdef = (SubsetDefinition)ser.Deserialize(reader);

            sdef.ResolveLinks();

            return sdef;
        }

        private void ResolveLinks()
        {
            // copy table list to dictionary
            this.tables.Clear();
            foreach (Table t in tempTables)
            {
                this.tables.Add(t.FullyQualifiedName, t);
            }
            tempTables.Clear();

            // update foreign key links

            foreach (Table t in tables.Values)
            {
                foreach (ForeignKey fk in t.ForeignKeys)
                {
                    fk.ReferencingTable = t;
                    fk.ReferencedTable = tables[fk.tempReferencedTable];
                    fk.ReferencedTable.ReferencedBy.Add(t);
                }
            }
                
        }
    }
}
