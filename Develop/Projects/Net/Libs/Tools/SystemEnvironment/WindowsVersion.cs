using System.ComponentModel;

namespace Savchin.SystemEnvironment
{
    /// <summary>
    /// WindowsVersion
    /// </summary>
    public enum WindowsVersion
    {
        /// <summary>
        /// Windows95
        /// </summary>
        [Description("Windows 95")]
        Windows95,
        /// <summary>
        /// Windows98Se
        /// </summary>
        [Description("Windows 98 Second Edition")]
        Windows98Se,
        /// <summary>
        /// Windows98
        /// </summary>
        [Description("Windows 98")]
        Windows98,
        /// <summary>
        /// WindowsMe
        /// </summary>
        [Description("Windows Millennium")]
        WindowsMe,
        /// <summary>
        /// WindowsNT3
        /// </summary>
        [Description("Windows NT 3.51")]
        WindowsNT3,
        /// <summary>
        /// WindowsNT4
        /// </summary>
        [Description("Windows NT 4.0")]
        WindowsNT4,
        /// <summary>
        /// Windows2000
        /// </summary>
        [Description("Windows 2000")]
        Windows2000,
        /// <summary>
        /// WindowsXp
        /// </summary>
        [Description("Windows XP")]
        WindowsXp,
        /// <summary>
        /// WindowsServer2003
        /// </summary>
        [Description("Windows Server 2003")]
        WindowsServer2003,
        /// <summary>
        /// WindowsVista
        /// </summary>
        [Description("Windows Vista")]
        WindowsVista,

        /// <summary>
        /// Windows7
        /// </summary>
        [Description("Windows Seven")]
        Windows7,

        /// <summary>
        /// Windows7
        /// </summary>
        [Description("Win8")]
        Windows8,
        /// <summary>
        /// Undefined
        /// </summary>
        Undefined,
        /// <summary>
        /// NotWindows
        /// </summary>
        NotWindows
    }
}