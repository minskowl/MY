namespace BotvaSpider.Controls.Configuration
{
    partial class TicketActionControl
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
            this.listActionType = new System.Windows.Forms.ComboBox();
            this.panelPrice = new System.Windows.Forms.Panel();
            this.boxPrice = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.panelPrice.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.boxPrice)).BeginInit();
            this.SuspendLayout();
            // 
            // listActionType
            // 
            this.listActionType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.listActionType.FormattingEnabled = true;
            this.listActionType.Location = new System.Drawing.Point(3, 3);
            this.listActionType.Name = "listActionType";
            this.listActionType.Size = new System.Drawing.Size(121, 21);
            this.listActionType.TabIndex = 0;
            this.listActionType.SelectedIndexChanged += new System.EventHandler(this.listActionType_SelectedIndexChanged);
            // 
            // panelPrice
            // 
            this.panelPrice.Controls.Add(this.boxPrice);
            this.panelPrice.Controls.Add(this.label1);
            this.panelPrice.Location = new System.Drawing.Point(126, 3);
            this.panelPrice.Name = "panelPrice";
            this.panelPrice.Size = new System.Drawing.Size(96, 22);
            this.panelPrice.TabIndex = 1;
            // 
            // boxPrice
            // 
            this.boxPrice.Location = new System.Drawing.Point(41, 2);
            this.boxPrice.Maximum = new decimal(new int[] {
            13,
            0,
            0,
            0});
            this.boxPrice.Name = "boxPrice";
            this.boxPrice.Size = new System.Drawing.Size(48, 20);
            this.boxPrice.TabIndex = 1;
            this.boxPrice.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "цена";
            // 
            // TicketActionControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelPrice);
            this.Controls.Add(this.listActionType);
            this.Name = "TicketActionControl";
            this.Size = new System.Drawing.Size(239, 29);
            this.panelPrice.ResumeLayout(false);
            this.panelPrice.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.boxPrice)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox listActionType;
        private System.Windows.Forms.Panel panelPrice;
        private System.Windows.Forms.NumericUpDown boxPrice;
        private System.Windows.Forms.Label label1;
    }
}
