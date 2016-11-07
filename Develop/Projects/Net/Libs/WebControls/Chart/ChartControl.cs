
using System;
using System.ComponentModel;
using dotnetCHARTING;
using Savchin.TimeManagment;


namespace Savchin.Web.UI
{
    /// <summary>
    /// TimeFrameType
    /// </summary>
    public enum TimeFrameType : int
    {
        /// <summary>
        /// Week
        /// </summary>
        Week = 0,
        /// <summary>
        /// Month
        /// </summary>
        Month = 1,
        /// <summary>
        /// Day
        /// </summary>
        Day = 2
    }
    public enum ChartType : int
    {
        Bar = 0,
        Lines = 1,
        Pie = 2
    }
    /// <summary>
    /// ChartControl
    /// </summary>
    internal class ChartControl : ChartEx
    {


        #region Properties
        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]
        public ChartType Type
        {
            get
            {
                object o = ViewState["Type"];
                if (o == null) return ChartType.Bar;
                return (ChartType)o;
            }
            set
            {
                ViewState["Type"] = value;
            }
        }
        /// <summary>
        /// Gets or sets the time frame.
        /// </summary>
        /// <value>The time frame.</value>
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        public TimeFrameType TimeFrame
        {
            get
            {
                object o = ViewState["TimeFrame"];
                if (o == null) return TimeFrameType.Week;
                return (TimeFrameType)o;
            }
            set
            {
                ViewState["TimeFrame"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the limit items.
        /// </summary>
        /// <value>The limit items.</value>
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("7")]
        public int LimitItems
        {
            get
            {
                object o = ViewState["LimitItems"];
                if (o == null) return 7;
                return (int)o;
            }
            set
            {
                ViewState["LimitItems"] = value;
            }
        }



        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="ChartControl"/> class.
        /// </summary>
        public ChartControl()
        {
            Instance.LegendBox.Template = "%icon %name";
        }


        protected override void OnFinalInitChart()
        {

            switch (Type)
            {
                case ChartType.Bar:
                    Instance.DefaultSeries.Type = SeriesType.Bar;
                    Instance.ShadingEffectMode = ShadingEffectMode.Three;

                    break;
                case ChartType.Lines:
                    //
                    //Instance.Use3D = false;
                    //Instance.XAxis.ClusterColumns = false;
                    //Instance.DefaultSeries.DefaultElement.Transparency = 20;

                    //Instance.DefaultSeries.Type = SeriesType.AreaLine;
                    //Instance.YAxis.Scale = Scale.FullStacked;
                    Instance.DefaultSeries.Type = SeriesType.Line;
                    //Instance.YAxis.Scale = Scale.Stacked;
                    break;
                case ChartType.Pie:
                    Instance.Type = dotnetCHARTING.ChartType.Pie;
                    Instance.ShadingEffectMode = ShadingEffectMode.Two;

                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            switch (TimeFrame)
            {
                case TimeFrameType.Week:
                    Instance.XAxis.TimeInterval = TimeInterval.None;
                    //Instance.XAxis.Interval = 1;

                    int minimum = DateTimeUtils.GetWeekOfYear() - LimitItems ;

                    Instance.XAxis.Minimum = (minimum < 1) ? 1 : minimum;
                    Instance.XAxis.Maximum = DateTimeUtils.GetWeekOfYear() + 1;

                    Instance.XAxis.Label.Text = DateTime.Today.Year + " Weeks";
                    break;
                case TimeFrameType.Month:
                    Instance.XAxis.TimeInterval = TimeInterval.Months;
                    //Instance.XAxis.Interval = double.NaN;
                    Instance.XAxis.FormatString = "MMMM";


                    //Instance.XAxis.Minimum = (DateTime.Today.Month > LimitItems) ? new DateTime(DateTime.Today.Year,DateTime.Today.Month-LimitItems,1)  : DateTimeUtils.GetFirstDate();
                    //Instance.XAxis.Maximum = DateTime.Today;

                    Instance.XAxis.Label.Text = DateTime.Today.Year + " Months";
                    break;
                case TimeFrameType.Day:
                    Instance.XAxis.TimeInterval = TimeInterval.Day;
                    //Instance.XAxis.Interval = double.NaN;
                    Instance.XAxis.FormatString = " d ";

                    Instance.XAxis.Minimum = DateTime.Today.AddDays(-7);
                    Instance.XAxis.Maximum = DateTime.Today;

                    Instance.XAxis.Label.Text = DateTime.Today.ToString("yyyy MMMM");
                    break;
                default:
                    Instance.XAxis.TimeInterval = TimeInterval.None;
                    break;
            }
            //Instance.YAxis.Label.Text = "Y Axis Label";

            //Instance.Use3D = true;
            //Instance.Depth = 5;

            //Instance.YAxis.SmartScaleBreakLimit = 1;

            Instance.YAxis.SmartMinorTicks = true;
            Instance.XAxis.SmartMinorTicks = true;
        }


    }
}
