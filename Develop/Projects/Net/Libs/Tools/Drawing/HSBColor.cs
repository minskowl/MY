using System;
using System.Drawing;

namespace Savchin.Drawing
{
    public enum ColorSheme
    {
        RGB,
        HSB
    }
    
    public struct HSBColor
    {
        int h;
        int s;
        int b;
        int a;

        public HSBColor(int h, int s, int b)
        {
            this.a = 0xff;
            this.h = Math.Min(Math.Max(h, 0), 255);
            this.s = Math.Min(Math.Max(s, 0), 255);
            this.b = Math.Min(Math.Max(b, 0), 255);
        }

        public HSBColor(int a, int h, int s, int b)
        {
            this.a = a;
            this.h = Math.Min(Math.Max(h, 0), 255);
            this.s = Math.Min(Math.Max(s, 0), 255);
            this.b = Math.Min(Math.Max(b, 0), 255);
        }

        public HSBColor(Color color)
        {
            HSBColor temp = FromColor(color);
            this.a = temp.a;
            this.h = temp.h;
            this.s = temp.s;
            this.b = temp.b;
        }

        public int H
        {
            get { return h; }
        }

        public int S
        {
            get { return s; }
        }

        public int B
        {
            get { return b; }
        }

        public int A
        {
            get { return a; }
        }

        public Color Color
        {
            get
            {
                return FromHSB(this);
            }
        }

        public static Color ShiftHue(Color c, int hueDelta)
        {
            HSBColor hsb = FromColor(c);
            hsb.h += hueDelta;
            hsb.h = Math.Min(Math.Max(hsb.h, 0), 255);
            return FromHSB(hsb);
        }

        public static Color ShiftSaturation(Color c, int saturationDelta)
        {
            HSBColor hsb = FromColor(c);
            hsb.s += saturationDelta;
            hsb.s = Math.Min(Math.Max(hsb.s, 0), 255);
            return FromHSB(hsb);
        }


        public static Color ShiftBrighness(Color c, int brightnessDelta)
        {
            HSBColor hsb = FromColor(c);
            hsb.b += brightnessDelta;
            hsb.b = Math.Min(Math.Max(hsb.b, 0), 255);
            return FromHSB(hsb);
        }
        
        public static  Color FromHSB(int hue, int saturation, int black)
        {
            float r = black;
            float g = black;
            float b = black;
            if (saturation != 0)
            {
                float max = black;
                float dif = black * saturation / 255f;
                float min = black - dif;

                float h = hue * 360f / 255f;

                if (h < 60f)
                {
                    r = max;
                    g = h * dif / 60f + min;
                    b = min;
                }
                else if (h < 120f)
                {
                    r = -(h - 120f) * dif / 60f + min;
                    g = max;
                    b = min;
                }
                else if (h < 180f)
                {
                    r = min;
                    g = max;
                    b = (h - 120f) * dif / 60f + min;
                }
                else if (h < 240f)
                {
                    r = min;
                    g = -(h - 240f) * dif / 60f + min;
                    b = max;
                }
                else if (h < 300f)
                {
                    r = (h - 240f) * dif / 60f + min;
                    g = min;
                    b = max;
                }
                else if (h <= 360f)
                {
                    r = max;
                    g = min;
                    b = -(h - 360f) * dif / 60 + min;
                }
                else
                {
                    r = 0;
                    g = 0;
                    b = 0;
                }
            }

            return Color.FromArgb
                (
                    (int)Math.Round(Math.Min(Math.Max(r, 0), 255)),
                    (int)Math.Round(Math.Min(Math.Max(g, 0), 255)),
                    (int)Math.Round(Math.Min(Math.Max(b, 0), 255))
                    );
        }
        public static Color FromHSB(HSBColor hsbColor)
        {
            return FromHSB(hsbColor.h,hsbColor.s,hsbColor.b);
        }

        public static HSBColor FromColor(Color color)
        {
            HSBColor ret = new HSBColor(0, 0, 0);
            ret.a = color.A;

            int r = color.R;
            int g = color.G;
            int b = color.B;

            int max = Math.Max(r, Math.Max(g, b));

            if (max <= 0)
            {
                return ret;
            }

            int min = Math.Min(r, Math.Min(g, b));
            int dif = max - min;

            if (max > min)
            {
                if (g == max)
                {
                    ret.h =Convert.ToInt32( (b - r) / dif * 60f + 120f);
                }
                else if (b == max)
                {
                    ret.h = Convert.ToInt32( (r - g) / dif * 60f + 240f);
                }
                else if (b > g)
                {
                    ret.h = Convert.ToInt32( (g - b) / dif * 60f + 360f);
                }
                else
                {
                    ret.h = Convert.ToInt32( (g - b) / dif * 60f);
                }
                if (ret.h < 0)
                {
                    ret.h = Convert.ToInt32( ret.h + 360f);
                }
            }
            else
            {
                ret.h = 0;
            }

            ret.h = Convert.ToInt32( ret.h * 255f / 360f);
            ret.s =Convert.ToInt32(  (dif / max) * 255f);
            ret.b = max;

            return ret;
        }
    }
}
