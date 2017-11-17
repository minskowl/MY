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
        private Move _move;

        public VehileCollection Vehiles { get; }
        public Player Me { get; private set; }
        public World World { get; private set; }
        public Game Game { get; private set; }
        Move ISituation.Move => _move;

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
                        //  Next = new ScaleGroup(VehicleType.Fighter)
                    }
                }
            });

        }

        
        private readonly List<Action> _actions = new List<Action>();


        public void Move(Player me, World world, Game game, Move move)
        {
            _move = move;
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
            _log?.Log("{0} Group {1} ({2},{3})-({4},{5}) ", _move.Action, _move.Group, _move.X, _move.Y, _move.Right, _move.Bottom);
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

        private Rectangle GetRect(IEnumerable<Vec> vehicles)
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
}