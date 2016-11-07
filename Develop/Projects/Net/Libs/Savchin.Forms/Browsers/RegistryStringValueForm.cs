using System;
using System.Windows.Forms;
using Microsoft.Win32;

namespace Savchin.Forms.Browsers
{
    public partial class RegistryStringValueForm : Form
    {
        public RegistryStringValueForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the Click event of the buttonOK control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void buttonOK_Click(object sender, EventArgs e)
        {
            DialogResult=DialogResult.OK;
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        /// <summary>
        /// Edits the value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="valueName">Name of the value.</param>
        public static void EditStringValue(RegistryKey key, string valueName)
        {
            RegistryStringValueForm form= new RegistryStringValueForm();
            form.Text = "Value of:" + valueName;
       
            form.textBox1.Text = (string)key.GetValue(valueName);

            if (form.ShowDialog() != DialogResult.OK)
                return;
            //key.(new RegistrySecurity());
            Registry.SetValue(key.Name, valueName, form.textBox1.Text, RegistryValueKind.String);
            
        }

        /// <summary>
        /// Edits the DWORD value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="valueName">Name of the value.</param>
        public static void EditDWORDValue(RegistryKey key, string valueName)
        {
            RegistryStringValueForm form = new RegistryStringValueForm();
            form.Text = "Value of:" + valueName;

            form.textBox1.Text = key.GetValue(valueName).ToString();

            if (form.ShowDialog() != DialogResult.OK)
                return;
            int value;
            if (!int.TryParse(form.textBox1.Text, out value))
                return;
            Registry.SetValue(key.Name ,valueName, value, RegistryValueKind.DWord);

        }
    }
}