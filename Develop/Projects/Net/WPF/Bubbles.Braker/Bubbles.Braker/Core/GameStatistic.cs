using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Linq;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;

namespace Savchin.Bubbles.Core
{
    public class GameStatistic : IDisposable
    {
        private DbConnection connection = SQLiteFactory.Instance.CreateConnection();
        private DataClassesDataContext context;
        private FileStream log;

        public GameStatistic()
        {
            connection.ConnectionString = string.Format("Data Source={0}Statistics.s3db", Settings.AppPath);
            connection.Open();
            context = new DataClassesDataContext(connection);

            log = File.OpenWrite(Path.Combine(Settings.LogPath, "Sql.txt"));

            context.Log = new StreamWriter(log);
        }
        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            context.Dispose();
            connection.Close();
            connection.Dispose();

            log.Flush();
            log.Close();
            log.Dispose();

        }
        public Table<GameScore> Scores
        {
            get
            {
                return context.GameScores;
            }
        }


        /// <summary>
        /// Adds the gameScore.
        /// </summary>
        /// <param name="gameScore">The score.</param>
        public void AddScore(GameScore gameScore)
        {


            //var context = new DataClassesDataContext(cnn);
            //context.GameScores.InsertOnSubmit(gameScore);

            //// Submit the change to the database.
            //try
            //{
            //    context.SubmitChanges();
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e);
            //    // Make some adjustments.
            //    // ...
            //    // Try again.
            //    context.SubmitChanges();
            //}


            DbCommand command = SQLiteFactory.Instance.CreateCommand();
            command.CommandText = string.Format("INSERT INTO [GameScores] ([Shift],[Score],[FieldSize]) VALUES({0},{1},{2})",
               (int)gameScore.Shift, gameScore.Score, gameScore.FieldSize);
            command.Connection = connection;
            command.ExecuteNonQuery();


        }
        /// <summary>
        /// Computes this instance.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<GamesStatisiticsGridRow> Compute()
        {
            List<GamesStatisiticsGridRow> result = new List<GamesStatisiticsGridRow>();

            // Create a SqlCommand to retrieve Suppliers data.
            DbCommand command = SQLiteFactory.Instance.CreateCommand();
            command.CommandText =
                "SELECT Shift, COUNT() AS Games,MAX(Score) AS Hi,Avg(Score) AS Average  FROM [GameScores] GROUP BY Shift";
            command.CommandType = CommandType.Text;
            command.Connection = connection;
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    result.Add(new GamesStatisiticsGridRow
                                   {
                                       Shift = ((ShiftStrategy)reader.GetInt32(0)).ToString(),
                                       Games = reader.GetInt32(1),
                                       Hi = reader.GetInt32(2),
                                       Average = (int)reader.GetDouble(3)
                                   });
                }
            }
            return result;
        }


        public IEnumerable<GamesStatisiticsGridRow> CreateStatistics()
        {

            var res = (from row in Scores
                       group row by row.Shift into g
                       select new GamesStatisiticsGridRow
                       {
                           Shift = g.Key.ToString(),
                           Average = (int)g.Average(row => row.Score),
                           Hi = g.Max(row => row.Score),
                           Games = g.Count()
                       }).ToArray();

            return res;




        }


    }

}
