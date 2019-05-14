using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using AoM.Viewer.Data;
using Savchin.Collection.Generic;
using Savchin.Wpf.Core;
using Savchin.Wpf.Input;

namespace AoM.Viewer.ViewModels
{
    public class LocationViewModel : ObjectBase
    {


        private LocationType _type;

        /// <summary>
        /// Gets or sets the Type.
        /// </summary>
        /// <value>The Type.</value>
        public LocationType Type
        {
            get { return _type; }
            set { Set(ref _type, value, nameof(Name)); }
        }


        private int _act;

        /// <summary>
        /// Gets or sets the Act.
        /// </summary>
        /// <value>The Act.</value>
        public int Act
        {
            get { return _act; }
            set { Set(ref _act, value, nameof(Name)); }
        }



        private int _part;

        /// <summary>
        /// Gets or sets the Part.
        /// </summary>
        /// <value>The Part.</value>
        public int Part
        {
            get { return _part; }
            set { Set(ref _part, value, nameof(Name)); }
        }

        private Location Data;


        public string Name => Data.ToString();

        public ObservableCollectionEx<Craft> Crafts { get; }
        public ICommand AddCraftCommand { get; }
        public LocationViewModel(Location data)
        {
            AddCraftCommand = new DelegateCommand(OnAddCraftCommand);
            Data = data;
            Part = Data.Part;
            Act = Data.Act;
            Type = Data.Type;
            
            if(data.Crafts==null)
                data.Crafts= new List<Craft>();
            Crafts= new ObservableCollectionEx<Craft>(data.Crafts);
        }

        private void OnAddCraftCommand()
        {
            Crafts.Add(new Craft());
        }

        public Location GetData()
        {
            Data.Type = Type;
            Data.Act = Act;
            Data.Part = Part;

            Data.Crafts.Clear();
            Data.Crafts.AddRange(Crafts);
            return Data;
        }

        public override string ToString()
        {
            return Data.ToString();
        }
    }
}
