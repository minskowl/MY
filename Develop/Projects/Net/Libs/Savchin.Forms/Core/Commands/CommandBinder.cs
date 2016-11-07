using System;
using System.Linq;
using System.Windows.Forms;
using Savchin.Forms.Core.Commands.Handlers;


namespace Savchin.Forms.Core.Commands
{
    /// <summary>
    /// Command Binder helper
    /// </summary>
    public static class CommandBinder
    {
        #region ToolStrip

        #region ToolStripMenuItem

        /// <summary>
        /// Binds the command.
        /// </summary>
        /// <param name="menuItem">The menu item.</param>
        /// <param name="command">The command.</param>
        public static void BindCommand(this ToolStripMenuItem menuItem, Command command)
        {
            new ToolStripMenuItemHandler(menuItem, command);
        }

        /// <summary>
        /// Binds the command.
        /// </summary>
        /// <param name="menuItem">The menu item.</param>
        /// <param name="command">The command.</param>
        /// <param name="parameterSource">The parameter source.</param>
        public static void BindCommand(this ToolStripMenuItem menuItem, Command command, ParameterSource parameterSource)
        {
            new ToolStripMenuItemHandler(menuItem, command, parameterSource);
        }


        /// <summary>
        /// Binds the state of the command.
        /// </summary>
        /// <param name="menuItem">The menu item.</param>
        /// <param name="command">The command.</param>
        public static void TrackState(this ToolStripMenuItem menuItem, Command command)
        {
            new ToolStripMenuItemTracker(menuItem, command);
        }

        /// <summary>
        /// Tracks the state.
        /// </summary>
        /// <param name="menuItem">The menu item.</param>
        /// <param name="command">The command.</param>
        /// <param name="parameterSource">The parameter source.</param>
        public static void TrackState(this ToolStripMenuItem menuItem, Command command, ParameterSource parameterSource)
        {
            new ToolStripMenuItemTracker(menuItem, command, parameterSource);
        }

        #endregion

        /// <summary>
        /// Tracks the command.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <param name="command">The command.</param>
        public static void BindCommand(this ToolStripItemCollection items, Command command)
        {
            foreach (var item in items.OfType<ToolStripMenuItem>())
            {
                new ToolStripMenuItemHandler(item, command);
            }
        }

        /// <summary>
        /// Binds the command.
        /// </summary>
        /// <param name="button">The button.</param>
        /// <param name="command">The command.</param>
        public static void BindCommand(this ToolStripButton button, Command command)
        {
            new ToolStripButtonHandler(button, command);
        }

        /// <summary>
        /// Binds the command.
        /// </summary>
        /// <param name="button">The button.</param>
        /// <param name="command">The command.</param>
        public static void BindCommand(this ToolStripComboBox button, Command command)
        {
            new ToolStripComboBoxHandler(button, command);
        }
        #endregion

        /// <summary>
        /// Binds the command.
        /// </summary>
        /// <param name="button">The button.</param>
        /// <param name="command">The command.</param>
        public static void BindCommand(this Button button, Command command)
        {
            button.DataBindings.Add(new Binding("Enabled", command, "Enabled"));
            button.DataBindings.Add(new Binding("Text", command, "DisplayName"));
            button.Click += ((sender, e) => command.Execute(((Button)sender).Tag, sender));
        }



        /// <summary>
        /// Binds the command.
        /// </summary>
        /// <param name="menuItem">The menu item.</param>
        /// <param name="command">The command.</param>
        public static void BindCommand(this MenuItem menuItem, Command command)
        {
            menuItem.Click += ((sender, e) => command.Execute(((MenuItem)sender).Tag, sender));
            menuItem.Enabled = command.Enabled;
            menuItem.Checked = command.IsChecked;
            command.PropertyChanged += ((sender, e) =>
            {
                if (e.PropertyName == "Enabled")
                    menuItem.Enabled = command.Enabled;
                if (e.PropertyName == "IsChecked")
                    menuItem.Checked = command.IsChecked;
            });
        }


    }
}