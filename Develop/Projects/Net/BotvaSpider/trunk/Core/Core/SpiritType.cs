using System;
using System.ComponentModel;

namespace BotvaSpider.Core
{
    [Flags]
    public enum SpiritType : int
    {
        [Description("Нету")]
        None = 0x0,
        [Description("Усердный шахтер")]
        AssiduousMiner = 0x1,
        [Description("Месть")]
        Revenge = 0x2,
        [Description("Титан")]
        Titan = 0x4,
        [Description("Озверин")]
        Ozverin = 0x8,
        [Description("Кышатсюда")]
        KyshOtsuda = 0x10,
        [Description("Живодер")]
        Zivoder = 0x20,
        [Description("Мега-щит")]
        MegShield = 0x40,
        [Description("Чудо-щит")]
        MiracleShield = 0x80,
        [Description("Оглушка")]
        Oglushka = 0x100,
        [Description("Отвратка")]
        Otvratka = 0x200,
        [Description("Антизаговорка")]
        Antizagavorka = 0x400,
        [Description("Магическая отравка")]
        MagicPoison = 0x800,
        [Description("Свистелка")]
        Svistelka = 0x800,
        [Description("Антиобидчик")]
        AntiOffender = 0x1000,
        [Description("Преданность")]
        Devotion = 0x2000,
        [Description("Отважный дозорный")]
        BraveScout = 0x4000,
        [Description("Трудолюбивый фермер")]
        IndustriousFarmer = 0x8000,
        [Description("Уклонение")]
        Digression = 0x10000,
        [Description("Берсерк")]
        Berserk = 0x20000,
        [Description("Золотая Чума")]
        GoldenPlague = 0x40000,
        [Description("Смесец")]
        Smesec = 0x80000,
        [Description("Большой смесец")]
        BigSmesec = 0x100000,
        [Description("Нашатырь")]
        Nashatyry = 0x200000,
        [Description("Антикрут")]
        Antikrut = 0x200000,
        [Description("Чистое сердце")]
        ClearHeart = 0x200000,

    }
}