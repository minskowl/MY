using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using BotvaSpider.Core;
using BotvaSpider.Gears;

namespace BotvaSpider.Configuration
{


    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class AccountantSettings
    {
        /// <summary>
        /// Gets or sets a value indicating whether [investment enabled].
        /// </summary>
        /// <value><c>true</c> if [investment enabled]; otherwise, <c>false</c>.</value>
        [XmlAttribute]
        public bool InvestmentEnabled { get; set; }



        /// <summary>
        /// Gets or sets the shoping inteval.
        /// </summary>
        /// <value>The shoping inteval.</value>
        [XmlAttribute]
        public int ShopingInteval { get; set; }


        /// <summary>
        /// Gets or sets the min money.
        /// </summary>
        /// <value>The min money.</value>
        [XmlAttribute]
        [DisplayName("������� �� �����.")]
        [Description("��� �������������� ��� ��������� ������ ����� �� �����. ���������� ���������� �������������� ��� �� ������� �� ���������")]
        public int MinMoney { get; set; }

        /// <summary>
        /// Gets or sets the upgrade couloubs.
        /// </summary>
        /// <value>The upgrade couloubs.</value>
        [XmlAttribute]
        [DisplayName("������ �����.")]
        public Coulomb UpgradeCouloub { get; set; }


        /// <summary>
        /// Gets or sets the search strategy.
        /// </summary>
        /// <value>The search strategy.</value>
        [XmlElement]
        public SearchStrategySettings SearchStrategy { get; set; }

        /// <summary>
        /// Gets or sets the normal strategy.
        /// </summary>
        /// <value>The normal strategy.</value>
        [XmlElement]
        [DisplayName("������� ��������������.")]
        [Category("��������������")]
        public InvestmentStrategySettings NormalStrategy { get; set; }

        /// <summary>
        /// Gets or sets the alert strategy.
        /// </summary>
        /// <value>The alert strategy.</value>
        [XmlElement]
        [Category("��������������")]
        [DisplayName("���������� ��������������.")]
        [Description("��� ��������� ��������� ������ � ���� �� ���������. ��� ��� ��������� ��������� �� ������ ��������. �������������� ������������� ����� 45 ��� ����� ���������.")]
        public InvestmentStrategySettings AlertStrategy { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountantSettings"/> class.
        /// </summary>
        public AccountantSettings()
        {
            NormalStrategy = new InvestmentStrategySettings();
            AlertStrategy = new InvestmentStrategySettings();
            SearchStrategy= new SearchStrategySettings();

            ShopingInteval = 5;
        }

        /// <summary>
        /// Gets the min resource.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <returns></returns>
        public int GetMinResource(Resource resource)
        {
            return resource == Resource.Gold ? MinMoney : 0;
        }

    }
}
