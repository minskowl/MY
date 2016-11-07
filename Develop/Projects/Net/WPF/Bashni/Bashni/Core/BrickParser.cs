using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Media;
using Bashni.Game;
using WatiN.Core;

namespace Bashni.Core
{
    class BrickParser
    {

        private Element _data;

        private NameValueCollection _style;
        private Regex _regexStyle = new Regex(@"(?<name>[\w\-]*):\s*(?<value>[^\:\;]*);");
        private Regex _regexColor = new Regex(@"\((?<r>\d+)\s*,\s*(?<g>\d+)\s*,\s*(?<b>\d+)\s*\)");
        public IEnumerable<Color> Colors
        {
            get
            {
                return from c in _colors
                       select _regexColor.Match(c) into m
                       let r = byte.Parse(m.Groups["r"].Value)
                       let g = byte.Parse(m.Groups["g"].Value)
                       let b = byte.Parse(m.Groups["b"].Value)
                       select Color.FromRgb(r, g, b);
            }
        }
        private List<string> _colors = new List<string>();
        public void SetElement(Element e)
        {
            _data = e;
            _style = null;
        }

        public string GetStyle(string key)
        {
            if (_style == null)
            {
                _style = new NameValueCollection();
                var mathes = _regexStyle.Matches(_data.GetAttributeValue("style"));
                foreach (Match m in mathes)
                {
                    _style.Add(m.Groups["name"].Value.Trim(), m.Groups["value"].Value.Trim());
                }
            }
            return _style[key];
        }


        public bool IsBrick
        {
            get { return _data.ClassName == "bricks"; }
        }

        public Brick GetBrick()
        {
            var color = GetStyle("background-color");
            if (!_colors.Contains(color))
                _colors.Add(color);
            var widthText = GetStyle("width");
            var width = int.Parse(widthText.Replace("px", string.Empty).Trim());

            return new Brick
                       {
                           Color = _colors.IndexOf(color),
                           Width = ((width - 35) / 4) + 1
                       };
        }

    }
}
