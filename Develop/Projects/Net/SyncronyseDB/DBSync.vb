Imports System.Text.RegularExpressions
Imports System.Collections
Imports System.Xml.Serialization
Imports System.IO


Public Class DBSync
    Inherits System.Windows.Forms.Form
#Region "Images ID"
    Const imit_OK As Integer = 5
    Const imit_Er As Integer = 6
    Const imit_Empty As Integer = 0

    Const imit_SP As Integer = 1
    Const imit_UDF As Integer = 3



    Const imit_Folder As Integer = 18
    Const imit_Folder_diff As Integer = 11

    Const imit_Table As Integer = 2
    Const imit_Table_not_exist As Integer = 10
    Const imit_Table_diff As Integer = 12

    Const imit_Column As Integer = 7
    Const imit_Column_not_exist As Integer = 13
    Const imit_Column_diff As Integer = 14

    Const imit_PrKey As Integer = 15
    Const imit_PrKey_not_exist As Integer = 16
    Const imit_PrKey_diff As Integer = 17

    Const imit_Key As Integer = 19
    Const imit_Key_not_exist As Integer = 20
    Const imit_Key_diff As Integer = 21

    Const imit_Index As Integer = 8
    Const imit_Index_not_exist As Integer = 22
    Const imit_Index_diff As Integer = 23

    Const imit_Check As Integer = 24
    Const imit_Check_not_exist As Integer = 25
    Const imit_Check_diff As Integer = 26

    Const imit_Trigger As Integer = 9
    Const imit_Trigger_not_exist As Integer = 33
    Const imit_Trigger_diff As Integer = 32

    Const imit_Role As Integer = 27
    Const imit_Role_not_exist As Integer = 28
    Const imit_Role_diff As Integer = 29

    Const imit_View As Integer = 4
    Const imit_View_not_exist As Integer = 31
    Const imit_View_diff As Integer = 30
#End Region
    Const ind_Table As Integer = 0
    Const ind_View As Integer = 1
    Const ind_SP As Integer = 2
    Const ind_UDF As Integer = 3

    Const text_Tables As String = "Tables"

    Private Const WM_MOUSEWHEEL As Integer = &H20A

    Const tabpg_id_Connections As Integer = 0




    Private str_Res(,) As String = New String(10, 1000) {}
    Private Compare_Res As New Hashtable
    Private oSQLServerDMOApp As SQLDMO.Application
    Private WithEvents oSQLServer_Source As SQLDMO.SQLServer = New SQLDMO.SQLServer
    Private WithEvents oSQLServer_Dest As SQLDMO.SQLServer = New SQLDMO.SQLServer
    Private oDB_Source As SQLDMO.Database2
    Private oDB_Destination As SQLDMO.Database2


    Dim WithEvents Transfer As New SQLDMO.Transfer2
    Dim WithEvents dataTransfer As New dataTransfer
    ' Dim metaComp As New MetaComp


    'Private Source_in_collapsing As Boolean
    'Private Destination_in_collapsing As Boolean
    'Private MouseWeel_from_source As Boolean
    'Private MouseWeel_from_destination As Boolean




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
    Friend WithEvents con_Source As System.Data.SqlClient.SqlConnection
    Friend WithEvents con_Dest As System.Data.SqlClient.SqlConnection
    Friend WithEvents comm_Source As System.Data.SqlClient.SqlCommand
    Friend WithEvents comm_Dest As System.Data.SqlClient.SqlCommand
    Friend WithEvents imgl_Objects As System.Windows.Forms.ImageList
    Friend WithEvents tabpg_Connections As System.Windows.Forms.TabPage
    Friend WithEvents gb_Source As System.Windows.Forms.GroupBox
    Friend WithEvents chb_Source_Auth As System.Windows.Forms.CheckBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents tb_Source_Password As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents tb_Source_User As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cb_Source_Server As System.Windows.Forms.ComboBox
    Friend WithEvents gb_Destination As System.Windows.Forms.GroupBox
    Friend WithEvents chb_Destination_Auth As System.Windows.Forms.CheckBox
    Friend WithEvents cb_Destination_Server As System.Windows.Forms.ComboBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents tb_Destination_Password As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents tb_Destination_User As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents tabpg_Trasfer As System.Windows.Forms.TabPage
    Friend WithEvents b_Init As System.Windows.Forms.Button
    Friend WithEvents trv_Tables As System.Windows.Forms.TreeView
    Friend WithEvents b_Check_All As System.Windows.Forms.Button
    Friend WithEvents b_Analize As System.Windows.Forms.Button
    Friend WithEvents l_TabCount As System.Windows.Forms.Label
    Friend WithEvents pb_Transfer As System.Windows.Forms.ProgressBar
    Friend WithEvents b_Transfer As System.Windows.Forms.Button
    Friend WithEvents l_TabTransfer As System.Windows.Forms.Label
    Friend WithEvents tb_Transfer_Log As System.Windows.Forms.TextBox
    Friend WithEvents b_Save_Transfer As System.Windows.Forms.Button
    Friend WithEvents OFD As System.Windows.Forms.OpenFileDialog
    Friend WithEvents b_Load_Transfer As System.Windows.Forms.Button
    Friend WithEvents b_UnCheck_All As System.Windows.Forms.Button
    Friend WithEvents MainMenu1 As System.Windows.Forms.MainMenu
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    Friend WithEvents chb_Clear_Table As System.Windows.Forms.CheckBox
    Friend WithEvents b_Connect As System.Windows.Forms.Button
    Friend WithEvents cntr_Compare As SyncronyseDB.CompareControl
    Friend WithEvents tabpg_Compare As System.Windows.Forms.TabPage
    Friend WithEvents mi_NextDiff As System.Windows.Forms.MenuItem
    Friend WithEvents mi_Execute As System.Windows.Forms.MenuItem
    Friend WithEvents tabpg_ComapeData As System.Windows.Forms.TabPage
    Friend WithEvents cntr_CompareData As SyncronyseDB.CompareDataControl
    Friend WithEvents DataSet1 As System.Data.DataSet
    Friend WithEvents DataColumn1 As System.Data.DataColumn
    Friend WithEvents users As System.Data.DataTable
    Friend WithEvents tabs_Tabs As System.Windows.Forms.TabControl
    Friend WithEvents tabpg_ClearData As System.Windows.Forms.TabPage
    Friend WithEvents cntr_ClearControl As SyncronyseDB.ClearControl
    Friend WithEvents cb_Destination_Database As System.Windows.Forms.ComboBox
    Friend WithEvents b_Destination_Database As System.Windows.Forms.Button
    Friend WithEvents cb_Source_Database As System.Windows.Forms.ComboBox
    Friend WithEvents b_Source_Database As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(DBSync))
        Me.con_Source = New System.Data.SqlClient.SqlConnection
        Me.con_Dest = New System.Data.SqlClient.SqlConnection
        Me.comm_Source = New System.Data.SqlClient.SqlCommand
        Me.comm_Dest = New System.Data.SqlClient.SqlCommand
        Me.imgl_Objects = New System.Windows.Forms.ImageList(Me.components)
        Me.tabs_Tabs = New System.Windows.Forms.TabControl
        Me.tabpg_Connections = New System.Windows.Forms.TabPage
        Me.b_Connect = New System.Windows.Forms.Button
        Me.gb_Destination = New System.Windows.Forms.GroupBox
        Me.b_Destination_Database = New System.Windows.Forms.Button
        Me.cb_Destination_Database = New System.Windows.Forms.ComboBox
        Me.chb_Destination_Auth = New System.Windows.Forms.CheckBox
        Me.cb_Destination_Server = New System.Windows.Forms.ComboBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.tb_Destination_Password = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.tb_Destination_User = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.gb_Source = New System.Windows.Forms.GroupBox
        Me.b_Source_Database = New System.Windows.Forms.Button
        Me.chb_Source_Auth = New System.Windows.Forms.CheckBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.tb_Source_Password = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.tb_Source_User = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.cb_Source_Server = New System.Windows.Forms.ComboBox
        Me.cb_Source_Database = New System.Windows.Forms.ComboBox
        Me.tabpg_Trasfer = New System.Windows.Forms.TabPage
        Me.chb_Clear_Table = New System.Windows.Forms.CheckBox
        Me.b_UnCheck_All = New System.Windows.Forms.Button
        Me.b_Load_Transfer = New System.Windows.Forms.Button
        Me.b_Save_Transfer = New System.Windows.Forms.Button
        Me.tb_Transfer_Log = New System.Windows.Forms.TextBox
        Me.l_TabTransfer = New System.Windows.Forms.Label
        Me.b_Transfer = New System.Windows.Forms.Button
        Me.pb_Transfer = New System.Windows.Forms.ProgressBar
        Me.l_TabCount = New System.Windows.Forms.Label
        Me.b_Analize = New System.Windows.Forms.Button
        Me.b_Check_All = New System.Windows.Forms.Button
        Me.b_Init = New System.Windows.Forms.Button
        Me.trv_Tables = New System.Windows.Forms.TreeView
        Me.tabpg_Compare = New System.Windows.Forms.TabPage
        Me.cntr_Compare = New SyncronyseDB.CompareControl
        Me.tabpg_ComapeData = New System.Windows.Forms.TabPage
        Me.cntr_CompareData = New SyncronyseDB.CompareDataControl
        Me.tabpg_ClearData = New System.Windows.Forms.TabPage
        Me.cntr_ClearControl = New SyncronyseDB.ClearControl
        Me.DataSet1 = New System.Data.DataSet
        Me.users = New System.Data.DataTable
        Me.DataColumn1 = New System.Data.DataColumn
        Me.OFD = New System.Windows.Forms.OpenFileDialog
        Me.MainMenu1 = New System.Windows.Forms.MainMenu
        Me.MenuItem1 = New System.Windows.Forms.MenuItem
        Me.mi_NextDiff = New System.Windows.Forms.MenuItem
        Me.mi_Execute = New System.Windows.Forms.MenuItem
        Me.tabs_Tabs.SuspendLayout()
        Me.tabpg_Connections.SuspendLayout()
        Me.gb_Destination.SuspendLayout()
        Me.gb_Source.SuspendLayout()
        Me.tabpg_Trasfer.SuspendLayout()
        Me.tabpg_Compare.SuspendLayout()
        Me.tabpg_ComapeData.SuspendLayout()
        Me.tabpg_ClearData.SuspendLayout()
        CType(Me.DataSet1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.users, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'comm_Source
        '
        Me.comm_Source.Connection = Me.con_Source
        '
        'comm_Dest
        '
        Me.comm_Dest.Connection = Me.con_Dest
        '
        'imgl_Objects
        '
        Me.imgl_Objects.ImageSize = New System.Drawing.Size(16, 16)
        Me.imgl_Objects.ImageStream = CType(resources.GetObject("imgl_Objects.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imgl_Objects.TransparentColor = System.Drawing.Color.Transparent
        '
        'tabs_Tabs
        '
        Me.tabs_Tabs.Controls.Add(Me.tabpg_Connections)
        Me.tabs_Tabs.Controls.Add(Me.tabpg_Trasfer)
        Me.tabs_Tabs.Controls.Add(Me.tabpg_Compare)
        Me.tabs_Tabs.Controls.Add(Me.tabpg_ComapeData)
        Me.tabs_Tabs.Controls.Add(Me.tabpg_ClearData)
        Me.tabs_Tabs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tabs_Tabs.ImageList = Me.imgl_Objects
        Me.tabs_Tabs.Location = New System.Drawing.Point(0, 0)
        Me.tabs_Tabs.Name = "tabs_Tabs"
        Me.tabs_Tabs.SelectedIndex = 0
        Me.tabs_Tabs.Size = New System.Drawing.Size(832, 405)
        Me.tabs_Tabs.TabIndex = 6
        '
        'tabpg_Connections
        '
        Me.tabpg_Connections.Controls.Add(Me.b_Connect)
        Me.tabpg_Connections.Controls.Add(Me.gb_Destination)
        Me.tabpg_Connections.Controls.Add(Me.gb_Source)
        Me.tabpg_Connections.Location = New System.Drawing.Point(4, 23)
        Me.tabpg_Connections.Name = "tabpg_Connections"
        Me.tabpg_Connections.Size = New System.Drawing.Size(824, 378)
        Me.tabpg_Connections.TabIndex = 0
        Me.tabpg_Connections.Text = "Connections"
        '
        'b_Connect
        '
        Me.b_Connect.Location = New System.Drawing.Point(16, 232)
        Me.b_Connect.Name = "b_Connect"
        Me.b_Connect.Size = New System.Drawing.Size(72, 24)
        Me.b_Connect.TabIndex = 3
        Me.b_Connect.Text = "Connect"
        '
        'gb_Destination
        '
        Me.gb_Destination.Controls.Add(Me.b_Destination_Database)
        Me.gb_Destination.Controls.Add(Me.cb_Destination_Database)
        Me.gb_Destination.Controls.Add(Me.chb_Destination_Auth)
        Me.gb_Destination.Controls.Add(Me.cb_Destination_Server)
        Me.gb_Destination.Controls.Add(Me.Label8)
        Me.gb_Destination.Controls.Add(Me.Label6)
        Me.gb_Destination.Controls.Add(Me.tb_Destination_Password)
        Me.gb_Destination.Controls.Add(Me.Label4)
        Me.gb_Destination.Controls.Add(Me.tb_Destination_User)
        Me.gb_Destination.Controls.Add(Me.Label2)
        Me.gb_Destination.Location = New System.Drawing.Point(240, 8)
        Me.gb_Destination.Name = "gb_Destination"
        Me.gb_Destination.Size = New System.Drawing.Size(240, 216)
        Me.gb_Destination.TabIndex = 2
        Me.gb_Destination.TabStop = False
        Me.gb_Destination.Text = "Destination"
        '
        'b_Destination_Database
        '
        Me.b_Destination_Database.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.b_Destination_Database.Location = New System.Drawing.Point(184, 152)
        Me.b_Destination_Database.Name = "b_Destination_Database"
        Me.b_Destination_Database.Size = New System.Drawing.Size(24, 23)
        Me.b_Destination_Database.TabIndex = 13
        Me.b_Destination_Database.Text = "*"
        '
        'cb_Destination_Database
        '
        Me.cb_Destination_Database.Location = New System.Drawing.Point(64, 152)
        Me.cb_Destination_Database.Name = "cb_Destination_Database"
        Me.cb_Destination_Database.Size = New System.Drawing.Size(121, 21)
        Me.cb_Destination_Database.TabIndex = 12
        Me.cb_Destination_Database.Text = "GAUST"
        '
        'chb_Destination_Auth
        '
        Me.chb_Destination_Auth.Location = New System.Drawing.Point(32, 48)
        Me.chb_Destination_Auth.Name = "chb_Destination_Auth"
        Me.chb_Destination_Auth.Size = New System.Drawing.Size(160, 24)
        Me.chb_Destination_Auth.TabIndex = 11
        Me.chb_Destination_Auth.Text = "Use NT Authentication"
        '
        'cb_Destination_Server
        '
        Me.cb_Destination_Server.Location = New System.Drawing.Point(80, 24)
        Me.cb_Destination_Server.Name = "cb_Destination_Server"
        Me.cb_Destination_Server.Size = New System.Drawing.Size(136, 21)
        Me.cb_Destination_Server.TabIndex = 10
        '
        'Label8
        '
        Me.Label8.Location = New System.Drawing.Point(32, 128)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(58, 23)
        Me.Label8.TabIndex = 9
        Me.Label8.Text = "DataBase"
        '
        'Label6
        '
        Me.Label6.Location = New System.Drawing.Point(32, 104)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(58, 23)
        Me.Label6.TabIndex = 7
        Me.Label6.Text = "Password"
        '
        'tb_Destination_Password
        '
        Me.tb_Destination_Password.Location = New System.Drawing.Point(112, 104)
        Me.tb_Destination_Password.Name = "tb_Destination_Password"
        Me.tb_Destination_Password.PasswordChar = Microsoft.VisualBasic.ChrW(42)
        Me.tb_Destination_Password.TabIndex = 6
        Me.tb_Destination_Password.Text = "74125"
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(32, 80)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(40, 23)
        Me.Label4.TabIndex = 5
        Me.Label4.Text = "User"
        '
        'tb_Destination_User
        '
        Me.tb_Destination_User.Location = New System.Drawing.Point(112, 80)
        Me.tb_Destination_User.Name = "tb_Destination_User"
        Me.tb_Destination_User.TabIndex = 4
        Me.tb_Destination_User.Text = "sa"
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(32, 24)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(40, 23)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Server"
        '
        'gb_Source
        '
        Me.gb_Source.Controls.Add(Me.b_Source_Database)
        Me.gb_Source.Controls.Add(Me.chb_Source_Auth)
        Me.gb_Source.Controls.Add(Me.Label7)
        Me.gb_Source.Controls.Add(Me.Label5)
        Me.gb_Source.Controls.Add(Me.tb_Source_Password)
        Me.gb_Source.Controls.Add(Me.Label3)
        Me.gb_Source.Controls.Add(Me.tb_Source_User)
        Me.gb_Source.Controls.Add(Me.Label1)
        Me.gb_Source.Controls.Add(Me.cb_Source_Server)
        Me.gb_Source.Controls.Add(Me.cb_Source_Database)
        Me.gb_Source.Location = New System.Drawing.Point(0, 8)
        Me.gb_Source.Name = "gb_Source"
        Me.gb_Source.Size = New System.Drawing.Size(224, 216)
        Me.gb_Source.TabIndex = 1
        Me.gb_Source.TabStop = False
        Me.gb_Source.Text = "Source"
        '
        'b_Source_Database
        '
        Me.b_Source_Database.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.b_Source_Database.Location = New System.Drawing.Point(176, 152)
        Me.b_Source_Database.Name = "b_Source_Database"
        Me.b_Source_Database.Size = New System.Drawing.Size(24, 23)
        Me.b_Source_Database.TabIndex = 14
        Me.b_Source_Database.Text = "*"
        '
        'chb_Source_Auth
        '
        Me.chb_Source_Auth.Location = New System.Drawing.Point(16, 48)
        Me.chb_Source_Auth.Name = "chb_Source_Auth"
        Me.chb_Source_Auth.Size = New System.Drawing.Size(160, 24)
        Me.chb_Source_Auth.TabIndex = 12
        Me.chb_Source_Auth.Text = "Use NT Authentication"
        '
        'Label7
        '
        Me.Label7.Location = New System.Drawing.Point(16, 128)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(58, 23)
        Me.Label7.TabIndex = 7
        Me.Label7.Text = "DataBase"
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(16, 104)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(58, 23)
        Me.Label5.TabIndex = 5
        Me.Label5.Text = "Password"
        '
        'tb_Source_Password
        '
        Me.tb_Source_Password.Location = New System.Drawing.Point(80, 104)
        Me.tb_Source_Password.Name = "tb_Source_Password"
        Me.tb_Source_Password.PasswordChar = Microsoft.VisualBasic.ChrW(42)
        Me.tb_Source_Password.TabIndex = 4
        Me.tb_Source_Password.Text = "74125"
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(16, 80)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(40, 23)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "User"
        '
        'tb_Source_User
        '
        Me.tb_Source_User.Location = New System.Drawing.Point(80, 80)
        Me.tb_Source_User.Name = "tb_Source_User"
        Me.tb_Source_User.TabIndex = 2
        Me.tb_Source_User.Text = "sa"
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(16, 24)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(40, 23)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Server"
        '
        'cb_Source_Server
        '
        Me.cb_Source_Server.Location = New System.Drawing.Point(64, 24)
        Me.cb_Source_Server.Name = "cb_Source_Server"
        Me.cb_Source_Server.Size = New System.Drawing.Size(152, 21)
        Me.cb_Source_Server.TabIndex = 6
        '
        'cb_Source_Database
        '
        Me.cb_Source_Database.Location = New System.Drawing.Point(56, 152)
        Me.cb_Source_Database.Name = "cb_Source_Database"
        Me.cb_Source_Database.Size = New System.Drawing.Size(121, 21)
        Me.cb_Source_Database.TabIndex = 4
        Me.cb_Source_Database.Text = "GAUST"
        '
        'tabpg_Trasfer
        '
        Me.tabpg_Trasfer.Controls.Add(Me.chb_Clear_Table)
        Me.tabpg_Trasfer.Controls.Add(Me.b_UnCheck_All)
        Me.tabpg_Trasfer.Controls.Add(Me.b_Load_Transfer)
        Me.tabpg_Trasfer.Controls.Add(Me.b_Save_Transfer)
        Me.tabpg_Trasfer.Controls.Add(Me.tb_Transfer_Log)
        Me.tabpg_Trasfer.Controls.Add(Me.l_TabTransfer)
        Me.tabpg_Trasfer.Controls.Add(Me.b_Transfer)
        Me.tabpg_Trasfer.Controls.Add(Me.pb_Transfer)
        Me.tabpg_Trasfer.Controls.Add(Me.l_TabCount)
        Me.tabpg_Trasfer.Controls.Add(Me.b_Analize)
        Me.tabpg_Trasfer.Controls.Add(Me.b_Check_All)
        Me.tabpg_Trasfer.Controls.Add(Me.b_Init)
        Me.tabpg_Trasfer.Controls.Add(Me.trv_Tables)
        Me.tabpg_Trasfer.Enabled = False
        Me.tabpg_Trasfer.Location = New System.Drawing.Point(4, 23)
        Me.tabpg_Trasfer.Name = "tabpg_Trasfer"
        Me.tabpg_Trasfer.Size = New System.Drawing.Size(824, 378)
        Me.tabpg_Trasfer.TabIndex = 4
        Me.tabpg_Trasfer.Text = "Copy data"
        '
        'chb_Clear_Table
        '
        Me.chb_Clear_Table.Location = New System.Drawing.Point(240, 152)
        Me.chb_Clear_Table.Name = "chb_Clear_Table"
        Me.chb_Clear_Table.TabIndex = 13
        Me.chb_Clear_Table.Text = "Clear Tables"
        '
        'b_UnCheck_All
        '
        Me.b_UnCheck_All.Image = CType(resources.GetObject("b_UnCheck_All.Image"), System.Drawing.Image)
        Me.b_UnCheck_All.Location = New System.Drawing.Point(160, 328)
        Me.b_UnCheck_All.Name = "b_UnCheck_All"
        Me.b_UnCheck_All.Size = New System.Drawing.Size(24, 24)
        Me.b_UnCheck_All.TabIndex = 12
        '
        'b_Load_Transfer
        '
        Me.b_Load_Transfer.Location = New System.Drawing.Point(229, 8)
        Me.b_Load_Transfer.Name = "b_Load_Transfer"
        Me.b_Load_Transfer.Size = New System.Drawing.Size(64, 24)
        Me.b_Load_Transfer.TabIndex = 11
        Me.b_Load_Transfer.Text = "Load"
        '
        'b_Save_Transfer
        '
        Me.b_Save_Transfer.Enabled = False
        Me.b_Save_Transfer.Location = New System.Drawing.Point(301, 8)
        Me.b_Save_Transfer.Name = "b_Save_Transfer"
        Me.b_Save_Transfer.Size = New System.Drawing.Size(64, 24)
        Me.b_Save_Transfer.TabIndex = 10
        Me.b_Save_Transfer.Text = "Save"
        '
        'tb_Transfer_Log
        '
        Me.tb_Transfer_Log.Location = New System.Drawing.Point(408, 16)
        Me.tb_Transfer_Log.Multiline = True
        Me.tb_Transfer_Log.Name = "tb_Transfer_Log"
        Me.tb_Transfer_Log.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.tb_Transfer_Log.Size = New System.Drawing.Size(304, 160)
        Me.tb_Transfer_Log.TabIndex = 9
        Me.tb_Transfer_Log.Text = ""
        '
        'l_TabTransfer
        '
        Me.l_TabTransfer.Location = New System.Drawing.Point(232, 184)
        Me.l_TabTransfer.Name = "l_TabTransfer"
        Me.l_TabTransfer.Size = New System.Drawing.Size(96, 16)
        Me.l_TabTransfer.TabIndex = 8
        Me.l_TabTransfer.Text = "Table:"
        '
        'b_Transfer
        '
        Me.b_Transfer.Enabled = False
        Me.b_Transfer.Location = New System.Drawing.Point(232, 112)
        Me.b_Transfer.Name = "b_Transfer"
        Me.b_Transfer.Size = New System.Drawing.Size(56, 24)
        Me.b_Transfer.TabIndex = 7
        Me.b_Transfer.Text = "Transfer"
        '
        'pb_Transfer
        '
        Me.pb_Transfer.Location = New System.Drawing.Point(232, 208)
        Me.pb_Transfer.Name = "pb_Transfer"
        Me.pb_Transfer.Size = New System.Drawing.Size(128, 24)
        Me.pb_Transfer.TabIndex = 6
        '
        'l_TabCount
        '
        Me.l_TabCount.Location = New System.Drawing.Point(8, 336)
        Me.l_TabCount.Name = "l_TabCount"
        Me.l_TabCount.Size = New System.Drawing.Size(112, 16)
        Me.l_TabCount.TabIndex = 5
        Me.l_TabCount.Text = "Tables: 0"
        '
        'b_Analize
        '
        Me.b_Analize.Enabled = False
        Me.b_Analize.Location = New System.Drawing.Point(232, 80)
        Me.b_Analize.Name = "b_Analize"
        Me.b_Analize.Size = New System.Drawing.Size(56, 24)
        Me.b_Analize.TabIndex = 3
        Me.b_Analize.Text = "Analize"
        '
        'b_Check_All
        '
        Me.b_Check_All.Image = CType(resources.GetObject("b_Check_All.Image"), System.Drawing.Image)
        Me.b_Check_All.Location = New System.Drawing.Point(128, 328)
        Me.b_Check_All.Name = "b_Check_All"
        Me.b_Check_All.Size = New System.Drawing.Size(24, 24)
        Me.b_Check_All.TabIndex = 2
        Me.b_Check_All.Text = "v"
        '
        'b_Init
        '
        Me.b_Init.Location = New System.Drawing.Point(232, 48)
        Me.b_Init.Name = "b_Init"
        Me.b_Init.Size = New System.Drawing.Size(56, 24)
        Me.b_Init.TabIndex = 1
        Me.b_Init.Text = "Init"
        '
        'trv_Tables
        '
        Me.trv_Tables.CheckBoxes = True
        Me.trv_Tables.ImageList = Me.imgl_Objects
        Me.trv_Tables.Location = New System.Drawing.Point(8, 8)
        Me.trv_Tables.Name = "trv_Tables"
        Me.trv_Tables.Size = New System.Drawing.Size(200, 312)
        Me.trv_Tables.TabIndex = 0
        '
        'tabpg_Compare
        '
        Me.tabpg_Compare.Controls.Add(Me.cntr_Compare)
        Me.tabpg_Compare.Location = New System.Drawing.Point(4, 23)
        Me.tabpg_Compare.Name = "tabpg_Compare"
        Me.tabpg_Compare.Size = New System.Drawing.Size(824, 378)
        Me.tabpg_Compare.TabIndex = 5
        Me.tabpg_Compare.Text = "Compare Meta"
        '
        'cntr_Compare
        '
        Me.cntr_Compare.action = Nothing
        Me.cntr_Compare.Enabled = False
        Me.cntr_Compare.Location = New System.Drawing.Point(8, 0)
        Me.cntr_Compare.Name = "cntr_Compare"
        Me.cntr_Compare.Size = New System.Drawing.Size(832, 384)
        Me.cntr_Compare.TabIndex = 0
        '
        'tabpg_ComapeData
        '
        Me.tabpg_ComapeData.Controls.Add(Me.cntr_CompareData)
        Me.tabpg_ComapeData.Location = New System.Drawing.Point(4, 23)
        Me.tabpg_ComapeData.Name = "tabpg_ComapeData"
        Me.tabpg_ComapeData.Size = New System.Drawing.Size(824, 378)
        Me.tabpg_ComapeData.TabIndex = 2
        Me.tabpg_ComapeData.Text = "Compare Data"
        '
        'cntr_CompareData
        '
        Me.cntr_CompareData.Location = New System.Drawing.Point(8, 8)
        Me.cntr_CompareData.Name = "cntr_CompareData"
        Me.cntr_CompareData.Size = New System.Drawing.Size(640, 456)
        Me.cntr_CompareData.TabIndex = 0
        '
        'tabpg_ClearData
        '
        Me.tabpg_ClearData.Controls.Add(Me.cntr_ClearControl)
        Me.tabpg_ClearData.Location = New System.Drawing.Point(4, 23)
        Me.tabpg_ClearData.Name = "tabpg_ClearData"
        Me.tabpg_ClearData.Size = New System.Drawing.Size(824, 378)
        Me.tabpg_ClearData.TabIndex = 6
        Me.tabpg_ClearData.Text = "Clear Tables"
        '
        'cntr_ClearControl
        '
        Me.cntr_ClearControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cntr_ClearControl.Location = New System.Drawing.Point(0, 0)
        Me.cntr_ClearControl.Name = "cntr_ClearControl"
        Me.cntr_ClearControl.Size = New System.Drawing.Size(824, 378)
        Me.cntr_ClearControl.TabIndex = 0
        '
        'DataSet1
        '
        Me.DataSet1.DataSetName = "NewDataSet"
        Me.DataSet1.Locale = New System.Globalization.CultureInfo("ru-RU")
        Me.DataSet1.Tables.AddRange(New System.Data.DataTable() {Me.users})
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
        'MainMenu1
        '
        Me.MainMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem1})
        '
        'MenuItem1
        '
        Me.MenuItem1.Index = 0
        Me.MenuItem1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mi_NextDiff, Me.mi_Execute})
        Me.MenuItem1.Text = "Compare"
        '
        'mi_NextDiff
        '
        Me.mi_NextDiff.Index = 0
        Me.mi_NextDiff.Shortcut = System.Windows.Forms.Shortcut.CtrlZ
        Me.mi_NextDiff.Text = "Ne&xt Difference"
        '
        'mi_Execute
        '
        Me.mi_Execute.Index = 1
        Me.mi_Execute.Shortcut = System.Windows.Forms.Shortcut.CtrlX
        Me.mi_Execute.Text = "Execute"
        '
        'DBSync
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(832, 405)
        Me.Controls.Add(Me.tabs_Tabs)
        Me.Menu = Me.MainMenu1
        Me.Name = "DBSync"
        Me.Text = "DBSync"
        Me.tabs_Tabs.ResumeLayout(False)
        Me.tabpg_Connections.ResumeLayout(False)
        Me.gb_Destination.ResumeLayout(False)
        Me.gb_Source.ResumeLayout(False)
        Me.tabpg_Trasfer.ResumeLayout(False)
        Me.tabpg_Compare.ResumeLayout(False)
        Me.tabpg_ComapeData.ResumeLayout(False)
        Me.tabpg_ClearData.ResumeLayout(False)
        CType(Me.DataSet1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.users, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Function Connect_Dest() As Boolean
        Try
            Connect_Dest = True
            If Not oSQLServer_Dest.HostName Is Nothing Then
                oSQLServer_Dest.DisConnect()
            End If
            oSQLServer_Dest.LoginTimeout = -1 '-1 is the ODBC default (60) seconds
            'Connect to the Server
            If chb_Destination_Auth.Checked Then
                With oSQLServer_Dest
                    'Use NT Authentication
                    .LoginSecure = True
                    'Do not reconnect automatically
                    .AutoReConnect = False
                    'Now connect
                    .Connect(cb_Destination_Server.Text)
                End With
            Else
                With oSQLServer_Dest
                    'Use SQL Server Authentication
                    .LoginSecure = False
                    'Do not reconnect automatically
                    .AutoReConnect = False
                    'Use SQL Security
                    .Connect(cb_Destination_Server.Text, tb_Destination_User.Text, tb_Destination_Password.Text)
                End With
            End If


            Exit Function
        Catch ex As Exception
            MsgBox("Error: " & ex.Message & ex.HelpLink, vbOKOnly, ex.Source & ": Login Error")
            Connect_Dest = False
        End Try
    End Function
    Private Function Connect_Source() As Boolean
        Try

            Connect_Source = True
            If Not oSQLServer_Source.HostName Is Nothing Then
                oSQLServer_Source.DisConnect()
            End If

            oSQLServer_Source.LoginTimeout = -1 '-1 is the ODBC default (60) seconds
            If chb_Source_Auth.Checked Then
                With oSQLServer_Source
                    .LoginSecure = True
                    .AutoReConnect = False
                    .Connect(cb_Source_Server.Text)
                End With
            Else
                With oSQLServer_Source
                    .LoginSecure = False
                    .AutoReConnect = False
                    .Connect(cb_Source_Server.Text, tb_Source_User.Text, tb_Source_Password.Text)
                End With
            End If

        Catch ex As Exception
            Connect_Source = False
            MsgBox("Error: " & ex.Message & ex.HelpLink, vbOKOnly, ex.Source & ": Login Error")
            Exit Function
        End Try







    End Function

    Private Sub Init_Obj_Tree_Table(ByRef nodes As System.Windows.Forms.TreeNodeCollection, ByRef oTable As SQLDMO.Table2)
        Dim oColumn As SQLDMO.Column2
        Dim oTrigger As SQLDMO.Trigger2
        Dim oIndex As SQLDMO.Index2

        Dim Node As TreeNode
        Dim n As Integer

        ' ADD COLUMNS
        Node = New TreeNode("Columns")
        Node.ImageIndex = Me.imit_Column
        Node.SelectedImageIndex = Me.imit_Column
        n = nodes.Add(Node)

        For Each oColumn In oTable.Columns
            Node = New TreeNode(oColumn.Name)
            Node.ImageIndex = imit_OK
            Node.SelectedImageIndex = imit_OK
            nodes(n).Nodes.Add(Node)
        Next

        'ADD TRIGGERS
        Node = New TreeNode("Triggers")
        Node.ImageIndex = Me.imit_Trigger
        Node.SelectedImageIndex = Me.imit_Trigger
        n = nodes.Add(Node)

        For Each oTrigger In oTable.Triggers
            Node = New TreeNode(oTrigger.Name)
            Node.ImageIndex = imit_OK
            Node.SelectedImageIndex = imit_OK
            nodes(n).Nodes.Add(Node)
        Next

        'ADD INDEX
        Node = New TreeNode("Indexes")
        Node.ImageIndex = Me.imit_Index
        Node.SelectedImageIndex = Me.imit_Index
        n = nodes.Add(Node)

        For Each oIndex In oTable.Indexes
            Node = New TreeNode(oIndex.Name)
            Node.ImageIndex = imit_OK
            Node.SelectedImageIndex = imit_OK
            nodes(n).Nodes.Add(Node)
        Next

    End Sub
    Private Sub Init_Obj_Tree(ByRef tree As System.Windows.Forms.TreeView, ByRef db As SQLDMO.Database2)
        Dim oTable As SQLDMO.Table2
        Dim oView As SQLDMO.View2
        Dim oSP As SQLDMO.StoredProcedure2
        Dim oUDF As SQLDMO.UserDefinedFunction
        Dim n As Integer
        Dim Node As TreeNode
        'oTable.PrimaryKey()
        For Each oTable In db.Tables
            'This keeps the system databases from being listed
            If Not oTable.SystemObject Then
                Node = New TreeNode(oTable.Name)
                Node.ImageIndex = imit_OK
                Node.SelectedImageIndex = imit_OK
                n = tree.Nodes(Me.ind_Table).Nodes.Add(Node)
                Init_Obj_Tree_Table(tree.Nodes(Me.ind_Table).Nodes(n).Nodes, oTable)
            End If
        Next

        For Each oView In db.Views
            'This keeps the system databases from being listed
            If Not oView.SystemObject Then
                Node = New TreeNode(oView.Name)
                Node.ImageIndex = imit_OK
                Node.SelectedImageIndex = imit_OK
                tree.Nodes(Me.ind_View).Nodes.Add(Node)
            End If
        Next


        For Each oSP In db.StoredProcedures
            'This keeps the system databases from being listed
            If Not oSP.SystemObject Then
                Node = New TreeNode(oSP.Name)
                Node.ImageIndex = imit_OK
                Node.SelectedImageIndex = imit_OK
                tree.Nodes(Me.ind_SP).Nodes.Add(Node)
            End If
        Next

        For Each oUDF In db.UserDefinedFunctions
            'This keeps the system databases from being listed
            If Not oUDF.SystemObject Then
                Node = New TreeNode(oUDF.Name)
                Node.ImageIndex = imit_OK
                Node.SelectedImageIndex = imit_OK
                tree.Nodes(Me.ind_UDF).Nodes.Add(Node)
            End If
        Next
    End Sub



#Region "Form Messages"

    Private Sub DBSync_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Dim i As Integer
        'Use the SQL DMO Application Object to find the available SQL Servers
        oSQLServerDMOApp = New SQLDMO.Application

        Dim namX As SQLDMO.NameList
        Dim ns As String
        namX = oSQLServerDMOApp.ListAvailableSQLServers
        For i = 1 To namX.Count
            ns = namX.Item(i)
            cb_Source_Server.Items.Add(ns)

            cb_Destination_Server.Items.Add(ns)
        Next
        cb_Source_Server.SelectedIndex = 0
        cb_Destination_Server.SelectedIndex = 0


        'FileOpen(1, "log.txt", OpenMode.Output)


        Dim args As String() = Environment.GetCommandLineArgs()
        Dim param As String()
        For i = 1 To args.Length - 1
            param = args(i).Split("=")
            Select Case param(0).Trim
                Case "-cmdAct"
                    Dim fs As FileStream
                    'Load Default connection
                    Try
                        If File.Exists(param(1).Trim) Then
                            Dim ConSerializer As New XmlSerializer(GetType(CompareAction))
                            fs = New FileStream(param(1).Trim, FileMode.Open)
                            cntr_Compare.action = CType(ConSerializer.Deserialize(fs), CompareAction)
                            fs.Close()
                        End If
                        cntr_Compare.CheckAllObj()
                        cntr_Compare.Compare_DB()
                    Catch ex As Exception
                        MsgBox("Error DefConnection: " & ex.Source & vbCrLf & ex.Message & ex.HelpLink)

                    End Try

            End Select


        Next
    End Sub
    Private Sub DBSync_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        'When done with the connection to SQLServer you must Disconnect
        'If Not oSQLServer_Source Is Nothing Then
        '    oSQLServer_Source.DisConnect()
        '    oSQLServer_Source = Nothing
        'End If

        'If Not oSQLServer_Dest Is Nothing Then
        '    oSQLServer_Dest.DisConnect()
        '    oSQLServer_Dest = Nothing
        'End If

        'FileClose(1)   ' Close file.
    End Sub

    Private Sub b_Connect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles b_Connect.Click
        'Connect_Source()
        'Connect_Dest()
        'tabpg_Compare.Enabled = True

        oDB_Destination = oSQLServer_Dest.Databases.Item(cb_Destination_Database.Text)
        oDB_Source = oSQLServer_Source.Databases.Item(cb_Source_Database.Text)
        cntr_Compare.Enabled = True

        Dim compAct As New CompareAction
        compAct.DstCon = New Connection
        compAct.DstCon.DataBase = tb_Destination_Database.Text
        compAct.DstCon.Password = tb_Destination_Password.Text
        compAct.DstCon.Server = cb_Destination_Server.Text
        compAct.DstCon.User = tb_Destination_User.Text

        compAct.SrcCon = New Connection
        compAct.SrcCon.DataBase = cb_Destination_Database.Text
        compAct.SrcCon.Password = tb_Source_Password.Text
        compAct.SrcCon.Server = cb_Source_Server.Text
        compAct.SrcCon.User = tb_Source_User.Text
        compAct.IgnUsr = New ArrayList
        cntr_Compare.action = compAct

        cntr_CompareData.Init("workstation id='SAVTCHIN-D';packet size=4096;user id=" & Me.tb_Source_User.Text & ";data source='" & cb_Source_Server.Text & "';persist security info=True;initial catalog=" & Me.tb_Source_Database.Text & ";password=" & Me.tb_Source_Password.Text, "workstation id='SAVTCHIN-D';packet size=4096;user id=" & Me.tb_Destination_User.Text & ";data source='" & cb_Destination_Server.Text & "';persist security info=True;initial catalog=" & Me.tb_Destination_Database.Text & ";password=" & Me.tb_Destination_Password.Text)

        cntr_ClearControl.Init("workstation id='SAVTCHIN-D';packet size=4096;user id=" & Me.tb_Source_User.Text & ";data source='" & cb_Source_Server.Text & "';persist security info=True;initial catalog=" & Me.tb_Source_Database.Text & ";password=" & Me.tb_Source_Password.Text)
        'con_Source.ConnectionString = "workstation id='SAVTCHIN-D';packet size=4096;user id=" & Me.tb_Source_User.Text & ";data source='" & cb_Source_Server.Text & "';persist security info=True;initial catalog=" & Me.tb_Source_Database.Text & ";password=" & Me.tb_Source_Password.Text
        'Dim ad As New SqlClient.SqlDataAdapter
        'ad.SelectCommand = New SqlClient.SqlCommand("select name from sysusers", con_Source)
        'If con_Source.State = ConnectionState.Closed Then
        '    con_Source.Open()
        'End If


        'ad.Fill(DataSet1.Tables(0))


    End Sub
    Private Sub b_InitComp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        '  metaComp.srcConStr = "workstation id='SAVTCHIN-D';packet size=4096;user id=" & _
        '  Me.tb_Source_User.Text & ";data source='" & cb_Destination_Server.Text & _
        '  "';persist security info=True;initial catalog=" & Me.tb_Source_Database.Text & _
        '  ";password=" & Me.tb_Source_Password.Text

        '  metaComp.destConStr = "workstation id='SAVTCHIN-D';packet size=4096;user id=" & _
        'Me.tb_Destination_User.Text & ";data source='" & cb_Source_Server.Text & _
        '"';persist security info=True;initial catalog=" & Me.tb_Destination_Database.Text & ";password=" & Me.tb_Destination_Password.Text

        '  metaComp.Init()

    End Sub



    Private Function rec(ByRef node As TreeNode, ByRef nodes As TreeNodeCollection) As Integer
        If Not node.Parent Is Nothing Then
            rec(node.Parent, nodes)
            rec = node.Parent.Index
        End If

    End Function


    Private Sub mi_NextDiff_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mi_NextDiff.Click
        cntr_Compare.NextDiff_Select()
    End Sub
    Private Sub mi_Execute_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mi_Execute.Click
        cntr_Compare.Execute()
    End Sub


#End Region

#Region "SQLDMO Servers messages"
    Private Function oSQLServer_Dest_QueryTimeout(ByVal Message As String) As Boolean Handles oSQLServer_Dest.QueryTimeout
        'QueryTimeout event occurs when Microsoft® SQL Server™ cannot complete execution of a Transact-SQL command batch within a user-defined period of time
        Dim sMsg As String
        sMsg = "QueryTimeout: " & vbCrLf & _
        Message

        MsgBox(sMsg, vbOKOnly, "Timeout SQLServer Object Event")
    End Function
    Private Function oSQLServer_Source_QueryTimeout(ByVal Message As String) As Boolean Handles oSQLServer_Source.QueryTimeout
        'QueryTimeout event occurs when Microsoft® SQL Server™ cannot complete execution of a Transact-SQL command batch within a user-defined period of time
        Dim sMsg As String
        sMsg = "QueryTimeout: " & vbCrLf & _
        Message

        MsgBox(sMsg, vbOKOnly, "Timeout SQLServer Object Event")
    End Function

    Private Function oSQLServer_Dest_ConnectionBroken(ByVal Message As String) As Boolean Handles oSQLServer_Dest.ConnectionBroken
        'ConnectionBroken event occurs when a connected SQLServer object loses its connection
        Dim sMsg As String
        sMsg = "ConnectionBroken: " & vbCrLf & _
        Message

        MsgBox(sMsg, vbOKOnly, "ConnectionBroken SQLServer Object Event")
    End Function
    Private Function oSQLServer_Source_ConnectionBroken(ByVal Message As String) As Boolean Handles oSQLServer_Source.ConnectionBroken
        'ConnectionBroken event occurs when a connected SQLServer object loses its connection
        Dim sMsg As String
        sMsg = "ConnectionBroken: " & vbCrLf & _
        Message

        MsgBox(sMsg, vbOKOnly, "ConnectionBroken SQLServer Object Event")
    End Function

    Private Sub oSQLServer_Dest_RemoteLoginFailed(ByVal Severity As Integer, ByVal MessageNumber As Integer, ByVal MessageState As Integer, ByVal Message As String) Handles oSQLServer_Dest.RemoteLoginFailed
        'RemoteLoginFailed event occurs when an instance of Microsoft® SQL Server™ attempts to connect to a remote server fails
        Dim sMsg As String
        sMsg = "RemoteLoginFailed: " & vbCrLf & _
        "Severity: " & Severity & vbCrLf & _
        "MessageNumber: " & MessageNumber & vbCrLf & _
        "MessageState: " & MessageState & vbCrLf & _
        "Message: " & Message

        MsgBox(sMsg, vbOKOnly, "RemoteLoginFailed SQLServer Object Event")
    End Sub
    Private Sub oSQLServer_Source_RemoteLoginFailed(ByVal Severity As Integer, ByVal MessageNumber As Integer, ByVal MessageState As Integer, ByVal Message As String) Handles oSQLServer_Source.RemoteLoginFailed
        'RemoteLoginFailed event occurs when an instance of Microsoft® SQL Server™ attempts to connect to a remote server fails
        Dim sMsg As String
        sMsg = "RemoteLoginFailed: " & vbCrLf & _
        "Severity: " & Severity & vbCrLf & _
        "MessageNumber: " & MessageNumber & vbCrLf & _
        "MessageState: " & MessageState & vbCrLf & _
        "Message: " & Message

        MsgBox(sMsg, vbOKOnly, "RemoteLoginFailed SQLServer Object Event")
    End Sub

    'Private Sub oSQLServer_Dest_ServerMessage(ByVal Severity As Integer, ByVal MessageNumber As Integer, ByVal MessageState As Integer, ByVal Message As String) Handles oSQLServer_Dest.ServerMessage
    '    'ServerMessage event occurs when a Microsoft® SQL Server™ success-with-information message is returned to the SQL-DMO application
    '    Dim sMsg As String
    '    sMsg = "ServerMessage: " & vbCrLf & _
    '    "Severity: " & Severity & vbCrLf & _
    '    "MessageNumber: " & MessageNumber & vbCrLf & _
    '    "MessageState: " & MessageState & vbCrLf & _
    '    "Message: " & Message

    '    MsgBox(sMsg, vbOKOnly, "ServerMessage SQLServer Object Event")
    'End Sub
    'Private Sub oSQLServer_Source_ServerMessage(ByVal Severity As Integer, ByVal MessageNumber As Integer, ByVal MessageState As Integer, ByVal Message As String) Handles oSQLServer_Source.ServerMessage
    '    'ServerMessage event occurs when a Microsoft® SQL Server™ success-with-information message is returned to the SQL-DMO application
    '    Dim sMsg As String
    '    sMsg = "ServerMessage: " & vbCrLf & _
    '    "Severity: " & Severity & vbCrLf & _
    '    "MessageNumber: " & MessageNumber & vbCrLf & _
    '    "MessageState: " & MessageState & vbCrLf & _
    '    "Message: " & Message

    '    MsgBox(sMsg, vbOKOnly, "ServerMessage SQLServer Object Event")
    'End Sub

    'Private Sub oSQLServer_Dest_CommandSent(ByVal SQLCommand As String) Handles oSQLServer_Dest.CommandSent
    '    'CommandSent event occurs when SQL-DMO submits a Transact-SQL command batch to the connected instance
    '    Dim sMsg As String
    '    sMsg = "CommandSent: " & vbCrLf & _
    '    SQLCommand
    '    PrintLine(1, "Destination>> ", sMsg)  ' Print in two print zones.
    '    'MsgBox(sMsg, vbOKOnly, "SQLServer Object Event")
    'End Sub
    'Private Sub oSQLServer_Source_CommandSent(ByVal SQLCommand As String) Handles oSQLServer_Source.CommandSent
    '    'CommandSent event occurs when SQL-DMO submits a Transact-SQL command batch to the connected instance
    '    Dim sMsg As String
    '    sMsg = "CommandSent: " & vbCrLf & _
    '    SQLCommand

    '    PrintLine(1, "Source>> ", sMsg)  ' Print in two print zones.

    '    ' MsgBox(sMsg, vbOKOnly, "SQLServer Object Event")
    'End Sub
#End Region

#Region "Copy data"
    Private Sub b_Save_Transfer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles b_Save_Transfer.Click
        Try
            ' Creates a new instance of the XmlSerializer class.
            Dim s = New XmlSerializer(GetType(DataTransfer))
            ' Needed a StreamWriter to write the file.
            Dim myWriter As New StreamWriter(oSQLServer_Dest.Name & "_" & oDB_Destination.Name & ".xml")

            s.Serialize(myWriter, Me.dataTransfer)
            myWriter.Close()

            'Dim xml_doc As New System.Xml.XmlDocument

            'Dim xml_First_Node As Xml.XmlNode, xml_Node As Xml.XmlNode, xml_Tab As Xml.XmlNode
            'Dim tr_Node As TreeNode

            'xml_First_Node = xml_doc.AppendChild(xml_doc.CreateElement("TabSeq"))
            'xml_First_Node.Attributes.Append(xml_doc.CreateAttribute("Type")).Value = "Insert"

            'xml_Node = xml_First_Node.AppendChild(xml_doc.CreateElement("Server"))
            'xml_Node.Attributes.Append(xml_doc.CreateAttribute("Name")).Value = oSQLServer_Dest.Name

            'xml_Node = xml_First_Node.AppendChild(xml_doc.CreateElement("Database"))
            'xml_Node.Attributes.Append(xml_doc.CreateAttribute("Name")).Value = oDB_Destination.Name

            'xml_Node = xml_First_Node.AppendChild(xml_doc.CreateElement("Tables"))


            'For Each tr_Node In trv_Tables.Nodes
            '    xml_Tab = xml_Node.AppendChild(xml_doc.CreateElement("Table"))
            '    xml_Tab.Attributes.Append(xml_doc.CreateAttribute("Name")).Value = tr_Node.Text
            'Next

            'xml_doc.Save(oSQLServer_Dest.Name & "_" & oDB_Destination.Name & ".xml")
            Exit Sub
        Catch ex As Exception
            tb_Transfer_Log.Text = "Transfer Error: " & ex.Source & vbCrLf & ex.Message & ex.HelpLink & vbCrLf & tb_Transfer_Log.Text
            '            MsgBox("Error: " & ex.Message & ex.HelpLink, vbOKOnly, ex.Source & ": Transfer Error")
        End Try
    End Sub

    Private Sub b_Load_Transfer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles b_Load_Transfer.Click
        OFD.Filter = ".xml|*.xml"
        If OFD.ShowDialog() <> DialogResult.OK Then
            Exit Sub
        End If

        Dim xml_doc As New System.Xml.XmlDocument
        Dim xml_Node As Xml.XmlNode, xml_Tab As Xml.XmlNode
        Dim TabName As String
        Dim Node As TreeNode

        Dim serializer As New XmlSerializer(GetType(DataTransfer))
        ' If the XML document has been altered with unknown
        ' nodes or attributes, handle them with the
        ' UnknownNode and UnknownAttribute events.
        'AddHandler serializer.UnknownNode, AddressOf serializer_UnknownNode
        'AddHandler serializer.UnknownAttribute, AddressOf serializer_UnknownAttribute

        ' A FileStream is needed to read the XML document.
        Dim fs As New FileStream(OFD.FileName, FileMode.Open)
        ' Declare an object variable of the type to be deserialized.
        Dim po As DataTransfer
        ' Use the Deserialize method to restore the object's state with
        ' data from the XML document. 
        dataTransfer = CType(serializer.Deserialize(fs), DataTransfer)

        b_Transfer.Enabled = True
    End Sub
    Private Sub b_Init_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles b_Init.Click
        'Dim oTable As SQLDMO.Table2
        Dim Node As TreeNode
        Dim i As Integer
        Dim enTables As IEnumerator


        trv_Tables.Nodes.Clear()
        trv_Tables.CheckBoxes = True

        If oDB_Destination Is Nothing Then
            If Connect_Dest() = False Then
                tb_Transfer_Log.Text = "Incorrect destination login"
                Exit Sub
            End If
        End If
        If oDB_Source Is Nothing Then
            If Connect_Source() = False Then
                tb_Transfer_Log.Text = "Incorrect source login"
                Exit Sub
            End If
        End If

        dataTransfer.setDatabase(Me.oDB_Source, Me.oDB_Destination)
        dataTransfer.Init()
        enTables = dataTransfer.arTables.GetEnumerator
        While enTables.MoveNext()
            i += 1
            Node = New TreeNode(enTables.Current)
            Node.ImageIndex = Me.imit_Table
            Node.SelectedImageIndex = Me.imit_Table
            trv_Tables.Nodes.Add(Node)
        End While

        trv_Tables.Enabled = True
        l_TabCount.Text = "Tables: " & i
        Me.b_Analize.Enabled = True

    End Sub
    Private Sub b_Check_All_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles b_Check_All.Click
        Dim Node As TreeNode
        For Each Node In trv_Tables.Nodes
            Node.Checked = True
        Next
    End Sub
    Private Sub b_UnCheck_All_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles b_UnCheck_All.Click
        Dim Node As TreeNode
        For Each Node In trv_Tables.Nodes
            Node.Checked = False
        Next
    End Sub


    Private Sub b_Analize_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles b_Analize.Click
        Dim Node As TreeNode
        Dim Tables As ArrayList = New ArrayList

        For Each Node In trv_Tables.Nodes
            If Node.Checked = True Then
                Tables.Add(Node.Text)
            End If
        Next
        dataTransfer.arTables = Tables
        dataTransfer.Analyze()

        'Dim Tables As New Hashtable
        'Dim par As SQLDMO.QueryResults2
        'Dim Node As TreeNode
        'Dim n As String
        '' Dim i As Integer
        'Dim Tab As DictionaryEntry

        'tb_Transfer_Log.Text = "Analize start "
        ''i = 20
        'Deleted_Seq.Clear()
        'Inserted_Seq.Clear()

        'For Each Node In trv_Tables.Nodes
        '    If Node.Checked = True Then
        '        tb_Transfer_Log.Text = "table " & Node.Text & vbCrLf & tb_Transfer_Log.Text
        '        Application.DoEvents()

        '        Ch_Tab_Ins(Node.Text, 0)
        '        Ch_Tab_Del(Node.Text, 0)
        '        'par = oDB_Destination.Tables.Item(Node.Text).EnumDependencies(SQLDMO.SQLDMO_DEPENDENCY_TYPE.SQLDMODep_Parents)
        '        'If par.Rows = 0 Then
        '        '    Tables1.Add(Node.Text)
        '        'Else
        '        '    Tables.Add(Node.Text, par)
        '        'End If

        '    End If
        'Next



        ''trv_Tables.Nodes.Clear()
        ''trv_Tables.CheckBoxes = False

        ''For Each n In Inserted_Seq
        ''    Node = New TreeNode(n)
        ''    Node.ImageIndex = Me.imit_Table
        ''    Node.SelectedImageIndex = Me.imit_Table
        ''    trv_Tables.Nodes.Add(Node)

        ''Next

        ''l_TabCount.Text = "Tables: " & Inserted_Seq.Count
        ''While i
        ''    For Each Tab In Tables
        ''        par = Tab.Value

        ''    Next
        ''    i -= 1
        ''End While
        trv_Tables.Enabled = False
        Me.b_Transfer.Enabled = True
        Me.b_Save_Transfer.Enabled = True

        'tb_Transfer_Log.Text = "Analize end " & vbCrLf & tb_Transfer_Log.Text
    End Sub

    Private Sub b_Transfer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles b_Transfer.Click

        dataTransfer.Transf(chb_Clear_Table.Checked)
        'Try
        '    ' Dim Node As TreeNode
        '    Dim tabName As String
        '    If oDB_Source Is Nothing Then
        '        Connect_Source()
        '    End If


        '    If Me.chb_Clear_Table.Checked = True Then
        '        For Each tabName In Me.Deleted_Seq
        '            oDB_Destination.ExecuteImmediate("DELETE FROM " & tabName)
        '        Next
        '    End If


        '    Transfer.CopyData = SQLDMO.SQLDMO_COPYDATA_TYPE.SQLDMOCopyData_Append
        '    Transfer.CopySchema = False

        '    Transfer.DestServer = oSQLServer_Dest.Name
        '    Transfer.DestLogin = oSQLServer_Dest.Login
        '    Transfer.DestPassword = oSQLServer_Dest.Password
        '    Transfer.DestDatabase = oDB_Destination.Name


        '    tb_Transfer_Log.Text = "Transfer start" & vbCrLf

        '    'Me.trv_Tables.Nodes.
        '    For Each tabName In Me.Inserted_Seq
        '        'If Node.ImageIndex = imit_Table Then
        '        pb_Transfer.Value = 0
        '        tb_Transfer_Log.Text = "table " & tabName & vbCrLf & tb_Transfer_Log.Text
        '        Application.DoEvents()
        '        Transfer.AddObjectByName(tabName, SQLDMO.SQLDMO_OBJECT_TYPE.SQLDMOObj_UserTable)
        '        oDB_Source.Transfer(Transfer)
        '        Transfer.RemoveAllObjects()
        '        ' Node.ImageIndex = imit_OK
        '        '  Node.SelectedImageIndex = imit_OK
        '        '  End If
        '    Next

        '    tb_Transfer_Log.Text = "Transfer end " & vbCrLf & tb_Transfer_Log.Text
        '    Exit Sub
        'Catch ex As Exception
        '    tb_Transfer_Log.Text = "Transfer Error: " & ex.Source & vbCrLf & ex.Message & ex.HelpLink & vbCrLf & tb_Transfer_Log.Text
        '    '            MsgBox("Error: " & ex.Message & ex.HelpLink, vbOKOnly, ex.Source & ": Transfer Error")
        'End Try
    End Sub
    Private Sub Transfer_StatusMessage(ByVal Message As String) Handles Transfer.StatusMessage
        tb_Transfer_Log.Text = "TSM message: " & Message & vbCrLf & tb_Transfer_Log.Text
        Application.DoEvents()
    End Sub
    Private Sub Transfer_TransferPercentComplete(ByVal Message As String, ByVal Percent As Integer) Handles Transfer.TransferPercentComplete
        tb_Transfer_Log.Text = "TPC message: " & Message & vbCrLf & tb_Transfer_Log.Text

        Application.DoEvents()

    End Sub
    Private Sub Transfer_PercentCompleteAtStep(ByVal Message As String, ByVal Percent As Integer) Handles Transfer.PercentCompleteAtStep
        'tb_Transfer_Log.Text = "TPCAS message: " & Message & vbCrLf & tb_Transfer_Log.Text
        'pb_Transfer.Value = Percent
        'Application.DoEvents()
    End Sub
    Private Sub dataTransfer_Status(ByVal m As String) Handles dataTransfer.Status
        tb_Transfer_Log.Text = m & vbCrLf & tb_Transfer_Log.Text
    End Sub
#End Region



    Private Sub b_Destination_Database_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles b_Destination_Database.Click

    End Sub




    Private Sub cb_Source_Database_DropDown(ByVal sender As Object, ByVal e As System.EventArgs) Handles cb_Source_Database.DropDown
        If Connect_Source() = False Then Exit Sub
        cb_Source_Database.Items.Clear()


        For Each d As SQLDMO.Database2 In oSQLServer_Source.Databases
            cb_Source_Database.Items.Add(d.Name)
        Next
    End Sub


    Private Sub cb_Destination_Database_DropDown(ByVal sender As Object, ByVal e As System.EventArgs) Handles cb_Destination_Database.DropDown
        If Connect_Dest() = False Then Exit Sub
        cb_Destination_Database.Items.Clear()

        For Each d As SQLDMO.Database2 In oSQLServer_Dest.Databases
            cb_Destination_Database.Items.Add(d.Name)
        Next


    End Sub
End Class
