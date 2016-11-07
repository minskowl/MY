namespace BotvaSpider.Controls.Statistics
{
    partial class MineTotalStatistics
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MineTotalStatistics));
            this.buttonShow = new System.Windows.Forms.Button();
            this.dateRangeControl1 = new Savchin.Forms.DateRangeControl();
            this.grid = new BotvaSpider.Controls.LabelValueGrid();
            this.SuspendLayout();
            // 
            // buttonShow
            // 
            this.buttonShow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonShow.Location = new System.Drawing.Point(570, 5);
            this.buttonShow.Name = "buttonShow";
            this.buttonShow.Size = new System.Drawing.Size(75, 23);
            this.buttonShow.TabIndex = 6;
            this.buttonShow.Text = "Показать";
            this.buttonShow.UseVisualStyleBackColor = true;
            this.buttonShow.Click += new System.EventHandler(this.buttonShow_Click);
            // 
            // dateRangeControl1
            // 
            this.dateRangeControl1.Location = new System.Drawing.Point(3, 0);
            this.dateRangeControl1.Name = "dateRangeControl1";
            this.dateRangeControl1.Size = new System.Drawing.Size(273, 28);
            this.dateRangeControl1.TabIndex = 5;
            this.dateRangeControl1.Value = ((Savchin.TimeManagment.DateRange)(resources.GetObject("dateRangeControl1.Value")));
            // 
            // grid
            // 
            this.grid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grid.Location = new System.Drawing.Point(3, 34);
            this.grid.Name = "grid";
            this.grid.Size = new System.Drawing.Size(642, 315);
            this.grid.TabIndex = 7;
            // 
            // MineTotalStatistics
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grid);
            this.Controls.Add(this.buttonShow);
            this.Controls.Add(this.dateRangeControl1);
            this.Name = "MineTotalStatistics";
            this.Size = new System.Drawing.Size(648, 349);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonShow;
        private Savchin.Forms.DateRangeControl dateRangeControl1;
        private LabelValueGrid grid;
    }
}
