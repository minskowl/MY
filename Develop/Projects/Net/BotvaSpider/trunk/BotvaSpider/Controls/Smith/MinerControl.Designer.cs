namespace BotvaSpider.Controls
{
    partial class MinerControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MinerControl));
            this.saperControl = new AxShockwaveFlashObjects.AxShockwaveFlash();
            ((System.ComponentModel.ISupportInitialize)(this.saperControl)).BeginInit();
            this.SuspendLayout();
            // 
            // saperControl
            // 
            this.saperControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.saperControl.Enabled = true;
            this.saperControl.Location = new System.Drawing.Point(0, 0);
            this.saperControl.Name = "saperControl";
            this.saperControl.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("saperControl.OcxState")));
            this.saperControl.Size = new System.Drawing.Size(402, 311);
            this.saperControl.TabIndex = 3;
            // 
            // MinerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.saperControl);
            this.Name = "MinerControl";
            this.Size = new System.Drawing.Size(402, 311);
            ((System.ComponentModel.ISupportInitialize)(this.saperControl)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private AxShockwaveFlashObjects.AxShockwaveFlash saperControl;
    }
}
