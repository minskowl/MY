namespace Savchin.Forms.ListView
{
    /// <summary>
    /// This class mimics the behavior of VirtualObjectListView v1.x.
    /// </summary>
    public class VirtualListVersion1DataSource : AbstractVirtualListDataSource
    {
        public VirtualListVersion1DataSource(VirtualObjectListView listView) : base(listView)
        {
        }

        #region Public properties

        /// <summary>
        /// How will the n'th object of the data source be fetched?
        /// </summary>
        public RowGetterDelegate RowGetter { get; set; }

        #endregion

        #region IVirtualListDataSource implementation

        public override object GetNthObject(int n)
        {
            if (RowGetter == null)
                return null;
            else
                return RowGetter(n);
        }

        public override int SearchText(string value, int first, int last, OLVColumn column)
        {
            return DefaultSearchText(value, first, last, column, this);
        }

        #endregion
    }
}