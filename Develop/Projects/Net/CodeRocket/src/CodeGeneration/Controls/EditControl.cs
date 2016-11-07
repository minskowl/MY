using System.IO;
using Syncfusion.Windows.Forms.Edit.Enums;

namespace Savchin.CodeGeneration.Controls
{
    public class EditControl : Syncfusion.Windows.Forms.Edit.EditControl
    {
        /// <summary>
        /// Loads the file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        public new void LoadFile(string fileName)
        {

            switch (Path.GetExtension(fileName))
            {
                case ".cs":
                    ApplyConfiguration(KnownLanguages.CSharp);
                    break;
                case ".vb":
                    ApplyConfiguration(KnownLanguages.VBNET);
                    break;
                case ".sql":
                    ApplyConfiguration(KnownLanguages.SQL);
                    break;
                case ".xml":
                    ApplyConfiguration(KnownLanguages.XML);
                    break;
                case ".html":
                case ".htm":
                    ApplyConfiguration(KnownLanguages.HTML);
                    break;
                default:
                    //SettingFile = "";
                    break;
            }

            base.LoadFile(fileName);
            //this.AutoFormatText(1, Lines.Length);
        }

    }
}
