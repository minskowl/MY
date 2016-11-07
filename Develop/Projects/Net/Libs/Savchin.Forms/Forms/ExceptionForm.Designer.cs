namespace Savchin.Forms
{
    partial class ExceptionForm
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
            this.tlpSepButtons = new System.Windows.Forms.TableLayoutPanel();
            this.bShowException = new System.Windows.Forms.Button();
            this.bClose = new System.Windows.Forms.Button();
            this.tlpText = new System.Windows.Forms.TableLayoutPanel();
            this.txtMessage = new System.Windows.Forms.Label();
            this.txtException = new System.Windows.Forms.TextBox();
            this.tlpButtons = new System.Windows.Forms.TableLayoutPanel();
            this.tlpSepButtons.SuspendLayout();
            this.tlpText.SuspendLayout();
            this.tlpButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpSepButtons
            // 
            this.tlpSepButtons.ColumnCount = 2;
            this.tlpSepButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpSepButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpSepButtons.Controls.Add(this.bShowException, 0, 0);
            this.tlpSepButtons.Controls.Add(this.bClose, 0, 0);
            this.tlpSepButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpSepButtons.Location = new System.Drawing.Point(3, 241);
            this.tlpSepButtons.Name = "tlpSepButtons";
            this.tlpSepButtons.RowCount = 1;
            this.tlpSepButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpSepButtons.Size = new System.Drawing.Size(286, 29);
            this.tlpSepButtons.TabIndex = 0;
            // 
            // bShowException
            // 
            this.bShowException.Dock = System.Windows.Forms.DockStyle.Right;
            this.bShowException.Location = new System.Drawing.Point(183, 3);
            this.bShowException.Name = "bShowException";
            this.bShowException.Size = new System.Drawing.Size(100, 23);
            this.bShowException.TabIndex = 5;
            this.bShowException.Text = "Show Exception";
            this.bShowException.UseVisualStyleBackColor = true;
            this.bShowException.Click += new System.EventHandler(this.bShowException_Click);
            // 
            // bClose
            // 
            this.bClose.Dock = System.Windows.Forms.DockStyle.Left;
            this.bClose.Location = new System.Drawing.Point(3, 3);
            this.bClose.Name = "bClose";
            this.bClose.Size = new System.Drawing.Size(75, 23);
            this.bClose.TabIndex = 2;
            this.bClose.Text = "Close";
            this.bClose.UseVisualStyleBackColor = true;
            this.bClose.Click += new System.EventHandler(this.bClose_Click);
            // 
            // tlpText
            // 
            this.tlpText.ColumnCount = 1;
            this.tlpText.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpText.Controls.Add(this.txtMessage, 0, 0);
            this.tlpText.Controls.Add(this.txtException, 0, 1);
            this.tlpText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpText.Location = new System.Drawing.Point(3, 3);
            this.tlpText.Name = "tlpText";
            this.tlpText.RowCount = 2;
            this.tlpText.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpText.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpText.Size = new System.Drawing.Size(286, 232);
            this.tlpText.TabIndex = 1;
            // 
            // txtMessage
            // 
            this.txtMessage.AutoSize = true;
            this.txtMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMessage.Location = new System.Drawing.Point(3, 0);
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(280, 116);
            this.txtMessage.TabIndex = 0;
            this.txtMessage.Text = "Label1";
            // 
            // txtException
            // 
            this.txtException.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtException.Location = new System.Drawing.Point(3, 119);
            this.txtException.Multiline = true;
            this.txtException.Name = "txtException";
            this.txtException.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtException.Size = new System.Drawing.Size(280, 110);
            this.txtException.TabIndex = 1;
            // 
            // tlpButtons
            // 
            this.tlpButtons.ColumnCount = 1;
            this.tlpButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpButtons.Controls.Add(this.tlpSepButtons, 0, 1);
            this.tlpButtons.Controls.Add(this.tlpText, 0, 0);
            this.tlpButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpButtons.Location = new System.Drawing.Point(0, 0);
            this.tlpButtons.Name = "tlpButtons";
            this.tlpButtons.RowCount = 2;
            this.tlpButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tlpButtons.Size = new System.Drawing.Size(292, 273);
            this.tlpButtons.TabIndex = 5;
            // 
            // ExceptionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 273);
            this.Controls.Add(this.tlpButtons);
            this.Name = "ExceptionForm";
            this.Text = "ExceptionForm";
            this.tlpSepButtons.ResumeLayout(false);
            this.tlpText.ResumeLayout(false);
            this.tlpText.PerformLayout();
            this.tlpButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.TableLayoutPanel tlpSepButtons;
        internal System.Windows.Forms.Button bShowException;
        internal System.Windows.Forms.Button bClose;
        internal System.Windows.Forms.TableLayoutPanel tlpText;
        internal System.Windows.Forms.Label txtMessage;
        private System.Windows.Forms.TextBox txtException;
        internal System.Windows.Forms.TableLayoutPanel tlpButtons;
    }
}