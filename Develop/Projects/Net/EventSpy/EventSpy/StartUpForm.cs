using System;
using System.IO;
using System.Reflection;
using System.Security;
using System.Security.Policy;
using System.Windows.Forms;
using Savchin.EventSpy.Core;
using Savchin.Forms;

namespace Savchin.EventSpy
{
    public partial class StartUpForm : Form
    {
        private bool startClosing;

        /// <summary>
        /// Initializes a new instance of the <see cref="StartUpForm"/> class.
        /// </summary>
        public StartUpForm()
        {
            InitializeComponent();
            EventSpyCore.Domains.Add(AppDomain.CurrentDomain);
        }

        /// <summary>
        /// Handles the FormClosing event of the StartUpForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.FormClosingEventArgs"/> instance containing the event data.</param>
        private void StartUpForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!startClosing)
                EventSpyCore.MainForm.Close();
        }

        #region Button Handlers
        /// <summary>
        /// Handles the Click event of the buttonBrowse control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() != DialogResult.OK)
                return;
            textBoxFilePath.Text = openFileDialog1.FileName;
        }
        /// <summary>
        /// Handles the Click event of the buttonLoad control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void buttonLoad_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxFilePath.Text))
            {
                MessageBox.Show(this, "Please select file to load.", "Error load file", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            try
            {
                var fileInfo = new FileInfo(textBoxFilePath.Text);
                AddLog("Try load assembly " + fileInfo.Name);

                //var assemblyRef = new AssemblyName
                //                               {
                //                                   CodeBase = fileInfo.ToString()
                //                               };
                //Assembly.Load(assemblyRef);
                //AppDomain.CurrentDomain.AppendPrivatePath(fileInfo.Directory.FullName);
                // AppDomain.CurrentDomain.BaseDirectory



                //Assembly.LoadFile(fileInfo.ToString());
                
                //LoadAssemblyToSeppareteDomain(fileInfo);
                LoadAssembly(fileInfo);

                comboBoxForms.Items.Clear();
                FillCombo();
                comboBoxForms.SelectedIndex = 0;
                buttonLoad.Enabled = false;
            }
            catch (ReflectionTypeLoadException ex)
            {
                AddLog(ex.ToString());
                AddLog("=========================== LoaderExceptions =====================================");
                foreach (var loaderException in ex.LoaderExceptions)
                {
                    AddLog(loaderException.ToString());
                }
                FormObject.ShowObject("Error load assembly", ex);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.ToString(), "Error load file", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        /// <summary>
        /// Handles the Click event of the buttonStart control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void buttonStart_Click(object sender, EventArgs e)
        {
            if (comboBoxForms.SelectedItem == null)
            {
                MessageBox.Show(this, "Please select form", "Error start form", MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;
            }
            try
            {
                var laodedForm = (Form)Activator.CreateInstance((Type)comboBoxForms.SelectedItem);
                EventSpyCore.MainForm.StartExplore(laodedForm, checkBoxInline.Checked);

                startClosing = true;
                Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.ToString(), "Error start form", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }
        #endregion
        private void LoadAssembly(FileInfo info)
        {
            AppDomain.CurrentDomain.SetupInformation.PrivateBinPath = info.Directory.FullName;
            Assembly.LoadFrom(info.FullName);
        }

        private void LoadAssemblyToSeppareteDomain(FileInfo info)
        {
            var domaininfo = new AppDomainSetup();
            domaininfo.ApplicationBase = domaininfo.PrivateBinPath = info.Directory.FullName;
   

            //var pset = new PermissionSet(PermissionState.None);
            //pset.AddPermission(new SecurityPermission(SecurityPermissionFlag.Execution));
            //pset.AddPermission(new UIPermission(PermissionState.Unrestricted));

            var evidence = new Evidence(AppDomain.CurrentDomain.Evidence);

            evidence.AddAssembly("(some assembly)");
            evidence.AddHost(new Zone(SecurityZone.MyComputer));
            var domain = AppDomain.CreateDomain(
                "MyDomain", 
                evidence, 
                domaininfo
                );
      

            // Write the application domain information to the console.
            //Console.WriteLine("Host domain: " + AppDomain.CurrentDomain.FriendlyName);
            //Console.WriteLine("child domain: " + domain.FriendlyName);
            //Console.WriteLine();
            //Console.WriteLine("Application base is: " + domain.SetupInformation.ApplicationBase);
            //Console.WriteLine("Configuration file is: " + domain.SetupInformation.ConfigurationFile);

            // Unloads the application domain.
            //AppDomain.Unload(domain);
           var assemblyRef = AssemblyName.GetAssemblyName(info.FullName);
            domain.Load(assemblyRef);
            
            EventSpyCore.Domains.Add(domain);

        }
        private void FillCombo()
        {


            foreach (var domain in EventSpyCore.Domains)
            {
                FillCombo(domain);
            }

        }
        private void FillComboFromCurrentDomain()
        {
            FillCombo(AppDomain.CurrentDomain);
        }

        private void FillCombo(AppDomain domain)
        {
            foreach (Assembly a in domain.GetAssemblies())
            {
                if (!a.FullName.StartsWith("Microsoft.") && !a.FullName.StartsWith("System."))
                    foreach (Type t in a.GetTypes())
                    {
                        if (!t.IsInterface && t.IsClass && t.IsSubclassOf(typeof(Form))
                            // && !t.FullName.StartsWith("System.") && !t.FullName.StartsWith("Savchin.EventSpy.")
                            )
                        {
                            comboBoxForms.Items.Add(t);
                        }
                    }
            }

        }

        private void AddLog(string message)
        {
            textBoxLog.AppendText(message + Environment.NewLine);
        }


    }
}
