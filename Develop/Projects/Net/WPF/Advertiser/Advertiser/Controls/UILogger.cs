using System;
using System.Windows.Controls;
using System.Windows.Threading;
using WatiN.Core.Interfaces;

namespace Advertiser.Controls
{
    public class UILogger : TextBox, ILogWriter
    {
        #region Implementation of ILogWriter

        public void LogAction(string message)
        {
            AddText(string.Format("Action: {0} {1}", message, Environment.NewLine));
        }

        public void LogDebug(string message)
        {
            AddText(string.Format("Debug: {0} {1}", message, Environment.NewLine));
        }

        #endregion

        private void AddText(string message)
        {
            Dispatcher.Invoke(DispatcherPriority.Normal,
                  new Action<string>(AppendText),
                 message);
        }
    }
}
