using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Media;
using Bashni.Game;
using WatiN.Core;

namespace Bashni.Core
{
    internal class GameController : IDisposable
    {
        private IE browser;
        private ILogger _logger;
        /// <summary>
        /// Gets the browser.
        /// </summary>
        /// <value>The browser.</value>
        public IE Browser
        {
            get { return browser; }
        }

        public GameController(ILogger logger)
        {
            _logger = logger;
            browser = IE.AttachToIE(Find.ByTitle("Саровские башни"));
            browser.AutoClose = false;
            Trace("Find browser");
        }

        public void Dispose()
        {
            browser.Dispose();
            browser = null;
            _logger = null;
        }

        public Table GetField()
        {
            return browser.Table(Find.ByClass("bigcell"));
        }


        public Session BuildField()
        {
            var parser = new BrickParser();

            var table = GetField();

            var field = Field.Create(table.TableRows.Count, table.TableRows[0].TableCells.Count);
            if (!table.Exists)
            {
                return null;
            }
            Trace("Find field");
            var rowIndex = 0;
            var columnIndex = 0;
            foreach (TableRow row in table.TableRows)
            {
                columnIndex = 0;
                foreach (TableCell cell in row.TableCells)
                {

                    parser.SetElement(cell.ElementWithTag("dd", null));

                    if (!parser.IsBrick) continue;

                    field[rowIndex, columnIndex] = parser.GetBrick();

                    columnIndex++;
                }
                rowIndex++;
            }

            Trace("Build session");

            return new Session(field, parser.Colors.ToList());
        }

        private void Trace(string text)
        {
            if (_logger != null)
                _logger.AddEntry(LogType.Info, text);
        }


        public Navigator GetNavigator()
        {
            return new Navigator(GetField());
        }

        public class Navigator
        {
            private TableRow _header;

            public Navigator(Table field)
            {
                _header = field.TableRows[0];
            }

            public void ViewInBrowser( Step node)
            {
                if (node.Previous != null)
                    ViewInBrowser( node.Previous);

                if (node.Move != null)
                    DoMove(node.Move);

            }

            public void DoMove(Movement move)
            {
                var from = _header.TableCells[move.From.Column];
                Click(from);
                var to = _header.TableCells[move.To];
                Click(to);
            }

            private void Click(TableCell cell)
            {
                var el = cell.ElementWithTag("dd", null);
                el.Click();
            }
        }


    }
}
