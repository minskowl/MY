using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using Savchin.WinApi;
using Savchin.WinApi.OleStorage;

namespace FileDescriptor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //using (var PropSetStg = new PropertySetStorage(@"c:\indicators.csv"))
            //{
            //    PropertyStorage PropStg;
            //    try
            //    {
            //        PropStg = PropSetStg.Open(PropertySetStorage.FMTID_UserProperties);
            //    }
            //    catch
            //    {
            //        PropStg = PropSetStg.Create(PropertySetStorage.FMTID_UserProperties);
            //    }
            //    var descr = PropStg["RtfDescription"];
            //    if (descr is byte[])
            //        SetDescription((byte[])descr);
            //}





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

        private void buttonOK_Click(object sender, EventArgs e)
        {
            //using (var PropSetStg = new PropertySetStorage(@"c:\indicators.csv"))
            //{

            //    // Open the property storage
            //    using (var PropStg = PropSetStg.Open(PropertySetStorage.FMTID_UserProperties))
            //    {
            //        PropStg["RtfDescription"] = GetDescription();
            //        PropStg.Flush();
            //    }
            //    using (var PropStg = PropSetStg.Open(PropertySetStorage.FMTID_SummaryInformation))
            //    {
            //        PropStg[(int)PropertyStorage.SummaryProperty.Comments] = editor.DocumentText;
            //        PropStg.Flush();
            //    }




            //}
            var hInstance = Marshal.GetHINSTANCE(typeof(Form1).Module);
            var hWnd = User32.CreateDialogParam(hInstance, 100, IntPtr.Zero, DialogProc, IntPtr.Zero);
            RECT res;
            User32.GetClientRect(hWnd, out res);
            User32.DestroyWindow(hWnd);

        }
  

        public int DialogProc(IntPtr hWnd, int uMsg, IntPtr wParam, IntPtr lParam)
        {
            return 0;
        }
    }
}
