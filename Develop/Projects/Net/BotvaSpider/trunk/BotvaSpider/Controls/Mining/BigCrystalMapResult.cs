using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BotvaSpider.Controls.Mining
{
    public partial class BigCrystalMapResult : CrystalMapBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BigCrystalMapResult"/> class.
        /// </summary>
        public BigCrystalMapResult()
        {
            InitializeComponent();
            layout = tableLayoutPanel1;
        }

        /// <summary>
        /// Shows the specified combination.
        /// </summary>
        /// <param name="combination">The combination.</param>
        public void Show(IEnumerable<int> combination)
        {
            for (var row = 0; row < bigSize; row++)
                for (var column = 0; column < bigSize; column++)
                {
                    var control = layout.GetControlFromPosition(column, row);
                    control.Text = "0";
                    control.ForeColor = Color.Gray;
                }
            var cells = Cells;
            foreach (var position in combination)
            {
                var num = position - 1;
                var row = num / (cells);
                var column = num % cells;
                var control = layout.GetControlFromPosition(column, row);
              
                control.Text = "1";
                control.ForeColor = Color.Black;
            }
        }

        /// <summary>
        /// Shows the specified map.
        /// </summary>
        /// <param name="map">The map.</param>
        public void Show(Dictionary<int, int> map)
        {
            var val = map.Sum(e => e.Value) / (Cells * Cells);
            for (var row = 0; row < bigSize; row++)
                for (var column = 0; column < bigSize; column++)
                {
                    var control =  layout.GetControlFromPosition(column, row);
                    control.Text = "0";
                    control.ForeColor = Color.Gray;
                }
            var cells = Cells;

            foreach (var pair in map)
            {
                var num = pair.Key - 1;
                var row = num / (cells);
                var column = num % cells;
                var control = layout.GetControlFromPosition(column, row);
                control.ForeColor = (pair.Value > val) ? Color.Black : Color.Gray;
                control.Text = pair.Value.ToString();
            }
        }

        /// <summary>
        /// Clears this instance.
        /// </summary>
        internal void Clear()
        {
            foreach (var box in Controls.OfType<CheckBox>())
            {
                box.Checked = false;
            }

        }
    }
}