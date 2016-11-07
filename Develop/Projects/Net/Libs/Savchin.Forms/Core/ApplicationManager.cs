using System;
using System.IO;
using System.Windows.Forms;
using Savchin.Core;


namespace Savchin.Forms.Core
{
    public class ApplicationManager<TSettings> : IDisposable
    {
        private const string settingsFileName = "def.cfg";
        private TSettings settings;
        private Form mainForm;

        /// <summary>
        /// Initializes a new instance of the <see cref="Application&lt;TSettings&gt;"/> class.
        /// </summary>
        /// <param name="mainForm">The main form.</param>
        public ApplicationManager(Form mainForm)
        {
            this.mainForm = mainForm;

            try
            {
                settings = TypeSerializer<TSettings>.FromXmlFile(settingsFileName);
            }
            catch (FileNotFoundException)
            {
                settings = Activator.CreateInstance<TSettings>();
            }


            this.mainForm.Closing += mainForm_Closing;
        }

        /// <summary>
        /// Gets the settings.
        /// </summary>
        /// <value>The settings.</value>
        public TSettings Settings
        {
            get { return settings; }
        }


        /// <summary>
        /// Handles the Closing event of the mainForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        void mainForm_Closing(object sender, global::System.ComponentModel.CancelEventArgs e)
        {
            TypeSerializer<TSettings>.ToXmlFile(settingsFileName, Settings);
        }



        ///<summary>
        ///Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        ///</summary>
        ///<filterpriority>2</filterpriority>
        public void Dispose()
        {

            this.mainForm.Closing -= mainForm_Closing;

        }
    }
}