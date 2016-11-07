using System;
using System.Collections.Generic;
using System.Text;
using PGMRX120Lib;

namespace Savchin.CodeParsing
{
    public class CodeObject
    {

        private readonly PgmrClass _p;
        private readonly String _text;
        readonly String _type;
        readonly int _id;

        /// <summary>
        /// Initializes a new instance of the <see cref="CodeObject"/> class.
        /// </summary>
        /// <param name="parser">The parser.</param>
        /// <param name="ID">The ID.</param>
        public CodeObject(PgmrClass parser, int ID)
        {

            _p = parser;
            _id = ID;
            _type = _p.GetLabel(ID);
            _text = _p.GetValue(ID);
        }

        /// <summary>
        /// Gets the ID.
        /// </summary>
        /// <value>The ID.</value>
        public int ID
        {
            get { return _id; }
        }

        /// <summary>
        /// Gets the text.
        /// </summary>
        /// <value>The text.</value>
        public string Text
        {
            get { return _text; }
        }

        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <value>The type.</value>
        public String Type
        {
            get { return _type; }
        }

        /// <summary>
        /// Gets the child count.
        /// </summary>
        /// <value>The child count.</value>
        public int ChildCount
        {
            get { return _p.GetNumChildren(_id); }
        }

        /// <summary>
        /// Creates the specified parser.
        /// </summary>
        /// <param name="parser">The parser.</param>
        /// <param name="ID">The ID.</param>
        /// <returns></returns>
        public static CodeObject Create(PgmrClass parser, int ID)
        {
            if (parser == null || ID == 0) return null;
            return new CodeObject(parser, ID);

        }

        /// <summary>
        /// Firsts the child.
        /// </summary>
        /// <returns></returns>
        public virtual CodeObject FirstChild()
        {
            return Create(_p, _p.GetChild(_id, 0));
        }

        /// <summary>
        /// Lasts the child.
        /// </summary>
        /// <returns></returns>
        public virtual CodeObject LastChild()
        {
            return Create(_p, _p.GetChild(_id, _p.GetNumChildren(_id)));
        }

        /// <summary>
        /// Nexts the sibling.
        /// </summary>
        /// <returns></returns>
        public virtual CodeObject NextSibling()
        {
            return Create(_p, _p.GetNextSibling(_id));
        }

        /// <summary>
        /// Previouses the sibling.
        /// </summary>
        /// <returns></returns>
        public virtual CodeObject PreviousSibling()
        {
            return Create(_p, _p.GetPrevSibling(_id));
        }
    }
}
