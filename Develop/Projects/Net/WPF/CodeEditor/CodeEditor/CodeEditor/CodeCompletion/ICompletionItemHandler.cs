using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Savchin.CodeEditor.CodeCompletion
{
    public interface ICompletionItemHandler
    {
        void Insert(CompletionContext context, ICompletionItem item);
        bool Handles(ICompletionItem item);
    }
}
