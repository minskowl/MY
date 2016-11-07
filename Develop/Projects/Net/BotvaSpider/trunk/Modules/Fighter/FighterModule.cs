using System.Windows.Forms;
using BotvaSpider.Consoles;
using BotvaSpider.Controls;
using BotvaSpider.Core;

namespace BotvaSpider.Fighter
{
    class FighterModule : IModule
    {
        /// <summary>
        /// Initilaizes this instance.
        /// </summary>
        /// <param name="form">The form.</param>
        /// <param name="statisticsMenu">The statistics menu.</param>
        public void Initilaize(MainFormBase form, ToolStripMenuItem statisticsMenu)
        {
            form.AddConsole<FightingConsole>("Бодалка");
            form.AddConsole<CowsFarmConsole>("Ферма");
        }
    }
}