using System.Web.UI;
using System.Web.UI.WebControls;

namespace Savchin.Web.UI
{
    /// <summary>
    /// CheckBox Template Column
    /// </summary>
    public class CheckBoxColumn : ITemplate
    {

        /// <summary>
        /// When implemented by a class, defines the <see cref="T:System.Web.UI.Control"></see> object that child controls and templates belong to. These child controls are in turn defined within an inline template.
        /// </summary>
        /// <param name="container">The <see cref="T:System.Web.UI.Control"></see> object to contain the instances of controls from the inline template.</param>
        public void InstantiateIn(Control container)
        {
            CheckBox checkBox = new CheckBox();
            container.Controls.Add(checkBox);
        }
    }
}
