namespace BotvaSpider.Controls.Configuration
{
    partial class UserListsControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.textBoxWhite = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.textBoxBastards = new System.Windows.Forms.TextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.boxWhiteClans = new System.Windows.Forms.TextBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(282, 267);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.textBoxWhite);
            this.tabPage1.Location = new System.Drawing.Point(4, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(274, 241);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Белые Юзера";
            this.tabPage1.ToolTipText = "Список юзеров которых не бодать";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // textBoxWhite
            // 
            this.textBoxWhite.AcceptsReturn = true;
            this.textBoxWhite.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxWhite.Location = new System.Drawing.Point(3, 3);
            this.textBoxWhite.Multiline = true;
            this.textBoxWhite.Name = "textBoxWhite";
            this.textBoxWhite.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxWhite.Size = new System.Drawing.Size(268, 235);
            this.textBoxWhite.TabIndex = 0;
            this.textBoxWhite.TextChanged += new System.EventHandler(this.textBoxWhite_TextChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.textBoxBastards);
            this.tabPage2.Location = new System.Drawing.Point(4, 4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(274, 241);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Ублюдков";
            this.tabPage2.ToolTipText = "Список юзеров от которых прятать деньги";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // textBoxBastards
            // 
            this.textBoxBastards.AcceptsReturn = true;
            this.textBoxBastards.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxBastards.Location = new System.Drawing.Point(3, 3);
            this.textBoxBastards.Multiline = true;
            this.textBoxBastards.Name = "textBoxBastards";
            this.textBoxBastards.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxBastards.Size = new System.Drawing.Size(268, 235);
            this.textBoxBastards.TabIndex = 1;
            this.textBoxBastards.TextChanged += new System.EventHandler(this.textBoxBastards_TextChanged);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.boxWhiteClans);
            this.tabPage3.Location = new System.Drawing.Point(4, 4);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(274, 241);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Белые Кланы";
            this.tabPage3.ToolTipText = "Список калнов на которые не нападать";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // boxWhiteClans
            // 
            this.boxWhiteClans.AcceptsReturn = true;
            this.boxWhiteClans.Dock = System.Windows.Forms.DockStyle.Fill;
            this.boxWhiteClans.Location = new System.Drawing.Point(0, 0);
            this.boxWhiteClans.Multiline = true;
            this.boxWhiteClans.Name = "boxWhiteClans";
            this.boxWhiteClans.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.boxWhiteClans.Size = new System.Drawing.Size(274, 241);
            this.boxWhiteClans.TabIndex = 1;
            this.boxWhiteClans.TextChanged += new System.EventHandler(this.boxWhiteClans_TextChanged);
            // 
            // UserListsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Name = "UserListsControl";
            this.Size = new System.Drawing.Size(282, 267);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox textBoxWhite;
        private System.Windows.Forms.TextBox textBoxBastards;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TextBox boxWhiteClans;
    }
}
