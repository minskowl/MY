using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

public class MyBot
{
    private static int turn;
    private static List<Planet> _readyForEcspancion = new List<Planet>();
    private static List<Planet> _needForHelp = new List<Planet>();
    private static int maxEnemies;
    private static int redyForEcsCount;
    public static StreamWriter log;

    // The DoTurn function is where your code goes. The PlanetWars object
    // contains the state of the game, including information about all planets
    // and fleets that currently exist. Inside this function, you issue orders
    // using the pw.IssueOrder() function. For example, to send 10 ships from
    // planet 3 to planet 8, you would say pw.IssueOrder(3, 8, 10).
    //
    // There is already a basic strategy in place here. You can use it as a
    // starting point, or you can throw it out entirely and replace it with
    // your own. Check out the tutorials and articles on the contest website at
    // http://www.ai-contest.com/resources.
    public static void DoTurn(PlanetWars pw)
    {
        turn++;
        var enemyPlanet = pw.EnemyPlanets();

        maxEnemies = enemyPlanet.Count > 0 ? pw.EnemyPlanets().Max(e => e.NumShips) : 0;

        redyForEcsCount = maxEnemies / 2;
        if (redyForEcsCount == 0)
        {
            var fleets = pw.EnemyFleets();
            redyForEcsCount = fleets.Count > 0 ? fleets.Max(e => e.NumShips()) / 2 : 50;
        }
        log.WriteLine(string.Format("#########{0} My Sheeps {1} Enemy {2} Max {3}",
            turn, pw.NumShips(1), pw.NumShips(2), maxEnemies)
            );

        InitSituation(pw);

        //DoHelp(pw);
        DoAttack(pw);
    exit:
        log.Flush();
    }
    private static void DoHelp(PlanetWars pw)
    {
        foreach (var planetForHelp in _needForHelp)
        {
            var pl = GetRichest(pw);
            var possibleShips = Math.Min(pl.NumShips, pl.TotalSheeps);
            IssueOrder(pw, pl, planetForHelp, possibleShips / 2);
        }
    }

    private static void DoAttack(PlanetWars pw)
    {
        for (int i = 0; i < 3; i++)
        {
            // (2) Find my strongest planet.
            Planet source = GetRichestForEcxspansion(pw);
            if (source == null) return;

            // (3) Find the weakest enemy or neutral planet.
            Planet dest = GetToAttack(pw, source);

            // (4) Send half the ships from my strongest planet to the weakest
            // planet that I do not own.
            if (dest != null)
            {
                int halfShips = source.NumShips / 2;
                var needShips = dest.RequiredShips;
                var sendShips = needShips < halfShips ? needShips : halfShips;
                IssueOrder(pw, source, dest, sendShips);
            }
        }
    }

    private static void InitSituation(PlanetWars pw)
    {
        _readyForEcspancion.Clear();
        _needForHelp.Clear();
        log.WriteLine("========== My planets ====");

        foreach (var myPlanet in pw.MyPlanets())
        {
            var id = myPlanet.PlanetID;

            myPlanet.FleetSheeps = pw.Fleets()
                .Where(e => e.DestinationPlanet() == id)
                .Sum(e => e.Owner() == 1 ? e.NumShips() : -e.NumShips());

            if (myPlanet.TotalSheeps > redyForEcsCount)
                _readyForEcspancion.Add(myPlanet);
            if (myPlanet.TotalSheeps <= 0)
                _needForHelp.Add(myPlanet);
            log.WriteLine(string.Format("#{0} Sheeps {1} TotalSheeps {2}",
                myPlanet.PlanetID, myPlanet.NumShips, myPlanet.TotalSheeps));
        }
    }
    private static Planet GetRichest(PlanetWars pw)
    {
        Planet source = null;
        double sourceScore = Double.MinValue;
        foreach (Planet p in pw.MyPlanets())
        {
            double score = (double)p.TotalSheeps;
            if (score > sourceScore)
            {
                sourceScore = score;
                source = p;
            }
        }
        return source;
    }

    private static Planet GetRichestForEcxspansion(PlanetWars pw)
    {
        Planet source = null;
        double sourceScore = Double.MinValue;
        foreach (Planet p in _readyForEcspancion)
        {
            double score = (double)p.NumShips;
            if (score > sourceScore)
            {
                sourceScore = score;
                source = p;
            }
        }
        return source;
    }
    private static Planet GetToAttack(PlanetWars pw, Planet source)
    {
        // (3) Find the weakest enemy or neutral planet.
        Planet dest = null;
        log.WriteLine("========== Enemy planets ====");
        double destScore = Double.MinValue;
        foreach (Planet p in pw.NotMyPlanets())
        {

            int requiredShips = GetPlannedShips(pw, p);
            var distance = pw.Distance(source.PlanetID, p.PlanetID);
            var rate = p.GrowthRate;
            double score = (double)rate / (double)(requiredShips * distance);

            log.WriteLine("Planet ID={0} Score={6} Owner{1} Ships={2} GrowthRate={3} Dist={4} requiredShips={5}",
                p.PlanetID, p.Owner, p.NumShips, rate, distance, requiredShips, score);

            if (score > destScore)
            {
                destScore = score;
                dest = p;
            }
        }
        return dest;
    }

    private static int GetPlannedShips(PlanetWars pw, Planet p)
    {
        var planetId = p.PlanetID;
        var result = p.NumShips;
        foreach (var fleet in pw.Fleets().Where(e => e.DestinationPlanet() == planetId))
        {
            result += fleet.Owner() == 1 ? -fleet.NumShips() : fleet.NumShips();
        }
        return result;
    }

    private static void IssueOrder(PlanetWars pw, Planet source, Planet dest, int numShips)
    {
        log.WriteLine("==============================================");
        log.WriteLine(string.Format("Send from {0} send {1}", source.NumShips, numShips));
        log.WriteLine(string.Format("To ID={0}  Sh={1} GrowthRate{2}", dest.PlanetID, dest.NumShips, dest.GrowthRate));

        pw.IssueOrder(source, dest, numShips);
        if (dest.Owner == 1)
            dest.AddShips(numShips);
        else
            dest.RemoveShips(numShips);
        source.RemoveShips(numShips);


    }


    public static void Main()
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = System.Threading.Thread.CurrentThread.CurrentUICulture;

        using (log = new StreamWriter(File.Open("c:\\log.txt", FileMode.Create)))
        using (var trace = new StreamWriter(File.Open("c:\\seq.txt", FileMode.Create)))
        {

            string line = "";
            string message = "";
            int c;
            try
            {
                while ((c = Console.Read()) >= 0)
                {
                    switch (c)
                    {
                        case '\n':
                            if (line.Equals("go"))
                            {
                                PlanetWars pw = new PlanetWars(message);
                                DoTurn(pw);
                                pw.FinishTurn();
                                message = "";
                            }
                            else
                            {
                                message += line + "\n";
                            }
                            trace.WriteLine(line);
                            trace.Flush();
                            line = "";
                            break;
                        default:
                            line += (char)c;
                            //log.Write(c);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                log.Write(ex.ToString());
            }
        }
    }
}

