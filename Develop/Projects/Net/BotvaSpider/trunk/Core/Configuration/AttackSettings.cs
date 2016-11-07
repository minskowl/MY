using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Xml.Serialization;
using BotvaSpider.Core;

namespace BotvaSpider.Configuration
{
    /// <summary>
    /// 
    /// </summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class AttackSettings
    {
        #region Properties

        /// <summary>
        /// Gets or sets the rival sources.
        /// </summary>
        /// <value>The rival sources.</value>
        public List<RivalSourceSettings> RivalSources { get; set; }
        /// <summary>
        /// Gets or sets the cows.
        /// </summary>
        /// <value>The cows.</value>
        public List<CowInfo> Cows { get; set; }
        /// <summary>
        /// Gets or sets the min benefit.
        /// </summary>
        /// <value>The min benefit.</value>
        [XmlAttribute]
        [DisplayName("Минимальный доход")]
        [Description("Если с коровы сбита сумма <= Минимальный доход, то бот перестает бить данную. Данная настройка работает для фермы.")]
        public int MinBenefit { get; set; }

        /// <summary>
        /// Gets or sets the white list.
        /// </summary>
        /// <value>The white list.</value>
        [Editor(
"System.Windows.Forms.Design.StringCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
"System.Drawing.Design.UITypeEditor,System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
)]
        public List<string> WhiteList { get; set; }

        [Editor(
"System.Windows.Forms.Design.StringCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
"System.Drawing.Design.UITypeEditor,System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
)]
        public List<string> ClanWhiteList { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [ignore wars clan].
        /// </summary>
        /// <value><c>true</c> if [ignore wars clan]; otherwise, <c>false</c>.</value>
        [XmlAttribute]
        public bool IgnoreWarsClan { get; set; }

        /// <summary>
        /// Gets or sets the attack time shift.
        /// </summary>
        /// <value>The attack time shift.</value>
        [XmlAttribute]
        public int AttackTimeShift { get; set; }



        /// <summary>
        /// Gets or sets the min skill difference.
        /// </summary>
        /// <value>The min skill difference.</value>
        [XmlAttribute]
        public int MinSkillDifference { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [allow lost glory].
        /// </summary>
        /// <value><c>true</c> if [allow lost glory]; otherwise, <c>false</c>.</value>
        [XmlAttribute]
        public bool AllowLostGlory { get; set; }



        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="AttackSettings"/> class.
        /// </summary>
        public AttackSettings()
        {
            Cows = new List<CowInfo>();
            WhiteList = new List<string>();
            RivalSources = new List<RivalSourceSettings>();
            MinSkillDifference = 40;
        }
        

        /// <summary>
        /// Gets the settings.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public RivalSourceSettings GetSettings(RivalSource type)
        {
            var result = RivalSources.Where(e => e.Source == type).FirstOrDefault();
            if (result == null)
            {
                result = new RivalSourceSettings{Source = type};
                RivalSources.Add(result);
            }
            return result;
        }

        /// <summary>
        /// Gets the level filter.
        /// </summary>
        /// <param name="type">The type.</param>
        public LevelFilter GetLevelFilter(RivalSource type)
        {
            return GetSettings(type).LevelFilter;
        }
    }
}
