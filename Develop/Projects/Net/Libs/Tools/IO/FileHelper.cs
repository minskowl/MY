using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Savchin.IO
{
    public class FileHelper
    {
        public static int GetLinesCount(string path)
        {
            var lineCount = 0;
            using (var reader = File.OpenText(path))
                while (reader.ReadLine() != null)
                    lineCount++;
            return lineCount;
        }
    }
}
