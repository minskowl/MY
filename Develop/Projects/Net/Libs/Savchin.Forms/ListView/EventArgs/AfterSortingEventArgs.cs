using System;
using System.Windows.Forms;

namespace Savchin.Forms.ListView
{
    public class AfterSortingEventArgs : EventArgs
    {
        public AfterSortingEventArgs(OLVColumn column, SortOrder order, OLVColumn column2, SortOrder order2)
        {
            this.columnToSort = column;
            this.sortOrder = order;
            this.secondaryColumnToSort = column2;
            this.secondarySortOrder = order2;
        }

        public OLVColumn ColumnToSort
        {
            get { return columnToSort; }
        }
        private OLVColumn columnToSort;

        public SortOrder SortOrder
        {
            get { return sortOrder; }
        }
        private SortOrder sortOrder;

        public OLVColumn SecondaryColumnToSort
        {
            get { return secondaryColumnToSort; }
        }
        private OLVColumn secondaryColumnToSort;

        public SortOrder SecondarySortOrder
        {
            get { return secondarySortOrder; }
        }
        private SortOrder secondarySortOrder;
    }
}