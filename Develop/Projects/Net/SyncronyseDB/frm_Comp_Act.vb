Public Class frm_Comp_Act
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
    Friend WithEvents b_OK As System.Windows.Forms.Button
    Friend WithEvents b_Cancel As System.Windows.Forms.Button
    Friend WithEvents tbc_Act As System.Windows.Forms.TabControl
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents tb_Connections As System.Windows.Forms.TabPage
    Friend WithEvents gb_Destination As System.Windows.Forms.GroupBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents tb_Destination_Database As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents tb_Destination_Password As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents tb_Destination_User As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents gb_Source As System.Windows.Forms.GroupBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents tb_Source_Database As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents tb_Source_Password As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents tb_Source_User As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents tb_Source_Server As System.Windows.Forms.TextBox
    Friend WithEvents tb_Destination_Server As System.Windows.Forms.TextBox
    Friend WithEvents cb_AutoStart As System.Windows.Forms.CheckBox
    Friend WithEvents cb_AutoClose As System.Windows.Forms.CheckBox
    Friend WithEvents cb_AllObjects As System.Windows.Forms.CheckBox
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents cb_FillFactor As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents b_Ign_User_Add As System.Windows.Forms.Button
    Friend WithEvents lb_Ign_Users As System.Windows.Forms.ListBox
    Friend WithEvents cb_Users As System.Windows.Forms.ComboBox
    Friend WithEvents ds As System.Data.DataSet
    Friend WithEvents users As System.Data.DataTable
    Friend WithEvents DataColumn1 As System.Data.DataColumn
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.tbc_Act = New System.Windows.Forms.TabControl
        Me.tb_Connections = New System.Windows.Forms.TabPage
        Me.gb_Destination = New System.Windows.Forms.GroupBox
        Me.tb_Destination_Server = New System.Windows.Forms.TextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.tb_Destination_Database = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.tb_Destination_Password = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.tb_Destination_User = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.gb_Source = New System.Windows.Forms.GroupBox
        Me.tb_Source_Server = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.tb_Source_Database = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.tb_Source_Password = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.tb_Source_User = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.TabPage2 = New System.Windows.Forms.TabPage
        Me.cb_AllObjects = New System.Windows.Forms.CheckBox
        Me.cb_AutoClose = New System.Windows.Forms.CheckBox
        Me.cb_AutoStart = New System.Windows.Forms.CheckBox
        Me.TabPage1 = New System.Windows.Forms.TabPage
        Me.cb_FillFactor = New System.Windows.Forms.CheckBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.b_Ign_User_Add = New System.Windows.Forms.Button
        Me.lb_Ign_Users = New System.Windows.Forms.ListBox
        Me.cb_Users = New System.Windows.Forms.ComboBox
        Me.b_OK = New System.Windows.Forms.Button
        Me.b_Cancel = New System.Windows.Forms.Button
        Me.ds = New System.Data.DataSet
        Me.users = New System.Data.DataTable
        Me.DataColumn1 = New System.Data.DataColumn
        Me.tbc_Act.SuspendLayout()
        Me.tb_Connections.SuspendLayout()
        Me.gb_Destination.SuspendLayout()
        Me.gb_Source.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.ds, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.users, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'tbc_Act
        '
        Me.tbc_Act.Controls.Add(Me.tb_Connections)
        Me.tbc_Act.Controls.Add(Me.TabPage2)
        Me.tbc_Act.Controls.Add(Me.TabPage1)
        Me.tbc_Act.Dock = System.Windows.Forms.DockStyle.Top
        Me.tbc_Act.Location = New System.Drawing.Point(0, 0)
        Me.tbc_Act.Name = "tbc_Act"
        Me.tbc_Act.SelectedIndex = 0
        Me.tbc_Act.Size = New System.Drawing.Size(512, 272)
        Me.tbc_Act.TabIndex = 0
        '
        'tb_Connections
        '
        Me.tb_Connections.Controls.Add(Me.gb_Destination)
        Me.tb_Connections.Controls.Add(Me.gb_Source)
        Me.tb_Connections.Location = New System.Drawing.Point(4, 22)
        Me.tb_Connections.Name = "tb_Connections"
        Me.tb_Connections.Size = New System.Drawing.Size(504, 246)
        Me.tb_Connections.TabIndex = 0
        Me.tb_Connections.Text = "Connections"
        '
        'gb_Destination
        '
        Me.gb_Destination.Controls.Add(Me.tb_Destination_Server)
        Me.gb_Destination.Controls.Add(Me.Label8)
        Me.gb_Destination.Controls.Add(Me.tb_Destination_Database)
        Me.gb_Destination.Controls.Add(Me.Label6)
        Me.gb_Destination.Controls.Add(Me.tb_Destination_Password)
        Me.gb_Destination.Controls.Add(Me.Label4)
        Me.gb_Destination.Controls.Add(Me.tb_Destination_User)
        Me.gb_Destination.Controls.Add(Me.Label2)
        Me.gb_Destination.Location = New System.Drawing.Point(248, 8)
        Me.gb_Destination.Name = "gb_Destination"
        Me.gb_Destination.Size = New System.Drawing.Size(208, 128)
        Me.gb_Destination.TabIndex = 4
        Me.gb_Destination.TabStop = False
        Me.gb_Destination.Text = "Destination"
        '
        'tb_Destination_Server
        '
        Me.tb_Destination_Server.Location = New System.Drawing.Point(96, 24)
        Me.tb_Destination_Server.Name = "tb_Destination_Server"
        Me.tb_Destination_Server.TabIndex = 10
        Me.tb_Destination_Server.Text = ""
        '
        'Label8
        '
        Me.Label8.Location = New System.Drawing.Point(16, 96)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(58, 23)
        Me.Label8.TabIndex = 9
        Me.Label8.Text = "DataBase"
        '
        'tb_Destination_Database
        '
        Me.tb_Destination_Database.Location = New System.Drawing.Point(96, 96)
        Me.tb_Destination_Database.Name = "tb_Destination_Database"
        Me.tb_Destination_Database.TabIndex = 8
        Me.tb_Destination_Database.Text = ""
        '
        'Label6
        '
        Me.Label6.Location = New System.Drawing.Point(16, 72)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(58, 23)
        Me.Label6.TabIndex = 7
        Me.Label6.Text = "Password"
        '
        'tb_Destination_Password
        '
        Me.tb_Destination_Password.Location = New System.Drawing.Point(96, 72)
        Me.tb_Destination_Password.Name = "tb_Destination_Password"
        Me.tb_Destination_Password.PasswordChar = Microsoft.VisualBasic.ChrW(42)
        Me.tb_Destination_Password.TabIndex = 6
        Me.tb_Destination_Password.Text = ""
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(16, 48)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(40, 23)
        Me.Label4.TabIndex = 5
        Me.Label4.Text = "User"
        '
        'tb_Destination_User
        '
        Me.tb_Destination_User.Location = New System.Drawing.Point(96, 48)
        Me.tb_Destination_User.Name = "tb_Destination_User"
        Me.tb_Destination_User.TabIndex = 4
        Me.tb_Destination_User.Text = ""
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(16, 24)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(40, 23)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Server"
        '
        'gb_Source
        '
        Me.gb_Source.Controls.Add(Me.tb_Source_Server)
        Me.gb_Source.Controls.Add(Me.Label7)
        Me.gb_Source.Controls.Add(Me.tb_Source_Database)
        Me.gb_Source.Controls.Add(Me.Label5)
        Me.gb_Source.Controls.Add(Me.tb_Source_Password)
        Me.gb_Source.Controls.Add(Me.Label3)
        Me.gb_Source.Controls.Add(Me.tb_Source_User)
        Me.gb_Source.Controls.Add(Me.Label1)
        Me.gb_Source.Location = New System.Drawing.Point(8, 8)
        Me.gb_Source.Name = "gb_Source"
        Me.gb_Source.Size = New System.Drawing.Size(224, 128)
        Me.gb_Source.TabIndex = 3
        Me.gb_Source.TabStop = False
        Me.gb_Source.Text = "Source"
        '
        'tb_Source_Server
        '
        Me.tb_Source_Server.Location = New System.Drawing.Point(80, 24)
        Me.tb_Source_Server.Name = "tb_Source_Server"
        Me.tb_Source_Server.TabIndex = 8
        Me.tb_Source_Server.Text = ""
        '
        'Label7
        '
        Me.Label7.Location = New System.Drawing.Point(16, 96)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(58, 23)
        Me.Label7.TabIndex = 7
        Me.Label7.Text = "DataBase"
        '
        'tb_Source_Database
        '
        Me.tb_Source_Database.Location = New System.Drawing.Point(80, 96)
        Me.tb_Source_Database.Name = "tb_Source_Database"
        Me.tb_Source_Database.TabIndex = 6
        Me.tb_Source_Database.Text = ""
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(16, 72)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(58, 23)
        Me.Label5.TabIndex = 5
        Me.Label5.Text = "Password"
        '
        'tb_Source_Password
        '
        Me.tb_Source_Password.Location = New System.Drawing.Point(80, 72)
        Me.tb_Source_Password.Name = "tb_Source_Password"
        Me.tb_Source_Password.PasswordChar = Microsoft.VisualBasic.ChrW(42)
        Me.tb_Source_Password.TabIndex = 4
        Me.tb_Source_Password.Text = ""
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(16, 48)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(40, 23)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "User"
        '
        'tb_Source_User
        '
        Me.tb_Source_User.Location = New System.Drawing.Point(80, 48)
        Me.tb_Source_User.Name = "tb_Source_User"
        Me.tb_Source_User.TabIndex = 2
        Me.tb_Source_User.Text = ""
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(16, 24)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(40, 23)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Server"
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.cb_AllObjects)
        Me.TabPage2.Controls.Add(Me.cb_AutoClose)
        Me.TabPage2.Controls.Add(Me.cb_AutoStart)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Size = New System.Drawing.Size(504, 246)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Options"
        '
        'cb_AllObjects
        '
        Me.cb_AllObjects.Location = New System.Drawing.Point(8, 56)
        Me.cb_AllObjects.Name = "cb_AllObjects"
        Me.cb_AllObjects.TabIndex = 2
        Me.cb_AllObjects.Text = "All Objects"
        '
        'cb_AutoClose
        '
        Me.cb_AutoClose.Location = New System.Drawing.Point(8, 32)
        Me.cb_AutoClose.Name = "cb_AutoClose"
        Me.cb_AutoClose.TabIndex = 1
        Me.cb_AutoClose.Text = "Auto Close"
        '
        'cb_AutoStart
        '
        Me.cb_AutoStart.Location = New System.Drawing.Point(8, 8)
        Me.cb_AutoStart.Name = "cb_AutoStart"
        Me.cb_AutoStart.TabIndex = 0
        Me.cb_AutoStart.Text = "Auto Start"
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.cb_FillFactor)
        Me.TabPage1.Controls.Add(Me.GroupBox1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Size = New System.Drawing.Size(504, 246)
        Me.TabPage1.TabIndex = 2
        Me.TabPage1.Text = "Ignore List"
        '
        'cb_FillFactor
        '
        Me.cb_FillFactor.Location = New System.Drawing.Point(264, 16)
        Me.cb_FillFactor.Name = "cb_FillFactor"
        Me.cb_FillFactor.TabIndex = 5
        Me.cb_FillFactor.Text = "FillFactor"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.b_Ign_User_Add)
        Me.GroupBox1.Controls.Add(Me.lb_Ign_Users)
        Me.GroupBox1.Controls.Add(Me.cb_Users)
        Me.GroupBox1.Location = New System.Drawing.Point(8, 8)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(232, 152)
        Me.GroupBox1.TabIndex = 4
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "User rights"
        '
        'b_Ign_User_Add
        '
        Me.b_Ign_User_Add.Location = New System.Drawing.Point(136, 24)
        Me.b_Ign_User_Add.Name = "b_Ign_User_Add"
        Me.b_Ign_User_Add.TabIndex = 2
        Me.b_Ign_User_Add.Text = "Add"
        '
        'lb_Ign_Users
        '
        Me.lb_Ign_Users.Location = New System.Drawing.Point(16, 56)
        Me.lb_Ign_Users.Name = "lb_Ign_Users"
        Me.lb_Ign_Users.Size = New System.Drawing.Size(104, 56)
        Me.lb_Ign_Users.TabIndex = 1
        '
        'cb_Users
        '
        Me.cb_Users.DataSource = Me.users
        Me.cb_Users.DisplayMember = "name"
        Me.cb_Users.Location = New System.Drawing.Point(8, 24)
        Me.cb_Users.Name = "cb_Users"
        Me.cb_Users.Size = New System.Drawing.Size(121, 21)
        Me.cb_Users.TabIndex = 0
        Me.cb_Users.ValueMember = "name"
        '
        'b_OK
        '
        Me.b_OK.Location = New System.Drawing.Point(8, 280)
        Me.b_OK.Name = "b_OK"
        Me.b_OK.TabIndex = 1
        Me.b_OK.Text = "OK"
        '
        'b_Cancel
        '
        Me.b_Cancel.Location = New System.Drawing.Point(96, 280)
        Me.b_Cancel.Name = "b_Cancel"
        Me.b_Cancel.TabIndex = 2
        Me.b_Cancel.Text = "Cancel"
        '
        'ds
        '
        Me.ds.DataSetName = "NewDataSet"
        Me.ds.Locale = New System.Globalization.CultureInfo("ru-RU")
        Me.ds.Tables.AddRange(New System.Data.DataTable() {Me.users})
        '
        'users
        '
        Me.users.Columns.AddRange(New System.Data.DataColumn() {Me.DataColumn1})
        Me.users.TableName = "users"
        '
        'DataColumn1
        '
        Me.DataColumn1.ColumnName = "name"
        '
        'frm_Comp_Act
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(512, 309)
        Me.Controls.Add(Me.b_Cancel)
        Me.Controls.Add(Me.b_OK)
        Me.Controls.Add(Me.tbc_Act)
        Me.Name = "frm_Comp_Act"
        Me.Text = "Compare Action"
        Me.tbc_Act.ResumeLayout(False)
        Me.tb_Connections.ResumeLayout(False)
        Me.gb_Destination.ResumeLayout(False)
        Me.gb_Source.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.ds, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.users, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region
    Private act As CompareAction
    Public con As SqlClient.SqlConnection

    Public Property action() As CompareAction
        Get
            Return act
        End Get
        Set(ByVal Value As CompareAction)
            act = Value
            If Value Is Nothing Then Return
            tb_Destination_Database.Text = act.DstCon.DataBase
            tb_Destination_Password.Text = act.DstCon.Password
            tb_Destination_Server.Text = act.DstCon.Server
            tb_Destination_User.Text = act.DstCon.User

            tb_Source_Database.Text = act.SrcCon.DataBase
            tb_Source_Password.Text = act.SrcCon.Password
            tb_Source_Server.Text = act.SrcCon.Server
            tb_Source_User.Text = act.SrcCon.User

            If Not act.Options Is Nothing Then
                Dim o As DictionaryEntry
                For Each o In act.Options
                    Select Case o.Key
                        Case "FillFactor"
                            cb_FillFactor.Checked = o.Value
                    End Select
                Next
            Else
                act.Options = New Hashtable
            End If
        End Set
    End Property
    Private Sub b_OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles b_OK.Click
        act.DstCon.DataBase = tb_Destination_Database.Text
        act.DstCon.Password=tb_Destination_Password.Text 
        act.DstCon.Server=tb_Destination_Server.Text 
        act.DstCon.User=tb_Destination_User.Text 

        act.SrcCon.DataBase=tb_Source_Database.Text 
        act.SrcCon.Password=tb_Source_Password.Text 
        act.SrcCon.Server = tb_Source_Server.Text
        act.SrcCon.User=tb_Source_User.Text 

        act.IgnUsr.Clear()
        Dim usr As String
        For Each usr In lb_Ign_Users.Items
            act.IgnUsr.Add(usr)
        Next


        If cb_FillFactor.Checked Then
            act.Options.Add("FillFactor", True)
        Else
            act.Options.Add("FillFactor", False)
        End If
        DialogResult = DialogResult.OK

        DialogResult = DialogResult.OK
        Close()

    End Sub

    Private Sub b_Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles b_Cancel.Click
        DialogResult = DialogResult.Cancel
        Close()
    End Sub

    Private Sub b_Ign_User_Add_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles b_Ign_User_Add.Click
        If lb_Ign_Users.Items.Contains(cb_Users.Text) = False Then
            lb_Ign_Users.Items.Add(cb_Users.Text)
        End If

    End Sub

    Private Sub frm_Comp_Act_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            If con Is Nothing Then
                Exit Sub
            End If
            If con.State = ConnectionState.Closed Then
                con.Open()
            End If
            Dim ad As New Data.SqlClient.SqlDataAdapter("select name from sysusers", con)
            ad.Fill(users)
        Catch ex As Exception
            MsgBox("error load Ignore list form:" & ex.Message)
        End Try
    End Sub
End Class
