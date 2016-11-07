using PdPDM;
using Savchin.Controls.Browsers;
using BaseObject=PdCommon.BaseObject;

namespace Savchin.CodeGeneration
{
    public class PDGenerationManager : GenerationManagerBase<PDGenerator, PDBrowser>
    {


        #region Methods

        /// <summary>
        /// Generates the specified mode.
        /// </summary>
        /// <param name="mode">The mode.</param>
        public override void Generate(GenerateMode mode)
        {
            ClearOutput();
            Generator.Generate(GetSelectedTable(), GenerateMode.OutPutDir, ProjectBrowser.SelectedGenerations);

            base.Generate(mode);
        }



        #endregion

        private BaseTable GetSelectedTable()
        {
            BaseObject baseObject = Browser.SelectedBaseObject;
            if (baseObject == null)
                return null;

            if (baseObject.ClassName != "Table" && baseObject.ClassName != "View")
            {
                throw new CodeGenerationException("Incorrect selected object in PD Browser");
            }

            return (BaseTable)baseObject;

        }



    }
}
