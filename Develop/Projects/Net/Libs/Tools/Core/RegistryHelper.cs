using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;

namespace Savchin.Core
{
    public static class RegistryHelper
    {
        #region Names Manipulation
        /// <summary>
        /// Gets the new name of the value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public static string GetNewValueName(RegistryKey key)
        {
            List<string> names = new List<string>(key.GetValueNames());
            string newName = "New Value # ";
            int num = 1;
            while (names.Contains(newName + num))
            {
                num++;
            }
            return newName + num;
        }
        /// <summary>
        /// Gets the new name of the key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public static string GetNewKeyName(RegistryKey key)
        {
            List<string> names = new List<string>(key.GetSubKeyNames());
            string newName = "New Key # ";
            int num = 1;
            while (names.Contains(newName + num))
            {
                num++;
            }
            return newName + num;
        }
        /// <summary>
        /// Gets the short name.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public static string GetShortName(RegistryKey key)
        {
            return GetShortName(key.Name);
        }
        /// <summary>
        /// Gets the short name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public static string GetShortName(string name)
        {
            return name.Substring(name.IndexOf("\\") + 1);
        }
        
        public static string GetLastKeyName(string name)
        {
     				int position = name.IndexOf('\\');
            return (position == -1)? name : name.Substring(position);
        }
        #endregion

        /// <summary>
        /// Opens the with create key.
        /// </summary>
        /// <param name="keyName">Name of the key.</param>
        /// <returns></returns>
        public static RegistryKey OpenWithCreateKey(string keyName)
        {
            RegistryKey rootKey = GetRootKey(keyName);
            RegistryKey result = rootKey.OpenSubKey(GetShortName(keyName));
            if (result == null)
                result = rootKey.CreateSubKey(GetShortName(keyName));

            return result;
        }
        /// <summary>
        /// Opens the key.
        /// </summary>
        /// <param name="keyName">Name of the key.</param>
        /// <returns></returns>
        public static RegistryKey OpenKey(string keyName)
        {
            return GetRootKey(keyName).OpenSubKey(GetShortName(keyName));
        }

        /// <summary>
        /// Opens the key.
        /// </summary>
        /// <param name="keyName">Name of the key.</param>
        /// <param name="writable">if set to <c>true</c> [writable].</param>
        /// <returns></returns>
        public static RegistryKey OpenKey(string keyName, bool writable)
        {
            return GetRootKey(keyName).OpenSubKey(GetShortName(keyName), writable);
        }

        /// <summary>
        /// Opens the key.
        /// </summary>
        /// <param name="keyName">Name of the key.</param>
        /// <param name="permissionCheck">The permission check.</param>
        /// <returns></returns>
        public static RegistryKey OpenKey(string keyName, RegistryKeyPermissionCheck permissionCheck)
        {
            return GetRootKey(keyName).OpenSubKey(GetShortName(keyName), permissionCheck);
        }

        /// <summary>
        /// Deletes the key.
        /// </summary>
        /// <param name="key">The key.</param>
        public static void DeleteKey(RegistryKey key)
        {
            DeleteKey(key.Name);
        }

        /// <summary>
        /// Deletes the key.
        /// </summary>
        /// <param name="keyName">Name of the key.</param>
        public static void DeleteKey(string keyName)
        {
            GetRootKey(keyName).DeleteSubKeyTree(GetShortName(keyName));
        }

        /// <summary>
        /// Creates the key.
        /// </summary>
        /// <param name="keyName">Name of the key.</param>
        public static RegistryKey CreateKey(string keyName)
        {
            return GetRootKey(keyName).CreateSubKey(GetShortName(keyName));
        }
        /// <summary>
        /// Gets the root key.
        /// </summary>
        /// <param name="keyName">Name of the key.</param>
        /// <returns></returns>
        public static RegistryKey GetRootKey(string keyName)
        {
            switch (keyName.Substring(0, keyName.IndexOf("\\")))
            {
                case "HKEY_LOCAL_MACHINE":
                    return Registry.LocalMachine;
                case "HKEY_CLASSES_ROOT":
                    return Registry.ClassesRoot;
                case "HKEY_CURRENT_USER":
                    return Registry.CurrentUser;
                case "HKEY_CURRENT_CONFIG":
                    return Registry.CurrentConfig;
                case "HKEY_USERS":
                    return Registry.Users;
                default:
                    return Registry.LocalMachine;
            }
        }
        /// <summary>
        /// Gets the root key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public static RegistryKey GetRootKey(RegistryKey key)
        {
            return GetRootKey(key.Name);
        }

        /// <summary>
        /// Renames the key.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="destinationKeyName">Name of the destination key.</param>
        /// <returns></returns>
        public static RegistryKey RenameKey(RegistryKey source, string destinationKeyName)
        {
            RegistryKey result = CopyKey(source, destinationKeyName);
            DeleteKey(source);
            return result;
        }

        /// <summary>
        /// Copies the specified source key name.
        /// </summary>
        /// <param name="sourceKeyName">Name of the source key.</param>
        /// <param name="destinationKeyName">Name of the destination key.</param>
        public static RegistryKey RenameKey(string sourceKeyName, string destinationKeyName)
        {
            RegistryKey result = CopyKey(sourceKeyName, destinationKeyName);
            DeleteKey(sourceKeyName);
            return result;
        }

        /// <summary>
        /// Copies the specified source key name.
        /// </summary>
        /// <param name="sourceKeyName">Name of the source key.</param>
        /// <param name="destinationKeyName">Name of the destination key.</param>
        public static RegistryKey CopyKey(string sourceKeyName, string destinationKeyName)
        {
            RegistryKey source = OpenKey(sourceKeyName);
            if (source == null) throw new ArgumentException("Invalid source key: " + sourceKeyName);

            return CopyKey(source, destinationKeyName);
        }

        /// <summary>
        /// Copies the key.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="destinationKeyName">Name of the destination key.</param>
        /// <returns></returns>
        public static RegistryKey CopyKey(RegistryKey source, string destinationKeyName)
        {
            RegistryKey destination = OpenKey(destinationKeyName);
            if (destination == null)
            {
                destination = CreateKey(destinationKeyName);
            }

            CopyKey(source, destination);
            return destination;
        }

        /// <summary>
        /// Copies the specified source.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        public static void CopyKey(RegistryKey source, RegistryKey destination)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (destination == null) throw new ArgumentNullException("destination");


            foreach (string valueName in source.GetValueNames())
            {
                destination.SetValue(valueName,
                                     source.GetValue(valueName),
                                     source.GetValueKind(valueName));
            }

            foreach (string subKeyName in source.GetSubKeyNames())
            {
                RegistryKey sub = destination.OpenSubKey(subKeyName, true);
                if (sub == null)
                    sub = destination.CreateSubKey(subKeyName);
                CopyKey(source.OpenSubKey(subKeyName), sub);
            }
        }
        /// <summary>
        /// Deletes the value.
        /// </summary>
        /// <param name="keyName">Name of the key.</param>
        /// <param name="valueName">Name of the value.</param>
        public static void DeleteValue(string  keyName, string valueName)
        {
            RegistryKey key= GetRootKey(keyName).OpenSubKey(GetShortName(keyName), true);
            key.DeleteValue(valueName);
            key.Flush();
        }
        /// <summary>
        /// Renames the value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="sourceValueName">Name of the source value.</param>
        /// <param name="destinationValueName">Name of the destination value.</param>
        public static void RenameValue(RegistryKey key, string sourceValueName, string destinationValueName)
        {
            CopyValue(key, sourceValueName, destinationValueName);
            DeleteValue(key.Name, sourceValueName);
        }

        /// <summary>
        /// Copies the value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="sourceValueName">Name of the source value.</param>
        /// <param name="destinationValueName">Name of the destination value.</param>
        public static void CopyValue(RegistryKey key, string sourceValueName, string destinationValueName)
        {
            if (key == null) throw new ArgumentNullException("key");
            if (string.IsNullOrEmpty(sourceValueName)) throw new ArgumentNullException("sourceValueName");
            if (string.IsNullOrEmpty(destinationValueName)) throw new ArgumentNullException("destinationValueName");
            Registry.SetValue(key.Name, 
                              destinationValueName, 
                              key.GetValue(sourceValueName), 
                              key.GetValueKind(sourceValueName));
        }

        #region Export

        /// <summary>
        /// Exports the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public static string Export(RegistryKey key)
        {

            StringBuilder builder = new StringBuilder("Windows Registry Editor Version 5.00");
            builder.AppendLine();
            ExportKey(key, builder);
            return builder.ToString();
        }
        private static void ExportKey(RegistryKey key, StringBuilder builder)
        {
            builder.AppendLine();
            builder.AppendLine("[" + key.Name + "]");
            foreach (string valueName in key.GetValueNames())
            {
                ExportValue(key, valueName, builder);
            }
            foreach (string keyName in key.GetSubKeyNames())
            {
                ExportKey(key.OpenSubKey(keyName), builder);
            }
        }
        private static void ExportValue(RegistryKey key, string valueName, StringBuilder builder)
        {
            //"ShowExceptions"=dword:00000001
            string value;
            RegistryValueKind kind = key.GetValueKind(valueName);
            switch (kind)
            {
                case RegistryValueKind.String:
                    value = "\"" + key.GetValue(valueName) + "\"";
                    break;

                case RegistryValueKind.DWord:
                    value = "dword:" + ((int)key.GetValue(valueName)).ToString("x").PadLeft(8, '0');
                    break;
                //case RegistryValueKind.ExpandString:
                //    break;
                //case RegistryValueKind.Binary:
                //    break;
                //case RegistryValueKind.MultiString:
                //    break;
                //case RegistryValueKind.QWord:
                //    break;
                //case RegistryValueKind.Unknown:
                //    break;
                default:
                    throw new NotImplementedException("ExportValue  NotImplemented :" + kind);
            }
            builder.AppendLine("\"" + valueName + "\"=" + value);
        }
        #endregion
    }
}
