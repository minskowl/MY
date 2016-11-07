using Savchin.Drawing;

namespace ControlTest
{
    partial class Form1
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
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.button1 = new System.Windows.Forms.Button();
            this.colorPicker1 = new Savchin.Forms.ColorPicker();
            this.HSBCheckBox = new System.Windows.Forms.CheckBox();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // colorPicker1
            // 
            this.colorPicker1.Location = new System.Drawing.Point(14, 71);
            this.colorPicker1.Mode = Savchin.Drawing.ColorSheme.RGB;
            this.colorPicker1.Name = "colorPicker1";
            this.colorPicker1.Size = new System.Drawing.Size(266, 318);
            this.colorPicker1.TabIndex = 1;
            this.colorPicker1.Value = System.Drawing.Color.Empty;
            // 
            // HSBCheckBox
            // 
            this.HSBCheckBox.AutoSize = true;
            this.HSBCheckBox.Location = new System.Drawing.Point(104, 16);
            this.HSBCheckBox.Name = "HSBCheckBox";
            this.HSBCheckBox.Size = new System.Drawing.Size(48, 17);
            this.HSBCheckBox.TabIndex = 2;
            this.HSBCheckBox.Text = "HSB";
            this.HSBCheckBox.UseVisualStyleBackColor = true;
            this.HSBCheckBox.CheckedChanged += new System.EventHandler(this.HSBCheckBox_CheckedChanged);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(172, 11);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 401);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.HSBCheckBox);
            this.Controls.Add(this.colorPicker1);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Button button1;
        private Savchin.Forms.ColorPicker colorPicker1;
        private System.Windows.Forms.CheckBox HSBCheckBox;
        private System.Windows.Forms.Button button2;
    }
}

