using System;
using System.Windows.Forms;
using Savchin.Comparer;


namespace Savchin.Forms.Comparer
{
    public partial class FormCompare : Form
    {
        public FormCompare()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Shows the results.
        /// </summary>
        /// <param name="result">The result.</param>
        public void ShowResults(ObjectResult result)
        {
            compareView1.ShowCompareResult(result);
        }
        private void compareView1_OkClick(object sender, EventArgs e)
        {
            Close();
        }

 

        private void compareView1_CancelClick(object sender, EventArgs e)
        {
            Close();
        }
    }
}
