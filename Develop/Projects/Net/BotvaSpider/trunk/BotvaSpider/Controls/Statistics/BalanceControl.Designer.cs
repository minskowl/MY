namespace BotvaSpider.Controls.Statistics
{
    partial class BalanceControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BalanceControl));
            this.dateRangeControl1 = new Savchin.Forms.DateRangeControl();
            this.buttonShow = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.gridTotal = new BotvaSpider.Controls.LabelValueGrid();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.gridMine = new BotvaSpider.Controls.LabelValueGrid();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.gridFights = new BotvaSpider.Controls.LabelValueGrid();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.gridGlade = new BotvaSpider.Controls.LabelValueGrid();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.SuspendLayout();
            // 
            // dateRangeControl1
            // 
            this.dateRangeControl1.Location = new System.Drawing.Point(3, 3);
            this.dateRangeControl1.Name = "dateRangeControl1";
            this.dateRangeControl1.Size = new System.Drawing.Size(273, 28);
            this.dateRangeControl1.TabIndex = 6;
            this.dateRangeControl1.Value = ((Savchin.TimeManagment.DateRange)(resources.GetObject("dateRangeControl1.Value")));
            // 
            // buttonShow
            // 
            this.buttonShow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonShow.Location = new System.Drawing.Point(345, 3);
            this.buttonShow.Name = "buttonShow";
            this.buttonShow.Size = new System.Drawing.Size(75, 23);
            this.buttonShow.TabIndex = 7;
            this.buttonShow.Text = "Показать";
            this.buttonShow.UseVisualStyleBackColor = true;
            this.buttonShow.Click += new System.EventHandler(this.buttonShow_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Location = new System.Drawing.Point(3, 32);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(417, 248);
            this.tabControl1.TabIndex = 8;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.gridTotal);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(409, 222);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Суммарная";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // gridTotal
            // 
            this.gridTotal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridTotal.Location = new System.Drawing.Point(3, 3);
            this.gridTotal.Name = "gridTotal";
            this.gridTotal.Size = new System.Drawing.Size(403, 216);
            this.gridTotal.TabIndex = 9;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.gridMine);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(409, 222);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Шахта";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // gridMine
            // 
            this.gridMine.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridMine.Location = new System.Drawing.Point(3, 3);
            this.gridMine.Name = "gridMine";
            this.gridMine.Size = new System.Drawing.Size(403, 216);
            this.gridMine.TabIndex = 10;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.gridFights);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(409, 222);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Бои";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // gridFights
            // 
            this.gridFights.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridFights.Location = new System.Drawing.Point(3, 3);
            this.gridFights.Name = "gridFights";
            this.gridFights.Size = new System.Drawing.Size(403, 216);
            this.gridFights.TabIndex = 10;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.gridGlade);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(409, 222);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Поляны";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // gridGlade
            // 
            this.gridGlade.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridGlade.Location = new System.Drawing.Point(3, 3);
            this.gridGlade.Name = "gridGlade";
            this.gridGlade.Size = new System.Drawing.Size(403, 216);
            this.gridGlade.TabIndex = 11;
            // 
            // BalanceControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.buttonShow);
            this.Controls.Add(this.dateRangeControl1);
            this.Name = "BalanceControl";
            this.Size = new System.Drawing.Size(423, 283);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Savchin.Forms.DateRangeControl dateRangeControl1;
        private System.Windows.Forms.Button buttonShow;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private LabelValueGrid gridTotal;
        private System.Windows.Forms.TabPage tabPage2;
        private LabelValueGrid gridMine;
        private System.Windows.Forms.TabPage tabPage3;
        private LabelValueGrid gridFights;
        private System.Windows.Forms.TabPage tabPage4;
        private LabelValueGrid gridGlade;
    }
}
