using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using Savchin.WinApi.Shell;
using IDataObject = System.Runtime.InteropServices.ComTypes.IDataObject;

namespace FileDescriptor
{


    [Guid("751AFB1A-A4A8-4b2f-974F-9FDBA5C9088A"), ComVisible(true)]
    public class PropertyPageLoader : IShellExtInit, IShellPropSheetExt
    {
        private IDataObject dobj = null;
        private string sClass;
        private string sObjADsPath;

        public PropertyPageLoader()
        {
        }

        /// <summary>
        /// Get data about object we want properties for
        /// </summary>
        /// <param name="pidlFolder"></param>
        /// <param name="lpdobj"></param>
        /// <param name="hKeyProgID"></param>
        /// <returns></returns>
        int IShellExtInit.Initialize(IntPtr pidlFolder, IDataObject lpdobj, uint hKeyProgID)
        {
            dobj = lpdobj;
            return 0;
        }

        /// <summary>
        /// Add property page for this object
        /// </summary>
        /// <param name="lpfnAddPage"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        int IShellPropSheetExt.AddPages(LPFNSVADDPROPSHEETPAGE pfnAddPage, IntPtr lParam)
        {
            try
            {
                // ADD PAGES HERE
                PropertySheetControl samplePage;
                PROPSHEETPAGE psp;
                IntPtr hPage;

                // In ActiveDirectory extensions, Get type of object, and show page according to class
                // GetADPath();
                // if(sClass.ToLower() == "organizationalunit") {...}


                // create new inherited property page(s) and pass dobj to it
                samplePage = new DescriptionControl();
                psp = samplePage.GetPSP(250, 230);



                hPage = Comctl32.CreatePropertySheetPage(ref psp);
                bool result = pfnAddPage(hPage, lParam);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return 0;
        }

        /// <summary>
        /// Not Used here!
        /// </summary>
        /// <param name="uPageID"></param>
        /// <param name="lpfnReplacePage"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        int IShellPropSheetExt.ReplacePage(uint uPageID, IntPtr lpfnReplacePage, IntPtr lParam)
        {
            return 0;
        }


     
    }
}
