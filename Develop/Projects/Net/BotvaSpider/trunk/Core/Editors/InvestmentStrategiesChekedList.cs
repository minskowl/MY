using System;
using System.Windows.Forms;
using BotvaSpider.Core;
using Savchin.Core;
using Savchin.Forms.Helpers;

namespace BotvaSpider.Controls.Configuration.Accountant
{
    public class InvestmentStrategiesChekedList : CheckedListBox
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvestmentStrategiesChekedList"/> class.
        /// </summary>
        public InvestmentStrategiesChekedList()
        {
            if (!DesignMode)
            {
                this.Setup(typeof(InvestmentStrategy), new Enum[] { InvestmentStrategy.Undefined });
            }
        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        public InvestmentStrategy Value
        {
            get
            {
                var result = 0;

                for (var i = 0; i < Items.Count; i++)
                {
                    var data = (EnumData)Items[i];
                    if (GetItemChecked(i))
                    {
                        result = result | Convert.ToInt32(data.Value);
                    }
                }
                return (InvestmentStrategy)result;
            }
            set
            {

                for (var i = 0; i < Items.Count; i++)
                {
                    var data = (EnumData)Items[i];
                    SetItemChecked(i, value.IsSet(data.Value));
                }
            }
        }
    }
}
