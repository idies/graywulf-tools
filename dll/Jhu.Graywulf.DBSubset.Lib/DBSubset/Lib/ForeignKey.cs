using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Jhu.Graywulf.DBSubset.Lib
{
    public class ForeignKey
    {
        private Table referencingTable;
        private List<string> referencingColumnNames;
        private Table referencedTable;
        private List<string> referencedColumnNames;

        //internal string tempReferencingTable;
        internal string tempReferencedTable;

        [XmlIgnore]
        public Table ReferencingTable
        {
            get { return referencingTable; }
            set { referencingTable = value; }
        }

        public List<string> ReferencingColumnNames
        {
            get { return referencingColumnNames; }
            set { referencingColumnNames = value; }
        }

        [XmlIgnore]
        public Table ReferencedTable
        {
            get { return referencedTable; }
            set { referencedTable = value; }
        }

        public List<string> ReferencedColumnNames
        {
            get { return referencedColumnNames; }
            set { referencedColumnNames = value; }
        }

        #region Properties for XML Serialization

        /*[XmlElement("ReferencingTable")]
        public string ReferencingTable_ForXml
        {
            get { return referencingTable.FullyQualifiedName; }
            set { this.tempReferencingTable = value; }
        }*/

        [XmlElement("ReferencedTable")]
        public string ReferencedTable_ForXml
        {
            get { return referencedTable.FullyQualifiedName; }
            set { this.tempReferencedTable = value; }
        }

        #endregion

        public ForeignKey()
        {
            InitializeMembers();
        }

        public ForeignKey(Table referencingTable)
        {
            InitializeMembers();

            this.referencingTable = referencingTable;
        }

        private void InitializeMembers()
        {
            this.referencingTable = null;
            this.referencingColumnNames = new List<string>();
            this.referencedTable = null;
            this.referencedColumnNames = new List<string>();
        }
    }
}
