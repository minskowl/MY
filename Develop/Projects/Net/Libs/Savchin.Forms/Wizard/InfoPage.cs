using System.ComponentModel;
using System.Windows.Forms;

namespace Savchin.Forms.Wizard
{

    /// <summary>
    /// An inherited <see cref="InfoContainer"/> that contains a <see cref="Label"/> 
    /// with the description of the page.
    /// </summary>
    public partial class InfoPage : InfoContainer
    {
        /// <summary>
        /// Gets/Sets the text on the info page
        /// </summary>
        [Category("Appearance")]
        public string PageText
        {
            get
            {
                return lblDescription.Text;
            }
            set
            {
                lblDescription.Text = value;
            }
        }

        /// <summary>
        /// Default Constructor
        /// </summary>
        public InfoPage()
        {
            // This call is required by the Windows Form Designer.
            InitializeComponent();


        }

    }
}

