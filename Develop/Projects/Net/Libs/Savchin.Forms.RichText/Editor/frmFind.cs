using System;
using System.Windows.Forms;

namespace Savchin.Forms.RichText.Editor
{
    public partial class frmFind : Form
    {
        // local member variable to hold main form

        private RichTextBox _doc;

        // default constructor
        public frmFind()
        {
            InitializeComponent();
        }


        // overloaded constructor - permits passing in main form
        public frmFind(RichTextBox doc)
            : this()
        {
            _doc = doc;

        }




        private void btnFind_Click(object sender, System.EventArgs e)
        {
            try
            {
                int StartPosition;
                StringComparison SearchType;

                if (chkMatchCase.Checked == true)
                {
                    SearchType = StringComparison.Ordinal;
                }
                else
                {
                    SearchType = StringComparison.OrdinalIgnoreCase;
                }

                StartPosition = _doc.Text.IndexOf(txtSearchTerm.Text, SearchType);

                if (StartPosition == 0)
                {
                    MessageBox.Show("String: " + txtSearchTerm.Text.ToString() + " not found", "No Matches", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return;
                }

                _doc.Select(StartPosition, txtSearchTerm.Text.Length);
                _doc.ScrollToCaret();
                Owner.Focus();
                btnFindNext.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error");
            }
        }




        private void btnFindNext_Click(object sender, System.EventArgs e)
        {
            try
            {
                int StartPosition = _doc.SelectionStart + 2;

                StringComparison SearchType;

                if (chkMatchCase.Checked == true)
                {
                    SearchType = StringComparison.Ordinal;
                }
                else
                {
                    SearchType = StringComparison.OrdinalIgnoreCase;
                }

                //StartPosition = Microsoft.VisualBasic.Strings.InStr(StartPosition, mMain.rtbDoc.Text, txtSearchTerm.Text, SearchType);
                StartPosition = _doc.Text.IndexOf(txtSearchTerm.Text, StartPosition, SearchType);

                if (StartPosition == 0 || StartPosition < 0)
                {
                    MessageBox.Show("String: " + txtSearchTerm.Text.ToString() + " not found", "No Matches", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return;
                }

                _doc.Select(StartPosition, txtSearchTerm.Text.Length);
                _doc.ScrollToCaret();
                Owner.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error");
            }
        }


        private void txtSearchTerm_TextChanged(object sender, EventArgs e)
        {
            btnFindNext.Enabled = false;
        }


    }
}