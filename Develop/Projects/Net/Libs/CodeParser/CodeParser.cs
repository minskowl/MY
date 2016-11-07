using System;
using System.IO;
using PGMRX120Lib;

namespace Savchin.CodeParsing
{
    public class CodeParser
    {
        public enum ProgramLanguage
        {
            TSQL,
            CSharp1,
            VBNet1
        }

        private readonly PgmrClass _p = new PgmrClass();

        /// <summary>
        /// Initializes a new instance of the <see cref="CodeParser"/> class.
        /// </summary>
        /// <param name="language">The language.</param>
        public CodeParser(ProgramLanguage language)
        {
            string fileName = Path.GetTempFileName();
            switch (language)
            {
                case ProgramLanguage.TSQL:
                    File.WriteAllBytes(fileName, Resource.TSQL);
                    break;
                case ProgramLanguage.CSharp1:
                    File.WriteAllBytes(fileName, Resource.CS);
                    break;
                case ProgramLanguage.VBNet1:
                    File.WriteAllBytes(fileName, Resource.VBNET);
                    break;
                default:
                    throw new ArgumentOutOfRangeException("language");
            }


            _p.SetGrammar(fileName);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CodeParser"/> class.
        /// </summary>
        /// <param name="grammarFile">The grammar file.</param>
        public CodeParser(string grammarFile)
        {
            _p.SetGrammar(grammarFile);
        }

        /// <summary>
        /// Parses the file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        public bool ParseFile(string fileName)
        {
            _p.SetInputFilename(fileName);
            if (_p.Parse() == PGStatus.pgStatusComplete) return true;

            return false;
        }

        /// <summary>
        /// Parses the string.
        /// </summary>
        /// <param name="Text">The text.</param>
        /// <returns></returns>
        public bool ParseString(string Text)
        {
            _p.SetInputString(Text);
            if (_p.Parse() == PGStatus.pgStatusComplete) return true;

            return false;
        }

        /// <summary>
        /// Gets the root.
        /// </summary>
        /// <returns></returns>
        public CodeObject GetRoot()
        {
            return GetCodeObjectByID(_p.GetRoot());
        }

        /// <summary>
        /// Gets the code object by ID.
        /// </summary>
        /// <param name="ID">The ID.</param>
        /// <returns></returns>
        public CodeObject GetCodeObjectByID(int ID)
        {
            return CodeObject.Create(_p, ID);
        }

        /// <summary>
        /// Finds the specified search pattern.
        /// </summary>
        /// <param name="SearchPattern">The search pattern.</param>
        /// <param name="StartObject">The start object.</param>
        /// <returns></returns>
        public CodeObject Find(string SearchPattern, CodeObject StartObject)
        {

            return GetCodeObjectByID(_p.Find(SearchPattern, StartObject.ID));
        }
    }
}
