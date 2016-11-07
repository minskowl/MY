using System;
using System.Windows.Forms;
using Savchin.Forms.Docking;


namespace CodeRocket.Controls
{
    public partial class PropertyConsole : DockContent
    {
        private Form _selectedForm;
        /// <summary>
        /// Gets or sets the selected form.
        /// </summary>
        /// <value>The selected form.</value>
        public Form SelectedForm
        {
            get { return _selectedForm; }
            set
            {
                _selectedForm = value;
                if (value != null)
                    InitCombo();
            }
        }

        /// <summary>
        /// Gets the browser.
        /// </summary>
        /// <value>The browser.</value>
        public PropertyGrid Browser
        {
            get { return propertyGrid; }
        }

        /// <summary>
        /// Gets or sets the selected object.
        /// </summary>
        /// <value>The selected object.</value>
        public object SelectedObject
        {
            get { return propertyGrid.SelectedObject; }
            set { propertyGrid.SelectedObject = value; }
        }



        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyConsole"/> class.
        /// </summary>
        public PropertyConsole()
        {
            InitializeComponent();

            HideOnClose = true;
            ShowHint = DockState.DockRight;
            TabText = "Properties";
        }

        /// <summary>
        /// Inits the combo.
        /// </summary>
        private void InitCombo()
        {
            comboBoxControls.Items.Clear();

            comboBoxControls.Items.Add(new ComboRow { Control = _selectedForm });

            foreach (Control control in _selectedForm.Controls)
            {
                comboBoxControls.Items.Add(new ComboRow { Control = control });
            }
        }
        /// <summary>
        /// Handles the SelectedIndexChanged event of the comboBoxControls control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void comboBoxControls_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxControls.SelectedItem != null)
            {
                propertyGrid.SelectedObject =((ComboRow)comboBoxControls.SelectedItem).Control;
            }
        }
        public class ComboRow
        {
            public Control Control { get; set; }
            /// <summary>
            /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
            /// </summary>
            /// <returns>
            /// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
            /// </returns>
            public override string ToString()
            {
                return Control.Name + " " + Control.GetType().FullName;
            }
        }


    }
}