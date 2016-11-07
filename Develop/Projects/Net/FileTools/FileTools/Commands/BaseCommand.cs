using System;
using FileTools.Core;
using Savchin.Forms.Core.Commands;

namespace FileTools.Commands
{
    public abstract class BaseCommand : Command
    {
        protected  readonly ILog log;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseCommand"/> class.
        /// </summary>
        /// <param name="log">The log.</param>
        protected BaseCommand(ILog log)
        {
            this.log = log;
        }



        /// <summary>
        /// Adds the log.
        /// </summary>
        /// <param name="message">The message.</param>
        protected void AddLog(string message)
        {
            log.AddLog(message);
        }


    }
}
