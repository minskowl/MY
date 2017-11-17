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

    }

    public sealed class MyStrategy : IStrategy, ISituation
    {
        private ILog _log;


        public VehileCollection Vehiles { get; }
        public Player Me { get; private set; }
        public World World { get; private set; }
        public Game Game { get; private set; }
        public Move Move { get; private set; }
        private RectangleF rect;
        public MyStrategy()
        {
            Vehiles = new VehileCollection();
            _log = new FileLogger();

            _actions.Add(new SelectUnit(VehicleType.Fighter)
            {
                Next = new AssingGroup(VehicleType.Fighter)
                {
                    Next = new MoveGroup(VehicleType.Fighter)
                    {
                        Act = s =>
                        {
                            var rect = Vehiles.GetGroupRect(VehicleType.Fighter);

                            Move.X = (World.Width / 2) - rect.Right;
                            Move.Y = (World.Height / 2) - rect.Bottom;
                        },
                        Next = new ScaleGroup(VehicleType.Fighter)
                        {
                            Can = s =>
                             {
                                 rect = Vehiles.GetGroupRect((int)VehicleType.Fighter);
                                 var minY = World.Height / 2;
                                 Console.WriteLine(rect);
                                 return rect.Y > minY && rect.Bottom > minY;

                             },
                            Act = (s) =>
                            {
                                Move.X = rect.X ;
                                Move.Y = rect.Y;
                                Move.Factor = 3;
                            }
                        }
                    }
                }
            });

        }


        private readonly List<Action> _actions = new List<Action>();


        void IStrategy.Move(Player me, World world, Game game, Move move)
        {
            Move = move;
            Me = me;
            World = world;
            Game = game;

            Log($"********************************** TickIndex = {world.TickIndex}");
            Vehiles.Add(world.NewVehicles.Where(e => e.PlayerId == me.Id));
            Vehiles.Update(world.VehicleUpdates);

            if (_actions.Count > 0)
            {
                var action = _actions.FirstOrDefault(e => e.CanAct(this));
                if (action != null)
                {
                    action.Do(this);
                    _actions.Remove(action);
                    if (action.Next != null)
                        _actions.Add(action.Next);
                }
            }

            Log();
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

       

    }
}