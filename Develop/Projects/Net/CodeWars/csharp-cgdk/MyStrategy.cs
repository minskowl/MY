using System.Collections.Generic;
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

        ILog Log { get; }
    }

    public sealed class MyStrategy : IStrategy, ISituation
    {
        public ILog Log { get; }

        public VehileCollection Vehiles { get; }
        public Player Me { get; private set; }
        public World World { get; private set; }
        public Game Game { get; private set; }
        public Move Move { get; private set; }
        public IList<Command> Commands { get; }
        Command Command { get; set; }

        public StartMatrix StartMatrix = new StartMatrix();

        public MyStrategy()
        {
            Commands = new List<Command>();
            Vehiles = new VehileCollection();
            Log = new FileLogger();
            Commands.Add(new FirstCommand());


        }

        private int _availibleActionCount;

        void IStrategy.Move(Player me, World world, Game game, Move move)
        {
            if (world.TickIndex % game.ActionDetectionInterval == 0)
                _availibleActionCount = game.BaseActionCount;

            Move = move;
            Me = me;
            World = world;
            Game = game;

            Trace($"********************************** TickIndex = {world.TickIndex} AvailibleActionCount={_availibleActionCount}");

            Vehiles.Add(world.NewVehicles.Where(e => e.PlayerId == me.Id));
            Vehiles.Update(world.VehicleUpdates);

            if (_availibleActionCount > 0)
            {
                PrepareCommand();
                if (Command != null)
                {
                    Command.Do(this);
                    Commands.Remove(Command);

                    Command = Command.Next;
                    _availibleActionCount--;
                }
            }

            Trace();
        }

        private void PrepareCommand()
        {
            if (Command != null)
                return;
            if (Commands.Count > 0)
                Command = Commands.FirstOrDefault(e => e.CanAct(this));

        }


        private void Trace()
        {
            if (Move.Action.HasValue)
                Log?.Log("{0} Group {1} {6} ({2},{3})-({4},{5}) ", Move.Action, Move.Group, Move.X, Move.Y, Move.Right, Move.Bottom, Move.VehicleType);
        }

        private void Trace(object text)
        {
            Log?.Log(text.ToString());
        }
        private void Trace(string text)
        {
            Log?.Log(text);
        }

        private void Trace(string text, params object[] args)
        {
            Log?.Log(text, args);
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



}