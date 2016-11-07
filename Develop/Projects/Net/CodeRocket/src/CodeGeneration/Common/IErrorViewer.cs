using System;
using System.Collections.Generic;
using System.Text;

namespace Savchin.CodeGeneration.Common
{
    public interface IErrorViewer
    {
        void ShowErrors(IDictionary<Generation, Exception> errors);
    }
}
