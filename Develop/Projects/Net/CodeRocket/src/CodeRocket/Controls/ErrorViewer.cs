using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Savchin.CodeGeneration;
using Savchin.CodeGeneration.Common;
using Savchin.Forms;


namespace CodeRocket.Controls
{
    public partial class ErrorViewer : UserControl, IErrorViewer
    {
        public ErrorViewer()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Shows the errors.
        /// </summary>
        /// <param name="errors">The errors.</param>
        public void ShowErrors(IEnumerable< Exception> errors)
        {
            var data = new List<DataRow>();
            foreach ( Exception ex in errors)
            {
                data.Add(new DataRow(ex));
            }
            dataGridView1.DataSource = data;
        }

        /// <summary>
        /// Shows the errors.
        /// </summary>
        /// <param name="errors">The errors.</param>
        public void ShowErrors(IDictionary<Generation, Exception> errors)
        {
            var data = new  List<DataRow>(errors.Count);

            foreach (KeyValuePair<Generation, Exception> pair in errors)
            {
                data.Add(new DataRow(pair)); 
            }

            dataGridView1.DataSource = data;
        }
        /// <summary>
        /// Handles the CellDoubleClick event of the dataGridView1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataRow data = (DataRow) dataGridView1.Rows[e.RowIndex].DataBoundItem;

            FormObject.ShowObject("Error " + data.GenerationName, data.Exception);
            
        }
        public struct DataRow
        {
            /// <summary>
            /// Gets or sets the type.
            /// </summary>
            /// <value>The type.</value>
            public string Type { get; set; }

            /// <summary>
            /// Gets or sets the message.
            /// </summary>
            /// <value>The message.</value>
            public string Message { get; set; }

            /// <summary>
            /// Gets or sets the exception.
            /// </summary>
            /// <value>The exception.</value>
            public Exception Exception { get; set; }

            /// <summary>
            /// Gets or sets the name of the generation.
            /// </summary>
            /// <value>The name of the generation.</value>
            public string GenerationName { get; set; }

            /// <summary>
            /// Initializes a new instance of the <see cref="DataRow"/> struct.
            /// </summary>
            /// <param name="ex">The ex.</param>
            public DataRow( Exception ex) : this()
            {
                GenerationName = string.Empty;
                Exception = ex;
                Type = ex.GetType().FullName;
                Message = ex.Message;
            }

            public DataRow(KeyValuePair<Generation, Exception> pair) : this()
            {
                GenerationName = pair.Key.TemplateFile;
                Type = pair.Value.GetType().FullName;
                Message = pair.Value.Message;
                Exception = pair.Value;
            }
        }


    }
}
