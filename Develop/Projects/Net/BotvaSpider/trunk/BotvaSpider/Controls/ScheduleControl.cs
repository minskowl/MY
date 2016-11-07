using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using Savchin.TimeManagment;

namespace BotvaSpider.Controls
{
    public partial class ScheduleControl : UserControl
    {
        private List<DataRow> data;
        /// <summary>
        /// Initializes a new instance of the <see cref="ScheduleControl"/> class.
        /// </summary>
        public ScheduleControl()
        {
            InitializeComponent();

            dataGridView1.AutoGenerateColumns = false;
            foreach (var i in DateTimeUtils.Hours)
            {
                ColumnToHour.Items.Add(i);
                ColumnFromHour.Items.Add(i);
            }
            foreach (var i in DateTimeUtils.MinutesAndSeconds)
            {
                ColumnToMinute.Items.Add(i);
                ColumnFromMinute.Items.Add(i);
            }

            ColumnToHour.DataPropertyName = "ToHour";
            ColumnFromHour.DataPropertyName = "FromHour";

            ColumnToMinute.DataPropertyName = "ToMinute";
            ColumnFromMinute.DataPropertyName = "FromMinute";
        }


        /// <summary>
        /// Shows the specified list.
        /// </summary>
        /// <param name="list">The list.</param>
        public void Show(IEnumerable<TimeRange> list)
        {
            data = list.Select(e => new DataRow(e)).ToList();
            dataGridView1.DataSource = new BindingList<DataRow>(data);

        }
        /// <summary>
        /// Gets the schedule.
        /// </summary>
        /// <returns></returns>
        public List<TimeRange> GetSchedule()
        {
            return data.Select(e => e.Range).ToList();
        }

        public class DataRow
        {
            /// <summary>
            /// Gets or sets from hour.
            /// </summary>
            /// <value>From hour.</value>
            public int FromHour { get; set; }
            /// <summary>
            /// Gets or sets from minute.
            /// </summary>
            /// <value>From minute.</value>
            public int FromMinute { get; set; }

            /// <summary>
            /// Gets or sets from hour.
            /// </summary>
            /// <value>From hour.</value>
            public int ToHour { get; set; }
            /// <summary>
            /// Gets or sets from minute.
            /// </summary>
            /// <value>From minute.</value>
            public int ToMinute { get; set; }

            /// <summary>
            /// Gets the range.
            /// </summary>
            /// <value>The range.</value>
            public TimeRange Range
            {
                get { return new TimeRange(new Time(FromHour, FromMinute), new Time(ToHour, ToMinute)); }
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="DataRow"/> class.
            /// </summary>
            public DataRow()
            { }

            /// <summary>
            /// Initializes a new instance of the <see cref="DataRow"/> class.
            /// </summary>
            /// <param name="range">The range.</param>
            public DataRow(TimeRange range)
                : this(range.From, range.To)
            {


            }
            /// <summary>
            /// Initializes a new instance of the <see cref="DataRow"/> class.
            /// </summary>
            /// <param name="from">From.</param>
            /// <param name="to">To.</param>
            public DataRow(Time from, Time to)
            {

                FromHour = from.Hour;
                FromMinute = from.Minute;

                ToHour = to.Hour;
                ToMinute = to.Minute;
            }
        }

        private void dataGridView1_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            var dataItem = dataGridView1.Rows[e.RowIndex].DataBoundItem;
            if (dataItem == null) return;

            try
            {
                var range = ((DataRow)dataItem).Range;
            }
            catch
            {
                e.Cancel = true;
            }
        }
    }
}
