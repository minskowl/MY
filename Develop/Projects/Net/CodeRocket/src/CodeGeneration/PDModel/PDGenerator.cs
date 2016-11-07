using System.Collections.Generic;
using Savchin.Data.Schema;
using PdPDM;

namespace Savchin.CodeGeneration
{
    public class PDGenerator : GeneratorBase
    {
        PDSchemaBuilder schemaBuilder = new PDSchemaBuilder();



        /// <summary>
        /// Generates the mode.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <param name="mode">The mode.</param>
        /// <param name="generations">The generations.</param>
        public void Generate(BaseTable table, GenerateMode mode, List<Generation> generations)
        {
            if (table == null)
                table = (BaseTable)PowerDesigner.GetSelectedObject();

            if ((string)table.GetAttribute("Stereotype") != "entity")
                throw new CodeGenerationException("Selected object is not entity");

 
            context.Put("tableschema", schemaBuilder.CreateTableSchema(table));
            context.Put("table", table);


            Generate(generations,mode);
        }








        /// <summary>
        /// Gets the name of the destination file.
        /// </summary>
        /// <param name="generation">The generation.</param>
        /// <returns></returns>
        protected override string GetDestinationFileName(Generation generation)
        {
            return string.Format(generation.DestinationFile,
                                 ((TableSchema)context.Get("tableschema")).Alias);
        }
    }


}



