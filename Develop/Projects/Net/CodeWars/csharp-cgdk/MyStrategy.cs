using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Com.CodeGame.CodeWars2017.DevKit.CSharpCgdk.Model;

namespace Com.CodeGame.CodeWars2017.DevKit.CSharpCgdk
{
    public sealed class MyStrategy : IStrategy
    {
        private ILog _log;
        private Move _move;

        public MyStrategy()
        {
            _log = new FileLogger();

            _actions.Add(new SelectUnit(VehicleType.Fighter)
            {
                Next = new AssingGroup(VehicleType.Fighter)
                {
                    Next = new MoveGroup(VehicleType.Fighter)
                    {
                        Next = new ScaleGroup(VehicleType.Fighter)
                    }
                }
            });

        }

        private VehileCollection _vehiles;
        private List<Action> _actions = new List<Action>();


        public void Move(Player me, World world, Game game, Move move)
        {
            _move = move;

            Log($"********************************** TickIndex = {world.TickIndex}");

            if (world.TickIndex == 0)
            {
                _vehiles = new VehileCollection(world.NewVehicles
                    .Where(e => e.PlayerId == me.Id));


            }
            else
            {
                _vehiles.Add(world.NewVehicles.Where(e => e.PlayerId == me.Id));
                _vehiles.Update(world.VehicleUpdates);
            }
            if (_actions.Count > 0)
            {
                var action = _actions.FirstOrDefault(e => e.CanAct(_vehiles));
                if (action != null)
                {
                    action.Do(world, move, _vehiles);
                    _actions.Remove(action);
                    if (action.Next != null)
                        _actions.Add(action.Next);
                }
            }

            Log();
        }




        private void Log()
        {
            _log?.Log("{0} ", _move.Action);
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