using System;
using System.Collections.Generic;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;

namespace Savchin.IO
{
    /// <summary>
    /// IOHelper
    /// </summary>
    public static class IOHelper
    {
        private static readonly Type domain = typeof(NTAccount);

        #region Directory Entries Permissions

        #region Remove Rights


        /// <summary>
        /// Removes the current identity file rights.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="rights">The rights.</param>
        public static void RemoveCurrentIdentityFileRights(FileInfo file, FileSystemRights rights)
        {
            RemoveFileRights(file, WindowsIdentity.GetCurrent(), rights);
        }

        /// <summary>
        /// Sets the file rights.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="identityReference">The identity reference.</param>
        /// <param name="rights">The rights.</param>
        public static void RemoveFileRights(FileInfo file, WindowsIdentity identityReference, FileSystemRights rights)
        {
            RemoveRights(file, identityReference, rights);
        }

        private static void RemoveRights(FileInfo file, WindowsIdentity identity,  FileSystemRights rights)
        {
            var security = new FileSecurity();
            foreach (var reference in GetAllReference(identity))
            {
                var rule = new FileSystemAccessRule(reference, rights, AccessControlType.Deny);
                security.SetAccessRule(rule);
            }

            file.SetAccessControl(security);
        }

        #endregion

        #region Set Rights

        /// <summary>
        /// Sets the current identity file rights.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="rights">The rights.</param>
        public static void SetCurrentIdentityFileRights(FileInfo file, FileSystemRights rights)
        {
            SetFileRights(file, WindowsIdentity.GetCurrent().User, rights);
        }

        /// <summary>
        /// Sets the file rights.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="identityReference">The identity reference.</param>
        /// <param name="rights">The rights.</param>
        public static void SetFileRights(FileInfo file, IdentityReference identityReference, FileSystemRights rights)
        {
            SetAccessToFile(file, identityReference, rights, AccessControlType.Allow);
        }

        private static void SetAccessToFile(FileInfo file, IdentityReference identityReference, FileSystemRights rights, AccessControlType controlType)
        {
            var rule = new FileSystemAccessRule(identityReference, rights, controlType);
            var security = new FileSecurity();
            security.SetAccessRule(rule);
            file.SetAccessControl(security);
        }

        #endregion

        #region Check Rights
        /// <summary>
        /// Identities the has accces to directory.
        /// </summary>
        /// <param name="directoryPath">The directory path.</param>
        /// <param name="fileSystemRights">The file system rights.</param>
        /// <returns></returns>
        public static bool HasCurrentIdentityAcccesToDirectory(string directoryPath, FileSystemRights fileSystemRights)
        {
            return HasIdentityAccces(
                WindowsIdentity.GetCurrent(),
                Directory.GetAccessControl(directoryPath),
                fileSystemRights);
        }

        /// <summary>
        /// Identities the has accces.
        /// </summary>
        /// <param name="identity">The identity.</param>
        /// <param name="directoryPath">The directory path.</param>
        /// <param name="fileSystemRights">The file system rights.</param>
        /// <returns></returns>
        public static bool HasIdentityAcccesToDirectory(WindowsIdentity identity, string directoryPath, FileSystemRights fileSystemRights)
        {
            return HasIdentityAccces(identity, Directory.GetAccessControl(directoryPath), fileSystemRights);
        }

        #region File
        /// <summary>
        /// Determines whether [has current identity accces to file] [the specified file path].
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="fileSystemRights">The file system rights.</param>
        /// <returns>
        /// 	<c>true</c> if [has current identity accces to file] [the specified file path]; otherwise, <c>false</c>.
        /// </returns>
        public static bool HasCurrentIdentityAcccesToFile(string filePath, FileSystemRights fileSystemRights)
        {
            return HasIdentityAccces(
                WindowsIdentity.GetCurrent(),
                new FileInfo(filePath).GetAccessControl(),
                fileSystemRights);
        }

        /// <summary>
        /// Determines whether [has current identity accces to file] [the specified info].
        /// </summary>
        /// <param name="info">The info.</param>
        /// <param name="fileSystemRights">The file system rights.</param>
        /// <returns>
        /// 	<c>true</c> if [has current identity accces to file] [the specified info]; otherwise, <c>false</c>.
        /// </returns>
        public static bool HasCurrentIdentityAcccesToFile(FileInfo info, FileSystemRights fileSystemRights)
        {
            return HasIdentityAccces(
                WindowsIdentity.GetCurrent(),
                info.GetAccessControl(),
                fileSystemRights);
        }

        /// <summary>
        /// Identities the has accces.
        /// </summary>
        /// <param name="identity">The identity.</param>
        /// <param name="filePath">The file path.</param>
        /// <param name="fileSystemRights">The file system rights.</param>
        /// <returns></returns>
        public static bool HasIdentityAcccesToFile(WindowsIdentity identity, string filePath, FileSystemRights fileSystemRights)
        {
            return HasIdentityAccces(identity, new FileInfo(filePath).GetAccessControl(), fileSystemRights);
        }
        #endregion

        /// <summary>
        /// Identities the has accces.
        /// </summary>
        /// <param name="identity">The identity.</param>
        /// <param name="security">The security.</param>
        /// <param name="fileSystemRights">The file system rights.</param>
        /// <returns>
        /// 	<c>true</c> if [has identity accces] [the specified identity]; otherwise, <c>false</c>.
        /// </returns>
        private static bool HasIdentityAccces(WindowsIdentity identity, CommonObjectSecurity security, FileSystemRights fileSystemRights)
        {

            var allReference = GetAllReference(identity);
            var authorizationRuleCollection = security.GetAccessRules(true, true, domain);
            foreach (FileSystemAccessRule fileSystemAccessRule in authorizationRuleCollection)
            {
                if (AccessControlType.Allow == fileSystemAccessRule.AccessControlType &&
                    fileSystemRights == (fileSystemAccessRule.FileSystemRights & fileSystemRights) &&
                    allReference.Contains(fileSystemAccessRule.IdentityReference)
                    )
                {
                    return true;
                }
            }
            return false;
        }
        private static List<IdentityReference> GetAllReference(WindowsIdentity current)
        {

            var allReference = new List<IdentityReference> { current.User.Translate(domain) };
            foreach (var group in current.Groups)
            {
                allReference.Add(group.Translate(domain));
            }
            return allReference;
        }


        #endregion

        #endregion

    }
}
