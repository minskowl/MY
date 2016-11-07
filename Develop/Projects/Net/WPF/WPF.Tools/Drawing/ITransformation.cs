using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Savchin.Wpf.Drawing
{
    interface ITransformation
    {
        void Transform();

        Matrix Matrix { get; set; }
    }
}
