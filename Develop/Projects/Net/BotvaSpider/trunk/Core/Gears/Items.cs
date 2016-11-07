using System.ComponentModel;
using BotvaSpider.Core;

namespace BotvaSpider.Gears
{
    public enum Ticket : int
    {
        [Description("Билет на мал. поляну")]
        Small=1,
        [Description("Билет на бол. поляну")]
        Big=2
    }
    public enum Weapon : int
    {
        [Description("Капец")]
        Capec = 19,

    }
    public enum Key : int
    {
        [Description("Ключ")]
        Default = 1,
    }

    public enum Shield : int
    {
        [Description("Славяеец")]
        Slavyanec = 4,

    }

    public enum Helmet : int
    {
        [Description("Желтапуза")]
        Zeltapuza = 9,

    }

    public enum Armor : int
    {
        [Description("Пластикрока")]
        Plasticroca = 13,

        [Description("Понтипилакас ")]
        Pontipilacas = 12,

        [Description("Василискус")]
        Vasiliscus = 11,
        [Description("Легинз")]
        Leginz = 10,

        [Description("Стеклязер")]
        Steclyazer = 10,

        [Description("Шотатам")]
        Shotatam = 8,

    }

    /// <summary>
    /// PendantType
    /// </summary>
    public enum Coulomb : int
    {
        /// <summary>
        /// HummerVodoo
        /// </summary>
        [Description("Молот вуду")]
        [Category("Боевые")]
        HummerVodoo = 9,

        /// <summary>
        /// AntiVodoo
        /// </summary>
        [Description("Антивудот")]
        [Category("Боевые")]
        AntiVodoo = 11,

        /// <summary>
        /// CrystalThief
        /// </summary>
        [Description("Кристахап")]
        [Category("Боевые")]
        CrystalThief = 14,

        /// <summary>
        /// CrystalLuck
        /// </summary>
        [Description("Везухай")]
        [Category("Трудовые")]
        CrystalLuck = 17,

        /// <summary>
        /// CrystalLuck
        /// </summary>
        [Description("Навыворышка")]
        [Category("Трудовые")]
        Unscrewer = 18,

        /// <summary>
        /// CopyCryst
        /// </summary>
        [Description("Копикрист")]
        [Category("Трудовые")]
        CopyCryst = 22,

        /// <summary>
        /// BigPaunch
        /// </summary>
        [Description("Большое Пузо")]
        [Category("Боевые")]
        BigPaunch = 4,

        /// <summary>
        /// 
        /// </summary>
        [Description("Ловкий Пупс")]
        [Category("Боевые")]
        SmartBaby = 1,

        /// <summary>
        /// 
        /// </summary>
        [Description("Сверлячок")]
        [Category("Боевые")]
        Drill = 12,

        /// <summary>
        /// BigPaunch
        /// </summary>
        [Description("Нападайка")]
        [Category("Боевые")]
        Attacker = 8,

        /// <summary>
        /// 
        /// </summary>
        [Description("Три копыта")]
        [Category("Боевые")]
        TrippleHoof = 5,


        [Description("Какдамс")]
        [Category("Боевые")]
        Kakdams = 2,


        /// <summary>
        /// Antimag
        /// </summary>
        [Description("Антимаг")]
        [Category("Разные")]
        Antimag = 27,

        /// <summary>
        /// CrystalRadar
        /// </summary>
        [Description("Кристрадар")]
        [Category("Трудовые")]
        CrystalRadar = 16,

        [Description("Скороход")]
        [Category("Трудовые")]
        Walker = 15,

        /// <summary>
        /// Builder
        /// </summary>
        [Description("Постройчик")]
        [Category("Трудовые")]
        Builder = 36,
        /// <summary>
        /// None
        /// </summary>
        [Description("Без кулона")]
        None = -1,

        /// <summary>
        /// None
        /// </summary>
        [Description("Поумолчанию")]
        Undefined = 0
    }
}