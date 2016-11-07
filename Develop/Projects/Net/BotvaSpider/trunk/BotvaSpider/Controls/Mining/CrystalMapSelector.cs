using System.Collections.Generic;

namespace BotvaSpider.Controls.Mining
{
    public partial class CrystalMapSelector : CrystalMapBase
    {
        public CrystalMapSelector()
        {
            InitializeComponent();

            layout = tableLayoutPanel1;
        }

        /// <summary>
        /// Gets the result.
        /// </summary>
        /// <returns></returns>
        public List<CrystalState> GetResult()
        {
            var result = new List<CrystalState>();
            var cells = Cells;
            for (var row = 0; row < cells; row++)
                for (var column = 0; column < cells; column++)
                {
                    var box = (CrystalButton)tableLayoutPanel1.GetControlFromPosition(column, row);
                    result.Add(box.State);
                    //if (box.Checked) result.Add(row * cells + column + 1);
                }
            return result;

        }
    }
}