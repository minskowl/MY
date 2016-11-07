using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TVSeriesTracker.Models;

namespace TVSeriesTracker.Core
{
    internal class MoviesEventArgs : EventArgs
    {
        public MoviesEventArgs(MovieModel[] movies)
        {
            Movies = movies;
        }

        public MovieModel[] Movies { get; private set; }
    }
}
