using System;

namespace Savchin.WinApi.Windows
{
    public class MessageBoxWatcher
    {
        #region Properties
        private int handledCounter = 0;
        /// <summary>
        /// Gets the handled counter.
        /// </summary>
        /// <value>The handled counter.</value>
        public int HandledCounter
        {
            get { return handledCounter; }
        }
        private int unexpectedWindows = 0;
        /// <summary>
        /// Gets the handled counter.
        /// </summary>
        /// <value>The handled counter.</value>
        public int UnexpectedWindows
        {
            get { return unexpectedWindows; }
        }
        private string text;
        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The text.</value>
        public string Text
        {
            get { return text; }
            set { text = value; }
        }
        private string title;
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public string Title
        {
            get { return title; }
            set { title = value; }
        }
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageBoxWatcher"/> class.
        /// </summary>
        /// <param name="title">The title.</param>
        public MessageBoxWatcher(string title)
        {
            this.title = title;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageBoxWatcher"/> class.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="text">The text.</param>
        public MessageBoxWatcher(string title, string text)
        {
            this.title = title;
            this.text = text;
        }



        /// <summary>
        /// Clicks the first button.
        /// </summary>
        public void ClickFirstButton()
        {
            WindowWatcher.Instance.AddHandler(ClickFirstButtonHandler);
            if (!WindowWatcher.Instance.Enabled)
                WindowWatcher.Instance.Enabled = true;
        }

        private bool ClickFirstButtonHandler(Window window)
        {
            if (!window.IsDialog)
                return false;
            if (title != null && title != window.Name)
                return false;
            var realText = WindowHandle.GetText(window.Handle);
            if (text != null && !realText.Contains(text))
            {
                unexpectedWindows++;
            }

            handledCounter++;
            var dialog = new MessageBox(window.Handle);
            dialog.FirstButton.Click();
            WindowWatcher.Instance.RemoveHandler(ClickFirstButtonHandler);
            return true;
        }


    }
}