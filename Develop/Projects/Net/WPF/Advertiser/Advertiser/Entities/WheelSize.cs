using System;

namespace Advertiser.Entities
{
    public class WheelSize : ObjectBase, IComparable<WheelSize>, IComparable
    {
        //public int Radius { get; set; }
        private int _radius;
        public int Radius
        {
            get { return _radius; }
            set
            {
                if (value == _radius) return;
                _radius = value;
                OnPropertyChanged("Radius");
            }
        }
        private int _height;
        public int Height
        {
            get { return _height; }
            set
            {
                if (value == _height) return;
                _height = value;
                OnPropertyChanged("Height");
            }
        }

        private int _width;
        public int Width
        {
            get { return _width; }
            set
            {
                if (value == _width) return;
                _width = value;
                OnPropertyChanged("Width");
            }
        }

        public int CompareTo(object obj)
        {
            return CompareTo((WheelSize)obj);
        }

        public int CompareTo(WheelSize other)
        {
            var res = _radius.CompareTo(other.Radius);
            if (res != 0) return res;
            res = _width.CompareTo(other.Width);
            if (res != 0) return res;
            return _height.CompareTo(other.Height);
        }

        public override string ToString()
        {
            return string.Format("R{0} {1}/{2}", Radius, Width, Height);
        }


    }
}
