using Savchin.Drawing;

namespace ColorPicker
{
    partial class Form1
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
            this.menuMain = new System.Windows.Forms.MenuStrip();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rGBToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hSBToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyHTMLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteHTMLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.colorPicker1 = new Savchin.Controls.Common.ColorPicker();
            this.menuMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuMain
            // 
            this.menuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editToolStripMenuItem});
            this.menuMain.Location = new System.Drawing.Point(0, 0);
            this.menuMain.Name = "menuMain";
            this.menuMain.Size = new System.Drawing.Size(292, 24);
            this.menuMain.TabIndex = 1;
            this.menuMain.Text = "menuStrip1";
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.modeToolStripMenuItem,
            this.copyHTMLToolStripMenuItem,
            this.pasteHTMLToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // modeToolStripMenuItem
            // 
            this.modeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rGBToolStripMenuItem,
            this.hSBToolStripMenuItem});
            this.modeToolStripMenuItem.Name = "modeToolStripMenuItem";
            this.modeToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.modeToolStripMenuItem.Text = "Mode";
            // 
            // rGBToolStripMenuItem
            // 
            this.rGBToolStripMenuItem.Checked = true;
            this.rGBToolStripMenuItem.CheckOnClick = true;
            this.rGBToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.rGBToolStripMenuItem.Name = "rGBToolStripMenuItem";
            this.rGBToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.rGBToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.rGBToolStripMenuItem.Text = "RGB";
            this.rGBToolStripMenuItem.CheckedChanged += new System.EventHandler(this.rGBToolStripMenuItem_CheckedChanged);
            // 
            // hSBToolStripMenuItem
            // 
            this.hSBToolStripMenuItem.CheckOnClick = true;
            this.hSBToolStripMenuItem.Name = "hSBToolStripMenuItem";
            this.hSBToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.H)));
            this.hSBToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.hSBToolStripMenuItem.Text = "HSB";
            this.hSBToolStripMenuItem.CheckedChanged += new System.EventHandler(this.hSBToolStripMenuItem_CheckedChanged);
            // 
            // copyHTMLToolStripMenuItem
            // 
            this.copyHTMLToolStripMenuItem.Name = "copyHTMLToolStripMenuItem";
            this.copyHTMLToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.copyHTMLToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.copyHTMLToolStripMenuItem.Text = "Copy HTML";
            this.copyHTMLToolStripMenuItem.Click += new System.EventHandler(this.copyHTMLToolStripMenuItem_Click);
            // 
            // pasteHTMLToolStripMenuItem
            // 
            this.pasteHTMLToolStripMenuItem.Name = "pasteHTMLToolStripMenuItem";
            this.pasteHTMLToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.pasteHTMLToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.pasteHTMLToolStripMenuItem.Text = "Paste HTML";
            this.pasteHTMLToolStripMenuItem.Click += new System.EventHandler(this.pasteHTMLToolStripMenuItem_Click);
            // 
            // colorPicker1
            // 
            this.colorPicker1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.colorPicker1.Location = new System.Drawing.Point(0, 24);
            this.colorPicker1.MinimumSize = new System.Drawing.Size(261, 248);
            this.colorPicker1.Mode = ColorSheme.RGB;
            this.colorPicker1.Name = "colorPicker1";
            this.colorPicker1.Size = new System.Drawing.Size(292, 351);
            this.colorPicker1.TabIndex = 0;
            this.colorPicker1.Value = System.Drawing.Color.Empty;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 375);
            this.Controls.Add(this.colorPicker1);
            this.Controls.Add(this.menuMain);
            this.MainMenuStrip = this.menuMain;
            this.Name = "Form1";
            this.Text = "Color Picker";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuMain.ResumeLayout(false);
            this.menuMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Savchin.Controls.Common.ColorPicker colorPicker1;
        private System.Windows.Forms.MenuStrip menuMain;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem modeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rGBToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hSBToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyHTMLToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteHTMLToolStripMenuItem;
    }
}

