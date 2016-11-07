using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Savchin.Forms.Wizard
{
    /// <summary>
    /// A wizard is the control added to a form to provide a step by step functionality.
    /// It contains <see cref="WizardPage"/>s in the <see cref="Pages"/> collection, which
    /// are containers for other controls. Only one wizard page is shown at a time in the client
    /// are of the wizard.
    /// </summary>
    [Designer(typeof(WizardDesigner))]
    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(Wizard))]
    public class Wizard : UserControl
    {
        #region Properties

        /// <summary>
        /// btnNext
        /// </summary>
        protected internal Button btnBack;
        /// <summary>
        /// btnNext
        /// </summary>
        protected internal Button btnNext;
        private Button btnCancel;
        private Panel pnlButtons;
        private Panel pnlButtonBright3d;
        private Panel pnlButtonDark3d;

        private Container components = null;
        private WizardPage vActivePage;
        private readonly PageCollection _pages;
        



        /// <summary>
        /// Returns the collection of Pages in the wizard
        /// </summary>
        [Category("Wizard")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PageCollection Pages
        {
            get { return _pages; }
        }
        /// <summary>
        /// Gets or sets the dialog result.
        /// </summary>
        /// <value>The dialog result.</value>
        [Category("Behavior")]
        public DialogResult DialogResult { get; set; }
        /// <summary>
        /// Gets/Sets the activePage in the wizard
        /// </summary>
        [Category("Wizard")]
        internal int PageIndex
        {
            get { return _pages.IndexOf(vActivePage); }
            set
            {
                //Do I have any pages?
                if (_pages.Count == 0)
                {
                    //No then show nothing
                    ActivatePage(-1);
                    return;
                }
                // Validate the page asked for
                if (value < -1 || value >= _pages.Count)
                {
                    throw new ArgumentOutOfRangeException("value",
                                                          value,
                                                          "The page index must be between 0 and " +
                                                          Convert.ToString(_pages.Count - 1)
                        );
                }
                //Select the new page
                ActivatePage(value);
            }
        }

        /// <summary>
        /// Alternative way of getting/Setiing  the current page by using wizardPage objects
        /// </summary>
        public WizardPage Page
        {
            get { return vActivePage; }
            //Dont use this anymore, see Next, Back, NextTo and BackTo
            //			set
            //			{
            //				ActivatePage(value);
            //			}
        }

        /// <summary>
        /// Gets/Sets the enabled state of the Next button. 
        /// </summary>
        [Category("Wizard")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool NextEnabled
        {
            get { return btnNext.Enabled; }
            set { btnNext.Enabled = value; }
        }

        /// <summary>
        /// Gets/Sets the enabled state of the back button. 
        /// </summary>
        [Category("Wizard")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool BackEnabled
        {
            get { return btnBack.Enabled; }
            set { btnBack.Enabled = value; }
        }

        /// <summary>
        /// Gets/Sets the enabled state of the cancel button. 
        /// </summary>
        [Category("Wizard")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool CancelEnabled
        {
            get { return btnCancel.Enabled; }
            set { btnCancel.Enabled = value; }
        }
        #endregion

        /// <summary>
        /// Called when the cancel button is pressed, before the form is closed. Set e.Cancel to true if 
        /// you do not wish the cancel to close the wizard.
        /// </summary>
        public event CancelEventHandler CloseFromCancel;

        /// <summary>
        /// Wizard control with designer support
        /// </summary>
        public Wizard()
        {
            //Empty collection of Pages
            _pages = new PageCollection(this);

            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();
        }

        private void Wizard_Load(object sender, EventArgs e)
        {
            //Attempt to activate a page
            ActivatePage(0);

            //Can I add my self as default cancel button
            Form form = FindForm();
            if (form != null && DesignMode == false)
                form.CancelButton = btnCancel;
        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }


        /// <summary>
        /// Activates the page.
        /// </summary>
        /// <param name="index">The index.</param>
        protected internal void ActivatePage(int index)
        {
            //If the new page is invalid
            if (index < 0 || index >= _pages.Count)
            {
                btnNext.Enabled = false;
                btnBack.Enabled = false;

                return;
            }


            //Change to the new Page
            WizardPage tWizPage = (_pages[index]);

            //Really activate the page
            ActivatePage(tWizPage);
        }

        /// <summary>
        /// Activates the page.
        /// </summary>
        /// <param name="page">The page.</param>
        protected internal void ActivatePage(WizardPage page)
        {
            //Deactivate the current
            if (vActivePage != null)
            {
                vActivePage.Visible = false;
            }


            //Activate the new page
            vActivePage = page;

            if (vActivePage != null)
            {
                //Ensure that this panel displays inside the wizard
                vActivePage.Parent = this;
                if (Contains(vActivePage) == false)
                {
                    Container.Add(vActivePage);
                }
                //Make it fill the space
                vActivePage.Dock = DockStyle.Fill;
                vActivePage.Visible = true;
                vActivePage.BringToFront();
                vActivePage.FocusFirstTabIndex();
            }

            //What should the back button say
            if (PageIndex > 0)
            {
                btnBack.Enabled = true;
            }
            else
            {
                btnBack.Enabled = false;
            }

            //What should the Next button say
            if (_pages.IndexOf(vActivePage) < _pages.Count - 1
                && vActivePage.IsFinishPage == false)
            {
                btnNext.Text = "&Next >";
                btnNext.Enabled = true;
                //Don't close the wizard :)
                btnNext.DialogResult = DialogResult.None;
            }
            else
            {
                btnNext.Text = "Fi&nish";
                //Dont allow a finish in designer
                if (DesignMode
                    && _pages.IndexOf(vActivePage) == _pages.Count - 1)
                {
                    btnNext.Enabled = false;
                }
                else
                {
                    btnNext.Enabled = true;
                    //If Not in design mode then allow a close
                    btnNext.DialogResult = DialogResult == DialogResult.None ? DialogResult.OK : DialogResult;
                }
            }

            //Cause a refresh
            if (vActivePage != null)
                vActivePage.Invalidate();
            else
                Invalidate();
        }


        #region Button Handlers
        private void btnNext_Click(object sender, EventArgs e)
        {
            Next();
        }

        private void btnNext_MouseDown(object sender, MouseEventArgs e)
        {
            if (DesignMode)
                Next();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Back();
        }

        private void btnBack_MouseDown(object sender, MouseEventArgs e)
        {
            if (DesignMode)
                Back();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            var arg = new CancelEventArgs();

            //Throw the event out to subscribers
            if (CloseFromCancel != null)
            {
                CloseFromCancel(this, arg);
            }
            //If nobody told me to cancel
            if (!arg.Cancel && ParentForm!=null)
            {
                ParentForm.DialogResult = DialogResult.Cancel;
                //Then we close the form
                ParentForm.Close();
            }
        }
        #endregion


        /// <summary>
        /// Closes the current page after a <see cref="WizardPage.CloseFromNext"/>, then moves to 
        /// the Next page and calls <see cref="WizardPage.ShowFromNext"/>
        /// </summary>
        public void Next()
        {
            Debug.Assert(PageIndex >= 0, "Page Index was below 0");

            //Tell the Application I just closed a Page
            var e = new PageEventArgs(PageIndex + 1, Pages);

            vActivePage.OnCloseFromNext(e);

            if (e.Cancel)
            {
                if (ParentForm != null)
                    this.ParentForm.DialogResult = DialogResult.None;
                return;
            }

            //Did I just press Finish instead of Next
            if (PageIndex < _pages.Count - 1
                && (vActivePage.IsFinishPage == false || DesignMode))
            {
                ActivatePage(e.PageIndex);
                //Tell the application, I have just shown a page
                vActivePage.OnShowFromNext(this);
            }
            else
            {
                Debug.Assert(PageIndex < _pages.Count, "Error I've just gone past the finish",
                             "btnNext_Click tried to go to page " + Convert.ToString(PageIndex + 1)
                             + ", but I only have " + Convert.ToString(_pages.Count));
                //yep Finish was pressed
                
                if (!DesignMode && ParentForm != null)
                {
                    if (DialogResult != DialogResult.None && ParentForm.DialogResult != DialogResult)
                        ParentForm.DialogResult = DialogResult;
                    ParentForm.Close();
                }
            }
        }

        /// <summary>
        /// Moves to the page given and calls <see cref="WizardPage.ShowFromNext"/>
        /// </summary>
        /// <remarks>Does NOT call <see cref="WizardPage.CloseFromNext"/> on the current page</remarks>
        /// <param name="page"></param>
        public void NextTo(WizardPage page)
        {
            //Since we have a page to go to, then there is no need to validate most of it
            ActivatePage(page);
            //Tell the application, I have just shown a page
            vActivePage.OnShowFromNext(this);
        }

        /// <summary>
        /// Closes the current page after a <see cref="WizardPage.CloseFromBack"/>, then moves to 
        /// the previous page and calls <see cref="WizardPage.ShowFromBack"/>
        /// </summary>
        public void Back()
        {
            Debug.Assert(PageIndex < _pages.Count, "Page Index was beyond Maximum pages");
            //Can I press back
            Debug.Assert(PageIndex > 0 && PageIndex < _pages.Count, "Attempted to go back to a page that doesn't exist");
            //Tell the application that I closed a page
            int newPage = vActivePage.OnCloseFromBack(this);

            ActivatePage(newPage);
            //Tell the application I have shown a page
            vActivePage.OnShowFromBack(this);
        }

        /// <summary>
        /// Moves to the page given and calls <see cref="WizardPage.ShowFromBack"/>
        /// </summary>
        /// <remarks>Does NOT call <see cref="WizardPage.CloseFromBack"/> on the current page</remarks>
        /// <param name="page"></param>
        public void BackTo(WizardPage page)
        {
            //Since we have a page to go to, then there is no need to validate most of it
            ActivatePage(page);
            //Tell the application, I have just shown a page
            vActivePage.OnShowFromNext(this);
        }


        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Paint"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs"/> that contains the event data.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (DesignMode)
            {
                const string noPagesText = "No wizard pages inside the wizard.";


                SizeF textSize = e.Graphics.MeasureString(noPagesText, Font);
                var layout = new RectangleF((Width - textSize.Width) / 2,
                                            (pnlButtons.Top - textSize.Height) / 2,
                                            textSize.Width, textSize.Height);

                var dashPen = (Pen)SystemPens.GrayText.Clone();
                dashPen.DashStyle = DashStyle.Dash;

                e.Graphics.DrawRectangle(dashPen,
                                         Left + 8, Top + 8,
                                         Width - 17, pnlButtons.Top - 17);

                e.Graphics.DrawString(noPagesText, Font, new SolidBrush(SystemColors.GrayText), layout);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data.</param>
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            if (DesignMode)
            {
                Invalidate();
            }
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlButtons = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.pnlButtonBright3d = new System.Windows.Forms.Panel();
            this.pnlButtonDark3d = new System.Windows.Forms.Panel();
            this.pnlButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlButtons
            // 
            this.pnlButtons.Controls.Add(this.btnCancel);
            this.pnlButtons.Controls.Add(this.btnNext);
            this.pnlButtons.Controls.Add(this.btnBack);
            this.pnlButtons.Controls.Add(this.pnlButtonBright3d);
            this.pnlButtons.Controls.Add(this.pnlButtonDark3d);
            this.pnlButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlButtons.Location = new System.Drawing.Point(0, 224);
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Size = new System.Drawing.Size(444, 48);
            this.pnlButtons.TabIndex = 0;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor =
                ((System.Windows.Forms.AnchorStyles)
                 ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnCancel.Location = new System.Drawing.Point(356, 12);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnNext
            // 
            this.btnNext.Anchor =
                ((System.Windows.Forms.AnchorStyles)
                 ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnNext.Location = new System.Drawing.Point(272, 12);
            this.btnNext.Name = "btnNext";
            this.btnNext.TabIndex = 4;
            this.btnNext.Text = "&Next >";
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            this.btnNext.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnNext_MouseDown);
            // 
            // btnBack
            // 
            this.btnBack.Anchor =
                ((System.Windows.Forms.AnchorStyles)
                 ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnBack.Location = new System.Drawing.Point(196, 12);
            this.btnBack.Name = "btnBack";
            this.btnBack.TabIndex = 3;
            this.btnBack.Text = "< &Back";
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            this.btnBack.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnBack_MouseDown);
            // 
            // pnlButtonBright3d
            // 
            this.pnlButtonBright3d.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.pnlButtonBright3d.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlButtonBright3d.Location = new System.Drawing.Point(0, 1);
            this.pnlButtonBright3d.Name = "pnlButtonBright3d";
            this.pnlButtonBright3d.Size = new System.Drawing.Size(444, 1);
            this.pnlButtonBright3d.TabIndex = 1;
            // 
            // pnlButtonDark3d
            // 
            this.pnlButtonDark3d.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pnlButtonDark3d.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlButtonDark3d.Location = new System.Drawing.Point(0, 0);
            this.pnlButtonDark3d.Name = "pnlButtonDark3d";
            this.pnlButtonDark3d.Size = new System.Drawing.Size(444, 1);
            this.pnlButtonDark3d.TabIndex = 2;
            // 
            // Wizard
            // 
            this.Controls.Add(this.pnlButtons);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular,
                                                System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.Name = "Wizard";
            this.Size = new System.Drawing.Size(444, 272);
            this.Load += new System.EventHandler(this.Wizard_Load);
            this.pnlButtons.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion


    }
}