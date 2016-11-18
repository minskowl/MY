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

    public class FindPairSettings
    {
        public int Rows { get; set; }
        public int Columns { get; set; }
        public int Images { get; set; }

        public FindPairSettings()
        {
            Rows = 4;
            Columns = 4;
            Images = 8;
        }
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
            Operation = SummationMode.Summation;
            FirstNumberFrom = 1;
            FirstNumberTo = 2;
            SecondNumberFrom = 1;
            SecondNumberTo = 9;
        }

    }
}