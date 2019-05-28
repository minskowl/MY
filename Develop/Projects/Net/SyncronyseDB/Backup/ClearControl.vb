Public Class ClearControl
    Inherits System.Windows.Forms.UserControl
    Private ConStr As String
    Private Deleted_Seq As New ArrayList
    Private con As New SqlClient.SqlConnection
#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'UserControl overrides dispose to clean up the component list.
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
    Friend WithEvents lb_Tabs As System.Windows.Forms.CheckedListBox
    Friend WithEvents gb_Main As System.Windows.Forms.GroupBox
    Friend WithEvents b_Clear As System.Windows.Forms.Button
    Friend WithEvents b_SelectAll As System.Windows.Forms.Button
    Friend WithEvents tb_Res As System.Windows.Forms.TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.lb_Tabs = New System.Windows.Forms.CheckedListBox
        Me.gb_Main = New System.Windows.Forms.GroupBox
        Me.tb_Res = New System.Windows.Forms.TextBox
        Me.b_SelectAll = New System.Windows.Forms.Button
        Me.b_Clear = New System.Windows.Forms.Button
        Me.gb_Main.SuspendLayout()
        Me.SuspendLayout()
        '
        'lb_Tabs
        '
        Me.lb_Tabs.Dock = System.Windows.Forms.DockStyle.Left
        Me.lb_Tabs.Location = New System.Drawing.Point(3, 16)
        Me.lb_Tabs.Name = "lb_Tabs"
        Me.lb_Tabs.Size = New System.Drawing.Size(181, 229)
        Me.lb_Tabs.TabIndex = 1
        '
        'gb_Main
        '
        Me.gb_Main.Controls.Add(Me.tb_Res)
        Me.gb_Main.Controls.Add(Me.b_SelectAll)
        Me.gb_Main.Controls.Add(Me.b_Clear)
        Me.gb_Main.Controls.Add(Me.lb_Tabs)
        Me.gb_Main.Location = New System.Drawing.Point(0, 0)
        Me.gb_Main.Name = "gb_Main"
        Me.gb_Main.Size = New System.Drawing.Size(424, 248)
        Me.gb_Main.TabIndex = 2
        Me.gb_Main.TabStop = False
        Me.gb_Main.Text = "GroupBox1"
        '
        'tb_Res
        '
        Me.tb_Res.Location = New System.Drawing.Point(192, 48)
        Me.tb_Res.Multiline = True
        Me.tb_Res.Name = "tb_Res"
        Me.tb_Res.Size = New System.Drawing.Size(224, 192)
        Me.tb_Res.TabIndex = 4
        Me.tb_Res.Text = "tb_Res"
        '
        'b_SelectAll
        '
        Me.b_SelectAll.Location = New System.Drawing.Point(192, 16)
        Me.b_SelectAll.Name = "b_SelectAll"
        Me.b_SelectAll.TabIndex = 3
        Me.b_SelectAll.Text = "Select All"
        '
        'b_Clear
        '
        Me.b_Clear.Location = New System.Drawing.Point(272, 16)
        Me.b_Clear.Name = "b_Clear"
        Me.b_Clear.TabIndex = 2
        Me.b_Clear.Text = "Clear"
        '
        'ClearControl
        '
        Me.Controls.Add(Me.gb_Main)
        Me.Name = "ClearControl"
        Me.Size = New System.Drawing.Size(432, 248)
        Me.gb_Main.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Public Sub Init(ByVal ConStr As String)
        Me.ConStr = ConStr
        con.ConnectionString = ConStr
        gb_Main.Text = con.Database & " on " & con.DataSource
        Dim com As SqlClient.SqlCommand = con.CreateCommand
        con.Open()
        com.CommandText = "select id,name from sysobjects where xtype='U' order by 2 "
        Dim reader As SqlClient.SqlDataReader = com.ExecuteReader(CommandBehavior.CloseConnection)
        While (reader.Read())
            lb_Tabs.SetItemChecked(lb_Tabs.Items.Add(reader.GetString(1)), True)
        End While
        reader.Close()
        con.Close()

    End Sub


    Private Sub b_Clear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles b_Clear.Click
        Try
            Dim com As SqlClient.SqlCommand = con.CreateCommand
            con.Open()
            com.CommandText = "SELECT     sysobjects.name, sysobjects_1.name AS dname FROM " & _
                          "sysobjects INNER JOIN " & _
                          "sysforeignkeys ON sysobjects.id = sysforeignkeys.fkeyid INNER JOIN " & _
                          "sysobjects sysobjects_1 ON sysforeignkeys.rkeyid = sysobjects_1.id ORDER BY 2"
            Dim reader As SqlClient.SqlDataReader = com.ExecuteReader(CommandBehavior.CloseConnection)
            Dim tabs As New ArrayList
            Dim str As String, curTab As String, sql As String
            Dim lTabs As ArrayList

            For Each str In lb_Tabs.CheckedItems
                tabs.Add(str)
            Next
            curTab = ""
            Dim h As New Hashtable
            While (reader.Read())
                str = reader.GetString(1)
                If (curTab <> str) Then 'произошла смена таблицы 
                    If (curTab.Length > 0) Then
                        h.Add(tabs.IndexOf(curTab), lTabs) ' сохраняем
                    End If
                    lTabs = New ArrayList
                    curTab = str

                End If
                lTabs.Add(reader.GetString(0))

            End While
            If curTab.Length > 0 Then
                h.Add(tabs.IndexOf(curTab), lTabs) ' сохраняем
            End If
            reader.Close()



            'For Each str In cTabs
            '    'Ch_Tab_Del(str, 0, h, tabs)
            'Next
            con.Close()
            Dim i As Integer
            Deleted_Seq.Clear()

            Dim cTabs As ArrayList
            tb_Res.Text = ""
nu:
            cTabs = tabs.Clone()
            sql = ""
            For Each curTab In tabs
                i = tabs.IndexOf(curTab)

                If h.ContainsKey(i) = False Then 'no parents
                    Deleted_Seq.Add(curTab)
                    sql &= curTab & ", "
                    cTabs.Remove(curTab)
                Else
                    For Each str In h(i)
                        If Deleted_Seq.Contains(str) = False Then ' Fill parent
                            GoTo ne
                        End If
                    Next

                    Deleted_Seq.Add(curTab)
                    cTabs.Remove(curTab)
                    sql &= curTab & ", "
                End If
ne:



            Next
            'tb_Res.Text &= "delete from " & sql.Substring(0, sql.Length - 2) & vbCrLf
            tabs = cTabs
            If tabs.Count > 0 Then
                GoTo nu
            End If
            str = ""
            For Each curTab In Deleted_Seq
                str &= "delete " & curTab & vbCrLf & "GO" & vbCrLf
            Next
            tb_Res.Text = str
        Catch ex As Exception
            If con.State <> ConnectionState.Closed Then
                con.Close()
            End If
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Function Ch_Tab_Del(ByVal TabName As String, ByVal Level As Integer, ByRef h As Hashtable, ByRef lTabs As ArrayList) As Boolean
        Dim par As SQLDMO.QueryResults2
        Dim i As Integer
        Dim str As String
        Ch_Tab_Del = True
        Level += 1



        If Deleted_Seq.Contains(TabName) = True Then 'Alredy clear
            Exit Function
        End If

        i = lTabs.IndexOf(TabName)

        If h.ContainsKey(i) = False Then 'no parents
            Deleted_Seq.Add(TabName)
            Exit Function
        End If

        For Each str In h(i)
            If Deleted_Seq.Contains(str) = False Then ' Fill parent
                Ch_Tab_Del = False
                Exit Function
            End If
        Next

        Deleted_Seq.Add(TabName)
        lTabs.Remove(TabName)
    End Function

    Private Sub b_SelectAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles b_SelectAll.Click
        'Dim it



    End Sub
End Class
