using System.Windows.Forms;

namespace BotvaSpider.Controls
{
    public partial class FormText : Form
    {
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        public string Value
        {
            get { return textBox1.Text; }
            set { textBox1.Text = value; }
        }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="FormText"/> is multiline.
        /// </summary>
        /// <value><c>true</c> if multiline; otherwise, <c>false</c>.</value>
        public bool Multiline
        {
            get { return textBox1.Multiline; }
            set { textBox1.Multiline = value; }
        }

        public FormText()
        {
            InitializeComponent();
        }
    }
}
