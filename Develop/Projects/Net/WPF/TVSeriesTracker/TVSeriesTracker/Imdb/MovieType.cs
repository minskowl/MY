using System.ComponentModel;

namespace TVSeriesTracker.Imdb
{
    public enum MovieType
    {
       
        [Description("none")]
        None,
        [Description("M")]
        Movie,
        [Description("TV")]
        TVMovie,
        [Description("TVS")]
        TVSeries,
        [Description("V")]
        Video,
        [Description("VG")]
        VideoGame
    }
}
