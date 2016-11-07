using System;
using System.Windows.Forms;

namespace Savchin.Forms.Core.Commands.Handlers
{
    internal class ToolStripMenuItemHandler : CommandBinding
    {
        private ToolStripMenuItem _menuItem;

        /// <summary>
        /// Binds the command.
        /// </summary>
        /// <param name="menuItem">The menu item.</param>
        /// <param name="command">The command.</param>
        public ToolStripMenuItemHandler(ToolStripMenuItem menuItem, Command command)
            : base(command)
        {
            _menuItem = menuItem;

            _menuItem.Click += ExecuteCommand;
            BindCommandBase();

        }

        /// <summary>
        /// Binds the command.
        /// </summary>
        /// <param name="menuItem">The menu item.</param>
        /// <param name="command">The command.</param>
        /// <param name="parameterSource">The parameter source.</param>
        public ToolStripMenuItemHandler(ToolStripMenuItem menuItem, Command command, ParameterSource parameterSource)
            : base(command, parameterSource)
        {
            _menuItem = menuItem;

            menuItem.Click += ParameterSourceExecuteCommand;
            BindCommandBase();

        }
        /// <summary>
        /// Realeases the tracking.
        /// </summary>
        public override void RealeaseTracking()
        {
            if (ParameterSource == null)
            {
                _menuItem.Click -= ExecuteCommand;
            }
            else
            {
                _menuItem.Click -= ParameterSourceExecuteCommand;
            }
            _menuItem = null;

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
                _menuItem.Enabled = Command.Enabled;
            if (e.PropertyName == "IsChecked")
                _menuItem.Checked = Command.IsChecked;
        }


        void ExecuteCommand(object sender, EventArgs e)
        {
            Command.Execute(_menuItem.Tag, sender);
        }

        void ParameterSourceExecuteCommand(object sender, EventArgs e)
        {
            Command.Execute(ParameterSource(), sender);
        }

        private void BindCommandBase()
        {
            _menuItem.Enabled = Command.Enabled;
            _menuItem.Checked = Command.IsChecked;
            CommandManager.AddBinding(_menuItem, this);
        }

    }
}