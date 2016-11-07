using System;
using System.ComponentModel;
using System.Text;
using BotvaSpider.Data;
using BotvaSpider.Gears;
using Savchin.ComponentModel;

namespace BotvaSpider.Core
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class FightResult
    {
        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        /// <value>The date.</value>
        [DisplayName("Время боя")]
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the source.
        /// </summary>
        /// <value>The source.</value>
        [DisplayName("От куда противник")]
        [TypeConverter(typeof(EnumTypeConverter))]
        public RivalSource RivalSource { get; set; }

        /// <summary>
        /// Gets or sets the rival.
        /// </summary>
        /// <value>The rival.</value>
        [DisplayName("Противник")]
        public Rival Rival { get; set; }


        [DisplayName("Время след. боя")]
        public DateTime NexFightTime { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="FightResult"/> is win.
        /// </summary>
        /// <value><c>true</c> if win; otherwise, <c>false</c>.</value>
        [DisplayName("Победа")]
        public bool Win { get; set; }

        /// <summary>
        /// Gets or sets the money.
        /// </summary>
        /// <value>The money.</value>
        [DisplayName("Золото")]
        [Description("Получено золота за бой")]
        public int Money { get; set; }
        /// <summary>
        /// Gets or sets the expirience.
        /// </summary>
        /// <value>The expirience.</value>
        [DisplayName("Опыт")]
        [Description("Получено опыта за бой")]
        public int Expirience { get; set; }
        /// <summary>
        /// Gets or sets the crystals.
        /// </summary>
        /// <value>The crystals.</value>
        [DisplayName("Кристаллы")]
        [Description("Получено кристаллов за бой")]
        public int Crystals { get; set; }

        /// <summary>
        /// Gets or sets the rival injury health.
        /// </summary>
        /// <value>The rival injury health.</value>
        [DisplayName("Ущерб здоровья")]
        [Description("Снесено у противника здоровья")]
        public int RivalInjuryHealth { get; set; }
        /// <summary>
        /// Gets or sets the rival health.
        /// </summary>
        /// <value>The rival health.</value>
        [DisplayName("Здоровье")]
        [Description("Кол-во оставщегося здоровья у противника")]
        public int RivalHealth { get; set; }



        /// <summary>
        /// Gets or sets the skill difference.
        /// </summary>
        /// <value>The skill difference.</value>
        [DisplayName("Разница в статах")]
        public double SkillDifference{get;set;}
        /// <summary>
        /// Gets or sets the coulomb.
        /// </summary>
        /// <value>The coulomb.</value>
        [DisplayName("Кулон")]
        [TypeConverter(typeof(EnumTypeConverter))]
        public Coulomb Coulomb { get; set; }

        /// <summary>
        /// Gets or sets the fight URL.
        /// </summary>
        /// <value>The fight URL.</value>
        [DisplayName("Лог боя")]
        public string FightUrl { get; set; }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </returns>
        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.AppendLine(string.Format("Бой {0} с '{1}' {2}", Date, Rival.Name, Win ? "победа" : "пройгрыш"));
            builder.AppendLine(string.Format("Разница в статах {0}", SkillDifference));
            builder.AppendLine(string.Format("Золота={0} Опыта={1} Кристалов={2}", Money, this.Expirience, this.Crystals));
            return builder.ToString();
        }

    }
}