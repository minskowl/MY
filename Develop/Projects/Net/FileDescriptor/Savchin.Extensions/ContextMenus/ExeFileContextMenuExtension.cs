using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Savchin.WinApi.Shell;



namespace Savchin.Extensions.ContextMenus
{
    [Guid("8F26EC84-C734-4192-9257-8EF8B3E20390")]
    [ComVisible(true)]
    [TargetExtension(".dll", true)]
    [TargetExtension(".exe", true)]
    [RegistryKeyNameAttribute("Savchin Inc. Extensions")]
    public class ExeFileContextMenuExtension : ContextMenuExtension
    {


        //   override 
        // Override this method to add your menu items to the context menu
        protected override void OnGetMenuItems(GetMenuitemsEventArgs e)
        {
            var parent = e.Menu.AddItem("Assembly", "assembly", "Assembly");
            parent.HasSubMenu = true;

            //parent.OwnerDraw = true;
            parent.SubMenu.InsertItem("Install to GAC", "installtogac", "Install assembly into GAC", 0);
            //submenuitem.OwnerDraw = true;

            parent.SubMenu.InsertItem("Uninstall from GAC", "uninstallfromgac", "Uninstall assembly from GAC", 1);
            //submenuitem.OwnerDraw = true;



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


            // Add menu items only if file is a .Net assembly
            if (!IsDotNetAssembly(TargetFiles[0]))
                return false;

            return true;
        }

        protected override bool OnExecuteMenuItem(ExecuteItemEventArgs e)
        {
            try
            {
                var file = TargetFiles[0];
                if (e.MenuItem.Verb == "installtogac")
                {
                   var result= Savchin.Core.Fusion.AddAssemblytoGac(file);
                   MessageBox.Show(string.Format("Rsult to install into GAC {0}",result));
                }
                else if (e.MenuItem.Verb == "uninstallfromgac")
                {
                    var result = Savchin.Core.Fusion.GacUninstall(Path.GetFileNameWithoutExtension(file));
                    MessageBox.Show(result
                                        ? "Assembly unistalled sucesfuly"
                                        : "Failure unistall assembly");
                }
                //  var assemlby =Assembly.ReflectionOnlyLoad(file);
                //MessageBox.Show(assemlby.ImageRuntimeVersion);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            // Return value is ignored.
            return true;

        }


        // Determines whether the dll or exe file is a .Net assembly
        bool IsDotNetAssembly(string filename)
        {
            try
            {
                byte[] data = new byte[4096];
                FileInfo file = new FileInfo(filename);
                Stream fin = file.Open(FileMode.Open, FileAccess.Read);
                int iRead = fin.Read(data, 0, 4096);
                fin.Close();

                // Verify this is a executable/dll
                if ((data[1] << 8 | data[0]) != 0x5a4d)
                    return false;

                // This will get the address for the WinNT header
                int iWinNTHdr = data[63] << 24 | data[62] << 16 | data[61] << 8 | data[60];
                // Verify this is an NT address
                if ((data[iWinNTHdr + 3] << 24 | data[iWinNTHdr + 2] << 16
                    | data[iWinNTHdr + 1] << 8
                    | data[iWinNTHdr]) != 0x00004550)
                    return false;

                int iLightningAddr = iWinNTHdr + 24 + 208;
                int iSum = 0;
                int iTop = iLightningAddr + 8;
                for (int i = iLightningAddr; i < iTop; i++)
                    iSum |= data[i];
                if (iSum == 0)
                    return false;
                else
                    return true;
            }
            catch
            {
                return true;
            }
        }




    }
}
