namespace BotvaSpider.Controls
{
    partial class FarmControl
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
            this.columnID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnUser = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnLevel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnCoulomb = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.columnAverageBenefit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnUserType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.ColumnCristals = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnSafe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxUser = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxUserType = new System.Windows.Forms.ComboBox();
            this.numericUpDownLevelFrom = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.numericUpDownLevelTo = new System.Windows.Forms.NumericUpDown();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.showFightLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLevelFrom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLevelTo)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnID,
            this.columnUser,
            this.columnLevel,
            this.columnCoulomb,
            this.columnAverageBenefit,
            this.columnUserType,
            this.ColumnCristals,
            this.ColumnSafe});
            this.dataGridView1.ContextMenuStrip = this.contextMenuStrip1;
            this.dataGridView1.Location = new System.Drawing.Point(4, 32);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(643, 327);
            this.dataGridView1.TabIndex = 2;
            this.dataGridView1.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.dataGridView1_UserDeletingRow);
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            this.dataGridView1.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellEndEdit);
            this.dataGridView1.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridView1_DataError);
            // 
            // columnID
            // 
            this.columnID.DataPropertyName = "UserID";
            this.columnID.HeaderText = "ID";
            this.columnID.Name = "columnID";
            this.columnID.ReadOnly = true;
            this.columnID.Width = 55;
            // 
            // columnUser
            // 
            this.columnUser.DataPropertyName = "UserName";
            this.columnUser.HeaderText = "Юзер";
            this.columnUser.Name = "columnUser";
            // 
            // columnLevel
            // 
            this.columnLevel.DataPropertyName = "Level";
            this.columnLevel.HeaderText = "Уровень";
            this.columnLevel.Name = "columnLevel";
            this.columnLevel.Width = 50;
            // 
            // columnCoulomb
            // 
            this.columnCoulomb.DataPropertyName = "MilkingCoulomb";
            this.columnCoulomb.HeaderText = "Кулон";
            this.columnCoulomb.Name = "columnCoulomb";
            // 
            // columnAverageBenefit
            // 
            this.columnAverageBenefit.DataPropertyName = "AverageBenefit";
            this.columnAverageBenefit.HeaderText = "Средний доход";
            this.columnAverageBenefit.Name = "columnAverageBenefit";
            this.columnAverageBenefit.ReadOnly = true;
            // 
            // columnUserType
            // 
            this.columnUserType.DataPropertyName = "UserType";
            this.columnUserType.HeaderText = "Тип";
            this.columnUserType.Name = "columnUserType";
            this.columnUserType.Width = 70;
            // 
            // ColumnCristals
            // 
            this.ColumnCristals.DataPropertyName = "AverageCristals";
            this.ColumnCristals.HeaderText = "Кристалы";
            this.ColumnCristals.Name = "ColumnCristals";
            this.ColumnCristals.ReadOnly = true;
            // 
            // ColumnSafe
            // 
            this.ColumnSafe.DataPropertyName = "Safe";
            this.ColumnSafe.HeaderText = "Сейф";
            this.ColumnSafe.Name = "ColumnSafe";
            this.ColumnSafe.ReadOnly = true;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showFightLogToolStripMenuItem,
            this.toolStripMenuItem1,
            this.deleteToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(170, 76);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.deleteToolStripMenuItem.Text = "Удалить";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Юзер";
            // 
            // textBoxUser
            // 
            this.textBoxUser.Location = new System.Drawing.Point(41, 8);
            this.textBoxUser.Name = "textBoxUser";
            this.textBoxUser.Size = new System.Drawing.Size(100, 20);
            this.textBoxUser.TabIndex = 4;
            this.textBoxUser.TextChanged += new System.EventHandler(this.textBoxUser_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(147, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Левел от";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(322, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(26, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Тип";
            // 
            // comboBoxUserType
            // 
            this.comboBoxUserType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxUserType.FormattingEnabled = true;
            this.comboBoxUserType.Location = new System.Drawing.Point(362, 8);
            this.comboBoxUserType.Name = "comboBoxUserType";
            this.comboBoxUserType.Size = new System.Drawing.Size(121, 21);
            this.comboBoxUserType.TabIndex = 8;
            this.comboBoxUserType.SelectedIndexChanged += new System.EventHandler(this.comboBoxUserType_SelectedIndexChanged);
            // 
            // numericUpDownLevelFrom
            // 
            this.numericUpDownLevelFrom.Location = new System.Drawing.Point(200, 8);
            this.numericUpDownLevelFrom.Name = "numericUpDownLevelFrom";
            this.numericUpDownLevelFrom.Size = new System.Drawing.Size(42, 20);
            this.numericUpDownLevelFrom.TabIndex = 9;
            this.numericUpDownLevelFrom.ValueChanged += new System.EventHandler(this.numericUpDownLevelFrom_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(241, 8);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(19, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "до";
            // 
            // numericUpDownLevelTo
            // 
            this.numericUpDownLevelTo.Location = new System.Drawing.Point(264, 8);
            this.numericUpDownLevelTo.Name = "numericUpDownLevelTo";
            this.numericUpDownLevelTo.Size = new System.Drawing.Size(42, 20);
            this.numericUpDownLevelTo.TabIndex = 9;
            this.numericUpDownLevelTo.ValueChanged += new System.EventHandler(this.numericUpDownLevelTo_ValueChanged);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(166, 6);
            // 
            // showFightLogToolStripMenuItem
            // 
            this.showFightLogToolStripMenuItem.Name = "showFightLogToolStripMenuItem";
            this.showFightLogToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.showFightLogToolStripMenuItem.Text = "Показать лог битв";
            this.showFightLogToolStripMenuItem.Click += new System.EventHandler(this.showFightLogToolStripMenuItem_Click);
            // 
            // FarmControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.numericUpDownLevelTo);
            this.Controls.Add(this.numericUpDownLevelFrom);
            this.Controls.Add(this.comboBoxUserType);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxUser);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "FarmControl";
            this.Size = new System.Drawing.Size(650, 362);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLevelFrom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLevelTo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxUser;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnID;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnUser;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnLevel;
        private System.Windows.Forms.DataGridViewComboBoxColumn columnCoulomb;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnAverageBenefit;
        private System.Windows.Forms.DataGridViewComboBoxColumn columnUserType;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnCristals;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnSafe;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBoxUserType;
        private System.Windows.Forms.NumericUpDown numericUpDownLevelFrom;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numericUpDownLevelTo;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showFightLogToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
    }
}
