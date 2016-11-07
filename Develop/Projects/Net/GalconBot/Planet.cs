using System;

public class Planet
{
    // Initializes a planet.
    public Planet(int planetID, int owner, int numShips, int growthRate, double x, double y)
    {
        this.PlanetID = planetID;
        this.Owner = owner;
        this.NumShips = numShips;
        this.GrowthRate = growthRate;
        this.x = x;
        this.y = y;
    }

    // Accessors and simple modification functions. These should be mostly
    // self-explanatory.

    public int PlanetID { get; private set; }
    public int Owner { get; private set; }
    public int GrowthRate { get; private set; }
    public int NumShips { get; private set; }
    public int RequiredShips
    {
        get { return Math.Abs(TotalSheeps)  + 10; }
    }
    public int TotalSheeps
    {
        get { return NumShips + FleetSheeps; }
    }
    public int FleetSheeps { get; set; }
    public double X
    {
        get { return x; }
    }

    public double Y
    {
        get { return y; }
    }

    public void ChangeOwner(int newOwner)
    {
        this.Owner = newOwner;
    }

    public void ChangeNumShips(int newNumShips)
    {
        this.NumShips = newNumShips;
    }

    public void AddShips(int amount)
    {
        NumShips += amount;
    }

    public void RemoveShips(int amount)
    {
        NumShips -= amount;
    }

    private double x, y;

    private Planet(Planet _p)
    {
        PlanetID = _p.PlanetID;
        Owner = _p.Owner;
        NumShips = _p.NumShips;
        GrowthRate = _p.GrowthRate;
        x = _p.x;
        y = _p.y;
    }
}
