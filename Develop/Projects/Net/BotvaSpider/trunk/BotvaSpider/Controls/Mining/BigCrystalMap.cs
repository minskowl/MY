using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BotvaSpider.Controls.Mining;

namespace BotvaSpider.Controls
{
    public partial class BigCrystalMap : CrystalMapBase
    {



        public BigCrystalMap()
        {
            InitializeComponent();
            layout = tableLayoutPanel1;
        }

        /// <summary>
        /// Gets the result.
        /// </summary>
        /// <returns></returns>
        public List<int> GetResult()
        {
            var result = new List<int>();
            var cells = Cells;
            for (var row = 0; row < cells; row++)
                for (var column = 0; column < cells; column++)
                {
                    var box = (CheckBox)tableLayoutPanel1.GetControlFromPosition(column, row);
                    if (box.Checked) result.Add(row * cells + column + 1);
                }
            return result;
           
        }


        /// <summary>
        /// Clears this instance.
        /// </summary>
        internal void Clear()
        {
            for (var row = 0; row < bigSize; row++)
                for (var column = 0; column < bigSize; column++)
                {
                    var box = (CheckBox)tableLayoutPanel1.GetControlFromPosition(column, row);
                    box.Checked = false;
                }

        }


    }
}
