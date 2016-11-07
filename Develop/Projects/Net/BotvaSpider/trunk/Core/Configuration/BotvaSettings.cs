using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using BotvaSpider.Core;
using BotvaSpider.Data;
using Savchin.TimeManagment;

namespace BotvaSpider.Configuration
{


    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class BotvaSettings
    {
        #region Properties

        /// <summary>
        /// Gets or sets the accountant settings.
        /// </summary>
        /// <value>The accountant settings.</value>
        public AccountantSettings AccountantSettings { get; set; }
        /// <summary>
        /// Gets or sets the attack settings.
        /// </summary>
        /// <value>The attack settings.</value>
        public AttackSettings AttackSettings { get; set; }

        /// <summary>
        /// Gets or sets the miner settings.
        /// </summary>
        /// <value>The miner settings.</value>
        public MinerSettings MinerSettings { get; set; }

        /// <summary>
        /// Gets or sets the acount settings.
        /// </summary>
        /// <value>The acount settings.</value>
        public AcountSettings AcountSettings { get; set; }

        /// <summary>
        /// Gets or sets the max internet errors.
        /// </summary>
        /// <value>The max internet errors.</value>
        [XmlAttribute]
        public int MaxInternetErrors { get; set; }
        /// <summary>
        /// Gets or sets the max errors.
        /// </summary>
        /// <value>The max errors.</value>
        [XmlAttribute]
        public int MaxDangerousErrors { get; set; }


        /// <summary>
        /// Gets or sets a value indicating whether [user patrol].
        /// </summary>
        /// <value><c>true</c> if [user patrol]; otherwise, <c>false</c>.</value>
        [XmlAttribute]
        public bool UsePatrol { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [show allerts].
        /// </summary>
        /// <value><c>true</c> if [show allerts]; otherwise, <c>false</c>.</value>
        [XmlAttribute]
        public bool ShowAllerts { get; set; }



        /// <summary>
        /// Gets or sets the wardrobe.
        /// </summary>
        /// <value>The wardrobe.</value>
        public WardrobeSettings Wardrobe { get; set; }




        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="BotvaSettings"/> is autoisguise.
        /// </summary>
        /// <value><c>true</c> if autoisguise; otherwise, <c>false</c>.</value>
        [XmlAttribute]
        public bool AutoDisguise { get; set; }



        /// <summary>
        /// Gets or sets the schedule.
        /// </summary>
        /// <value>The schedule.</value>
        public List<TimeRange> Schedule { get; set; }




        /// <summary>
        /// Gets or sets the bastard list.
        /// </summary>
        /// <value>The bastard list.</value>
        [Editor(
"System.Windows.Forms.Design.StringCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
"System.Drawing.Design.UITypeEditor,System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
)]
        public List<string> BastardList { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [use cow search].
        /// </summary>
        /// <value><c>true</c> if [use cow search]; otherwise, <c>false</c>.</value>
        [XmlAttribute]
        public bool UseCowSearch { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [refresh info].
        /// </summary>
        /// <value><c>true</c> if [refresh info]; otherwise, <c>false</c>.</value>
        [XmlAttribute]
        public bool RefreshInfo { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [cow search counter].
        /// </summary>
        /// <value><c>true</c> if [cow search counter]; otherwise, <c>false</c>.</value>
        [XmlAttribute]
        public string CowSearchCounter { get; set; }


        /// <summary>
        /// Gets or sets a value indicating whether [allow debugger].
        /// </summary>
        /// <value><c>true</c> if [allow debugger]; otherwise, <c>false</c>.</value>
        [XmlAttribute]
        public bool AllowDebugger { get; set; }



        /// <summary>
        /// Gets or sets a value indicating whether [auto treat].
        /// </summary>
        /// <value><c>true</c> if [auto treat]; otherwise, <c>false</c>.</value>
        [XmlAttribute]
        public bool AutoTreat { get; set; }




        #endregion



        /// <summary>
        /// Initializes a new instance of the <see cref="BotvaSettings"/> class.
        /// </summary>
        public BotvaSettings()
        {
            AccountantSettings = new AccountantSettings();
            AttackSettings = new AttackSettings();
            Wardrobe = new WardrobeSettings();
            MinerSettings = new MinerSettings();
            AcountSettings = new AcountSettings();

            Schedule = new List<TimeRange>();

            BastardList = new List<string>();

        }
        /// <summary>
        /// Gets the sleep range.
        /// </summary>
        /// <returns></returns>
        public TimeRange GetSleepRange()
        {
            return Schedule.Where(e => e.IsInRange(DateTime.Now.ToTime())).FirstOrDefault();
        }

    }
}