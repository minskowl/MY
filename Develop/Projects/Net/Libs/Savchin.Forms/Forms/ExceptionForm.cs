using System;
using System.Windows.Forms;
using Savchin.Text;

namespace Savchin.Forms
{
    public partial class ExceptionForm : Form
    {
        /// <summary>
        /// Gets or sets a value indicating whether [exception showed].
        /// </summary>
        /// <value><c>true</c> if [exception showed]; otherwise, <c>false</c>.</value>
        public Boolean ExceptionShowed
        {
            get { return txtException.Visible; }
            set
            {
                txtException.Visible = value;
                if (value)
                {
                    tlpText.RowStyles[0].Height = 30;
                    tlpText.RowStyles[1].Height = 70;
                    Height = Height + 200;
                }
                else
                {
                    tlpText.RowStyles[0].Height = 100;
                    tlpText.RowStyles[1].Height = 0;
                    Height = Height - 200;
                }


            }

        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionForm"/> class.
        /// </summary>
        public ExceptionForm()
        {
            InitializeComponent();

            ExceptionShowed = false;
        }

        
        /// <summary>
        /// Shows the exception.
        /// </summary>
        /// <param name="Title">The title.</param>
        /// <param name="Message">The message.</param>
        /// <param name="ex">The ex.</param>
        /// <returns></returns>
        public static ExceptionForm ShowException(String Title, String Message, Exception ex)
        {
            ExceptionForm f = new ExceptionForm();
            f.Text = Title;
            f.txtException.Text = StringUtil.ToString(ex);
            f.txtMessage.Text = Message;
            f.ShowDialog();
            return f;
        }


        /// <summary>
        /// Handles the Click event of the bClose control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void bClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Handles the Click event of the bShowException control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void bShowException_Click(object sender, EventArgs e)
        {
            ExceptionShowed = !ExceptionShowed;
        }
    }
}