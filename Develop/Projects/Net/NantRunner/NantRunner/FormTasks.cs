using System;
using System.Drawing;
using System.Windows.Forms;
using NantRunner.Core;

namespace NantRunner
{
    public partial class FormTasks : Form
    {
        private Settings settings;
        public Settings Settings
        {
            set
            {
                settings = value;
                foreach (Category category in value.Categories)
                {
                    AddCategory(category);
                }
            }
        }
        private void AddCategory(Category category)
        {
            ListViewGroup group = listView1.Groups.Add(category.Name, category.Name);
            group.Tag = category;

            foreach (Task task in category.Tasks)
            {
                ListViewItem item = group.Items.Add(task.Name);
                item.Tag = task;
                Image image = Image.FromFile(task.IconPath);
                imageList1.Images.Add(category.Name + task.Name, image);
                item.ImageKey = category.Name + task.Name;

                listView1.Items.Add(item);
            }
        }

        public FormTasks()
        {
            InitializeComponent();
        }

        private void listView1_Click(object sender, EventArgs e)
        {
            if(listView1.SelectedItems.Count==0)
                return;

            FormTask.StartTask((Task)listView1.SelectedItems[0].Tag, settings);

            Hide();
        }


    }
}