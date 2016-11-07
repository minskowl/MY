using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using NUnit.Framework;

namespace Savchin.Data.Schema.Tests.Core
{
    public static class Helper
    {
        private static readonly string resourcesPath;
        public  static readonly string SchemaPath;
        /// <summary>
        /// Initializes the <see cref="Helper"/> class.
        /// </summary>
        static Helper()
        {
            resourcesPath = Path.Combine(Environment.CurrentDirectory, "Resources\\");
            SchemaPath = Path.Combine(resourcesPath, "Data.schema");

        }


    }
}
