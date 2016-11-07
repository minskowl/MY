using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nuclex.UserInterface.Visuals.Flat;

namespace GamePack.Bubles
{
    public class BubleRenderer : IFlatControlRenderer<Bubble>
    {
        private static readonly string[] States = 
            new[]{"buble.Empty","buble.Red","buble.Green",
                "buble.Blue","buble.Yellow","buble.Violet",
               "buble.Empty.selected","buble.Red.selected","buble.Green.selected",
                "buble.Blue.selected","buble.Yellow.selected","buble.Violet.selected"};

        /// <summary>
        /// Renders the specified control.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <param name="graphics">The graphics.</param>
         void IFlatControlRenderer<Bubble>.Render(Bubble control, IFlatGuiGraphics graphics)
        {
            // Draw the button's frame
            var index = control.Status == BubbleStatus.Selected ? 
                (int) control.Color + 6 : (int) control.Color;

            graphics.DrawElement(States[index], control.GetAbsoluteBounds());

        }
    }
}
