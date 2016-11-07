using System.Windows.Media.Imaging;
using Reading.Core;

namespace Reading.Models
{
    public class MenuModel : BaseModel
    {
        public BitmapSource Logo { get; private set; }
        public override string Title
        {
            get { return "Обучайка"; }
        }

        public MenuModel()
        {
            Logo = ResourceProvider.GetImage("Logo");
        }


    }
}
