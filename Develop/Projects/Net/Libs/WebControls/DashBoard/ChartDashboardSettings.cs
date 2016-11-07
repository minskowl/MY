#region Version & Copyright
/* 
 * $Id: ChartDashboardSettings.cs 20379 2007-08-22 08:48:23Z svd $ 
 *
 * <:COPYRIGHT_LICENSE:>
 */
#endregion

using Savchin.Web.Core;


namespace Savchin.Web.UI
{
    public class ChartDashboardSettings //: UserObject
    {
        private ChartType chartType = ChartType.Bar;
        private TimeFrameType timeFrameType = TimeFrameType.Month;

        /// <summary>
        /// Gets or sets the type of the chart.
        /// </summary>
        /// <value>The type of the chart.</value>
        public ChartType ChartType
        {
            get { return chartType; }
            set { chartType = value; }
        }

        /// <summary>
        /// Gets or sets the type of the time frame.
        /// </summary>
        /// <value>The type of the time frame.</value>
        public TimeFrameType TimeFrameType
        {
            get { return timeFrameType; }
            set { timeFrameType = value; }
        }




        internal string toJSONString()
        {
            return TypeSerializer<ChartDashboardSettings>.ToJsonString(this);
        }

        public static ChartDashboardSettings fromJSONString(string s)
        {
            return TypeSerializer<ChartDashboardSettings>.FromJsonString(s);
        }
    }
}
