using System.ComponentModel;
using BotvaSpider.Core;

namespace BotvaSpider.Gears
{
    public enum Ticket : int
    {
        [Description("����� �� ���. ������")]
        Small=1,
        [Description("����� �� ���. ������")]
        Big=2
    }
    public enum Weapon : int
    {
        [Description("�����")]
        Capec = 19,

    }
    public enum Key : int
    {
        [Description("����")]
        Default = 1,
    }

    public enum Shield : int
    {
        [Description("��������")]
        Slavyanec = 4,

    }

    public enum Helmet : int
    {
        [Description("���������")]
        Zeltapuza = 9,

    }

    public enum Armor : int
    {
        [Description("�����������")]
        Plasticroca = 13,

        [Description("������������ ")]
        Pontipilacas = 12,

        [Description("����������")]
        Vasiliscus = 11,
        [Description("������")]
        Leginz = 10,

        [Description("���������")]
        Steclyazer = 10,

        [Description("�������")]
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
        [Description("����� ����")]
        [Category("������")]
        HummerVodoo = 9,

        /// <summary>
        /// AntiVodoo
        /// </summary>
        [Description("���������")]
        [Category("������")]
        AntiVodoo = 11,

        /// <summary>
        /// CrystalThief
        /// </summary>
        [Description("���������")]
        [Category("������")]
        CrystalThief = 14,

        /// <summary>
        /// CrystalLuck
        /// </summary>
        [Description("�������")]
        [Category("��������")]
        CrystalLuck = 17,

        /// <summary>
        /// CrystalLuck
        /// </summary>
        [Description("�����������")]
        [Category("��������")]
        Unscrewer = 18,

        /// <summary>
        /// CopyCryst
        /// </summary>
        [Description("���������")]
        [Category("��������")]
        CopyCryst = 22,

        /// <summary>
        /// BigPaunch
        /// </summary>
        [Description("������� ����")]
        [Category("������")]
        BigPaunch = 4,

        /// <summary>
        /// 
        /// </summary>
        [Description("������ ����")]
        [Category("������")]
        SmartBaby = 1,

        /// <summary>
        /// 
        /// </summary>
        [Description("���������")]
        [Category("������")]
        Drill = 12,

        /// <summary>
        /// BigPaunch
        /// </summary>
        [Description("���������")]
        [Category("������")]
        Attacker = 8,

        /// <summary>
        /// 
        /// </summary>
        [Description("��� ������")]
        [Category("������")]
        TrippleHoof = 5,


        [Description("�������")]
        [Category("������")]
        Kakdams = 2,


        /// <summary>
        /// Antimag
        /// </summary>
        [Description("�������")]
        [Category("������")]
        Antimag = 27,

        /// <summary>
        /// CrystalRadar
        /// </summary>
        [Description("����������")]
        [Category("��������")]
        CrystalRadar = 16,

        [Description("��������")]
        [Category("��������")]
        Walker = 15,

        /// <summary>
        /// Builder
        /// </summary>
        [Description("����������")]
        [Category("��������")]
        Builder = 36,
        /// <summary>
        /// None
        /// </summary>
        [Description("��� ������")]
        None = -1,

        /// <summary>
        /// None
        /// </summary>
        [Description("�����������")]
        Undefined = 0
    }
}