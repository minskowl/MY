using System;
using System.Windows.Forms;


namespace IES.PerformanceTester.Gui.Commands
{
    /// <summary>
    /// Command Binder helper
    /// </summary>
    public static class CommandBinder
    {
        #region ToolStrip

        /// <summary>
        /// Binds the command.
        /// </summary>
        /// <param name="menuItem">The menu item.</param>
        /// <param name="command">The command.</param>
        public static void BindCommand(this ToolStripMenuItem menuItem, Command command)
        {
            menuItem.Click += ((sender, e) =>
                                   {
                                       var parameter = ((ToolStripMenuItem)sender).Tag;
                                       if (command.CanExecute(parameter))
                                           command.Execute(parameter);
                                   });
            menuItem.Enabled = command.Enabled;
            menuItem.Checked = command.IsChecked;
            command.PropertyChanged += ((sender, e) =>
                                            {
                                                if (e.PropertyName == "Enabled")
                                                    menuItem.Enabled = command.Enabled;
                                                if (e.PropertyName == "IsChecked")
                                                    menuItem.Checked = command.IsChecked;
                                            });
            CommandManager.AddBinding(menuItem, command);
        }

        /// <summary>
        /// Binds the command.
        /// </summary>
        /// <param name="button">The button.</param>
        /// <param name="command">The command.</param>
        public static void BindCommand(this ToolStripButton button, Command command)
        {
            button.Click += ((sender, e) =>
                                 {
                                     var parameter = ((ToolStripButton)sender).Tag;
                                     if (command.CanExecute(parameter))
                                         command.Execute(parameter);
                                 });
            button.Enabled = command.Enabled;
            button.Checked = command.IsChecked;
            command.PropertyChanged += ((sender, e) =>
                                            {
                                                if (e.PropertyName == "Enabled")
                                                    button.Enabled = command.Enabled;
                                                if (e.PropertyName == "IsChecked")
                                                    button.Checked = command.IsChecked;
                                            });
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
            button.Click += ((sender, e) =>
                                 {
                                     var parameter = ((Button)sender).Tag;
                                     if (command.CanExecute(parameter))
                                         command.Execute(parameter);
                                 });
        }



        /// <summary>
        /// Binds the command.
        /// </summary>
        /// <param name="menuItem">The menu item.</param>
        /// <param name="command">The command.</param>
        public static void BindCommand(this MenuItem menuItem, Command command)
        {
            menuItem.Click += ((sender, e) =>
                                   {
                                       var parameter = ((MenuItem)sender).Tag;
                                       if (command.CanExecute(parameter))
                                           command.Execute(parameter);
                                   });
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

        /// <summary>
        /// Binds the command.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="eventName">Name of the event.</param>
        /// <param name="command">The command.</param>
        public static void BindCommand(this object source, string eventName, Command command)
        {
            var eventInfo = source.GetType().GetEvent(eventName);
            eventInfo.AddEventHandler(source, new EventHandler((sender, e) => command.Execute()));
        }
    }
}