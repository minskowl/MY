using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Savchin.TimeManagment;

namespace Savchin.Forms
{
    public partial class DateRangeControl : UserControl
    {
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        public DateRange Value
        {
            get
            {
                return new DateRange(timeFrom.Value.Date, timeTo.Value.Date.AddDays(1).AddMilliseconds(-1));
            }
            set
            {
                timeFrom.Value = value.From;
                timeTo.Value = value.To;
            }
        }
        public DateRangeControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Validating"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.ComponentModel.CancelEventArgs"/> that contains the event data.</param>
        protected override void OnValidating(CancelEventArgs e)
        {
            base.OnValidating(e);

            e.Cancel = timeFrom.Value.Date > timeTo.Value.Date;
        }
    }
}
