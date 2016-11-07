using System;
using System.Windows.Forms;

namespace Savchin.Forms.Browsers
{
    /// <summary>
    /// ControlBrowser
    /// </summary>
    public partial class ControlBrowser : UserControl
    {
        private Form SelectedForm
        {
            get
            {
                var item = listForms.SelectedItem;
                return item == null ? null : ((FormContaner)item).Form;
            }
        }
        private Control SelectedControl
        {
            get
            {
                var item = listControls.SelectedItem;
                return item == null ? null : ((ControlContaner)item).Control;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ControlBrowser"/> class.
        /// </summary>
        public ControlBrowser()
        {
            InitializeComponent();

            listControls.Sorted = true;
            listForms.Sorted = true;
        }




        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Form.Load"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data.</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
                FillFormsList();
        }

        #region Event Handlers

        private void SelectControlClick(object sender, EventArgs e)
        {
            controlFinder1.IsSelectionEnabled = true;
        }
        private void RefreshClick(object sender, EventArgs e)
        {
            FillFormsList();
        }

        private void controlFinder1_SelectedControl(object sender, Savchin.WinApi.FormComponentEventArgs e)
        {
            ShowObject(e.Component);
            controlFinder1.IsSelectionEnabled = false;
        }

        private void listForms_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillControlList();
        }

        private void listControls_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowObject(SelectedControl);
        } 
        #endregion

        private void ShowObject(object o)
        {
            propertyGrid1.SelectedObject = o;
            textBox1.Text = o == null ?
                string.Empty : o.GetType().FullName + " " + o;
        }

        private void FillFormsList()
        {
            listForms.Items.Clear();
            foreach (Form form in Application.OpenForms)
            {
                listForms.Items.Add(new FormContaner { Form = form });
            }

            if (listForms.Items.Count > 0)
                listForms.SelectedIndex = 0;
        }

        private void FillControlList()
        {
            listControls.Items.Clear();
            var form = SelectedForm;
            if (form == null) return;
            AddControl(form);
        }

        private void AddControl(Control control)
        {
            listControls.Items.Add(new ControlContaner { Control = control });
            foreach (Control sub in control.Controls)
            {
                AddControl(sub);
            }
        }

        private class FormContaner
        {
            /// <summary>
            /// Gets or sets the form.
            /// </summary>
            /// <value>The form.</value>
            public Form Form { get; set; }
            /// <summary>
            /// Returns a <see cref="System.String"/> that represents this instance.
            /// </summary>
            /// <returns>
            /// A <see cref="System.String"/> that represents this instance.
            /// </returns>
            public override string ToString()
            {
                return Form == null ? "<Empty>" : Form.GetType().FullName + " " + Form.Name;
            }
        }

        private class ControlContaner
        {
            /// <summary>
            /// Gets or sets the form.
            /// </summary>
            /// <value>The form.</value>
            public Control Control { get; set; }
            public override string ToString()
            {
                return Control == null ? "<Empty>" : Control.GetType().FullName + " " + Control.Name;
            }
        }


    }
}