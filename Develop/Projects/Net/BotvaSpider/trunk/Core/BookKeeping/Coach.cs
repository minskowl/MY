using System.Collections.Generic;
using System.Linq;
using BotvaSpider.Configuration;
using BotvaSpider.Core;
using BotvaSpider.Data;
using Savchin.Core;
using WatiN.Core;

namespace BotvaSpider.BookKeeping
{
    /// <summary>
    /// Coach
    /// </summary>
    public class Coach
    {
        private Dictionary<SkillType, int> skillPrices;
        private readonly Player _player;
        private readonly GameController _controller;

        /// <summary>
        /// Initializes a new instance of the <see cref="Coach"/> class.
        /// </summary>
        /// <param name="_player">The _player.</param>
        /// <param name="_controller">The _controller.</param>
        public Coach(Player _player, GameController _controller)
        {
            this._player = _player;
            this._controller = _controller;
        }

        /// <summary>
        /// Does the traning.
        /// </summary>
        public bool DoTraning()
        {
            return MakeInvestments(AppCore.AccountantSettings.NormalStrategy);
        }

        /// <summary>
        /// Makes the investments.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <returns></returns>
        public bool MakeInvestments(InvestmentStrategySettings settings)
        {
            if (!settings.Enabled)
            {
                AppCore.LogAccountant.Suggestion("Инвестирование выключено.", "Автоматическое инвестирование позволит вам сохранить деньги и автоматически делать покупки. \n Зайдите в Настройки > Бухгалтерия. ");
                return false;
            }
            if (skillPrices == null) InitializeSkillsPrices();

            if (settings.Type == InvestmentStrategy.Undefined) return true;
            var success = false;
            if (settings.IsSet(InvestmentStrategy.BuyMostExpensiveSkill))
            {
                if (BuyMostExpensiveSkill())
                    success = true;
            }

            if (settings.IsSet(InvestmentStrategy.BuyMostExpensiveSkill))
            {
                if (BuyMostExpenciveFromAvailible())
                    success = true;
            }

            if (settings.IsSet(InvestmentStrategy.BuySpecifiedSkill) &&
                settings.SelectedSkill != SkillType.Undefined &&
                settings.SelectedSkill != SkillType.Glory)
            {
                if (BuySpecifiedSkill(settings.SelectedSkill))
                    success = true;
            }

            if (settings.IsSet(InvestmentStrategy.BuyLowestSkill))
            {
                if (BuyLowestSkill())
                    success = true;
            }


            return success;
        }


        /// <summary>
        /// Initializes the skills prices.
        /// </summary>
        private void InitializeSkillsPrices()
        {
            AppCore.LogAccountant.Debug("Читаем цены на статы");

            _player.PrepareForAction(PlayerAction.BySkill);

            skillPrices = new Dictionary<SkillType, int>();
            _controller.OpenUrl(_controller.UrlTraining);

            var row = _controller.Browser.TableRow(Find.ByClass("row_1 center"));
            var skillTable = row.ContainingTable;

            SetSkillInfo(SkillType.Power, skillTable.TableRows[1]);
            SetSkillInfo(SkillType.Protection, skillTable.TableRows[3]);
            SetSkillInfo(SkillType.Dexterity, skillTable.TableRows[5]);
            SetSkillInfo(SkillType.Weight, skillTable.TableRows[7]);
            SetSkillInfo(SkillType.Mastery, skillTable.TableRows[9]);
        }



        private bool BuyLowestSkill()
        {
            var minLevelSkin = _player.Skills.Where(e => e.SkilType != SkillType.Glory).Select(e => e.Points).Min();
            var minSkillType = _player.Skills.Where(e => e.Points == minLevelSkin).FirstOrDefault().SkilType;
            var minSkillPrice = skillPrices[minSkillType];

            return BuySkill(minSkillType, minSkillPrice, "Куплен самый маленький ");
        }

        private bool BuySpecifiedSkill(SkillType type)
        {
            return BuySkill(type, skillPrices[type], "Куплен оределенный скилл ");
        }

        private bool BuyMostExpensiveSkill()
        {
            var mostExpenciveSkillPrice = skillPrices.Select(e => e.Value).Max();
            var mostExpenciveSkillType = skillPrices.Where(e => e.Value == mostExpenciveSkillPrice).FirstOrDefault().Key;

            return BuySkill(mostExpenciveSkillType, mostExpenciveSkillPrice, "Куплен самый дорой скилл ");
        }

        private bool BuyMostExpenciveFromAvailible()
        {
            var availible = skillPrices.Where(e => e.Value + AppCore.AccountantSettings.MinMoney <= _player.Money).ToArray();
            if (availible.Length == 0) return false;


            var mostExpenciveSkillPrice = availible.Max(e => e.Value);
            var mostExpenciveSkillType = skillPrices.Where(e => e.Value == mostExpenciveSkillPrice).FirstOrDefault().Key;

            return BuySkill(mostExpenciveSkillType, mostExpenciveSkillPrice, "Куплен самый дорой из доступных скиллов ");
        }

        private bool BuySkill(SkillType type, int price, string message)
        {

            if (_player.Money <= price + AppCore.BotvaSettings.AccountantSettings.MinMoney) return false;

            _player.PrepareForAction(PlayerAction.BySkill);
            if (!_controller.BuySkill(type)) return false;


            var skillName = type.GetDescription();
            Accountant.RegisterPurchase(BalanceCategory.Skills, skillName, Price.Gold(price));
            AppCore.LogAccountant.Debug(message + " " + skillName);
            InitializeSkillsPrices();
            return true;
        }


        private void SetSkillInfo(SkillType skillType)
        {
            var img = _controller.Browser.Image(Find.BySrc(_controller.SkillImages[skillType]));
            var row = (TableRow)img.Parent.Parent;
            SetSkillInfo(skillType, (TableRow)row.PreviousSibling);


        }
        private void SetSkillInfo(SkillType skillType, TableRow row)
        {
            var price = int.Parse(row.TableCells[2].Text);
            skillPrices.Add(skillType, price);

            if (AppCore.AppSettings.UseMainSkills)
            {
                var level = int.Parse(((TableRow)row.NextSibling).TableCells[0].Text);
                _player.Skills.SetSkill(skillType, level);
            }
        }


    }
}
