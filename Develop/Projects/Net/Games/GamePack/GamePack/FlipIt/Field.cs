using System;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Xna.Framework;
using Nuclex.UserInterface;
using Nuclex.UserInterface.Controls;

namespace GamePack.FlipIt
{
    /// <summary>
    /// Field
    /// </summary>
    public class Field
    {
        public int Rows { get; private set; }
        public int Columns { get; private set; }
        private readonly Collection<Control> _screen;
        public Vector2 Position;

        private const int TileSize = 50;


        private Tile[,] _tiles;

        /// <summary>
        /// Gets the <see cref="Tile"/> with the specified row.
        /// </summary>
        /// <value></value>
        public Tile this[int row, int column]
        {
            get { return _tiles[row, column]; }
        }

        public bool IsWin
        {
            get
            {
                foreach (var tile in _tiles)
                {
                    if (tile != null && !tile.IsWright) return false;
                }
                return true;
            }
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Field"/> class.
        /// </summary>
        /// <param name="screen">The screen.</param>
        public Field(Screen screen)
        {

            _screen = screen.Desktop.Children;
        }

        /// <summary>
        /// Sets the tiles.
        /// </summary>
        /// <param name="isWright">if set to <c>true</c> [is wright].</param>
        public void SetTiles(bool isWright)
        {
            foreach (var tile in _tiles)
            {
                if (tile != null)
                    tile.IsWright = isWright;
            }
        }

        /// <summary>
        /// Toggles the tile.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="column">The column.</param>
        public void ToggleTile(int row, int column)
        {
            if (row < 0 || row >= Rows)
                throw new ArgumentOutOfRangeException("row");
            if (column < 0 || column >= Columns)
                throw new ArgumentOutOfRangeException("column");

            FlipTile(row, column);
            FlipTile(row - 1, column);
            FlipTile(row, column - 1);
            FlipTile(row + 1, column);
            FlipTile(row, column + 1);
        }

        private void FlipTile(int row, int column)
        {
            if (row < 0 || column < 0 || column >= Columns || row >= Rows) return;
            var tile = _tiles[row, column];
            if (tile != null) tile.Toggle();
        }

        /// <summary>
        /// Initialize the specified values.
        /// </summary>
        /// <param name="values">The values.</param>
        public void Initialize(int[][] values)
        {
            Rows = values.Length;
            Columns = values[0].Length;

            _tiles = new Tile[Rows, Columns];
            ClearTilesFromScreen();
            
            var row = 0;
            foreach (var columnsValues in values)
            {
                var column = 0;
                foreach (var value in columnsValues)
                {
                    if (value > 0)
                        AddTile(row, column, value == 2);
                    column++;
                }
                row++;
            }
        }

        private void ClearTilesFromScreen()
        {
            foreach (var tile in _screen.OfType<Tile>().ToArray())
            {
                _screen.Remove(tile);
            }
        }

        /// <summary>
        /// Adds the tile.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="column">The column.</param>
        /// <param name="isRight">if set to <c>true</c> [is right].</param>
        private void AddTile(int row, int column, bool isRight)
        {
            var tile = new Tile(row, column, this);
            tile.IsWright = isRight;
            tile.Bounds.Location.X = column * TileSize + Position.X;
            tile.Bounds.Location.Y = row * TileSize + Position.Y;
            tile.Bounds.Size.X = TileSize;
            tile.Bounds.Size.Y = TileSize;

            _screen.Add(tile);


            _tiles[row, column] = tile;
        }
    }
}
