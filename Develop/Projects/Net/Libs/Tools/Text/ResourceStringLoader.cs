using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using Savchin.Validation;

namespace Savchin.Text
{

    public static class ResourceStringLoader
    {
        /// <summary>
        /// Load a resource string.
        /// </summary>
        /// <param name="baseName">The base name of the resource.</param>
        /// <param name="resourceName">The resource name.</param>
        /// <returns>The string from the resource.</returns>
        public static string LoadString(string baseName, string resourceName)
        {
            return LoadString(baseName, resourceName, Assembly.GetCallingAssembly());
        }

        /// <summary>
        /// Load a resource string.
        /// </summary>
        /// <param name="baseName">The base name of the resource.</param>
        /// <param name="resourceName">The resource name.</param>
        /// <param name="asm">The assembly to load the resource from.</param>
        /// <returns>The string from the resource.</returns>
        public static string LoadString(string baseName, string resourceName, Assembly asm)
        {
            if (string.IsNullOrEmpty(baseName)) throw new ArgumentNullException("baseName");
            if (string.IsNullOrEmpty(resourceName)) throw new ArgumentNullException("resourceName");


            string value = null;

            if (null != asm) value = SearchForResource(asm, baseName, resourceName);
            if (null == value) value = LoadAssemblyString(Assembly.GetExecutingAssembly(), baseName, resourceName);
            if (null == value) return string.Empty;
            return value;
        }
        // Remove additional .resource token
        private const string token = ".resources";
        private static string SearchForResource(Assembly asm, string baseName, string resourceName)
        {
            string[] resources = asm.GetManifestResourceNames();

            foreach (string resource in resources)
            {

                string resourceToUse = (string)resource.Clone();
                if (resource.EndsWith(token))
                {
                    resourceToUse = resource.Replace(token, string.Empty);
                }

                string result = LoadAssemblyString(asm, resourceToUse, resourceName);

                if (!string.IsNullOrEmpty(result))
                {
                    return result;
                }
            }

            return null;
        }
        private static Dictionary<string, ResourceManager> _cache = new Dictionary<string, ResourceManager>();

        private static string LoadAssemblyString(Assembly asm, string baseName, string resourceName)
        {
            try
            {
                return GetManager(asm, baseName).GetString(resourceName);
            }
            catch (MissingManifestResourceException)
            {
            }
            return null;
        }

        private static ResourceManager GetManager(Assembly asm, string baseName)
        {
            var key = asm.FullName + "|" + baseName;
            if (_cache.ContainsKey(key)) return _cache[key];
            var result = new ResourceManager(baseName, asm);
            _cache.Add(key, result);
            return result;
        }
    }
}
