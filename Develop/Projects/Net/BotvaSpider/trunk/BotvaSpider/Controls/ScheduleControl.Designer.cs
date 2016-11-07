namespace BotvaSpider.Controls
{
    partial class ScheduleControl
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
            this.ColumnFromHour = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.ColumnFromMinute = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.ColumnToHour = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.ColumnToMinute = new System.Windows.Forms.DataGridViewComboBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnFromHour,
            this.ColumnFromMinute,
            this.ColumnToHour,
            this.ColumnToMinute});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(541, 303);
            this.dataGridView1.TabIndex = 2;
            this.dataGridView1.RowValidating += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dataGridView1_RowValidating);
            // 
            // ColumnFromHour
            // 
            this.ColumnFromHour.HeaderText = "Час";
            this.ColumnFromHour.Name = "ColumnFromHour";
            this.ColumnFromHour.Width = 50;
            // 
            // ColumnFromMinute
            // 
            this.ColumnFromMinute.HeaderText = "Мин";
            this.ColumnFromMinute.Name = "ColumnFromMinute";
            this.ColumnFromMinute.Width = 50;
            // 
            // ColumnToHour
            // 
            this.ColumnToHour.HeaderText = "Час";
            this.ColumnToHour.Name = "ColumnToHour";
            this.ColumnToHour.Width = 50;
            // 
            // ColumnToMinute
            // 
            this.ColumnToMinute.HeaderText = "Мин";
            this.ColumnToMinute.Name = "ColumnToMinute";
            this.ColumnToMinute.Width = 50;
            // 
            // ScheduleControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dataGridView1);
            this.Name = "ScheduleControl";
            this.Size = new System.Drawing.Size(541, 303);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewComboBoxColumn ColumnFromHour;
        private System.Windows.Forms.DataGridViewComboBoxColumn ColumnFromMinute;
        private System.Windows.Forms.DataGridViewComboBoxColumn ColumnToHour;
        private System.Windows.Forms.DataGridViewComboBoxColumn ColumnToMinute;
    }
}
