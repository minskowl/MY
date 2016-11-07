using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using BotvaSpider.Core;
using BotvaSpider.Gears;

namespace BotvaSpider.Automation.Mining
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class SearchCristalResult : CristalBenefit, IEquatable<SearchCristalResult>
    {
        /// <summary>
        /// Gets or sets the statistics ID.
        /// </summary>
        /// <value>The statistics ID.</value>
        public int StatisticsID { get; set; }
        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        /// <value>The date.</value>
        [DisplayName("Дата")]
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the percentage.
        /// </summary>
        /// <value>The percentage.</value>
        [DisplayName("Вероятость успеха")]
        public byte Percentage { get; set; }

        [DisplayName("Пытались добыть")]
        public bool DoAttempt { get; set; }

        /// <summary>
        /// Gets or sets the coulomb.
        /// </summary>
        /// <value>The coulomb.</value>
        [DisplayName("Кулон")]
        public Coulomb Coulomb { get; set; }
        /// <summary>
        /// Gets or sets the coulomb works.
        /// </summary>
        /// <value>The coulomb works.</value>
        [DisplayName("Сколько раз сработал кулон.")]
        public byte CoulombWorks { get; set; }

        /// <summary>
        /// Gets or sets the level.
        /// </summary>
        /// <value>The level.</value>
        [DisplayName("Уровень")]
        public int Level { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [miner gear].
        /// </summary>
        /// <value><c>true</c> if [miner gear]; otherwise, <c>false</c>.</value>
        [DisplayName("Амуниция")]
        public MinerGear MinerGear { get; set; }

        /// <summary>
        /// Gets or sets the spirit.
        /// </summary>
        /// <value>The spirit.</value>
        [DisplayName("Заговоры")]
        public SpiritType Spirit { get; set; }

        /// <summary>
        /// Parses the result.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        public void ParseResult(string message)
        {
            var sentences = message.Split(new char[] { '.', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var sentence in sentences)
            {
                var s = sentence.Trim();
                switch (s)
                {
                    case "Неудачная попытка":
                    case "Может, слабо копал":
                    case "Какой ты молодец":
                        break;
                    case "Ты успешно добыл кристалл":
                        Cristals = 1;
                        break;
                    case "Сработал ваш кулон":
                        CoulombWorks++;
                        break;
                    case "Ты нашёл билет на большую поляну":
                        BigTicket = true;
                        break;
                    case "Ты нашёл билет на маленькую поляну":
                        SmallTicket = true;
                        break;
                    default:
                        if (s.StartsWith("Вы добыли"))
                        {
                            var parts = sentence.Split(new[] { ' ' });
                            Cristals = byte.Parse(parts[2]);
                        }
                        else
                        {
                            AppCore.LogMine.Warn(
                                string.Format("Неизвестное предложение в результате '{0}'", s),
                                message);
                        }
                        break;
                }
            }

        }



        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
        /// </returns>
        public bool Equals(SearchCristalResult other)
        {
            if (other.BigTicket != BigTicket) return false;
            if (other.Coulomb != Coulomb) return false;
            if (other.CoulombWorks != CoulombWorks) return false;
            if (other.Cristals != Cristals) return false;
            if (other.Date != Date) return false;
            if (other.Level != Level) return false;
            if (other.MinerGear != MinerGear) return false;
            if (other.Percentage != Percentage) return false;
            if (other.SmallTicket != SmallTicket) return false;
            if (other.Spirit != Spirit) return false;
            return true;
        }
    }
}
