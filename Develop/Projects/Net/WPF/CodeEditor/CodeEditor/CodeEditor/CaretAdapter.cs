using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using ICSharpCode.AvalonEdit;
using ICSharpCode.AvalonEdit.Editing;

namespace Savchin.CodeEditor
{
    sealed class OptionsAdapter : ITextEditorOptions
    {
        TextEditorOptions avalonEditOptions;

        public OptionsAdapter(TextEditorOptions avalonEditOptions)
        {
            this.avalonEditOptions = avalonEditOptions;
        }

        public string IndentationString
        {
            get
            {
                return avalonEditOptions.IndentationString;
            }
        }

        public bool AutoInsertBlockEnd
        {
            get
            {
                return true;
            }
        }

        public bool ConvertTabsToSpaces
        {
            get
            {
                return avalonEditOptions.ConvertTabsToSpaces;
            }
        }

        public int IndentationSize
        {
            get
            {
                return avalonEditOptions.IndentationSize;
            }
        }

        public int VerticalRulerColumn
        {
            get
            {
                return 120;
            }
        }

        public bool UnderlineErrors
        {
            get
            {
                return true;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged
        {
            add { avalonEditOptions.PropertyChanged += value; }
            remove { avalonEditOptions.PropertyChanged -= value; }
        }

        public string FontFamily
        {
            get
            {
                return "Consolas";
            }
        }
    }
    sealed class CaretAdapter : ITextEditorCaret
    {
        Caret caret;

        public CaretAdapter(Caret caret)
        {
            Debug.Assert(caret != null);
            this.caret = caret;
        }

        public int Offset
        {
            get { return caret.Offset; }
            set { caret.Offset = value; }
        }

        public int Line
        {
            get { return caret.Line; }
            set { caret.Line = value; }
        }

        public int Column
        {
            get { return caret.Column; }
            set { caret.Column = value; }
        }

        public ICSharpCode.NRefactory.Location Position
        {
            get { return AvalonEditDocumentAdapter.ToLocation(caret.Location); }
            set { caret.Location = AvalonEditDocumentAdapter.ToPosition(value); }
        }

        public event EventHandler PositionChanged
        {
            add { caret.PositionChanged += value; }
            remove { caret.PositionChanged -= value; }
        }
    }
}
