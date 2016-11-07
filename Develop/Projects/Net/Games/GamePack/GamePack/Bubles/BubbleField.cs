using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Savchin.Utils;

namespace GamePack.Bubles
{
    internal class BubbleField : IEnumerable<Bubble>
    {

        #region Properties
        private readonly History _history = new History();
        private readonly FieldControl _control;
        private readonly Bubble[,] storage;

        private readonly int size;

        /// <summary>
        /// Gets or sets the max colors count.
        /// </summary>
        /// <value>The max colors count.</value>
        public int MaxColorsCount { get; set; }
        /// <summary>
        /// Gets the size.
        /// </summary>
        /// <value>The size.</value>
        public int Size
        {
            get { return size; }
        }
        /// <summary>
        /// Gets the selected count.
        /// </summary>
        /// <value>The selected count.</value>
        public int SelectedCount
        {
            get
            {
                var selectedCount = 0;
                foreach (Bubble bubble in this)
                {
                    if (bubble.Status == BubbleStatus.Selected)
                        selectedCount++;
                }
                return selectedCount;
            }
        }
        /// <summary>
        /// Gets the selected score.
        /// </summary>
        /// <value>The selected score.</value>
        public int SelectedScore
        {
            get
            {
                var count = SelectedCount;
                return count * (count - 1);
            }
        }
        /// <summary>
        /// Gets a value indicating whether this instance is end game.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is end game; otherwise, <c>false</c>.
        /// </value>
        public bool IsEndGame
        {
            get
            {
                for (int row = size - 1; row >= 0; row--)
                    for (int column = size - 1; column >= 0; column--)
                        if (CanSelect(row, column))
                            return false;

                return true;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance can undo.
        /// </summary>
        /// <value><c>true</c> if this instance can undo; otherwise, <c>false</c>.</value>
        public bool CanUndo
        {
            get { return _history.CanUndo; }
        }

        /// <summary>
        /// Gets a value indicating whether this instance can redo.
        /// </summary>
        /// <value><c>true</c> if this instance can redo; otherwise, <c>false</c>.</value>
        public bool CanRedo
        {
            get { return _history.CanRedo; }
        }

        /// <summary>
        /// Gets or sets the <see cref="Savchin.Bubbles.Controls.Bubble"/> with the specified row.
        /// </summary>
        /// <value></value>
        public Bubble this[int row, int column]
        {
            get { return storage[row, column]; }
            set
            {

                storage[row, column] = value;
                if(value!=null)
                {
                    value.SetLocation(row,column);
                }
            }
        }
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="BubbleField"/> class.
        /// </summary>
        /// <param name="size">The size.</param>
        /// <param name="control">The control.</param>
        public BubbleField(int size, FieldControl control)
        {
            _control = control;
            this.size = size;
            storage = new Bubble[size, size];
            MaxColorsCount = 6;
        }

        #region Interface
        /// <summary>
        /// News the game.
        /// </summary>
        public void NewGame()
        {
            Clear();
            GenerateField();
            _history.Clear();
            MakeSnapShot(0);
        }
        /// <summary>
        /// Adds the bubble.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="column">The column.</param>
        public void AddBubble(int row, int column)
        {
            AddBubble(row, column, GetRandomColor());
        }

        /// <summary>
        /// Adds the bubble.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="column">The column.</param>
        /// <param name="color">The color.</param>
        private void AddBubble(int row, int column, BubbleColor color)
        {
            var bubble = new Bubble(row, column, _control, color);
            storage[row, column] = bubble;
            _control.AddBubble(bubble);

        }

        /// <summary>
        /// Makes the snap shot.
        /// </summary>
        public void MakeSnapShot(int score)
        {
            var shot = new BubbleColor[size, size];
            for (var row = 0; row < size; row++)
                for (var column = 0; column < size; column++)
                {
                    var bubble = storage[row, column];

                    shot[row, column] = bubble == null ? BubbleColor.Empty : bubble.Color;
                }
            _history.AddSnapShot(new Snapshot { Score = score, Bubbles = shot });
        }

        /// <summary>
        /// Determines whether [is empty column] [the specified column].
        /// </summary>
        /// <param name="column">The column.</param>
        /// <returns>
        /// 	<c>true</c> if [is empty column] [the specified column]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsEmptyColumn(int column)
        {
            for (int row = 0; row < size; row++)
            {
                if (storage[row, column] != null)
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Determines whether this instance can select the specified row.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="column">The column.</param>
        /// <returns>
        /// 	<c>true</c> if this instance can select the specified row; otherwise, <c>false</c>.
        /// </returns>
        public bool CanSelect(int row, int column)
        {
            var bubble = storage[row, column];
            if (bubble == null)
                return false;
            var color = bubble.Color;

            if (row == 0 || storage[row - 1, column] == null || storage[row - 1, column].Color != color)
                if (row == Size - 1 || storage[row + 1, column] == null || storage[row + 1, column].Color != color)
                    if (column == 0 || storage[row, column - 1] == null || storage[row, column - 1].Color != color)
                        if (column == Size - 1 || storage[row, column + 1] == null || storage[row, column + 1].Color != color)
                            return false;

            return true;
        }
        /// <summary>
        /// Redoes this instance.
        /// </summary>
        /// <returns></returns>
        public int Redo()
        {
            var snapshot = _history.RedoStep();
            RestoreFromSnapshot(snapshot);
            return snapshot.Score;
        }
        /// <summary>
        /// Undoes this instance.
        /// </summary>
        /// <returns>Score</returns>
        public int Undo()
        {
            var snapshot = _history.UndoStep();
            RestoreFromSnapshot(snapshot);
            return snapshot.Score;
        }

        /// <summary>
        /// Selects the bubble.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="column">The column.</param>
        /// <param name="color">The color.</param>
        public void SelectBubble(int row, int column, BubbleColor color)
        {
            UnSelectAll();
            TrySelectBuble(row, column, color);
        }
        /// <summary>
        /// Moves the specified bubble.
        /// </summary>
        /// <param name="bubble">The bubble.</param>
        /// <param name="row">The row.</param>
        /// <param name="column">The column.</param>
        public void Move(Bubble bubble, int row, int column)
        {
            Debug.Assert(storage[row, column] == null, "Move not empty cell");

            this[bubble.Row, bubble.Column] = null;
            this[row , column] = bubble;
        }
        /// <summary>
        /// Determines whether this instance is valid.
        /// </summary>
        /// <returns></returns>
        public string IsValid()
        {
            var bub = new List<Bubble>();
            for (int column = 0; column < size; column++)
            {
                var findedNull = false;
                for (int row = size - 1; row >= 0; row--)
                {
                    var bubble = storage[row, column];
                    if (bubble == null)
                    {
                        findedNull = true;
                    }
                    else
                    {
                        if (findedNull)
                            return string.Format("Find hole row={0} column={1}", row, column);

                        if (bub.Contains(bubble))
                        {
                            return string.Format("Find duplicate row={0} column={1}", row, column);
                        }
                        else
                        {
                            bub.Add(bubble);
                        }
                    }

                }
            }
            return "true";
        }
        #endregion

        /// <summary>
        /// Generates the field.
        /// </summary>
        private void GenerateField()
        {
            for (var row = 0; row < size; row++)
                for (var column = 0; column < size; column++)
                {
                    AddBubble(row, column);
                }

        }

        private void RestoreFromSnapshot(Snapshot snapshot)
        {
            var colors = snapshot.Bubbles;

            Clear();
            for (var row = 0; row < size; row++)
                for (var column = 0; column < size; column++)
                {
                    var color = colors[row, column];
                    if (color != BubbleColor.Empty)
                        AddBubble(row, column, color);
                }
        }

        private void Clear()
        {
            _control.Clear();
        }

     

        private BubbleColor GetRandomColor()
        {
            return (BubbleColor)Randomizer.GetIntegerBetween(1, MaxColorsCount);
        }

        private void TrySelectBuble(int row, int column, BubbleColor color)
        {
            var bubble = storage[row, column];

            if (bubble == null || bubble.Status != BubbleStatus.Normal || bubble.Color != color)
                return;

            bubble.Status = BubbleStatus.Selected;

            if (column > 0)
            {
                TrySelectBuble(row, column - 1, color);
            }
            if (column < Size - 1)
            {
                TrySelectBuble(row, column + 1, color);
            }
            if (row > 0)
            {
                TrySelectBuble(row - 1, column, color);
            }
            if (row < Size - 1)
            {
                TrySelectBuble(row + 1, column, color);
            }

        }
        /// <summary>
        /// Uns the select all.
        /// </summary>
        private void UnSelectAll()
        {
            foreach (Bubble bubble in this)
            {
                if (bubble.Status != BubbleStatus.Normal)
                    bubble.Status = BubbleStatus.Normal;
            }
        }

        #region IEnumerable
        public IEnumerator<Bubble> GetEnumerator()
        {
            for (int row = 0; row < size; row++)
                for (int column = 0; column < size; column++)
                {
                    var bubble = storage[row, column];
                    if (bubble != null)
                        yield return bubble;
                }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        } 
        #endregion

  
    }
}
