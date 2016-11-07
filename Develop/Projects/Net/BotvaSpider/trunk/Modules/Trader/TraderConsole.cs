using System;
using System.Threading;
using BotvaSpider.BookKeeping;
using BotvaSpider.Data;

namespace BotvaSpider.Consoles
{

    public partial class TraderConsole : ControllerConsole
    {
        private TradeMaster _master;
        private Player player;


        /// <summary>
        /// Initializes a new instance of the <see cref="TraderConsole"/> class.
        /// </summary>
        public TraderConsole()
        {
            InitializeComponent();
  
        }

        private void InitializeTradeMaster()
        {
            if (_master==null)
            {
            
             
                player = new Player(Controller);
                _master = new TradeMaster(player, Controller);
                _master.ItemMatched += _master_ItemMatched;
                //tradeSearcherControl1.Show(_master.Matchers);
            }
            _master.Player.Update();
            tradeSearcherControl1.Save(_master.Matchers);
        }

        /// <summary>
        /// Handles the Click event of the searchInMarketToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void searchInMarketToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InitializeTradeMaster();
            _master.StartSearch();
        }

      
        /// <summary>
        /// Handles the Click event of the investMoneyToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void investMoneyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InitializeTradeMaster();
            _master.InvestmentMoney();
        }


        /// <summary>
        /// Handles the ItemMatched event of the _master control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ItemMatchedEventArgs"/> instance containing the event data.</param>
        void _master_ItemMatched(object sender, ItemMatchedEventArgs e)
        {
            AddLog("Лот найден: " + e.Condition + " " + e.Price);
        }

        private void AddLog(string message)
        {
            textBox1.AppendText(message + Environment.NewLine);
        }
    }
}
