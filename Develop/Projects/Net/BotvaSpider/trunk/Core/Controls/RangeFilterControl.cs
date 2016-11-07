using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BotvaSpider.Core;
using BotvaSpider.Data;
using Savchin.Core;

namespace BotvaSpider.Controls
{
    public partial class RangeFilterControl : UserControl
    {
        private IRange<int> value;
        /// <summary>
        /// Initializes a new instance of the <see cref="LevelFilterControl"/> class.
        /// </summary>
        public RangeFilterControl()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public string Title
        {
            get { return boxEnabled.Text; }
            set { boxEnabled.Text = value; }
        }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="LevelFilterControl"/> is checked.
        /// </summary>
        /// <value><c>true</c> if checked; otherwise, <c>false</c>.</value>
        public bool Checked
        {
            get { return boxEnabled.Checked; }
            set { boxEnabled.Checked = value; }
        }

        /// <summary>
        /// Gets the range.
        /// </summary>
        /// <returns></returns>
        public IRange<int> GetRange()
        {
            if (value == null) return null;
            value.SetValue((int)boxRivalLevelFrom.Value, (int)boxRivalLevelTo.Value);
            return value;
        }

        /// <summary>
        /// Shows the range.
        /// </summary>
        /// <param name="range">The range.</param>
        public void ShowRange(IRange<int> range)
        {
            if (range == null) return;
            value = range;
            boxRivalLevelFrom.Value = range.From;
            boxRivalLevelTo.Value = range.To;

        }

        public bool ValidateRange()
        {
            if (boxRivalLevelFrom.Value >= boxRivalLevelTo.Value)
            {
                MessageBox.Show(this,
                    "Уровень провтивника ОТ должен быть меньше чем ДО ",
                    "Ошибка", MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);
                boxRivalLevelFrom.Focus();
                return false;
            }
            return true;
        }

        private void boxEnabled_CheckedChanged(object sender, EventArgs e)
        {
            groupBoxRivalLevel.Enabled = boxEnabled.Checked;
        }


    }
}
