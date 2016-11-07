using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Savchin.Tools.System;
using NantRunner.Core;

namespace NantRunner
{
    public partial class FormMain : Form
    {
        private ApplicationManager<Settings> applicationManager;

        public FormMain()
        {
            applicationManager = new ApplicationManager<Settings>(this);

            InitializeComponent();

            foreach (Category category in applicationManager.Settings.Categories)
            {
                AddCategory(category);
            }


        }

        private void AddCategory(Category category)
        {
            ToolStripMenuItem group = new ToolStripMenuItem(category.Name);

            group.Tag = category;
            taskToolStripMenuItem.DropDownItems.Add(group);

            foreach (Task task in category.Tasks)
            {
                ToolStripMenuItem taskItem = new ToolStripMenuItem(task.Name);

                taskItem.Tag = task;
                taskItem.Click += new EventHandler(MenuItem_Click);

                if (File.Exists(task.IconPath))
                {
                    taskItem.Image = Image.FromFile(task.IconPath);
                }
                group.DropDownItems.Add(taskItem);
            }
        }


        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            propertyGrid1.SelectedObject = applicationManager.Settings;
        }



        private void MenuItem_Click(object sender, EventArgs e)
        {
            if (!(sender is ToolStripMenuItem))
                return;

            ToolStripMenuItem item = (ToolStripMenuItem)sender;

            if (item.Tag == null)
                return;

            if (!(item.Tag is Task))
                return;

            FormTask.StartTask((Task)item.Tag, applicationManager.Settings);
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShoFromTray();
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(e.CloseReason==CloseReason.UserClosing)
            {
                HideInTray();
                e.Cancel = true;
            }

        }
        void ShoFromTray()
        {
            WindowState = FormWindowState.Maximized;
            ShowInTaskbar = true;
        }
        void HideInTray()
        {
            WindowState = FormWindowState.Minimized;
            ShowInTaskbar = false;
        }
    }
}