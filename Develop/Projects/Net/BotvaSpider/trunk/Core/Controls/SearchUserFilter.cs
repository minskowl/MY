using System;
using System.Windows.Forms;
using BotvaSpider.Core;
using Savchin.Core;
using Savchin.Forms.Helpers;

namespace BotvaSpider.Controls
{
    public partial class SearchUserFilter : Form
    {
        #region Properties
        /// <summary>
        /// Gets the page range.
        /// </summary>
        /// <value>The page range.</value>
        public IRange<int> PageRange
        {
            get { return filterPage.Checked ? filterPage.GetRange() : null; }
        }
        /// <summary>
        /// Gets the level range.
        /// </summary>
        /// <value>The level range.</value>
        public IRange<int> LevelRange
        {
            get { return filterLevel.Checked ? filterLevel.GetRange() : null; }
        }

        /// <summary>
        /// Gets the second value.
        /// </summary>
        /// <value>The second value.</value>
        public Enum SecondValue
        {
            get { return listSecond.GetValue(); }
        }
        /// <summary>
        /// Gets the race.
        /// </summary>
        /// <value>The race.</value>
        public Race Race
        {
            get { return (Race)listRace.GetValue(); }
        }
        /// <summary>
        /// Gets a value indicating whether this <see cref="SearchUserFilter"/> is import.
        /// </summary>
        /// <value><c>true</c> if import; otherwise, <c>false</c>.</value>
        public bool Import
        {
            get { return boxImport.Checked; }
        }


        /// <summary>
        /// Gets the skill difference.
        /// </summary>
        /// <value>The skill difference.</value>
        public int SkillDifference
        {
            get { return (int)boxSkillDifference.Value; }
        }
        #endregion
        public enum DisplayMode
        {
            Guild,
            TopSearch
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchUserFilter"/> class.
        /// </summary>
        public SearchUserFilter()
        {
            InitializeComponent();



            listRace.Setup(typeof(Race), Race.All);
            listRace.SelectedIndex = 0;

            filterPage.ShowRange(new Range<int>(1, 10));
            filterLevel.ShowRange(new Range<int>(25, 26));
        }

        /// <summary>
        /// Shows the dialog.
        /// </summary>
        /// <param name="mode">The mode.</param>
        /// <returns></returns>
        public DialogResult ShowDialog(DisplayMode mode)
        {
            switch (mode)
            {
                case DisplayMode.Guild:
                    listSecond.Setup(typeof(GuildType), GuildType.None);
                    listSecond.SelectedIndex = 0;
                    labelSecondList.Text = "Гильдия";
                    break;
                case DisplayMode.TopSearch:
                    listSecond.Setup(typeof(TopSearchSort));
                    listSecond.SelectedIndex = 0;
                    labelSecondList.Text = "Кого";
                    break;
                default:
                    throw new ArgumentOutOfRangeException("mode");
            }
            return ShowDialog();
        }

        /// <summary>
        /// Handles the Click event of the buttonOK control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void buttonOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }


    }
}
