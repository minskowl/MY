using System;
using System.Collections.Generic;
using System.Text;
using NAnt.Core;
using NAnt.Core.Attributes;
using NAnt.Savchin.Tasks.Core;

namespace NAnt.Savchin.Tasks
{


    [TaskName("impersonate")]
    public class ImpersonateTask : Task
    {

        #region Public Instance Properties

        /// <summary>
        /// Gets or sets the run block.
        /// </summary>
        /// <value>The run block.</value>
        [BuildElement("run", Required = true)]
        public TaskContainer RunBlock { get; set; }


        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="StartProcessTask"/> is domain.
        /// </summary>
        /// <value><c>true</c> if domain; otherwise, <c>false</c>.</value>
        [TaskAttribute("domain", Required = false)]
        [StringValidator(AllowEmpty = true)]
        public string Domain { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [user name].
        /// </summary>
        /// <value><c>true</c> if [user name]; otherwise, <c>false</c>.</value>
        [TaskAttribute("username", Required = true)]
        [StringValidator(AllowEmpty = false)]
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [user name].
        /// </summary>
        /// <value><c>true</c> if [user name]; otherwise, <c>false</c>.</value>
        [TaskAttribute("password", Required = false)]
        [StringValidator(AllowEmpty = true)]
        public string Password { get; set; }

        #endregion Public Instance Properties

     
        /// <summary>
        /// Executes the task.
        /// </summary>
        protected override void ExecuteTask()
        {
            using (new Impersonator(Domain, UserName, Password))
            {
                if (RunBlock != null)
                {
                    RunBlock.Execute();
                }
            }
          
        }



     
    }
}
