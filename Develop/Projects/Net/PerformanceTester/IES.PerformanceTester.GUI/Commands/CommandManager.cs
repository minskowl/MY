using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace IES.PerformanceTester.Gui.Commands
{
    /// <summary>
    /// CommandManager
    /// </summary>
    public static class CommandManager
    {
        private static Dictionary<Object, Command> commandBindigs = new Dictionary<Object, Command>();

        /// <summary>
        /// Adds the binding.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <param name="command">The command.</param>
        public static void AddBinding(Object control, Command command)
        {
            if (commandBindigs.ContainsKey(control))
            {
                commandBindigs[control] = command;
            }
            else
            {
                commandBindigs.Add(control, command);
            }

        }
        /// <summary>
        /// Gets the command.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <returns></returns>
        public static Command GetCommand(Object control)
        {
            return commandBindigs.ContainsKey(control) ? commandBindigs[control] : null;
        }

        /// <summary>
        /// Invalidates the can execute.
        /// </summary>
        /// <param name="menu">The menu.</param>
        public static void InvalidateCanExecute(ContextMenu menu)
        {
            foreach (MenuItem item in menu.MenuItems)
            {
                var command = GetCommand(item);
                if (command != null)
                    item.Enabled = command.CanExecute(item.Tag);
            }
        }
        /// <summary>
        /// Invalidates the can execute.
        /// </summary>
        /// <param name="items">The items.</param>
        public static void InvalidateCanExecute(ToolStripItemCollection items)
        {
            foreach (ToolStripItem item in items)
            {
                var command = GetCommand(item);
                if (command != null)
                    item.Enabled = command.CanExecute(item.Tag);
            }
        }
        #region Track\Release
        /// <summary>
        /// Tracks the commands.
        /// </summary>
        /// <param name="strip">The strip.</param>
        public static void TrackCommands(ContextMenuStrip strip)
        {
            strip.Opening += strip_Opening;
            foreach (var dropDownItem in strip.Items.OfType<ToolStripDropDownItem>())
            {
                dropDownItem.DropDownOpening += dropDownItem_DropDownOpening;
            }
        }


        /// <summary>
        /// Tracks the commands.
        /// </summary>
        /// <param name="strip">The strip.</param>
        public static void TrackCommands(MenuStrip strip)
        {
            foreach (var dropDownItem in strip.Items.OfType<ToolStripDropDownItem>())
            {
                dropDownItem.DropDownOpening += dropDownItem_DropDownOpening;
            }
        }

        /// <summary>
        /// Releases the tracking.
        /// </summary>
        /// <param name="strip">The strip.</param>
        public static void ReleaseTracking(ContextMenuStrip strip)
        {
            strip.Opening -= strip_Opening;
            foreach (var dropDownItem in strip.Items.OfType<ToolStripDropDownItem>())
            {
                dropDownItem.DropDownOpening -= dropDownItem_DropDownOpening;
            }
        }
        /// <summary>
        /// Releases the tracking.
        /// </summary>
        /// <param name="strip">The strip.</param>
        public static void ReleaseTracking(MenuStrip strip)
        {
            foreach (var dropDownItem in strip.Items.OfType<ToolStripDropDownItem>())
            {
                dropDownItem.DropDownOpening -= dropDownItem_DropDownOpening;
            }
        }
        #endregion
        static void dropDownItem_DropDownOpening(object sender, EventArgs e)
        {
            InvalidateCanExecute(((ToolStripDropDownItem)sender).DropDownItems);
        }
        static void strip_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            InvalidateCanExecute(((ContextMenuStrip)sender).Items);
        }
    }
}