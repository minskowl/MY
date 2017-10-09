using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

namespace PackageMatcher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Package[] _packages = new Package[]
        {
            new Package {
            Heroes = new[]
            {
                new Hero
                {
                    Name = "Гаргул",
                    Evo = 1,
                    Required = true
                },
                new Hero
                {
                    Name = "Тыква",
                    Evo = 1,
                    Required = true
                },
                new Hero
                {
                    Name = "Череп",
                    Evo = 1,
                    Required = true
                },
                new Hero
                {
                    Name = "Душегуб",
                    Alias = "Душик",
                    Evo = 1,
                    Required = true
                },
                new Hero
                {
                    Name = "Смерть",
                    Evo = 1,

                },
                new Hero
                {
                    Name = "Дед",
                    Evo = 1,

                },
                new Hero
                {
                    Name = "Шапка",
                    Evo = 1,
                },
}
            }

        };


        public MainWindow()
        {
            InitializeComponent();


        }

        private SharedStringTable _sharedStringTable;
        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var heroes = ReadHeroes(@"C:\Users\Dmitry.Savchin\Downloads\Герои.xlsx");
            for (int i = 0; i < _packages.Length; i++)
            {
                MessageBox.Show("Package " + (i + 1) + " is match " + IsMatch(heroes, _packages[i]));
            }

        }

        private double IsMatch(List<Hero> heroes, Package package)
        {
            int matched = 0;
            foreach (var required in package.Heroes)
            {
                var exists =
                    heroes.FirstOrDefault(
                        e =>
                            string.Equals(e.Name, required.Name, StringComparison.OrdinalIgnoreCase) ||
                            string.Equals(e.Name, required.Alias, StringComparison.OrdinalIgnoreCase));

                if (exists == null)
                {
                    if (required.Required)
                    {
                        MessageBox.Show($"Reqired not found {required}");

                        return 0.0;
                    }

                    continue;
                }

                if (required.Evo > exists.Evo || required.Skill > exists.Skill || required.Level > exists.Level)
                {
                    MessageBox.Show($"Not matched {required}");
                    continue;
                }


                matched++;
            }

            return matched / 5f;
        }

        private List<Hero> ReadHeroes(string fileName)
        {
            //var book =new HSSFWorkbook(new POIFSFileSystem()));
            List<Hero> res = new List<Hero>();
            using (SpreadsheetDocument spreadsheetDocument =
                SpreadsheetDocument.Open(fileName, false))
            {
                var worksheetPart = spreadsheetDocument.WorkbookPart.WorksheetParts.First();
                _sharedStringTable =
                    spreadsheetDocument.WorkbookPart.GetPartsOfType<SharedStringTablePart>().First().SharedStringTable;

                var sheet = worksheetPart.Worksheet;
                var data = sheet.Elements<SheetData>().First();
                foreach (Row r in data.Elements<Row>())
                {
                    var hero = GetValue(r);
                    if (hero != null)
                        res.Add(hero);
                }

                // Code removed here.
            }
            return res;
        }

        private Hero GetValue(Row row)
        {
            try
            {
                return new Hero
                {
                    Owner = GetValue<string>(row, "A"),
                    Name = GetValue<string>(row, "B"),
                    Evo = (int?)GetValue<decimal?>(row, "C"),
                    Level = (int)GetValue<decimal>(row, "D"),
                    Skill = (int)GetValue<decimal>(row, "E")
                };
            }
            catch (Exception)
            {
                return null;
            }
        }

        private int GetInt(Cell theCell)
        {
            return Convert.ToInt32(GetValue(theCell));
        }

        private T GetValue<T>(Row row, string c)
        {
            return (T)GetValue(row.Elements<Cell>().FirstOrDefault(e => e.CellReference.Value.StartsWith(c)));
        }

        private object GetValue(Cell theCell)
        {
            string value = null;
            // If the cell does not exist, return an empty string.
            if (theCell == null) return null;


            value = theCell.InnerText;


            if (theCell.DataType == null)
            {
                if (string.IsNullOrWhiteSpace(value))
                    return 0m;

                return Decimal.Parse(value.Replace('.', ','));
            }


            switch (theCell.DataType.Value)
            {
                case CellValues.SharedString:
                    return _sharedStringTable.ElementAt(int.Parse(value)).InnerText;


                case CellValues.Boolean:
                    switch (value)
                    {
                        case "0":
                            return false;

                        default:
                            return true;
                    }

            }

            return value;


        }
    }
}
