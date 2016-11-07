using Savchin.Forms.Comparer;

namespace CodeRocket.Controls
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
            this.compareView = new CompareView();
            this.SuspendLayout();
            // 
            // compareView
            // 
            this.compareView.AutoSize = true;
            this.compareView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.compareView.Location = new System.Drawing.Point(0, 0);
            this.compareView.Name = "compareView";
            this.compareView.Size = new System.Drawing.Size(528, 419);
            this.compareView.TabIndex = 0;
            this.compareView.CancelClick += new System.EventHandler(this.compareView_CancelClick);
            this.compareView.OkClick += new System.EventHandler(this.compareView_OkClick);
            // 
            // CompareConsole
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(528, 419);
            this.Controls.Add(this.compareView);
            this.Name = "CompareConsole";
            this.TabText = "CompareConsole";
            this.Text = "CompareConsole";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CompareView compareView;
    }
}