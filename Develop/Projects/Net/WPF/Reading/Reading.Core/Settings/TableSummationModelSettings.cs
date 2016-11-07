namespace Reading.Core.Settings
{
    public enum SummationMode
    {
        Summation,
        Subtraction,
        All
    }

    public enum TableSummationMode
    {
        Text,
        Questions
    }

    public class TableSummationModelSettings
    {
        public int FirstNumberFrom { get; set; }
        public int FirstNumberTo { get; set; }
        public int SecondNumberFrom { get; set; }
        public int SecondNumberTo { get; set; }
        public SummationMode Operation { get; set; }
        public TableSummationMode Mode { get; set; }

        public TableSummationModelSettings()
        {
            Operation=SummationMode.Summation;
            FirstNumberFrom = 1;
            FirstNumberTo = 2;
            SecondNumberFrom = 1;
            SecondNumberTo = 9;
        }

    }
}