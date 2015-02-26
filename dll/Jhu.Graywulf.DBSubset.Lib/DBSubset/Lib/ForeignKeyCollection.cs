using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jhu.Graywulf.DBSubset.Lib
{
    public class ForeignKeyCollection : List<ForeignKey>
    {
        private Table referencingTable;

        public ForeignKeyCollection(Table referencingTable)
            : base()
        {
            this.referencingTable = referencingTable;
        }

        public new void Add(ForeignKey item)
        {
            item.ReferencingTable = referencingTable;
            base.Add(item);
        }
    }
}
