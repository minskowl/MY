using Nuclex.UserInterface.Controls;
using Nuclex.UserInterface.Visuals.Flat;

namespace GameBox.Core.Controls
{
    public class SimpleSprite : Control
    {
        /// <summary>
        /// FrameName
        /// </summary>
        public string FrameName;

    }
    /// <summary>
    /// SimpleSpriteRenderer
    /// </summary>
    public class SimpleSpriteRenderer : IFlatControlRenderer<SimpleSprite>
    {
        /// <summary>
        /// Renders the specified control.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <param name="graphics">The graphics.</param>
        public void Render(SimpleSprite control, IFlatGuiGraphics graphics)
        {
            graphics.DrawElement(control.FrameName, control.GetAbsoluteBounds());
        }
    }
}
