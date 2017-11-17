using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Com.CodeGame.CodeWars2017.DevKit.CSharpCgdk.Model;

namespace Com.CodeGame.CodeWars2017.DevKit.CSharpCgdk
{

    public abstract class Action
    {
        private ISituation _situation;
        protected Move Move => _situation.Move;
        protected World World => _situation.World;
        protected VehileCollection Vechiles => _situation.Vehiles;

        public Action Next { get; set; }

        public void Do(ISituation situation)
        {
            _situation = situation;
            DoImpl();
        }

        protected abstract void DoImpl();

        public Predicate<ISituation> Can { get; set; }

        public virtual bool CanAct(ISituation situation)
        {
            _situation = situation;
            return Can?.Invoke(_situation) ?? true;
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

        protected override void DoImpl()
        {
            Move.Action = ActionType.Scale;
            Move.Group = (int)Type;
            Move.X = World.Width / 2;
            Move.Y = World.Height / 2;
            Move.Factor = 3;
        }
    }

    public class AssingGroup : TypeAction
    {
        public AssingGroup(VehicleType type) : base(type)
        {
        }

        protected override void DoImpl()
        {
            Move.Action = ActionType.Assign;
            Move.Group = (int)Type;
        }
    }

    public class MoveGroup : TypeAction
    {
        public MoveGroup(VehicleType type) : base(type)
        {
        }

        protected override void DoImpl()
        {
            Move.Action = ActionType.Move;
            Move.Group = (int)Type;
            Move.X = World.Width / 2;
            Move.Y = World.Height / 2;
        }
    }

    public class SelectUnit : TypeAction
    {
        public SelectUnit(VehicleType type) : base(type)
        {
        }

        protected override void DoImpl()
        {
            var rect = Vechiles.Where(e => e.Type == Type).GetRect();

            Move.Action = ActionType.ClearAndSelect;
            Move.VehicleType = Type;
            Move.X = rect.X;
            Move.Y = rect.Y;
            Move.Right = rect.Right;
            Move.Bottom = rect.Bottom;
        }
    }

    public class VehileCollection : IEnumerable<Vec>
    {
        private readonly Dictionary<long, Vec> _storage;

        public VehileCollection()
        {
            _storage = new Dictionary<long, Vec>();
        }

        public VehileCollection(IEnumerable<Vehicle> vehicles)
        {
            _storage = vehicles.ToDictionary(e => e.Id, e => new Vec(e));
        }


        public IEnumerator<Vec> GetEnumerator()
        {
            return _storage.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(IEnumerable<Vehicle> vehicles)
        {
            vehicles.ForEach(e => _storage[e.Id] = new Vec(e));
        }

        public void Update(VehicleUpdate[] updates)
        {
            if (updates.IsNotEmpty())
                foreach (var update in updates)
                {
                    Vec v;
                    if (_storage.TryGetValue(update.Id, out v))
                        v.Update(update);
                }
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
        public Vec(Vehicle v)
        {
            Id = v.Id;
            X = v.X;
            Y = v.Y;
            Type = v.Type;
            Groups = v.Groups;
        }

        public long Id { get; }
        public double X { get; private set; }
        public double Y { get; private set; }
        public int[] Groups { get; private set; }
        public VehicleType Type { get; }

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

        public bool IsEmpty
        {
            get
            {
                if (Width == 0)
                    return Height == 0;
                return false;
            }
        }

        public int Width { get; set; }

        public int Height { get; set; }

        public Size(Point pt)
        {
            Width = pt.X;
            Height = pt.Y;
        }

        public Size(int width, int height)
        {
            Width = width;
            Height = height;
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
            return Add(sz1, sz2);
        }

        public static Size operator -(Size sz1, Size sz2)
        {
            return Subtract(sz1, sz2);
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
            var size = (Size)obj;
            if (size.Width == Width)
                return size.Height == Height;
            return false;
        }

        public override int GetHashCode()
        {
            return Width ^ Height;
        }

        public override string ToString()
        {
            return "{Width=" + Width + ", Height=" + Height + "}";
        }
    }

    public struct Point
    {
        public static readonly Point Empty;


        public bool IsEmpty
        {
            get
            {
                if (X == 0)
                    return Y == 0;
                return false;
            }
        }

        public int X { get; set; }

        public int Y { get; set; }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Point(Size sz)
        {
            X = sz.Width;
            Y = sz.Height;
        }

        public Point(int dw)
        {
            X = (short)LOWORD(dw);
            Y = (short)HIWORD(dw);
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
            return Add(pt, sz);
        }

        public static Point operator -(Point pt, Size sz)
        {
            return Subtract(pt, sz);
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
            var point = (Point)obj;
            if (point.X == X)
                return point.Y == Y;
            return false;
        }

        public override int GetHashCode()
        {
            return X ^ Y;
        }

        public void Offset(int dx, int dy)
        {
            X = X + dx;
            Y = Y + dy;
        }

        public void Offset(Point p)
        {
            Offset(p.X, p.Y);
        }

        public override string ToString()
        {
            return "{X=" + X + ",Y=" + Y + "}";
        }

        private static int HIWORD(int n)
        {
            return (n >> 16) & ushort.MaxValue;
        }

        private static int LOWORD(int n)
        {
            return n & ushort.MaxValue;
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
            get => new Point(X, Y);
            set
            {
                X = value.X;
                Y = value.Y;
            }
        }


        public Size Size
        {
            get => new Size(Width, Height);
            set
            {
                Width = value.Width;
                Height = value.Height;
            }
        }

        public int X
        {
            get => x;
            set => x = value;
        }

        public int Y
        {
            get => y;
            set => y = value;
        }

        public int Width
        {
            get => width;
            set => width = value;
        }

        public int Height
        {
            get => height;
            set => height = value;
        }


        public int Left => X;


        public int Top => Y;


        public int Right => X + Width;


        public int Bottom => Y + Height;


        public bool IsEmpty
        {
            get
            {
                if (height == 0 && width == 0 && x == 0)
                    return y == 0;
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
            x = location.X;
            y = location.Y;
            width = size.Width;
            height = size.Height;
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
            var rectangle = (Rectangle)obj;
            if (rectangle.X == X && rectangle.Y == Y && rectangle.Width == Width)
                return rectangle.Height == Height;
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
            if (X <= x && x < X + Width && Y <= y)
                return y < Y + Height;
            return false;
        }

        public bool Contains(Point pt)
        {
            return Contains(pt.X, pt.Y);
        }

        public bool Contains(Rectangle rect)
        {
            if (X <= rect.X && rect.X + rect.Width <= X + Width && Y <= rect.Y)
                return rect.Y + rect.Height <= Y + Height;
            return false;
        }

        public override int GetHashCode()
        {
            return X ^ ((Y << 13) | (int)((uint)Y >> 19)) ^ ((Width << 26) | (int)((uint)Width >> 6)) ^
                   ((Height << 7) | (int)((uint)Height >> 25));
        }

        public void Inflate(int width, int height)
        {
            X = X - width;
            Y = Y - height;
            Width = Width + 2 * width;
            Height = Height + 2 * height;
        }

        public void Inflate(Size size)
        {
            Inflate(size.Width, size.Height);
        }

        public static Rectangle Inflate(Rectangle rect, int x, int y)
        {
            var rectangle = rect;
            rectangle.Inflate(x, y);
            return rectangle;
        }

        public void Intersect(Rectangle rect)
        {
            var rectangle = Intersect(rect, this);
            X = rectangle.X;
            Y = rectangle.Y;
            Width = rectangle.Width;
            Height = rectangle.Height;
        }

        public static Rectangle Intersect(Rectangle a, Rectangle b)
        {
            var x = Math.Max(a.X, b.X);
            var num1 = Math.Min(a.X + a.Width, b.X + b.Width);
            var y = Math.Max(a.Y, b.Y);
            var num2 = Math.Min(a.Y + a.Height, b.Y + b.Height);
            if (num1 >= x && num2 >= y)
                return new Rectangle(x, y, num1 - x, num2 - y);
            return Empty;
        }

        public bool IntersectsWith(Rectangle rect)
        {
            if (rect.X < X + Width && X < rect.X + rect.Width && rect.Y < Y + Height)
                return Y < rect.Y + rect.Height;
            return false;
        }

        public static Rectangle Union(Rectangle a, Rectangle b)
        {
            var x = Math.Min(a.X, b.X);
            var num1 = Math.Max(a.X + a.Width, b.X + b.Width);
            var y = Math.Min(a.Y, b.Y);
            var num2 = Math.Max(a.Y + a.Height, b.Y + b.Height);
            return new Rectangle(x, y, num1 - x, num2 - y);
        }

        public void Offset(Point pos)
        {
            Offset(pos.X, pos.Y);
        }

        public void Offset(int x, int y)
        {
            X = X + x;
            Y = Y + y;
        }

        public override string ToString()
        {
            return "{X=" + X + ",Y=" + Y + ",Width=" + Width + ",Height=" + Height + "}";
        }
    }
}