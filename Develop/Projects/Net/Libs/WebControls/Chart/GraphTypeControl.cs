#region Version & Copyright
/* 
 * $Id: GraphTypeControl.cs 19610 2007-08-06 20:33:59Z svd $ 
 *
 * <:COPYRIGHT_LICENSE:>
 */
#endregion
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;


namespace Savchin.Web.UI
{
    public class GraphTypeControl : DropPanelButton
    {
        private const string groupName = "GraphTypeControl";
        #region Properties
        private readonly HtmlTable table = new HtmlTable();
        readonly ButtonEx buttonApply = new ButtonEx();
        private readonly RadioButtonEx buttonBar = new RadioButtonEx();
        private readonly RadioButtonEx buttonLine = new RadioButtonEx();

        /// <summary>
        /// Gets or sets the type of the chart.
        /// </summary>
        /// <value>The type of the chart.</value>
        public ChartType ChartType
        {
            get
            {
                if (buttonBar.Checked)
                    return ChartType.Bar;
                return ChartType.Lines;
            }
            set
            {
                switch (value)
                {
                    case ChartType.Bar:
                        buttonBar.Checked = true;
                        buttonLine.Checked = false;
                        break;
                    case ChartType.Lines:
                        buttonBar.Checked = false;
                        buttonLine.Checked = true;
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
        internal string ButtonBarID
        {
            get { return buttonBar.ClientID; }
        }
        /// <summary>
        /// Gets the button line ID.
        /// </summary>
        /// <value>The button line ID.</value>
        internal string ButtonLineID
        {
            get { return buttonLine.ClientID; }
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
        public GraphTypeControl()
        {
            Button.Mode = ButtonEx.ButtonType.Link;
            Button.Text = "Graph Type";
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
            CreateFirstRow();
            CreateSecondRow();
            CreateButtonRow();

            Panel.Controls.Add(table);

            Panel.Width = 150;
            Panel.Height = 100;

            if (!Page.IsPostBack)
            {
                buttonBar.Checked = true;
            }
        }
        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.PreRender"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> object that contains the event data.</param>
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            buttonLine.GroupName = groupName + ClientID;
            buttonBar.GroupName = groupName + ClientID;
        }


        #region Create Table
        private void CreateTitleRow()
        {
            CreateCell().InnerText = "Choose graph type:";
        }



        private void CreateFirstRow()
        {
            buttonBar.ID = ID + "_ButtonBar";
            buttonBar.Text = "Bar graph";


            CreateCell().Controls.Add(buttonBar);

        }
        private void CreateSecondRow()
        {
            buttonLine.ID = ID + "_ButtonLine";
            buttonLine.Text = "Staked Lines";

            CreateCell().Controls.Add(buttonLine);
        }
        private void CreateButtonRow()
        {

            buttonApply.UseSubmitBehavior = false;
            buttonApply.Text = "Apply";
            CreateCell().Controls.Add(buttonApply);

        }
        private HtmlTableCell CreateCell()
        {
            HtmlTableRow row = new HtmlTableRow();
            HtmlTableCell cell = new HtmlTableCell();
            row.Cells.Add(cell);
            table.Rows.Add(row);
            return cell;
        }
        #endregion
    }
}
