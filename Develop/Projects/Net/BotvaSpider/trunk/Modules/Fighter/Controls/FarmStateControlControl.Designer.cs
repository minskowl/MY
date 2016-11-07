namespace BotvaSpider.Fighter
{
    partial class FarmStateControlControl
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
            this.ColumnUserName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnLevel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnState = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnAverageBenefit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnLastBenefit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnMilkingCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnReadyAgain = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                                                                                                  this.ColumnUserName,
                                                                                                  this.ColumnLevel,
                                                                                                  this.ColumnState,
                                                                                                  this.ColumnAverageBenefit,
                                                                                                  this.ColumnLastBenefit,
                                                                                                  this.ColumnMilkingCount,
                                                                                                  this.ColumnReadyAgain});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(670, 404);
            this.dataGridView1.TabIndex = 0;
            // 
            // ColumnUserName
            // 
            this.ColumnUserName.DataPropertyName = "UserName";
            this.ColumnUserName.HeaderText = "Юзер";
            this.ColumnUserName.Name = "ColumnUserName";
            this.ColumnUserName.ReadOnly = true;
            // 
            // ColumnLevel
            // 
            this.ColumnLevel.DataPropertyName = "Level";
            this.ColumnLevel.HeaderText = "Уровень";
            this.ColumnLevel.Name = "ColumnLevel";
            this.ColumnLevel.ReadOnly = true;
            this.ColumnLevel.Width = 50;
            // 
            // ColumnState
            // 
            this.ColumnState.DataPropertyName = "State";
            this.ColumnState.HeaderText = "Состояние";
            this.ColumnState.Name = "ColumnState";
            this.ColumnState.ReadOnly = true;
            this.ColumnState.Width = 80;
            // 
            // ColumnAverageBenefit
            // 
            this.ColumnAverageBenefit.DataPropertyName = "AverageBenefit";
            this.ColumnAverageBenefit.HeaderText = "Ср. Доход";
            this.ColumnAverageBenefit.Name = "ColumnAverageBenefit";
            this.ColumnAverageBenefit.ReadOnly = true;
            // 
            // ColumnLastBenefit
            // 
            this.ColumnLastBenefit.DataPropertyName = "LastBenefit";
            this.ColumnLastBenefit.HeaderText = "Последний Доход";
            this.ColumnLastBenefit.Name = "ColumnLastBenefit";
            this.ColumnLastBenefit.ReadOnly = true;
            // 
            // ColumnMilkingCount
            // 
            this.ColumnMilkingCount.DataPropertyName = "MilkingCount";
            this.ColumnMilkingCount.HeaderText = "Кол-во доек";
            this.ColumnMilkingCount.Name = "ColumnMilkingCount";
            this.ColumnMilkingCount.ReadOnly = true;
            // 
            // ColumnReadyAgain
            // 
            this.ColumnReadyAgain.DataPropertyName = "ReadyAgain";
            this.ColumnReadyAgain.HeaderText = "Время готовности";
            this.ColumnReadyAgain.Name = "ColumnReadyAgain";
            this.ColumnReadyAgain.ReadOnly = true;
            // 
            // FarmStateControlControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dataGridView1);
            this.Name = "FarmStateControlControl";
            this.Size = new System.Drawing.Size(670, 404);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnUserName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnLevel;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnState;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnAverageBenefit;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnLastBenefit;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnMilkingCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnReadyAgain;
    }
}