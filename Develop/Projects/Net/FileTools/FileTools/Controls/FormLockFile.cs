using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FileTools.Controls
{
    public partial class FormLockFile : Form
    {
        private FileStream _fileStream;
        public FormLockFile()
        {
            InitializeComponent();
        }

        private void buttonLock_Click(object sender, EventArgs e)
        {
            if (_fileStream == null)
                LockFile();
            else
                UnlockFile();


        }

        private void LockFile()
        {
            if (!File.Exists(fileSelectControl1.FileName))
            {
                MessageBox.Show("Please select file");
                return;
            }
            if (!checkBoxReadLock.Checked && !checkBoxWriteLock.Checked)
            {
                MessageBox.Show("Please select lock");
                return;
            }

            FileAccess mode = FileAccess.Write;
            if (checkBoxReadLock.Checked)
                mode = FileAccess.ReadWrite;

            _fileStream = File.Open(fileSelectControl1.FileName, FileMode.Open, mode);

            checkBoxReadLock.Enabled = checkBoxWriteLock.Enabled = fileSelectControl1.Enabled = false;
            buttonLock.Text = "Unlock";
        }

        private void UnlockFile()
        {
            _fileStream.Dispose();
            _fileStream = null;
            checkBoxReadLock.Enabled = checkBoxWriteLock.Enabled = fileSelectControl1.Enabled = true;
            buttonLock.Text = "Lock";
        }
    }
}
