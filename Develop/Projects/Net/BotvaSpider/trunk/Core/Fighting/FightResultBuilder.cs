using System;
using System.Linq;
using System.Text.RegularExpressions;
using BotvaSpider.Data;
using WatiN.Core;

namespace BotvaSpider.Core
{
    class FightResultBuilder
    {

        private static readonly Regex regMoney = new Regex(@"((?<value>[\d\.]+)(?=\s*<IMG\sclass=png\salt=золото\s))", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private static readonly Regex regCristal = new Regex(@"((?<value>[\d\.]+)(?=\s*<IMG\sclass=png\salt=кристалл\s))", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private static readonly Regex regExp = new Regex(@"((?<value>\d+)(?=\s*<IMG\sclass=png\salt=опыта))", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        private IE browser;
        private GameController _controller;
        /// <summary>
        /// Initializes a new instance of the <see cref="FightResultBuilder"/> class.
        /// </summary>
        /// <param name="controller">The controller.</param>
        public FightResultBuilder(GameController controller)
        {
            _controller = controller;
            this.browser = controller.Browser;
        }

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns></returns>
        public FightResult Build()
        {
            var result = new FightResult();

            result.Rival = CreateRival();

            var table = GetResultTable();
            result.FightUrl = browser.Url;
            result.Date = ParseTime(table.TableRows[0].TableCells[0].Text);
            result.Date = result.Date.AddHours(-1);

            var winnerRow = FindWinnerRow(table);
            if (winnerRow < 0) return result;

            var winner = ParseWinner(table.TableRows[winnerRow].TableCells[1].Text);
            result.Win = result.Rival.Name != winner;
            ParseWinnigs(result, table.TableRows[winnerRow + 1].TableCells[1].InnerHtml);

            result.RivalInjuryHealth = int.Parse(table.TableRows[2].TableCells[2].Text);
            result.RivalHealth = int.Parse(table.TableRows[3].TableCells[3].Text);
            result.Rival.UserType = result.Win ? UserType.Cow : UserType.Fighter;
            
            return result;
        }

        private int FindWinnerRow(Table table)
        {
            for (var i = 5; i < table.TableRows.Count; i++)
            {
                var tmp = table.TableRows[i].Text;
                if (!string.IsNullOrEmpty(tmp) && tmp.Contains("Победитель"))
                    return i;
            }
            return -1;
        }

        private Table GetResultTable()
        {
            foreach (Table table in browser.Tables)
            {
                if (table.TableRows.Count == 0) continue;
                if (table.TableRows[0].TableCells.Count == 0) continue;
                if (string.IsNullOrEmpty(table.TableRows[0].TableCells[0].Text)) continue;
                if (table.TableRows[0].TableCells[0].Text.StartsWith("Ход боя"))
                    return table;
            }
            return null;

        }

        private Rival CreateRival()
        {
            var mainTable = FindRivalTable();
            var statusTable = ((Table)mainTable.NextSibling);

            var result = new Rival();
            result.Name = mainTable.TableRows[0].ToString();

            result.Safe = GetSafe(mainTable);
            var valueCell = 2;
            result.Level = int.Parse(statusTable.TableRows[1].TableCells[valueCell].Text);
            //FIXED
            result.FillSkils(statusTable, 1, valueCell);

            return result;
        }

        private Safe? GetSafe(Table table)
        {
            var imageSafe = table.Images.Where(e =>
                                             !string.IsNullOrEmpty(e.Src) &&
                                             e.Src.StartsWith(_controller.UrlImages + "ico_m1_") &&
                                             e.Src.EndsWith(".jpg")).FirstOrDefault();
            if (imageSafe == null || !imageSafe.Exists) return null;

            var code = imageSafe.Src.Substring(imageSafe.Src.Length - 5, 1);
            switch (code)
            {
                case "0":
                    return Safe.None;
                case "1":
                    return Safe.Money;
                case "3":
                    return Safe.Crystal;
                case "5":
                    return Safe.Crystal | Safe.Money;
                default:
                    return null;

            }
        }

        private Table FindRivalTable()
        {
            var statusTables = browser.Tables.Filter(Find.ByClass("playerStats"));
            foreach (Table statusTable in statusTables)
            {
                var mainTable = ((Table)statusTable.PreviousSibling);
                if (mainTable.GetAttributeValue("width") == "206" &&
                    mainTable.GetAttributeValue("height") == "250" &&
                    mainTable.TableRows[0].ToString() != AppCore.AcountSettings.UserName)
                    return mainTable;
            }
            return null;
        }


        /// <summary>
        /// Parses the winner.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        private string ParseWinner(string text)
        {
     
            return text.Substring(10);
        }

        /// <summary>
        /// Parses the winnigs.
        /// </summary>
        /// <param name="result">The result.</param>
        /// <param name="html">The HTML.</param>
        private void ParseWinnigs(FightResult result, string html)
        {
            if (string.IsNullOrEmpty(html)) return;

            var money = GetWinnigsValue(html, regMoney);
            var exp = GetWinnigsValue(html, regExp);
            var cristal = GetWinnigsValue(html, regCristal);
            var sign = result.Win ? 1 : -1;
            result.Money = money * sign;
            result.Expirience = exp * sign;
            result.Crystals = cristal * sign;
        }

        private int GetWinnigsValue(string html, Regex mather)
        {
            var match = mather.Match(html);
            return match.Groups["value"].Success ? Integer.Parse(match.Groups["value"].Value) : 0;
        }

        private DateTime ParseTime(string text)
        {
            var parts = text.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var tmp = parts[3];
            tmp = tmp.Substring(1, tmp.Length - 2);
            tmp = tmp + " " + parts[2];
            return DateTime.Parse(tmp);
        }
    }
}
