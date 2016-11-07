namespace Savchin.Forms.Net
{
    partial class ProxySettingsControl
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
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxPort = new System.Windows.Forms.MaskedTextBox();
            this.checkBoxUseDefaultCredentials = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.credentialControl1 = new CredentialControl();
            this.textBoxAddress = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Address";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Port";
            // 
            // textBoxPort
            // 
            this.textBoxPort.Location = new System.Drawing.Point(56, 29);
            this.textBoxPort.Mask = "0000";
            this.textBoxPort.Name = "textBoxPort";
            this.textBoxPort.Size = new System.Drawing.Size(100, 20);
            this.textBoxPort.TabIndex = 5;
            // 
            // checkBoxUseDefaultCredentials
            // 
            this.checkBoxUseDefaultCredentials.AutoSize = true;
            this.checkBoxUseDefaultCredentials.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBoxUseDefaultCredentials.Location = new System.Drawing.Point(8, 55);
            this.checkBoxUseDefaultCredentials.Name = "checkBoxUseDefaultCredentials";
            this.checkBoxUseDefaultCredentials.Size = new System.Drawing.Size(144, 17);
            this.checkBoxUseDefaultCredentials.TabIndex = 6;
            this.checkBoxUseDefaultCredentials.Text = "Use Default Credential    ";
            this.checkBoxUseDefaultCredentials.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.credentialControl1);
            this.groupBox1.Location = new System.Drawing.Point(8, 88);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(255, 109);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Credential";
            // 
            // credentialControl1
            // 
            this.credentialControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.credentialControl1.Location = new System.Drawing.Point(3, 16);
            this.credentialControl1.Name = "credentialControl1";
            this.credentialControl1.Size = new System.Drawing.Size(249, 90);
            this.credentialControl1.TabIndex = 0;
            // 
            // textBoxAddress
            // 
            this.textBoxAddress.Location = new System.Drawing.Point(56, 2);
            this.textBoxAddress.Name = "textBoxAddress";
            this.textBoxAddress.Size = new System.Drawing.Size(100, 20);
            this.textBoxAddress.TabIndex = 8;
            // 
            // ProxySettingsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.textBoxAddress);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.checkBoxUseDefaultCredentials);
            this.Controls.Add(this.textBoxPort);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "ProxySettingsControl";
            this.Size = new System.Drawing.Size(271, 203);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.MaskedTextBox textBoxPort;
        private System.Windows.Forms.CheckBox checkBoxUseDefaultCredentials;
        private System.Windows.Forms.GroupBox groupBox1;
        private CredentialControl credentialControl1;
        private System.Windows.Forms.TextBox textBoxAddress;
    }
}
