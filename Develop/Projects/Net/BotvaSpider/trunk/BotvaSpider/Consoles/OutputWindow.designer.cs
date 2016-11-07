namespace BotvaSpider.Consoles
{
    partial class OutputWindow 
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OutputWindow));
            this.comboBoxLoggers = new System.Windows.Forms.ComboBox();
            this.boxLevelFrom = new System.Windows.Forms.ComboBox();
            this.logViewer = new BotvaSpider.Controls.LogViewer();
            this.SuspendLayout();
            // 
            // comboBoxLoggers
            // 
            this.comboBoxLoggers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLoggers.Location = new System.Drawing.Point(0, 2);
            this.comboBoxLoggers.Name = "comboBoxLoggers";
            this.comboBoxLoggers.Size = new System.Drawing.Size(132, 21);
            this.comboBoxLoggers.TabIndex = 1;
            this.comboBoxLoggers.SelectedIndexChanged += new System.EventHandler(this.comboBoxLoggers_SelectedIndexChanged);
            // 
            // boxLevelFrom
            // 
            this.boxLevelFrom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.boxLevelFrom.FormattingEnabled = true;
            this.boxLevelFrom.Location = new System.Drawing.Point(140, 2);
            this.boxLevelFrom.Name = "boxLevelFrom";
            this.boxLevelFrom.Size = new System.Drawing.Size(121, 21);
            this.boxLevelFrom.TabIndex = 3;
            this.boxLevelFrom.SelectedIndexChanged += new System.EventHandler(this.boxLevelFrom_SelectedIndexChanged);
            // 
            // logViewer
            // 
            this.logViewer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.logViewer.Location = new System.Drawing.Point(0, 28);
            this.logViewer.Name = "logViewer";
            this.logViewer.Size = new System.Drawing.Size(374, 394);
            this.logViewer.TabIndex = 2;
            // 
            // OutputWindow
            // 
            this.ClientSize = new System.Drawing.Size(375, 427);
            this.Controls.Add(this.boxLevelFrom);
            this.Controls.Add(this.logViewer);
            this.Controls.Add(this.comboBoxLoggers);
            this.HideOnClose = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "OutputWindow";
            this.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.ShowHint = Savchin.Forms.Docking.DockState.DockBottomAutoHide;
            this.TabText = "Output";
            this.Text = "Output";
            this.ResumeLayout(false);

        }
        #endregion

        private System.Windows.Forms.ComboBox comboBoxLoggers;
        private BotvaSpider.Controls.LogViewer logViewer;
        private System.Windows.Forms.ComboBox boxLevelFrom;
    }
}