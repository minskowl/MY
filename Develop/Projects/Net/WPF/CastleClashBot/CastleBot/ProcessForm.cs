using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Castle.Core;
using CastleBot.Core;
using CastleController.Core;
using ChristianMoser.WpfInspector.Services;

namespace CastleBot
{
    public partial class ProcessForm : Form
    {
        private ManagedApplicationsService _managedApplicationsService;
        private readonly InspectionService _inspectionService;
        public List<ManagedApplicationInfo> Applications { get; set; }

        public ProcessForm()
        {
            InitializeComponent();
            _managedApplicationsService = new ManagedApplicationsService();
            _inspectionService = ServiceLocator.Resolve<InspectionService>();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            listBox1.Items.AddRange(_managedApplicationsService.GetManagedApplications().ToArray());

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var process = listBox1.SelectedItem as ManagedApplicationInfo;
            if (process == null) return;


            _inspectionService.Inspect(process);
        }
    }
}
