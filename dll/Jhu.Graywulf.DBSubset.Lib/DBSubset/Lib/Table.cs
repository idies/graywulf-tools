using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using smo = Microsoft.SqlServer.Management.Smo;

namespace Jhu.Graywulf.DBSubset.Lib
{
    public class Table
    {
        private int order;
        private string schema;
        private string name;
        private double samplingFactor;
        private SamplingMethod samplingMethod;
        private List<string> primaryKeyColumns;
        private List<string> primaryKeyColumnTypes;
        private bool hasIdentityProperty;
        private List<string> columns;
        private ForeignKeyCollection foreignKeys;
        private List<Table> referencedBy;
        private double dataSpaceUsed;
        private long rowCount;

        public int Order
        {
            get { return order; }
            set { order = value; }
        }

        public string Schema
        {
            get { return schema; }
            set { schema = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public double SamplingFactor
        {
            get { return samplingFactor; }
            set { samplingFactor = value; }
        }

        public SamplingMethod SamplingMethod
        {
            get { return samplingMethod; }
            set { samplingMethod = value; }
        }

        public List<string> PrimaryKeyColumns
        {
            get { return primaryKeyColumns; }
        }

        public List<string> PrimaryKeyColumnTypes
        {
            get { return primaryKeyColumnTypes; }
        }

        public bool HasIdentityProperty
        {
            get { return hasIdentityProperty; }
            set { hasIdentityProperty = value; }
        }

        public List<string> Columns
        {
            get { return columns; }
        }

        public ForeignKeyCollection ForeignKeys
        {
            get { return foreignKeys; }
        }

        [XmlIgnore]
        public List<Table> ReferencedBy
        {
            get { return referencedBy; }
        }

        public double DataSpaceUsed
        {
            get { return dataSpaceUsed; }
            set { dataSpaceUsed = value; }
        }

        public long RowCount
        {
            get { return rowCount; }
            set { rowCount = value; }
        }

        [XmlIgnore]
        public string FullyQualifiedName
        {
            get { return String.Format("[{0}].[{1}]", schema, name); }
        }

        public Table()
        {
            InitializeMembers();
        }

        public Table(smo::Table table, SubsetDefinition subsetDefinition)
        {
            InitializeMembers();
            LoadFromSmoObject(table, subsetDefinition);
        }

        private void InitializeMembers()
        {
            this.order = -1;
            this.schema = null;
            this.name = null;
            this.samplingFactor = 0;
            this.samplingMethod = SamplingMethod.None;
            this.primaryKeyColumns = new List<string>();
            this.primaryKeyColumnTypes = new List<string>();
            this.hasIdentityProperty = false;
            this.columns = new List<string>();
            this.foreignKeys = new ForeignKeyCollection(this);
            this.referencedBy = new List<Table>();
        }

        private string GetDataTypeString(smo::DataType type)
        {
            var dt = Jhu.Graywulf.Schema.SqlServer.SqlServerDataset.CreateDataType(type);
            return dt.TypeNameWithLength;
        }

        private void LoadFromSmoObject(smo::Table t, SubsetDefinition subsetDefinition)
        {
            this.Schema = t.Schema;
            this.Name = t.Name;
            this.dataSpaceUsed = t.DataSpaceUsed;
            this.rowCount = t.RowCount;

            foreach (smo::Column c in t.Columns)
            {
                this.Columns.Add(c.Name);
            }

            // Find primary key
            foreach (smo::Index i in t.Indexes)
            {
                if (i.IndexKeyType == smo::IndexKeyType.DriPrimaryKey)
                {
                    foreach (smo::IndexedColumn ic in i.IndexedColumns)
                    {
                        this.primaryKeyColumns.Add(ic.Name);
                        this.primaryKeyColumnTypes.Add(GetDataTypeString(t.Columns[ic.Name].DataType));

                        if (t.Columns[ic.Name].Identity)
                        {
                            this.hasIdentityProperty = true;
                        }
                    }

                    
                }
            }

            // Find foreign keys
            foreach (smo::ForeignKey fk in t.ForeignKeys)
            {
                ForeignKey nfk = new ForeignKey();

                string tablename = String.Format("[{0}].[{1}]",
                    fk.ReferencedTableSchema, fk.ReferencedTable);

                nfk.ReferencedTable = subsetDefinition.Tables[tablename];

                foreach (smo::ForeignKeyColumn fkc in fk.Columns)
                {
                    nfk.ReferencingColumnNames.Add(fkc.Name);
                    nfk.ReferencedColumnNames.Add(fkc.ReferencedColumn);
                }

                this.ForeignKeys.Add(nfk);
            }
        }
    }
}
