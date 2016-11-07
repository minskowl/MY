using System;
using PGMRX120Lib;

namespace PDSchema
{

    public class CodeObject 
    {

        private PgmrClass _p;

        private String _text;

        String _type;

        int _id;

        public CodeObject(PgmrClass parser, int ID)
        {

            _p = parser;
            _id = ID;
            _type = _p.GetLabel(ID);
            _text = _p.GetValue(ID);
        }

        public int ID
        {
            get { return _id; }
        }

        public string Text
        {
            get { return _text; }
        }

        public String Type
        {
            get { return _type; }
        }

        public int ChildCount
        {
            get { return _p.GetNumChildren(_id); }
        }

        public static CodeObject Create(PgmrClass parser, int ID)
        {
            if (parser == null || ID == 0) return null;
            return new CodeObject(parser, ID);

        }

        public virtual CodeObject FirstChild()
        {
            return Create(_p, _p.GetChild(_id, 0));
        }

        public virtual CodeObject LastChild()
        {
            return Create(_p, _p.GetChild(_id, _p.GetNumChildren(_id)));
        }

        public virtual CodeObject NextSibling()
        {
            return Create(_p, _p.GetNextSibling(_id));
        }

        public virtual CodeObject PreviousSibling()
        {
            return Create(_p, _p.GetPrevSibling(_id));
        }
    }

    public class CodeParser 
    {
        private PgmrClass _p;
        public CodeParser(string grammarFile)
        {
            _p = new PgmrClass();
            _p.SetGrammar(grammarFile);
        }
        
        public bool ParseFile(string fileName)
        {
            _p.SetInputFilename(fileName);
            if (_p.Parse() == PGStatus.pgStatusComplete) return true;

            return false;
        }

        public bool ParseString(string Text)
        {
            _p.SetInputString(Text);
            if (_p.Parse() == PGStatus.pgStatusComplete) return true;

            return false;
        }

        public CodeObject GetRoot()
        {
            return GetCodeObjectByID(_p.GetRoot());
        }

        public CodeObject GetCodeObjectByID(int ID)
        {
            return CodeObject.Create(_p, ID);
        }

        public CodeObject Find(string SearchPattern, CodeObject StartObject)
        {

            return GetCodeObjectByID(_p.Find(SearchPattern, StartObject.ID));
        }
    }


}
