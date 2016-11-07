using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ExtendedWebBrowser2;
using FlatSearcher.Core;
using MyCustomWebBrowser.Core;
using Savchin.Forms.Core;
using Savchin.Logging;
using Savchin.Forms;
using WatiN.Core;

namespace FlatSearcher.Controls
{
    public partial class SearchResultControl : UserControl
    {
        #region Properties
        private Panel _panelTop;
        private ExtendedWebBrowser _panelBrowser;
        private CollapsibleGroupBox _panelComments;
        private System.Windows.Forms.Label _labelLocation;
        public event EventHandler<FlatEventArgs> FlatReaded;

        /// <summary>
        /// Gets or sets the map.
        /// </summary>
        /// <value>
        /// The map.
        /// </value>
        public IMap Map { get; set; }

        /// <summary>
        /// Gets the browser.
        /// </summary>
        public ExtendedWebBrowser Browser
        {
            get { return _panelBrowser; }
        }

        /// <summary>
        /// Gets the watin browser.
        /// </summary>
        private IE WatinBrowser;

        #endregion

        private bool ignoreResize = true;
        /// <summary>
        /// Initializes a new instance of the <see cref="SearchResultControl"/> class.
        /// </summary>
        public SearchResultControl()
        {
            InitializeComponent();

            CreateTopPanel();
            CreateComments();
            CreateBrowser();

            if (ControlHelper.DesignMode) return;

            Browser.DocumentCompleted += Browser_DocumentCompleted;
            ignoreResize = false;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (ignoreResize) return;
            _panelTop.Width = Width;
            _panelComments.Width = Width;
            _panelBrowser.Width = Width;
            SetBroserHeight();
        }

        private void SetBroserHeight()
        {
            _panelBrowser.Top = _panelComments.Top + _panelComments.Height + 3;
            _panelBrowser.Height = Height - _panelBrowser.Top - 20;
        }


        private void Browser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (e.Url != Browser.Url) return;
            try
            {
                OnFlatReaded(new FlatEventArgs(ProcessResult()));

                HidePanels();
            }
            catch (Exception ex)
            {
                SearchContext.Current.Log.AddMessage(Severity.Error, "Ошибка процессинга квартиры", ex);
            }
        }

        private void HidePanels()
        {
           // WatinBrowser.Divs[0].Style.SetAttributeValue("display", "none");
            WatinBrowser.Tables[0].Style.SetAttributeValue("visibility", "collapse");
            WatinBrowser.Tables[1].Style.SetAttributeValue("visibility", "collapse");

            var t = WatinBrowser.Table("center");
            var row = t.TableRows[0];
            row.TableCells[0].Style.SetAttributeValue("display", "none");
           // row.TableCells[2].Style.SetAttributeValue("display", "none");

            WatinBrowser.Div("footer").Style.SetAttributeValue("display", "none");
            WatinBrowser.ElementsWithTag("noindex")[0].Style.SetAttributeValue("display", "none");
        }

        private Flat ProcessResult()
        {
            var infos = WatinBrowser.Tables.Where(t => t.ClassName == "object-view").ToArray();

            var flat = new Flat(infos);
            var isInregion = Map.IsInRegion(flat.Lng, flat.Lat);

            var dbFlat = SearchContext.Current.Data.Flats.FirstOrDefault(f => f.Id == flat.Id);
            if (dbFlat == null)
            {
                dbFlat = flat;
                dbFlat.Visible = isInregion;
                SearchContext.Current.Data.Flats.Add(dbFlat);
            }
            else
            {
                dbFlat.GetInfo(flat);
            }
            SearchContext.Current.Data.SetVisibility(flat.Address, isInregion);
            flatBindingSource.DataSource = dbFlat;
            SetStatus(isInregion);

            Parent.Text = dbFlat.Address;

            return dbFlat;
        }

        private void SetStatus(bool isInregion)
        {
            if (isInregion)
            {
                _labelLocation.Text = "В выбраном районе";
                _labelLocation.ForeColor = Color.Green;
            }
            else
            {
                _labelLocation.Text = "Вне выбраного района";
                _labelLocation.ForeColor = Color.Red;
            }
        }

        private void OnFlatReaded(FlatEventArgs e)
        {
            EventHandler<FlatEventArgs> handler = FlatReaded;
            if (handler != null) handler(this, e);
        }

        #region Crete Controls

        private void CreateTopPanel()
        {
            var label1 = new System.Windows.Forms.Label();
            var comboBoxRating = new ComboBox();

            var checkBoxVisible = new System.Windows.Forms.CheckBox();
            _labelLocation = new System.Windows.Forms.Label();
            _panelTop = new Panel();
            comboBoxRating.Items.AddRange(Enumerable.Range(0, 10).Cast<object>().ToArray());

            _panelTop.SuspendLayout();

            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(3, 1);
            label1.Name = "label1";
            label1.Size = new Size(48, 13);
            label1.TabIndex = 1;
            label1.Text = "Рейтинг";
            // 
            // comboBoxRating
            // 
            comboBoxRating.DataBindings.Add(new Binding("SelectedItem", flatBindingSource, "Rating", true));
            comboBoxRating.FormattingEnabled = true;
            comboBoxRating.Location = new System.Drawing.Point(57, 3);
            comboBoxRating.Name = "comboBoxRating";
            comboBoxRating.Size = new System.Drawing.Size(30, 21);
            comboBoxRating.TabIndex = 2;

            // 
            // checkBoxVisible
            // 
            checkBoxVisible.AutoSize = true;
            checkBoxVisible.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            checkBoxVisible.DataBindings.Add(new Binding("Checked", flatBindingSource, "Visible", true));
            checkBoxVisible.Location = new System.Drawing.Point(93, 3);
            checkBoxVisible.Name = "checkBoxVisible";
            checkBoxVisible.Size = new System.Drawing.Size(89, 17);
            checkBoxVisible.TabIndex = 4;
            checkBoxVisible.Text = "Показывать";
            checkBoxVisible.UseVisualStyleBackColor = true;
            // 
            // labelLocation
            // 
            _labelLocation.AutoSize = true;
            _labelLocation.Location = new Point(188, 3);
            _labelLocation.Name = "labelLocation";
            _labelLocation.Size = new System.Drawing.Size(76, 13);
            _labelLocation.TabIndex = 5;
            _labelLocation.Text = "Загружаемся";
            // 
            // panel1
            // 
            _panelTop.Controls.Add(label1);
            _panelTop.Controls.Add(comboBoxRating);
            _panelTop.Controls.Add(_labelLocation);
            _panelTop.Controls.Add(checkBoxVisible);
            //_panelTop.Dock = DockStyle.Top;
            _panelTop.Location = new Point(0, 5);
            _panelTop.Name = "panel1";
            _panelTop.Size = new Size(708, 37);
            _panelTop.TabIndex = 6;

            Controls.Add(_panelTop);
            ResumeLayout(false);
            PerformLayout();
        }

        private void CreateBrowser()
        {
            _panelBrowser = new ExtendedWebBrowser
                                {
                                    Location = new Point(0, 195),
                                    Name = "browserControl1",
                                    Size = new Size(708, 528),
                                    TabIndex = 0
                                };
            Controls.Add(_panelBrowser);
            WatinBrowser = new IE(_panelBrowser.ActiveXInstance, false);
        }

        private void CreateComments()
        {

            var textBoxComments = new TextBox
                                      {
                                          Dock = DockStyle.Fill,
                                          Location = new Point(3, 16),
                                          Multiline = true,
                                          Name = "textBoxComments",
                                          ScrollBars = ScrollBars.Vertical,
                                          Size = new System.Drawing.Size(702, 1),
                                          TabIndex = 0
                                      };
            textBoxComments.DataBindings.Add(new Binding("Text", flatBindingSource, "Comments", true));

            _panelComments = new CollapsibleGroupBox();
            _panelComments.SuspendLayout();
            //_panelComments.Dock = DockStyle.Top;
            _panelComments.Location = new Point(0, 40);
            _panelComments.Name = "collapsibleGroupBox1";
            _panelComments.Size = new Size(708, 150);
            _panelComments.FullHeight = 150;
            _panelComments.TabIndex = 6;
            _panelComments.TabStop = false;
            _panelComments.Text = "Комментарий";
            _panelComments.Controls.Add(textBoxComments);
            _panelComments.IsCollapsed = true;

            Controls.Add(_panelComments);
            _panelComments.ResumeLayout(false);
            _panelComments.PerformLayout();
            _panelComments.Invalidated += new InvalidateEventHandler(OnPanelCommentsInvalidated);
        }

        private void OnPanelCommentsInvalidated(object sender, InvalidateEventArgs e)
        {
            SetBroserHeight();
        }
        #endregion


    }
}
