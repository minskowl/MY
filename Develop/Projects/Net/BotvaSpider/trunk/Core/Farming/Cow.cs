using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using BotvaSpider.Core;
using BotvaSpider.Data;
using BotvaSpider.Gears;
using Savchin.Utils;

namespace BotvaSpider.Farming
{


    public class Cow
    {
        /// <summary>
        /// Gets or sets the user ID.
        /// </summary>
        /// <value>The user ID.</value>
        [XmlAttribute]
        public int UserID { get; set; }
        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>The name of the user.</value>
        [XmlAttribute]
        public string UserName { get; set; }
        /// <summary>
        /// Gets or sets the level.
        /// </summary>
        /// <value>The level.</value>
        [XmlAttribute]
        public int Level { get; set; }
        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        /// <value>The state.</value>
        [XmlAttribute]
        public CowState State { get; set; }
        /// <summary>
        /// Gets or sets the average benefit.
        /// </summary>
        /// <value>The average benefit.</value>
        [XmlAttribute]
        public decimal AverageBenefit { get; set; }
        /// <summary>
        /// Gets or sets the average cristals.
        /// </summary>
        /// <value>The average cristals.</value>
        [XmlAttribute]
        public decimal AverageCristals { get; set; }

        /// <summary>
        /// Gets or sets the last benefit.
        /// </summary>
        /// <value>The last benefit.</value>
        [XmlAttribute]
        public int LastBenefit { get; set; }
        /// <summary>
        /// Gets or sets the cristals.
        /// </summary>
        /// <value>The cristals.</value>
        [XmlAttribute]
        public int Cristals { get; set; }
        /// <summary>
        /// Gets or sets the rival injury health.
        /// </summary>
        /// <value>The rival injury health.</value>
        [XmlAttribute]
        public int RivalInjuryHealth { get; set; }
        /// <summary>
        /// Gets or sets the rival health.
        /// </summary>
        /// <value>The rival health.</value>
        [XmlAttribute]
        public int RivalHealth { get; set; }
        /// <summary>
        /// Gets or sets the ready again.
        /// </summary>
        /// <value>The ready again.</value>
        [XmlAttribute]
        public DateTime ReadyAgain { get; set; }

        /// <summary>
        /// Gets or sets the milking count.
        /// </summary>
        /// <value>The milking count.</value>
        [XmlIgnore]
        public int MilkingCount { get; set; }

        /// <summary>
        /// Gets or sets the coulomb.
        /// </summary>
        /// <value>The coulomb.</value>
        [XmlAttribute]
        public Coulomb MilkingCoulomb { get; set; }

        /// <summary>
        /// Gets or sets the safe.
        /// </summary>
        /// <value>The safe.</value>
        [XmlIgnore]
        public Safe? Safe { get; set; }

        /// <summary>
        /// Gets or sets the type of the user.
        /// </summary>
        /// <value>The type of the user.</value>
        [XmlAttribute]
        public UserType UserType { get; set; }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </returns>
        public override string ToString()
        {

            var builder = new StringBuilder();
            builder.AppendLine(String.Format("User '{0}' level={1} UserID={2} ", UserName, Level, UserID));
            builder.AppendLine(String.Format("State '{0}' AverageBenefit={1} MilkingCount={2} ", State, AverageBenefit, MilkingCount));
            return builder.ToString();
        }
        /// <summary>
        /// Goes the rest.
        /// </summary>
        /// <param name="minutes">The minutes.</param>
        public void GoRest(int minutes)
        {
            State = CowState.Rest;
            ReadyAgain = DateTime.Now.AddMinutes(Randomizer.GetIntegerBetween(120, 180));
        }

        /// <summary>
        /// Goes the rest.
        /// </summary>
        public void GoRest()
        {
            //Standart sleep 2 hours. But need to fuzze fights statistics for antibot programs
            GoRest(Randomizer.GetIntegerBetween(120, 180));
        }
    }
}