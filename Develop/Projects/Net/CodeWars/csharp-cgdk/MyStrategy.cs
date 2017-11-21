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
        ICommandCollection Commands { get; }

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
        public ICommandCollection Commands { get; }
        Command Command { get; set; }

        public readonly StartMatrix StartMatrix = new StartMatrix();

        public MyStrategy()
        {
            Vehiles = new VehileCollection();
            EnemyVehiles = new VehileCollection();

            Commands = new CommandCollection(this);
            Commands.Add(new FirstCommand());
            Commands.Add(new NuclearStriceCommand());


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
                if (Command == null)
                    Command = Commands.GetToPropcess();
                if (Command != null)
                {
                    Command.Situation = this;
                    Command.Do();
                    Commands.Remove(Command);

                    Command = Command.Next;
                    _availibleActionCount--;
                }
            }

            Trace();
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
        public static int Count { get; private set; }

        protected override void DoImpl()
        {
            var gr = Strategy.StartMatrix.GetFreeGroup();

            if (gr == null)
                return;

            Count++;
            Type = gr.Type;
            base.DoImpl();

            Next = new AssingGroupCommand(Type)
            {
                Next = new MoveToCenterCommand(Type)
                {
                    Act = s =>
                    {
                        s.Commands.Add(new DeployScaleCommand(Type));

                        if (DeployCommand.Count < 5)
                            s.Commands.Add(new DeployCommand());
                    }
                }
            };
        }
    }

    public class NuclearStriceCommand : Command
    {
        protected override ActionType ActionType => ActionType.TacticalNuclearStrike;
        private Veh[] seeVehiles;
        private Veh _vehicle;
        public override bool CanAct()
        {
            base.CanAct();

            if (!Situation.CanNuclearStrike)
                return false;

            var rect = EnemyVehiles.GetRect();

            seeVehiles = Vehiles.Where(e => e.Durability > 50 && rect.Contains(e.X, e.Y)).ToArray();
            if (seeVehiles.IsEmpty()) return false;

            var maxStrice = 0;
            foreach (var seeVehile in seeVehiles)
            {
                var el = new Ellipse(seeVehile.X, seeVehile.Y, 150, 150);
                var inStriceEnemies = EnemyVehiles.Count(e => el.InBound(e.X, e.Y));

                var inStriceOurs = Vehiles.Count(e => el.InBound(e.X, e.Y));

                if (inStriceOurs < inStriceEnemies && inStriceOurs < 20 && inStriceEnemies > maxStrice)
                {
                    maxStrice = inStriceEnemies;
                    _vehicle = seeVehile;
                }
                if (maxStrice > 200)
                    break;
            }


            return seeVehiles.IsNotEmpty() && maxStrice > 50;
        }

        protected override void DoImpl()
        {
            base.DoImpl();

            Move.X = _vehicle.X;
            Move.Y = _vehicle.Y;
            Move.VehicleId = _vehicle.Id;

            Commands.Add(new NuclearStriceCommand());
        }
    }






    public class DeployScaleCommand : ScaleGroup
    {
        private RectangleF _rectangle;
        public DeployScaleCommand(VehicleType type) : base(type)
        {
        }

        public override bool CanAct()
        {
            _rectangle = Vehiles.GetGroupRect(Type);
            var minY = World.Height / 3;
            Log.Log("Wait Scale Type {0} {1} minY {2}", Type, _rectangle, minY);
            return _rectangle.Y >= minY;
        }

        protected override void DoImpl()
        {
            base.DoImpl();

            Move.Factor = 6;
            var center = _rectangle.Center;
            Move.X = center.X;
            Move.Y = center.Y;
        }
    }


}
