using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using NAnt.Core;
using NAnt.Core.Attributes;
using NAnt.Core.Util;

namespace NAnt.Savchin.Tasks.IO
{
    [TaskName("permissionset")]
    public class PermissionSetTask : Task
    {
        private FileInfo _file;
        [TaskAttribute("file")]
        public virtual FileInfo File
        {
            get { return _file; }
            set { _file = value; }
        }

        private FileSystemRights _rights;
        [TaskAttribute("rights")]
        public virtual FileSystemRights Rights
        {
            get { return _rights; }
            set { _rights = value; }
        }

        /// <summary>
        /// Executes the task.
        /// </summary>
        protected override void ExecuteTask()
        {
            if (Verbose) Console.WriteLine("Setup  " + _rights + " for file " + _file.FullName);

            var security = new FileSecurity();
            var identity = WindowsIdentity.GetCurrent();

            var rule = new FileSystemAccessRule(identity.User, _rights, AccessControlType.Allow);
            security.SetAccessRule(rule);
            File.SetAccessControl(security);
            if (Verbose) Console.WriteLine("End permissionset: ");
        }

    }
}
