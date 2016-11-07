#region Version & Copyright
/* 
 * $Id: IDashboard.cs 19907 2007-08-14 10:18:32Z svd $ 
 *
 * <:COPYRIGHT_LICENSE:>
 */
#endregion

namespace Savchin.Web.UI
{
    public interface IDashboard
    {
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        string Title { get; set;}
        /// <summary>
        /// Gets the client ID.
        /// </summary>
        /// <value>The client ID.</value>
        string ClientID { get; }
        /// <summary>
        /// Gets the ID.
        /// </summary>
        /// <value>The ID.</value>
        string ID { get; }
        /// <summary>
        /// Gets the name of the JS object.
        /// </summary>
        /// <value>The name of the JS object.</value>
        string JSObjectName { get; }

        /// <summary>
        /// Gets the settings.
        /// </summary>
        /// <value>The settings.</value>
        DashboardSettings Settings { get; }
    }
}
