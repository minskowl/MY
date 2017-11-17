using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Com.CodeGame.CodeWars2017.DevKit.CSharpCgdk.Model;

namespace Com.CodeGame.CodeWars2017.DevKit.CSharpCgdk
{
    public interface ISituation
    {
        VehileCollection Vehiles { get; }
        Player Me { get; }
        World World { get; }
        Game Game { get; }
        Move Move { get; }

        IList<Command> Commands { get; }
    }

    public sealed class MyStrategy : IStrategy, ISituation
    {
        private ILog _log;


        public VehileCollection Vehiles { get; }
        public Player Me { get; private set; }
        public World World { get; private set; }
        public Game Game { get; private set; }
        public Move Move { get; private set; }
        public IList<Command> Commands { get; } 
         Command Command { get; set; }
        private RectangleF rect;

        public MyStrategy()
        {
            Commands = new List<Command>();
            Vehiles = new VehileCollection();
            _log = new FileLogger();

            Commands.Add(new FirstCommand());
            //Commands.Add(new SelectUnitCommand(VehicleType.Fighter)
            //{
            //    Next = new AssingGroup(VehicleType.Fighter)
            //    {
            //        Next = new MoveGroup(VehicleType.Fighter)
            //        {
            //            Act = s =>
            //            {
            //                var rect = Vehiles.GetGroupRect(VehicleType.Fighter);

            //                Move.X = (World.Width / 2) - rect.Right;
            //                Move.Y = (World.Height / 2) - rect.Bottom;
            //            },
            //            Next = new ScaleGroup(VehicleType.Fighter)
            //            {
            //                Can = s =>
            //                 {
            //                     rect = Vehiles.GetGroupRect((int)VehicleType.Fighter);
            //                     var minY = World.Height / 2;
            //                     Log("minY {0} {1}", minY, rect);
            //                     return rect.Y > minY && rect.Bottom > minY;

            //                 },
            //                Act = (s) =>
            //                {
            //                    Move.X = rect.X;
            //                    Move.Y = rect.Y;
            //                    Move.Factor = 3;
            //                }
            //            }
            //        }
            //    }
            //});

        }


      

  


       public VecGroup[,] matrix = new VecGroup[3, 3];
        void IStrategy.Move(Player me, World world, Game game, Move move)
        {
            Move = move;
            Me = me;
            World = world;
            Game = game;

            Log($"********************************** TickIndex = {world.TickIndex}");

            Vehiles.Add(world.NewVehicles.Where(e => e.PlayerId == me.Id));
            Vehiles.Update(world.VehicleUpdates);

          
            PrepareCommand();
            if (Command != null)
            {
                Command.Do(this);
                Commands.Remove(Command);

                Command = Command.Next;
            }

            Log();
        }

        private void PrepareCommand()
        {
            if (Command != null)
                return;
            if (Commands.Count > 0)
                Command = Commands.FirstOrDefault(e => e.CanAct(this));

        }

       

        private void Log()
        {
            _log?.Log("{0} Group {1} ({2},{3})-({4},{5}) ", Move.Action, Move.Group, Move.X, Move.Y, Move.Right, Move.Bottom);
            foreach (var vehile in Vehiles.Where(e => e.Type == VehicleType.Fighter).Take(10))
            {
                _log?.Log("{0} Id {1} ({2},{3}) Group {4}", vehile.Type, vehile.Id, vehile.X, vehile.Y, vehile.Groups?.FirstOrDefault());
            }
        }

        private void Log(object text)
        {
            _log?.Log(text.ToString());
        }
        private void Log(string text)
        {
            _log?.Log(text);
        }

        private void Log(string text, params object[] args)
        {
            _log?.Log(text, args);
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
    public class FirstCommand : SelectUnitCommand
    {
        
        protected override void DoImpl()
        {
            BuilMatrix();
            var gr= GetFreeGroup();
           
            if(gr==null)
                return;

            Type = gr.Type;
            base.DoImpl();

            Next = new AssingGroup(Type)
            {
                Next = new MoveGroup(Type)
                {
                    Act = s =>
                    {
                        var rect = Vehiles.GetGroupRect(Type);

                        s.Move.X = (s.World.Width / 2) - rect.Right;
                        s.Move.Y = (s.World.Height / 2) - rect.Bottom;
                    }
                }
            };
        }

        private VecGroup GetFreeGroup()
        {
            for (int i = 2; i >= 0; i--)
            {
                for (int j = 2; j >= 0; j--)
                {
                    var gr = Strategy.matrix[i, j];
                    if (gr != null)
                    {
                        Strategy.matrix[i, j] = null;
                        return gr;
                    }

                }
            }
            return null;
        }

        private void BuilMatrix()
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
                Strategy.matrix[y, x] = vecGroup;
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
}