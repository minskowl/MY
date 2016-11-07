namespace KnowledgeBase.KbTools.Controls
{
    partial class ExportToHtmlControl
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
            this.button = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxDestintionFolder = new System.Windows.Forms.TextBox();
            this.buttonBrowseDestintionFolder = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxUrlBase = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // button
            // 
            this.button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button.Location = new System.Drawing.Point(378, 187);
            this.button.Name = "button";
            this.button.Size = new System.Drawing.Size(75, 23);
            this.button.TabIndex = 0;
            this.button.Text = "Export";
            this.button.UseVisualStyleBackColor = true;
            this.button.Click += new System.EventHandler(this.button_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Destination Folder";
            // 
            // textBoxDestintionFolder
            // 
            this.textBoxDestintionFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxDestintionFolder.Location = new System.Drawing.Point(115, 10);
            this.textBoxDestintionFolder.Name = "textBoxDestintionFolder";
            this.textBoxDestintionFolder.Size = new System.Drawing.Size(258, 20);
            this.textBoxDestintionFolder.TabIndex = 2;
            this.textBoxDestintionFolder.Text = "D:\\Tmp\\";
            // 
            // buttonBrowseDestintionFolder
            // 
            this.buttonBrowseDestintionFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonBrowseDestintionFolder.Location = new System.Drawing.Point(378, 7);
            this.buttonBrowseDestintionFolder.Name = "buttonBrowseDestintionFolder";
            this.buttonBrowseDestintionFolder.Size = new System.Drawing.Size(75, 23);
            this.buttonBrowseDestintionFolder.TabIndex = 3;
            this.buttonBrowseDestintionFolder.Text = "Browse...";
            this.buttonBrowseDestintionFolder.UseVisualStyleBackColor = true;
            this.buttonBrowseDestintionFolder.Click += new System.EventHandler(this.buttonBrowseDestintionFolder_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Base Url";
            // 
            // textBoxUrlBase
            // 
            this.textBoxUrlBase.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxUrlBase.Location = new System.Drawing.Point(115, 36);
            this.textBoxUrlBase.Name = "textBoxUrlBase";
            this.textBoxUrlBase.Size = new System.Drawing.Size(258, 20);
            this.textBoxUrlBase.TabIndex = 5;
            this.textBoxUrlBase.Text = "http://localhost/KnowledgeBase/KnowledgeInfo.aspx?ID=";
            // 
            // ExportToHtmlControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.textBoxUrlBase);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonBrowseDestintionFolder);
            this.Controls.Add(this.textBoxDestintionFolder);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button);
            this.Name = "ExportToHtmlControl";
            this.Size = new System.Drawing.Size(460, 229);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxDestintionFolder;
        private System.Windows.Forms.Button buttonBrowseDestintionFolder;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxUrlBase;
    }
}
