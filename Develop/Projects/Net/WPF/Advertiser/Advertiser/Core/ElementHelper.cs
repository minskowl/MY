using System;
using System.Threading;
using System.Windows;
using Savchin.Wpf.Controls.Windows;
using WatiN.Core;
using WatiN.Core.Constraints;
using WatiN.Core.DialogHandlers;

namespace Advertiser.Controllers
{
    static class ElementHelper
    {
        public static void SetCaptcha(this IElementContainer element, string name)
        {
            var captcha = element.TextField(Find.ByName(name));
            var f = new TextWindow();
            f.Title = "Введите капчу";
            f.CanCancel = false;
            do
            {
                captcha.Focus();
            } while ((f.ShowDialog() ?? false) == false || string.IsNullOrWhiteSpace(f.Value));

            captcha.Value = f.Value;
        }
        /// <summary>
        /// Selects the option.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        public static void SelectOption(this IElementContainer element, string name, string value)
        {
            element.SelectList(Find.ByName(name)).SelectByValue(value);
        }
        /// <summary>
        /// Sets the text.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        public static void SetText(this IElementContainer element, string name, string value)
        {
            element.TextField(Find.ByName(name)).Value = value;
        }
        public static void SetTextById(this IElementContainer element, string id, string value)
        {
            element.TextField(id).Value = value;
        }

        /// <summary>
        /// Gets the form by action.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="action">The action.</param>
        /// <returns></returns>
        public static Form GetFormByAction(this IElementContainer element, string action)
        {
            return element.Form(Find.ByElement(e => e.GetAttributeValue("action") == action));
        }
        /// <summary>
        /// Gets the form by action start.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="action">The action.</param>
        /// <returns></returns>
        public static Form GetFormByActionStart(this IElementContainer element, string action)
        {
            return element.Form(Find.ByElement(e => e.GetAttributeValue("action") != null && e.GetAttributeValue("action").StartsWith(action)));
        }
        /// <summary>
        /// Finds the element.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="tag">The tag.</param>
        /// <param name="constraint">The constraint.</param>
        /// <returns></returns>
        public static ElementContainer<Element> FindElement(this IElementContainer element, string tag, AttributeConstraint constraint)
        {
            return ElementFactory.CreateUntypedElement(element.DomContainer, element.ElementWithTag(tag, constraint).NativeElement);
        }
        /// <summary>
        /// Finds the element.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="tag">The tag.</param>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public static ElementContainer<Element> FindElement(this IElementContainer element, string tag, string id)
        {
            return element.FindElement(tag, Find.ById(id));
        }
        /// <summary>
        /// Sets the upload file.
        /// </summary>
        /// <param name="elUpload">The el upload.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="watcher">The watcher.</param>
        public static void SetUploadFile(this Element elUpload, string fileName, DialogWatcher watcher)
        {
            using (new UseDialogOnce(watcher, new FileUploadDialogHandler(fileName)))
            {
                elUpload.Click();
            }
        }

        /// <summary>
        /// Does the async browser.
        /// </summary>
        /// <param name="action">The action.</param>
        public static void DoAsyncBrowser(this ThreadStart action)
        {
            var thread = new Thread(action);
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }


    }
}
