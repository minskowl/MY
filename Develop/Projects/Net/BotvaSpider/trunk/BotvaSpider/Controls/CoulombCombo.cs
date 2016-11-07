using System;
using System.ComponentModel;
using System.Windows.Forms;
using BotvaSpider.Gears;
using Savchin.Forms.Helpers;

namespace BotvaSpider.Controls
{
    public class CoulombCombo : ComboBox
    {
        /// <summary>
        /// Gets or sets the selected coulomb.
        /// </summary>
        /// <value>The selected coulomb.</value>
        [Browsable(false)]
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden)]
        public Coulomb SelectedCoulomb
        {
            get
            {
                return (Coulomb)((EnumData)this.SelectedItem).Value;
            }
            set
            {
                foreach (EnumData item in Items)
                {
                    if ((Coulomb)item.Value == value)
                        SelectedItem = item;
                }
            }
        }


        /// <summary>
        /// Fills this instance.
        /// </summary>
        public void Fill()
        {
            var values = Enum.GetValues(typeof(Coulomb));
            foreach (Coulomb value in values)
            {
                Items.Add(new EnumData(value));
            }
        }



    }
}
