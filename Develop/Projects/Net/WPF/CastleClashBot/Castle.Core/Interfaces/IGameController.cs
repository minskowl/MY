using System.Windows.Forms;

namespace Castle.Core
{
    public interface IGameController
    {
        void HandleMouseUp(MouseEventArgs evt);
        void HandleMouseDown(MouseEventArgs evt);
        void HandleMouseMove(MouseEventArgs evt);
    }
}