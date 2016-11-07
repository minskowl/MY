using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Nuclex.UserInterface;
using Nuclex.UserInterface.Controls;
using Nuclex.UserInterface.Controls.Desktop;

namespace GameBox.Core
{
    public static class Gui
    {
        #region Button

        /// <summary>
        /// Adds the button.
        /// </summary>
        /// <param name="screen">The screen.</param>
        /// <param name="text">The text.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns></returns>
        public static ButtonControl AddButton(this Screen screen, string text, UniScalar x, UniScalar y)
        {
            var r = CreateButton(text, x, y);
            screen.Desktop.Children.Add(r);
            return r;
        }
        /// <summary>
        /// Adds the button.
        /// </summary>
        /// <param name="screen">The screen.</param>
        /// <param name="text">The text.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns></returns>
        public static ButtonControl AddButton(this Collection<Control> screen, string text, UniScalar x, UniScalar y)
        {
            var r = CreateButton(text, x, y);
            screen.Add(r);
            return r;
        }
        /// <summary>
        /// Creates the button.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns></returns>
        public static ButtonControl CreateButton(string text, UniScalar x, UniScalar y)
        {
            return new ButtonControl
            {
                Bounds = { Size = new UniVector(120, 35), Location = new UniVector(x, y) },
                Text = text
            };

        }
        
        #endregion

        /// <summary>
        /// Adds the label.
        /// </summary>
        /// <param name="screen">The screen.</param>
        /// <param name="text">The text.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns></returns>
        public static LabelControl AddLabel(this Collection<Control> screen, string text, UniScalar x, UniScalar y)
        {
            var r = CreateLabel(text, x, y);
            screen.Add(r);
            return r;
        }
        public static LabelControl AddLabel(this Screen screen, string text, UniScalar x, UniScalar y)
        {
            var r = CreateLabel(text, x, y);
            screen.Desktop.Children.Add(r);
            return r;
        }
        /// <summary>
        /// Creates the label.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns></returns>
        public static LabelControl CreateLabel(string text, UniScalar x, UniScalar y)
        {
            return new LabelControl
            {
                Bounds = { Location = new UniVector(x, y) },
                Text = text
            };
        }

        /// <summary>
        /// Adds the radio.
        /// </summary>
        /// <param name="screen">The screen.</param>
        /// <param name="text">The text.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns></returns>
        public static ChoiceControl AddRadio(this Collection<Control> screen, string text, UniScalar x, UniScalar y)
        {
            var r = CreateRadio(text, x, y);
            screen.Add(r);
            return r;
        }
        /// <summary>
        /// Adds the radio.
        /// </summary>
        /// <param name="screen">The screen.</param>
        /// <param name="text">The text.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns></returns>
        public static ChoiceControl AddRadio(this Screen screen, string text, UniScalar x, UniScalar y)
        {
            var r = CreateRadio(text, x, y);
            screen.Desktop.Children.Add(r);
            return r;
        }
        /// <summary>
        /// Creates the radio.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns></returns>
        public static ChoiceControl CreateRadio(string text, UniScalar x, UniScalar y)
        {
            return new ChoiceControl
            {
                Bounds = { 
                    Location = new UniVector(x, y) ,
                    Size = new UniVector(120, 13),
                },
                Text = text
            };
        }
    }
}
