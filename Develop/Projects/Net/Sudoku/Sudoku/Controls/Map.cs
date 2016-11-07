using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Savchin.Collection.Generic;

namespace Savchin.Sudoku.Controls
{
    public partial class Map : UserControl
    {
        #region Properties
        /// <summary>
        /// Gets the count.
        /// </summary>
        /// <value>The count.</value>
        public int Count
        {
            get { return layoutPanel.RowCount; }
        }

        /// <summary>
        /// Gets a value indicating whether this instance has variants.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance has variants; otherwise, <c>false</c>.
        /// </value>
        public bool HasVariants
        {
            get
            {
                return GetFields().Any(e => e.isVariant);
            }
        }
        /// <summary>
        /// Gets or sets the difficulty.
        /// </summary>
        /// <value>The difficulty.</value>
        public Difficulty Difficulty
        {
            get { return (Difficulty)(layoutPanel.RowCount); }
            set { SetDifficulty(value); }
        }



        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="Map"/> class.
        /// </summary>
        public Map()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Gets the snapshot.
        /// </summary>
        /// <returns></returns>
        public Game GetSnapshot()
        {
            var result = new Game
                             {
                                 Difficulty = Difficulty,
                                 Values = new int[Count][]
                             };

            for (var row = 0; row < Count; row++)
            {
                result.Values[row] = new int[Count];
                for (var column = 0; column < Count; column++)
                {
                    var field = (Field)
                    layoutPanel.GetControlFromPosition(column, row);
                    result.Values[row][column] = field.Value;
                }
            }
            return result;
        }
        /// <summary>
        /// Loads from snapshot.
        /// </summary>
        /// <param name="game">The game.</param>
        public void LoadFromSnapshot(Game game)
        {
            Difficulty = game.Difficulty;
            for (var row = 0; row < Count; row++)
                for (var column = 0; column < Count; column++)
                {
                    var field = (Field)
                    layoutPanel.GetControlFromPosition(column, row);
                    field.Value = game.Values[row][column];
                }
        }
        /// <summary>
        /// Clears the avaliliblity.
        /// </summary>
        public void ClearAvaliliblity()
        {
            SetColor(Color.Empty);
        }

        /// <summary>
        /// Validates the values.
        /// </summary>
        public void ValidateValues()
        {
            ClearAvaliliblity();
            for (var row = 0; row < Count; row++)
                for (var column = 0; column < Count; column++)
                {
                    var field = (Field)layoutPanel.GetControlFromPosition(column, row);
                    if (field.Value > 0)
                    {
                        CheckRow(row, field);
                        CheckColumn(column, field);
                        CheckRegion(column, row, field);
                    }
                }
        }

        /// <summary>
        /// Clears the values.
        /// </summary>
        public void ClearValues()
        {
            foreach (var field in GetFields())
                field.Value = 0;
            ClearAvaliliblity();
        }

        /// <summary>
        /// Shows the avalilible.
        /// </summary>
        /// <param name="p">The p.</param>
        public void ShowAvalilible(int p)
        {
            SetColor(Color.Green);

            for (var row = 0; row < Count; row++)
                for (var column = 0; column < Count; column++)
                {
                    var field = (Field)layoutPanel.GetControlFromPosition(column, row);
                    if (field.Value == p)
                    {
                        DisableColumn(column);
                        DisableRow(row);
                        DisableRegion(column, row);
                    }
                    if (field.Value > 0)
                    {
                        field.BackColor = Color.Empty;
                    }
                }
        }
        /// <summary>
        /// Autoes the find.
        /// </summary>
        public void AutoFind()
        {
            for (var value = 1; value <= (int)Difficulty; value++)
            {
                ShowAvalilible(value);
                if (!HasVariants) continue;

                if (FindSingle(value)) return;

            }
        }

        #region Disable
        private void DisableRegion(int column, int row)
        {
            new Region(Difficulty, column, row).GetFields(layoutPanel).ForEach(Clear);
        }

        private void DisableRow(int row)
        {
            GetRowFields(row).ForEach(Clear);
        }

        private void DisableColumn(int column)
        {
            GetColumnFields(column).ForEach(Clear);
        }
        #endregion

        #region Check
        private void CheckRegion(int column, int row, Field field)
        {
            var region = new Region(Difficulty, column, row);
            foreach (var position in region.Positions)
            {
                var control = (Field)layoutPanel.GetControlFromPosition(position.Column, position.Row);
                if (field != control && field.Value == control.Value)
                    control.BackColor = Color.Red;
            }
        }

        private void CheckColumn(int column, Field field)
        {
            foreach (var control in GetColumnFields(column))
            {
                if (field != control && field.Value == control.Value)
                    control.BackColor = Color.Red;
            }
        }

        private void CheckRow(int row, Field field)
        {
            foreach (var control in GetRowFields(row))
            {
                if (field != control && field.Value == control.Value)
                    control.BackColor = Color.Red;
            }
        }

        #endregion

        private void SetDifficulty(Difficulty value)
        {
            var size = (int)value;
            layoutPanel.ColumnStyles.Clear();
            layoutPanel.RowStyles.Clear();
            layoutPanel.Controls.Clear();

            layoutPanel.RowCount = size;
            layoutPanel.ColumnCount = size + 1;
            for (var i = 0; i < size; i++)
            {
                layoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 40F));
                layoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            }
            for (var row = 0; row < size; row++)
                for (var column = 0; column < size; column++)
                {

                    var control = new Field
                                      {
                                          Name = ("F" + row + "_" + column),
                                          Difficulty = value
                                      };
                    layoutPanel.Controls.Add(control, column, row);
                }
        }

        private bool FindSingle(int value)
        {
            bool result = false;
            for (var row = 0; row < Count; row++)
                for (var column = 0; column < Count; column++)
                {
                    var field = (Field)layoutPanel.GetControlFromPosition(column, row);
                    if (!field.isVariant) continue;


                    if (
                        GetRowFields(row).Count(e => e.isVariant) == 1 ||
                        GetColumnFields(column).Count(e => e.isVariant) == 1 ||
                        new Region(Difficulty, column, row).Count(layoutPanel, e => e.isVariant) == 1
                        )
                    {
                        field.Value = value;
                        result = true;
                    }

                }
            foreach (var field in GetFields().Where(e => e.isVariant))
            {
                if (field.Value == -1)
                {
                    field.BackColor = Color.Empty;
                }
            }

            return result;
        }



        private IEnumerable<Field> GetFields()
        {
            for (var row = 0; row < Count; row++)
                for (var column = 0; column < Count; column++)
                {
                    yield return (Field)layoutPanel.GetControlFromPosition(column, row);
                }
        }

        private IEnumerable<Field> GetRowFields(int row)
        {
            for (var column = 0; column < Count; column++)
                yield return (Field)layoutPanel.GetControlFromPosition(column, row);
        }

        private IEnumerable<Field> GetColumnFields(int column)
        {
            for (var row = 0; row < Count; row++)
                yield return (Field)layoutPanel.GetControlFromPosition(column, row);
        }

        private static void Clear(Field field)
        {
            field.BackColor = Color.Empty;
        }

        private void SetColor(Color color)
        {
            foreach (var field in GetFields())
            {
                field.BackColor = color;
            }
        }
    }
}
