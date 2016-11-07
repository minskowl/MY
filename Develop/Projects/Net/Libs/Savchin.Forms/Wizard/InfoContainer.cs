using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Savchin.Forms.Wizard
{
    /// <summary>
    /// Summary description for UserControl1.
    /// </summary>
    [Designer(typeof (InfoContainerDesigner))]
    public partial class InfoContainer : UserControl
    {
       
        private Label lblTitle;
        private PictureBox picImage;

        /// <summary>
        /// 
        /// </summary>
        public InfoContainer()
        {
            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();
        }

        /// <summary>
        /// Get/Set the title for the info page
        /// </summary>
        [Category("Appearance")]
        public string PageTitle
        {
            get { return lblTitle.Text; }
            set { lblTitle.Text = value; }
        }


        /// <summary>
        /// Gets/Sets the Icon
        /// </summary>
        [Category("Appearance")]
        public Image Image
        {
            get { return picImage.Image; }
            set { picImage.Image = value; }
        }


        private void InfoContainer_Load(object sender, EventArgs e)
        {
            //Handle really irating resize that doesn't take account of Anchor
            lblTitle.Left = picImage.Width + 8;
            lblTitle.Width = (Width - 4) - lblTitle.Left;
        }


    }
}