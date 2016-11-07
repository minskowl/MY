using System.Windows.Forms;
using Savchin.Forms.Docking;

namespace BotvaSpider.Controls
{
    public class MainFormBase : Form
    {
        /// <summary>
        /// Gets the dock panel.
        /// </summary>
        /// <value>The dock panel.</value>
        public virtual DockPanel DockPanel
        {
            get { return null; }
        }

        /// <summary>
        /// Hides the alert.
        /// </summary>
        public virtual void HideAlert()
        {

        }

        /// <summary>
        /// Adds the console.
        /// </summary>
        /// <param name="name">The name.</param>
        public virtual void AddConsole<T>(string name) where T : DockContent, new()
        {

        }
    }
}
