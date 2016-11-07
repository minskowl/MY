using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using BotvaSpider.Core;
using BotvaSpider.Gears;
using WatiN.Core;
using WatiN.Core.Interfaces;


namespace BotvaSpider.Data
{


    /// <summary>
    /// User
    /// </summary>
    public class User : IComparable<User>
    {

        private static readonly string numberDecimalSeparator = Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator;
        /// <summary>
        /// Gets or sets the race.
        /// </summary>
        /// <value>The race.</value>
        [DisplayName("Расса")]
        public Race Race { get; set; }
        /// <summary>
        /// Gets or sets the guild.
        /// </summary>
        /// <value>The guild.</value>
        [DisplayName("Гильдия")]
        public GuildType Guild { get; set; }
        /// <summary>
        /// Gets or sets the user ID.
        /// </summary>
        /// <value>The user ID.</value>
        [DisplayName("Идентификатор")]
        public int UserID { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        [DisplayName("Ник")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the type of the user.
        /// </summary>
        /// <value>The type of the user.</value>
        [DisplayName("Тип")]
        public UserType UserType { get; set; }
        /// <summary>
        /// Gets or sets the clan ID.
        /// </summary>
        /// <value>The clan ID.</value>
        [Browsable(false)]
        public int ClanID { get; set; }
        /// <summary>
        /// Gets or sets the clan.
        /// </summary>
        /// <value>The clan.</value>
        [Browsable(false)]
        public Clan Clan { get; set; }

        /// <summary>
        /// Gets or sets the level.
        /// </summary>
        /// <value>The level.</value>
        [DisplayName("Уровень")]
        public int Level { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="User"/> is female.
        /// </summary>
        /// <value><c>true</c> if female; otherwise, <c>false</c>.</value>
        public bool Female { get; set; }


        /// <summary>
        /// Gets or sets the recruitment.
        /// </summary>
        /// <value>The recruitment.</value>
        [DisplayName("Вербовка")]
        public string Recruitment { get; set; }

        /// <summary>
        /// Gets or sets the fights.
        /// </summary>
        /// <value>The fights.</value>
        [DisplayName("Боев")]
        public int Fights { get; set; }

        /// <summary>
        /// Gets or sets the victories.
        /// </summary>
        /// <value>The victories.</value>
        [DisplayName("Побед")]
        public int Victories { get; set; }

        /// <summary>
        /// Gets or sets the stealing.
        /// </summary>
        /// <value>The stealing.</value>
        [DisplayName("Награблено")]
        public int Stealing { get; set; }

        /// <summary>
        /// Gets or sets the lose.
        /// </summary>
        /// <value>The lose.</value>
        [DisplayName("Утрачено")]
        public int Lose { get; set; }
        /// <summary>
        /// Gets or sets the glory.
        /// </summary>
        /// <value>The glory.</value>
        [DisplayName("Урон")]
        public int Injury { get; set; }

        /// <summary>
        /// Gets or sets the updated.
        /// </summary>
        /// <value>The updated.</value>
        [DisplayName("Дата обновления")]
        public DateTime Updated { get; set; }

        /// <summary>
        /// Gets or sets the safe.
        /// </summary>
        /// <value>The safe.</value>
        [DisplayName("Сейф")]
        public Safe? Safe { get; set; }

        /// <summary>
        /// Gets or sets the milking coulomb.
        /// </summary>
        /// <value>The milking coulomb.</value>
        [DisplayName("Кулон для дойки")]
        public Coulomb MilkingCoulomb { get; set; }

        private readonly SkillCollection skills = new SkillCollection();
        /// <summary>
        /// Gets the skills.
        /// </summary>
        /// <value>The skills.</value>
        [DisplayName("Статы")]
        public SkillCollection Skills
        {
            get { return skills; }
        }


        /// <summary>
        /// Compares the current object with another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// A 32-bit signed integer that indicates the relative order of the objects being compared. The return value has the following meanings:
        /// Value
        /// Meaning
        /// Less than zero
        /// This object is less than the <paramref name="other"/> parameter.
        /// Zero
        /// This object is equal to <paramref name="other"/>.
        /// Greater than zero
        /// This object is greater than <paramref name="other"/>.
        /// </returns>
        public int CompareTo(User other)
        {
            foreach (Skill skill in skills)
            {
                if (skill.SkilType == SkillType.Weight || skill.SkilType == SkillType.Glory) continue;

                if (skill.Points < other.Skills[skill.SkilType].Points)
                    return -1;

            }
            return 1;
        }

        /// <summary>
        /// Gets the skill difference.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns></returns>
        public double GetSkillDifference(User other)
        {
            double dif = 0;
            foreach (Skill skill in skills)
            {
                if (skill.SkilType == SkillType.Weight || skill.SkilType == SkillType.Glory) continue;
                var skillDelta = skill.Points - other.Skills[skill.SkilType].Points;
                dif += (skill.SkilType == SkillType.Power || skill.SkilType == SkillType.Mastery) ? skillDelta * 1.5 : skillDelta;
            }
            return dif;
        }


        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </returns>
        public override string ToString()
        {

            var builder = new StringBuilder();
            builder.AppendLine(String.Format("User '{0}' level={1} ", Name, Level));
            foreach (Skill skill in skills)
            {
                if (skill.AdditionalPoints != 0)
                    builder.AppendLine(String.Format("'{0}' level={1} {2} ", skill.SkilType, skill.Points, skill.AdditionalPoints));
                else
                    builder.AppendLine(String.Format("'{0}' level={1} ", skill.SkilType, skill.Points));


            }

            return builder.ToString();
        }


        /// <summary>
        /// Fills the skils.
        /// </summary>
        /// <param name="browser">The browser.</param>
        public virtual void FillSkils(IE browser)
        {
            var skillsTable = browser.Table(Find.ByClass("skills"));

            Level = int.Parse(skillsTable.TableRows[1].TableCells[2].Text);
            //FIXED
            FillSkils(skillsTable, 1, 3);
        }

        /// <summary>
        /// Fills the skils.
        /// </summary>
        /// <param name="table">The skills.</param>
        /// <param name="startRow">The start row.</param>
        /// <param name="column">The skill column.</param>
        public void FillSkils(Table table, int startRow, int column)
        {

            var text = table.TableRows[startRow + 1].TableCells[column].Text;
            SetSkill(SkillType.Power, text);

            text = table.TableRows[startRow + 2].TableCells[column].Text;
            SetSkill(SkillType.Protection, text);

            text = table.TableRows[startRow + 3].TableCells[column].Text;
            SetSkill(SkillType.Dexterity, text);

            text = table.TableRows[startRow + 4].TableCells[column].Text;
            SetSkill(SkillType.Weight, text);

            text = table.TableRows[startRow + 5].TableCells[column].Text;
            SetSkill(SkillType.Mastery, text);

            text = table.TableRows[startRow + 6].TableCells[2].Text.Trim();
            if (text.Contains(" "))
            {
                text = text.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)[0];
            }
            SetSkill(SkillType.Glory, text);
        }

        #region Skills


        private void SetSkill(SkillType type, string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                skills.SetSkill(type, 0);
                return;
            }
            text = text.Trim();
            if (string.IsNullOrEmpty(text))
            {
                skills.SetSkill(type, 0);
            }
            else if (text.Contains("+"))
            {
                var parts = text.Split(new char[] { '+' });
                skills.SetSkill(type, GetInteger(parts[0]), GetInteger(parts[1]));
            }
            else
            {
                skills.SetSkill(type, GetInteger(text));
            }
        }

        private double GetInteger(string text)
        {
            var validIntegerString = (numberDecimalSeparator == ".") ?
                                                                         text : text.Replace(".", numberDecimalSeparator);

            try
            {
                try
                {
                    return double.Parse(validIntegerString);
                }
                catch (FormatException)
                {
                    return double.Parse(text.Replace('.', ','));
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException(string.Format("Ошибка перевода строки '{0}' в число", text), ex);
            }

        }

        #endregion

        public static User CreateFormUserPage(IE browser)
        {
            var result = new User();

            var table = browser.Table(Find.ByClass("attack playerInfo"));
            if(!table.Exists) return null;

            result.SetRace(browser);

            result.Name = table.Div(Find.ByClass("blockTitle ")).Text;
            // result.Clan = Clan.Create(table.Div(Find.ByClass("blockTitle2 nobold")).Text);
            result.UpdateFormUserPage(browser);
            return result;
        }
        public void SetRace(IE browser)
        {
            var divAvatar = browser.Div(Find.ByClass("avatar "));
            if (divAvatar.Exists)
            {
                if (divAvatar.Images.Count > 0)
                {
                    var imgUrl = divAvatar.Images[0].Src;
                    AppCore.LogSystem.Debug("parse user", imgUrl);
                    var builder = new UriBuilder(imgUrl);
                    var fileName = Path.GetFileName(builder.Path);
                    Race = fileName.StartsWith("2") ? Race.Sheeps : Race.Pigs;
                }
                else if (divAvatar.Divs.Count > 0)
                {
                    var text = divAvatar.Divs[0].GetAttributeValue("style");
                    text = text.Substring(text.LastIndexOf('/') + 1);

                    Race = text.StartsWith("0") ? Race.Sheeps : Race.Pigs;
                }
                else
                {
                    AppCore.LogSystem.Warn("неизвестный тип аватар", divAvatar.OuterHtml);
                }
            }
        }

        /// <summary>
        /// Updates the form user page.
        /// </summary>
        /// <param name="browser">The browser.</param>
        public void UpdateFormUserPage(IE browser)
        {
            var skillTable = browser.Table(Find.ByClass("skills"));

            Level = int.Parse(skillTable.TableRows[1].TableCells[2].Text);
            FillSkils(skillTable, 1, 3);
            FillStatistics(browser.Table(Find.ByClass("stats")));
        }



        private void FillStatistics(Table table)
        {
            //this.Recruitment = int.Parse(skillTable.TableRows[4].TableCells[6].Text);
            Fights = int.Parse(table.TableRows[3].TableCells[1].Text);
            Victories = int.Parse(table.TableRows[4].TableCells[1].Text);
            Stealing = int.Parse(table.TableRows[5].TableCells[1].Text);
            Lose = int.Parse(table.TableRows[6].TableCells[1].Text);
            Injury = int.Parse(table.TableRows[6].TableCells[1].Text);
        }
        static readonly string[] delimenter = new[] { Environment.NewLine };

        /// <summary>
        /// Parses the user name list.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        public static List<string> ParseUserNameList(string text)
        {

            var lines = text.Split(delimenter, StringSplitOptions.RemoveEmptyEntries);
            var result = new List<string>();

            var reg = new System.Text.RegularExpressions.Regex(@"(?<name>.*)?\s+\[\d+\]\s+.*?");
            foreach (var line in lines)
            {
                var match = reg.Match(line.Trim());
                var userName = (match.Success) ? match.Groups["name"].Value.Trim() : line.Trim();
                if (!result.Contains(userName)) result.Add(userName);
            }
            return result;
        }
    }
}