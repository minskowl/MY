namespace Savchin.Data.Schema.Controls
{
    partial class SchemaBrowser
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SchemaBrowser));
            this.ilObj = new System.Windows.Forms.ImageList(this.components);
            this.tvObj = new Savchin.Forms.TreeViewEx();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // ilObj
            // 
            this.ilObj.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilObj.ImageStream")));
            this.ilObj.TransparentColor = System.Drawing.Color.Transparent;
            this.ilObj.Images.SetKeyName(0, "diagr.ico");
            this.ilObj.Images.SetKeyName(1, "fk.ico");
            this.ilObj.Images.SetKeyName(2, "fold.ico");
            this.ilObj.Images.SetKeyName(3, "fold_open.ico");
            this.ilObj.Images.SetKeyName(4, "pk.ico");
            this.ilObj.Images.SetKeyName(5, "sp.ico");
            this.ilObj.Images.SetKeyName(6, "tab.ico");
            this.ilObj.Images.SetKeyName(7, "load.gif");
            this.ilObj.Images.SetKeyName(8, "column.gif");
            // 
            // tvObj
            // 
            this.tvObj.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tvObj.ImageIndex = 0;
            this.tvObj.ImageList = this.ilObj;
            this.tvObj.Location = new System.Drawing.Point(0, 27);
            this.tvObj.Name = "tvObj";
            this.tvObj.SelectedImageIndex = 0;
            this.tvObj.Size = new System.Drawing.Size(217, 231);
            this.tvObj.TabIndex = 0;
            this.tvObj.DoubleClick += new System.EventHandler(this.tvObj_DoubleClick);
            this.tvObj.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvObj_AfterSelect);
            this.tvObj.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvObj_NodeMouseClick);
            this.tvObj.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.tvObj_AfterExpand);
            // 
            // comboBox1
            // 
            this.comboBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(0, 0);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(217, 21);
            this.comboBox1.TabIndex = 1;
            // 
            // SchemaBrowser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.tvObj);
            this.Name = "SchemaBrowser";
            this.Size = new System.Drawing.Size(217, 258);
            this.ResumeLayout(false);

        }

        #endregion

        private Savchin.Forms.TreeViewEx tvObj;
        private System.Windows.Forms.ImageList ilObj;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}
