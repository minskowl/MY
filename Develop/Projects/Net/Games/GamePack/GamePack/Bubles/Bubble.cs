using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nuclex.UserInterface;
using Nuclex.UserInterface.Controls;

namespace GamePack.Bubles
{
    public enum BubbleColor : int
    {
        Empty = 0,
        Red = 1,
        Green = 2,
        Blue = 3,
        Yellow = 4,
        Violet = 5,
    }
    public enum BubbleStatus
    {
        Selected,
        Killed,
        Normal
    }

    internal class Bubble : PressableControl
    {
        #region Properties
        /// <summary>
        /// Size
        /// </summary>
        public const int Size = 40;
        private int _row;
        private int _column;
        private FieldControl _field;
        /// <summary>
        /// Gets or sets the color.
        /// </summary>
        /// <value>The color.</value>
        public BubbleColor Color { get; private set; }
        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>The status.</value>
        public BubbleStatus Status { get; set; }

        /// <summary>
        /// Gets the row.
        /// </summary>
        /// <value>The row.</value>
        public int Row
        {
            get { return _row; }
            set
            {
                _row = value;
                SetLocation();
            }
        }

        /// <summary>
        /// Gets the column.
        /// </summary>
        /// <value>The column.</value>
        public int Column
        {
            get { return _column; }
            set
            {
                _column = value;
                SetLocation();
            }
        }



        #endregion

        public event EventHandler Clicked;



        /// <summary>
        /// Initializes a new instance of the <see cref="Bubble"/> class.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="column">The column.</param>
        /// <param name="field">The field.</param>
        /// <param name="color">The color.</param>
        internal Bubble(int row, int column, FieldControl field, BubbleColor color)
        {

            _field = field;
            Color = color;
            Status = BubbleStatus.Normal;
            _row = row;
            _column = column;
            SetLocation();
            Bounds.Size= new UniVector(Size,Size);
        }
        /// <summary>
        /// Sets the location.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="column">The column.</param>
        public void SetLocation(int row, int column)
        {
            _row = row;
            _column = column;
            SetLocation();
        }

        /// <summary>
        /// Called when the control is pressed
        /// </summary>
        /// <remarks>
        /// If you were to implement a button, for example, you could trigger a 'Pressed'
        /// event here are call a user-provided delegate, depending on your design.
        /// </remarks>
        protected override void OnPressed()
        {
            base.OnPressed();
            InvokeClicked();
        }
        private void InvokeClicked()
        {
            EventHandler handler = Clicked;
            if (handler != null) handler(this, EventArgs.Empty);
        }
        private void SetLocation()
        {
            Bounds.Location = new UniVector(_column * Size, _row * Size);
        }
    }
}
