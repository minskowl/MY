using System;
using System.Windows.Forms;

namespace Savchin.Forms.Core.Commands.Handlers
{
    /// <summary>
    /// 
    /// </summary>
    internal class ToolStripComboBoxHandler : CommandBinding
    {
        private ToolStripComboBox _button;

        /// <summary>
        /// Initializes a new instance of the <see cref="ToolStripComboBoxHandler"/> class.
        /// </summary>
        /// <param name="button">The button.</param>
        /// <param name="command">The command.</param>
        public ToolStripComboBoxHandler(ToolStripComboBox button, Command command)
            : base(command)
        {
            _button = button;

            _button.SelectedIndexChanged += ButtonSelectedIndexChanged;
            _button.Enabled = command.Enabled;
        }

        /// <summary>
        /// Realeases the tracking.
        /// </summary>
        public override void RealeaseTracking()
        {
            _button.SelectedIndexChanged -= ButtonSelectedIndexChanged;
            _button = null;

            base.RealeaseTracking();
        }

        /// <summary>
        /// Called when [command property changed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.ComponentModel.PropertyChangedEventArgs"/> instance containing the event data.</param>
        protected override void OnCommandPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnCommandPropertyChanged(sender, e);
            if (e.PropertyName == "Enabled")
                _button.Enabled = Command.Enabled;
        }

        void ButtonSelectedIndexChanged(object sender, EventArgs e)
        {
            Command.Execute(_button.SelectedItem, sender);
        }
    }
}
