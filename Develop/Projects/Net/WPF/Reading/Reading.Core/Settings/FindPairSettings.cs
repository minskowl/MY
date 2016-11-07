using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reading.Core.Settings
{
    public class FindPairSettings
    {
        public int Rows { get; set; }
        public int Columns { get; set; }
        public int Images { get; set; }

        public FindPairSettings()
        {
            Rows = 4;
            Columns = 4;
            Images = 8;
        }
    }
}
