using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Savchin.Data.Schema;
using Savchin.Data.Schema.Controls;

namespace Savchin.CodeGeneration
{
    public class SchemaGenerationManager : GenerationManagerBase<SchemaGenerator, SchemaBrowser>
    {


        /// <summary>
        /// Generates the specified mode.
        /// </summary>
        /// <param name="mode">The mode.</param>
        public override void Generate(GenerateMode mode)
        {
            if (Browser == null)
                return;
            ClearOutput();

            List<Generation> generations = ProjectBrowser.SelectedGenerations;
            List<TableSchema> tablesToGenerate = new List<TableSchema>();
            foreach (DatabaseSchema database in Browser.Schemas)
            {
                foreach (TableSchema table in database.Tables)
                {
                    foreach (ColumnSchema column in table.Columns)
                    {
                        column.IsSelected = false;
                    }
                }
            }


            foreach (TreeNode node in Browser.CheckedNodes)
            {
                if (node.ImageIndex == (int)SchemaBrowser.ObjImage.Table)
                {
                    tablesToGenerate.Add((TableSchema)node.Tag);
                }
                else if (node.ImageIndex == (int)SchemaBrowser.ObjImage.Column)
                {
                    ((ColumnSchema)node.Tag).IsSelected = true;
                }
            }
      
            foreach (TableSchema table in tablesToGenerate)
            {
                Generator.Generate(table, mode, generations);
            }
            base.Generate(mode);
        }



    }
}
