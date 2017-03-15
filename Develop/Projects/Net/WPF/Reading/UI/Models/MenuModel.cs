using System.Windows.Media.Imaging;
using Prodigy.Models.Core;
using Reading.Core;

namespace Prodigy.Models
{
    public class MenuModel : BaseModel
    {
        public BitmapSource Logo { get; private set; }
        public override string Title => "Обучайка";

        public MenuModel()
        {
            Logo = ResourceProvider.GetImage("Logo");
        }


    }
}
