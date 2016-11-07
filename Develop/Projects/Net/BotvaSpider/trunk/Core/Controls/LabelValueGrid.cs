using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BotvaSpider.Controls
{
    public partial class LabelValueGrid : UserControl
    {
        /// <summary>
        /// 
        /// </summary>
        DataTable data = new DataTable();
        /// <summary>
        /// Initializes a new instance of the <see cref="LabelValueGrid"/> class.
        /// </summary>
        public LabelValueGrid()
        {
            InitializeComponent();
            grid.AutoGenerateColumns = false;
            data.Columns.Add("Label", typeof(string));
            data.Columns.Add("Value", typeof(double));
        }

        /// <summary>
        /// Adds the row.
        /// </summary>
        /// <param name="label">The label.</param>
        /// <param name="value">The value.</param>
        public void AddRow(string label, object value)
        {
            if (value is double)
                data.Rows.Add(label, (double)value);
            else if (value is long)
                data.Rows.Add(label, (long)value);
            else
                data.Rows.Add(label, value);
        }

        /// <summary>
        /// Shows the data.
        /// </summary>
        public void ShowData()
        {
            grid.DataSource = data;
        }

        /// <summary>
        /// Clears this instance.
        /// </summary>
        public void Clear()
        {
            data.Rows.Clear();
            grid.DataSource = data;
        }
    }
}
