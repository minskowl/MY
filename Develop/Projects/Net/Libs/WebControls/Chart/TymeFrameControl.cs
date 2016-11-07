#region Version & Copyright
/* 
 * $Id: TymeFrameControl.cs 19978 2007-08-15 09:11:28Z svd $ 
 *
 * <:COPYRIGHT_LICENSE:>
 */
#endregion

using System;


namespace Savchin.Web.UI
{
    public class TymeFrameControl : DropPanelButton
    {
        private const string groupName = "TymeFrameControl";

        #region Controls
        private readonly HtmlTableEx table = new HtmlTableEx();
        readonly ButtonEx buttonApply = new ButtonEx();
        private readonly RadioButtonEx buttonWeek = new RadioButtonEx();
        private readonly RadioButtonEx buttonMonth = new RadioButtonEx();
        private readonly RadioButtonEx buttonDay = new RadioButtonEx();
        #endregion

        #region Properties

        public TimeFrameType TimeFrameType
        {
            get
            {
                if (buttonWeek.Checked)
                    return TimeFrameType.Week;
                else if (buttonMonth.Checked)
                    return TimeFrameType.Month;
                else
                    return TimeFrameType.Day;
            }
            set
            {
                switch (value)
                {
                    case TimeFrameType.Week:
                        buttonWeek.Checked = true;
                        buttonMonth.Checked = false;
                        buttonDay.Checked = false;
                        break;

                    case TimeFrameType.Month:
                        buttonWeek.Checked = false;
                        buttonMonth.Checked = true;
                        buttonDay.Checked = false;
                        break;

                    case TimeFrameType.Day:
                        buttonWeek.Checked = false;
                        buttonMonth.Checked = false;
                        buttonDay.Checked = true;
                        break;

                    default:
                        throw new ArgumentOutOfRangeException("value");
                }
            }
        }

        /// <summary>
        /// Gets the button bar ID.
        /// </summary>
        /// <value>The button bar ID.</value>
        internal string ButtonWeekID
        {
            get { return buttonWeek.ClientID; }
        }

        /// <summary>
        /// Gets the button line ID.
        /// </summary>
        /// <value>The button line ID.</value>
        internal string ButtonMonthID
        {
            get { return buttonMonth.ClientID; }
        }

        /// <summary>
        /// Gets the button day ID.
        /// </summary>
        /// <value>The button day ID.</value>
        internal string ButtonDayID
        {
            get { return buttonDay.ClientID; }
        }

        /// <summary>
        /// Gets or sets the apply script.
        /// </summary>
        /// <value>The apply script.</value>
        public string ApplyScript
        {
            get { return buttonApply.OnClientClick; }
            set { buttonApply.OnClientClick = value; }
        }


        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="GraphTypeControl"/> class.
        /// </summary>
        public TymeFrameControl()
        {
            Button.Mode = ButtonEx.ButtonType.Link;
            Button.Text = "Time";
        }

        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.Init"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> object that contains the event data.</param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            table.Align = "center";

            CreateTitleRow();

            buttonDay.ID = ID + "_ButtonDay";
            buttonDay.Text = "Day";
            table.CreateCell(buttonDay);

            buttonWeek.ID = ID + "_ButtonWeek";
            buttonWeek.Text = "Week";
            table.CreateCell(buttonWeek);

            buttonMonth.ID = ID + "_ButtonMonth";
            buttonMonth.Text = "Month";
            table.CreateCell(buttonMonth);

            CreateButtonRow();

            Panel.Controls.Add(table);

            Panel.Width = 150;
            Panel.Height = 120;

            if (!Page.IsPostBack)
            {
                buttonDay.Checked = true;
            }
        }
        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.PreRender"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> object that contains the event data.</param>
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            string fullGroupName = groupName + ClientID;

            buttonDay.GroupName = fullGroupName;
            buttonWeek.GroupName = fullGroupName;
            buttonMonth.GroupName = fullGroupName;
        }

        #region Create Table
        private void CreateTitleRow()
        {
            table.CreateCell("Choose time frame:");
        }




        private void CreateButtonRow()
        {
            buttonApply.UseSubmitBehavior = false;
            buttonApply.Text = "Apply";
            table.CreateCell(buttonApply);
        }


        #endregion
    }
}
