using System;
using System.Collections.Generic;

namespace TVSeriesTracker.Imdb
{
    public class MovieInfo
    {
        public List<String> Actors { get; set; }
        public List<String> Genres { get; set; }
        public List<EpisodeInfo> Episodes { get; set; }
        public string Imdb_id { get; set; }
        public string Imdb_url { get; set; }
        public float Rating { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public string Plot { get; set; }
        public string Plot_simple { get; set; }
        public string Poster { get; set; }
        public int Year { get; set; }

    }
}
