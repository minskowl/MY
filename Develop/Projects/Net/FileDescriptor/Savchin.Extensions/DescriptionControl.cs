using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Savchin.WinApi.OleStorage;
using Savchin.WinApi.Shell;

namespace Savchin.Extensions
{
    [Guid("A86AD81B-028C-4DF5-90A9-2532BE58F075")]
    [ComVisible(true)]
    [TargetExtension("*", false)]
    [RegistryKeyNameAttribute("Savchin Inc. Extensions")]
    public partial class DescriptionControl : PropertySheetExtension
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="DescriptionControl"/> class.
        /// </summary>
        public DescriptionControl()
        {




            InitializeComponent();
            Text = "Summary v1.0.3";
            Name = "Summary v1.0.3";
            editor.Editor.ModifiedChanged += new EventHandler(Editor_ModifiedChanged);

        }

        /// <summary>
        /// Handles the ModifiedChanged event of the Editor control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        void Editor_ModifiedChanged(object sender, EventArgs e)
        {
            if (editor.Modified)
                Changed();
        }
        protected override bool OnActivate()
        {
            try
            {
                using (var propertySet = new FilePropertySet(TargetFiles[0]))
                {
                    var descr = propertySet.GetRtfDescription();
                    if (descr != null && descr.Length > 0)
                        SetDescription(descr);
                    else
                        editor.DocumentText = propertySet.GetComment();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return base.OnActivate();
        }

        protected override NotifyResult OnApply()
        {
            try
            {
                using (var propertySet = new FilePropertySet(TargetFiles[0]))
                {
                    propertySet.SetRtfDescription(GetDescription());
                    var comment = editor.DocumentText;
                    if (!string.IsNullOrEmpty(comment))
                        propertySet.SetComment(comment);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return NotifyResult.Invalid;
            }

            return base.OnApply();

        }

        private byte[] GetDescription()
        {
            using (var stream = new MemoryStream())
            {
                editor.SaveFile(stream, RichTextBoxStreamType.RichText);
                return stream.ToArray();
            }
        }
        private void SetDescription(byte[] data)
        {
            using (var stream = new MemoryStream(data))
            {
                editor.LoadFile(stream, RichTextBoxStreamType.RichText);

            }
        }
    }
}
