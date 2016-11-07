using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using Savchin.Extensions.Core;
using Savchin.WinApi.Shell;

namespace Savchin.Extensions.ContextMenus
{


    [Guid("2996F860-5D85-45FC-ABFB-B92867A9CD30")]
    [ComVisible(true)]
    [TargetExtension(".sln", true)]
    [RegistryKeyNameAttribute("Savchin Inc. Extensions")]
    public class SlnFileContextMenuExtension : ContextMenuExtension
    {


        //   override 
        // Override this method to add your menu items to the context menu
        protected override void OnGetMenuItems(GetMenuitemsEventArgs e)
        {

            var parent = e.Menu.AddItem("Solution", "solution", "Solution");
            parent.HasSubMenu = true;

            foreach (var studio in VisualStudio.InstalledStudios)
            {
                var studioMenu = parent.SubMenu.AddItem(studio.Version.ToString());
                studioMenu.HasSubMenu = true;
                CreateStudioMenu(studioMenu, studio.Path);
            }

            var studioDeafult = parent.SubMenu.AddItem("Default");
            studioDeafult.HasSubMenu = true;
            CreateStudioMenu(studioDeafult, null);
        }

        private void CreateStudioMenu(ShellMenuItem parent, object tag)
        {
            var item = parent.SubMenu.InsertItem("Build Debug", "BuildDebug", "Build solution in Debub configuration", 0);
            item.Tag = tag;
            item = parent.SubMenu.InsertItem("Build Release", "BuildRelease", "Build solution in Release configuration", 1);
            item.Tag = tag;
        }

        /// <summary>
        /// Called when [initialize].
        /// </summary>
        /// <returns></returns>
        protected override bool OnInitialize()
        {
            // Add menu items only if 1 file is selected
            if (TargetFiles.Length > 1)
                return false;

            return true;
        }

        protected override bool OnExecuteMenuItem(ExecuteItemEventArgs e)
        {
            try
            {
                var file = TargetFiles[0];


                if (e.MenuItem.Verb == "BuildDebug")
                {
                    Build(file, "Debug", (string)e.MenuItem.Tag);
                }
                else if (e.MenuItem.Verb == "BuildRelease")
                {
                    Build(file, "Release", (string)e.MenuItem.Tag);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            // Return value is ignored.
            return true;

        }
        void Build(string file, string config, string studioPath)
        {
 
           Process.Start("SolutionBuilder.exe", string.Format("\"{0}\" \"{1}\" \"{2}\"", file, config, studioPath));

        }




    }
}
