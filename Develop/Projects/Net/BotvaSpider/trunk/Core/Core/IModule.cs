using System.Windows.Forms;
using BotvaSpider.Controls;

namespace BotvaSpider.Core
{
    public interface IModule
    {
        /// <summary>
        /// Initilaizes this instance.
        /// </summary>
        /// <param name="form">The form.</param>
        /// <param name="statisticsMenu">The statistics menu.</param>
        void Initilaize(MainFormBase form, ToolStripMenuItem statisticsMenu);

    }
}
