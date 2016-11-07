using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Xml.Serialization;
using BotvaSpider.Controls.Configuration.Accountant;
using BotvaSpider.Core;
using Savchin.ComponentModel;


namespace BotvaSpider.Configuration
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    [Description("��������� �������������� �����. �� ��� � ��� ��������.")]
    public class InvestmentStrategySettings
    {
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="InvestmentStrategySettings"/> is enabled.
        /// </summary>
        /// <value><c>true</c> if enabled; otherwise, <c>false</c>.</value>
        [XmlAttribute]
        public bool Enabled { get; set; }


        [XmlAttribute]
        [DisplayName("���")]
        [Description("�������� ��� ��������������. �� �� ��� ���������� ���������� ������.")]
        [Editor(typeof(InvestmentStrategiesEditor), typeof(UITypeEditor))]
        public InvestmentStrategy Type { get; set; }

        /// <summary>
        /// Gets or sets the alert selected skill.
        /// </summary>
        /// <value>The alert selected skill.</value>
        [XmlAttribute]
        [TypeConverter(typeof(EnumTypeConverter))]
        [DisplayName("����")]
        [Description("�������� ���� � ������� ����� ��������������� ��� ��������� ������")]
        public SkillType SelectedSkill { get; set; }

        /// <summary>
        /// Gets or sets the stuff conditions.
        /// </summary>
        /// <value>The stuff conditions.</value>
        [DisplayName("�������� ������")]
        [Description("�������� ��� � ��� �������� � ��������. �������� � �������� ������������ !!!!")]
        public List<StuffSearchCondition> StuffConditions { get; set; }

        /// <summary>
        /// Determines whether [is set normal] [the specified mode].
        /// </summary>
        /// <param name="mode">The mode.</param>
        /// <returns>
        /// 	<c>true</c> if [is set normal] [the specified mode]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsSet(InvestmentStrategy mode)
        {
            return (Type & mode) == mode;
        }

        public InvestmentStrategySettings()
        {
            StuffConditions = new List<StuffSearchCondition>();
        }
    }
}