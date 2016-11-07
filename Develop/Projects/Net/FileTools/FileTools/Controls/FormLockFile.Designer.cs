namespace FileTools.Controls
{
    partial class FormLockFile
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
            this.checkBoxReadLock = new System.Windows.Forms.CheckBox();
            this.checkBoxWriteLock = new System.Windows.Forms.CheckBox();
            this.buttonLock = new System.Windows.Forms.Button();
            this.fileSelectControl1 = new FileTools.Controls.FileSelectControl();
            this.SuspendLayout();
            // 
            // checkBoxReadLock
            // 
            this.checkBoxReadLock.AutoSize = true;
            this.checkBoxReadLock.Location = new System.Drawing.Point(13, 39);
            this.checkBoxReadLock.Name = "checkBoxReadLock";
            this.checkBoxReadLock.Size = new System.Drawing.Size(79, 17);
            this.checkBoxReadLock.TabIndex = 1;
            this.checkBoxReadLock.Text = "Read Lock";
            this.checkBoxReadLock.UseVisualStyleBackColor = true;
            // 
            // checkBoxWriteLock
            // 
            this.checkBoxWriteLock.AutoSize = true;
            this.checkBoxWriteLock.Location = new System.Drawing.Point(13, 63);
            this.checkBoxWriteLock.Name = "checkBoxWriteLock";
            this.checkBoxWriteLock.Size = new System.Drawing.Size(78, 17);
            this.checkBoxWriteLock.TabIndex = 2;
            this.checkBoxWriteLock.Text = "Write Lock";
            this.checkBoxWriteLock.UseVisualStyleBackColor = true;
            // 
            // buttonLock
            // 
            this.buttonLock.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonLock.Location = new System.Drawing.Point(258, 57);
            this.buttonLock.Name = "buttonLock";
            this.buttonLock.Size = new System.Drawing.Size(75, 23);
            this.buttonLock.TabIndex = 3;
            this.buttonLock.Text = "Lock";
            this.buttonLock.UseVisualStyleBackColor = true;
            this.buttonLock.Click += new System.EventHandler(this.buttonLock_Click);
            // 
            // fileSelectControl1
            // 
            this.fileSelectControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fileSelectControl1.Location = new System.Drawing.Point(12, 3);
            this.fileSelectControl1.Name = "fileSelectControl1";
            this.fileSelectControl1.Size = new System.Drawing.Size(323, 29);
            this.fileSelectControl1.TabIndex = 0;
            // 
            // FormLockFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(338, 97);
            this.Controls.Add(this.buttonLock);
            this.Controls.Add(this.checkBoxWriteLock);
            this.Controls.Add(this.checkBoxReadLock);
            this.Controls.Add(this.fileSelectControl1);
            this.Name = "FormLockFile";
            this.Text = "FormLockFile";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FileSelectControl fileSelectControl1;
        private System.Windows.Forms.CheckBox checkBoxReadLock;
        private System.Windows.Forms.CheckBox checkBoxWriteLock;
        private System.Windows.Forms.Button buttonLock;
    }
}