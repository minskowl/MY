using System.Windows.Forms;
using BotvaSpider.Consoles;
using BotvaSpider.Controls;
using BotvaSpider.Core;

namespace BotvaSpider.Tools.Core
{
    class ToolsModule : IModule
    {
        /// <summary>
        /// Initilaizes this instance.
        /// </summary>
        /// <param name="form">The form.</param>
        /// <param name="statisticsMenu">The statistics menu.</param>
        public void Initilaize(MainFormBase form, ToolStripMenuItem statisticsMenu)
        {
            form.AddConsole<AccountantConsole>("Бухгалтерия");
            form.AddConsole<TopSearchConsole>("Поиск в ТОПе");
            form.AddConsole<ToolsConsole>("Утилиты");
        }
    }
}