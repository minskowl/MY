using BotvaSpider.Controls.Mining;

namespace BotvaSpider.Consoles
{
    partial class CrystalMapConsole
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.saveMapControl1 = new SaveMapControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.showVariantsControlEx1 = new ShowVariantsControl();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(524, 299);
            this.tabControl1.TabIndex = 7;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.saveMapControl1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(516, 273);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "����������";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // saveMapControl1
            // 
            this.saveMapControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.saveMapControl1.Location = new System.Drawing.Point(3, 3);
            this.saveMapControl1.Name = "saveMapControl1";
            this.saveMapControl1.Size = new System.Drawing.Size(510, 267);
            this.saveMapControl1.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.showVariantsControlEx1);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(516, 273);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "�����";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // showVariantsControlEx1
            // 
            this.showVariantsControlEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.showVariantsControlEx1.Location = new System.Drawing.Point(3, 3);
            this.showVariantsControlEx1.Name = "showVariantsControlEx1";
            this.showVariantsControlEx1.Size = new System.Drawing.Size(510, 267);
            this.showVariantsControlEx1.TabIndex = 0;
            // 
            // CrystalMapConsole
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(524, 299);
            this.Controls.Add(this.tabControl1);
            this.Name = "CrystalMapConsole";
            this.TabText = "������ ����������";
            this.Text = "������ ����������";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private SaveMapControl saveMapControl1;
        private System.Windows.Forms.TabPage tabPage3;
        private ShowVariantsControl showVariantsControlEx1;
    }
}