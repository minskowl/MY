using System.Windows;

namespace Savchin.Wpf.Controls.Windows
{
    public class WindowStateBack
    {
        public double Width { get; set; }
        public double Height { get; set; }
        public double Left { get; set; }
        public double Top { get; set; }
        public WindowState State { get; set; }

        /// <summary>
        /// Restores the specified window.
        /// </summary>
        /// <param name="window">The window.</param>
        public void Restore(Window window)
        {
            window.Width = Width;
            window.Height = Height;
            window.Left = Left;
            window.Top = Top;
            window.WindowState = State;
        }

        /// <summary>
        /// Copies the specified window.
        /// </summary>
        /// <param name="window">The window.</param>
        public void Copy(Window window)
        {
            Width = window.Width;
            Height = window.Height;
            Left = window.Left;
            Top = window.Top;
            State = window.WindowState;
        }
    }
}
