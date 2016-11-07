using System;
using System.Windows.Forms;
using BotvaSpider.Core;
using BotvaSpider.Logging;

namespace BotvaSpider
{
    public partial class FormTest : Form
    {
        public FormTest()
        {
            InitializeComponent();

            AppCore.LogOutput.Error("Test error", new Exception("Exception"));
            AppCore.LogOutput.Add(LogEntryType.Suggestion, "Surrended", "sdsdadsdssdsd", null);
            AppCore.LogOutput.Warn("Warnig", new Exception("Exception"));
            AppCore.LogOutput.Debug("Info");
            logViewer.Show(LoggerType.Output);
        }
    }
}
