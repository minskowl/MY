using System.Collections.Generic;
using Savchin.Data.Schema;

namespace Savchin.CodeGeneration
{
    public class SchemaGenerator : GeneratorBase
    {
        /// <summary>
        /// Generates the specified table.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <param name="mode">The mode.</param>
        /// <param name="generations">The generations.</param>
        public void Generate(TableSchema table, GenerateMode mode, List<Generation> generations)
        {
            context.Put("table", table);
            Generate(generations, mode);
        }
        /// <summary>
        /// Gets the name of the destination file.
        /// </summary>
        /// <param name="generation">The generation.</param>
        /// <returns></returns>
        protected override string GetDestinationFileName(Generation generation)
        {
            return string.Format(generation.DestinationFile,
                                 ((TableSchema)context.Get("table")).Alias);
  
        }
    }
}
