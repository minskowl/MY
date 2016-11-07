using System.Windows.Forms;

namespace Savchin.Forms.Core.Commands.Handlers
{
    /// <summary>
    /// ToolStripMenuItemTracker
    /// </summary>
    internal class ToolStripMenuItemTracker : CommandBinding
    {
        private readonly ToolStripMenuItem _menuItem;


        /// <summary>
        /// Binds the state of the command.
        /// </summary>
        /// <param name="menuItem">The menu item.</param>
        /// <param name="command">The command.</param>
        public ToolStripMenuItemTracker(ToolStripMenuItem menuItem, Command command)
            : base(command)
        {
            _menuItem = menuItem;
            Initilaize();
        }

        /// <summary>
        /// Tracks the state.
        /// </summary>
        /// <param name="menuItem">The menu item.</param>
        /// <param name="command">The command.</param>
        /// <param name="parameterSource">The parameter source.</param>
        public ToolStripMenuItemTracker(ToolStripMenuItem menuItem, Command command, ParameterSource parameterSource)
            : base(command, parameterSource)
        {
            _menuItem = menuItem;
            Initilaize();
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
        }

        private void Initilaize()
        {
            _menuItem.Enabled = Command.Enabled;
            CommandManager.AddBinding(_menuItem, this);
        }



    }
}