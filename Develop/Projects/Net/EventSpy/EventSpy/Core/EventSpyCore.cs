using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Savchin.EventSpy.Consoles;

namespace Savchin.EventSpy.Core
{
    /// <summary>
    /// EventSpyCore
    /// </summary>
    static class EventSpyCore
    {
        public static readonly log4net.ILog LogApp = log4net.LogManager.GetLogger("App");
 
        public static readonly EventListenerManager EventListenerManager = new EventListenerManager();
        public static  readonly List<AppDomain> Domains = new List<AppDomain>();
        private static ExplorerForm mainForm;
        public static readonly StartUpForm StartUpForm = new StartUpForm();

        /// <summary>
        /// Gets or sets the explore form.
        /// </summary>
        /// <value>The explore form.</value>
        public static Form ExploreForm { get; set; }


        /// <summary>
        /// Gets the main form.
        /// </summary>
        /// <value>The main form.</value>
        public static ExplorerForm MainForm
        {
            get { return mainForm; }
            set { mainForm = value; }
        }

        /// <summary>
        /// Gets or sets the output.
        /// </summary>
        /// <value>The output.</value>
        public static ILog Output { get; set; }
    }
}
