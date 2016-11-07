using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NPOI.SS.UserModel;

namespace FlatSearcher.Core
{
    internal static class ExcelHelper
    {
        public static ICell CreateCell(this IRow row,int index, string text)
        {
            var res = row.CreateCell(index);
            res.SetCellValue(text);
            return res;
        }
    }
}
