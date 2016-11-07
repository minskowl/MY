
Imports System.CodeDom.Compiler
Imports System.IO

Public Class Form1
    Inherits System.Windows.Forms.Form

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
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents cmdClip As System.Windows.Forms.Button
    Friend WithEvents txtSource As System.Windows.Forms.TextBox
    Friend WithEvents txtDest As System.Windows.Forms.TextBox
    Friend WithEvents txtFile As System.Windows.Forms.TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.Button1 = New System.Windows.Forms.Button
        Me.cmdClip = New System.Windows.Forms.Button
        Me.txtSource = New System.Windows.Forms.TextBox
        Me.txtDest = New System.Windows.Forms.TextBox
        Me.txtFile = New System.Windows.Forms.TextBox
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(182, 299)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(88, 24)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "From file"
        '
        'cmdClip
        '
        Me.cmdClip.Location = New System.Drawing.Point(276, 299)
        Me.cmdClip.Name = "cmdClip"
        Me.cmdClip.Size = New System.Drawing.Size(75, 23)
        Me.cmdClip.TabIndex = 1
        Me.cmdClip.Text = "Clippboard"
        '
        'txtSource
        '
        Me.txtSource.Dock = System.Windows.Forms.DockStyle.Top
        Me.txtSource.Location = New System.Drawing.Point(0, 0)
        Me.txtSource.Multiline = True
        Me.txtSource.Name = "txtSource"
        Me.txtSource.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtSource.Size = New System.Drawing.Size(566, 138)
        Me.txtSource.TabIndex = 2
        '
        'txtDest
        '
        Me.txtDest.Dock = System.Windows.Forms.DockStyle.Top
        Me.txtDest.Location = New System.Drawing.Point(0, 138)
        Me.txtDest.Multiline = True
        Me.txtDest.Name = "txtDest"
        Me.txtDest.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtDest.Size = New System.Drawing.Size(566, 155)
        Me.txtDest.TabIndex = 3
        '
        'txtFile
        '
        Me.txtFile.Location = New System.Drawing.Point(0, 303)
        Me.txtFile.Name = "txtFile"
        Me.txtFile.Size = New System.Drawing.Size(176, 20)
        Me.txtFile.TabIndex = 4
        Me.txtFile.Text = "c:\test.vb"
        '
        'Form1
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(566, 335)
        Me.Controls.Add(Me.txtFile)
        Me.Controls.Add(Me.txtDest)
        Me.Controls.Add(Me.txtSource)
        Me.Controls.Add(Me.cmdClip)
        Me.Controls.Add(Me.Button1)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Dim cSharpProvider As CodeDomProvider
    Dim vbProvider As CodeDomProvider
    Dim codGenOpt As New CodeGeneratorOptions

    Dim vbCodeParser As CodeParser.VBCodeBuilderMSCodeDom



    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If Not System.IO.File.Exists(txtFile.Text) Then
            MsgBox("File not exists: " & txtFile.Text)
            Exit Sub
        End If

        Dim unit As CodeDom.CodeCompileUnit

        Try

            unit = vbCodeParser.Parse(File.ReadAllText(txtFile.Text))

            GenerateCode("c:\cls2.vb", vbProvider, unit)
            GenerateCode("c:\cls2.c", cSharpProvider, unit)


            txtDest.Text = File.ReadAllText("c:\cls2.c")


            MsgBox("OK")
        Catch ex As Exception
            ErrorMes.ShowMessage(ex)
        End Try

    End Sub

    Private Sub GenerateCode(ByVal fn As String, ByRef provider As CodeDomProvider, ByRef unit As CodeDom.CodeCompileUnit)
        Dim wr As System.IO.TextWriter = Nothing
        Try

            wr = System.IO.File.CreateText(fn)
            provider.GenerateCodeFromCompileUnit(unit, wr, codGenOpt)

        Catch ex As Exception
            ErrorMes.ShowMessage(ex)
        Finally
            If Not wr Is Nothing Then
                wr.Flush()
                wr.Close()
            End If

        End Try

    End Sub

    Private Sub cmdClip_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClip.Click
        Try
            Dim o As IDataObject = Clipboard.GetDataObject
            Dim str() As String = o.GetFormats()
            Dim text As String = String.Empty

            If o.GetDataPresent("UnicodeText") Then
                text = o.GetData("UnicodeText")
            ElseIf o.GetDataPresent("Text") Then
                text = o.GetData("UnicodeText")
            End If
            text = Trim(text)
            If text.Length = 0 Then Exit Sub


            GenerateCode("c:\tmp.c", cSharpProvider, vbCodeParser.Parse(text))


            txtSource.Text = text
            txtDest.Text = File.ReadAllText("c:\tmp.c")

        Catch ex As Exception
            ErrorMes.ShowMessage(ex)

        End Try

    End Sub

    Private Sub Form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        cSharpProvider = CodeDomProvider.CreateProvider("C#")
        vbProvider = CodeDomProvider.CreateProvider("VisualBasic")

        vbCodeParser = CodeParser.VBCodeBuilderMSCodeDom.Create(New CodeParser.vbCodeParser())


    End Sub
End Class


