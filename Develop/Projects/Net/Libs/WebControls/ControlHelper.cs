using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.Compilation;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Savchin.Core;


namespace Savchin.Web.UI
{

    /// <summary>
    /// ControlHelper
    /// </summary>
    public static class ControlHelper
    {

        /// <summary>
        /// Gets the app base URL.
        /// </summary>
        /// <value>The app base URL.</value>
        public static string AppBaseUrl
        {
            get { return HttpRuntime.AppDomainAppVirtualPath; }
        }

        /// <summary>
        /// Gets the app image URL.
        /// </summary>
        /// <value>The app image URL.</value>
        public static string AppImageUrl
        {
            get { return AppBaseUrl + "/images/"; }
        }

        /// <summary>
        /// Finds first the control.
        /// </summary>
        /// <param name="collection">The collection.</param>
        /// <returns></returns>
        public static Control FindControl<TControl>(ControlCollection collection)
        {
            foreach (Control control in collection)
            {
                if (control is TControl)
                    return control;
            }
            return null;

        }
        /// <summary>
        /// Gets the control HTML.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <returns></returns>
        public static string GetControlHtml(Control control)
        {
            using (var stringWriter = new StringWriter())
            {
                var writer = new HtmlTextWriter(stringWriter);
                control.RenderControl(writer);
                return stringWriter.ToString();
            }
        }

        /// <summary>
        /// Gets the type of the browser.
        /// </summary>
        /// <returns></returns>
        public static BrowserType BrowserType
        {
            get { return HttpContext.Current.Request.GetBrowserType(); }
        }

        /// <summary>
        /// Hides the control.
        /// </summary>
        /// <param name="control">The control.</param>
        public static void HideControl(HtmlControl control)
        {
            control.Attributes.CssStyle.Add(HtmlTextWriterStyle.Display, "none");
        }
        /// <summary>
        /// Shows the control.
        /// </summary>
        /// <param name="control">The control.</param>
        public static void ShowControl(HtmlControl control)
        {
            control.Attributes.CssStyle.Add(HtmlTextWriterStyle.Display, string.Empty);
        }

        /// <summary>
        /// Finds the control.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <param name="controlID">The control ID.</param>
        /// <returns></returns>
        public static Control FindControl(Control control, string controlID)
        {
            Control namingContainer = control;
            Control control3 = null;
            if (control != control.Page)
            {
                while ((control3 == null) && (namingContainer != control.Page))
                {
                    namingContainer = namingContainer.NamingContainer;
                    if (namingContainer == null)
                    {
                        throw new HttpException("DataBoundControlHelper_NoNamingContainer");
                    }
                    control3 = namingContainer.FindControl(controlID);
                }
                return control3;
            }
            return control.FindControl(controlID);
        }





        /// <summary>
        /// Finds the controls.
        /// </summary>
        /// <typeparam name="TControl">The type of the control.</typeparam>
        /// <param name="collection">The collection.</param>
        /// <returns></returns>
        public static List<TControl> FindControls<TControl>(ControlCollection collection)
                   where TControl : class
        {
            var result = new List<TControl>();

            foreach (object control in collection)
            {
                if (control is TControl)
                    result.Add((TControl)control);
            }
            return result;
        }

        /// <summary>
        /// Finds the controls recursive.
        /// </summary>
        /// <typeparam name="TControl">The type of the control.</typeparam>
        /// <param name="collection">The collection.</param>
        /// <param name="result">The result.</param>
        public static void FindControlsRecursive<TControl>(ControlCollection collection, List<TControl> result)
           where TControl : class
        {
            foreach (Control control in collection)
            {
                if (control is TControl)
                {
                    TControl obj = control as TControl;
                    if (!result.Contains(obj))
                        result.Add(obj);
                }

                FindControlsRecursive(control.Controls, result);
            }

        }
        /// <summary>
        /// Creates the CSS link.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="type">The type.</param>
        /// <param name="linkName">Name of the link.</param>
        /// <returns></returns>
        public static void AddCssInclude(Page page, Type type, string linkName)
        {
            AddCssInclude(page, page.ClientScript.GetWebResourceUrl(type, linkName));
        }

        /// <summary>
        /// Adds the CSS include.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="styleUrl">The stryle URL.</param>
        public static void AddCssInclude(Page page, string styleUrl)
        {
            var myHtmlLink = new HtmlLink();
            myHtmlLink.Href = styleUrl;
            myHtmlLink.Attributes.Add("rel", "stylesheet");
            myHtmlLink.Attributes.Add("type", "text/css");
            page.Header.Controls.Add(myHtmlLink);
        }

        /// <summary>
        /// Sets the CSS class.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <param name="cssClass">The CSS class.</param>
        public static void SetCssClass(HtmlControl control, string cssClass)
        {
            control.Attributes.Add("class", cssClass);
        }

        #region Image Urls

        /// <summary>
        /// Gets the full image URL.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="page">The page.</param>
        /// <returns></returns>
        public static string GetFullImageUrl(string url, Page page)
        {
            if (string.IsNullOrEmpty(url))
                return url;
            if (url.StartsWith("~"))
            {
                return url.StartsWith("~/App_Themes/")
                           ? AppBaseUrl + "/App_Themes/" + page.Theme + "/" + url.Substring(13)
                           : page.ResolveUrl(url);
            }

            if (url.StartsWith("http://") || url.StartsWith(AppBaseUrl))
                return url;


            return AppBaseUrl + url;
        }


        /// <summary>
        /// Gets the image full URL.
        /// </summary>
        /// <param name="url">The image URL.</param>
        /// <param name="theme">The theme.</param>
        /// <returns></returns>
        public static string GetThemebleUrl(string url, string theme)
        {
            if (url.StartsWith("~/App_Themes/"))
                return AppBaseUrl + "/App_Themes/" + theme + "/" + url.Substring(13);
            else
                return AppBaseUrl + "/App_Themes/" + theme + "/" + url;
        }
        #endregion

        /// <summary>
        /// Sets the background imager.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <param name="theme">The theme.</param>
        /// <param name="image">The image.</param>
        public static void SetBackgroundImager(HtmlControl control, string theme, string image)
        {
            control.Attributes.CssStyle.Add("background-image", "url(" + GetThemebleUrl(image, theme) + ")");

        }

        /// <summary>
        /// Sets the client visible.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <param name="visible">if set to <c>true</c> [visible].</param>
        public static void SetClientVisible(HtmlControl control, bool visible)
        {
            if (visible)
            {
                control.Attributes.CssStyle.Remove("display");
            }
            else
            {
                control.Attributes.CssStyle.Add("display", "none");
            }
        }



        /// <summary>
        /// Gets the size of the image.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="theme">The theme.</param>
        /// <returns></returns>
        public static SizeF GetImageSize(string filePath, string theme)
        {
            return GetImageSize(HttpContext.Current.Server.MapPath(GetThemebleUrl(filePath, theme)));
        }

        private static Dictionary<string, SizeF> imagesSizes = new Dictionary<string, SizeF>();
        /// <summary>
        /// Gets the size of the image.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        public static SizeF GetImageSize(string fileName)
        {
            if (imagesSizes.ContainsKey(fileName))
            {
                return imagesSizes[fileName];
            }
            if (!File.Exists(fileName))
                return SizeF.Empty;

            using (Image drImage = Image.FromFile(fileName))
            {
                SizeF sizeF = drImage.PhysicalDimension;
                imagesSizes.Add(fileName, sizeF);
                return sizeF;
            }
        }


        /// <summary>
        /// Gets the color of the HTML.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <returns></returns>
        public static string GetHtmlColor(Color color)
        {
            return String.Format("H{0:X}{1:X}{2:X}", color.R, color.G, color.B);
        }

        private static Type _scriptManagerType;
        internal static Type ScriptManagerType
        {
            get
            {
                if (_scriptManagerType == null)
                {
                    _scriptManagerType = BuildManager.GetType("System.Web.UI.ScriptManager", false);
                }
                return _scriptManagerType;
            }
        }


        /// Shows the specified o.
        /// </summary>
        /// <param name="output">The output.</param>
        /// <param name="o">The o.</param>
        public static void Show(this Control output, Object o)
        {
            if (o == null) throw new ArgumentNullException("o");

            var controls = new List<IBindable>();
            FindControlsRecursive<IBindable>(output.Controls, controls);

            var type = o.GetType();
            foreach (var control in controls)
            {
                var propertyName = control.PropertyName;
                if (string.IsNullOrEmpty(propertyName))
                    continue;

                var property = type.GetProperty(propertyName);
                if (property == null)
                {
                    throw new InvalidOperationException("Error databinding. Property not exists " + propertyName);
                }
                control.SetValue(property.GetValue(o, null));
            }
        }

        /// <summary>
        /// Fills the specified o.
        /// </summary>
        /// <param name="output">The output.</param>
        /// <param name="o">The o.</param>
        public static void Fill(this Control output, Object o)
        {
            if (o == null) throw new ArgumentNullException("o");

            var controls = new List<IBindable>();
            FindControlsRecursive<IBindable>(output.Controls, controls);

            var type = o.GetType();
            foreach (IBindable control in controls)
            {
                var propertyName = control.PropertyName;
                if (!control.CanGetValue || string.IsNullOrEmpty(propertyName))
                    continue;


                var property = type.GetProperty(propertyName);
                if (property == null)
                {
                    throw new InvalidOperationException(string.Format("Error bind property {0}. Property not found.",
                                                                      propertyName));
                }
                if (!property.CanRead)
                {
                    throw new InvalidOperationException(string.Format("Error bind property {0}. Property write only.",
                                                                      propertyName));
                }
                var value = control.GetValue();
                try
                {
                    value = property.PropertyType.Convert(value);
                    property.SetValue(o, value, null);
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException(string.Format("Error bind property '{0}'. Can't set value '{1}'.",
                                                                      propertyName, value), ex);
                }
            }
        }



    }
}
