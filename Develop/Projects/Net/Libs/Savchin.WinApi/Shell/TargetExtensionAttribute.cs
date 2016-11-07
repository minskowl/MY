using System;
using Microsoft.Win32;

namespace Savchin.WinApi.Shell
{
    [AttributeUsage(AttributeTargets.ReturnValue | AttributeTargets.Delegate | AttributeTargets.Parameter | AttributeTargets.Interface | AttributeTargets.Event | AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Method | AttributeTargets.Constructor | AttributeTargets.Enum | AttributeTargets.Struct | AttributeTargets.Class | AttributeTargets.Module | AttributeTargets.Assembly, AllowMultiple = true)]
    public class TargetExtensionAttribute : Attribute
    {
        // Fields
        internal string fileExtension;
        internal string fileProgID;
        internal RegistryHive rh;

        // Methods
        public TargetExtensionAttribute(RegistryHive root, string subKeyPath)
        {
            if (!Enum.IsDefined(typeof(RegistryHive), root))
            {
                throw new ArgumentException();
            }
            rh = root;
            fileExtension = subKeyPath;
            while (fileExtension.StartsWith(@"\"))
            {
                fileExtension = fileExtension.Remove(0, 1);
            }
            while (fileExtension.EndsWith(@"\"))
            {
                fileExtension = fileExtension.Substring(0, fileExtension.Length - 1);
            }
        }

        public TargetExtensionAttribute(string param, bool extension)
        {
            rh = (RegistryHive)0;
            if ((param == null) || (param == string.Empty))
            {
                throw new ArgumentException();
            }
            if (extension)
            {
                fileExtension = param;
                fileProgID = "";
            }
            else
            {
                fileExtension = "";
                fileProgID = param;
            }
        }

        internal static TargetExtensionAttribute[] AttributesFromType(Type t)
        {
            return (TargetExtensionAttribute[])t.GetCustomAttributes(typeof(TargetExtensionAttribute), false);
        }

        // Properties
        public string ProgID
        {
            get
            {
                if (fileProgID != string.Empty)
                {
                    return fileProgID;
                }
                RegistryKey key = Registry.ClassesRoot.CreateSubKey(fileExtension);
                if (key == null)
                {
                    return null;
                }
                string str = (string)key.GetValue(string.Empty);
                if (string.IsNullOrEmpty(str))
                {

                    str = fileExtension.Replace(".", null) + "file";
                    key.SetValue(string.Empty, str);
                }
                else
                {
                    RegistryKey key2 = Registry.ClassesRoot.OpenSubKey(str + @"\CurVer", false);
                    if (key2 != null)
                    {
                        str = (string)key.GetValue(string.Empty);
                        key2.Close();
                    }

                }

                key.Close();
                return str;
            }
        }

        internal RegistryKey RootKey
        {
            get
            {
                if (rh == RegistryHive.ClassesRoot)
                {
                    return Registry.ClassesRoot;
                }
                if (rh == RegistryHive.CurrentConfig)
                {
                    return Registry.CurrentConfig;
                }
                if (rh == RegistryHive.CurrentUser)
                {
                    return Registry.CurrentUser;
                }
                if (rh == RegistryHive.DynData)
                {
                    return Registry.DynData;
                }
                if (rh == RegistryHive.LocalMachine)
                {
                    return Registry.LocalMachine;
                }
                if (rh == RegistryHive.PerformanceData)
                {
                    return Registry.PerformanceData;
                }
                if (rh == RegistryHive.Users)
                {
                    return Registry.Users;
                }
                return null;
            }
        }
    }





}
