using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Microsoft.Win32;
using Savchin.Core;

namespace Savchin.SystemEnvironment
{
    [Serializable]
    public class FrameworksVersionCollection : List<FrameworkVersion>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FrameworksCollection"/> class.
        /// </summary>
        public FrameworksVersionCollection()
        {
            var rootKey = RegistryHelper.OpenKey(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\NET Framework Setup\NDP");
            if (rootKey == null) return;
            var subKeys = rootKey.GetSubKeyNames();
            foreach (var subKey in subKeys)
            {
                var verKey = rootKey.OpenSubKey(subKey);
                if (verKey == null) continue;

                CreateVesion(verKey, subKey);
                CreateVesion(verKey.OpenSubKey("Client"), subKey + " Client");
                CreateVesion(verKey.OpenSubKey("Full"), subKey + " Full");

            }

        }


        private void CreateVesion(RegistryKey key, string name)
        {
            if (key == null) return;
            var tmp = key.GetValue("Install");
            if (tmp == null || !(tmp is int) || (int)tmp != 1) return;

            var versionString = (string)key.GetValue("Version");
            tmp = key.GetValue("SP");

            var servicePack = tmp == null ? 0 : (int)tmp;

            if (string.IsNullOrEmpty(versionString)) versionString = name.Substring(1);

            Add(new FrameworkVersion(name, servicePack, versionString));
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class FrameworkVersion
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        [XmlAttribute]
        public string Name { get; private set; }

        private string _version;
        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        /// <value>The version.</value>
        [XmlAttribute]
        public string Version
        {
            get { return _version; }
            private set
            {
                _version = value;
                if (!string.IsNullOrEmpty(_version)) versionObject = new Version(_version);
            }
        }

        private Version versionObject;
        /// <summary>
        /// Gets the version object.
        /// </summary>
        /// <value>The version object.</value>
        [XmlIgnore]
        public Version VersionObject
        {
            get { return versionObject; }
        }

        /// <summary>
        /// Gets or sets the service pack.
        /// </summary>
        /// <value>The service pack.</value>
        [XmlAttribute]
        public int ServicePack { get; private set; }

        public FrameworkVersion()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FrameworkVersion"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="servicePack">The service pack.</param>
        /// <param name="version">The version.</param>
        internal FrameworkVersion(string name, int servicePack, string version)
        {
            Name = name;
            ServicePack = servicePack;
            Version = version;
        }



        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </returns>
        public override string ToString()
        {
            return string.Format("Framework {0} SP {1} Full version {2}", Name, ServicePack, Version);
        }
    }
}