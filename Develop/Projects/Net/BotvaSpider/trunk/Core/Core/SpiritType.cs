using System;
using System.ComponentModel;

namespace BotvaSpider.Core
{
    [Flags]
    public enum SpiritType : int
    {
        [Description("����")]
        None = 0x0,
        [Description("�������� ������")]
        AssiduousMiner = 0x1,
        [Description("�����")]
        Revenge = 0x2,
        [Description("�����")]
        Titan = 0x4,
        [Description("�������")]
        Ozverin = 0x8,
        [Description("���������")]
        KyshOtsuda = 0x10,
        [Description("�������")]
        Zivoder = 0x20,
        [Description("����-���")]
        MegShield = 0x40,
        [Description("����-���")]
        MiracleShield = 0x80,
        [Description("�������")]
        Oglushka = 0x100,
        [Description("��������")]
        Otvratka = 0x200,
        [Description("�������������")]
        Antizagavorka = 0x400,
        [Description("���������� �������")]
        MagicPoison = 0x800,
        [Description("���������")]
        Svistelka = 0x800,
        [Description("�����������")]
        AntiOffender = 0x1000,
        [Description("�����������")]
        Devotion = 0x2000,
        [Description("�������� ��������")]
        BraveScout = 0x4000,
        [Description("������������ ������")]
        IndustriousFarmer = 0x8000,
        [Description("���������")]
        Digression = 0x10000,
        [Description("�������")]
        Berserk = 0x20000,
        [Description("������� ����")]
        GoldenPlague = 0x40000,
        [Description("������")]
        Smesec = 0x80000,
        [Description("������� ������")]
        BigSmesec = 0x100000,
        [Description("��������")]
        Nashatyry = 0x200000,
        [Description("��������")]
        Antikrut = 0x200000,
        [Description("������ ������")]
        ClearHeart = 0x200000,

    }
}