using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

using Savchin.Web.UI.PropertyGrid;
using Savchin.Text;
using Savchin.Web.UI;

[assembly: WebResource(Savchin.Web.UI.EmbeddedResources.JsPropertyGrid, Savchin.Web.UI.EmbeddedResources.JavaScript, PerformSubstitution = true)]
[assembly: WebResource(Savchin.Web.UI.EmbeddedResources.CssPropertyGrid, Savchin.Web.UI.EmbeddedResources.Css)]

namespace Savchin.Web.UI
{
    /// <summary>
    /// Summary description for Class1.
    /// </summary>
    [ToolboxBitmap(typeof(PropertyGridControl), "PropertyGrid.bmp")]
    [ToolboxData("<{0}:PropertyGrid runat=server></{0}:PropertyGrid>")]
    [Designer(typeof(PropertyGridDesigner))]
    public sealed class PropertyGridControl : Control, INamingContainer
    {
        #region Designer
        internal class PropertyGridDesigner : System.Web.UI.Design.ControlDesigner
        {
            public override string GetDesignTimeHtml()
            {
                PropertyGridControl pg = this.Component as PropertyGridControl;
                System.IO.StringWriter output = new System.IO.StringWriter();

                try
                {
                    pg.OnInit(EventArgs.Empty);
                    pg.OnLoad(EventArgs.Empty);

                    pg.SelectedObject = pg;
                    pg.OnPreRender(EventArgs.Empty);

                    HtmlTextWriter w = new HtmlTextWriter(output);

                    pg.Render(w);
                }
                catch (Exception ex)
                {
                    output.Write(ex.ToString());
                }

                return output.ToString();
            }
        }

        #endregion

        #region Overrides

        [Browsable(false)]
        public override bool EnableViewState
        {
            get { return base.EnableViewState; }
            set { base.EnableViewState = false; }
        }



        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.Load"/> event.
        /// </summary>
        /// <param name="e">The <see cref="T:System.EventArgs"/> object that contains the event data.</param>
        protected override void OnLoad(EventArgs e)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture =
              System.Threading.Thread.CurrentThread.CurrentUICulture = System.Globalization.CultureInfo.InvariantCulture;
            base.OnLoad(e);

            if (!(Site != null && Site.DesignMode))
            {
                Skinny.Manager.Register(this);
            }
        }


        /// <summary>
        /// Sends server control content to a provided <see cref="T:System.Web.UI.HtmlTextWriter"/> object, which writes the content to be rendered on the client.
        /// </summary>
        /// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter"/> object that receives the server control content.</param>
        protected override void Render(HtmlTextWriter writer)
        {
            if (Visible || (Site != null && Site.DesignMode))
            {
                bool pad = false;
                if (!(Site != null && Site.DesignMode))
                {
                    pad = (Page.Request.Browser.Browser == "IE");
                }
                else
                {
                    pad = true;
                }
                writer.Write(@"<div id=""{0}""{1} class=""PG PG_{0}"">", ClientID, pad ? "" : " style='padding-right:2px'");
                RenderChildren(writer);
                writer.Write("</div>");
            }
        }

        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.PreRender"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> object that contains the event data.</param>
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            if (Skinny.Manager.IsCallBack || Visible == false || DesignMode)
                return;


            ControlHelper.AddCssInclude(Page, typeof(PropertyGridControl), EmbeddedResources.CssPropertyGrid);
            Page.ClientScript.RegisterClientScriptResource(GetType(), EmbeddedResources.JsPropertyGrid);

           

            if (!Page.ClientScript.IsClientScriptBlockRegistered(typeof(PropertyGridControl), "PropertyGrid_style" + ClientID))
            {
                Page.ClientScript.RegisterClientScriptBlock(typeof(PropertyGridControl), "PropertyGrid_style" + ClientID, GetCSS());
            }


            Page.ClientScript.RegisterStartupScript(typeof(PropertyGridControl), ClientID + "_init", GenereatInitScript(), true);
        }

        private string GenereatInitScript()
        {
            List<string> ids = new List<string>();

            foreach (PropertyGridItem pgi in proplist)
            {
                ids.Add("'" + pgi.controlid.Replace(ClientID + "_", string.Empty) + "'");
            }

            List<string> cats = new List<string>();

            foreach (PropertyGridCategory pgc in categories)
            {
                cats.Add("'" + pgc.ClientID.Replace(ClientID + "_cat", string.Empty) + "'");
            }


            int lh = GetFontHeight(fontfamily, fontsize) + 5;

            return string.Format(@"
var {0} = new PropertyGrid('{0}',[{3}],'{1}','{2}',[{4}],'{5}','{6}','{7}',{8},'{9}','{10}','{11}',{12}, '{13}');
{0}.ApplyStyles(document.styleSheets[document.styleSheets.length - 1]);
{0}.SetViewLevel(); 
{0}.UpdateDescription();",
                    ClientID,
                    CSSColor(selcolor),
                    CSSColor(itembgcolor),
                    StringUtil.Join(ids, ","),
                    StringUtil.Join(cats, ","),
                    width,
                    CSSColor(bgcolor),
                    CSSColor(headercolor),
                    lh,
                    CSSColor(color),
                    fontfamily,
                    fontsize,
                    interval,
                    respath);
        }

        #endregion

        #region AJAX calls

        [Skinny.Method]
        public string[] GetValues()
        {
            string[] output = new string[proplist.Count];

            for (int i = 0; i < output.Length; i++)
            {
                output[i] = (proplist[i] as PropertyGridItem).PropertyValue;
            }
            return output;
        }

        [Skinny.Method]
        public string[] SetValue(string id, string val)
        {
            if (!ReadOnly)
            {
                PropertyGridItem pgi = properties[ClientID + "_" + id] as PropertyGridItem;
                pgi.PropertyValue = val;
            }

            return GetValues();
        }

        [Skinny.Method]
        public string[] GetDescription(string id)
        {
            PropertyGridItem pgi = properties[ClientID + "_" + id] as PropertyGridItem;
            PropertyDescriptor pd = pgi.Descriptor;

            string[] output = new string[] { pd.DisplayName + " : " + pd.PropertyType.Name, pd.Description };

            return output;
        }

        #endregion

        #region Properties

        string fontfamily = "Verdana";
        FontUnit fontsize = new FontUnit("8pt");

        Color bgcolor = Color.Gainsboro;
        Color headercolor = Color.DimGray;
        Color color = Color.Black;
        Color itembgcolor = Color.White;
        Color selcolor = Color.LightSteelBlue;

        int width = 300;
        bool isreadonly = false;
        bool showhelp = false;
        int interval = 3000;

        private string respath;
        [DefaultValue(typeof(Color), "Gainsboro")]
        [Category("Appearance")]
        [TypeConverter(typeof(WebColorConverter))]
        public string ResPath
        {
            get { return respath; }
            set { respath = value; }
        }

        [DefaultValue(3000)]
        [Category("Behavior")]
        public int UpdateInterval
        {
            get { return interval; }
            set { interval = value; }
        }

        [DefaultValue(false)]
        [Category("Appearance")]
        public bool ShowHelp
        {
            get { return showhelp; }
            set { showhelp = value; }
        }

        [DefaultValue(false)]
        [Category("Behavior")]
        public bool ReadOnly
        {
            get { return isreadonly; }
            set { isreadonly = value; }
        }

        [DefaultValue(300)]
        [Category("Appearance")]
        public int Width
        {
            get { return width; }
            set { width = value; }
        }

        [DefaultValue(typeof(Color), "Gainsboro")]
        [Category("Appearance")]
        [TypeConverter(typeof(WebColorConverter))]
        public Color BackgroundColor
        {
            get { return bgcolor; }
            set { bgcolor = value; }
        }

        [DefaultValue(typeof(Color), "DimGray")]
        [Category("Appearance")]
        [TypeConverter(typeof(WebColorConverter))]
        public Color HeaderForeColor
        {
            get { return headercolor; }
            set { headercolor = value; }
        }

        [DefaultValue(typeof(Color), "Black")]
        [Category("Appearance")]
        [TypeConverter(typeof(WebColorConverter))]
        public Color ForeColor
        {
            get { return color; }
            set { color = value; }
        }

        [DefaultValue(typeof(Color), "White")]
        [Category("Appearance")]
        [TypeConverter(typeof(WebColorConverter))]
        public Color ItemBackgroundColor
        {
            get { return itembgcolor; }
            set { itembgcolor = value; }
        }

        [DefaultValue(typeof(Color), "LightSteelBlue")]
        [Category("Appearance")]
        [TypeConverter(typeof(WebColorConverter))]
        public Color SelectionColor
        {
            get { return selcolor; }
            set { selcolor = value; }
        }


        [DefaultValue("Verdana")]
        [Category("Appearance")]
        [TypeConverter(typeof(FontConverter.FontNameConverter))]
        public string FontFamily
        {
            get { return fontfamily; }
            set { fontfamily = value; }
        }

        [DefaultValue(typeof(FontUnit), "8pt")]
        [Category("Appearance")]
        //[TypeConverter(typeof(FontConverter.FontUnitConverter))]
        public FontUnit FontSize
        {
            get { return fontsize; }
            set { fontsize = value; }
        }

        #endregion

        #region CSS

        static string CSSColor(Color c)
        {
            return string.Format("#{0:X2}{1:X2}{2:X2}", c.R, c.G, c.B);
        }

        static int GetFontHeight(string family, FontUnit fontsize)
        {
            GraphicsUnit gu = GraphicsUnit.Point;
            float fv = 8;

            switch (fontsize.Unit.Type)
            {
                case UnitType.Cm:
                    gu = GraphicsUnit.Millimeter;
                    fv = (float)fontsize.Unit.Value * 10;
                    break;
                case UnitType.Inch:
                    gu = GraphicsUnit.Inch;
                    fv = (float)fontsize.Unit.Value;
                    break;
                case UnitType.Mm:
                    gu = GraphicsUnit.Millimeter;
                    fv = (float)fontsize.Unit.Value;
                    break;

                case UnitType.Pica:
                    gu = GraphicsUnit.Point;
                    fv = (float)fontsize.Unit.Value * 12;
                    break;
                case UnitType.Pixel:
                    gu = GraphicsUnit.Pixel;
                    fv = (float)fontsize.Unit.Value;
                    break;
                case UnitType.Point:
                    gu = GraphicsUnit.Point;
                    fv = (float)fontsize.Unit.Value;
                    break;
                case UnitType.Em:
                case UnitType.Ex:
                case UnitType.Percentage:
                default:
                    break;
            }

            using (Font fnt = new Font(family, fv, gu))
            {
                int fh = fnt.Height;
                fh += (fh) % 2;
                return fh;
            }
        }

        string GetOperaCSS()
        {
            int fh = GetFontHeight(fontfamily, fontsize);
            int pgwidth = Width; // {0}
            string fontfam = fontfamily; //{1}
            string fntsize = fontsize.ToString(); // {2}
            int widthinner = Width - 2; // {3}
            int lineheight = fh + 6; // {4}
            int padwidth = 18; // {5}
            int lineheightmarge = lineheight + 1; // {6}
            int widthlesspad = widthinner - padwidth; // {7}
            int halfwidth = widthlesspad / 2; // {8}
            int halfwidthless3 = halfwidth - 5; // {9}
            int inputlineheight = lineheight - 4; // {10}

            string bgcol = CSSColor(bgcolor); // {11}
            string hdcol = CSSColor(headercolor); // {12}
            string frcol = CSSColor(color); // {13}
            string itbgcol = CSSColor(itembgcolor); // {14}
            string selcol = CSSColor(selcolor); // {15}



            return string.Format(@"
<style type=""text/css"">
.PG_{16}
{{
  width:{0}px;
}}
.PG_{16} *
{{
  font-family:{1};
  font-size:{2};
  color:{13};
}}
.PGH_{16}, .PGF_{16}, .PGC_{16}, .PGF2_{16}
{{
  border-color: {12};
  background-color:{11};
}}
.PGC_{16} *
{{
  line-height:{4}px;
  height:{4}px;
}}
.PGC_{16} a, .PGC_OPEN_{16}, .PGC_CLOSED_{16}
{{
  width:{5}px;
}}
.PGC_HEAD_{16} span
{{
  color:{12};
}}
.PGI_NONE_{16}, .PGI_CLOSED_{16}, .PGI_OPEN_{16}
{{
  width:{5}px;
  height:{6}px;
}}
.PGI_NAME_{16}, .PGI_VALUE_{16}, .PGI_NAME_SUB_{16}
{{
  width:{8}px;
  background-color:{14};
}}
.PGI_VALUE_{16} a, .PGI_VALUE_{16} select
{{
  width:100%;
}}
.PGI_NAME_SUB_{16} span
{{
  margin-left:{5}px;
}}
.PGI_VALUE_{16} a:hover
{{
  background-color:{15};
}}
.PGI_VALUE_{16} input
{{
  width:{9}px;
  line-height:{10}px;
  height:{10}px;
}}
</style>",

              pgwidth,
              fontfam,
              fontsize,
              widthinner,
              lineheight,
              padwidth,
              lineheightmarge,
              widthlesspad,
              halfwidth,
              halfwidthless3,
              inputlineheight,
              bgcol,
              hdcol,
              frcol,
              itbgcol,
              selcol, ClientID);
        }

        string GetCSS()
        {
            if (Page.Request.Browser.Browser == "Opera")
            {
                return GetOperaCSS();
            }

            return string.Empty;
        }

        #endregion

        #region Object Binding

        object selobj;

        [Browsable(false)]
        public object SelectedObject
        {
            get { return selobj; }
            set
            {
                if (selobj != value)
                {
                    selobj = value;
                    CreateGrid();
                }
            }
        }

        ArrayList proplist = new ArrayList();
        Hashtable properties = new Hashtable();
        List<PropertyGridCategory> categories = new List<PropertyGridCategory>();

        /// <summary>
        /// 
        /// </summary>
        internal int catcounter = 0;
        internal int subcounter = 0;
        int itemcounter = 0;

        /// <summary>
        /// Creates the grid.
        /// </summary>
        internal void CreateGrid()
        {
            if (selobj == null) return;


            Controls.Clear();
            properties.Clear();
            proplist.Clear();
            categories.Clear();

            itemcounter = catcounter = subcounter = 0;

            try
            {
                Controls.Add(new PropertyGridHeader());

                Hashtable cats = CreatePropeties();


                categories.Sort();

                CreateCategories(cats);

                Controls.Add(new PropertyGridFooter());
            }
            catch (Exception ex)
            {
                Util.Log.Error("CreateGrid.PropertyGridControl", ex);
            }
        }

        private void CreateCategories(Hashtable cats)
        {
            HtmlContainerControl cc = new HtmlGenericControl("div");
            cc.ID = "cats";
            Controls.Add(cc);

            foreach (string cat in cats.Keys)
            {
                PropertyGridCategory pgc = new PropertyGridCategory();
                pgc.CategoryName = cat;

                this.categories.Add(pgc);

                cc.Controls.Add(pgc);

                Hashtable i = cats[cat] as Hashtable;

                ArrayList il = new ArrayList(i.Keys);
                il.Sort();

                foreach (string pginame in il)
                {
                    PropertyGridItem pgi = i[pginame] as PropertyGridItem;

                    proplist.Add(pgi);

                    pgc.Controls.Add(pgi);

                    if (pgi.subitems.Count > 0)
                    {
                        SubItems si = new SubItems();
                        pgi.Controls.Add(si);

                        foreach (PropertyGridItem spgi in pgi.subitems)
                        {
                            si.Controls.Add(spgi);

                            proplist.Add(spgi);
                        }
                    }
                }
            }
        }

        private Hashtable CreatePropeties()
        {
            Hashtable cats = new Hashtable();

            foreach (PropertyDescriptor pd in TypeDescriptor.GetProperties(selobj))
            {
                if (!pd.IsBrowsable)
                {
                    continue;
                }

                string category = pd.Category;

                Hashtable mems = cats[category] as Hashtable;
                if (mems == null)
                {
                    cats[category] = mems = new Hashtable();
                }

                PropertyGridItem pgi = new PropertyGridItem(pd);
                pgi.controlid = ClientID + "_" + itemcounter++;

                properties[pgi.controlid] = pgi;

                object o = selobj;
                object subo = null;

                try
                {
                    subo = pd.GetValue(o);
                }
                catch
                { }

                if (pd.Converter.GetPropertiesSupported())
                {
                    foreach (PropertyDescriptor spd in pd.Converter.GetProperties(subo))
                    {
                        if (spd.IsBrowsable)
                        {
                            PropertyGridItem pgsi = new PropertyGridSubItem(spd, pgi);
                            pgsi.controlid = ClientID + "_" + itemcounter++;
                            pgi.subitems.Add(pgsi);

                            properties[pgsi.controlid] = pgsi;
                        }
                    }
                }

                mems.Add(pd.Name, pgi);

            }
            return cats;
        }

        #endregion
    }
}
