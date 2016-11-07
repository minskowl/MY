using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MyCustomWebBrowser.Core;
using NPOI.HPSF;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using Savchin.Forms.Core.Commands;
using Savchin.Logging;

namespace FlatSearcher.Core
{
    public class ExportToExcelCommand : Command
    {
        #region Overrides of Command

        /// <summary>
        /// Executes this command.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <param name="target">The target.</param>
        public override void Execute(object parameter, object target)
        {
            try
            {
                DoExport(parameter);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка экспорта в Excel");
                SearchContext.Current.Log.AddMessage(Severity.Error, "Ошибка экспорта в Excel", ex);
            }
        }

        private static void DoExport(object parameter)
        {
            using (var d = new SaveFileDialog())
            {
                if (d.ShowDialog() != DialogResult.OK) return;

                using (var hssfworkbook = GeneraeWorkBook((IMap)parameter))
                using (var file = new FileStream(d.FileName, FileMode.Create))
                {
                    hssfworkbook.Write(file);
                }
            }
        }

        private static HSSFWorkbook GeneraeWorkBook(IMap map)
        {
            var hssfworkbook = new HSSFWorkbook();
            ////create a entry of DocumentSummaryInformation
            DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
            dsi.Company = "NPOI Team";
            hssfworkbook.DocumentSummaryInformation = dsi;

            ////create a entry of SummaryInformation
            SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
            si.Subject = "NPOI SDK Example";
            hssfworkbook.SummaryInformation = si;


            ISheet sheet = hssfworkbook.CreateSheet("Timesheet");
            IPrintSetup printSetup = sheet.PrintSetup;
            printSetup.Landscape = true;
            sheet.FitToPage = (true);
            sheet.HorizontallyCenter = (true);

            //title row
            IRow titleRow = sheet.CreateRow(0);
            titleRow.HeightInPoints = (35);

            titleRow.CreateCell(0, "#");
            titleRow.CreateCell(1, "Адресс");
            titleRow.CreateCell(2, "Площадь");
            titleRow.CreateCell(3, "Цена");
            titleRow.CreateCell(4, "Этаж");
            titleRow.CreateCell(5, "Год");
            titleRow.CreateCell(6, "Отображать");
            titleRow.CreateCell(7, "B районе");


            var flats = SearchContext.Current.Data.Flats;
            for (int i = 0; i < flats.Count; i++)
            {
                IRow row = sheet.CreateRow(i + 1);
                row.HeightInPoints = (30);

                var f = flats[i];

                row.CreateCell(0, f.Id);
                row.CreateCell(1, f.Address);
                row.CreateCell(2, f.Square);
                row.CreateCell(3, f.Price);
                row.CreateCell(4, f.Floor);
                row.CreateCell(5, f.Year.ToString());
                row.CreateCell(6, f.Visible.ToString());
                if (!string.IsNullOrWhiteSpace(f.Lat) && !string.IsNullOrWhiteSpace(f.Lng))
                    row.CreateCell(7, map.IsInRegion(f.Lng, f.Lat).ToString());
            }
            return hssfworkbook;
        }

        #endregion
    }
}
