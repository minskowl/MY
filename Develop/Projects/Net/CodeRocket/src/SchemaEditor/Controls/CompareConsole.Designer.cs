﻿namespace SchemaEditor.Controls
{
    partial class CompareConsole
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
            this.compareView1 = new Savchin.Controls.Common.Comparer.CompareView();
            this.SuspendLayout();
            // 
            // compareView1
            // 
            this.compareView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.compareView1.Location = new System.Drawing.Point(0, 0);
            this.compareView1.Name = "compareView1";
            this.compareView1.Size = new System.Drawing.Size(401, 381);
            this.compareView1.TabIndex = 0;
            this.compareView1.CancelClick += new System.EventHandler(this.compareView1_CancelClick);
            this.compareView1.OkClick += new System.EventHandler(this.compareView1_OkClick);
            // 
            // CompareConsole
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(401, 381);
            this.Controls.Add(this.compareView1);
            this.Name = "CompareConsole";
            this.TabText = "CompareConsole";
            this.Text = "CompareConsole";
            this.ResumeLayout(false);

        }

        #endregion

        private Savchin.Controls.Common.Comparer.CompareView compareView1;
    }
}