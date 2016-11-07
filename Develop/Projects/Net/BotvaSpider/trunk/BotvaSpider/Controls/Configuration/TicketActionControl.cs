using System;
using System.Windows.Forms;
using BotvaSpider.Configuration;
using BotvaSpider.Gears;
using Savchin.Forms.Helpers;

namespace BotvaSpider.Controls.Configuration
{
    public partial class TicketActionControl : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TicketActionControl"/> class.
        /// </summary>
        public TicketActionControl()
        {
            InitializeComponent();

            listActionType.Setup(typeof(TicketActionType));
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the listActionType control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void listActionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            panelPrice.Visible = ((TicketActionType)listActionType.GetValue() == TicketActionType.Sale);
        }

        /// <summary>
        /// Shows the specified action.
        /// </summary>
        /// <param name="action">The action.</param>
        public void Show(TicketAction action)
        {
            listActionType.SetValue(action.ActionType);
            boxPrice.Value = action.Price;
        }

        /// <summary>
        /// Gets the action.
        /// </summary>
        /// <param name="ticket">The ticket.</param>
        /// <returns></returns>
        public TicketAction GetAction(Ticket ticket)
        {
            return new TicketAction
                       {
                           TicketType = ticket,
                           Price = (int)boxPrice.Value,
                           ActionType = (TicketActionType)listActionType.GetValue()
                       };
        }
    }
}
