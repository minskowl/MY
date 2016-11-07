using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Savchin.Sudoku.Controls
{
    public class Field : ComboBox
    {
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        public int Value
        {
            get { return SelectedIndex; }
            set { SelectedIndex = value; }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is variant.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is variant; otherwise, <c>false</c>.
        /// </value>
        public bool isVariant
        {
            get { return BackColor == Color.Green; }
        }

        /// <summary>
        /// Gets or sets the dificulty.
        /// </summary>
        /// <value>The dificulty.</value>
        public Difficulty Difficulty
        {
            get { return (Difficulty)(Items.Count - 1); }
            set
            {
                Items.Clear();
                Items.Add(" ");
                for (var i = 1; i <= (int)value; i++)
                {
                    Items.Add(i.ToString("x"));
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Field"/> class.
        /// </summary>
        public Field()
        {
            DropDownStyle = ComboBoxStyle.DropDownList;
            Difficulty = Difficulty.F4;
        }
    }
}
