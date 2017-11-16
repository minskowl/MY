using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Com.CodeGame.CodeWars2017.DevKit.CSharpCgdk.Model;

namespace Com.CodeGame.CodeWars2017.DevKit.CSharpCgdk
{
    public abstract class Action
    {
        public abstract void Do(World world, Move move, VehileCollection collection);

        public Action Next { get; set; }

        public bool CanAct(VehileCollection collection)
        {
            return true;
        }
    }

    public abstract class TypeAction : Action
    {
        protected readonly VehicleType Type;

        protected TypeAction(VehicleType type)
        {
            Type = type;
        }
    }

    public class ScaleGroup : TypeAction
    {
        public ScaleGroup(VehicleType type) : base(type)
        {
        }

        public override void Do(World world, Move move, VehileCollection collection)
        {
            move.Action = ActionType.Scale;
            move.Group = (int)Type;
            move.X = world.Width / 2;
            move.Y = world.Height / 2;
            move.Factor = 3;
        }
    }

    public class AssingGroup : TypeAction
    {
        public AssingGroup(VehicleType type) : base(type)
        {
        }

        public override void Do(World world, Move move, VehileCollection collection)
        {
           
                move.Action = ActionType.Assign;
                move.Group = (int)Type;
           
        }
    }
    public class MoveGroup : TypeAction
    {
   


        public MoveGroup(VehicleType type) : base(type)
        {
        }

        public override void Do(World world, Move move, VehileCollection collection)
        {

            move.Action = ActionType.Move;
            move.Group = (int)Type;
            move.X = world.Width / 2;
            move.Y = world.Height / 2;

        }
    }
    public class SelectUnit : TypeAction
    {
   


        public SelectUnit(VehicleType type) : base(type)
        {
        }

        public override void Do(World world, Move move, VehileCollection collection)
        {

            var rect = collection.Where(e => e.Type == Type).GetRect();

            move.Action = ActionType.ClearAndSelect;
            move.VehicleType = Type;
            move.X = rect.X;
            move.Y = rect.Y;
            move.Right = rect.Right;
            move.Bottom = rect.Bottom;

        }
    }

    public class VehileCollection : IEnumerable<Vec>
    {
        private readonly Dictionary<long, Vec> _storage;

        public VehileCollection(IEnumerable<Vehicle> vehicles)
        {
            _storage = vehicles.ToDictionary(e => e.Id, e => new Vec(e));
        }

        public void Add(IEnumerable<Vehicle> vehicles)
        {
            vehicles.ForEach(e => _storage[e.Id] = new Vec(e));
        }

        public void Update(VehicleUpdate[] updates)
        {
            foreach (var update in updates)
            {
                Vec v;
                if (_storage.TryGetValue(update.Id, out v))
                {
                    v.Update(update);
                }
            }
        }



        public IEnumerator<Vec> GetEnumerator()
        {
            return _storage.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public static class VecEx
    {
        public static Rectangle GetRect(this IEnumerable<Vec> vehicles)
        {
            if (!vehicles.Any())
                return Rectangle.Empty;

            double maxX = int.MinValue;
            double minX = int.MaxValue;
            double maxY = int.MinValue;
            double minY = int.MaxValue;
            vehicles.ForEach(e =>
            {
                maxX = Math.Max(maxX, e.X);
                minX = Math.Min(minX, e.X);
                maxY = Math.Max(maxY, e.Y);
                minY = Math.Min(minY, e.Y);
            });

            return new Rectangle((int)minX, (int)minY, (int)(maxX - minX), (int)(maxY - minY));
        }
    }

    public class Vec
    {

        public long Id { get; private set; }
        public double X { get; private set; }
        public double Y { get; private set; }
        public int[] Groups { get; private set; }
        public VehicleType Type { get; private set; }
        public Vec(Vehicle v)
        {
            Id = v.Id;
            X = v.X;
            Y = v.Y;
            Type = v.Type;
            Groups = v.Groups;
        }

        public Vec Update(VehicleUpdate u)
        {
            X = u.X;
            Y = u.Y;
            Groups = u.Groups;
            return this;
        }
    }

    public struct Size
    {
        public static readonly Size Empty;
        private int width;
        private int height;

        public bool IsEmpty
        {
            get
            {
                if (this.width == 0)
                    return this.height == 0;
                return false;
            }
        }

        public int Width
        {
            get
            {
                return this.width;
            }
            set
            {
                this.width = value;
            }
        }

        public int Height
        {
            get
            {
                return this.height;
            }
            set
            {
                this.height = value;
            }
        }

        public Size(Point pt)
        {
            this.width = pt.X;
            this.height = pt.Y;
        }

        public Size(int width, int height)
        {
            this.width = width;
            this.height = height;
        }

        //public static implicit operator SizeF(Size p)
        //{
        //    return new SizeF((float)p.Width, (float)p.Height);
        //}

        public static explicit operator Point(Size size)
        {
            return new Point(size.Width, size.Height);
        }

        public static Size operator +(Size sz1, Size sz2)
        {
            return Size.Add(sz1, sz2);
        }

        public static Size operator -(Size sz1, Size sz2)
        {
            return Size.Subtract(sz1, sz2);
        }

        public static bool operator ==(Size sz1, Size sz2)
        {
            if (sz1.Width == sz2.Width)
                return sz1.Height == sz2.Height;
            return false;
        }

        public static bool operator !=(Size sz1, Size sz2)
        {
            return !(sz1 == sz2);
        }

        public static Size Add(Size sz1, Size sz2)
        {
            return new Size(sz1.Width + sz2.Width, sz1.Height + sz2.Height);
        }

        //public static Size Ceiling(SizeF value)
        //{
        //    return new Size((int)Math.Ceiling((double)value.Width), (int)Math.Ceiling((double)value.Height));
        //}

        public static Size Subtract(Size sz1, Size sz2)
        {
            return new Size(sz1.Width - sz2.Width, sz1.Height - sz2.Height);
        }

        //public static Size Truncate(SizeF value)
        //{
        //    return new Size((int)value.Width, (int)value.Height);
        //}

        //public static Size Round(SizeF value)
        //{
        //    return new Size((int)Math.Round((double)value.Width), (int)Math.Round((double)value.Height));
        //}

        public override bool Equals(object obj)
        {
            if (!(obj is Size))
                return false;
            Size size = (Size)obj;
            if (size.width == this.width)
                return size.height == this.height;
            return false;
        }

        public override int GetHashCode()
        {
            return this.width ^ this.height;
        }

        public override string ToString()
        {
            return "{Width=" + this.width + ", Height=" + this.height + "}";
        }
    }

    public struct Point
    {
        public static readonly Point Empty;
        private int x;
        private int y;


        public bool IsEmpty
        {
            get
            {
                if (this.x == 0)
                    return this.y == 0;
                return false;
            }
        }

        public int X
        {
            get
            {
                return this.x;
            }
            set
            {
                this.x = value;
            }
        }

        public int Y
        {
            get
            {
                return this.y;
            }
            set
            {
                this.y = value;
            }
        }

        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public Point(Size sz)
        {
            this.x = sz.Width;
            this.y = sz.Height;
        }

        public Point(int dw)
        {
            this.x = (int)(short)Point.LOWORD(dw);
            this.y = (int)(short)Point.HIWORD(dw);
        }

        //public static implicit operator PointF(Point p)
        //{
        //    return new PointF((float)p.X, (float)p.Y);
        //}

        public static explicit operator Size(Point p)
        {
            return new Size(p.X, p.Y);
        }

        public static Point operator +(Point pt, Size sz)
        {
            return Point.Add(pt, sz);
        }

        public static Point operator -(Point pt, Size sz)
        {
            return Point.Subtract(pt, sz);
        }

        public static bool operator ==(Point left, Point right)
        {
            if (left.X == right.X)
                return left.Y == right.Y;
            return false;
        }

        public static bool operator !=(Point left, Point right)
        {
            return !(left == right);
        }

        public static Point Add(Point pt, Size sz)
        {
            return new Point(pt.X + sz.Width, pt.Y + sz.Height);
        }

        public static Point Subtract(Point pt, Size sz)
        {
            return new Point(pt.X - sz.Width, pt.Y - sz.Height);
        }

        //public static Point Ceiling(PointF value)
        //{
        //    return new Point((int)Math.Ceiling((double)value.X), (int)Math.Ceiling((double)value.Y));
        //}

        //public static Point Truncate(PointF value)
        //{
        //    return new Point((int)value.X, (int)value.Y);
        //}

        //public static Point Round(PointF value)
        //{
        //    return new Point((int)Math.Round((double)value.X), (int)Math.Round((double)value.Y));
        //}

        public override bool Equals(object obj)
        {
            if (!(obj is Point))
                return false;
            Point point = (Point)obj;
            if (point.X == this.X)
                return point.Y == this.Y;
            return false;
        }

        public override int GetHashCode()
        {
            return this.x ^ this.y;
        }

        public void Offset(int dx, int dy)
        {
            this.X = this.X + dx;
            this.Y = this.Y + dy;
        }

        public void Offset(Point p)
        {
            this.Offset(p.X, p.Y);
        }

        public override string ToString()
        {
            return "{X=" + this.X + ",Y=" + this.Y + "}";
        }

        private static int HIWORD(int n)
        {
            return n >> 16 & (int)ushort.MaxValue;
        }

        private static int LOWORD(int n)
        {
            return n & (int)ushort.MaxValue;
        }
    }

    [Serializable]
    public struct Rectangle
    {
        public static readonly Rectangle Empty;
        private int x;
        private int y;
        private int width;
        private int height;


        public Point Location
        {
            get
            {
                return new Point(this.X, this.Y);
            }
            set
            {
                this.X = value.X;
                this.Y = value.Y;
            }
        }


        public Size Size
        {
            get
            {
                return new Size(this.Width, this.Height);
            }
            set
            {
                this.Width = value.Width;
                this.Height = value.Height;
            }
        }

        public int X
        {
            get
            {
                return this.x;
            }
            set
            {
                this.x = value;
            }
        }

        public int Y
        {
            get
            {
                return this.y;
            }
            set
            {
                this.y = value;
            }
        }

        public int Width
        {
            get
            {
                return this.width;
            }
            set
            {
                this.width = value;
            }
        }

        public int Height
        {
            get
            {
                return this.height;
            }
            set
            {
                this.height = value;
            }
        }


        public int Left
        {
            get
            {
                return this.X;
            }
        }


        public int Top
        {
            get
            {
                return this.Y;
            }
        }


        public int Right
        {
            get
            {
                return this.X + this.Width;
            }
        }


        public int Bottom
        {
            get
            {
                return this.Y + this.Height;
            }
        }


        public bool IsEmpty
        {
            get
            {
                if (this.height == 0 && this.width == 0 && this.x == 0)
                    return this.y == 0;
                return false;
            }
        }

        public Rectangle(int x, int y, int width, int height)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
        }

        public Rectangle(Point location, Size size)
        {
            this.x = location.X;
            this.y = location.Y;
            this.width = size.Width;
            this.height = size.Height;
        }

        public static bool operator ==(Rectangle left, Rectangle right)
        {
            if (left.X == right.X && left.Y == right.Y && left.Width == right.Width)
                return left.Height == right.Height;
            return false;
        }

        public static bool operator !=(Rectangle left, Rectangle right)
        {
            return !(left == right);
        }

        public static Rectangle FromLTRB(int left, int top, int right, int bottom)
        {
            return new Rectangle(left, top, right - left, bottom - top);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Rectangle))
                return false;
            Rectangle rectangle = (Rectangle)obj;
            if (rectangle.X == this.X && rectangle.Y == this.Y && rectangle.Width == this.Width)
                return rectangle.Height == this.Height;
            return false;
        }

        //public static Rectangle Ceiling(RectangleF value)
        //{
        //    return new Rectangle((int)Math.Ceiling((double)value.X), (int)Math.Ceiling((double)value.Y), (int)Math.Ceiling((double)value.Width), (int)Math.Ceiling((double)value.Height));
        //}

        //public static Rectangle Truncate(RectangleF value)
        //{
        //    return new Rectangle((int)value.X, (int)value.Y, (int)value.Width, (int)value.Height);
        //}

        //public static Rectangle Round(RectangleF value)
        //{
        //    return new Rectangle((int)Math.Round((double)value.X), (int)Math.Round((double)value.Y), (int)Math.Round((double)value.Width), (int)Math.Round((double)value.Height));
        //}

        public bool Contains(int x, int y)
        {
            if (this.X <= x && x < this.X + this.Width && this.Y <= y)
                return y < this.Y + this.Height;
            return false;
        }

        public bool Contains(Point pt)
        {
            return this.Contains(pt.X, pt.Y);
        }

        public bool Contains(Rectangle rect)
        {
            if (this.X <= rect.X && rect.X + rect.Width <= this.X + this.Width && this.Y <= rect.Y)
                return rect.Y + rect.Height <= this.Y + this.Height;
            return false;
        }

        public override int GetHashCode()
        {
            return this.X ^ (this.Y << 13 | (int)((uint)this.Y >> 19)) ^ (this.Width << 26 | (int)((uint)this.Width >> 6)) ^ (this.Height << 7 | (int)((uint)this.Height >> 25));
        }

        public void Inflate(int width, int height)
        {
            this.X = this.X - width;
            this.Y = this.Y - height;
            this.Width = this.Width + 2 * width;
            this.Height = this.Height + 2 * height;
        }

        public void Inflate(Size size)
        {
            this.Inflate(size.Width, size.Height);
        }

        public static Rectangle Inflate(Rectangle rect, int x, int y)
        {
            Rectangle rectangle = rect;
            rectangle.Inflate(x, y);
            return rectangle;
        }

        public void Intersect(Rectangle rect)
        {
            Rectangle rectangle = Rectangle.Intersect(rect, this);
            this.X = rectangle.X;
            this.Y = rectangle.Y;
            this.Width = rectangle.Width;
            this.Height = rectangle.Height;
        }

        public static Rectangle Intersect(Rectangle a, Rectangle b)
        {
            int x = Math.Max(a.X, b.X);
            int num1 = Math.Min(a.X + a.Width, b.X + b.Width);
            int y = Math.Max(a.Y, b.Y);
            int num2 = Math.Min(a.Y + a.Height, b.Y + b.Height);
            if (num1 >= x && num2 >= y)
                return new Rectangle(x, y, num1 - x, num2 - y);
            return Rectangle.Empty;
        }

        public bool IntersectsWith(Rectangle rect)
        {
            if (rect.X < this.X + this.Width && this.X < rect.X + rect.Width && rect.Y < this.Y + this.Height)
                return this.Y < rect.Y + rect.Height;
            return false;
        }

        public static Rectangle Union(Rectangle a, Rectangle b)
        {
            int x = Math.Min(a.X, b.X);
            int num1 = Math.Max(a.X + a.Width, b.X + b.Width);
            int y = Math.Min(a.Y, b.Y);
            int num2 = Math.Max(a.Y + a.Height, b.Y + b.Height);
            return new Rectangle(x, y, num1 - x, num2 - y);
        }

        public void Offset(Point pos)
        {
            this.Offset(pos.X, pos.Y);
        }

        public void Offset(int x, int y)
        {
            this.X = this.X + x;
            this.Y = this.Y + y;
        }

        public override string ToString()
        {
            return "{X=" + this.X + ",Y=" + this.Y + ",Width=" + this.Width + ",Height=" + this.Height + "}";
        }
    }

}
