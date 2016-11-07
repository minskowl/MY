using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Savchin.Controls.Common;

namespace SchemaEditor.Controls
{
    public partial class ExceptionViewer : UserControl
    {
        public ExceptionViewer()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Shows the exception.
        /// </summary>
        /// <param name="exption">The exption.</param>
        public void ShowException(Exception exption)
        {
            List<DataRow> rows = new List<DataRow>();
            rows.Add(new DataRow(exption));
            dataGridView1.DataSource = rows;
        }

        /// <summary>
        /// Shows the exceptions.
        /// </summary>
        /// <param name="exptions">The exptions.</param>
        public void ShowExceptions(IEnumerable<Exception> exptions)
        {
            List<DataRow> rows = new List<DataRow>();
            foreach (Exception exption in exptions)
            {
                rows.Add(new DataRow(exption));

            }
            dataGridView1.DataSource = rows;

        }

        public struct DataRow
        {
            private string type;
            private string message;
            private Exception exception;

            public string Type
            {
                get { return type; }
                set { type = value; }
            }

            public string Message
            {
                get { return message; }
                set { message = value; }
            }

            public Exception Exception
            {
                get { return exception; }
                set { exception = value; }
            }
            public DataRow(Exception ex)
            {
                type = ex.GetType().FullName;
                message = ex.Message;
                exception = ex;
            }
        }

        /// <summary>
        /// Handles the CellClick event of the dataGridView1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataRow row = (DataRow)dataGridView1.Rows[e.RowIndex].DataBoundItem;
            //ExceptionForm.ShowException("View exception", "View exception", row.Exception);
            FormObject.ShowObject("View exception", row.Exception);
        }
    }
}
