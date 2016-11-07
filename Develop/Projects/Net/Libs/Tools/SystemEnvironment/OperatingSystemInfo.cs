using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Savchin.SystemEnvironment
{
    /// <summary>
    /// OperatingSystemInfo
    /// </summary>
    [Serializable]
    public class OperatingSystemInfo : IXmlSerializable
    {
        #region Properies

        // Properties
        /// <summary>
        /// Gets the platform.
        /// </summary>
        /// <value>The platform.</value>
        public PlatformID Platform { get; set; }

        /// <summary>
        /// Gets the service pack.
        /// </summary>
        /// <value>The service pack.</value>
        public string ServicePack { get; set; }

        /// <summary>
        /// Gets the version.
        /// </summary>
        /// <value>The version.</value>
        public string Version { get; set; }

        /// <summary>
        /// Gets the version string.
        /// </summary>
        /// <value>The version string.</value>
        public string VersionString { get; set; }

        /// <summary>
        /// Gets or sets the windows version.
        /// </summary>
        /// <value>The windows version.</value>
        public WindowsVersion WindowsVersion { get; set; }

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="OperatingSystemInfo"/> class.
        /// </summary>
        public OperatingSystemInfo()
        {
        }

        /// <summary>
        /// Gets the windows version.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        public static WindowsVersion GetWindowsVersion(OperatingSystem source)
        {

            switch (source.Platform)
            {
                case PlatformID.Win32Windows:
                    {
                        switch (source.Version.Minor)
                        {
                            case 0:
                                return WindowsVersion.Windows95;
                            case 10:
                                {
                                    if (source.Version.Revision.ToString() == "2222A")
                                    {
                                        return WindowsVersion.Windows98Se;
                                    }
                                    else
                                    {
                                        return WindowsVersion.Windows98;
                                    }
                                }

                            case 90:
                                {
                                    return WindowsVersion.WindowsMe;
                                }
                        }
                        break;
                    }

                case PlatformID.Win32NT:
                    {
                        switch (source.Version.Major)
                        {
                            case 3:
                                return WindowsVersion.WindowsNT3;

                            case 4:
                                return WindowsVersion.WindowsNT4;

                            case 5:
                                {
                                    if (source.Version.Minor == 0)
                                    {
                                        return WindowsVersion.Windows2000;
                                    }
                                    else if (source.Version.Minor == 1)
                                    {
                                        return WindowsVersion.WindowsXp;
                                    }
                                    else if (source.Version.Minor == 2)
                                    {
                                        return WindowsVersion.WindowsServer2003;
                                    }
                                    break;
                                }

                            case 6:
                                {
                                    if (source.Version.Minor == 0)
                                    {
                                        return WindowsVersion.WindowsVista;
                                    }
                                    else if (source.Version.Minor == 1)
                                    {
                                        return WindowsVersion.Windows7;
                                    }
                                    else
                                    {
                                        return WindowsVersion.Windows8;
                                    }
                                }
                        }
                        break;
                    }
                default:
                    return WindowsVersion.NotWindows;
            }
            return WindowsVersion.Undefined;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OperatingSystemInfo"/> class.
        /// </summary>
        /// <param name="source">The source.</param>
        public OperatingSystemInfo(OperatingSystem source)
        {
            Platform = source.Platform;
            ServicePack = source.ServicePack;
            VersionString = source.VersionString;
            Version = source.Version.ToString();
            WindowsVersion = GetWindowsVersion(source);


        }

        #region Implementation of IXmlSerializable

        XmlSchema IXmlSerializable.GetSchema()
        {
            return null;
        }

        void IXmlSerializable.ReadXml(XmlReader reader)
        {
            throw new NotImplementedException();
        }

        void IXmlSerializable.WriteXml(XmlWriter writer)
        {
            writer.WriteAttributeString("Platform", Platform.ToString());
            writer.WriteAttributeString("Name", WindowsVersion.ToString());
            writer.WriteAttributeString("Version", Version);
            writer.WriteAttributeString("SP", ServicePack);
            writer.WriteString(VersionString);
        }

        #endregion
    }
}