using IES.PerformanceTester.Core;

namespace IES.PerformanceTester.Gui.Controls
{
    partial class TestRunnerControl 
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
            this.label1 = new System.Windows.Forms.Label();
            this.boxThreadCount = new System.Windows.Forms.NumericUpDown();
            this.boxEnabled = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.labelAttempts = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.labelErrors = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.labelThreads = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.labelRunned = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.boxThreadCount)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Thread Count";
            // 
            // boxThreadCount
            // 
            this.boxThreadCount.Location = new System.Drawing.Point(81, 20);
            this.boxThreadCount.Maximum = new decimal(new int[] {
                                                                    50,
                                                                    0,
                                                                    0,
                                                                    0});
            this.boxThreadCount.Minimum = new decimal(new int[] {
                                                                    1,
                                                                    0,
                                                                    0,
                                                                    0});
            this.boxThreadCount.Name = "boxThreadCount";
            this.boxThreadCount.Size = new System.Drawing.Size(45, 20);
            this.boxThreadCount.TabIndex = 1;
            this.boxThreadCount.Value = new decimal(new int[] {
                                                                  1,
                                                                  0,
                                                                  0,
                                                                  0});
            this.boxThreadCount.ValueChanged += new System.EventHandler(this.boxThreadCount_ValueChanged);
            // 
            // boxEnabled
            // 
            this.boxEnabled.AutoSize = true;
            this.boxEnabled.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.boxEnabled.Checked = true;
            this.boxEnabled.CheckState = System.Windows.Forms.CheckState.Checked;
            this.boxEnabled.Location = new System.Drawing.Point(3, 0);
            this.boxEnabled.Name = "boxEnabled";
            this.boxEnabled.Size = new System.Drawing.Size(92, 17);
            this.boxEnabled.TabIndex = 2;
            this.boxEnabled.Text = "Enabled         ";
            this.boxEnabled.UseVisualStyleBackColor = true;
            this.boxEnabled.CheckedChanged += new System.EventHandler(this.boxEnabled_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Attempts:";
            // 
            // labelAttempts
            // 
            this.labelAttempts.AutoSize = true;
            this.labelAttempts.Location = new System.Drawing.Point(64, 51);
            this.labelAttempts.Name = "labelAttempts";
            this.labelAttempts.Size = new System.Drawing.Size(13, 13);
            this.labelAttempts.TabIndex = 4;
            this.labelAttempts.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Errors:";
            // 
            // labelErrors
            // 
            this.labelErrors.AutoSize = true;
            this.labelErrors.Location = new System.Drawing.Point(64, 77);
            this.labelErrors.Name = "labelErrors";
            this.labelErrors.Size = new System.Drawing.Size(13, 13);
            this.labelErrors.TabIndex = 5;
            this.labelErrors.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 103);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Threads:";
            // 
            // labelThreads
            // 
            this.labelThreads.AutoSize = true;
            this.labelThreads.Location = new System.Drawing.Point(64, 103);
            this.labelThreads.Name = "labelThreads";
            this.labelThreads.Size = new System.Drawing.Size(13, 13);
            this.labelThreads.TabIndex = 5;
            this.labelThreads.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 128);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Runned:";
            // 
            // labelRunned
            // 
            this.labelRunned.AutoSize = true;
            this.labelRunned.Location = new System.Drawing.Point(66, 128);
            this.labelRunned.Name = "labelRunned";
            this.labelRunned.Size = new System.Drawing.Size(13, 13);
            this.labelRunned.TabIndex = 5;
            this.labelRunned.Text = "0";
            // 
            // TestRunnerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labelErrors);
            this.Controls.Add(this.labelRunned);
            this.Controls.Add(this.labelThreads);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.labelAttempts);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.boxEnabled);
            this.Controls.Add(this.boxThreadCount);
            this.Controls.Add(this.label1);
            this.Name = "TestRunnerControl";
            this.Size = new System.Drawing.Size(276, 180);
            ((System.ComponentModel.ISupportInitialize)(this.boxThreadCount)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown boxThreadCount;
        private System.Windows.Forms.CheckBox boxEnabled;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelAttempts;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labelErrors;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labelThreads;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label labelRunned;
    }
}