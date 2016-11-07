using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TVSeriesTracker.Imdb
{
    public class ImdbRequest
    {
        public string Text { get; set; }
        public int? Limit { get; set; }
        public int? Year { get; set; }
        public MovieType Type { get; set; }
    }
}
