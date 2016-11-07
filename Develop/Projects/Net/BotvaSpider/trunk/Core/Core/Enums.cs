using System;
using System.ComponentModel;

namespace BotvaSpider.Core
{
    public delegate void ShowState(string message, int percentage);
    public delegate void SimpleAction();
    public delegate void TextDelegate(string text);

    public enum GuildType : byte
    {
        [Description("Не состоит")]
        None = 0,
        [Description("Толстосумы")]
        Traders = 1,
        [Description("Железячники")]
        Smiths = 2,
        [Description("Трудяги")]
        Miner = 3,
        [Description("Работяги")]
        Farmers = 4,

    }
    /// <summary>
    /// PurchaseCategory
    /// </summary>
    public enum BalanceCategory : byte
    {
        Mine = 1,
        CoulombUpgrade = 2,
        Glades = 3,
        Skills = 4,
        Fights = 5
    }

    /// <summary>
    /// 
    /// </summary>
    public enum UserType
    {
        /// <summary>
        /// 
        /// </summary>
        [Description("Корова")]
        Cow = 1,
        /// <summary>
        /// 
        /// </summary>
        [Description("Для мести")]
        ToKill = 2,
        /// <summary>
        /// 
        /// </summary>
        [Description("Враг")]
        Fighter = 3,
        [Description("Соклан\\Друг")]
        Comrade = 7,
        [Description("Тощая корова")]
        CowPoor = 5
    }

    public enum CowState
    {
        Ready,
        Rest,
        Blocked
    }

    public enum GetRivalResult
    {
        [Description("Мало здоровья")]
        BadHealth,
        [Description("Был в бою в течении часа")]
        HaveFightInHour,
        [Description("В отпуске")]
        Holiday,
        [Description("Было 5 нападений")]
        ManyFights,
        System,
        [Description("Враг не найден")]
        NotFinded,
        [Description("Слишком часто нападаем")]
        FrequentlyFight,
        [Description("Можно бить")]
        OK,
        [Description("Спрятался")]
        Hide,
        [Description("Поставлен на удаление")]
        ToDelete,
        [Description("На штурме")]
        OnStorm,
        [Description("Заблокирован")]
        WasBlocked,
        [Description("В белом списке")]
        InWhiteList,
        [Description("Нет денег")]
        NeedMoney,

    }

    /// <summary>
    /// SimpleAction
    /// </summary>


    /// <summary>
    /// Resource
    /// </summary>
    public enum Resource : byte
    {
        /// <summary>
        /// Gold
        /// </summary>
        [Description("Золото")]
        Gold = 1,
        /// <summary>
        /// Crystals
        /// </summary>
        [Description("Кристалы")]
        Crystals = 2,
        /// <summary>
        /// Green
        /// </summary>
        [Description("Зелень")]
        Green = 3
    }
    [Flags]
    public enum Safe : int
    {
        [Description("Без сейфа")]
        None = 0,
        [Description("Денежный")]
        Money = 1,
        [Description("Кристальный")]
        Crystal = 2
    }

    /// <summary>
    /// 
    /// </summary>
    public enum SkillType
    {
        /// <summary>
        /// 
        /// </summary>
        [Description("Сила")]
        Power = 1,
        /// <summary>
        /// 
        /// </summary>
        [Description("Защита")]
        Protection = 2,
        /// <summary>
        /// 
        /// </summary>
        [Description("Ловкость")]
        Dexterity = 3,
        /// <summary>
        /// 
        /// </summary>
        [Description("Масса")]
        Weight = 4,
        /// <summary>
        /// Mastery
        /// </summary>
        [Description("Мастерство")]
        Mastery = 5,
        /// <summary>
        /// 
        /// </summary>
        [Description("Слава")]
        Glory = 6,

        /// <summary>
        /// Undefined
        /// </summary>
        [Description(" ")]
        Undefined = 0
    }


    [Flags]
    public enum InvestmentStrategy
    {
        [Description("Выключено")]
        Undefined = 0,

        [Description("Покупать самый дорогой скилл")]
        BuyMostExpensiveSkill = 0x1,

        [Description("Покупать самый маленький скилл")]
        BuyLowestSkill = 0x2,

        [Description("Покупать оперделенный ")]
        BuySpecifiedSkill = 0x4,

        [Description("Покупать самый дорогой из доступных скиллов")]
        BuyMostExpensiveFromAvalibleSkill = 0x8,

        [Description("Покупать шмотки")]
        BuySpecifiedStuff = 0x10,
    }

    public enum PlayerAction
    {
        [Description("Покупка стат")]
        BySkill = 10,
        [Description("Поиск в шахте")]
        SearchMine = 3,
        [Description("Поход в дозор")]
        GoToPatrol = 2,
        [Description("Драка")]
        Fight = 1,
        [Description("Сон")]
        Sleep = 0
    }



    public enum TradeType
    {
        /// <summary>
        /// Sale
        /// </summary>
        [Description("Продажа")]
        Sale = 0,
        /// <summary>
        /// Auction
        /// </summary>
        [Description("Аукцион")]
        Auction = 1,
    }

    /// <summary>
    /// TradeStrategy
    /// </summary>
    public enum TradeStrategy
    {
        /// <summary>
        /// ExistsMoney
        /// </summary>
        [Description("<= наличности")]
        BelowOrEqualExists,
        /// <summary>
        /// BelowOrEqualAmmount
        /// </summary>
        [Description("<= суммы")]
        BelowOrEqualAmmount
    }
}
