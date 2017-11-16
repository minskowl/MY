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

        public MyStrategy()
        {
            _log = new FileLogger();
        }

        private VehileCollection _vehiles;

        public void Move(Player me, World world, Game game, Move move)
        {

            //Log($"********************************** TickIndex = {world.TickIndex}");
            //var rect = GetRect(world.NewVehicles
            //              .Where(e => e.PlayerId == me.Id && e.Type == VehicleType.Fighter));
            //Log(rect);

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

            switch (world.TickIndex)
            {
                case 0:
                    SelectUnit(move, VehicleType.Fighter);
                    return;
                case 1:
                    move.Action = ActionType.Assign;
                    move.Group = (int)VehicleType.Fighter;
                    return;

                case 2:
                    SelectUnit(move, VehicleType.Helicopter);
                    return;
                case 3:
                    move.Action = ActionType.Assign;
                    move.Group = (int)VehicleType.Helicopter;
                    return;

                case 4:
                    SelectUnit(move, VehicleType.Arrv);
                    return;
                case 5:
                    move.Action = ActionType.Assign;
                    move.Group = (int)VehicleType.Arrv;
                    return;

                case 6:
                    SelectUnit(move, VehicleType.Ifv);
                    return;
                case 7:
                    move.Action = ActionType.Assign;
                    move.Group = (int)VehicleType.Ifv;
                    return;

                case 8:
                    SelectUnit(move, VehicleType.Tank);
                    return;
                case 9:
                    move.Action = ActionType.Assign;
                    move.Group = (int)VehicleType.Tank;
                    return;


                case 10:
                    move.Action = ActionType.Move;
                    move.Group = (int)VehicleType.Fighter;
                    move.X = world.Width / 2;
                    move.Y = world.Height / 2;

                    return;

                case 11:
                    move.Action = ActionType.Move;
                    move.Group = (int)VehicleType.Helicopter;
                    move.X = (world.Width / 2) - 150;
                    move.Y = world.Height / 2;
                    return;

                case 12:
                    move.Action = ActionType.Move;
                    move.Group = (int)VehicleType.Ifv;
                    move.X = (world.Width / 2) + 150;
                    move.Y = world.Height / 2;
                    return;

            }




        }

        private void SelectUnit(Move move, VehicleType type)
        {
            var rect = GetRect(_vehiles.Where(e => e.Type == type));

            move.Action = ActionType.ClearAndSelect;
            move.VehicleType = type;
            move.X = rect.X;
            move.Y = rect.Y;
            move.Right = rect.Right;
            move.Bottom = rect.Bottom;
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