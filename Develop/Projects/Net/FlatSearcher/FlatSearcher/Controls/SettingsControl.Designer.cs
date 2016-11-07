namespace FlatSearcher.Controls
{
    partial class SettingsControl
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
            this.listAddresees = new System.Windows.Forms.CheckedListBox();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.checkBoxFilterByAddress = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // listAddresees
            // 
            this.listAddresees.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listAddresees.FormattingEnabled = true;
            this.listAddresees.Location = new System.Drawing.Point(182, 4);
            this.listAddresees.Name = "listAddresees";
            this.listAddresees.Size = new System.Drawing.Size(237, 379);
            this.listAddresees.TabIndex = 0;
            this.listAddresees.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.listAddresees_ItemCheck);
            this.listAddresees.DoubleClick += new System.EventHandler(this.listAddresees_DoubleClick);
            // 
            // checkBoxFilterByAddress
            // 
            this.checkBoxFilterByAddress.AutoSize = true;
            this.checkBoxFilterByAddress.Checked = global::FlatSearcher.Properties.Settings.Default.FilterByAddress;
            this.checkBoxFilterByAddress.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::FlatSearcher.Properties.Settings.Default, "FilterByAddress", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.checkBoxFilterByAddress.Location = new System.Drawing.Point(13, 4);
            this.checkBoxFilterByAddress.Name = "checkBoxFilterByAddress";
            this.checkBoxFilterByAddress.Size = new System.Drawing.Size(84, 17);
            this.checkBoxFilterByAddress.TabIndex = 1;
            this.checkBoxFilterByAddress.Text = "По адрессу";
            this.checkBoxFilterByAddress.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = global::FlatSearcher.Properties.Settings.Default.FilterByFlat;
            this.checkBox1.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::FlatSearcher.Properties.Settings.Default, "FilterByFlat", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.checkBox1.Location = new System.Drawing.Point(13, 28);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(90, 17);
            this.checkBox1.TabIndex = 2;
            this.checkBox1.Text = "По квартире";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Checked = global::FlatSearcher.Properties.Settings.Default.RestoreQuery;
            this.checkBox2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox2.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::FlatSearcher.Properties.Settings.Default, "RestoreQuery", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.checkBox2.Location = new System.Drawing.Point(13, 61);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(148, 17);
            this.checkBox2.TabIndex = 3;
            this.checkBox2.Text = "Востанавливать запрос";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // SettingsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.checkBoxFilterByAddress);
            this.Controls.Add(this.listAddresees);
            this.Name = "SettingsControl";
            this.Size = new System.Drawing.Size(433, 401);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckedListBox listAddresees;
        private System.Windows.Forms.CheckBox checkBoxFilterByAddress;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox2;
    }
}
