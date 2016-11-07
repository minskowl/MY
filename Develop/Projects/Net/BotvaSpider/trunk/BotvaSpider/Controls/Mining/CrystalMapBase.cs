using System.Windows.Forms;

namespace BotvaSpider.Controls.Mining
{
    public class CrystalMapBase : UserControl
    {
        private bool isSmall;
        protected TableLayoutPanel layout;
        protected const int bigSize = 6;
        private const int smallSize = 3;

        /// <summary>
        /// Gets or sets a value indicating whether this instance is big.
        /// </summary>
        /// <value><c>true</c> if this instance is big; otherwise, <c>false</c>.</value>
        public bool IsSmall
        {
            get { return isSmall; }
            set
            {
                isSmall = value;
                SetSize(Cells);
            }
        }

        /// <summary>
        /// Gets the cells.
        /// </summary>
        /// <value>The cells.</value>
        public int Cells
        {
            get { return isSmall ? smallSize : bigSize; }
        }

        private void SetSize(int size)
        {
            for (var row = 0; row < bigSize; row++)
                for (var column = 0; column < bigSize; column++)
                {
                    layout.GetControlFromPosition(column,row ).Visible = false;

                }
            for (var row = 0; row < size; row++)
                for (var column = 0; column < size; column++)
                {
                    layout.GetControlFromPosition(column,row ).Visible = true;

                }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // CrystalMapBase
            // 
            this.Name = "CrystalMapBase";
            this.Size = new System.Drawing.Size(197, 197);
            this.ResumeLayout(false);

        }
    }
}