namespace BotvaSpider.Controls.Configuration
{
    partial class RivalSourcesControl
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
            this.components = new System.ComponentModel.Container();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.columnEnabled = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.columnSource = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.сolumnAttempts = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnLevelFrom = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnLevelTo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnCoulomb = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.upToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.downToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnEnabled,
            this.columnSource,
            this.сolumnAttempts,
            this.ColumnLevelFrom,
            this.ColumnLevelTo,
            this.ColumnCoulomb});
            this.dataGridView1.ContextMenuStrip = this.contextMenuStrip1;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 25;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(513, 209);
            this.dataGridView1.TabIndex = 2;
            // 
            // columnEnabled
            // 
            this.columnEnabled.DataPropertyName = "Enabled";
            this.columnEnabled.HeaderText = "Включен";
            this.columnEnabled.Name = "columnEnabled";
            this.columnEnabled.Width = 40;
            // 
            // columnSource
            // 
            this.columnSource.DataPropertyName = "Source";
            this.columnSource.HeaderText = "Источник";
            this.columnSource.Name = "columnSource";
            this.columnSource.Width = 150;
            // 
            // сolumnAttempts
            // 
            this.сolumnAttempts.DataPropertyName = "MaxAttemptCount";
            this.сolumnAttempts.HeaderText = "Попыток";
            this.сolumnAttempts.Name = "сolumnAttempts";
            this.сolumnAttempts.Width = 50;
            // 
            // ColumnLevelFrom
            // 
            this.ColumnLevelFrom.DataPropertyName = "LevelFrom";
            this.ColumnLevelFrom.HeaderText = "Ур. от";
            this.ColumnLevelFrom.Name = "ColumnLevelFrom";
            this.ColumnLevelFrom.Width = 70;
            // 
            // ColumnLevelTo
            // 
            this.ColumnLevelTo.DataPropertyName = "LevelTo";
            this.ColumnLevelTo.HeaderText = "Ур. до";
            this.ColumnLevelTo.Name = "ColumnLevelTo";
            this.ColumnLevelTo.Width = 70;
            // 
            // ColumnCoulomb
            // 
            this.ColumnCoulomb.DataPropertyName = "Coulomb";
            this.ColumnCoulomb.HeaderText = "Кулон";
            this.ColumnCoulomb.Name = "ColumnCoulomb";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.upToolStripMenuItem,
            this.downToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(106, 48);
            // 
            // upToolStripMenuItem
            // 
            this.upToolStripMenuItem.Name = "upToolStripMenuItem";
            this.upToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.upToolStripMenuItem.Text = "Вверх";
            this.upToolStripMenuItem.Click += new System.EventHandler(this.upToolStripMenuItem_Click);
            // 
            // downToolStripMenuItem
            // 
            this.downToolStripMenuItem.Name = "downToolStripMenuItem";
            this.downToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.downToolStripMenuItem.Text = "Вниз";
            this.downToolStripMenuItem.Click += new System.EventHandler(this.downToolStripMenuItem_Click);
            // 
            // RivalSourcesControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dataGridView1);
            this.Name = "RivalSourcesControl";
            this.Size = new System.Drawing.Size(513, 209);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn columnEnabled;
        private System.Windows.Forms.DataGridViewComboBoxColumn columnSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn сolumnAttempts;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnLevelFrom;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnLevelTo;
        private System.Windows.Forms.DataGridViewComboBoxColumn ColumnCoulomb;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem upToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem downToolStripMenuItem;
    }
}
