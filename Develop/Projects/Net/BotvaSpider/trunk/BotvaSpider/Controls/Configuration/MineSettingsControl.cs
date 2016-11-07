using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using BotvaSpider.Configuration;
using BotvaSpider.Gears;
using Savchin.Core;

namespace BotvaSpider.Controls.Configuration
{
    public partial class MineSettingsControl : UserControl
    {
        private List<Range<int>> data;
        /// <summary>
        /// Initializes a new instance of the <see cref="MineSettingsControl"/> class.
        /// </summary>
        public MineSettingsControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Shows the specified settings.
        /// </summary>
        /// <param name="settings">The settings.</param>
        public void Show(MinerSettings settings)
        {
            boxUseMine.Checked = settings.VisitMine;
            boxUseGlasses.Checked = settings.UseGlasses;
            boxUseHelmet.Checked = settings.UseHelmet;
            boxSearchUntilTry.Checked = settings.SearchUntilTry;

            smallTicketAction.Show(settings.GetTicketAction(Ticket.Small));
            bigTicketAction.Show(settings.GetTicketAction(Ticket.Big));

            data = settings.SearchCrystalLimits.ToList();
            gridLimits.DataSource = new BindingList<Range<int>>(data);
 
        }

        /// <summary>
        /// Saves the specified settings.
        /// </summary>
        /// <param name="settings">The settings.</param>
        public void Save(MinerSettings settings)
        {
            settings.VisitMine = boxUseMine.Checked;
            settings.UseGlasses = boxUseGlasses.Checked;
            settings.UseHelmet = boxUseHelmet.Checked;
            settings.SearchUntilTry = boxSearchUntilTry.Checked;

            settings.SearchCrystalLimits.Clear();
            settings.SearchCrystalLimits.AddRange(data);


            settings.TicketActions.Clear();
            settings.TicketActions.Add(smallTicketAction.GetAction(Ticket.Small));
            settings.TicketActions.Add(bigTicketAction.GetAction(Ticket.Big));
        }
    }
}
