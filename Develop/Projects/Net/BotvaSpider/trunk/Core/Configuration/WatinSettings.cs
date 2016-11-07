using System;
using System.ComponentModel;
using System.Xml.Serialization;
using BotvaSpider.Core;
using WatiN.Core;
using WatiN.Core.Interfaces;

namespace BotvaSpider.Configuration
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class WatinSettings
    {

        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether [attach to browser].
        /// </summary>
        /// <value><c>true</c> if [attach to browser]; otherwise, <c>false</c>.</value>
        [XmlAttribute]
        public bool AttachToBrowser { get; set; }
        /// <summary>
        /// Gets or sets the attach to IE time out.
        /// </summary>
        /// <value>The attach to IE time out.</value>
        public int AttachToIETimeOut { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [auto close dialogs].
        /// </summary>
        /// <value><c>true</c> if [auto close dialogs]; otherwise, <c>false</c>.</value>
        public bool AutoCloseDialogs { get; set; }

        /// <summary>
        /// Gets or sets the wait for complete time out.
        /// </summary>
        /// <value>The wait for complete time out.</value>
        public int WaitForCompleteTimeOut { get; set; }

        /// <summary>
        /// Gets or sets the wait until exists time out.
        /// </summary>
        /// <value>The wait until exists time out.</value>
        public int WaitUntilExistsTimeOut { get; set; }

 #endregion



        public static WatinSettings Create()
        {
            var result = new WatinSettings
                             {
                                 AttachToIETimeOut = 5,
                                 AutoCloseDialogs = Settings.AutoCloseDialogs,
                                 WaitForCompleteTimeOut = 120,
                                 WaitUntilExistsTimeOut = 20,
                             };

            return result;
        }
        /// <summary>
        /// Setups this instance.
        /// </summary>
        public void Setup()
        {

            Settings.AutoMoveMousePointerToTopLeft = false;
            Settings.AttachToIETimeOut = AttachToIETimeOut;
            Settings.AutoCloseDialogs = AutoCloseDialogs;
            Settings.WaitForCompleteTimeOut = WaitForCompleteTimeOut;
            Settings.WaitUntilExistsTimeOut = WaitUntilExistsTimeOut;
        }

        /// <summary>
        /// Creates the browser.
        /// </summary>
        /// <returns></returns>
        public IE CreateBrowser()
        {
   
            if ( AttachToBrowser)
            {
                var result = TryToAttach();
                if (result != null) return result;

            }

            return new IE();


        }

        private IE TryToAttach()
        {
            try
            {
                return IE.AttachToIE(Find.ByTitle("Ботва Онлайн - бесплатная онлайн игра"));
            }
            catch (Exception ex)
            {
                AppCore.LogFights.Warn("Несмогли присоедининться к существующему IE ", ex);
                return null;
            }
        }
    }
}