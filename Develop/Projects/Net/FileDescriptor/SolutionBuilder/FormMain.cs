using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;


namespace Savchin.SolutionBuilder
{
    public partial class FormMain : Form
    {
        
        private const string FileName = "devenv.com";
        private Process process;

        public string[] Parts { get; set; }

        public FormMain()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (Parts.Length < 2)
            {
                MessageBox.Show("Неверная коммандная строка");
                return;
            }

            Build(Parts.Length > 2 ? Parts[2] : null, Parts[0], Parts[1]);
        }


        private void Build(string studioPath, string solutionFile, string solutionConfig)
        {

            try
            {
                Text = string.Format("{0} {1}", Text, Path.GetFileNameWithoutExtension(solutionFile));

                var fileName = string.IsNullOrWhiteSpace(studioPath)
                                   ? FileName
                                   : Path.Combine(studioPath.TrimEnd(new[] { '"' }), FileName);
                var arguments = "\"" + solutionFile + "\"" + @" /rebuild " + solutionConfig;


                AddLogLine("Start: " + fileName);
                AddLogLine("Arguments: " + arguments);

                StartBuild(fileName, arguments);
            }
            catch (Exception ex)
            {
                Forms.ExceptionForm.ShowException("Solution builder", "Ошибка старта компиляции", ex);
                ReleaseProcess();
            }

        }

        private void StartBuild(string fileName, string arguments)
        {
            var psi = new ProcessStartInfo
                          {
                              FileName = fileName,
                              ErrorDialog = true,
                              CreateNoWindow = true,
                              Arguments = arguments,
                              RedirectStandardError = true,
                              RedirectStandardOutput = true,
                              UseShellExecute = false
                          };


            process = new Process
                          {
                              EnableRaisingEvents = true,
                              StartInfo = psi
                          };

            process.OutputDataReceived += process_OutputDataReceived;
            process.ErrorDataReceived += process_OutputDataReceived;
            process.Exited += process_Exited;
            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
        }

        void process_Exited(object sender, EventArgs e)
        {
            AddLogLine(string.Format("Finished: {0}", process.ExitCode == 0 ? "succesfuly" : "failure"));
            ReleaseProcess();
        }

        private void ReleaseProcess()
        {
            if (process == null) return;

            process.OutputDataReceived -= process_OutputDataReceived;
            process.ErrorDataReceived -= process_OutputDataReceived;
            process.Exited -= process_Exited;
            process.Close();
            process = null;
        }

        void process_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            AddLogLine(e.Data);
        }
        private void AddLogLine(string text)
        {
            AddLog(text + Environment.NewLine);
        }
        private void AddLog(string text)
        {
            if (textBox1 == null || text == null) return;

            textBox1.AppendText(text);
            Application.DoEvents();
        }
    }
}
