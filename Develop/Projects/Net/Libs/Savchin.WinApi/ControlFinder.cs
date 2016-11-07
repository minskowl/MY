using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Savchin.WinApi.UserActivity;

namespace Savchin.WinApi
{
    /// <summary>
    /// ControlFinder
    /// </summary>
    public class ControlFinder : Component
    {

        /// <summary>
        /// Occurs when [control finded].
        /// </summary>
        public event FormComponentEventHandler FindedControl;
        /// <summary>
        /// Occurs when [control selected].
        /// </summary>
        public event FormComponentEventHandler SelectedControl;

        #region Properties
        private Color _backupBackColor;
        private object _controlHovered;

        private MouseButtons _selectionButton = MouseButtons.Right;
        /// <summary>
        /// Gets or sets the selection button.
        /// </summary>
        /// <value>The selection button.</value>
        public MouseButtons SelectionButton
        {
            get { return _selectionButton; }
            set
            {
                if (value == MouseButtons.Left)
                    throw new ArgumentOutOfRangeException("value");

                _selectionButton = value;
            }
        }

        private bool _isSelectionEnabled;
        /// <summary>
        /// Gets or sets a value indicating whether this instance is selection enabled.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is selection enabled; otherwise, <c>false</c>.
        /// </value>
        public bool IsSelectionEnabled
        {
            get { return _isSelectionEnabled; }
            set
            {
                if (_isSelectionEnabled == value) return;

                _isSelectionEnabled = value;
                if (_isSelectionEnabled)
                    SetupHandlers();
                else
                    ReleaseHandlers();
            }
        }

        /// <summary>
        /// Gets a value indicating whether the component can raise an event.
        /// </summary>
        /// <value></value>
        /// <returns>true if the component can raise events; otherwise, false. The default is true.
        /// </returns>
        protected override bool CanRaiseEvents
        {
            get { return true; }
        }


        private object _controlSelected;
        /// <summary>
        /// Gets the control selected.
        /// </summary>
        /// <value>The control selected.</value>
        public object ControlSelected
        {
            get { return _controlSelected; }
        }

        private Form _formSelected;
        /// <summary>
        /// Gets the form selected.
        /// </summary>
        /// <value>The form selected.</value>
        public Form FormSelected
        {
            get { return _formSelected; }
        }

        #endregion

        /// <summary>
        /// Releases the unmanaged resources used by the <see cref="T:System.ComponentModel.Component"/> and optionally releases the managed resources.
        /// </summary>
        /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            IsSelectionEnabled = false;
            base.Dispose(disposing);
        }

        #region Event Handlers
        void HookManager_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != _selectionButton || _controlHovered == null) return;

            IsSelectionEnabled = false;
            if (SelectedControl == null) return;

            _controlSelected = _controlHovered;
            SelectedControl(this, new FormComponentEventArgs(_formSelected, _controlSelected));
            RestoreBackground();
        }


        void HookManager_MouseMove(object sender, MouseEventArgs e)
        {
            var form = Form.ActiveForm;
            if (form == null) return;


            var point = new Point(e.X, e.Y);

            var control = FindInMenu(form, point) ?? (object)Find(form, point);
            if (control != null)
                Console.WriteLine("Find control !!!!!!!!!!!  " + control.ToString());



            if (control != null && _controlHovered != null && ReferenceEquals(_controlHovered, control))
                return;

            if (_controlHovered != null)
            {
                RestoreBackground();
            }
            if (control == null) return;

            _formSelected = form;
            HoverFormComponent(control);

            if (FindedControl != null) FindedControl(this, new FormComponentEventArgs(_formSelected, control));
        }

        #endregion

        #region Helpers

        #region Finders

        private ToolStripItem FindInMenu(Form form, Point point)
        {
            var menu = form.MainMenuStrip;
            if (menu == null) return null;
            var clientPoint = menu.PointToClient(point);
            return FindInItems(menu.Items, clientPoint, point);
        }

        private ToolStripItem FindInItems(ToolStripItemCollection items, Point point, Point screenPoint)
        {
            ToolStripItem result = null;
            foreach (ToolStripItem item in items)
            {
                if (item is ToolStripMenuItem)
                {
                    var submenu = (ToolStripDropDownItem)item;
                    if (submenu.DropDown.Visible)
                    {
                        var clientPoint = submenu.DropDown.PointToClient(screenPoint);
                        if (clientPoint.X >= 0 && clientPoint.Y >= 0)
                            result = FindInItems(
                                submenu.DropDownItems,
                                submenu.DropDown.PointToClient(screenPoint),
                                screenPoint);
                        if (result != null) return result;
                    }
                }


                if (item.Visible && item.Bounds.Contains(point))
                    return item;

            }
            return null;
        }



        private Control Find(Control parent, Point point)
        {

            var control = parent.GetChildAtPoint(parent.PointToClient(point));
            if (control == null)
                return null;

            var childControl = Find(control, point);
            return childControl == null ? control : childControl;
        }

        #endregion

        #region Hovers
        private void HoverFormComponent(object o)
        {
            if (o is Control)
                HoverControl((Control)o);
            else if (o is ToolStripItem)
                HoverToolStripItem((ToolStripItem)o);
        }

        private void HoverControl(Control control)
        {
            _backupBackColor = control.BackColor;
            control.BackColor = Color.Red;
            _controlHovered = control;
        }

        private void HoverToolStripItem(ToolStripItem control)
        {
            _backupBackColor = control.BackColor;
            control.BackColor = Color.Red;
            _controlHovered = control;
        }
        #endregion

        private void ReleaseHandlers()
        {
            HookManager.MouseMove -= HookManager_MouseMove;
            HookManager.MouseClick -= HookManager_MouseClick;
        }

        private void SetupHandlers()
        {
            HookManager.MouseMove += HookManager_MouseMove;
            HookManager.MouseClick += HookManager_MouseClick;
        }



        private void RestoreBackground()
        {
            if (_controlHovered is Control)
                ((Control)_controlHovered).BackColor = _backupBackColor;
            else if (_controlHovered is ToolStripItem)
                ((ToolStripItem)_controlHovered).BackColor = _backupBackColor;
            _controlHovered = null;
        }
        #endregion
    }
}