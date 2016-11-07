using System;
using System.Windows.Forms;

namespace Savchin.Forms.Core.Commands.Handlers
{
    internal class ToolStripButtonHandler : CommandBinding
    {
        private ToolStripButton _button;

        /// <summary>
        /// Initializes a new instance of the <see cref="ToolStripButtonHandler"/> class.
        /// </summary>
        /// <param name="button">The button.</param>
        /// <param name="command">The command.</param>
        public ToolStripButtonHandler(ToolStripButton button, Command command)
            : base(command)
        {

            _button = button;

            _button.Click += ButtonClick;
            _button.Enabled = Command.Enabled;
            _button.Checked = Command.IsChecked;

        }

        /// <summary>
        /// Realeases the tracking.
        /// </summary>
        public override void RealeaseTracking()
        {
            _button.Click -= ButtonClick;

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
            if (e.PropertyName == "IsChecked")
                _button.Checked = Command.IsChecked;

        }



        void ButtonClick(object sender, EventArgs e)
        {
            Command.Execute(_button.Tag, sender);
        }



    }
}