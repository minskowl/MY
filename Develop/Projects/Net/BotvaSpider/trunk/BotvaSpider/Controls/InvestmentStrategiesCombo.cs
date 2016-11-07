using System.ComponentModel;
using System.Windows.Forms;
using BotvaSpider.Core;
using Savchin.Forms.Helpers;

namespace BotvaSpider.Controls
{
    public class InvestmentStrategiesCombo : ComboBox
    {
        /// <summary>
        /// Gets or sets the selected coulomb.
        /// </summary>
        /// <value>The selected coulomb.</value>
        [Browsable(false)]
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden)]
        public InvestmentStrategy SelectedStrategy
        {
            get
            {
                return (InvestmentStrategy)((EnumData)this.SelectedItem).Value;
            }
            set
            {
                foreach (EnumData item in Items)
                {
                    if ((InvestmentStrategy)item.Value == value)
                        SelectedItem = item;
                }
            }
        }



        /// <summary>
        /// Initializes a new instance of the <see cref="InvestmentStrategiesCombo"/> class.
        /// </summary>
        public InvestmentStrategiesCombo()
        {
            DropDownStyle=ComboBoxStyle.DropDownList;
            this.Setup(typeof (InvestmentStrategy));
            SelectedIndex = 0;
        }

    }
}
