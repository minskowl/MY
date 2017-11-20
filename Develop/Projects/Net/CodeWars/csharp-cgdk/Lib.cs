using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using Com.CodeGame.CodeWars2017.DevKit.CSharpCgdk.Model;

namespace Com.CodeGame.CodeWars2017.DevKit.CSharpCgdk
{
    #region Actions



    public class FirstCommand : DeployCommand
    {
        protected override void DoImpl()
        {
            Strategy.StartMatrix.BuilMatrix(Vehiles);

            base.DoImpl();
        }

    }

    public class DeployCommand : SelectUnitCommand
    {
        protected override void DoImpl()
        {
            var gr = Strategy.StartMatrix.GetFreeGroup();

            if (gr == null)
                return;

            Type = gr.Type;
            base.DoImpl();

    

            Next = new AssingGroup(Type);
            {
                Act = (s3) =>
                {
                    var c = new MoveGroup(Type)
                    {
                        Act = s =>
                        {
                            var rect = Vehiles.GetGroupRect(Type);

                            s.Move.X = (s.World.Width / 2) - rect.Right;
                            s.Move.Y = (s.World.Height / 2) - rect.Bottom;

                            var scale = new ScaleGroup(VehicleType.Fighter)
                            {
                                Can = e =>
                                {
                                    var r = Vehiles.GetGroupRect(Type);
                                    var minY = World.Height / 5;
                                    e.Log.Log("Wait Scale Type {0} {1} minY {2}", Type, r, minY);
                                    return r.Y >= minY;

                                },
                                Act = (s1) =>
                                {
                                    s1.Move.Factor = 5;
                                    var r = Vehiles.GetGroupRect(Type);
                                    s1.Move.X = r.X;
                                    s1.Move.Y = r.Y;
                                }
                            };
                            s.Commands.Add(scale);
                            s.Commands.Add(new DeployCommand());
                        }
                    };
                    s3.Commands.Add(c);
                };
            }; 


        }




    }



    public abstract class Command
    {
        private ISituation _situation;
        protected Move Move => _situation.Move;
        protected World World => _situation.World;
        protected VehileCollection Vehiles => _situation.Vehiles;

        protected MyStrategy Strategy
        {
            get { return (MyStrategy)_situation; }
        }
        public Command Next { get; set; }

        protected abstract ActionType ActionType { get; }

        public void Do(ISituation situation)
        {
            _situation = situation;
            Move.Action = ActionType;
            DoImpl();
            Act?.Invoke(_situation);
        }

        protected abstract void DoImpl();

        public Predicate<ISituation> Can { get; set; }

        public Action<ISituation> Act { get; set; }

        public virtual bool CanAct(ISituation situation)
        {
            _situation = situation;
            return Can?.Invoke(_situation) ?? true;
        }
    }

    public abstract class TypeCommand : Command
    {
        public VehicleType Type;

        protected TypeCommand()
        {

        }

        protected TypeCommand(VehicleType type)
        {
            Type = type;
        }

        protected override void DoImpl()
        {
            Move.Group = (int)Type + 1;
        }

        protected RectangleF GetVehileRect()
        {
            return Vehiles.Where(e => e.Type == Type).GetRect();
        }
        protected RectangleF GetGroupRect()
        {
            return Vehiles.Where(e => e.IsInGroup((int)Type)).GetRect();
        }
    }

    public class ScaleGroup : TypeCommand
    {
        protected override ActionType ActionType => ActionType.Scale;

        public ScaleGroup(VehicleType type) : base(type)
        {
        }


    }

    public class AssingGroup : TypeCommand
    {
        protected override ActionType ActionType => ActionType.Assign;
        public AssingGroup() { }

        public AssingGroup(VehicleType type) : base(type)
        {
        }

    }

    public class MoveGroup : TypeCommand
    {
        protected override ActionType ActionType => ActionType.Move;
        public MoveGroup(VehicleType type) : base(type)
        {
        }

    }

    public class SelectUnitCommand : TypeCommand
    {
        protected override ActionType ActionType => ActionType.ClearAndSelect;
        public SelectUnitCommand() { }
        public SelectUnitCommand(VehicleType type) : base(type)
        {
        }

        protected override void DoImpl()
        {
            var rect = GetVehileRect();
            Move.VehicleType = Type;
            Move.X = rect.X;
            Move.Y = rect.Y;
            Move.Right = rect.Right;
            Move.Bottom = rect.Bottom;
        }
    }

    #endregion

    public class StartMatrix
    {
        private readonly VecGroup[,] _storage = new VecGroup[3, 3];

        public VecGroup GetFreeGroup()
        {
            for (int i = 2; i >= 0; i--)
            {
                for (int j = 2; j >= 0; j--)
                {
                    var gr = _storage[i, j];
                    if (gr != null)
                    {
                        _storage[i, j] = null;
                        return gr;
                    }

                }
            }
            return null;
        }

        public void BuilMatrix(VehileCollection Vehiles)
        {
            var groups = Vehiles.GroupBy(e => e.Type).Select(e => new VecGroup(e.GetRect(), e.Key)).ToArray();
            var maxY = groups.Max(e => e.Rect.Bottom);
            var maxX = groups.Max(e => e.Rect.Right);

            var minY = groups.Min(e => e.Rect.Bottom);
            var minX = groups.Min(e => e.Rect.Right);
            foreach (var vecGroup in groups)
            {
                int x = GetIndex(vecGroup.Rect.Right, minX, maxX);
                int y = GetIndex(vecGroup.Rect.Bottom, minY, maxY);
                _storage[y, x] = vecGroup;
            }
        }
        private int GetIndex(float cur, float min, float max)
        {
            if (cur == min)
                return 0;
            if (cur == max)
                return 2;

            return 1;
        }
    }

    public class VehileCollection : IEnumerable<Vec>
    {
        private readonly Dictionary<long, Vec> _storage = new Dictionary<long, Vec>();



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

        public RectangleF GetVehileRect(VehicleType type)
        {
            return this.Where(e => e.Type == type).GetRect();
        }
        public RectangleF GetGroupRect(VehicleType group)
        {
            return GetGroupRect(group.ToInt());
        }
        public RectangleF GetGroupRect(int group)
        {
            return this.Where(e => e.IsInGroup(group)).GetRect();
        }
    }

    public static class VecEx
    {
        public static int ToInt(this VehicleType type)
        {
            return (int)type + 1;
        }

        public static RectangleF GetRect(this IEnumerable<Vec> vehicles)
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

            return new RectangleF((float)minX, (float)minY, (float)(maxX - minX), (float)(maxY - minY));
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

        public bool IsInGroup(VehicleType type)
        {
            return IsInGroup(type.ToInt());
        }

        public bool IsInGroup(int group)
        {
            return Groups.IsNotEmpty() && Groups.Contains(group);
        }

        public Vec Update(VehicleUpdate u)
        {
            X = u.X;
            Y = u.Y;
            Groups = u.Groups;
            return this;
        }
    }

    public interface ILog : IDisposable
    {
        void Log(string text);
        void Log(string text, params object[] args);

        void Log(Move move);
    }

    public class NullLogger : ILog
    {
        public void Dispose()
        { }

        public void Log(string text)
        { }

        public void Log(string text, params object[] args)
        { }

        public void Log(Move move)
        { }
    }

    class FileLogger : ILog
    {
        private const string FileName = "Log.txt";

        private readonly StreamWriter _stream;
        public FileLogger()
        {
            if (File.Exists(FileName))
                File.Delete(FileName);
            _stream = new StreamWriter(FileName);

        }

        public void Log(string text)
        {
            //  Console.WriteLine(text);
            _stream.WriteLine(text);
        }

        public void Log(string text, params object[] args)
        {
            //Console.WriteLine(text, args);
            _stream.WriteLine(text, args);
        }

        public void Log(Move move)
        {
            if (move.Action == null) return;
            var vt = move.VehicleType ?? (VehicleType)(move.Group - 1);

            Log("Action {0} Group {1} ({2},{3})-({4},{5}) ", move.Action, vt, move.X, move.Y, move.Right, move.Bottom);
        }

        public void Dispose()
        {
            _stream.Dispose();
        }
    }

    #region Geometry




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

        public static implicit operator SizeF(Size p)
        {
            return new SizeF((float)p.Width, (float)p.Height);
        }

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

        public static Size Ceiling(SizeF value)
        {
            return new Size((int)Math.Ceiling((double)value.Width), (int)Math.Ceiling((double)value.Height));
        }

        public static Size Subtract(Size sz1, Size sz2)
        {
            return new Size(sz1.Width - sz2.Width, sz1.Height - sz2.Height);
        }

        public static Size Truncate(SizeF value)
        {
            return new Size((int)value.Width, (int)value.Height);
        }

        public static Size Round(SizeF value)
        {
            return new Size((int)Math.Round((double)value.Width), (int)Math.Round((double)value.Height));
        }

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

    [ComVisible(true)]
    [Serializable]
    public struct SizeF
    {
        public static readonly SizeF Empty;
        private float width;
        private float height;


        public bool IsEmpty
        {
            get
            {
                if ((double)this.width == 0.0)
                    return (double)this.height == 0.0;
                return false;
            }
        }

        public float Width
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

        public float Height
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

        public SizeF(SizeF size)
        {
            this.width = size.width;
            this.height = size.height;
        }

        public SizeF(PointF pt)
        {
            this.width = pt.X;
            this.height = pt.Y;
        }

        public SizeF(float width, float height)
        {
            this.width = width;
            this.height = height;
        }

        public static explicit operator PointF(SizeF size)
        {
            return new PointF(size.Width, size.Height);
        }

        public static SizeF operator +(SizeF sz1, SizeF sz2)
        {
            return SizeF.Add(sz1, sz2);
        }

        public static SizeF operator -(SizeF sz1, SizeF sz2)
        {
            return SizeF.Subtract(sz1, sz2);
        }

        public static bool operator ==(SizeF sz1, SizeF sz2)
        {
            if ((double)sz1.Width == (double)sz2.Width)
                return (double)sz1.Height == (double)sz2.Height;
            return false;
        }

        public static bool operator !=(SizeF sz1, SizeF sz2)
        {
            return !(sz1 == sz2);
        }

        public static SizeF Add(SizeF sz1, SizeF sz2)
        {
            return new SizeF(sz1.Width + sz2.Width, sz1.Height + sz2.Height);
        }

        public static SizeF Subtract(SizeF sz1, SizeF sz2)
        {
            return new SizeF(sz1.Width - sz2.Width, sz1.Height - sz2.Height);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is SizeF))
                return false;
            SizeF sizeF = (SizeF)obj;
            if ((double)sizeF.Width == (double)this.Width && (double)sizeF.Height == (double)this.Height)
                return sizeF.GetType().Equals(this.GetType());
            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public PointF ToPointF()
        {
            return (PointF)this;
        }

        public Size ToSize()
        {
            return Size.Truncate(this);
        }

        public override string ToString()
        {
            return "{Width=" + this.width.ToString((IFormatProvider)CultureInfo.CurrentCulture) + ", Height=" + this.height.ToString((IFormatProvider)CultureInfo.CurrentCulture) + "}";
        }
    }

    [ComVisible(true)]
    [Serializable]
    public struct PointF
    {
        public static readonly PointF Empty;
        private float x;
        private float y;


        public bool IsEmpty
        {
            get
            {
                if ((double)this.x == 0.0)
                    return (double)this.y == 0.0;
                return false;
            }
        }

        public float X
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

        public float Y
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

        public PointF(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public static PointF operator +(PointF pt, Size sz)
        {
            return PointF.Add(pt, sz);
        }

        public static PointF operator -(PointF pt, Size sz)
        {
            return PointF.Subtract(pt, sz);
        }

        public static PointF operator +(PointF pt, SizeF sz)
        {
            return PointF.Add(pt, sz);
        }

        public static PointF operator -(PointF pt, SizeF sz)
        {
            return PointF.Subtract(pt, sz);
        }

        public static bool operator ==(PointF left, PointF right)
        {
            if ((double)left.X == (double)right.X)
                return (double)left.Y == (double)right.Y;
            return false;
        }

        public static bool operator !=(PointF left, PointF right)
        {
            return !(left == right);
        }

        public static PointF Add(PointF pt, Size sz)
        {
            return new PointF(pt.X + (float)sz.Width, pt.Y + (float)sz.Height);
        }

        public static PointF Subtract(PointF pt, Size sz)
        {
            return new PointF(pt.X - (float)sz.Width, pt.Y - (float)sz.Height);
        }

        public static PointF Add(PointF pt, SizeF sz)
        {
            return new PointF(pt.X + sz.Width, pt.Y + sz.Height);
        }

        public static PointF Subtract(PointF pt, SizeF sz)
        {
            return new PointF(pt.X - sz.Width, pt.Y - sz.Height);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is PointF))
                return false;
            PointF pointF = (PointF)obj;
            if ((double)pointF.X == (double)this.X && (double)pointF.Y == (double)this.Y)
                return pointF.GetType().Equals(this.GetType());
            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format((IFormatProvider)CultureInfo.CurrentCulture, "{{X={0}, Y={1}}}", new object[2]
            {
                (object) this.x,
                (object) this.y
            });
        }

    }

    [Serializable]
    public struct RectangleF
    {
        public static readonly RectangleF Empty;
        private float x;
        private float y;
        private float width;
        private float height;


        public PointF Location
        {
            get
            {
                return new PointF(this.X, this.Y);
            }
            set
            {
                this.X = value.X;
                this.Y = value.Y;
            }
        }


        public SizeF Size
        {
            get
            {
                return new SizeF(this.Width, this.Height);
            }
            set
            {
                this.Width = value.Width;
                this.Height = value.Height;
            }
        }

        public float X
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

        public float Y
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

        public float Width
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

        public float Height
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


        public float Left
        {
            get
            {
                return this.X;
            }
        }


        public float Top
        {
            get
            {
                return this.Y;
            }
        }

        public float Right
        {
            get
            {
                return this.X + this.Width;
            }
        }


        public float Bottom
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
                if ((double)this.Width > 0.0)
                    return (double)this.Height <= 0.0;
                return true;
            }
        }

        public RectangleF(float x, float y, float width, float height)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
        }

        public RectangleF(PointF location, SizeF size)
        {
            this.x = location.X;
            this.y = location.Y;
            this.width = size.Width;
            this.height = size.Height;
        }

        public static implicit operator RectangleF(Rectangle r)
        {
            return new RectangleF((float)r.X, (float)r.Y, (float)r.Width, (float)r.Height);
        }

        public static bool operator ==(RectangleF left, RectangleF right)
        {
            if ((double)left.X == (double)right.X && (double)left.Y == (double)right.Y && (double)left.Width == (double)right.Width)
                return (double)left.Height == (double)right.Height;
            return false;
        }

        public static bool operator !=(RectangleF left, RectangleF right)
        {
            return !(left == right);
        }

        public static RectangleF FromLTRB(float left, float top, float right, float bottom)
        {
            return new RectangleF(left, top, right - left, bottom - top);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is RectangleF))
                return false;
            RectangleF rectangleF = (RectangleF)obj;
            if ((double)rectangleF.X == (double)this.X && (double)rectangleF.Y == (double)this.Y && (double)rectangleF.Width == (double)this.Width)
                return (double)rectangleF.Height == (double)this.Height;
            return false;
        }

        public bool Contains(float x, float y)
        {
            if ((double)this.X <= (double)x && (double)x < (double)this.X + (double)this.Width && (double)this.Y <= (double)y)
                return (double)y < (double)this.Y + (double)this.Height;
            return false;
        }

        public bool Contains(PointF pt)
        {
            return this.Contains(pt.X, pt.Y);
        }

        public bool Contains(RectangleF rect)
        {
            if ((double)this.X <= (double)rect.X && (double)rect.X + (double)rect.Width <= (double)this.X + (double)this.Width && (double)this.Y <= (double)rect.Y)
                return (double)rect.Y + (double)rect.Height <= (double)this.Y + (double)this.Height;
            return false;
        }

        public override int GetHashCode()
        {
            return (int)(uint)this.X ^ ((int)(uint)this.Y << 13 | (int)((uint)this.Y >> 19)) ^ ((int)(uint)this.Width << 26 | (int)((uint)this.Width >> 6)) ^ ((int)(uint)this.Height << 7 | (int)((uint)this.Height >> 25));
        }

        public void Inflate(float x, float y)
        {
            this.X = this.X - x;
            this.Y = this.Y - y;
            this.Width = this.Width + 2f * x;
            this.Height = this.Height + 2f * y;
        }

        public void Inflate(SizeF size)
        {
            this.Inflate(size.Width, size.Height);
        }

        public static RectangleF Inflate(RectangleF rect, float x, float y)
        {
            RectangleF rectangleF = rect;
            rectangleF.Inflate(x, y);
            return rectangleF;
        }

        public void Intersect(RectangleF rect)
        {
            RectangleF rectangleF = RectangleF.Intersect(rect, this);
            this.X = rectangleF.X;
            this.Y = rectangleF.Y;
            this.Width = rectangleF.Width;
            this.Height = rectangleF.Height;
        }

        public static RectangleF Intersect(RectangleF a, RectangleF b)
        {
            float x = Math.Max(a.X, b.X);
            float num1 = Math.Min(a.X + a.Width, b.X + b.Width);
            float y = Math.Max(a.Y, b.Y);
            float num2 = Math.Min(a.Y + a.Height, b.Y + b.Height);
            if ((double)num1 >= (double)x && (double)num2 >= (double)y)
                return new RectangleF(x, y, num1 - x, num2 - y);
            return RectangleF.Empty;
        }

        public bool IntersectsWith(RectangleF rect)
        {
            if ((double)rect.X < (double)this.X + (double)this.Width && (double)this.X < (double)rect.X + (double)rect.Width && (double)rect.Y < (double)this.Y + (double)this.Height)
                return (double)this.Y < (double)rect.Y + (double)rect.Height;
            return false;
        }

        public static RectangleF Union(RectangleF a, RectangleF b)
        {
            float x = Math.Min(a.X, b.X);
            float num1 = Math.Max(a.X + a.Width, b.X + b.Width);
            float y = Math.Min(a.Y, b.Y);
            float num2 = Math.Max(a.Y + a.Height, b.Y + b.Height);
            return new RectangleF(x, y, num1 - x, num2 - y);
        }

        public void Offset(PointF pos)
        {
            this.Offset(pos.X, pos.Y);
        }

        public void Offset(float x, float y)
        {
            this.X = this.X + x;
            this.Y = this.Y + y;
        }



        public override string ToString()
        {
            return "{X=" + this.X + ",Y=" + this.Y.ToString((IFormatProvider)CultureInfo.CurrentCulture) + ",Right=" + this.Right + ",Bottom=" + this.Bottom.ToString((IFormatProvider)CultureInfo.CurrentCulture) + "}";
        }
        #endregion

    }
}