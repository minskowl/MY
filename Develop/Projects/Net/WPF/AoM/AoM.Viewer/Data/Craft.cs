namespace AoM.Viewer.Data
{
    public class Craft
    {
        public int Count { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return $"x{Count} {Name}";
        }
    }
}
