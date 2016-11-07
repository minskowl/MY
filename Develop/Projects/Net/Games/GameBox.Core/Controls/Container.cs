using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nuclex.UserInterface.Controls;
using Nuclex.UserInterface.Visuals.Flat;

namespace GameBox.Core.Controls
{
    /// <summary>
    /// Container
    /// </summary>
    public class Container : Control
    {
    }

    /// <summary>
    /// 
    /// </summary>
    public class ContanerRenderer: IFlatControlRenderer<Container>
    {
        /// <summary>
        /// Renders the specified control.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <param name="graphics">The graphics.</param>
        public void Render(Container control, IFlatGuiGraphics graphics)
        {
            
        }
    }
}
