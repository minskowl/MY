using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using BotvaSpider.Automation.Mining;
using BotvaSpider.BookKeeping;
using BotvaSpider.Core;
using BotvaSpider.Farming;
using BotvaSpider.Gears;
using Savchin.Text;
using Savchin.TimeManagment;

namespace BotvaSpider.Data
{
    /// <summary>
    /// 
    /// </summary>
    public class ObjectProvider : IDisposable
    {
        private OleDbConnection connection;

        #region Const

        private const string commandTextSelectFightLog =
            @"SELECT  Fights.Date, Fights.Money, Fights.Cristals, Fights.Expirience, Fights.IsFarm
FROM Fights WHERE Fights.UserID=@UserID ORDER BY Fights.Date;
";

        private const string commandTextUserInsert =
            @"INSERT INTO Users 
( [Name], [Level], [ClanID], [UserTypeID], [Female], [Fights], [Victories], [Stealing],
[Lose], [Injury],[Updated],[MilkingCoulombID],[Safe] ) VALUES
(?,?,?,?,?,?,?,?,?,?,?,?,?)";

        private const string commandTextUserSelect =
            @"SELECT  [Users].[UserID],[Users].[Name], [Users].[Level], [Users].[ClanID], [Users].[UserTypeID], [Users].[Female], 
[Users].[Fights], [Users].[Victories], [Users].[Stealing],[Users].[Lose],[Users].[Injury],[Users].[Updated],
[Users].[MilkingCoulombID],[Users].[Safe]
FROM [Users]";

        private const string commandTextUserUpdate =
            @"UPDATE Users SET  
 [Name]=?, [Level]=?, [ClanID]=?, [UserTypeID]=?, [Female]=?,[Fights]=?, [Victories]=?, [Stealing]=?,
[Lose]=?, [Injury]=?,[Updated]=?, [MilkingCoulombID]=?,[Safe]=? WHERE UserID=?; ";

        private const string commandeTextClanSelect = "SELECT [ClanID],[Name],[Tag] FROM Clans";
        private const string commandTextClanInsert = @"INSERT INTO Clans ( [Name],[Tag] ) VALUES (@Name, @Tag);";

        private const string commandTextSkillPointsInsert =
            @"INSERT INTO SkillPoints 
( [UserID], [SkilID], [Points], [AdditionalPoints] ) VALUES (@UserID, @SkilID, @Points, @AdditionalPoints)";


        private const string commandTextSkillPointsDelete =
            @"DELETE FROM SkillPoints";




        private const string commandTextFightSelect =
            @"SELECT UserID,[Date],Money,Cristals ,Expirience,IsFarm FROM Fights ";
        private const string commandTextCristalOwners =
            @"
SELECT Users.Name
FROM FightsStatistics INNER JOIN Users ON FightsStatistics.UserID = Users.UserID
WHERE ((Users.Level) Between  @LevelFrom AND @LevelTo) And ((FightsStatistics.AvgOfCristals)>0)
ORDER BY FightsStatistics.AvgOfCristals DESC , FightsStatistics.AvgOfMoney DESC;
";

        private const string commandTextCowsSelectStartPart =
            @"SELECT UserID, Name, [Level], AvgOfMoney, MilkingCoulombID, AvgOfCristals, Safe,UserTypeID
FROM UserInfo
";

        private const string commandTextNotCowsSelect = "SELECT  Name FROM Users WHERE UserTypeID IN(3,7);";

        private const string commandTextCowsSelectAll = commandTextCowsSelectStartPart + " ORDER BY [Level],Name ";
        private const string commandTextCowsSelect = commandTextCowsSelectStartPart +
                                                     @" WHERE UserTypeID=1 AND (  [Level] BETWEEN @LevelFrom AND @LevelTo);";

        private const string commandTextCowsSelectByName = commandTextCowsSelectStartPart + @" WHERE Name=? ";

        private const string commandTextCowsPotentialSelect =
            @"SELECT Users.UserID, Users.Name, Users.Level,( [Lose]/([Fights]-[Victories])) AS AvgOfMoney, MilkingCoulombID, 0, Safe,UserTypeID
FROM Users 
INNER JOIN FightsCount ON FightsCount.UserID = Users.UserID
WHERE   ( ( [Users].[Level] BETWEEN @LevelFrom AND @LevelTo) ) AND ([Fights]-[Victories]>0) AND(FightsCount.FightCount<5) ; ";


        private const string commandTextMineSelectBest =
            @"SELECT [Position], COUNT([Attempt]) FROM [MineMap] GROUP BY [Position];";

        private const string commandTextMineMaxOfAttempt = @"SELECT Max(Attempt) AS M FROM MineMap";
        private const string commandTextMineMapInsert =
            @"INSERT INTO MineMap ( [Attempt], [Position]) VALUES (?,?)";
        private const string commandTextMinesSelect =
            @"SELECT [Attempt], [Position] FROM MineMap ORDER BY [Attempt]; ";
        private const string commandTextCrystalMaxOfAttempt = @"SELECT Max(CrystalMap.Attempt) AS MaxOfAttempt
FROM CrystalMap";
        private const string commandTextCrystalMapInsert =
            @"INSERT INTO CrystalMap ( [Attempt], [Position],[IsSmall] ) VALUES (?,?,?)";





        private const string commandTextCrystalGetMaps =
            @"SELECT Attempt,[Position] FROM [CrystalMap] WHERE [IsSmall]=? ORDER BY  Attempt;";

        private const string commandTextWormHoleSelectBest =
            @"SELECT WormHoles.Hole,Count(WormHoles.ID) AS cnt 
FROM WormHoles GROUP BY WormHoles.Hole; ";

        private const string commandTextWormHoleSelect =
            @"SELECT WormHoles.Hole FROM WormHoles; ";

        private const string commandTextWormHoleInsert =
            @"INSERT INTO WormHoles ( [Hole] ) VALUES (?)";
        #endregion

        /// <summary>
        /// 
        /// </summary>
        public static readonly ObjectProvider Instance = new ObjectProvider();

        private ObjectProvider()
        {

        }


        /// <summary>
        /// Connections the is valid.
        /// </summary>
        /// <returns></returns>
        public bool ConnectionIsValid()
        {
            try
            {
                CreateConnection();
                GetUserByID(-1);
                return true;
            }
            catch (Exception ex)
            {
                var message = string.Format("Ошибка подключения к базе коров. \n '{0}'",
                                            AppCore.GameSettings.GetDatabaseFullPath());

                AppCore.LogSystem.Error(message, ex);
                return false;
            }
        }
        private void CreateConnection()
        {
            connection = new OleDbConnection(string.Format(
                                                 @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0}",
                                                 AppCore.GameSettings.GetDatabaseFullPath()));
            connection.Open();
        }

        #region Skill Points managment
        /// <summary>
        /// Adds the skill points.
        /// </summary>
        /// <param name="userID">The user ID.</param>
        /// <param name="skill">The skill.</param>
        private void AddSkillPoints(int userID, Skill skill)
        {
            var command = connection.CreateCommand(commandTextSkillPointsInsert);
            command.AddParameter("@UserID", userID);
            command.AddParameter("@SkilID", (int)skill.SkilType);
            command.AddParameter("@Points", skill.Points);
            command.AddParameter("@AdditionalPoints", skill.AdditionalPoints);
            command.ExecuteNonQuery();
        }
        /// <summary>
        /// Deletes the skill points.
        /// </summary>
        /// <param name="userID">The user ID.</param>
        private void DeleteSkillPoints(int userID)
        {
            var command = connection.CreateCommand(commandTextSkillPointsDelete + " WHERE UserID=@UserID ;");
            command.AddParameter("@UserID", userID);
            command.ExecuteNonQuery();
        }
        /// <summary>
        /// Refreshes the skills.
        /// </summary>
        /// <param name="user">The user.</param>
        public void UserSaveSkills(User user)
        {
            DeleteSkillPoints(user.UserID);
            foreach (var skill in user.Skills)
            {
                AddSkillPoints(user.UserID, skill);
            }
        }

        #endregion

        #region Cows Managment
        /// <summary>
        /// Gets the not cows.
        /// </summary>
        /// <returns></returns>
        public List<string> GetNotCows()
        {
            var command = connection.CreateCommand(commandTextNotCowsSelect);
            return ReadString(command);
        }

        /// <summary>
        /// Gets the cows.
        /// </summary>
        /// <returns></returns>
        public List<Cow> GetCows()
        {
            var command = connection.CreateCommand(commandTextCowsSelectAll);
            return ReadObjects<Cow>(command, CreateCow);
        }

        /// <summary>
        /// Gets the fights.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public List<Cow> GetCows(LevelFilter filter)
        {
            var command = connection.CreateCommand(commandTextCowsSelect);
            command.AddParameter(filter);
            return ReadObjects<Cow>(command, CreateCow);
        }

        /// <summary>
        /// Gets the guild users.
        /// </summary>
        /// <param name="guild">The guild.</param>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public List<string> GetGuildUsers(GuildType guild, LevelFilter filter)
        {
            var command = connection.CreateCommand(@"SELECT Users.Name
FROM Users 
WHERE ((Users.Level) Between  @LevelFrom AND @LevelTo) And Guild=@Guild");
            command.AddParameter(filter);
            command.AddParameter("Guild", (byte)guild);
            return ReadString(command);
        }

        /// <summary>
        /// Gets the cristal owners.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public List<string> GetCristalOwners(LevelFilter filter)
        {
            var command = connection.CreateCommand(commandTextCristalOwners);
            command.AddParameter(filter);
            return ReadString(command);
        }
        /// <summary>
        /// Gets the name of the cows by.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public List<Cow> GetCowsByName(string name)
        {
            var command = (OleDbCommand)connection.CreateCommand(commandTextCowsSelectByName);
            command.Parameters.AddWithValue("Name", name);
            return ReadObjects<Cow>(command, CreateCow);
        }


        /// <summary>
        /// Gets the potential cows.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="avgLimit">The avg limit.</param>
        /// <returns></returns>
        public List<Cow> GetPotentialCows(LevelFilter filter, decimal avgLimit)
        {
            var command = connection.CreateCommand(commandTextCowsPotentialSelect);
            command.AddParameter(filter);
            return ReadObjects<Cow>(command, CreateCow).Where(e => e.AverageBenefit > avgLimit).ToList();
        }


        private Cow CreateCow(IDataRecord reader)
        {
            var avg = reader.GetValue(3);
            var avgCristals = reader.GetValue(5);
            var safeValue = reader.GetValue(6);

            var result = new Cow();

            result.UserID = reader.GetInt32(0);
            result.UserName = reader.GetString(1);
            result.Level = reader.GetByte(2);
            result.AverageBenefit = avg is DBNull ? (decimal)0 : ((decimal)(double)avg);
            result.State = CowState.Ready;
            result.MilkingCoulomb = (Coulomb)reader.GetInt32(4);
            result.AverageCristals = avgCristals is DBNull ? 0 : Convert.ToDecimal(avgCristals);
            result.Safe = (safeValue == null || safeValue is DBNull ? (Safe?)null : (Safe)safeValue);
            result.UserType = (UserType)reader.GetInt32(7);


            return result;
        }

        #endregion

        #region Fight Managment
        public DataTable GetFightLog(int userID)
        {
            var result = new DataTable();
            using (var command = (OleDbCommand)connection.CreateCommand(commandTextSelectFightLog))
            {
                command.AddParameter("@UserID", userID);
                using (var adapter = new OleDbDataAdapter(command))
                {
                    adapter.Fill(result);
                }
                return result;
            }
        }

        /// <summary>
        /// Adds the fight.
        /// </summary>
        /// <param name="result">The result.</param>
        public void AddFight(FightResult result)
        {
            var command = connection.CreateCommand(@"INSERT INTO Fights 
( [UserID], [Money], [Cristals], [Expirience],[Date],[IsFarm],[SkillDifference] ) 
VALUES (@UserID, @Money, @Cristals, @Expirience,@Date,@IsFarm,@SkillDifference)");
            command.AddParameter("@UserID", result.Rival.UserID);
            command.AddParameter("@Money", result.Money);
            command.AddParameter("@Cristals", result.Crystals);
            command.AddParameter("@Expirience", result.Expirience);
            command.AddParameter("@Date", result.Date);
            command.AddParameter("@IsFarm", result.Rival.Source == RivalSource.FromFarm);
            command.AddParameter("@SkillDifference", result.SkillDifference);
            command.ExecuteNonQuery();
        }

        /// <summary>
        /// Gets the fights.
        /// </summary>
        /// <returns></returns>
        public List<FightResult> GetFights()
        {
            var command = connection.CreateCommand(commandTextFightSelect);
            return ReadObjects<FightResult>(command, CreateResult);
        }
        /// <summary>
        /// Gets the fight.
        /// </summary>
        /// <param name="userID">The user ID.</param>
        /// <param name="date">The date.</param>
        /// <returns></returns>
        public FightResult GetFight(int userID, DateTime date)
        {
            var command = connection.CreateCommand(commandTextFightSelect + " WHERE [UserID]=@UserID AND [Date]=@Date ");
            command.AddParameter("@UserID", userID);
            command.AddParameter("@Date", date);

            return ReadObject<FightResult>(command, CreateResult);
        }

        private FightResult CreateResult(IDataRecord reader)
        {//UserID,[Date],Money,Cristals ,Expirience 
            var result = new FightResult
                             {
                                 Date = reader.GetDateTime(1),
                                 Money = reader.GetInt32(2),
                                 Crystals = reader.GetByte(3),
                                 Expirience = reader.GetByte(4),

                             };

            return result;


        }
        #endregion

        #region User managment


        private void CreateUserParameters(OleDbCommand command, User user)
        {
            command.Parameters.AddWithValue("Name", user.Name);
            command.Parameters.AddWithValue("Level", user.Level);
            command.Parameters.AddWithValue("ClanID", user.ClanID == 0 ? DBNull.Value : (object)user.ClanID);
            command.Parameters.AddWithValue("UserTypeID", (int)user.UserType);
            command.Parameters.AddWithValue("Female", user.Female);
            command.Parameters.AddWithValue("Fights", user.Fights);
            command.Parameters.AddWithValue("Victories", user.Victories);
            command.Parameters.AddWithValue("Stealing", user.Stealing);
            command.Parameters.AddWithValue("Lose", user.Lose);
            command.Parameters.AddWithValue("Injury", user.Injury);
            command.Parameters.AddWithValue("Updated", user.Updated.ToString());
            command.Parameters.AddWithValue("MilkingCoulombID", (int)user.MilkingCoulomb);
            command.Parameters.AddWithValue("Safe", user.Safe.HasValue ? (int)user.Safe.Value : (object)DBNull.Value);
        }
        /// <summary>
        /// Adds the users.
        /// </summary>
        /// <param name="users">The users.</param>
        public void AddUsers(IEnumerable<User> users)
        {
            foreach (var user in users)
            {
                AddUser(user);
            }
        }

        /// <summary>
        /// Adds the user.
        /// </summary>
        /// <param name="user">The user.</param>
        public void AddUser(User user)
        {
            user.Updated = DateTime.Now;
            var command = connection.CreateCommand(commandTextUserInsert);
            CreateUserParameters((OleDbCommand)command, user);
            command.ExecuteNonQuery();
            user.UserID = connection.GetInsertedID();
        }

        /// <summary>
        /// Sets the guild.
        /// </summary>
        /// <param name="userID">The user ID.</param>
        /// <param name="type">The type.</param>
        public void SetGuild(int userID, GuildType type)
        {
            var command = connection.CreateCommand("UPDATE Users SET  [Guild]=? WHERE [UserID]=?;");
            command.AddParameter("Guild", (byte)type);
            command.AddParameter("UserID", userID);
            command.ExecuteNonQuery();
        }
        /// <summary>
        /// Deletes the user.
        /// </summary>
        /// <param name="user">The user.</param>
        public void DeleteUser(User user)
        {
            DeleteUser(user.UserID);
        }
        /// <summary>
        /// Deletes the user.
        /// </summary>
        /// <param name="userID">The user ID.</param>
        public void DeleteUser(int userID)
        {
            var command = (OleDbCommand)connection.CreateCommand("DELETE [Users] FROM [Users] WHERE [UserID]=?");
            command.Parameters.AddWithValue("UserID", userID);
            command.ExecuteNonQuery();
        }

        /// <summary>
        /// Updates the user.
        /// </summary>
        /// <param name="user">The user.</param>
        public void UpdateUser(User user)
        {
            user.Updated = DateTime.Now;
            var command = (OleDbCommand)connection.CreateCommand(commandTextUserUpdate);
            CreateUserParameters(command, user);
            command.Parameters.AddWithValue("UserID", user.UserID);
            command.ExecuteNonQuery();
        }

        /// <summary>
        /// Gets the users.
        /// </summary>
        /// <returns></returns>
        public List<User> GetUsers()
        {
            return ReadObjects<User>(connection.CreateCommand(commandTextUserSelect), CreateUser);
        }

        /// <summary>
        /// Gets the name of the users by clan.
        /// </summary>
        /// <param name="clanName">Name of the clan.</param>
        /// <returns></returns>
        public List<User> GetUsersByClanName(string clanName)
        {
            var command =
                (OleDbCommand)connection.CreateCommand(commandTextUserSelect +
                                                       " INNER JOIN Clans ON Clans.ClanID = Users.ClanID WHERE Clans.Name=?;");
            command.Parameters.AddWithValue("Name", clanName);
            return ReadObjects<User>(command, CreateUser);
        }
        /// <summary>
        /// Gets the users for update.
        /// </summary>
        /// <returns></returns>
        public List<User> GetUsersForUpdate()
        {
            var command =
                connection.CreateCommand(commandTextUserSelect +
                                         " WHERE [Updated] IS NULL OR DateDiff('d',[Updated],Now())>5 ;");
            return ReadObjects<User>(command, CreateUser);
        }

        /// <summary>
        /// Gets the name of the user by.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public User GetUserByName(string name)
        {
            var command = connection.CreateCommand(commandTextUserSelect + " WHERE [Name]=@Name");
            command.AddParameter("@Name", name);

            return ReadObject<User>(command, CreateUser);
        }



        /// <summary>
        /// Gets the user by ID.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public User GetUserByID(int id)
        {
            var command = connection.CreateCommand(commandTextUserSelect + " WHERE [UserID]=@UserID");
            command.AddParameter("@UserID", id);
            return ReadObject<User>(command, CreateUser);
        }
        private User CreateUser(IDataRecord reader)
        {
            var safeValue = reader.GetValue(13);
            var result = new User
                             {
                                 UserID = reader.GetInt32(0),
                                 Name = reader.GetString(1),
                                 Level = reader.GetByte(2),

                                 UserType = (UserType)reader.GetInt32(4),
                                 Female = reader.GetBoolean(5),
                                 Fights = reader.GetInt32(6),
                                 Victories = reader.GetInt32(7),

                                 Stealing = reader.GetInt32(8),
                                 Lose = reader.GetInt32(9),
                                 Injury = reader.GetInt32(10),
                                 Updated = reader.IsDBNull(11) ? DateTime.MinValue : reader.GetDateTime(11),
                                 MilkingCoulomb = (Coulomb)reader.GetInt32(12),
                                 Safe = (safeValue == null || safeValue is DBNull ? (Safe?)null : (Safe)safeValue)
                             };
            var tmp = reader.GetValue(3);
            result.ClanID = tmp is DBNull ? 0 : (int)tmp;
            return result;


        }
        #endregion

        #region Clan Managment
        /// <summary>
        /// Gets the clans.
        /// </summary>
        /// <returns></returns>
        public List<Clan> GetClans()
        {
            var command = connection.CreateCommand(commandeTextClanSelect);

            return ReadObjects<Clan>(command, CreateClan);
        }


        /// <summary>
        /// Gets the name of the clan by.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public List<Clan> GetClanByName(string name)
        {
            var command = connection.CreateCommand(commandeTextClanSelect + " WHERE [Name]=@Name");
            command.AddParameter("@Name", name);
            return ReadObjects<Clan>(command, CreateClan);
        }

        /// <summary>
        /// Gets the clan by tag.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <returns></returns>
        public Clan GetClanByTag(string tag)
        {
            var command = connection.CreateCommand(commandeTextClanSelect + " WHERE [Tag]=@Tag");
            command.AddParameter("@Tag", tag);

            return ReadObject<Clan>(command, CreateClan);
        }

        private Clan CreateClan(IDataRecord reader)
        {
            return new Clan
                       {
                           ClanID = reader.GetInt32(0),
                           Name = reader.GetString(1),
                           Tag = reader.GetString(2)
                       };
        }

        /// <summary>
        /// Adds the clan.
        /// </summary>
        /// <param name="clan">The clan.</param>
        public void AddClan(Clan clan)
        {
            var command = connection.CreateCommand(commandTextClanInsert);
            command.AddParameter("@Name", DbType.String, clan.Name);
            command.AddParameter("@Tag", DbType.String, clan.Tag ?? string.Empty);
            command.ExecuteNonQuery();
            clan.ClanID = connection.GetInsertedID();
        }
        #endregion

        #region Worm Holes
        /// <summary>
        /// Saves the worm hole.
        /// </summary>
        /// <param name="hole">The hole.</param>
        public void SaveWormHole(byte hole)
        {
            var commandInsert = (OleDbCommand)connection.CreateCommand(commandTextWormHoleInsert);
            commandInsert.Parameters.Add("Hole", OleDbType.TinyInt).Value = hole;
            commandInsert.ExecuteNonQuery();
        }

        /// <summary>
        /// Gets the top worm hole.
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, int> GetTopWormHole()
        {
            var result = new Dictionary<int, int>();
            var command = (OleDbCommand)connection.CreateCommand(commandTextWormHoleSelectBest);
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    result.Add(reader.GetByte(0), reader.GetInt32(1));
                }
            }
            return result;
        }

        /// <summary>
        /// Gets the worm holes.
        /// </summary>
        /// <returns></returns>
        public List<byte> GetWormHoles()
        {
            var result = new List<byte>();
            var command = (OleDbCommand)connection.CreateCommand(commandTextWormHoleSelect);
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    result.Add(reader.GetByte(0));
                }
            }
            return result;
        }

        #endregion


        /// <summary>
        /// Adds the purchase.
        /// </summary>
        /// <param name="result">The result.</param>
        public void AddBalanceItem(BalanceItem result)
        {
            var command = (OleDbCommand)connection.CreateCommand(
@"INSERT INTO [AccountBalance]
([Profit],[CategoryID],[Gold], [Cristal],[Item],[SmallTicket],[BigTicket])
VALUES (?,?,?,?,?,?,?)");

            command.AddParameter("@Profit", result.IsProfit);
            command.AddParameter("@CategoryID", (byte)result.Category);
            command.AddParameter("@Gold", result.Gold);
            command.AddParameter("@Cristal", (byte)result.Cristal);
            command.AddParameter("@Item", result.Item);
            command.AddParameter("@SmallTicket", result.SmallTicket);
            command.AddParameter("@BigTicket", result.BigTicket);

            command.ExecuteNonQuery();
            result.ID = connection.GetInsertedID();
        }

        /// <summary>
        /// Gets the balance items.
        /// </summary>
        /// <param name="range">The range.</param>
        /// <returns></returns>
        public List<BalanceItem> GetBalanceItems(DateRange range)
        {
            var command = (OleDbCommand)connection.CreateCommand(
          @"SELECT [Date],[Profit],[CategoryID],[Gold], [Cristal],[Item],[SmallTicket],[BigTicket]
FROM  [AccountBalance] WHERE [Date] BETWEEN @From AND @To");
            command.AddParameter("@From", range.From);
            command.AddParameter("@To", range.To);
            return ReadObjects<BalanceItem>(command, CreateBalanceItem);
        }

        /// <summary>
        /// Creates the balance item.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <returns></returns>
        private BalanceItem CreateBalanceItem(IDataRecord reader)
        {
            var result = new BalanceItem();

            result.Date = reader.GetDateTime(0);
            result.IsProfit = reader.GetBoolean(1);

            result.Category = (BalanceCategory)reader.GetByte(2);
            result.Gold = reader.GetInt32(3);
            result.Cristal = reader.GetInt16(4);
            result.Item = reader.GetString(5);
            result.SmallTicket = reader.GetBoolean(6);
            result.BigTicket = reader.GetBoolean(7);

            return result;
        }

        #region Mine Statistics
        /// <summary>
        /// Reads the mine statistics.
        /// </summary>
        /// <returns></returns>
        public List<SearchCristalResult> GetMineStatistics()
        {
            return ReadObjects<SearchCristalResult>(connection.CreateCommand(@"SELECT 
[Cristals],[Couloumb], [Level], [Spirits],[Gears],[SmallTicket],[BigTicket],[Percentage],[DoAttempt],[Date],
[StatisticsID] FROM MineStatistics;            "), CreateMineStatistics);
        }
        private SearchCristalResult CreateMineStatistics(IDataRecord reader)
        {

            var result = new SearchCristalResult
            {
                Cristals = reader.GetByte(0),
                Coulomb = (Coulomb)reader.GetInt32(1),
                Level = reader.GetInt16(2),
                Spirit = (SpiritType)reader.GetInt32(3),
                MinerGear = (MinerGear)reader.GetByte(4),
                SmallTicket = reader.GetBoolean(5),
                BigTicket = reader.GetBoolean(6),
                Percentage = reader.GetByte(7),
                DoAttempt = reader.GetBoolean(8),
                Date = reader.GetDateTime(9),
                StatisticsID = reader.GetInt32(10)
            };

            return result;


        }
        /// <summary>
        /// Adds the mine statistics.
        /// </summary>
        /// <param name="result">The result.</param>
        public void AddMineStatistics(SearchCristalResult result)
        {
            try
            {
                var command = (OleDbCommand)connection.CreateCommand(
    @"INSERT INTO [MineStatistics]
( [Cristals],[Couloumb], [Level], [Spirits],[Gears],[SmallTicket],[BigTicket],[Percentage],[DoAttempt])
VALUES 
(?,?,?,?,?,?,?,?,?)");


                command.AddParameter("@Cristals", result.Cristals);
                command.AddParameter("@Couloumb", (int)result.Coulomb);
                command.AddParameter("@Level", result.Level);
                command.AddParameter("@Spirits", (int)result.Spirit);
                command.AddParameter("@Gears", (byte)result.MinerGear);
                command.AddParameter("@SmallTicket", result.SmallTicket);
                command.AddParameter("@BigTicket", result.BigTicket);
                command.AddParameter("@Percentage", result.Percentage);
                command.AddParameter("@DoAttempt", result.DoAttempt);

                command.ExecuteNonQuery();
                result.StatisticsID = connection.GetInsertedID();
            }
            catch (Exception ex)
            {
                AppCore.LogSystem.Warn("Ошибка статистики.", "Не смогли добавить результаты посещения шахты", ex);
            }
        }
        #endregion
        #region Mine Map


        /// <summary>
        /// Saves the mine map.
        /// </summary>
        /// <param name="map">The map.</param>
        public void SaveMineMap(IEnumerable<int> map)
        {
            var command = connection.CreateCommand(commandTextMineMaxOfAttempt);
            var result = command.ExecuteScalar();
            var lastAttmptNumber = result is DBNull ? 0 : (int)result;
            lastAttmptNumber++;

            var commandInsert = (OleDbCommand)connection.CreateCommand(commandTextMineMapInsert);
            commandInsert.Parameters.Add("Attempt", OleDbType.Integer);
            commandInsert.Parameters.Add("Position", OleDbType.TinyInt);
            foreach (var position in map)
            {
                commandInsert.Parameters[0].Value = lastAttmptNumber;
                commandInsert.Parameters[1].Value = position;
                commandInsert.ExecuteNonQuery();
            }
        }
        /// <summary>
        /// Gets the mines.
        /// </summary>
        /// <returns></returns>
        public List<List<int>> GetMines()
        {
            var result = new List<List<int>>();
            var command = (OleDbCommand)connection.CreateCommand(commandTextMinesSelect);
            var currentAttempt = 0;
            var storage = new List<int>();
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var attempt = reader.GetInt32(0);
                    var position = reader.GetByte(1);
                    if (currentAttempt != attempt)
                    {
                        storage = new List<int>();
                        result.Add(storage);
                        currentAttempt = attempt;
                    }
                    storage.Add(position);
                }
            }
            return result;
        }
        /// <summary>
        /// Gets the crystal map positions top.
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, int> GetMineMapPositionsTop()
        {
            var result = new Dictionary<int, int>();
            var command = (OleDbCommand)connection.CreateCommand(commandTextMineSelectBest);
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    result.Add(reader.GetByte(0), reader.GetInt32(1));
                }
            }
            return result;
        }
        #endregion

        #region Crystam Map
        /// <summary>
        /// Saves the crystal map.
        /// </summary>
        /// <param name="isSmall">if set to <c>true</c> [is small].</param>
        /// <param name="map">The map.</param>
        public void SaveCrystalMap(bool isSmall, IEnumerable<int> map)
        {
            var command = connection.CreateCommand(commandTextCrystalMaxOfAttempt);
            var result = command.ExecuteScalar();
            var lastAttmptNumber = result is DBNull ? 0 : (int)result;
            lastAttmptNumber++;

            var commandInsert = (OleDbCommand)connection.CreateCommand(commandTextCrystalMapInsert);
            commandInsert.Parameters.Add("Attempt", OleDbType.Integer);
            commandInsert.Parameters.Add("Position", OleDbType.TinyInt);
            commandInsert.Parameters.Add("IsSmall", OleDbType.Boolean).Value = isSmall;
            foreach (var position in map)
            {
                commandInsert.Parameters[0].Value = lastAttmptNumber;
                commandInsert.Parameters[1].Value = position;
                commandInsert.ExecuteNonQuery();
            }
        }


        /// <summary>
        /// Gets the crystal map positions top.
        /// </summary>
        /// <param name="isSmall">if set to <c>true</c> [is small].</param>
        /// <returns></returns>
        public Dictionary<int, int> GetCrystalMapPositionsTop(bool isSmall)
        {
            var result = new Dictionary<int, int>();
            var command = (OleDbCommand)connection.CreateCommand(@"SELECT 
[Position], COUNT([Attempt]) FROM [CrystalMap] WHERE [IsSmall]=? GROUP BY [Position];");
            command.Parameters.Add("IsSmall", OleDbType.Boolean).Value = isSmall;
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    result.Add(reader.GetInt32(0), reader.GetInt32(1));
                }
            }
            return result;
        }


        /// <summary>
        /// Gets the crystal map last attempt.
        /// </summary>
        /// <param name="isSmall">if set to <c>true</c> [is small].</param>
        /// <returns></returns>
        public int GetCrystalMapLastAttempt(bool isSmall)
        {
            var command = (OleDbCommand)connection.CreateCommand(@"SELECT  Max(CrystalMap.Attempt) AS MaxOfAttempt
FROM [CrystalMap] WHERE [IsSmall]=@isSmall ;");
            command.AddParameter("@isSmall", isSmall);
            return (int)command.ExecuteScalar();
        }

        /// <summary>
        /// Gets the crystal map last attempt.
        /// </summary>
        /// <param name="isSmall">if set to <c>true</c> [is small].</param>
        /// <returns></returns>
        public List<int> GetCrystalMapLastAttemptMap(bool isSmall)
        {
            var lastAtempt = GetCrystalMapLastAttempt(isSmall);

            var command = connection.CreateCommand(@" SELECT [Position] FROM [CrystalMap] WHERE [Attempt]=@Attempt ; ");
            command.AddParameter("Attempt", lastAtempt);
            return ReadInt(command);

        }
        /// <summary>
        /// Gets the crystal maps.
        /// </summary>
        /// <param name="isSmall">if set to <c>true</c> [is small].</param>
        /// <returns></returns>
        public List<List<int>> GetCrystalMaps(bool isSmall)
        {
            var result = new List<List<int>>();
            var command = (OleDbCommand)connection.CreateCommand(commandTextCrystalGetMaps);
            command.Parameters.Add("IsSmall", OleDbType.Boolean).Value = isSmall;
            var currentAttempt = 0;
            List<int> storage = null;
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var attempt = reader.GetInt32(0);
                    var position = reader.GetInt32(1);
                    if (currentAttempt != attempt)
                    {
                        storage = new List<int>();
                        result.Add(storage);
                        currentAttempt = attempt;
                    }
                    storage.Add(position);
                }
            }
            return result;
        }

        #endregion




        public void Dispose()
        {
            connection.Dispose();
        }

        private List<int> ReadInt(DbCommand command)
        {
            var result = new List<int>();
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    result.Add(reader.GetInt32(0));
                }
            }
            return result;
        }
        private List<String> ReadString(DbCommand command)
        {
            var result = new List<String>();
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    result.Add(reader.GetString(0));
                }
            }
            return result;
        }

        private delegate T Fabric<T>(IDataRecord reader);

        private List<T> ReadObjects<T>(DbCommand command, Fabric<T> fabric) where T : class
        {
            var result = new List<T>();
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    result.Add(fabric(reader));
                }
            }
            return result;
        }

        private T ReadObject<T>(DbCommand command, Fabric<T> fabric) where T : class
        {
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    return fabric(reader);
                }
            }
            return null;
        }



    }
}