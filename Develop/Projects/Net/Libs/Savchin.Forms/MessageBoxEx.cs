using System.Windows.Forms;

namespace Savchin.Forms
{
    /// <summary>
    /// MessageBoxEx
    /// </summary>
    public static class MessageBoxEx
    {
        /// <summary>
        /// Shows the exclamation.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <param name="title">The title.</param>
        /// <param name="text">The text.</param>
        public static void ShowExclamation(Form owner, string title, string text)
        {
            Show(owner, title, text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }


        /// <summary>
        /// Shows the waring.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <param name="title">The title.</param>
        /// <param name="text">The text.</param>
        public static void ShowError(Form owner, string title, string text)
        {
            Show(owner, title, text, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Shows the waring.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <param name="title">The title.</param>
        /// <param name="text">The text.</param>
        public static void ShowWaring(Form owner, string title, string text)
        {
            Show(owner, title, text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }



        private delegate void ShowDelegate(Form owner, string title, string text, MessageBoxButtons buttons, MessageBoxIcon icon);
        /// <summary>
        /// Shows the specified owner.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <param name="title">The title.</param>
        /// <param name="text">The text.</param>
        /// <param name="buttons">The buttons.</param>
        /// <param name="icon">The icon.</param>
        public static void Show(Form owner, string title, string text, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            if (owner == null || owner.IsDisposed)
            {
                MessageBox.Show(text, title, buttons, icon);
            }
            else
            {
                if (owner.InvokeRequired)
                {
                    owner.Invoke(
                        new ShowDelegate(Show),
                        new object[] { owner, title, text, buttons, icon });

                }
                else
                {
                    MessageBox.Show(
                        owner, 
                        text, 
                        string.IsNullOrEmpty(title) ? owner.Text : title, 
                        buttons, 
                        icon);
                }
            }
        }




        /// <summary>
        /// Shows the specified owner.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <param name="title">The title.</param>
        /// <param name="text">The text.</param>
        public static void Show(Form owner, string title, string text)
        {
            Show(owner, title, text, MessageBoxButtons.OK, MessageBoxIcon.None);
        }
    }
}
