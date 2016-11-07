using System;
using System.Collections.Generic;
using Microsoft.Win32;

namespace Savchin.Extensions.Core
{
    /// <summary>
    /// Summary description for Builder.
    /// </summary>
    public class VisualStudio
    {

        private static VisualStudioInfo[] _installedStudios;
        public static VisualStudioInfo[] InstalledStudios
        {
            get { return _installedStudios ?? (_installedStudios = GetIstalledStudios()); }
        }



        private static VisualStudioInfo[] GetIstalledStudios()
        {
            var key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\VisualStudio");
            if (key == null)
                return new VisualStudioInfo[0];
            var result = new List<VisualStudioInfo>();
            foreach (var subKeyName in key.GetSubKeyNames())
            {
                try
                {

                    var version = new Version(subKeyName);
                    var path = key.OpenSubKey(subKeyName).GetValue("InstallDir").ToString();
                    result.Add(new VisualStudioInfo { Version = version, Path = path });

                }
                catch
                {

                }

            }
            return result.ToArray();
        }

    }

    public class VisualStudioInfo
    {
        public string Path { get; set; }
        public Version Version { get; set; }
    }
}
