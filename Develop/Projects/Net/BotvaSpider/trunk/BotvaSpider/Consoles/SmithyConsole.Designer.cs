namespace BotvaSpider.Consoles
{
    partial class SmithyConsole
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.wormHoleControl1 = new BotvaSpider.Controls.WormHoleControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.foundryContro1 = new BotvaSpider.Controls.FoundryControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.minerControl1 = new BotvaSpider.Controls.MinerControl();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(475, 259);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.wormHoleControl1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(467, 233);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Очистка";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // wormHoleControl1
            // 
            this.wormHoleControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wormHoleControl1.Location = new System.Drawing.Point(3, 3);
            this.wormHoleControl1.Name = "wormHoleControl1";
            this.wormHoleControl1.Size = new System.Drawing.Size(461, 227);
            this.wormHoleControl1.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.foundryContro1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(467, 233);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Плавка Статистика";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // foundryContro1
            // 
            this.foundryContro1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.foundryContro1.Location = new System.Drawing.Point(3, 3);
            this.foundryContro1.Name = "foundryContro1";
            this.foundryContro1.Size = new System.Drawing.Size(461, 227);
            this.foundryContro1.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.minerControl1);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(467, 233);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Плавка Сапер";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // minerControl1
            // 
            this.minerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.minerControl1.Location = new System.Drawing.Point(3, 3);
            this.minerControl1.Name = "minerControl1";
            this.minerControl1.Size = new System.Drawing.Size(461, 227);
            this.minerControl1.TabIndex = 0;
            // 
            // SmithyConsole
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(475, 259);
            this.Controls.Add(this.tabControl1);
            this.MinimumSize = new System.Drawing.Size(470, 27);
            this.Name = "SmithyConsole";
            this.TabText = "Кузница";
            this.Text = "Кузница";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private BotvaSpider.Controls.WormHoleControl wormHoleControl1;
        private BotvaSpider.Controls.FoundryControl foundryContro1;
        private System.Windows.Forms.TabPage tabPage3;
        private BotvaSpider.Controls.MinerControl minerControl1;

    }
}