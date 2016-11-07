using System;
using BotvaSpider.Core;
using BotvaSpider.Gears;
using Savchin.Core;
using WatiN.Core;

namespace BotvaSpider.Data
{
    public class Player : User, ICloneable
    {

        #region Properties
        /// <summary>
        /// Gets or sets the spirits.
        /// </summary>
        /// <value>The spirits.</value>
        public SpiritType Spirits { get; private set; }
        /// <summary>
        /// Gets or sets the guild.
        /// </summary>
        /// <value>The guild.</value>
        public GuildType Guild { get; private set; }
        /// <summary>
        /// Gets or sets the money.
        /// </summary>
        /// <value>The money.</value>
        public int Money { get; private set; }
        /// <summary>
        /// Gets or sets the cristals.
        /// </summary>
        /// <value>The cristals.</value>
        public int Cristals { get; private set; }
        /// <summary>
        /// Green
        /// </summary>
        public int Green { get; private set; }

        /// <summary>
        /// Gets or sets the health.
        /// </summary>
        /// <value>The health.</value>
        public int Health { get; set; }
        /// <summary>
        /// Gets or sets the max health.
        /// </summary>
        /// <value>The max health.</value>
        public int MaxHealth { get; set; }


        /// <summary>
        /// Gets or sets the coulomb.
        /// </summary>
        /// <value>The coulomb.</value>
        public Coulomb Coulomb { get; private set; }

        /// <summary>
        /// Gets or sets the blue potion count.
        /// </summary>
        /// <value>The blue potion count.</value>
        public int BluePotionCount { get; private set; }
        /// <summary>
        /// Gets or sets a value indicating whether [wardrobe hase emty slots].
        /// </summary>
        /// <value>
        /// 	<c>true</c> if [wardrobe hase emty slots]; otherwise, <c>false</c>.
        /// </value>
        public bool WardrobeHasEmtySlots
        {
            get
            {
                return Wardrobe.EmptySlotsCount > 0;
            }
        }
        ///// <summary>
        ///// Gets the controller.
        ///// </summary>
        ///// <value>The controller.</value>
        //public GameController Controller
        //{
        //    get { return controller; }
        //}
        private readonly Wardrobe wardrobe;
        /// <summary>
        /// Gets the wardrobe.
        /// </summary>
        /// <value>The wardrobe.</value>
        public Wardrobe Wardrobe
        {
            get { return wardrobe; }
        }


        private readonly GameController controller;
        private readonly IE browser;
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="Player"/> class.
        /// </summary>
        public Player(GameController controller)
        {
            this.controller = controller;
            browser = controller.Browser;
            wardrobe = new Wardrobe(controller);

        }
        /// <summary>
        /// Prepares for action.
        /// </summary>
        /// <param name="action">The action.</param>
        public void PrepareForAction(PlayerAction action)
        {
            var settings = AppCore.BotvaSettings;
            if (settings.AutoDisguise) PutOn(settings.Wardrobe.GetCoulomb(action));
        }

        /// <summary>
        /// Puts the on.
        /// </summary>
        /// <param name="item">The item.</param>
        public void PutOn(Coulomb item)
        {
            if (item == Coulomb.Undefined || Coulomb == item) return;

            if (Wardrobe.PutOn(item)) Coulomb = item;
        }

        /// <summary>
        /// Gets the resource count.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public int GetResourceCount(Resource type)
        {
            switch (type)
            {
                case Resource.Gold:
                    return Money;

                case Resource.Crystals:
                    return Cristals;
                case Resource.Green:
                    return Green;
                default:
                    throw new ArgumentOutOfRangeException("type");
            }
        }


        /// <summary>
        /// Determines whether [is put on] [the specified item].
        /// </summary>
        /// <param name="item">The item.</param>
        public bool IsPutOn(Coulomb item)
        {
            return Coulomb == item;
        }
        /// <summary>
        /// Tries the treat.
        /// </summary>
        public void TryTreat()
        {
            Wardrobe.TryTreat();
            SetHealth();

        }

        /// <summary>
        /// Updates the specified browser.
        /// </summary>
        public void Update()
        {
            controller.OpenUrl(controller.UrlAccount);

            SetRace(browser);
            UpdateResources();
            SetHealth();
            FillSkils(browser);
            UpdateItems();
        }

        /// <summary>
        /// Updates the items.
        /// </summary>
        public void UpdateItems()
        {
            Wardrobe.UpdateStatus();
            Coulomb = Wardrobe.GetClothedPendent();
            BluePotionCount = Wardrobe.GetBluePotionCount();
            if (AppCore.BotvaSettings.AutoDisguise) CheckExistsCouloubs();

            UpdateSpirits();
        }



        /// <summary>
        /// Updates the resources.
        /// </summary>
        public void UpdateResources()
        {
            var tableState = browser.Table(Find.By("background", controller.ImageTitleBackground));
            if(!tableState.Exists)
            {
                AppCore.LogSystem.Error("Не найден изображение ImageTitleBackground");
                return;
            }
            var guildCell = tableState.TableRows[4].TableCells[2];
            if (guildCell.Images.Count > 0)
            {
                var guilUrl = guildCell.Images[0].Src;
                var tmp = guilUrl.Substring(controller.UrlImageGuildsSSTemplate.IndexOf('{'));
                tmp = tmp.Substring(0, tmp.Length - 6);
                Guild = (GuildType)int.Parse(tmp);
            }
            if (string.IsNullOrEmpty(Name))
            {
                AppCore.AcountSettings.UserName = Name = tableState.TableRows[2].TableCells[0].Text;
            }

            var resourceRow = tableState.TableRows[5];

            Money = Integer.Parse(resourceRow.TableCells[0].Text);
            Cristals = Integer.Parse(resourceRow.TableCells[1].Text);
            Green = Integer.Parse(resourceRow.TableCells[2].Text);
        }

        /// <summary>
        /// Determines whether this instance can kill the specified user.
        /// </summary>
        /// <param name="rival">The user.</param>
        /// <returns>
        /// 	<c>true</c> if this instance can kill the specified user; otherwise, <c>false</c>.
        /// </returns>
        public bool CanKill(Rival rival)
        {
            if (rival.Source != RivalSource.FromHotList &&
                !AppCore.AttackSettings.GetLevelFilter(rival.Source).IsValid(Level, rival.Level))
            {
                AppCore.LogFights.Debug(
                    string.Format("Противник {0} негодиться для атаки/", rival.Name),
                    "Он не прошел фильтр по уровню. См. Настройки > Атака", rival);
                return false;
            }

            var skillDifference = GetSkillDifference(rival);
            double minDifference = AppCore.AttackSettings.MinSkillDifference;
            if (this.Coulomb != Coulomb.SmartBaby) minDifference *= 2;
            if (rival.Level > Level) minDifference *= 1.5;
            if (skillDifference < minDifference)
            {
                var title = string.Format("Противник {0} негодиться для атаки.", rival.Name);
                var message = string.Format("Разница в скилах {0} а требуется {1}", skillDifference, minDifference);
                if (rival.Source == RivalSource.FromFarm)
                {
                    AppCore.LogFights.Warn(title, message, rival);
                }
                else
                {
                    AppCore.LogFights.Debug(title, message, rival);
                }
                return false;
            }

            if (CompareTo(rival) < 0)
            {
                AppCore.LogFights.Warn(
                          string.Format("Противник {0} негодиться для атаки.", rival.Name),
                          string.Format("Разница в скилах {0} а требуется {1}", skillDifference, minDifference), rival);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Fills the skils.
        /// </summary>
        /// <param name="browser">The browser.</param>
        public override void FillSkils(IE browser)
        {
            var skillsTable = browser.Table(Find.ByClass("skills"));

            Level = int.Parse(skillsTable.TableRows[0].TableCells[2].Text);
            //FIXED
            FillSkils(skillsTable, 0, 3);
        }
        #region Helpers

        /// <summary>
        /// Updates the spirits.
        /// </summary>
        private void UpdateSpirits()
        {
            SpiritType result = 0;
            foreach (var item in wardrobe.Items)
            {
                if (item.IsPutOn) result = result | item.Spirit;
            }
            Spirits = result;
        }

        private void SetHealth()
        {
            var healthTable = browser.Table(Find.ByClass("expHP"));
            var text = healthTable.TableRows[0].TableCells[0].Text;

            var parts = text.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            parts = parts[1].Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            Health = int.Parse(parts[0]);
            MaxHealth = int.Parse(parts[1]);
        }





        private void CheckExistsCouloubs()
        {
            foreach (var pair in AppCore.BotvaSettings.Wardrobe)
            {
                if ((int)pair.Value < 1 || Wardrobe.HasItem(pair.Value)) continue;

                var message = String.Format(
                    "Для дествия '{0}' указан кулон '{1}'. Он остуствует в одевалке. Положите данный кулон в одевалку или поменяйте настройки.",
                    pair.Key.GetDescription(), pair.Value.GetDescription());
                AppCore.LogFights.Warn("Отсутсвует кулон в одевалке.", message);
            }
        }
        #endregion

        #region Implementation of ICloneable

        /// <summary>
        ///                     Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>
        ///                     A new object that is a copy of this instance.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public object Clone()
        {
            return MemberwiseClone();
        }

        #endregion
    }
}