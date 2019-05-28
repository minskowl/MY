Public Class frm_IgnoreList
    Inherits System.Windows.Forms.Form
    Public con As Data.SqlClient.SqlConnection
    Private opt As Hashtable
    Public Property Options() As Hashtable
        Get

            Return opt
        End Get
        Set(ByVal Value As Hashtable)
            ' The Set property procedure is called when the value 
            ' of a property is modified. 
            ' The value to be assigned is passed in the argument to Set. 
            opt = Value
            Dim o
            For Each o In opt

            Next

        End Set
    End Property

    Private ar_Ign_Usr As ArrayList
    Public Property Ign_User() As ArrayList
        Get

            Return ar_Ign_Usr
        End Get
        Set(ByVal Value As ArrayList)
            ' The Set property procedure is called when the value 
            ' of a property is modified. 
            ' The value to be assigned is passed in the argument to Set. 
            ar_Ign_Usr = Value
            If ar_Ign_Usr Is Nothing Then
                Exit Property
            End If
            Dim usr As String

            For Each usr In ar_Ign_Usr
                lb_Ign_Users.Items.Add(usr)
            Next

        End Set
    End Property

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
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents cb_Users As System.Windows.Forms.ComboBox
    Friend WithEvents lb_Ign_Users As System.Windows.Forms.ListBox
    Friend WithEvents ds As System.Data.DataSet
    Friend WithEvents users As System.Data.DataTable
    Friend WithEvents DataColumn1 As System.Data.DataColumn
    Friend WithEvents b_OK As System.Windows.Forms.Button
    Friend WithEvents b_Ign_User_Add As System.Windows.Forms.Button
    Friend WithEvents cb_FillFactor As System.Windows.Forms.CheckBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.b_Ign_User_Add = New System.Windows.Forms.Button
        Me.lb_Ign_Users = New System.Windows.Forms.ListBox
        Me.cb_Users = New System.Windows.Forms.ComboBox
        Me.users = New System.Data.DataTable
        Me.DataColumn1 = New System.Data.DataColumn
        Me.ds = New System.Data.DataSet
        Me.b_OK = New System.Windows.Forms.Button
        Me.cb_FillFactor = New System.Windows.Forms.CheckBox
        Me.GroupBox1.SuspendLayout()
        CType(Me.users, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ds, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.b_Ign_User_Add)
        Me.GroupBox1.Controls.Add(Me.lb_Ign_Users)
        Me.GroupBox1.Controls.Add(Me.cb_Users)
        Me.GroupBox1.Location = New System.Drawing.Point(8, 8)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(232, 152)
        Me.GroupBox1.TabIndex = 1
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
        'users
        '
        Me.users.Columns.AddRange(New System.Data.DataColumn() {Me.DataColumn1})
        Me.users.TableName = "users"
        '
        'DataColumn1
        '
        Me.DataColumn1.ColumnName = "name"
        '
        'ds
        '
        Me.ds.DataSetName = "NewDataSet"
        Me.ds.Locale = New System.Globalization.CultureInfo("ru-RU")
        Me.ds.Tables.AddRange(New System.Data.DataTable() {Me.users})
        '
        'b_OK
        '
        Me.b_OK.Location = New System.Drawing.Point(8, 240)
        Me.b_OK.Name = "b_OK"
        Me.b_OK.TabIndex = 2
        Me.b_OK.Text = "OK"
        '
        'cb_FillFactor
        '
        Me.cb_FillFactor.Location = New System.Drawing.Point(264, 16)
        Me.cb_FillFactor.Name = "cb_FillFactor"
        Me.cb_FillFactor.TabIndex = 3
        Me.cb_FillFactor.Text = "FillFactor"
        '
        'frm_IgnoreList
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(504, 273)
        Me.Controls.Add(Me.cb_FillFactor)
        Me.Controls.Add(Me.b_OK)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "frm_IgnoreList"
        Me.Text = "frm_IgnoreList"
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.users, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ds, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub b_OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles b_OK.Click
        ar_Ign_Usr.Clear()
        Dim usr As String
        For Each usr In lb_Ign_Users.Items
            ar_Ign_Usr.Add(usr)
        Next

        opt.Clear()

        If cb_FillFactor.Checked Then
            opt.Add("FillFactor", True)
        Else
            opt.Add("FillFactor", False)
        End If
        DialogResult = DialogResult.OK
        Close()

    End Sub

    Private Sub frm_IgnoreList_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
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

    Private Sub b_Ign_User_Add_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles b_Ign_User_Add.Click
        If lb_Ign_Users.Items.Contains(cb_Users.Text) = False Then
            lb_Ign_Users.Items.Add(cb_Users.Text)
        End If

    End Sub


    Private Sub lb_Ign_Users_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lb_Ign_Users.DoubleClick
        lb_Ign_Users.Items.RemoveAt(lb_Ign_Users.SelectedIndex)

    End Sub
End Class
