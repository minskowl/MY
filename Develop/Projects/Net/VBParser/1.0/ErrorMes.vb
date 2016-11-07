Public Class ErrorMes
    Inherits System.Windows.Forms.Form
    Const marg As Integer = 25
#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents bClose As System.Windows.Forms.Button
    Friend WithEvents txtMessage As System.Windows.Forms.TextBox
    Friend WithEvents txtError As System.Windows.Forms.TextBox
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.bClose = New System.Windows.Forms.Button
        Me.txtMessage = New System.Windows.Forms.TextBox
        Me.txtError = New System.Windows.Forms.TextBox
        Me.Splitter1 = New System.Windows.Forms.Splitter
        Me.SuspendLayout()
        '
        'bClose
        '
        Me.bClose.Location = New System.Drawing.Point(440, 360)
        Me.bClose.Name = "bClose"
        Me.bClose.Size = New System.Drawing.Size(80, 24)
        Me.bClose.TabIndex = 0
        Me.bClose.Text = "Close"
        '
        'txtMessage
        '
        Me.txtMessage.Dock = System.Windows.Forms.DockStyle.Top
        Me.txtMessage.Location = New System.Drawing.Point(0, 59)
        Me.txtMessage.Multiline = True
        Me.txtMessage.Name = "txtMessage"
        Me.txtMessage.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtMessage.Size = New System.Drawing.Size(528, 285)
        Me.txtMessage.TabIndex = 1
        Me.txtMessage.Text = "TextBox1"
        '
        'txtError
        '
        Me.txtError.Dock = System.Windows.Forms.DockStyle.Top
        Me.txtError.Location = New System.Drawing.Point(0, 0)
        Me.txtError.Multiline = True
        Me.txtError.Name = "txtError"
        Me.txtError.Size = New System.Drawing.Size(528, 56)
        Me.txtError.TabIndex = 2
        Me.txtError.Text = "TextBox1"
        '
        'Splitter1
        '
        Me.Splitter1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Splitter1.Location = New System.Drawing.Point(0, 56)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(528, 3)
        Me.Splitter1.TabIndex = 3
        Me.Splitter1.TabStop = False
        '
        'ErrorMes
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(528, 389)
        Me.Controls.Add(Me.txtMessage)
        Me.Controls.Add(Me.Splitter1)
        Me.Controls.Add(Me.txtError)
        Me.Controls.Add(Me.bClose)
        Me.Name = "ErrorMes"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "ErrorMes"
        Me.ResumeLayout(False)

    End Sub

#End Region
    Public Shared Sub ShowMessage(ByVal ex As Exception)
        Dim f As New ErrorMes
        f.txtMessage.Text = ex.StackTrace
        f.txtError.Text = ex.Message
        f.Text = "Error"

        f.Left = 0
        f.Width = Screen.GetWorkingArea(f).Width
        f.ShowDialog()
    End Sub
    Private Sub bClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bClose.Click
        Close()
    End Sub

    Private Sub ErrorMes_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        If WindowState = FormWindowState.Minimized Then Exit Sub
        bClose.Top = ClientSize.Height - bClose.Height - marg
        bClose.Left = ClientSize.Width - bClose.Width - marg

        txtMessage.Height = bClose.Top - marg
        txtMessage.Width = Width - marg
    End Sub
End Class
