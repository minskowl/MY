using System;
using Bashni.Core;

namespace Bashni.Controls
{
    /// <summary>
    /// Interaction logic for TabLog.xaml
    /// </summary>
    public partial class TabLog : ILogger
    {
        public TabLog()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Adds the entry.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="message">The message.</param>
        public void AddEntry(LogType type, string message)
        {
            txtLog.AppendText(GetText(type, message));
            OnTextAdded(type);
        }

        /// <summary>
        /// Adds the entry.
        /// </summary>
        /// <param name="entry">The entry.</param>
        public void AddEntry(LogEntry entry)
        {
            txtLog.AppendText(GetText(entry.Type, entry.Message));
            if (entry.Data != null)
                txtLog.AppendText(Environment.NewLine + "Data: " + entry.Data);
            OnTextAdded(entry.Type);
        }

        private void OnTextAdded(LogType type)
        {

            if (txtLog.LineCount > 0)
                txtLog.ScrollToLine(txtLog.LineCount - 1);
            if (type == LogType.Error)
            {
                txtLog.Focus();
            }
            System.Windows.Forms.Application.DoEvents();

        }
        private static string GetText(LogType type, string message)
        {
            return string.Format("{0} {3} {1} : {2}", Environment.NewLine, type, message, DateTime.Now);
        }
    }
}
