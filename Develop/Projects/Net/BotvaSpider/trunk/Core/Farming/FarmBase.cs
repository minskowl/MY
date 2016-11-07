using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BotvaSpider.Core;
using BotvaSpider.Data;
using Savchin.Utils;
using WatiN.Core;

namespace BotvaSpider.Farming
{
    /// <summary>
    /// FarmBase
    /// </summary>
    public abstract class FarmBase
    {
        #region Properties
        private string fileName;
        protected List<Cow> cows;



        /// <summary>
        /// Gets a value indicating whether this <see cref="MilkingFarm"/> is initialized.
        /// </summary>
        /// <value><c>true</c> if initialized; otherwise, <c>false</c>.</value>
        public bool Initialized
        {
            get { return cows != null; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is war mode.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is war mode; otherwise, <c>false</c>.
        /// </value>
        public bool IsWarMode { get; set; }

        /// <summary>
        /// Gets the cows.
        /// </summary>
        /// <value>The cows.</value>
        public List<Cow> Cows
        {
            get { return cows; }
        }
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="FarmBase"/> class.
        /// </summary>
        public FarmBase()
        {
            fileName = GetType().Name + ".xml";

        }

        /// <summary>
        /// Saves this instance.
        /// </summary>
        public void Save()
        {
            try
            {
                if (Initialized)
                    TypeSerializer<List<Cow>>.ToXmlFile(fileName, cows);
            }
            catch (Exception ex)
            {
                AppCore.LogSystem.Log.Error("Ошибка сохраниения состояния фермы", ex);
            }
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public virtual void Initialize(Player player, GameController controller)
        {
            cows = new List<Cow>();
            if (!File.Exists(fileName) || DateTime.Today.Day != File.GetLastWriteTime(fileName).Day)
            {
                return;
            }
            try
            {
                var readCows = TypeSerializer<List<Cow>>.FromXmlFile(fileName);
                if (readCows != null && readCows.Count > 0) cows = readCows;
            }

            catch (Exception ex)
            {
                AppCore.LogSystem.Log.Error("Ошибка чтения состояния фермы", ex);
            }
        }

        /// <summary>
        /// Gets the best cow.
        /// </summary>
        /// <returns></returns>
        public abstract Cow GetBestCow();

        #region Interface

        /// <summary>
        /// Finds the cow.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        public Cow FindCow(string userName)
        {
            return cows.Where(e => e.UserName == userName).FirstOrDefault();
        }
        /// <summary>
        /// Processes the specified cow.
        /// </summary>
        /// <param name="cow">The cow.</param>
        /// <param name="result">The result.</param>
        public void Process(Cow cow, GetRivalResult result)
        {
            switch (result)
            {
                case GetRivalResult.BadHealth:
                case GetRivalResult.HaveFightInHour:
                    cow.GoRest((IsWarMode) ? 15 : 30);
                    break;
                case GetRivalResult.OnStorm:
                    cow.GoRest((IsWarMode) ? 30 : 60);
                    break;

                case GetRivalResult.Hide:
                    cow.GoRest(120);
                    break;
                case GetRivalResult.Holiday:
                    cow.GoRest(360);
                    break;
                case GetRivalResult.ManyFights:

                    cow.MilkingCount = 5;
                    cow.GoRest(360);
                    break;

                case GetRivalResult.WasBlocked:
                case GetRivalResult.ToDelete:
                case GetRivalResult.NotFinded:
                case GetRivalResult.InWhiteList:
                    RemoveCow(cow);
                    ObjectProvider.Instance.DeleteUser(cow.UserID);
                    break;
                case GetRivalResult.System:
                    cow.State = CowState.Blocked;
                    break;
            }
        }
        /// <summary>
        /// Adds the cow.
        /// </summary>
        /// <param name="result">The result.</param>
        public void AddResult(FightResult result)
        {
            var cow = cows.Where(c => c.UserName == result.Rival.Name).FirstOrDefault();
            if (cow == null)
            {
                if (result.Win)
                {
                    cow = new Cow
                    {
                        UserID = result.Rival.UserID,
                        UserName = result.Rival.Name,
                        Level = result.Rival.Level,
                        AverageBenefit = result.Money,
                        LastBenefit = result.Money,
                        MilkingCount = 1,
                        Cristals = result.Crystals,
                        RivalInjuryHealth = result.RivalInjuryHealth,
                        RivalHealth = result.RivalHealth,
                        UserType = UserType.Cow
                    };
                    cows.Add(cow);
                    cow.GoRest();
                }

            }
            else
            {
                if (result.Win)
                {
                    cow.MilkingCount++;
                    cow.RivalInjuryHealth = result.RivalInjuryHealth;
                    cow.RivalHealth = result.RivalHealth;
                    cow.Cristals += result.Crystals;
                    cow.AverageBenefit = (cow.AverageBenefit + result.Money) / 2;
                    cow.LastBenefit = result.Money;
                    cow.GoRest();
                }
                else
                {
                    cows.Remove(cow);
                }
            }
        }
        #endregion

        /// <summary>
        /// Gets the cows ID.
        /// </summary>
        /// <returns></returns>
        protected int[] GetCowsID()
        {
            return cows == null ? new int[0] : cows.Select(e => e.UserID).ToArray();
        }
        /// <summary>
        /// Adds the new.
        /// </summary>
        /// <param name="newCows">The new cows.</param>
        protected void AddNew(IEnumerable<Cow> newCows)
        {
            if (newCows == null) return;

            var exists = GetCowsID();
            cows.AddRange(newCows.Where(e => !exists.Contains(e.UserID)));
        }

        /// <summary>
        /// Fills the results.
        /// </summary>
        /// <param name="controller">The controller.</param>
        protected void FillResults(GameController controller)
        {
            controller.OpenUrl(controller.UrlShtab);


            var resultTables = controller.Browser.Tables.OfType<Table>().Where(
                t => !string.IsNullOrEmpty(t.ClassName) && t.ClassName == "default center shtab2").ToArray();

            foreach (var resultTable in resultTables)
            {
                try
                {
                    var mainRow = (TableRow)(resultTable.Parent.Parent.Parent).PreviousSibling;
                    var text = mainRow.TableCells[1].Text;

                    var name = text.Substring(0, text.LastIndexOf('[')).Trim();


                    var cow = cows.Where(c => c.UserName == name).FirstOrDefault();
                    if (cow == null) continue;

                    var moneyTotal = 0;
                    int.TryParse(mainRow.TableCells[4].Text, out moneyTotal);
                    var count = int.Parse(mainRow.TableCells[3].Text);

                    var lastResult = resultTable.TableRows[resultTable.TableRows.Length - 2];
                    var resultParts = lastResult.TableCells[0].Text.Split(new[] { ' ' },
                                                                          StringSplitOptions.RemoveEmptyEntries);
                    var lastMoney = 0;
                    int.TryParse(resultParts[1], out lastMoney);

                    cow.MilkingCount = count;
                    cow.AverageBenefit = (cow.AverageBenefit + (moneyTotal / count)) / 2;
                    cow.LastBenefit = lastMoney;

                    if (cow.MilkingCount == 5)
                    {
                        cow.State = CowState.Rest;
                        cow.ReadyAgain = DateTime.Now.AddHours(6);
                    }
                }
                catch (Exception ex)
                {
                    AppCore.LogFights.Error("Ошибка инициализации Фермы", ex);
                }
            }
        }
        /// <summary>
        /// Removes the cow.
        /// </summary>
        /// <param name="cow">The cow.</param>
        public void RemoveCow(Cow cow)
        {
            cows.Remove(cow);
        }

        /// <summary>
        /// Refreshes the state.
        /// </summary>
        protected void RefreshState()
        {
            var now = DateTime.Now;
            var restCows = cows.Where(c => c.State == CowState.Rest && c.ReadyAgain < now).ToArray();
            foreach (var cow in restCows)
            {
                cow.State = CowState.Ready;
                if (cow.MilkingCount == 5 && !IsWarMode) cow.MilkingCount = 0;
            }
        }
    }
}