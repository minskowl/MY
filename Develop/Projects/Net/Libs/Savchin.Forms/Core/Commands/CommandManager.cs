using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using Savchin.Forms.Core.Commands.Handlers;

namespace Savchin.Forms.Core.Commands
{
    /// <summary>
    /// ParameterSource delegate
    /// </summary>
    public delegate object ParameterSource();

    /// <summary>
    /// CommandManager Class
    /// </summary>
    public static class CommandManager
    {
        private static readonly Dictionary<int, WeakReference> Bindigs = new Dictionary<int, WeakReference>();

        #region Interface


        /// <summary>
        /// Adds the binding.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <param name="binding">The binding.</param>
        internal static void AddBinding(Object control, ICommandBinding binding)
        {
            Bindigs[control.GetHashCode()] = new WeakReference(binding);
        }

        /// <summary>
        /// Clears the deads.
        /// </summary>
        public static void ClearDeads()
        {
            foreach (var key in Bindigs.Keys.ToArray())
            {
                try
                {
                    if (!Bindigs[key].IsAlive) Bindigs.Remove(key);
                }
                catch
                {
                    Bindigs.Remove(key);
                }
            }
        }

        /// <summary>
        /// Tracks the commands.
        /// </summary>
        /// <param name="strip">The strip.</param>
        /// <param name="form">The form.</param>
        public static void TrackCommands(ContextMenuStrip strip, Form form)
        {
            TrackCommands(strip);
            if (form != null) new ClosingHandler(strip, form);
        }

        /// <summary>
        /// Tracks the commands.
        /// </summary>
        /// <param name="strip">The strip.</param>
        public static void TrackCommands(ContextMenuStrip strip)
        {
            strip.Opening += StripOpening;
            TrackItemsState(strip.Items);
        }


        /// <summary>
        /// Tracks the commands.
        /// </summary>
        /// <param name="strip">The strip.</param>
        public static void TrackCommands(MenuStrip strip)
        {
            TrackCommands(strip, strip.FindForm());
        }
        /// <summary>
        /// Tracks the commands.
        /// </summary>
        /// <param name="strip">The strip.</param>
        /// <param name="form">The form.</param>
        public static void TrackCommands(MenuStrip strip, Form form)
        {
            TrackItemsState(strip.Items);
            if (form != null) new ClosingHandler(strip, form);
        }

        /// <summary>
        /// Releases the tracking.
        /// </summary>
        /// <param name="strip">The strip.</param>
        public static void ReleaseTracking(ContextMenuStrip strip)
        {
            strip.Opening -= StripOpening;
            ReleaseTracking(strip.Items);
        }

        /// <summary>
        /// Releases the tracking.
        /// </summary>
        /// <param name="strip">The strip.</param>
        public static void ReleaseTracking(MenuStrip strip)
        {
            ReleaseTracking(strip.Items);

        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// Handles the DropDownOpening event of the dropDown control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        static void DropDownDropDownOpening(object sender, EventArgs e)
        {
            var menu = (ToolStripDropDownItem)sender;
            InvalidateEnabled(menu.DropDownItems);
        }
        /// <summary>
        /// Handles the Opening event of the strip control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        static void StripOpening(object sender, CancelEventArgs e)
        {
            var menu = (ContextMenuStrip)sender;
            InvalidateEnabled(menu.Items);
        }

        #endregion


        #region Helpers
        private static void InvalidateEnabled(ToolStripItemCollection items)
        {
            foreach (var item in items.OfType<ToolStripItem>())
            {
                var binding = GetBinding(item);
                if (binding == null || binding.Command == null) continue;

                var parameter = binding.ParameterSource == null ? item.Tag : binding.ParameterSource();
                item.Enabled = binding.Command.CanExecute(parameter, item);
            }
        }

        private static ICommandBinding GetBinding(Object control)
        {
            var key = control.GetHashCode();
            if (!Bindigs.ContainsKey(key)) return null;
            try
            {
                return (ICommandBinding)Bindigs[key].Target;
            }
            catch
            {
                Bindigs.Remove(key);
                return null;
            }
        }

        private static void TrackItemsState(ToolStripItemCollection items)
        {
            foreach (var item in items.OfType<ToolStripDropDownItem>())
            {
                item.DropDownOpening += DropDownDropDownOpening;
            }
        }

        private static void ReleaseTracking(ToolStripItemCollection items)
        {
            foreach (ToolStripItem item in items)
            {
                var binding = GetBinding(item);
                if (binding != null)
                {
                    binding.RealeaseTracking();
                    Bindigs.Remove(item.GetHashCode());
                }

                var dropDown = item as ToolStripDropDownItem;
                if (dropDown != null)
                {
                    dropDown.DropDownOpening -= DropDownDropDownOpening;
                    ReleaseTracking(dropDown.DropDownItems);
                }
            }
        }

        #endregion

        private class ClosingHandler
        {
            private MenuStrip _menu;
            private ContextMenuStrip _contextMenu;
            private Form _form;

            /// <summary>
            /// Initializes a new instance of the <see cref="ClosingHandler"/> class.
            /// </summary>
            /// <param name="menu">The menu.</param>
            /// <param name="form">The form.</param>
            public ClosingHandler(ContextMenuStrip menu, Form form)
            {
                _contextMenu = menu;
                _form = form;

                _form.Closed += FormClosed;
            }
            /// <summary>
            /// Initializes a new instance of the <see cref="ClosingHandler"/> class.
            /// </summary>
            /// <param name="menu">The menu.</param>
            /// <param name="form">The form.</param>
            public ClosingHandler(MenuStrip menu, Form form)
            {
                _menu = menu;
                _form = form;

                _form.Closed += FormClosed;
            }


            void FormClosed(object sender, EventArgs e)
            {
                _form.Closed -= FormClosed;
                _form = null;

                if (_contextMenu != null)
                {
                    ReleaseTracking(_contextMenu);
                    _contextMenu = null;
                }

                if (_menu != null)
                {
                    ReleaseTracking(_menu);
                    _menu = null;
                }
                ClearDeads();
            }
        }
    }
}