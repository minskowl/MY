using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using NantRunner.Core;

namespace NantRunner
{
    public partial class FormTask : Form
    {
        private Task task;
        private Settings settings;
        private Process processNant;

        public FormTask()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Starts the task.
        /// </summary>
        /// <param name="task">The task.</param>
        /// <param name="settings">The settings.</param>
        public static void StartTask(Task task, Settings settings)
        {
            FormTask form = new FormTask();
            form.task = task;
            form.settings = settings;
            form.Show();
        }

        /// <summary>
        /// Handles the Load event of the FormTask control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void FormTask_Load(object sender, EventArgs e)
        {
            Text = task.Name;


            processNant = CreateNantProcess(settings, task);
            processNant.OutputDataReceived += OutputDataReceived;
            processNant.Exited += ProcessStoped;

            Text = "[Runnig] " + task.Name;

        }

        #region Process Managment
        /// <summary>
        /// Processes the stoped.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ProcessStoped(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;

            if (textBoxLog.Text.LastIndexOf("BUILD SUCCEEDED") == -1)
                Text = "[Finished Failed] ";
            else
                Text = "[Finished OK] ";
            Text += task.Name;
        }

        /// <summary>
        /// Outputs the data received.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Diagnostics.DataReceivedEventArgs"/> instance containing the event data.</param>
        private void OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            textBoxLog.AppendText(e.Data + Environment.NewLine);
        }

        /// <summary>
        /// Runs the specified settings.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <param name="task">The task.</param>
        private static Process CreateNantProcess(Settings settings, Task task)
        {
            // Initialize the process and its StartInfo properties.
            // The sort command is a console application that
            // reads and sorts text input.

            Process taskProcess = new Process();

            taskProcess.EnableRaisingEvents = true;
            taskProcess.StartInfo.FileName = settings.NantPath + "Nant.exe";
            taskProcess.StartInfo.Arguments = " -buildfile:" + Path.GetFileName(task.FilePath) + " " + task.TargetName;
            taskProcess.StartInfo.WorkingDirectory = Path.GetDirectoryName(task.FilePath);
            taskProcess.StartInfo.CreateNoWindow = true;
            // Set UseShellExecute to false for redirection.
            taskProcess.StartInfo.UseShellExecute = false;

            // Redirect the standard output of the sort command.  
            // This stream is read asynchronously using an event handler.
            taskProcess.StartInfo.RedirectStandardOutput = true;


            // Set our event handler to asynchronously read the sort output.

            // Redirect standard input as well.  This stream
            // is used synchronously.
            //taskProcess.StartInfo.RedirectStandardInput = true;

            // Start the process.
            taskProcess.Start();

            taskProcess.BeginOutputReadLine();

            return taskProcess;
        }
        #endregion

    }
}