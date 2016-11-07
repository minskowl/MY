Public Class frmSettings

    Private checkCorrect As Boolean = True
    Private _setts As Settings
    Public Property setts() As Settings
        Get
            Return _setts
        End Get
        Set(ByVal value As Settings)
            _setts = value
        End Set
    End Property

    Private Sub frmSettings_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Left = 100
        Me.Top = CInt((Screen.PrimaryScreen.WorkingArea.Height - Me.Height) / 2)

        Me.Width = Screen.PrimaryScreen.WorkingArea.Width - 200



        txtPathIcons.Text = setts.PathIcons


    End Sub

    Private Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click
        setts.PathIcons = txtPathIcons.Text


        DialogResult = Windows.Forms.DialogResult.OK

        Close()
    End Sub

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        DialogResult = Windows.Forms.DialogResult.Cancel
        Close()
    End Sub


    Private Sub cmdPathIconsBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPathIconsBrowse.Click
        EditPath(txtPathIcons)
    End Sub
    Private Sub Path_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPathIcons.LostFocus
        CheckPath(CType(sender, TextBox))
    End Sub





    Private Sub EditFile(ByRef EditBox As System.Windows.Forms.TextBox)
        ofd.FileName = EditBox.Text
        If ofd.ShowDialog <> Windows.Forms.DialogResult.OK Then Exit Sub
        EditBox.Text = ofd.FileName
    End Sub

    Private Sub EditPath(ByRef EditBox As System.Windows.Forms.TextBox)
        fbd.SelectedPath = EditBox.Text
        If fbd.ShowDialog <> Windows.Forms.DialogResult.OK Then Exit Sub
        EditBox.Text = fbd.SelectedPath
    End Sub
    Private Sub CheckPath(ByRef EditBox As System.Windows.Forms.TextBox)
        If checkCorrect = False Then Exit Sub

        If EditBox.Text.Trim.Length > 0 Then
            If Not System.IO.Directory.Exists(EditBox.Text) Then
                checkCorrect = False
                MsgBox("Directory not exists: " & EditBox.Text, MsgBoxStyle.OkOnly Or MsgBoxStyle.Exclamation)
                EditBox.Select()
                Exit Sub
            End If
        End If
    End Sub
    Private Sub CheckFile(ByRef EditBox As System.Windows.Forms.TextBox)
        If checkCorrect = False Then Exit Sub
        If EditBox.Text.Trim.Length > 0 Then
            If Not System.IO.File.Exists(EditBox.Text) Then
                checkCorrect = False
                MsgBox("File not exists: " & EditBox.Text, MsgBoxStyle.OkOnly Or MsgBoxStyle.Exclamation)
                EditBox.Select()
                Exit Sub
            End If
        End If
    End Sub







End Class