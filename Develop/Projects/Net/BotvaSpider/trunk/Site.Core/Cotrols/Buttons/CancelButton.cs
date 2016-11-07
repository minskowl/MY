
using System.ComponentModel;
using System.Web.UI.WebControls;
using Savchin.Web.UI;

namespace Site.Cotrols
{
    /// <summary>
    /// Cancel Button
    /// </summary>
    public class CancelButton : ButtonEx 
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CancelButton"/> class.
        /// </summary>
        public CancelButton()
        {
            CausesValidation = false;
            Text = "Отмена";
        }
    }
}