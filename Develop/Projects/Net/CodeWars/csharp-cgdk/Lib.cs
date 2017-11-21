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




    public abstract class Command
    {
        public ISituation Situation { protected get; set; }

        protected Move Move => Situation.Move;
        protected VehileCollection Vehiles => Situation.Vehiles;
        protected VehileCollection EnemyVehiles => Situation.EnemyVehiles;
        protected ICommandCollection Commands => Situation.Commands;
        protected ILog Log => Situation.Log;
        protected World World => Situation.World;

        protected MyStrategy Strategy => (MyStrategy)Situation;

        public Command Next { get; set; }

        protected abstract ActionType ActionType { get; }

        public void Do()
        {
            DoImpl();
            Act?.Invoke(Situation);
        }

        protected virtual void DoImpl()
        {
            Situation.Move.Action = ActionType;
        }

        public Predicate<ISituation> Can { get; set; }

        public Action<ISituation> Act { get; set; }

        public virtual bool CanAct()
        {
            return Can?.Invoke(Situation) ?? true;
        }


    }



    public abstract class GroupCommand : Command
    {
        public VehicleType Type { get; private set; }


        protected GroupCommand(VehicleType type)
        {
            Type = type;
        }

        private Command _prevNext;
        protected override void DoImpl()
        {
            if (Vehiles.Any(e => e.IsSelected && e.IsInGroup(Type)))
            {
                if (Next == this)
                    Next = _prevNext;
                base.DoImpl();
            }
            else
            {
                Move.Action = ActionType.ClearAndSelect;
                Move.Group = Type.ToInt();
                _prevNext = Next;
                Next = this;
            }
        }

        protected RectangleF GetGroupRect()
        {
            return Vehiles.Where(e => e.IsInGroup(Type)).GetRect();
        }
        protected RectangleF GetVehileRect()
        {
            return Vehiles.Where(e => e.Type == Type).GetRect();
        }
    }

    public class ScaleGroup : GroupCommand
    {
        protected override ActionType ActionType => ActionType.Scale;

        public ScaleGroup(VehicleType type) : base(type)
        {
        }


    }

    public class AssingGroupCommand : Command
    {
        protected override ActionType ActionType => ActionType.Assign;
        private readonly VehicleType _type;



        public AssingGroupCommand(VehicleType type)
        {
            _type = type;
        }

        protected override void DoImpl()
        {
            Move.Group = _type.ToInt();
            base.DoImpl();
        }
    }

    public class MoveCommand : GroupCommand
    {
        protected override ActionType ActionType => ActionType.Move;
        public MoveCommand(VehicleType type) : base(type)
        {
        }

    }

    public class MoveToCenterCommand : MoveCommand
    {
        public MoveToCenterCommand(VehicleType type) : base(type)
        {
        }

        protected override void DoImpl()
        {
            base.DoImpl();

            Move.X = World.Width / 2;
            Move.Y = World.Height / 2;
        }
    }

    public class SelectUnitCommand : Command
    {
        public VehicleType Type { get; protected set; }
        protected override ActionType ActionType => ActionType.ClearAndSelect;
        public SelectUnitCommand() { }
        public SelectUnitCommand(VehicleType type)
        {
            Type = type;
        }

        protected override void DoImpl()
        {
            var rect = Vehiles.GetVehileRect(Type);
            Move.VehicleType = Type;
            Move.X = rect.X;
            Move.Y = rect.Y;
            Move.Right = rect.Right;
            Move.Bottom = rect.Bottom;
            base.DoImpl();
        }
    }

    public interface ICommandCollection
    {
        void Add(Command command);
        void Remove(Command command);

        Command GetToPropcess();
    }

    public class CommandCollection : ICommandCollection
    {
        private readonly ISituation _situation;
        private readonly List<Command> _storage = new List<Command>();

        public CommandCollection(ISituation situation)
        {
            _situation = situation;
        }

        public void Add(Command command)
        {
            command.Situation = _situation;
            _storage.Add(command);
        }

        public void Remove(Command command)
        {
            command.Situation = null;
            _storage.Remove(command);
        }

        public Command GetToPropcess()
        {

            return _storage.Count > 0 ? _storage.FirstOrDefault(e => e.CanAct()) : null;
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
        private int GetIndex(double cur, double min, double max)
        {
            if (cur == min)
                return 0;
            if (cur == max)
                return 2;

            return 1;
        }
    }

    public class VecGroup
    {
        public VecGroup(RectangleF rect, VehicleType type)
        {
            Rect = rect;
            Type = type;
        }

        public RectangleF Rect { get; set; }
        public VehicleType Type { get; set; }
        public override string ToString()
        {
            return $"{Type} {Rect}";
        }
    }


    public class VehileCollection : IEnumerable<Veh>
    {
        private readonly Dictionary<long, Veh> _storage = new Dictionary<long, Veh>();
        private int playerId;

        public int Count => _storage.Count;

        public IEnumerator<Veh> GetEnumerator()
        {
            return _storage.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Initialize(Player player, World world)
        {
            if (world.VehicleUpdates.IsNotEmpty())
            {


                foreach (var update in world.VehicleUpdates)
                {
                    Veh v;
                    if (_storage.TryGetValue(update.Id, out v))
                    {
                        if (v.Durability == 0)
                            _storage.Remove(update.Id);
                        else
                            v.Update(update);
                    }
                }


            }

            if (world.NewVehicles.IsNotEmpty())
            {
                world.NewVehicles.Where(e => e.PlayerId == player.Id).ForEach(e => _storage[e.Id] = new Veh(e));
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

        public static RectangleF GetRect(this IEnumerable<Veh> vehicles)
        {
            if (vehicles == null)
                return RectangleF.Empty;

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

            return maxX == int.MinValue && minX == int.MaxValue ? RectangleF.Empty : new RectangleF(minX, minY, (maxX - minX), (maxY - minY));
        }
    }

    public class Veh
    {
        public Veh(Vehicle v)
        {
            Id = v.Id;
            X = v.X;
            Y = v.Y;
            Type = v.Type;
            Groups = v.Groups;
            IsSelected = v.IsSelected;
            Durability = v.Durability;
        }


        public long Id { get; }

        public int Durability { get; private set; }
        public bool IsSelected { get; private set; }
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

        public Veh Update(VehicleUpdate u)
        {
            X = u.X;
            Y = u.Y;
            Groups = u.Groups;
            IsSelected = u.IsSelected;
            Durability = u.Durability;
            return this;
        }

        public override string ToString()
        {
            return $"{Type} ({X},{Y})";
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
            var action = move.Action.Value;
            var vt = move.VehicleType ?? (VehicleType)(move.Group - 1);

            if (action == ActionType.Scale)
                Log("Action {0}  ({1},{2})-{3} ", action, move.X, move.Y, move.Factor);
            else if (action == ActionType.ClearAndSelect)
                Log("Action {0} Group {1} ({2},{3})-({4},{5}) ", action, vt, move.X, move.Y, move.Right, move.Bottom);
            else if (action == ActionType.Move)
                Log("Action {0} ({1},{2}) ", action, move.X, move.Y);
            else
                Log("Action {0} Group {1} ({2},{3})-({4},{5}) ", action, vt, move.X, move.Y, move.Right, move.Bottom);

            _stream.Flush();
        }

        public void Dispose()
        {
            _stream.Dispose();
        }
    }

    #region Geometry




    [Serializable]
    public struct SizeF
    {
        public static readonly SizeF Empty;
        private double width;
        private double height;


        public bool IsEmpty
        {
            get
            {
                if (width == 0.0)
                    return this.height == 0.0;
                return false;
            }
        }

        public double Width
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

        public double Height
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

        public SizeF(double width, double height)
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

        //public Size ToSize()
        //{
        //    return Size.Truncate(this);
        //}

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
        private double x;
        private double y;


        public bool IsEmpty
        {
            get
            {
                if ((double)this.x == 0.0)
                    return (double)this.y == 0.0;
                return false;
            }
        }

        public double X
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

        public double Y
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

        public PointF(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        //public static PointF operator +(PointF pt, Size sz)
        //{
        //    return PointF.Add(pt, sz);
        //}

        //public static PointF operator -(PointF pt, Size sz)
        //{
        //    return PointF.Subtract(pt, sz);
        //}

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

        //public static PointF Add(PointF pt, Size sz)
        //{
        //    return new PointF(pt.X + (double)sz.Width, pt.Y + (double)sz.Height);
        //}

        //public static PointF Subtract(PointF pt, Size sz)
        //{
        //    return new PointF(pt.X - (double)sz.Width, pt.Y - (double)sz.Height);
        //}

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
        private double x;
        private double y;
        private double width;
        private double height;


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

        public double X
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

        public double Y
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

        public double Width
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

        public double Height
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


        public double Left
        {
            get
            {
                return this.X;
            }
        }


        public double Top
        {
            get
            {
                return this.Y;
            }
        }

        public double Right
        {
            get
            {
                return X + Width;
            }
        }


        public double Bottom
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

        public RectangleF(double x, double y, double width, double height)
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

        //public static implicit operator RectangleF(Rectangle r)
        //{
        //    return new RectangleF((double)r.X, (double)r.Y, (double)r.Width, (double)r.Height);
        //}

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

        public static RectangleF FromLTRB(double left, double top, double right, double bottom)
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

        public bool Contains(double x, double y)
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

        public void Inflate(double x, double y)
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

        public static RectangleF Inflate(RectangleF rect, double x, double y)
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
            double x = Math.Max(a.X, b.X);
            double num1 = Math.Min(a.X + a.Width, b.X + b.Width);
            double y = Math.Max(a.Y, b.Y);
            double num2 = Math.Min(a.Y + a.Height, b.Y + b.Height);
            if ((double)num1 >= (double)x && (double)num2 >= (double)y)
                return new RectangleF(x, y, num1 - x, num2 - y);
            return RectangleF.Empty;
        }

        public PointF Center => new PointF(Left + Width / 2,
            Top + Height / 2);

        public bool IntersectsWith(RectangleF rect)
        {
            if ((double)rect.X < (double)this.X + (double)this.Width && (double)this.X < (double)rect.X + (double)rect.Width && (double)rect.Y < (double)this.Y + (double)this.Height)
                return (double)this.Y < (double)rect.Y + (double)rect.Height;
            return false;
        }

        public static RectangleF Union(RectangleF a, RectangleF b)
        {
            double x = Math.Min(a.X, b.X);
            double num1 = Math.Max(a.X + a.Width, b.X + b.Width);
            double y = Math.Min(a.Y, b.Y);
            double num2 = Math.Max(a.Y + a.Height, b.Y + b.Height);
            return new RectangleF(x, y, num1 - x, num2 - y);
        }

        public void Offset(PointF pos)
        {
            this.Offset(pos.X, pos.Y);
        }

        public void Offset(double x, double y)
        {
            this.X = this.X + x;
            this.Y = this.Y + y;
        }



        public override string ToString()
        {
            return "{X=" + this.X + ",Y=" + this.Y.ToString((IFormatProvider)CultureInfo.CurrentCulture) + ",Right=" + this.Right + ",Bottom=" + this.Bottom.ToString((IFormatProvider)CultureInfo.CurrentCulture) + "}";
        }




    }
    public abstract class DrawingPrimitive
    {
        protected const int InBoundOffset = 3;

        public abstract bool InBound(PointF point);
        public abstract bool InHorizontalRange(PointF point, bool considerOffset = true);
        protected static int GetOffset(bool considerOffset)
        {
            return considerOffset ? 3 : 0;
        }
    }

    public abstract class PointSizePrimitive : DrawingPrimitive
    {
        public PointF Point
        {
            get;
            set;
        }
        public SizeF Size
        {
            get;
            set;
        }
        protected PointSizePrimitive(PointF point, SizeF size)
        {
            Point = point;
            Size = size;
        }
    }
    public class Ellipse : PointSizePrimitive
    {
        private PointF _center = PointF.Empty;
        protected PointF Center
        {
            get
            {
                if (_center.IsEmpty)
                    _center = new PointF(Point.X + Size.Width / 2, Point.Y + Size.Height / 2);
                return _center;
            }
        }

        public Ellipse(PointF point, SizeF size) : base(point, size)
        {
        }
        public Ellipse(double x, double y, double width, float height) : this(new PointF(x, y), new SizeF(width, height))
        {
        }

        public bool InBound(double x, double y)
        {

            double xRadius = Size.Width / 2;
            double yRadius = Size.Height / 2;

            if (xRadius <= 0.0 || yRadius <= 0.0)
                return false;
            /* This is a more general form of the circle equation
             *
             * X^2/a^2 + Y^2/b^2 <= 1
             */
            var normalized = new PointF(x - Center.X, y - Center.Y);

            return ((normalized.X * normalized.X)
                    / (xRadius * xRadius)) + ((normalized.Y * normalized.Y) / (yRadius * yRadius))
                   <= 1.0;
        }

        public override bool InBound(PointF point)
        {
            return InBound(point.X, point.Y);
        }
        public override bool InHorizontalRange(PointF point, bool considerOffset = true)
        {
            return (base.Point.X - GetOffset(considerOffset)) <= point.X && (base.Point.X + base.Size.Width + GetOffset(considerOffset)) >= point.X;
        }
    }

    #endregion

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1708:IdentifiersShouldDifferByMoreThanCase")]
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Ins the specified array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <param name="array">The array.</param>
        /// <returns></returns>
        public static bool In<T>(this T value, params T[] array)
        {
            return array.Contains(value);
        }


        /// <summary>
        /// Concats the specified second.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <param name="first">The first.</param>
        /// <param name="second">The second.</param>
        /// <returns></returns>
        public static IEnumerable<TSource> Concat<TSource>(this IEnumerable<TSource> first, TSource second)
        {
            if (first != null)
                foreach (var element in first) yield return element;
            if (second != null)
                yield return second;
        }

        public static IEnumerable<TSource> Concat<TSource>(this IEnumerable<TSource> first, TSource second, IEnumerable<TSource> third)
        {
            if (first != null)
                foreach (var element in first) yield return element;
            if (second != null)
                yield return second;
            if (third != null)
                foreach (var element in third) yield return element;
        }
        /// <summary>
        /// Concats the specified second.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <param name="first">The first.</param>
        /// <param name="second">The second.</param>
        /// <returns></returns>
        public static IEnumerable<TSource> Concat<TSource>(this TSource first, IEnumerable<TSource> second)
        {
            if (first != null)
                yield return first;

            if (second != null)
                foreach (var element in second) yield return element;

        }
        /// <summary>
        /// Concats the specified arrays.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arrays">The arrays.</param>
        /// <returns></returns>
        public static IEnumerable<T> Concat<T>(params IEnumerable<T>[] arrays)
        {
            return arrays.Where(array => array != null).SelectMany(array => array);
        }
        public static IEnumerable<T> Concat<T>(params T[][] arrays)
        {
            return arrays.Where(array => array != null).SelectMany(array => array);
        }
        /// <summary>
        /// Concats the specified items.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items">The items.</param>
        /// <param name="arrays">The arrays.</param>
        /// <returns></returns>
        public static IEnumerable<T> Concat<T>(this IEnumerable<T> items, params IEnumerable<T>[] arrays)
        {
            return Enumerable.Concat(items, Concat(arrays));
        }
        public static IEnumerable<T> Concat<T>(this IEnumerable<T> first, params T[] args)
        {
            foreach (T element in first) yield return element;
            foreach (T element in args) yield return element;
        }

        public static IEnumerable<T> ConcatNotNull<T>(this IEnumerable<T> first, params T[] args)
        {
            foreach (T element in first) yield return element;
            foreach (T element in args)
                if (element != null)
                    yield return element;
        }
        /// <summary>
        /// Equalses the specified array1.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array1">The array1.</param>
        /// <param name="array2">The array2.</param>
        /// <returns></returns>
        public static bool ArrayEquals<T>(this T[] array1, T[] array2)
        {
            if (ReferenceEquals(array1, array2))
                return true;

            if (array1 == null || array2 == null)
                return false;

            if (array1.Length != array2.Length)
                return false;

            EqualityComparer<T> comparer = EqualityComparer<T>.Default;
            for (int i = 0; i < array1.Length; i++)
            {
                if (!comparer.Equals(array1[i], array2[i])) return false;
            }
            return true;
        }

        /// <summary>
        /// Adds the range.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <param name="items">The items.</param>
        public static void AddRange(this IList list, IEnumerable items)
        {
            if (items != null && list != null)
                foreach (var item in items)
                    list.Add(item);
        }
        /// <summary>
        /// Removes the range.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <param name="items">The items.</param>
        public static void RemoveRange(this IList list, IEnumerable items)
        {
            if (items != null && list != null)
                foreach (var item in items)
                    list.Remove(item);
        }

        /// <summary>
        /// Determines whether the specified ar is empty.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items">The ar.</param>
        /// <returns>
        ///   <c>true</c> if the specified ar is empty; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsEmpty<T>(this IEnumerable<T> items)
        {
            return items == null || !items.Any();
        }

        /// <summary>
        /// Determines whether the specified ar is empty.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array">The items.</param>
        /// <returns></returns>
        public static bool IsEmpty<T>(this T[] array)
        {
            return array == null || array.Length == 0;
        }

        /// <summary>
        /// Determines whether [is not empty] [the specified ar].
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items">The items.</param>
        /// <returns></returns>
        public static bool IsNotEmpty<T>(this IEnumerable<T> items)
        {
            return items != null && items.Any();
        }

        /// <summary>
        /// Excepts the specified except.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable">The enumerable.</param>
        /// <param name="filter">The except.</param>
        /// <returns></returns>
        public static IEnumerable<T> Except<T>(this IEnumerable<T> enumerable, params T[] filter)
        {
            return enumerable.Except((IEnumerable<T>)filter);
        }

        /// <summary>
        /// Fors the each.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable">The enumerable.</param>
        /// <param name="action">The action.</param>
        public static IEnumerable<T> Foreach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            if (enumerable != null && action != null)
                foreach (var e in enumerable)
                    action(e);
            return enumerable;
        }

        /// <summary>
        /// Fors the each.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable">The enumerable.</param>
        /// <param name="action">The action.</param>
        /// <returns></returns>
        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            if (enumerable != null && action != null)
                foreach (var e in enumerable)
                    action(e);
            return enumerable;
        }


        /// <summary>
        /// Fills the specified list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">The list.</param>
        /// <param name="data">The data.</param>
        /// <param name="count">The count.</param>
        public static void Fill<T>(this IList<T> list, T data, int count)
        {
            if (list != null)
                for (var i = 0; i < count; i++)
                    list.Add(data);
        }

        //<summary>Finds the index of the first item matching an expression in an enumerable.</summary>
        ///<param name="items">The enumerable to search.</param>
        ///<param name="predicate">The expression to test the items against.</param>
        ///<returns>The index of the first matching item, or -1 if no items match.</returns>
        public static int FindIndex<T>(this IEnumerable<T> items, Func<T, bool> predicate)
        {
            if (items == null) throw new ArgumentNullException("items");
            if (predicate == null) throw new ArgumentNullException("predicate");

            int retVal = 0;
            foreach (var item in items)
            {
                if (predicate(item)) return retVal;
                retVal++;
            }
            return -1;
        }
        ///<summary>Finds the index of the first occurence of an item in an enumerable.</summary>
        ///<param name="items">The enumerable to search.</param>
        ///<param name="item">The item to find.</param>
        ///<returns>The index of the first matching item, or -1 if the item was not found.</returns>
        public static int IndexOf<T>(this IEnumerable<T> items, T item) { return items.FindIndex(i => EqualityComparer<T>.Default.Equals(item, i)); }
    }

}