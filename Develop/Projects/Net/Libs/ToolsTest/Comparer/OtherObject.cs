using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using NUnit.Framework;

namespace ToolsTest.Comparer
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto, Pack = 2)]
    internal struct x0b92a08e52b9ff9c
    {
       // internal x2370ad8218c5b454 x44ecfea61c937b8e;
        internal uint xdec78e8ca1b23607;
        internal ushort xab3ad07eee64fe38;
        internal short x;
        internal short y;
        internal short cx;
        internal short cy;
        internal short x27563cc5c53efffb;
        internal short x4c02e7c160d65a3b;
        internal short x6548c9a581d109dc;
        internal short x592f8828575d6342;
        [MarshalAs(UnmanagedType.LPWStr)]
        internal string x34db0a9fc671eb21;
    }

    [TestFixture]
    public class OtherObject
    {
        [Test]
        public void Test()
        {
            x0b92a08e52b9ff9c structure = new x0b92a08e52b9ff9c();
   
            structure.cx = 15;
            structure.cy = 20;
           // structure.x44ecfea61c937b8e = x2370ad8218c5b454.x4fc8bfd0f16f31b4;
            structure.x592f8828575d6342 = 8;
            byte[] source = ConvertByteArray("MS Shell Dlg");
            int num = Marshal.SizeOf(typeof(x0b92a08e52b9ff9c));
            IntPtr ptr = Marshal.AllocHGlobal(num + source.Length);
            Marshal.StructureToPtr(structure, ptr, false);
            Marshal.Copy(source, 0, (IntPtr)(((long)ptr) + num), source.Length);
          
        }


        internal byte[] ConvertByteArray(string text)
        {
            int length = text.Length;
            byte[] buffer = new byte[(length + 1) * 2];
            char[] chArray = text.ToCharArray();
            int index = 0;
            for (int i = 0; i < length; i++)
            {
                buffer[index++] = (byte)chArray[i];
                buffer[index++] = 0;
            }
            buffer[index++] = 0;
            buffer[index] = 0;
            return buffer;
        }
    }
}
