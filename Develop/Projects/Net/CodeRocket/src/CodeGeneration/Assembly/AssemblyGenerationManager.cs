using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using Savchin.Forms.Browsers;

namespace Savchin.CodeGeneration
{
    public class AssemblyGenerationManager : GenerationManagerBase<AssemblyGenerator, AssemblyBrowser>
    {


        #region Interface
        /// <summary>
        /// Generates the specified mode.
        /// </summary>
        /// <param name="mode">The mode.</param>
        public override void Generate(GenerateMode mode)
        {
            if (Browser == null)
                return;

            List<Generation> generations = ProjectBrowser.SelectedGenerations;
            ClearOutput();
            foreach (TreeNode node in Browser.CheckedNodes)
            {
                Generator.Generate(node, mode, generations);
            }

            base.Generate(mode);
        }



        /// <summary>
        /// Called when [project loaded].
        /// </summary>
        protected override void OnProjectLoaded()
        {
            base.OnProjectLoaded();
            if (Browser != null && File.Exists(project.SchemaFile))
                Browser.SelectNodeByFullPath(project.LastView);


            /*
           

                    miAssemblyBookmarks.MenuItems.Clear()
                    BookMarks.Clear()
                    For Each bm As BookMark In assemblyProject.BookMarks
                        AddBookMarkMenu(bm)
                    Next
                         */
        }
        #endregion


    }
}
