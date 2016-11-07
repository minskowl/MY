using BotvaSpider.Gears;

namespace BotvaSpider.Data
{
    public class CoulombInfo
    {
        /// <summary>
        /// Coulombs
        /// </summary>
        public static readonly CoulombInfo[] Coulombs = new[]
                                                            {
                                                                new CoulombInfo(Coulomb.BigPaunch,15,100,1500),
                                                                new CoulombInfo(Coulomb.CopyCryst, 25,100,2500),
                                                                new CoulombInfo(Coulomb.CrystalLuck, 15,100,1500),
                                                                new CoulombInfo(Coulomb.CrystalRadar, 15,100,2500),
                                                                new CoulombInfo(Coulomb.CrystalThief, 25,77,2500),
                                                                new CoulombInfo(Coulomb.Drill, 25,100,2500),
                                                                new CoulombInfo(Coulomb.Kakdams, 5,100,500),
                                                                new CoulombInfo(Coulomb.SmartBaby, 15,100,1500),
                                                                new CoulombInfo(Coulomb.TrippleHoof, 5,100,1000),
                                                                new CoulombInfo(Coulomb.Unscrewer, 10,100,1000),
                                                                new CoulombInfo(Coulomb.Walker, 5,100,500),
                                                                new CoulombInfo(Coulomb.Antimag, 15,96,2500),
                                                                new CoulombInfo(Coulomb.Builder, 50,100,5000),
                                                                new CoulombInfo(Coulomb.Attacker, 50,100,5000),
                                                            };

        /// <summary>
        /// Gets or sets the level price.
        /// </summary>
        /// <value>The level price.</value>
        public int LevelPrice { get; set; }
        /// <summary>
        /// Gets or sets the last level price.
        /// </summary>
        /// <value>The last level price.</value>
        public int LastLevelPrice { get; set; }
        /// <summary>
        /// Gets or sets the levels.
        /// </summary>
        /// <value>The levels.</value>
        public byte Levels { get; set; }
        /// <summary>
        /// Gets or sets the coulomb.
        /// </summary>
        /// <value>The coulomb.</value>
        public Coulomb Coulomb { get; set; }
        /// <summary>
        /// Gets or sets the lastlevel description.
        /// </summary>
        /// <value>The lastlevel description.</value>
        public string LastLevelDescription { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="CoulombInfo"/> class.
        /// </summary>
        /// <param name="coulomb">The coulomb.</param>
        /// <param name="levelPrice">The level price.</param>
        /// <param name="levels">The levels.</param>
        /// <param name="lastLevelPrice">The last level price.</param>
        public CoulombInfo(Coulomb coulomb, int levelPrice, byte levels, int lastLevelPrice)
        {
            Coulomb = coulomb;
            LevelPrice = levelPrice;
            Levels = levels;
            LastLevelPrice = lastLevelPrice;
        }
    }
}