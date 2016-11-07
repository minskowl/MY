using System.Collections.Generic;
using System.Net;

namespace TVSeriesTracker.Imdb
{
    class ImdbResult
    {
        public IEnumerable<MovieInfo> Items { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string ReasonPhrase { get; set; }
        public bool IsSuccess { get; set; }
    }
}
