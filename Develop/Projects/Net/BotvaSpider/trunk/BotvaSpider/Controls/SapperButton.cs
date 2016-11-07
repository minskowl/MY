using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BotvaSpider.Controls
{
    public class SapperButton : Button
    {
        /// <summary>
        /// Gets or sets the mine count.
        /// </summary>
        /// <value>The mine count.</value>
        public int MineCount { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SapperButton"/> class.
        /// </summary>
        public SapperButton()
        {
            MineCount = -1;
        }

        /// <summary>
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data.</param>
        protected override void OnClick(EventArgs e)
        {
            if (MineCount<8)
            {
                MineCount++;
                Text = MineCount.ToString();
            }
            else
            {
                MineCount = -1;
                Text = string.Empty;
            }
            base.OnClick(e);
        }
    }
}
