using System;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using BotvaSpider.Configuration;
using BotvaSpider.Core;
using Savchin.Core;

namespace BotvaSpider.Controls.Configuration.Accountant
{
    public partial class InvestmentStrategiesSelector : UserControl
    {


        /// <summary>
        /// Initializes a new instance of the <see cref="InvestmentStrategiesSelector"/> class.
        /// </summary>
        public InvestmentStrategiesSelector()
        {
            InitializeComponent();

        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        public InvestmentStrategy Value
        {
            get { return checkedListBox1.Value; }
            set { checkedListBox1.Value = value; }
        }


        private void buttonOk_Click(object sender, EventArgs e)
        {
            ((IWindowsFormsEditorService)Tag).CloseDropDown();
        }
    }
}