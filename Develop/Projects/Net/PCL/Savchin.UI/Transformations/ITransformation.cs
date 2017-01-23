using Savchin.UI.Shapes;

namespace Savchin.UI.Transformations
{
    interface ITransformation
    {
        void Transform();

        Matrix Matrix { get; set; }
    }
}
