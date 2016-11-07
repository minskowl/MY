<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form2
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.CompareView1 = New Savchin.Controls.Common.CompareView()
        Me.SuspendLayout()
        '
        'CompareView1
        '
        Me.CompareView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CompareView1.Location = New System.Drawing.Point(0, 0)
        Me.CompareView1.Name = "CompareView1"
        Me.CompareView1.Size = New System.Drawing.Size(617, 425)
        Me.CompareView1.TabIndex = 0
        '
        'Form2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(617, 425)
        Me.Controls.Add(Me.CompareView1)
        Me.Name = "Form2"
        Me.Text = "Form2"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents CompareView1 As Savchin.Controls.Common.CompareView
End Class
