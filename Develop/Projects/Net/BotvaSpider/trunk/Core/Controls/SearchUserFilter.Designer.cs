namespace BotvaSpider.Controls
{
    partial class SearchUserFilter
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
            this.filterPage = new BotvaSpider.Controls.RangeFilterControl();
            this.filterLevel = new BotvaSpider.Controls.RangeFilterControl();
            this.buttonOK = new System.Windows.Forms.Button();
            this.listSecond = new System.Windows.Forms.ComboBox();
            this.labelSecondList = new System.Windows.Forms.Label();
            this.listRace = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.boxImport = new System.Windows.Forms.CheckBox();
            this.boxSkillDifference = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.boxSkillDifference)).BeginInit();
            this.SuspendLayout();
            // 
            // filterPage
            // 
            this.filterPage.Checked = true;
            this.filterPage.Location = new System.Drawing.Point(3, 31);
            this.filterPage.Name = "filterPage";
            this.filterPage.Size = new System.Drawing.Size(181, 59);
            this.filterPage.TabIndex = 0;
            this.filterPage.Title = "Страницы";
            // 
            // filterLevel
            // 
            this.filterLevel.Checked = false;
            this.filterLevel.Location = new System.Drawing.Point(184, 31);
            this.filterLevel.Name = "filterLevel";
            this.filterLevel.Size = new System.Drawing.Size(181, 59);
            this.filterLevel.TabIndex = 1;
            this.filterLevel.Title = "Уровень";
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(282, 121);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 2;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // listSecond
            // 
            this.listSecond.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.listSecond.FormattingEnabled = true;
            this.listSecond.Location = new System.Drawing.Point(236, 4);
            this.listSecond.Name = "listSecond";
            this.listSecond.Size = new System.Drawing.Size(121, 21);
            this.listSecond.TabIndex = 3;
            // 
            // labelSecondList
            // 
            this.labelSecondList.AutoSize = true;
            this.labelSecondList.Location = new System.Drawing.Point(181, 4);
            this.labelSecondList.Name = "labelSecondList";
            this.labelSecondList.Size = new System.Drawing.Size(49, 13);
            this.labelSecondList.TabIndex = 4;
            this.labelSecondList.Text = "Гильдия";
            // 
            // listRace
            // 
            this.listRace.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.listRace.FormattingEnabled = true;
            this.listRace.Location = new System.Drawing.Point(54, 4);
            this.listRace.Name = "listRace";
            this.listRace.Size = new System.Drawing.Size(121, 21);
            this.listRace.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(0, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Расса";
            // 
            // boxImport
            // 
            this.boxImport.AutoSize = true;
            this.boxImport.Location = new System.Drawing.Point(184, 87);
            this.boxImport.Name = "boxImport";
            this.boxImport.Size = new System.Drawing.Size(174, 17);
            this.boxImport.TabIndex = 5;
            this.boxImport.Text = "Импортировать в базу коров";
            this.boxImport.UseVisualStyleBackColor = true;
            // 
            // boxSkillDifference
            // 
            this.boxSkillDifference.Location = new System.Drawing.Point(101, 82);
            this.boxSkillDifference.Name = "boxSkillDifference";
            this.boxSkillDifference.Size = new System.Drawing.Size(74, 20);
            this.boxSkillDifference.TabIndex = 6;
            this.boxSkillDifference.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 84);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Разница стат от ";
            // 
            // SearchUserFilter
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(361, 147);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.boxSkillDifference);
            this.Controls.Add(this.boxImport);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.labelSecondList);
            this.Controls.Add(this.listRace);
            this.Controls.Add(this.listSecond);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.filterLevel);
            this.Controls.Add(this.filterPage);
            this.Name = "SearchUserFilter";
            this.Text = "SearchUserFilter";
            ((System.ComponentModel.ISupportInitialize)(this.boxSkillDifference)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private RangeFilterControl filterPage;
        private RangeFilterControl filterLevel;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.ComboBox listSecond;
        private System.Windows.Forms.Label labelSecondList;
        private System.Windows.Forms.ComboBox listRace;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox boxImport;
        private System.Windows.Forms.NumericUpDown boxSkillDifference;
        private System.Windows.Forms.Label label1;

    }
}