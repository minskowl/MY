using Nuclex.UserInterface.Controls;

namespace GamePack.FlipIt
{

    public class Tile : PressableControl
    {
        private int _row;
        private int _column;
        private Field _field;
        /// <summary>
        /// Gets or sets a value indicating whether this instance is wright.
        /// </summary>
        /// <value><c>true</c> if this instance is wright; otherwise, <c>false</c>.</value>
        public bool IsWright { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Tile"/> class.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="column">The column.</param>
        /// <param name="field">The field.</param>
        public Tile(int row, int column,Field field)
        {
            _row = row;
            _column = column;
            _field = field;

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
            _field.ToggleTile(_row, _column);
        }
        /// <summary>
        /// Toggles this instance.
        /// </summary>
        public void Toggle()
        {
            IsWright = !IsWright;
        }


    }
}
