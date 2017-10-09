using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reading.Core.Settings
{
    public class FindLetterSettings
    {
        public SelectionMode Mode { get; set; }

        public LettersTypes LettersTypes { get; set; }

        public int LettersCount { get; set; }
    }
}
