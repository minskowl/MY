namespace AoM.Viewer.Data
{
    public class Craft
    {
        public int Count;
        public string Name;

        public override string ToString()
        {
            return $"x{Count} {Name}";
        }
    }
}
