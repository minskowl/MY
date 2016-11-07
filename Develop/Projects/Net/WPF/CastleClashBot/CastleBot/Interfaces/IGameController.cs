using System.Windows.Forms;

namespace Castle.Core
{
    public interface IGameController
    {
        void HandleMouseUp(MouseEvent evt);
        void HandleMouseDown(MouseEvent evt);
        void HandleMouseMove(MouseEvent evt);
    }
}