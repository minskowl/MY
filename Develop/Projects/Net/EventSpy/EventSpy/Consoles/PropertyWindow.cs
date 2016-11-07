using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;


namespace Savchin.EventSpy.Consoles
{
    public partial class PropertyWindow : ToolWindow
    {
        private Form selectedForm;
        /// <summary>
        /// Gets or sets the selected form.
        /// </summary>
        /// <value>The selected form.</value>
        public Form SelectedForm
        {
            get { return selectedForm; }
            set
            {
                selectedForm = value;
                if (value != null)
                    InitCombo();
            }
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
        /// Initializes a new instance of the <see cref="PropertyWindow"/> class.
        /// </summary>
        public PropertyWindow()
        {
            InitializeComponent();
            propertyGrid.PropertyTabs.AddTabType(typeof(EventListenersTab), PropertyTabScope.Static);
        }

        /// <summary>
        /// Inits the combo.
        /// </summary>
        private void InitCombo()
        {
            comboBoxControls.Items.Clear();

            comboBoxControls.Items.Add(new ComboRow { Control = selectedForm });

            foreach (Control control in selectedForm.Controls)
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