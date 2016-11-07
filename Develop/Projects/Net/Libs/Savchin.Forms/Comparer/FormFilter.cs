using System;
using System.ComponentModel;
using System.Windows.Forms;
using Savchin.Comparer;

namespace Savchin.Forms.Comparer
{
    public partial class FormFilter : Form
    {
        private Filter filter;
        public FormFilter()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Edits the filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        public static void EditFilter(Filter filter)
        {
            using(FormFilter formFilter= new FormFilter())
            {
                formFilter.filter = filter;
             
                formFilter.dataGridView1.AutoGenerateColumns = true;
                formFilter.dataGridView1.AllowUserToAddRows = true;
                formFilter.dataGridView1.AllowUserToDeleteRows = true;

         

                formFilter.bindingSource1.DataSource = new BindingList<KeyMatcher>(filter.PrimitivesKeys);
                formFilter.ShowDialog();
            }
        }

        private void buttonApply_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
