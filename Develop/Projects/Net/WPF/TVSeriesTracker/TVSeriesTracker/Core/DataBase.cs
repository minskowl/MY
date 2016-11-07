using System.Linq;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Linq;

namespace TVSeriesTracker.Core
{
    internal interface IDataBase
    {
        Movie[] GetMovies();
        void AddMovie(Movie movie);
        void UpdateMovie(Movie movie);
        Movie GetByImdbId(string imdbId);
    }

    class DataBase : IDataBase
    {
        private static ISessionFactory factory;

        static DataBase()
        {
            var configuration = new Configuration();
            configuration.SetProperty(Environment.ConnectionString, string.Format("Data Source={0};Version=3", DbPath));
            ConfigurationHelper.ApplyConfiguration(configuration);
            factory = configuration.BuildSessionFactory();
        }

        private static string DbPath
        {
            get
            {
                var res = Properties.Settings.Default.DbPath;
                return string.IsNullOrWhiteSpace(res) ? "data.db" : res;
            }
        }

        public Movie[] GetMovies()
        {
            using (var session = factory.OpenSession())
            {
                return session.Query<Movie>().ToArray();
            }
        }

        public void AddMovie(Movie movie)
        {
            using (var session = factory.OpenSession())
            {
                session.Save(movie);
            }
        }

        public void UpdateMovie(Movie movie)
        {
            using (var session = factory.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                session.Update(movie);
                transaction.Commit();
            }
        }

        public Movie GetByImdbId(string imdbId)
        {
            using (var session = factory.OpenSession())
            {
                return session.Query<Movie>().FirstOrDefault(e => e.ImdbId == imdbId);
            }
        }
    }
}
