using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TVSeriesTracker.Imdb
{
    public class EpisodeInfo
    {
        public string Date { get; set; }
        public string Season { get; set; }
        public string Episode { get; set; }
        public string Title { get; set; }

        public override string ToString()
        {
            return string.Format("{0} {1} {2}", Episode, Title,Date);
        }
    }
}
