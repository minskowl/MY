using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WatiN.Core;

namespace FlatSearcher.Core
{
    public static class ElementHelper
    {
        public static void SetCheked(this IElementContainer b, string name, bool chek)
        {
            b.CheckBox(Find.ByName(name)).SetChecked(chek, false);
        }

        public static void SelectOption(this IElementContainer b, string name,string value)
        {
            b.SelectList(Find.ByName(name)).Option(value).SelectNoWait();
        }
    }
}
