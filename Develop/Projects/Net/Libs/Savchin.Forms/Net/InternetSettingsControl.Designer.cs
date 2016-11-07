namespace Savchin.Forms.Net
{
    partial class InternetSettingsControl
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
            this.checkBoxUseProxy = new System.Windows.Forms.CheckBox();
            this.checkBoxUseDefaultCredentials = new System.Windows.Forms.CheckBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageProxy = new System.Windows.Forms.TabPage();
            this.tabPageCredential = new System.Windows.Forms.TabPage();
            this.proxySettingsControl1 = new ProxySettingsControl();
            this.credentialControl1 = new CredentialControl();
            this.tabControl1.SuspendLayout();
            this.tabPageProxy.SuspendLayout();
            this.tabPageCredential.SuspendLayout();
            this.SuspendLayout();
            // 
            // checkBoxUseProxy
            // 
            this.checkBoxUseProxy.AutoSize = true;
            this.checkBoxUseProxy.Location = new System.Drawing.Point(16, 16);
            this.checkBoxUseProxy.Name = "checkBoxUseProxy";
            this.checkBoxUseProxy.Size = new System.Drawing.Size(74, 17);
            this.checkBoxUseProxy.TabIndex = 1;
            this.checkBoxUseProxy.Text = "Use Proxy";
            this.checkBoxUseProxy.UseVisualStyleBackColor = true;
            this.checkBoxUseProxy.CheckedChanged += new System.EventHandler(this.checkBoxUseProxy_CheckedChanged);
            // 
            // checkBoxUseDefaultCredentials
            // 
            this.checkBoxUseDefaultCredentials.AutoSize = true;
            this.checkBoxUseDefaultCredentials.Location = new System.Drawing.Point(6, 16);
            this.checkBoxUseDefaultCredentials.Name = "checkBoxUseDefaultCredentials";
            this.checkBoxUseDefaultCredentials.Size = new System.Drawing.Size(132, 17);
            this.checkBoxUseDefaultCredentials.TabIndex = 2;
            this.checkBoxUseDefaultCredentials.Text = "Use Default Credential";
            this.checkBoxUseDefaultCredentials.UseVisualStyleBackColor = true;
            this.checkBoxUseDefaultCredentials.CheckedChanged += new System.EventHandler(this.checkBoxUseDefaultCredentials_CheckedChanged);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageProxy);
            this.tabControl1.Controls.Add(this.tabPageCredential);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(321, 281);
            this.tabControl1.TabIndex = 3;
            // 
            // tabPageProxy
            // 
            this.tabPageProxy.Controls.Add(this.proxySettingsControl1);
            this.tabPageProxy.Controls.Add(this.checkBoxUseProxy);
            this.tabPageProxy.Location = new System.Drawing.Point(4, 22);
            this.tabPageProxy.Name = "tabPageProxy";
            this.tabPageProxy.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageProxy.Size = new System.Drawing.Size(313, 255);
            this.tabPageProxy.TabIndex = 0;
            this.tabPageProxy.Text = "Proxy";
            this.tabPageProxy.UseVisualStyleBackColor = true;
            // 
            // tabPageCredential
            // 
            this.tabPageCredential.Controls.Add(this.credentialControl1);
            this.tabPageCredential.Controls.Add(this.checkBoxUseDefaultCredentials);
            this.tabPageCredential.Location = new System.Drawing.Point(4, 22);
            this.tabPageCredential.Name = "tabPageCredential";
            this.tabPageCredential.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageCredential.Size = new System.Drawing.Size(313, 255);
            this.tabPageCredential.TabIndex = 1;
            this.tabPageCredential.Text = "Credential";
            this.tabPageCredential.UseVisualStyleBackColor = true;
            // 
            // proxySettingsControl1
            // 
            this.proxySettingsControl1.Location = new System.Drawing.Point(6, 39);
            this.proxySettingsControl1.Name = "proxySettingsControl1";
            this.proxySettingsControl1.Size = new System.Drawing.Size(290, 216);
            this.proxySettingsControl1.TabIndex = 0;
            // 
            // credentialControl1
            // 
            this.credentialControl1.Location = new System.Drawing.Point(6, 49);
            this.credentialControl1.Name = "credentialControl1";
            this.credentialControl1.Size = new System.Drawing.Size(293, 91);
            this.credentialControl1.TabIndex = 3;
            // 
            // InternetSettingsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Name = "InternetSettingsControl";
            this.Size = new System.Drawing.Size(321, 281);
            this.tabControl1.ResumeLayout(false);
            this.tabPageProxy.ResumeLayout(false);
            this.tabPageProxy.PerformLayout();
            this.tabPageCredential.ResumeLayout(false);
            this.tabPageCredential.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBoxUseProxy;
        private System.Windows.Forms.CheckBox checkBoxUseDefaultCredentials;
        private ProxySettingsControl proxySettingsControl1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageProxy;
        private System.Windows.Forms.TabPage tabPageCredential;
        private CredentialControl credentialControl1;
    }
}
