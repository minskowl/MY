using System.Collections.Generic;
using System.Linq;
using Com.CodeGame.CodeWars2017.DevKit.CSharpCgdk.Model;

namespace Com.CodeGame.CodeWars2017.DevKit.CSharpCgdk
{
    public interface ISituation
    {
        VehileCollection Vehiles { get; }
        VehileCollection EnemyVehiles { get; }

        Player Me { get; }
        World World { get; }
        Game Game { get; }
        Move Move { get; }

        IList<Command> Commands { get; }

        bool CanNuclearStrike { get; }
        ILog Log { get; }
    }

    public sealed class MyStrategy : IStrategy, ISituation
    {
        private VehicleType[] _types = { VehicleType.Arrv, VehicleType.Fighter, VehicleType.Helicopter, VehicleType.Ifv, VehicleType.Tank };
        public bool CanNuclearStrike { get; private set; }
        public ILog Log { get; }

        public VehileCollection Vehiles { get; }
        public VehileCollection EnemyVehiles { get; }

        public Player Me { get; private set; }
        public World World { get; private set; }
        public Game Game { get; private set; }
        public Move Move { get; private set; }
        public IList<Command> Commands { get; }
        Command Command { get; set; }

        public readonly StartMatrix StartMatrix = new StartMatrix();

        public MyStrategy()
        {
            Vehiles = new VehileCollection();
            EnemyVehiles = new VehileCollection();

            Commands = new List<Command>
            {
                new FirstCommand(),
                new NuclearStriceCommand()
            };

            Log = new NullLogger();

        }

        private int _availibleActionCount;

        void IStrategy.Move(Player me, World world, Game game, Move move)
        {
            if (world.TickIndex % game.ActionDetectionInterval == 0)
                _availibleActionCount = game.BaseActionCount;

            CanNuclearStrike = me.RemainingNuclearStrikeCooldownTicks <= 0;


            Move = move;
            Me = me;
            World = world;
            Game = game;

            Trace($"********************************** TickIndex = {world.TickIndex} AvailibleActionCount={_availibleActionCount} NextNuclearStrikeTickIndex={me.NextNuclearStrikeTickIndex}");

            Vehiles.Initialize(me, world);
            EnemyVehiles.Initialize(world.GetOpponentPlayer(), world);


            foreach (var type in _types)
            {
                Trace("{0} {1}", type, Vehiles.GetVehileRect(type));
            }
            if (EnemyVehiles.Count > 0)
            {
                Trace("##### ENEMIES #######");
                foreach (var type in _types)
                {
                    Trace("{0} {1}", type, Vehiles.GetVehileRect(type));
                }
            }

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
            Log?.Log(Move);
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




}
