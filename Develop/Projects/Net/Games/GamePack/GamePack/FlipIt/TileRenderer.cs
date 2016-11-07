using Nuclex.UserInterface.Visuals.Flat;

namespace GamePack.FlipIt
{
    /// <summary>
    /// TileRenderer
    /// </summary>
    public class TileRenderer : IFlatControlRenderer<Tile>
    {

        /// <summary>Names of the states the button control can be in</summary>
        /// <remarks>
        ///   Storing this as full strings instead of building them dynamically prevents
        ///   any garbage from forming during rendering.
        /// </remarks>
        private static readonly string[] States = new[] { "tile.wright", "tile.wrongth", };

        /// <summary>
        ///   Renders the specified control using the provided graphics interface
        /// </summary>
        /// <param name="control">Control that will be rendered</param>
        /// <param name="graphics">
        ///   Graphics interface that will be used to draw the control
        /// </param>
        public void Render(Tile control, IFlatGuiGraphics graphics)
        {

            // Determine the style to use for the button
            int stateIndex = control.IsWright ? 0 : 1;

            // Draw the button's frame
            graphics.DrawElement(States[stateIndex], control.GetAbsoluteBounds());


        }

    }

}
