
using Savchin.Forms.RichText.Editor;

namespace Savchin.Extensions
{
    partial class DescriptionControl
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
            this.editor = new Savchin.Forms.RichText.Editor.RichTextEditor();
            this.SuspendLayout();
            // 
            // editor
            // 
            this.editor.AcceptsReturn = true;
            this.editor.AcceptsTab = true;
            this.editor.AutoSize = true;
            this.editor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.editor.Location = new System.Drawing.Point(0, 0);
            this.editor.Name = "editor";
            this.editor.Size = new System.Drawing.Size(350, 450);
            this.editor.TabIndex = 2;
            // 
            // DescriptionControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.editor);
            this.Name = "DescriptionControl";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private RichTextEditor editor;


    }
}
