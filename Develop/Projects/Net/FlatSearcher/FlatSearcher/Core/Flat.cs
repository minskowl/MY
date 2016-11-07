using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using WatiN.Core;

namespace MyCustomWebBrowser.Core
{
    public class Flat
    {


        [XmlAttribute]
        public string Id { get; set; }
        public string Address { get; set; }
        public string Square { get; set; }
        public string Price { get; set; }
        [XmlAttribute]
        public string Floor { get; set; }
        [XmlAttribute]
        public int Year { get; set; }
        public string Lng { get; set; }
        public string Lat { get; set; }

        [XmlAttribute]
        public bool Visible { get; set; }
        [XmlAttribute]
        public int Rating { get; set; }

        public string Comments { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="Flat"/> class.
        /// </summary>
        public Flat()
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Flat"/> class.
        /// </summary>
        /// <param name="infos">The infos.</param>
        public Flat(Table[] infos)
        {
            InitMainInfo(infos.FirstOrDefault(t => t.InnerHtml.Contains("Общая информация")));
            InitMapInfo(infos.FirstOrDefault(t => t.InnerHtml.Contains("YMaps.Map")));
        }

        private static readonly Regex _reg = new Regex(@"(?:YMaps\.GeoPoint\()(?'lng'\d+\.\d+),\s(?'lat'\d+\.\d+)");

        private void InitMapInfo(Table t)
        {
            if(t==null) return;

            var map = t.Div("map");
            var m = _reg.Match(map.InnerHtml);
            Lng = m.Groups["lng"].Value;
            Lat = m.Groups["lat"].Value;
        }

        private void InitMainInfo(Table info)
        {
            Id = FindValue(info, "Код объекта");
            Address = FindValue(info, "Адрес");
            Square = FindValue(info, "Плошадь общая");
            Price = FindValue(info, "Цена");
            Floor = FindValue(info, "Этаж");

            int tmp;
            int.TryParse(FindValue(info, "Год постройки"), out tmp);
            Year = tmp;
        }

        private string FindValue(Table info, string key)
        {
            var row = info.TableRows.FirstOrDefault(e => e.InnerHtml.Contains(key));
            return row != null && row.Exists ? row.TableCells[1].Text : null;
        }

        public void GetInfo(Flat flat)
        {
            Address = flat.Address;
            Square = flat.Square;
            Price = flat.Price;
            Floor = flat.Floor;
            Lat = flat.Lat;
            Lng = flat.Lng;
        }
    }
}
