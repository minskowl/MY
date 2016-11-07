using System;
using System.Windows.Forms;

namespace BotvaSpider.Controls
{
    public partial class PageControl : UserControl
    {
        /// <summary>
        /// Occurs when [current page changed].
        /// </summary>
        public event EventHandler CurrentPageChanged;

        private int pages;

        /// <summary>
        /// Gets or sets the pages.
        /// </summary>
        /// <value>The pages.</value>
        public int Pages
        {
            get { return pages; }
            set
            {
                pages = value;
                SetLabel();
            }
        }

        /// <summary>
        /// Gets or sets the current page.
        /// </summary>
        /// <value>The current page.</value>
        public int CurrentPage { get; private set; }

        public PageControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the Click event of the buttonRewind control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void buttonRewind_Click(object sender, EventArgs e)
        {
            SetPage(0);
        }

        /// <summary>
        /// Handles the Click event of the buttonPrev control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void buttonPrev_Click(object sender, EventArgs e)
        {
            if (CurrentPage > 0)
            {
                SetPage(CurrentPage - 1);
            }
        }

        /// <summary>
        /// Handles the Click event of the buttonNext control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void buttonNext_Click(object sender, EventArgs e)
        {
            if (CurrentPage < Pages-1)
            {
                SetPage(CurrentPage + 1);
            }
        }

        private void buttonForward_Click(object sender, EventArgs e)
        {
            SetPage(Pages - 1);
        }
        /// <summary>
        /// Raises the <see cref="E:CurrentPageChanged"/> event.
        /// </summary>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected virtual void OnCurrentPageChanged(EventArgs e)
        {
            if (CurrentPageChanged != null)
                CurrentPageChanged(this, e);
        }

        /// <summary>
        /// Sets the page.
        /// </summary>
        /// <param name="page">The page.</param>
        public void SetPage(int page)
        {
            if (CurrentPage == page) return;
            CurrentPage = page;
            SetLabel();
            OnCurrentPageChanged(EventArgs.Empty);
        }

        private void SetLabel()
        {
            label1.Text = (CurrentPage + 1) + " из " + Pages;
        }
    }
}
