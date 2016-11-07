using System;
using System.Windows.Forms;


namespace Savchin.Forms.RichText.Editor
{

    public partial class frmReplace : Form
    {
        // member variable pointing to main form
        private RichTextBox _doc;

        // default constructor
        public frmReplace()
        {
            InitializeComponent();
        }


        // overloaded constructor accepteing main form as
        // an argument
        public frmReplace(RichTextBox f)
        {
            InitializeComponent();
            _doc = f;
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




        private void btnReplace_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (_doc.SelectedText.Length != 0)
                {
                    _doc.SelectedText = txtReplacementText.Text;
                }

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



        private void btnReplaceAll_Click(object sender, System.EventArgs e)
        {

            try
            {
                _doc.Rtf = _doc.Rtf.Replace(txtSearchTerm.Text.Trim(), txtReplacementText.Text.Trim());


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

                StartPosition = _doc.Text.IndexOf(txtReplacementText.Text, SearchType);

                _doc.Select(StartPosition, txtReplacementText.Text.Length);
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