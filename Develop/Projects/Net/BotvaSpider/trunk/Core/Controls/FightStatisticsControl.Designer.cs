namespace BotvaSpider.Controls.Statistics
{
    partial class FightStatisticsControl
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.ColumnData = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnGold = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnCrystal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnExpirience = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnFromFarm = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.totalStatistics = new BotvaSpider.Controls.LabelValueGrid();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnData,
            this.ColumnGold,
            this.ColumnCrystal,
            this.ColumnExpirience,
            this.ColumnFromFarm});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(600, 296);
            this.dataGridView1.TabIndex = 0;
            // 
            // ColumnData
            // 
            this.ColumnData.DataPropertyName = "Date";
            this.ColumnData.HeaderText = "Дата";
            this.ColumnData.Name = "ColumnData";
            this.ColumnData.ReadOnly = true;
            // 
            // ColumnGold
            // 
            this.ColumnGold.DataPropertyName = "Money";
            this.ColumnGold.HeaderText = "Золото";
            this.ColumnGold.Name = "ColumnGold";
            this.ColumnGold.ReadOnly = true;
            this.ColumnGold.Width = 80;
            // 
            // ColumnCrystal
            // 
            this.ColumnCrystal.DataPropertyName = "Cristals";
            this.ColumnCrystal.HeaderText = "Кристалы";
            this.ColumnCrystal.Name = "ColumnCrystal";
            this.ColumnCrystal.ReadOnly = true;
            this.ColumnCrystal.Width = 60;
            // 
            // ColumnExpirience
            // 
            this.ColumnExpirience.DataPropertyName = "Expirience";
            this.ColumnExpirience.HeaderText = "Опыт";
            this.ColumnExpirience.Name = "ColumnExpirience";
            this.ColumnExpirience.ReadOnly = true;
            this.ColumnExpirience.Width = 50;
            // 
            // ColumnFromFarm
            // 
            this.ColumnFromFarm.DataPropertyName = "IsFarm";
            this.ColumnFromFarm.HeaderText = "Бой по ферме";
            this.ColumnFromFarm.Name = "ColumnFromFarm";
            this.ColumnFromFarm.ReadOnly = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(614, 328);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dataGridView1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(606, 302);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Бои";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.totalStatistics);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(606, 302);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Статистика";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // totalStatistics
            // 
            this.totalStatistics.Dock = System.Windows.Forms.DockStyle.Fill;
            this.totalStatistics.Location = new System.Drawing.Point(3, 3);
            this.totalStatistics.Name = "totalStatistics";
            this.totalStatistics.Size = new System.Drawing.Size(600, 296);
            this.totalStatistics.TabIndex = 0;
            // 
            // FightStatisticsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Name = "FightStatisticsControl";
            this.Size = new System.Drawing.Size(614, 328);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnData;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnGold;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnCrystal;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnExpirience;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ColumnFromFarm;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private LabelValueGrid totalStatistics;
    }
}