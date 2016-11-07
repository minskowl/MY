using System.ComponentModel;
using System.Windows.Forms;
using BotvaSpider.Core;
using Savchin.Forms.Helpers;

namespace BotvaSpider.Controls
{
    public class SkillCombo : ComboBox
    {
        /// <summary>
        /// Gets or sets the selected coulomb.
        /// </summary>
        /// <value>The selected coulomb.</value>
        [Browsable(false)]
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden)]
        public SkillType SelectedSkill
        {
            get
            {
                return (SkillType)((EnumData)this.SelectedItem).Value;
            }
            set
            {
                foreach (EnumData item in Items)
                {
                    if ((SkillType)item.Value == value)
                        SelectedItem = item;
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SkillCombo"/> class.
        /// </summary>
        public SkillCombo()
        {
            DropDownStyle = ComboBoxStyle.DropDownList;
            Items.Add(new EnumData(SkillType.Power));
            Items.Add(new EnumData(SkillType.Mastery));
            Items.Add(new EnumData(SkillType.Protection));
            Items.Add(new EnumData(SkillType.Dexterity));
            Items.Add(new EnumData(SkillType.Weight));
            SelectedIndex = 0;
        }
    }
}
