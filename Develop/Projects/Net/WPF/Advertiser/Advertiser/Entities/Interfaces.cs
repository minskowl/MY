using System.ComponentModel;

namespace Advertiser.Entities
{
    public interface IEntity : INotifyPropertyChanged
    {
        int Id { get; set; }
    }

}