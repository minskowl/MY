using FileTools.Core;

namespace FileTools
{
    partial class FormMain : ILog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.mP3ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rusCheckToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.filesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.getFileListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.replaceTextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.renameFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.findNameDuplicatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imagesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lockFileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gACToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dLLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkExistsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mathConverterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fbd = new System.Windows.Forms.FolderBrowserDialog();
            this.textBoxLog = new System.Windows.Forms.TextBox();
            this.getFilesStatisticsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mP3ToolStripMenuItem,
            this.filesToolStripMenuItem,
            this.gACToolStripMenuItem,
            this.dLLToolStripMenuItem,
            this.toolStripMenuItem1,
            this.toolsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(295, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // mP3ToolStripMenuItem
            // 
            this.mP3ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rusCheckToolStripMenuItem});
            this.mP3ToolStripMenuItem.Name = "mP3ToolStripMenuItem";
            this.mP3ToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.mP3ToolStripMenuItem.Text = "MP3";
            // 
            // rusCheckToolStripMenuItem
            // 
            this.rusCheckToolStripMenuItem.Name = "rusCheckToolStripMenuItem";
            this.rusCheckToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.rusCheckToolStripMenuItem.Text = "Rus Check";
            // 
            // filesToolStripMenuItem
            // 
            this.filesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.getFileListToolStripMenuItem,
            this.replaceTextToolStripMenuItem,
            this.renameFilesToolStripMenuItem,
            this.findNameDuplicatesToolStripMenuItem,
            this.imagesToolStripMenuItem,
            this.lockFileMenuItem,
            this.getFilesStatisticsToolStripMenuItem});
            this.filesToolStripMenuItem.Name = "filesToolStripMenuItem";
            this.filesToolStripMenuItem.Size = new System.Drawing.Size(42, 20);
            this.filesToolStripMenuItem.Text = "Files";
            // 
            // getFileListToolStripMenuItem
            // 
            this.getFileListToolStripMenuItem.Name = "getFileListToolStripMenuItem";
            this.getFileListToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.getFileListToolStripMenuItem.Text = "Get File List";
            // 
            // replaceTextToolStripMenuItem
            // 
            this.replaceTextToolStripMenuItem.Name = "replaceTextToolStripMenuItem";
            this.replaceTextToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.replaceTextToolStripMenuItem.Text = "Replace Text";
            // 
            // renameFilesToolStripMenuItem
            // 
            this.renameFilesToolStripMenuItem.Name = "renameFilesToolStripMenuItem";
            this.renameFilesToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.renameFilesToolStripMenuItem.Text = "Rename Files";
            // 
            // findNameDuplicatesToolStripMenuItem
            // 
            this.findNameDuplicatesToolStripMenuItem.Name = "findNameDuplicatesToolStripMenuItem";
            this.findNameDuplicatesToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.findNameDuplicatesToolStripMenuItem.Text = "Find Name Duplicates";
            // 
            // imagesToolStripMenuItem
            // 
            this.imagesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.resizeToolStripMenuItem});
            this.imagesToolStripMenuItem.Name = "imagesToolStripMenuItem";
            this.imagesToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.imagesToolStripMenuItem.Text = "Images";
            // 
            // resizeToolStripMenuItem
            // 
            this.resizeToolStripMenuItem.Name = "resizeToolStripMenuItem";
            this.resizeToolStripMenuItem.Size = new System.Drawing.Size(106, 22);
            this.resizeToolStripMenuItem.Text = "Resize";
            // 
            // lockFileMenuItem
            // 
            this.lockFileMenuItem.Name = "lockFileMenuItem";
            this.lockFileMenuItem.Size = new System.Drawing.Size(190, 22);
            this.lockFileMenuItem.Text = "Lock File";
            // 
            // gACToolStripMenuItem
            // 
            this.gACToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.manageToolStripMenuItem});
            this.gACToolStripMenuItem.Name = "gACToolStripMenuItem";
            this.gACToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.gACToolStripMenuItem.Text = "GAC";
            // 
            // manageToolStripMenuItem
            // 
            this.manageToolStripMenuItem.Name = "manageToolStripMenuItem";
            this.manageToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.manageToolStripMenuItem.Text = "Manage";
            this.manageToolStripMenuItem.Click += new System.EventHandler(this.manageToolStripMenuItem_Click);
            // 
            // dLLToolStripMenuItem
            // 
            this.dLLToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.checkExistsToolStripMenuItem});
            this.dLLToolStripMenuItem.Name = "dLLToolStripMenuItem";
            this.dLLToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.dLLToolStripMenuItem.Text = "DLL";
            // 
            // checkExistsToolStripMenuItem
            // 
            this.checkExistsToolStripMenuItem.Name = "checkExistsToolStripMenuItem";
            this.checkExistsToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.checkExistsToolStripMenuItem.Text = "Check Exists";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(12, 20);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mathConverterToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // mathConverterToolStripMenuItem
            // 
            this.mathConverterToolStripMenuItem.Name = "mathConverterToolStripMenuItem";
            this.mathConverterToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.mathConverterToolStripMenuItem.Text = "Math Converter";
            this.mathConverterToolStripMenuItem.Click += new System.EventHandler(this.mathConverterToolStripMenuItem_Click);
            // 
            // textBoxLog
            // 
            this.textBoxLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxLog.Location = new System.Drawing.Point(0, 24);
            this.textBoxLog.Multiline = true;
            this.textBoxLog.Name = "textBoxLog";
            this.textBoxLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxLog.Size = new System.Drawing.Size(295, 255);
            this.textBoxLog.TabIndex = 1;
            // 
            // getFilesStatisticsToolStripMenuItem
            // 
            this.getFilesStatisticsToolStripMenuItem.Name = "getFilesStatisticsToolStripMenuItem";
            this.getFilesStatisticsToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.getFilesStatisticsToolStripMenuItem.Text = "Get Files Statistics";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(295, 279);
            this.Controls.Add(this.textBoxLog);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormMain";
            this.Text = "FormMain";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mP3ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rusCheckToolStripMenuItem;
        private System.Windows.Forms.FolderBrowserDialog fbd;
        private System.Windows.Forms.TextBox textBoxLog;
        private System.Windows.Forms.ToolStripMenuItem filesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem getFileListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem replaceTextToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem renameFilesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem findNameDuplicatesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gACToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem manageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dLLToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem checkExistsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem imagesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resizeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lockFileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mathConverterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem getFilesStatisticsToolStripMenuItem;
    }
}

