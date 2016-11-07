using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace Savchin.Core
{
    /// <summary>
    /// 
    /// </summary>
    public static class AppInfo
    {

        /// <summary>
        /// Gets the version.
        /// </summary>
        /// <value>The version.</value>
        public static Version Version { get; private set; }


        /// <summary>
        /// Gets or sets the assembly company.
        /// </summary>
        /// <value>The assembly company.</value>
        public static string Company { get; private set; }
        /// <summary>
        /// Gets or sets the product.
        /// </summary>
        /// <value>The product.</value>
        public static string Product { get; private set; }
        /// Gets or sets the copyright.
        /// </summary>
        /// <value>The copyright.</value>
        public static string Copyright { get; private set; }
        /// <summary>
        /// Gets or sets the product name + version.
        /// </summary>
        /// <value>The product label.</value>
        public static string ProductLabel { get; private set; }
        /// <summary>
        /// Gets the application path.
        /// </summary>
        /// <value>The application path.</value>
        public static string ApplicationPath { get; private set; }
        public static string ApplicationExePath { get; private set; }

        /// <summary>
        /// Gets or sets the application data path.
        /// </summary>
        /// <value>The application data path.</value>
        public static string ApplicationDataPath { get; private set; }
        /// <summary>
        /// Gets or sets the application user data path.
        /// </summary>
        /// <value>The application user data path.</value>
        public static string ApplicationUserDataPath { get; private set; }


        /// <summary>
        /// Initializes the <see cref="AppInfo"/> class.
        /// </summary>
        static AppInfo()
        {
            var assembly = Assembly.GetEntryAssembly() ?? Assembly.GetExecutingAssembly();
            Initilaize(assembly);
        }

        /// <summary>
        /// Initilaizes the specified assembly.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        public static void Initilaize(Assembly assembly)
        {
            Version = assembly.GetName().Version;

            var companyAttr = assembly.GetAttribute<AssemblyCompanyAttribute>(false);
            Company = companyAttr == null ? string.Empty : companyAttr.Company;

            var copyrightAttr = assembly.GetAttribute<AssemblyCopyrightAttribute>(false);
            Copyright = copyrightAttr == null ? string.Empty : copyrightAttr.Copyright;

            var productAttr = assembly.GetAttribute<AssemblyProductAttribute>(false);
            Product = productAttr == null ? string.Empty : productAttr.Product;

            ProductLabel = string.Format("{0} {1}.{2}", Product, Version.Major, Version.Minor);
            InitializeDirectories();
        }

        #region Helpers
        private static void InitializeDirectories()
        {
            ApplicationPath = AppDomain.CurrentDomain.BaseDirectory;
            ApplicationExePath = Process.GetCurrentProcess().MainModule.FileName;

            var productPathSuffuix = string.Format("{0}\\{1}\\{2}.{3}\\", Company, Product, Version.Major, Version.Minor);
            var basePath = Environment.GetEnvironmentVariable("ProgramData");

            ApplicationDataPath = DirectoryIsValid(basePath) ?
                Path.Combine(basePath, productPathSuffuix) : ApplicationPath;

            ApplicationUserDataPath = Path.Combine(GetApplicationUserDataBasePath(), productPathSuffuix);
        }
        private static string GetApplicationUserDataBasePath()
        {
            var result = Environment.GetEnvironmentVariable("LOCALAPPDATA");
            if (DirectoryIsValid(result)) return result;

            result = Environment.GetEnvironmentVariable("APPDATA");
            if (DirectoryIsValid(result)) return result;

            return Path.Combine(ApplicationPath, Environment.UserName);
        }

        /// <summary>
        /// Directories the is valid.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        private static bool DirectoryIsValid(string path)
        {
            return !string.IsNullOrEmpty(path) && Directory.Exists(path);
        } 
        #endregion

    }
}
