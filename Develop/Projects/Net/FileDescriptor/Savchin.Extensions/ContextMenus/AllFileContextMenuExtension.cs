using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using Savchin.WinApi.Shell;

namespace Savchin.Extensions
{
    [Guid("A20C3AF4-6E50-42C2-AD70-851781917E12")]
    [ComVisible(true)]
    [TargetExtension("*", false)]
    public class AllFileContextMenuExtension : ContextMenuExtension
    {

        //   override 
        // Override this method to add your menu items to the context menu
        protected override void OnGetMenuItems(GetMenuitemsEventArgs e)
        {
            var parent = e.Menu.AddItem("Copy Info", "copyInfo", "Copy some information into clipboard");
            parent.HasSubMenu = true;

            //parent.OwnerDraw = true;
         var item1=   parent.SubMenu.InsertItem("Copy File Path", "copyFilePath", "Copied file path into clipboard.", 0);

            parent.SubMenu.InsertItem("Copy File Folder", "copyFileFolder", "Copied file folder into clipboard.", 1);
            parent.SubMenu.InsertItem("Copy File Name", "copyFileName", "Copied file name into clipboard.", 1);

        }

        /// <summary>
        /// Called when [initialize].
        /// </summary>
        /// <returns></returns>
        protected override bool OnInitialize()
        {
            // Add menu items only if 1 file is selected
            if (TargetFiles.Length != 1)
                return false;



            return true;
        }

        protected override bool OnExecuteMenuItem(ExecuteItemEventArgs e)
        {
            try
            {
                var file = TargetFiles[0];
                if (e.MenuItem.Verb == "copyFilePath")
                {
                    Clipboard.SetText(file);
                }
                else if (e.MenuItem.Verb == "copyFileFolder")
                {
                    Clipboard.SetText(Path.GetDirectoryName(file));
                }
                else if (e.MenuItem.Verb == "copyFileName")
                {
                    Clipboard.SetText(Path.GetFileName(file));
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            // Return value is ignored.
            return true;

        }
    }
}
