using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;

namespace Savchin.Xml
{
    /// <summary>
    /// AssemblyGenerator class build serializators assembly for perfomance XmlSerializer
    /// </summary>
    public class AssemblyGenerator
    {
        #region Properties
        /// <summary>
        /// Gets or sets the refences.
        /// </summary>
        /// <value>The refences.</value>
        public IEnumerable<string> References { get; set; }
        /// <summary>
        /// Gets or sets the key file.
        /// </summary>
        /// <value>The key file.</value>
        public string KeyFile { get; set; }

        /// <summary>
        /// Gets or sets the serializable types.
        /// </summary>
        /// <value>The serializable types.</value>
        public Type[] SerializableTypes { get; set; }

        #endregion

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <param name="assemblyFullPath">The assembly full path.</param>
        public void Build(string assemblyFullPath)
        {
            if (assemblyFullPath == null)
                throw new ArgumentNullException("assemblyFullPath");
            if (!File.Exists(assemblyFullPath))
                throw new FileNotFoundException(string.Format(" Assembly '{0}' not found.", assemblyFullPath));

            var assembly = LoadAssembly(assemblyFullPath);

            if (SerializableTypes == null || SerializableTypes.Length == 0)
                SerializableTypes = GetAssemblySerializableTypes(assembly);

            var importer = new XmlReflectionImporter();
            var mappings = SerializableTypes
                .Select(type => importer.ImportTypeMapping(type)).ToArray();

            var compilerParameters = new CompilerParameters
                                         {
                                             TempFiles = new TempFileCollection(),
                                             IncludeDebugInformation = false,
                                             GenerateInMemory = false,
                                             OutputAssembly = Path.ChangeExtension(assemblyFullPath, ".XmlSerializers.dll")
                                         };

            if (!string.IsNullOrEmpty(KeyFile))
                compilerParameters.CompilerOptions += " /keyfile:\"" + KeyFile + "\"";

            XmlSerializer.GenerateSerializer(SerializableTypes, mappings, compilerParameters);
        }

        private Type[] GetAssemblySerializableTypes(Assembly assembly)
        {
            return assembly.GetExportedTypes()
                           .Where(type => type.IsDefined(typeof(XmlRootAttribute), false)).ToArray();
        }

        private Assembly LoadAssembly(string assemblyFullPath)
        {
            var absoluteAssemblyDir = Path.GetDirectoryName(assemblyFullPath);
            if (References != null)
            {
                AppDomain.CurrentDomain.AssemblyResolve += (sender, e) =>
                          {
                              // FIXME: Imprecise
                              var requestedAssemblyName = new AssemblyName(e.Name).Name;
                              if (!requestedAssemblyName.Contains("System"))
                              {
                                  var requestedAssemblyDll = requestedAssemblyName + ".dll";

                                  foreach (var reference in References.Where(reference => reference.ToLowerInvariant().Contains(requestedAssemblyDll.ToLowerInvariant())))
                                  {
                                      return Assembly.LoadFrom(reference);
                                  }

                                  var candidateAssemblyPath = Path.Combine(absoluteAssemblyDir, requestedAssemblyDll);
                                  if (File.Exists(candidateAssemblyPath))
                                      return Assembly.LoadFrom(candidateAssemblyPath);
                              }

                              return null;
                          };
            }


            return Assembly.LoadFrom(assemblyFullPath);
        }
    }
}
