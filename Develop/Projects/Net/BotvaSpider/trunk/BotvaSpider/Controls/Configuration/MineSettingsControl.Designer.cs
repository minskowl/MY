namespace BotvaSpider.Controls.Configuration
{
    partial class MineSettingsControl
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.boxUseMine = new System.Windows.Forms.CheckBox();
            this.boxUseHelmet = new System.Windows.Forms.CheckBox();
            this.boxUseGlasses = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.gridLimits = new System.Windows.Forms.DataGridView();
            this.ColumnFrom = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnTo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.boxSearchUntilTry = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.smallTicketAction = new BotvaSpider.Controls.Configuration.TicketActionControl();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.bigTicketAction = new BotvaSpider.Controls.Configuration.TicketActionControl();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridLimits)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // boxUseMine
            // 
            this.boxUseMine.AutoSize = true;
            this.boxUseMine.Location = new System.Drawing.Point(9, 19);
            this.boxUseMine.Name = "boxUseMine";
            this.boxUseMine.Size = new System.Drawing.Size(103, 17);
            this.boxUseMine.TabIndex = 18;
            this.boxUseMine.Text = "Ходить в шахту";
            this.boxUseMine.UseVisualStyleBackColor = true;
            // 
            // boxUseHelmet
            // 
            this.boxUseHelmet.AutoSize = true;
            this.boxUseHelmet.Location = new System.Drawing.Point(9, 39);
            this.boxUseHelmet.Name = "boxUseHelmet";
            this.boxUseHelmet.Size = new System.Drawing.Size(132, 17);
            this.boxUseHelmet.TabIndex = 21;
            this.boxUseHelmet.Text = "Использовать Каску";
            this.boxUseHelmet.UseVisualStyleBackColor = true;
            // 
            // boxUseGlasses
            // 
            this.boxUseGlasses.AutoSize = true;
            this.boxUseGlasses.Location = new System.Drawing.Point(9, 59);
            this.boxUseGlasses.Name = "boxUseGlasses";
            this.boxUseGlasses.Size = new System.Drawing.Size(127, 17);
            this.boxUseGlasses.TabIndex = 21;
            this.boxUseGlasses.Text = "Использовать Очки";
            this.boxUseGlasses.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.gridLimits);
            this.groupBox1.Controls.Add(this.boxUseMine);
            this.groupBox1.Controls.Add(this.boxSearchUntilTry);
            this.groupBox1.Controls.Add(this.boxUseGlasses);
            this.groupBox1.Controls.Add(this.boxUseHelmet);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(284, 111);
            this.groupBox1.TabIndex = 23;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Шахта";
            // 
            // gridLimits
            // 
            this.gridLimits.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.gridLimits.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridLimits.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnFrom,
            this.ColumnTo});
            this.gridLimits.Location = new System.Drawing.Point(147, 13);
            this.gridLimits.Name = "gridLimits";
            this.gridLimits.RowHeadersWidth = 21;
            this.gridLimits.Size = new System.Drawing.Size(128, 92);
            this.gridLimits.TabIndex = 25;
            this.toolTip1.SetToolTip(this.gridLimits, "Доспустимы уровни % добычи");
            // 
            // ColumnFrom
            // 
            this.ColumnFrom.DataPropertyName = "From";
            dataGridViewCellStyle1.Format = "##";
            this.ColumnFrom.DefaultCellStyle = dataGridViewCellStyle1;
            this.ColumnFrom.HeaderText = "От";
            this.ColumnFrom.Name = "ColumnFrom";
            this.ColumnFrom.Width = 50;
            // 
            // ColumnTo
            // 
            this.ColumnTo.DataPropertyName = "To";
            dataGridViewCellStyle2.Format = "##";
            this.ColumnTo.DefaultCellStyle = dataGridViewCellStyle2;
            this.ColumnTo.HeaderText = "До";
            this.ColumnTo.Name = "ColumnTo";
            this.ColumnTo.Width = 50;
            // 
            // boxSearchUntilTry
            // 
            this.boxSearchUntilTry.AutoSize = true;
            this.boxSearchUntilTry.Location = new System.Drawing.Point(6, 82);
            this.boxSearchUntilTry.Name = "boxSearchUntilTry";
            this.boxSearchUntilTry.Size = new System.Drawing.Size(124, 17);
            this.boxSearchUntilTry.TabIndex = 21;
            this.boxSearchUntilTry.Text = "Искать до попытки";
            this.boxSearchUntilTry.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.smallTicketAction);
            this.groupBox2.Location = new System.Drawing.Point(3, 118);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(284, 53);
            this.groupBox2.TabIndex = 24;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Малая поляна";
            // 
            // smallTicketAction
            // 
            this.smallTicketAction.Location = new System.Drawing.Point(9, 15);
            this.smallTicketAction.Name = "smallTicketAction";
            this.smallTicketAction.Size = new System.Drawing.Size(239, 32);
            this.smallTicketAction.TabIndex = 25;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.bigTicketAction);
            this.groupBox3.Location = new System.Drawing.Point(3, 175);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(284, 53);
            this.groupBox3.TabIndex = 24;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Большая поляна";
            // 
            // bigTicketAction
            // 
            this.bigTicketAction.Location = new System.Drawing.Point(9, 13);
            this.bigTicketAction.Name = "bigTicketAction";
            this.bigTicketAction.Size = new System.Drawing.Size(239, 34);
            this.bigTicketAction.TabIndex = 25;
            // 
            // MineSettingsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.MinimumSize = new System.Drawing.Size(240, 130);
            this.Name = "MineSettingsControl";
            this.Size = new System.Drawing.Size(318, 237);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridLimits)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox boxUseMine;
        private System.Windows.Forms.CheckBox boxUseHelmet;
        private System.Windows.Forms.CheckBox boxUseGlasses;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView gridLimits;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnFrom;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnTo;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.CheckBox boxSearchUntilTry;
        private TicketActionControl smallTicketAction;
        private System.Windows.Forms.GroupBox groupBox3;
        private TicketActionControl bigTicketAction;
    }
}
