
namespace Savchin.Forms.ListView
{
    /// <summary>
    /// A FastObjectListView trades function for speed.
    /// </summary>
    /// <remarks>
    /// <para>On my mid-range laptop, this view builds a list of 10,000 objects in 0.1 seconds,
    /// as opposed to a normal ObjectListView which takes 10-15 seconds. Lists of up to 50,000 items should be
    /// able to be handled with sub-second response times even on low end machines.</para>
    /// <para>
    /// A FastObjectListView is implemented as a virtual list with some of the virtual modes limits (e.g. no sorting)
    /// fixed through coding. There are some functions that simply cannot be provided. Specifically, a FastObjectListView cannot:
    /// <list>
    /// <item>shows groups</item>
    /// <item>use Tile view</item>
    /// <item>display images on subitems</item>
    /// </list>
    /// </para>
    /// <para>You can circumvent the limit on subitem images by making the list owner drawn, and giving the column
    /// a Renderer of BaseRenderer, e.g. <code>myColumnWithImage.Renderer = new BaseRenderer();</code> </para>
    /// </remarks>
    public class FastObjectListView : VirtualObjectListView
    {
        /// <summary>
        /// Make a FastObjectListView
        /// </summary>
        public FastObjectListView()
        {
            DataSource = new FastObjectListDataSource(this);
        }

    }
}
