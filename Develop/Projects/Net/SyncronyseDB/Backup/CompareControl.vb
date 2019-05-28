Imports System.Text.RegularExpressions
Imports System.Xml.Serialization
Imports System.Globalization

Public Class CompareControl
    Inherits System.Windows.Forms.UserControl
#Region "Constants"
    Private Const WM_MOUSEWHEEL As Integer = &H20A
#Region "Images ID"
    Const imit_OK As Integer = 5
    Const imit_Er As Integer = 6
    Const imit_Empty As Integer = 0

    Const imit_Default As Integer = 60
    Const imit_Default_not_exist As Integer = 61
    Const imit_Default_diff As Integer = 62
    Const imit_Default_move As Integer = 63
    Const imit_Default_del As Integer = 64

    Const imit_SP As Integer = 1
    Const imit_SP_not_exist As Integer = 52
    Const imit_SP_diff As Integer = 53
    Const imit_SP_move As Integer = 54
    Const imit_SP_del As Integer = 55

    Const imit_UDF As Integer = 3
    Const imit_UDF_not_exist As Integer = 56
    Const imit_UDF_diff As Integer = 57
    Const imit_UDF_move As Integer = 58
    Const imit_UDF_del As Integer = 59


    Const imit_Folder As Integer = 18
    Const imit_Folder_diff As Integer = 11

    Const imit_Table As Integer = 2
    Const imit_Table_not_exist As Integer = 10
    Const imit_Table_diff As Integer = 12
    Const imit_Table_move As Integer = 40
    Const imit_Table_del As Integer = 49

    Const imit_Column As Integer = 7
    Const imit_Column_not_exist As Integer = 13
    Const imit_Column_diff As Integer = 14
    Const imit_Column_move As Integer = 35
    Const imit_Column_del As Integer = 44

    Const imit_PrKey As Integer = 15
    Const imit_PrKey_not_exist As Integer = 16
    Const imit_PrKey_diff As Integer = 17
    Const imit_PrKey_del As Integer = 46

    Const imit_Key As Integer = 19
    Const imit_Key_not_exist As Integer = 20
    Const imit_Key_diff As Integer = 21
    Const imit_Key_move As Integer = 37
    Const imit_Key_del As Integer = 47

    Const imit_Index As Integer = 8
    Const imit_Index_not_exist As Integer = 22
    Const imit_Index_diff As Integer = 23
    Const imit_Index_move As Integer = 38
    Const imit_Index_del As Integer = 45

    Const imit_Check As Integer = 24
    Const imit_Check_not_exist As Integer = 25
    Const imit_Check_diff As Integer = 26
    Const imit_Check_move As Integer = 34
    Const imit_Check_del As Integer = 43

    Const imit_Trigger As Integer = 9
    Const imit_Trigger_not_exist As Integer = 33
    Const imit_Trigger_diff As Integer = 32
    Const imit_Trigger_move As Integer = 41
    Const imit_Trigger_del As Integer = 50

    Const imit_Role As Integer = 27
    Const imit_Role_not_exist As Integer = 28
    Const imit_Role_diff As Integer = 29
    Const imit_Role_move As Integer = 39
    Const imit_Role_del As Integer = 48

    'Const imit_User As Integer = 27
    'Const imit_User_not_exist As Integer = 28
    'Const imit_User_diff As Integer = 29
    'Const imit_User_move As Integer = 39
    'Const imit_User_del As Integer = 48

    Const imit_View As Integer = 4
    Const imit_View_not_exist As Integer = 31
    Const imit_View_diff As Integer = 30
    Const imit_View_move As Integer = 42
    Const imit_View_del As Integer = 51

#End Region
    Const ind_User As Integer = 0
    Const ind_Default As Integer = 1
    Const ind_Table As Integer = 2
    Const ind_View As Integer = 3
    Const ind_SP As Integer = 4
    Const ind_UDF As Integer = 5

    Const text_Tables As String = "Tables"
#End Region

    Private oDB_Destination As SQLDMO.Database2
    Private oDB_Source As SQLDMO.Database2
    Private WithEvents oSQLServer_Source As SQLDMO.SQLServer = New SQLDMO.SQLServer
    Private WithEvents oSQLServer_Dest As SQLDMO.SQLServer = New SQLDMO.SQLServer


    Private Compare_Res As New Hashtable
    Private ImagToType As New Hashtable
    Private regexp_Alter As New Regex("(?<=(\r\n\s*)|(\A\s*))CREATE", RegexOptions.IgnoreCase Or RegexOptions.Singleline Or RegexOptions.Compiled)
    Private regexp_AlterSP As New Regex("(?<=(\r\n\s*)|(\A\s*))CREATE\s*?PROCEDURE", RegexOptions.IgnoreCase Or RegexOptions.Singleline Or RegexOptions.Compiled)
    Private Source_in_collapsing As Boolean = False
    Private Destination_in_collapsing As Boolean = False
    Private MouseWeel_from_source As Boolean = False
    Private MouseWeel_from_destination As Boolean = False
    Private In_Selecting As Boolean = False
    Private act As CompareAction
    Public Property action() As CompareAction
        Get
            Return act
        End Get
        Set(ByVal Value As CompareAction)
            act = Value
            If Value Is Nothing Then Return

            'Destination connect
            Try
                If Not oSQLServer_Dest.HostName Is Nothing Then
                    oSQLServer_Dest.DisConnect()
                End If
                oSQLServer_Dest.LoginTimeout = -1 '-1 is the ODBC default (60) seconds
                'Connect to the Server
                    With oSQLServer_Dest
                        'Use SQL Server Authentication
                        .LoginSecure = False
                        'Do not reconnect automatically
                    .AutoReConnect = True
                        'Use SQL Security
                    .Connect(act.DstCon.Server, act.DstCon.User, act.DstCon.Password)
                    End With

                oDB_Destination = oSQLServer_Dest.Databases.Item(act.DstCon.DataBase)

                If act.Options Is Nothing Then
                    act.Options = New Hashtable

                End If
            Catch ex As Exception
                MsgBox("Error: " & ex.Message & ex.HelpLink, vbOKOnly, ex.Source & ": Login Error")

            End Try

            ' Source connect
            Try


                If Not oSQLServer_Source.HostName Is Nothing Then
                    oSQLServer_Source.DisConnect()
                End If

                oSQLServer_Source.LoginTimeout = -1 '-1 is the ODBC default (60) seconds

                With oSQLServer_Source
                    .LoginSecure = False
                    .AutoReConnect = True
                    .Connect(act.SrcCon.Server, act.SrcCon.User, act.SrcCon.Password)
                End With

                oDB_Source = oSQLServer_Source.Databases.Item(act.SrcCon.DataBase)


            Catch ex As Exception
                MsgBox("Error: " & ex.Message & ex.HelpLink, vbOKOnly, ex.Source & ": Login Error")
            End Try


            If Src_Con.State = ConnectionState.Open Then
                Src_Con.Close()
            End If
            Src_Con.ConnectionString = "packet size=4096;user id=" & act.SrcCon.User & ";data source='" & act.SrcCon.Server & "';persist security info=True;initial catalog=" & act.SrcCon.DataBase & ";password=" & act.SrcCon.Password

            If Dest_Con.State = ConnectionState.Open Then
                Dest_Con.Close()
            End If
            Dest_Con.ConnectionString = "packet size=4096;user id=" & act.DstCon.User & ";data source='" & act.DstCon.Server & "';persist security info=True;initial catalog=" & act.DstCon.DataBase & ";password=" & act.DstCon.Password
            'Dest_Con.Open()

            Init()
        End Set
    End Property


#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        ImagToType.Add(imit_Default, imit_Default)
        ImagToType.Add(imit_Default_not_exist, imit_Default)
        ImagToType.Add(imit_Default_diff, imit_Default)
        ImagToType.Add(imit_Default_move, imit_Default)
        ImagToType.Add(imit_Default_del, imit_Default)

        ImagToType.Add(imit_SP, imit_SP)
        ImagToType.Add(imit_SP_not_exist, imit_SP)
        ImagToType.Add(imit_SP_diff, imit_SP)
        ImagToType.Add(imit_SP_move, imit_SP)
        ImagToType.Add(imit_SP_del, imit_SP)

        ImagToType.Add(imit_UDF, imit_SP)
        ImagToType.Add(imit_UDF_not_exist, imit_UDF)
        ImagToType.Add(imit_UDF_diff, imit_UDF)
        ImagToType.Add(imit_UDF_move, imit_UDF)
        ImagToType.Add(imit_UDF_del, imit_UDF)


        ImagToType.Add(imit_Folder, imit_Folder)
        ImagToType.Add(imit_Folder_diff, imit_Folder)

        ImagToType.Add(imit_Table, imit_Table)
        ImagToType.Add(imit_Table_not_exist, imit_Table)
        ImagToType.Add(imit_Table_diff, imit_Table)
        ImagToType.Add(imit_Table_move, imit_Table)
        ImagToType.Add(imit_Table_del, imit_Table)

        ImagToType.Add(imit_Column, imit_Column)
        ImagToType.Add(imit_Column_not_exist, imit_Column)
        ImagToType.Add(imit_Column_diff, imit_Column)
        ImagToType.Add(imit_Column_move, imit_Column)
        ImagToType.Add(imit_Column_del, imit_Column)

        ImagToType.Add(imit_PrKey, imit_PrKey)
        ImagToType.Add(imit_PrKey_not_exist, imit_PrKey)
        ImagToType.Add(imit_PrKey_diff, imit_PrKey)
        ImagToType.Add(imit_PrKey_del, imit_PrKey)

        ImagToType.Add(imit_Key, imit_Key)
        ImagToType.Add(imit_Key_not_exist, imit_Key)
        ImagToType.Add(imit_Key_diff, imit_Key)
        ImagToType.Add(imit_Key_move, imit_Key)
        ImagToType.Add(imit_Key_del, imit_Key)

        ImagToType.Add(imit_Index, imit_Index)
        ImagToType.Add(imit_Index_not_exist, imit_Index)
        ImagToType.Add(imit_Index_diff, imit_Index)
        ImagToType.Add(imit_Index_move, imit_Index)
        ImagToType.Add(imit_Index_del, imit_Index)

        ImagToType.Add(imit_Check, imit_Check)
        ImagToType.Add(imit_Check_not_exist, imit_Check)
        ImagToType.Add(imit_Check_diff, imit_Check)
        ImagToType.Add(imit_Check_move, imit_Check)
        ImagToType.Add(imit_Check_del, imit_Check)

        ImagToType.Add(imit_Trigger, imit_Trigger)
        ImagToType.Add(imit_Trigger_not_exist, imit_Trigger)
        ImagToType.Add(imit_Trigger_diff, imit_Trigger)
        ImagToType.Add(imit_Trigger_move, imit_Trigger)
        ImagToType.Add(imit_Trigger_del, imit_Trigger)

        ImagToType.Add(imit_Role, imit_Role)
        ImagToType.Add(imit_Role_not_exist, imit_Role)
        ImagToType.Add(imit_Role_diff, imit_Role)
        ImagToType.Add(imit_Role_move, imit_Role)
        ImagToType.Add(imit_Role_del, imit_Role)

        'ImagToType.Add(imit_User, imit_User)
        'ImagToType.Add(imit_User_not_exist, imit_User)
        'ImagToType.Add(imit_User_diff, imit_User)
        'ImagToType.Add(imit_User_move, imit_User)
        'ImagToType.Add(imit_User_del, imit_User)

        ImagToType.Add(imit_View, imit_View)
        ImagToType.Add(imit_View_not_exist, imit_View)
        ImagToType.Add(imit_View_diff, imit_View)
        ImagToType.Add(imit_View_move, imit_View)
        ImagToType.Add(imit_View_del, imit_View)
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
    Friend WithEvents b_DifFilter As System.Windows.Forms.Button
    Friend WithEvents b_Execute As System.Windows.Forms.Button
    Friend WithEvents tb_Compare_Res As System.Windows.Forms.TextBox
    Friend WithEvents b_Compare As System.Windows.Forms.Button
    Friend WithEvents gb_DB_Destination As System.Windows.Forms.GroupBox
    Friend WithEvents trv_DB_Destination As SyncronyseDB.SynTree
    Friend WithEvents gb_DB_Source As System.Windows.Forms.GroupBox
    Friend WithEvents trv_DB_Source As SyncronyseDB.SynTree
    Friend WithEvents imgl_Objects As System.Windows.Forms.ImageList
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    Friend WithEvents conmen_CompareProp As System.Windows.Forms.ContextMenu
    Friend WithEvents mi_Columns As System.Windows.Forms.MenuItem
    Friend WithEvents mi_Keys As System.Windows.Forms.MenuItem
    Friend WithEvents mi_Indexes As System.Windows.Forms.MenuItem
    Friend WithEvents mi_Checks As System.Windows.Forms.MenuItem
    Friend WithEvents mi_Triggers As System.Windows.Forms.MenuItem
    Friend WithEvents b_CheckAll As System.Windows.Forms.Button
    Friend WithEvents mi_tabPermissions As System.Windows.Forms.MenuItem
    Friend WithEvents b_NextDiff As System.Windows.Forms.Button
    Friend WithEvents Src_Con As System.Data.SqlClient.SqlConnection
    Friend WithEvents Dest_Con As System.Data.SqlClient.SqlConnection
    Friend WithEvents b_UnCheckAll As System.Windows.Forms.Button
    Friend WithEvents mi_spPermissions As System.Windows.Forms.MenuItem
    Friend WithEvents mi_udfPermissions As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem4 As System.Windows.Forms.MenuItem
    Friend WithEvents mi_viewPermissions As System.Windows.Forms.MenuItem
    Friend WithEvents mi_UDF As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem2 As System.Windows.Forms.MenuItem
    Friend WithEvents mi_SavesAsAct As System.Windows.Forms.MenuItem
    Friend WithEvents SFD As System.Windows.Forms.SaveFileDialog
    Friend WithEvents mi_View_Action As System.Windows.Forms.MenuItem
    Friend WithEvents b_Exec_SQL As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(CompareControl))
        Me.b_DifFilter = New System.Windows.Forms.Button
        Me.b_Execute = New System.Windows.Forms.Button
        Me.tb_Compare_Res = New System.Windows.Forms.TextBox
        Me.b_Compare = New System.Windows.Forms.Button
        Me.gb_DB_Destination = New System.Windows.Forms.GroupBox
        Me.trv_DB_Destination = New SyncronyseDB.SynTree
        Me.conmen_CompareProp = New System.Windows.Forms.ContextMenu
        Me.MenuItem1 = New System.Windows.Forms.MenuItem
        Me.mi_Columns = New System.Windows.Forms.MenuItem
        Me.mi_Keys = New System.Windows.Forms.MenuItem
        Me.mi_Indexes = New System.Windows.Forms.MenuItem
        Me.mi_Checks = New System.Windows.Forms.MenuItem
        Me.mi_Triggers = New System.Windows.Forms.MenuItem
        Me.mi_UDF = New System.Windows.Forms.MenuItem
        Me.MenuItem4 = New System.Windows.Forms.MenuItem
        Me.mi_tabPermissions = New System.Windows.Forms.MenuItem
        Me.mi_viewPermissions = New System.Windows.Forms.MenuItem
        Me.mi_spPermissions = New System.Windows.Forms.MenuItem
        Me.mi_udfPermissions = New System.Windows.Forms.MenuItem
        Me.MenuItem2 = New System.Windows.Forms.MenuItem
        Me.mi_SavesAsAct = New System.Windows.Forms.MenuItem
        Me.mi_View_Action = New System.Windows.Forms.MenuItem
        Me.imgl_Objects = New System.Windows.Forms.ImageList(Me.components)
        Me.gb_DB_Source = New System.Windows.Forms.GroupBox
        Me.trv_DB_Source = New SyncronyseDB.SynTree
        Me.b_CheckAll = New System.Windows.Forms.Button
        Me.b_NextDiff = New System.Windows.Forms.Button
        Me.Src_Con = New System.Data.SqlClient.SqlConnection
        Me.Dest_Con = New System.Data.SqlClient.SqlConnection
        Me.b_UnCheckAll = New System.Windows.Forms.Button
        Me.SFD = New System.Windows.Forms.SaveFileDialog
        Me.b_Exec_SQL = New System.Windows.Forms.Button
        Me.gb_DB_Destination.SuspendLayout()
        Me.gb_DB_Source.SuspendLayout()
        Me.SuspendLayout()
        '
        'b_DifFilter
        '
        Me.b_DifFilter.Image = CType(resources.GetObject("b_DifFilter.Image"), System.Drawing.Image)
        Me.b_DifFilter.Location = New System.Drawing.Point(488, 8)
        Me.b_DifFilter.Name = "b_DifFilter"
        Me.b_DifFilter.Size = New System.Drawing.Size(20, 20)
        Me.b_DifFilter.TabIndex = 13
        '
        'b_Execute
        '
        Me.b_Execute.Location = New System.Drawing.Point(568, 48)
        Me.b_Execute.Name = "b_Execute"
        Me.b_Execute.Size = New System.Drawing.Size(56, 23)
        Me.b_Execute.TabIndex = 12
        Me.b_Execute.Text = "Execute"
        '
        'tb_Compare_Res
        '
        Me.tb_Compare_Res.Location = New System.Drawing.Point(488, 80)
        Me.tb_Compare_Res.Multiline = True
        Me.tb_Compare_Res.Name = "tb_Compare_Res"
        Me.tb_Compare_Res.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.tb_Compare_Res.Size = New System.Drawing.Size(328, 296)
        Me.tb_Compare_Res.TabIndex = 11
        Me.tb_Compare_Res.Text = ""
        '
        'b_Compare
        '
        Me.b_Compare.Location = New System.Drawing.Point(488, 48)
        Me.b_Compare.Name = "b_Compare"
        Me.b_Compare.TabIndex = 10
        Me.b_Compare.Text = "Compare"
        '
        'gb_DB_Destination
        '
        Me.gb_DB_Destination.Controls.Add(Me.trv_DB_Destination)
        Me.gb_DB_Destination.Dock = System.Windows.Forms.DockStyle.Left
        Me.gb_DB_Destination.Location = New System.Drawing.Point(240, 0)
        Me.gb_DB_Destination.Name = "gb_DB_Destination"
        Me.gb_DB_Destination.Size = New System.Drawing.Size(240, 384)
        Me.gb_DB_Destination.TabIndex = 9
        Me.gb_DB_Destination.TabStop = False
        Me.gb_DB_Destination.Text = "Destination"
        '
        'trv_DB_Destination
        '
        Me.trv_DB_Destination.ContextMenu = Me.conmen_CompareProp
        Me.trv_DB_Destination.ImageList = Me.imgl_Objects
        Me.trv_DB_Destination.ItemHeight = 20
        Me.trv_DB_Destination.Location = New System.Drawing.Point(8, 16)
        Me.trv_DB_Destination.Name = "trv_DB_Destination"
        Me.trv_DB_Destination.Nodes.AddRange(New System.Windows.Forms.TreeNode() {New System.Windows.Forms.TreeNode("Users", 18, 18), New System.Windows.Forms.TreeNode("Defaults", 18, 18), New System.Windows.Forms.TreeNode("Tables", 18, 18), New System.Windows.Forms.TreeNode("View", 18, 18), New System.Windows.Forms.TreeNode("SP", 18, 18), New System.Windows.Forms.TreeNode("UDF", 18, 18)})
        Me.trv_DB_Destination.PathSeparator = "."
        Me.trv_DB_Destination.Size = New System.Drawing.Size(224, 352)
        Me.trv_DB_Destination.TabIndex = 0
        '
        'conmen_CompareProp
        '
        Me.conmen_CompareProp.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem1, Me.mi_UDF, Me.MenuItem4, Me.MenuItem2, Me.mi_SavesAsAct, Me.mi_View_Action})
        '
        'MenuItem1
        '
        Me.MenuItem1.Index = 0
        Me.MenuItem1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mi_Columns, Me.mi_Keys, Me.mi_Indexes, Me.mi_Checks, Me.mi_Triggers})
        Me.MenuItem1.Text = "Tables"
        '
        'mi_Columns
        '
        Me.mi_Columns.Checked = True
        Me.mi_Columns.Index = 0
        Me.mi_Columns.Text = "Columns"
        '
        'mi_Keys
        '
        Me.mi_Keys.Checked = True
        Me.mi_Keys.Index = 1
        Me.mi_Keys.Text = "Keys"
        '
        'mi_Indexes
        '
        Me.mi_Indexes.Checked = True
        Me.mi_Indexes.Index = 2
        Me.mi_Indexes.Text = "Indexes"
        '
        'mi_Checks
        '
        Me.mi_Checks.Checked = True
        Me.mi_Checks.Index = 3
        Me.mi_Checks.Text = "Checks"
        '
        'mi_Triggers
        '
        Me.mi_Triggers.Checked = True
        Me.mi_Triggers.Index = 4
        Me.mi_Triggers.Text = "Triggers"
        '
        'mi_UDF
        '
        Me.mi_UDF.Checked = True
        Me.mi_UDF.Index = 1
        Me.mi_UDF.Text = "UDF"
        '
        'MenuItem4
        '
        Me.MenuItem4.Index = 2
        Me.MenuItem4.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mi_tabPermissions, Me.mi_viewPermissions, Me.mi_spPermissions, Me.mi_udfPermissions})
        Me.MenuItem4.Text = "Permissions"
        '
        'mi_tabPermissions
        '
        Me.mi_tabPermissions.Checked = True
        Me.mi_tabPermissions.Index = 0
        Me.mi_tabPermissions.Text = "Table"
        '
        'mi_viewPermissions
        '
        Me.mi_viewPermissions.Checked = True
        Me.mi_viewPermissions.Index = 1
        Me.mi_viewPermissions.Text = "View"
        '
        'mi_spPermissions
        '
        Me.mi_spPermissions.Checked = True
        Me.mi_spPermissions.Index = 2
        Me.mi_spPermissions.Text = "SP"
        '
        'mi_udfPermissions
        '
        Me.mi_udfPermissions.Checked = True
        Me.mi_udfPermissions.Index = 3
        Me.mi_udfPermissions.Text = "UDF"
        '
        'MenuItem2
        '
        Me.MenuItem2.Index = 3
        Me.MenuItem2.Text = "-"
        '
        'mi_SavesAsAct
        '
        Me.mi_SavesAsAct.Index = 4
        Me.mi_SavesAsAct.Text = "Save as Action"
        '
        'mi_View_Action
        '
        Me.mi_View_Action.Index = 5
        Me.mi_View_Action.Text = "View Action"
        '
        'imgl_Objects
        '
        Me.imgl_Objects.ImageSize = New System.Drawing.Size(16, 16)
        Me.imgl_Objects.ImageStream = CType(resources.GetObject("imgl_Objects.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imgl_Objects.TransparentColor = System.Drawing.Color.Transparent
        '
        'gb_DB_Source
        '
        Me.gb_DB_Source.Controls.Add(Me.trv_DB_Source)
        Me.gb_DB_Source.Dock = System.Windows.Forms.DockStyle.Left
        Me.gb_DB_Source.Location = New System.Drawing.Point(0, 0)
        Me.gb_DB_Source.Name = "gb_DB_Source"
        Me.gb_DB_Source.Size = New System.Drawing.Size(240, 384)
        Me.gb_DB_Source.TabIndex = 8
        Me.gb_DB_Source.TabStop = False
        Me.gb_DB_Source.Text = "Source"
        '
        'trv_DB_Source
        '
        Me.trv_DB_Source.CheckBoxes = True
        Me.trv_DB_Source.ContextMenu = Me.conmen_CompareProp
        Me.trv_DB_Source.ImageList = Me.imgl_Objects
        Me.trv_DB_Source.ItemHeight = 20
        Me.trv_DB_Source.Location = New System.Drawing.Point(8, 16)
        Me.trv_DB_Source.Name = "trv_DB_Source"
        Me.trv_DB_Source.Nodes.AddRange(New System.Windows.Forms.TreeNode() {New System.Windows.Forms.TreeNode("Users", 18, 18), New System.Windows.Forms.TreeNode("Defaults", 18, 18), New System.Windows.Forms.TreeNode("Tables", 18, 18), New System.Windows.Forms.TreeNode("View", 18, 18), New System.Windows.Forms.TreeNode("SP", 18, 18), New System.Windows.Forms.TreeNode("UDF", 18, 18)})
        Me.trv_DB_Source.PathSeparator = "."
        Me.trv_DB_Source.Size = New System.Drawing.Size(224, 352)
        Me.trv_DB_Source.TabIndex = 0
        '
        'b_CheckAll
        '
        Me.b_CheckAll.Image = CType(resources.GetObject("b_CheckAll.Image"), System.Drawing.Image)
        Me.b_CheckAll.Location = New System.Drawing.Point(512, 8)
        Me.b_CheckAll.Name = "b_CheckAll"
        Me.b_CheckAll.Size = New System.Drawing.Size(20, 20)
        Me.b_CheckAll.TabIndex = 15
        '
        'b_NextDiff
        '
        Me.b_NextDiff.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.b_NextDiff.Location = New System.Drawing.Point(640, 8)
        Me.b_NextDiff.Name = "b_NextDiff"
        Me.b_NextDiff.Size = New System.Drawing.Size(20, 20)
        Me.b_NextDiff.TabIndex = 16
        Me.b_NextDiff.Text = ">"
        '
        'b_UnCheckAll
        '
        Me.b_UnCheckAll.Image = CType(resources.GetObject("b_UnCheckAll.Image"), System.Drawing.Image)
        Me.b_UnCheckAll.Location = New System.Drawing.Point(536, 8)
        Me.b_UnCheckAll.Name = "b_UnCheckAll"
        Me.b_UnCheckAll.Size = New System.Drawing.Size(20, 20)
        Me.b_UnCheckAll.TabIndex = 17
        '
        'b_Exec_SQL
        '
        Me.b_Exec_SQL.Location = New System.Drawing.Point(632, 48)
        Me.b_Exec_SQL.Name = "b_Exec_SQL"
        Me.b_Exec_SQL.Size = New System.Drawing.Size(64, 24)
        Me.b_Exec_SQL.TabIndex = 18
        Me.b_Exec_SQL.Text = "Exec SQL"
        '
        'CompareControl
        '
        Me.Controls.Add(Me.b_Exec_SQL)
        Me.Controls.Add(Me.b_UnCheckAll)
        Me.Controls.Add(Me.b_NextDiff)
        Me.Controls.Add(Me.b_CheckAll)
        Me.Controls.Add(Me.b_DifFilter)
        Me.Controls.Add(Me.b_Execute)
        Me.Controls.Add(Me.tb_Compare_Res)
        Me.Controls.Add(Me.b_Compare)
        Me.Controls.Add(Me.gb_DB_Destination)
        Me.Controls.Add(Me.gb_DB_Source)
        Me.Name = "CompareControl"
        Me.Size = New System.Drawing.Size(832, 384)
        Me.gb_DB_Destination.ResumeLayout(False)
        Me.gb_DB_Source.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

#Region "Compare Methods"

    Private Sub Set_Diff(ByRef node As TreeNode)
        Select Case node.ImageIndex
            Case Me.imit_Default
                node.ImageIndex = Me.imit_Default_diff
                node.SelectedImageIndex = Me.imit_Default_diff
                Set_Diff(node.Parent)

            Case Me.imit_SP
                node.ImageIndex = Me.imit_SP_diff
                node.SelectedImageIndex = Me.imit_SP_diff
                Set_Diff(node.Parent)

            Case Me.imit_UDF
                node.ImageIndex = Me.imit_UDF_diff
                node.SelectedImageIndex = Me.imit_UDF_diff
                Set_Diff(node.Parent)

            Case Me.imit_Folder
                node.ImageIndex = Me.imit_Folder_diff
                node.SelectedImageIndex = Me.imit_Folder_diff
                If Not node.Parent Is Nothing Then
                    Set_Diff(node.Parent)
                End If
            Case Me.imit_Table
                node.ImageIndex = Me.imit_Table_diff
                node.SelectedImageIndex = Me.imit_Table_diff
                Set_Diff(node.Parent)

            Case Me.imit_Column
                node.ImageIndex = Me.imit_Column_diff
                node.SelectedImageIndex = Me.imit_Column_diff
                Set_Diff(node.Parent)

            Case Me.imit_Key
                node.ImageIndex = Me.imit_Key_diff
                node.SelectedImageIndex = Me.imit_Key_diff
                Set_Diff(node.Parent)

            Case Me.imit_Index
                node.ImageIndex = Me.imit_Index_diff
                node.SelectedImageIndex = Me.imit_Index_diff
                Set_Diff(node.Parent)

            Case Me.imit_Check
                node.ImageIndex = Me.imit_Check_diff
                node.SelectedImageIndex = Me.imit_Check_diff
                Set_Diff(node.Parent)

            Case Me.imit_Role
                node.ImageIndex = Me.imit_Role_diff
                node.SelectedImageIndex = Me.imit_Role_diff
                Set_Diff(node.Parent)

                'Case Me.imit_User
                '    node.ImageIndex = Me.imit_User_diff
                '    node.SelectedImageIndex = Me.imit_User_diff
                '    Set_Diff(node.Parent)

            Case Me.imit_View
                node.ImageIndex = Me.imit_View_diff
                node.SelectedImageIndex = Me.imit_View_diff
                Set_Diff(node.Parent)
            Case Else
                If Not node.Parent Is Nothing Then
                    Set_Diff(node.Parent)
                End If
        End Select

    End Sub
    Private Sub b_DifFilter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles b_DifFilter.Click
        'trv_DB_Destination.Nodes.


    End Sub
    Private Sub Show_Compare_Res(ByRef node As TreeNode)
        Dim key As String
        Dim n As TreeNode
        Me.tb_Compare_Res.Text = Me.Compare_Res.Item(node.FullPath)

    End Sub

    Private Function Compare_Properties(ByRef oSource As SQLDMO.Properties, ByVal Source_Table_Node As TreeNode, ByRef oDestination As SQLDMO.Properties, ByVal Destination_Table_Node As TreeNode) As String
        Dim i As Integer
        Compare_Properties = ""
        Dim FillFactor As Boolean = True
        FillFactor = act.Options("FillFactor")
        ' Compare properties
        For i = 1 To oSource.Count
            If oSource.Item(i).Name <> "ID" And oSource.Item(i).Name <> "SpaceUsed" And oSource.Item(i).Get Then
                If act.Options.ContainsKey(oSource.Item(i).Name) = False Or (act.Options.ContainsKey(oSource.Item(i).Name) And act.Options(oSource.Item(i).Name) = True) Then
                    If oSource.Item(i).Value <> oDestination.Item(i).Value Then

                        Compare_Properties &= oSource.Item(i).Name & vbTab & oSource.Item(i).Value & vbTab & oDestination.Item(i).Value & vbCrLf
                        Source_Table_Node.Nodes.Add(oSource.Item(i).Name & "=" & oSource.Item(i).Value)
                        Destination_Table_Node.Nodes.Add(oSource.Item(i).Name & "=" & oDestination.Item(i).Value)
                    End If
                End If
            End If
        Next

        If Len(Compare_Properties) > 0 Then
            Set_Diff(Source_Table_Node)
            Set_Diff(Destination_Table_Node)
        End If
    End Function

#Region "Table"
    Private Function GetStr_Column(ByRef oColumn As SQLDMO.Column2, ByRef col_info As SQLDMO.QueryResults2) As String

        Dim str As String

        GetStr_Column = oColumn.Name & " " & oColumn.Datatype
        If oColumn.Datatype.IndexOf("char") > -1 Then
            GetStr_Column &= "(" & oColumn.Length & ") "
        Else
            If oColumn.Datatype = "decimal" Or oColumn.Datatype = "numeric" Then
                GetStr_Column &= "(" & oColumn.NumericPrecision & "," & oColumn.NumericScale & ") "
            End If
        End If

        If oColumn.AllowNulls Then
            GetStr_Column &= " NULL "
        Else
            GetStr_Column &= " NOT NULL "

            Dim i As Integer
            For i = 1 To col_info.Rows
                If col_info.GetColumnString(i, 1) = oColumn.Name Then
                    str = col_info.GetColumnString(i, 16)
                    If Not str Is Nothing Then
                        GetStr_Column &= "DEFAULT " & col_info.GetColumnString(i, 16)
                    End If
                    Exit For
                End If
            Next
        End If

        If Not oColumn.Default Is Nothing Then 'And oColumn.Default.Length Then
            GetStr_Column &= " DEFAULT " & oColumn.Default
        End If

    End Function
    Private Sub Print_Column(ByRef oColumn As SQLDMO.Column2, ByRef node As TreeNode)

        node.Nodes.Add("AllowNulls=" & oColumn.AllowNulls)
        node.Nodes.Add("Datatype=" & oColumn.Datatype)
        node.Nodes.Add("PhysicalDatatype=" & oColumn.PhysicalDatatype)

        If (oColumn.PhysicalDatatype = "numeric" Or oColumn.PhysicalDatatype = "decimal") Then
            node.Nodes.Add("NumericPrecision=" & oColumn.NumericPrecision)
            node.Nodes.Add("NumericScale=" & oColumn.NumericScale)
            node.Nodes.Add("NotForRepl=" & oColumn.NotForRepl)
        Else
            If (oColumn.PhysicalDatatype.IndexOf("char") > -1) Then
                node.Nodes.Add("FullTextIndex=" & oColumn.FullTextIndex)
                node.Nodes.Add("Collation=" & oColumn.Collation)
            End If
        End If
        node.Nodes.Add("Length=" & oColumn.Length)
        node.Nodes.Add("Default=" & oColumn.Default)
        node.Nodes.Add("InPrimaryKey=" & oColumn.InPrimaryKey)

        If (oColumn.PhysicalDatatype.IndexOf("char") = -1) Then
            node.Nodes.Add("Identity=" & oColumn.Identity)
            If (oColumn.Identity = True) Then
                node.Nodes.Add("IdentityIncrement=" & oColumn.IdentityIncrement)
                node.Nodes.Add("IdentitySeed=" & oColumn.IdentitySeed)
            End If
        End If

        node.Nodes.Add("IsComputed=" & oColumn.IsComputed)
        If (oColumn.IsComputed = True) Then
            node.Nodes.Add("ComputedText=" & oColumn.ComputedText)
        End If
        node.Nodes.Add("Rule=" & oColumn.Rule)

    End Sub
    Protected Friend Function Compare_Column(ByRef oSource As SQLDMO.Column2, ByVal Source_Table_Node As TreeNode, ByRef oDestination As SQLDMO.Column2, ByVal Destination_Table_Node As TreeNode, ByRef bindRule As String) As Boolean
        Compare_Column = True
        If oSource.AllowNulls <> oDestination.AllowNulls Then
            Compare_Column = False
            Source_Table_Node.Nodes.Add("AllowNulls=" & oSource.AllowNulls)
            Destination_Table_Node.Nodes.Add("AllowNulls=" & oDestination.AllowNulls)
        End If

        If oSource.Collation <> oDestination.Collation Then
            Compare_Column = False
            Source_Table_Node.Nodes.Add("Collation=" & oSource.Collation)
            Destination_Table_Node.Nodes.Add("Collation=" & oDestination.Collation)
        End If

        If oSource.ComputedText <> oDestination.ComputedText Then
            Compare_Column = False
            Source_Table_Node.Nodes.Add("ComputedText=" & oSource.ComputedText)
            Destination_Table_Node.Nodes.Add("ComputedText=" & oDestination.ComputedText)
        End If

        If oSource.Datatype <> oDestination.Datatype Then
            Compare_Column = False
            Source_Table_Node.Nodes.Add("Datatype=" & oSource.Datatype)
            Destination_Table_Node.Nodes.Add("Datatype=" & oDestination.Datatype)
        End If

        If oSource.Default <> oDestination.Default Then
            Compare_Column = False
            Source_Table_Node.Nodes.Add("Default=" & oSource.Default)
            Destination_Table_Node.Nodes.Add("Default=" & oDestination.Default)
        End If

        If oSource.FullTextIndex <> oDestination.FullTextIndex Then
            Compare_Column = False
            Source_Table_Node.Nodes.Add("FullTextIndex=" & oSource.FullTextIndex)
            Destination_Table_Node.Nodes.Add("FullTextIndex=" & oDestination.FullTextIndex)
        End If

        If oSource.Identity <> oDestination.Identity Then
            Compare_Column = False
            Source_Table_Node.Nodes.Add("Identity=" & oSource.Identity)
            Destination_Table_Node.Nodes.Add("Identity=" & oDestination.Identity)
        End If

        If oSource.IdentityIncrement <> oDestination.IdentityIncrement Then
            Compare_Column = False
            Source_Table_Node.Nodes.Add("IdentityIncrement=" & oSource.IdentityIncrement)
            Destination_Table_Node.Nodes.Add("IdentityIncrement=" & oDestination.IdentityIncrement)
        End If

        If oSource.IdentitySeed <> oDestination.IdentitySeed Then
            Compare_Column = False
            Source_Table_Node.Nodes.Add("IdentitySeed=" & oSource.IdentitySeed)
            Destination_Table_Node.Nodes.Add("IdentitySeed=" & oDestination.IdentitySeed)
        End If

        'If oSource.InPrimaryKey <> oDestination.InPrimaryKey Then
        '    Compare_Column = False
        '    Source_Table_Node.Nodes.Add("InPrimaryKey=" & oSource.InPrimaryKey)
        '    Destination_Table_Node.Nodes.Add("InPrimaryKey=" & oDestination.InPrimaryKey)
        'End If

        If oSource.IsComputed <> oDestination.IsComputed Then
            Compare_Column = False
            Source_Table_Node.Nodes.Add("IsComputed=" & oSource.IsComputed)
            Destination_Table_Node.Nodes.Add("IsComputed=" & oDestination.IsComputed)
        End If

        If oSource.Length <> oDestination.Length Then
            Compare_Column = False
            Source_Table_Node.Nodes.Add("Length=" & oSource.Length)
            Destination_Table_Node.Nodes.Add("Length=" & oDestination.Length)
        End If

        If oSource.NotForRepl <> oDestination.NotForRepl Then
            Compare_Column = False
            Source_Table_Node.Nodes.Add("NotForRepl=" & oSource.NotForRepl)
            Destination_Table_Node.Nodes.Add("NotForRepl=" & oDestination.NotForRepl)
        End If

        If oSource.NumericPrecision <> oDestination.NumericPrecision Then
            Compare_Column = False
            Source_Table_Node.Nodes.Add("NumericPrecision=" & oSource.NumericPrecision)
            Destination_Table_Node.Nodes.Add("NumericPrecision=" & oDestination.NumericPrecision)
        End If

        If oSource.NumericScale <> oDestination.NumericScale Then
            Compare_Column = False
            Source_Table_Node.Nodes.Add("NumericScale=" & oSource.NumericScale)
            Destination_Table_Node.Nodes.Add("NumericScale=" & oDestination.NumericScale)
        End If

        If oSource.PhysicalDatatype <> oDestination.PhysicalDatatype Then
            Compare_Column = False
            Source_Table_Node.Nodes.Add("PhysicalDatatype=" & oSource.PhysicalDatatype)
            Destination_Table_Node.Nodes.Add("PhysicalDatatype=" & oDestination.PhysicalDatatype)
        End If

        If oSource.Rule <> oDestination.Rule Then
            Compare_Column = False
            Source_Table_Node.Nodes.Add("Rule=" & oSource.Rule)
            Destination_Table_Node.Nodes.Add("Rule=" & oDestination.Rule)
            bindRule = "USE " & oDB_Destination.Name & vbCrLf & "EXEC sp_binderule '" & oSource.Rule & "' , "

        End If

        If Compare_Column = False Then
            Set_Diff(Source_Table_Node)
            Set_Diff(Destination_Table_Node)
        End If

    End Function
    Private Sub Compare_Columns(ByRef oSource As SQLDMO.Table2, ByVal Source_Table_Node As TreeNode, ByRef oDestination As SQLDMO.Table2, ByVal Destination_Table_Node As TreeNode)
        ' ÃÂÚÓ‰ Ò‡‚ÌË‚‡ÂÚ ÍÓÎÓÌÍË
        Dim oColumn As SQLDMO.Column2
        Dim oColumn1 As SQLDMO.Column2
        Dim Node As TreeNode, Node1 As TreeNode
        Dim n As Integer, n1 As Integer, sc_ind As Integer, dc_ind As Integer
        Dim key As String, sql As String, bindRule As String

        ' ADD COLUMNS
        Node = New TreeNode("Columns")
        Node.ImageIndex = Me.imit_Folder
        Node.SelectedImageIndex = Me.imit_Folder
        sc_ind = Source_Table_Node.Nodes.Add(Node)

        Node = New TreeNode("Columns")
        Node.ImageIndex = Me.imit_Folder
        Node.SelectedImageIndex = Me.imit_Folder
        dc_ind = Destination_Table_Node.Nodes.Add(Node)

        Dim Destination As New Hashtable
        Dim col_info As SQLDMO.QueryResults2 = oDB_Source.ExecuteWithResults("exec sp_MShelpcolumns N'" & oSource.Name & "', null, 'col_name', 1")

        For Each oColumn In oDestination.Columns
            Destination.Add(oColumn.Name, 0)
        Next

        For Each oColumn In oSource.Columns
            If (Destination.ContainsKey(oColumn.Name)) Then
                'Exists in both base
                Destination.Item(oColumn.Name) = 1

                Node = New TreeNode(oColumn.Name)
                Node.ImageIndex = Me.imit_Column
                Node.SelectedImageIndex = Me.imit_Column
                n = Source_Table_Node.Nodes(sc_ind).Nodes.Add(Node)

                Node1 = New TreeNode(oColumn.Name)
                Node1.ImageIndex = Me.imit_Column
                Node1.SelectedImageIndex = Me.imit_Column
                n1 = Destination_Table_Node.Nodes(dc_ind).Nodes.Add(Node1)

                oColumn1 = oDestination.Columns.Item(oColumn.Name)
                bindRule = ""
                If (Compare_Column(oColumn, Node, oColumn1, Node1, bindRule) = False) Then
                    sql = "ALTER TABLE  " & oSource.Name & vbCrLf & _
                    "ALTER COLUMN " & GetStr_Column(oColumn, col_info)
                    If (bindRule.Length) Then
                        sql &= bindRule & "'[" & oSource.Name & "].[" & oColumn.Name & "]'" & vbCrLf
                    End If

                    Me.Compare_Res.Add(Node.FullPath, sql)
                End If

            Else
                'Exists in source base
                Node = New TreeNode(oColumn.Name)
                Node.SelectedImageIndex = imit_Column_move
                Node.ImageIndex = imit_Column_move
                n = Source_Table_Node.Nodes(sc_ind).Nodes.Add(Node)
                Me.Print_Column(oColumn, Node)

                Node1 = New TreeNode(oColumn.Name)
                Node1.ImageIndex = Me.imit_Column_not_exist
                Node1.SelectedImageIndex = Me.imit_Column_not_exist
                n1 = Destination_Table_Node.Nodes(dc_ind).Nodes.Add(Node1)
                Me.Print_Column(oColumn, Node1)
                sql = "ALTER TABLE  " & oSource.Name & vbCrLf & _
                "ADD " & GetStr_Column(oColumn, col_info)

                Me.Compare_Res.Add(Node.FullPath, sql)

                Set_Diff(Node.Parent)
                Set_Diff(Node1.Parent)
            End If

        Next

        For Each key In Destination.Keys
            If Destination.Item(key) = 0 Then
                'Exists in destination base
                Node = New TreeNode(key)
                Node.ImageIndex = Me.imit_Column_del
                Node.SelectedImageIndex = Me.imit_Column_del
                n = Destination_Table_Node.Nodes(dc_ind).Nodes.Add(Node)
                Me.Print_Column(oDestination.Columns.Item(key), Node)

                Node1 = New TreeNode(key)
                Node1.ImageIndex = Me.imit_Column_not_exist
                Node1.SelectedImageIndex = Me.imit_Column_not_exist
                n1 = Source_Table_Node.Nodes(dc_ind).Nodes.Add(Node1)
                Me.Print_Column(oDestination.Columns.Item(key), Node1)

                'ALTER TABLE %TABLE_NAME% DROP COLUMN %COLUMN_NAME%
                Me.Compare_Res.Add(Node.FullPath, "ALTER TABLE " & oDestination.Name & " DROP COLUMN " & key & vbCrLf & "GO")

                Set_Diff(Node.Parent)
                Set_Diff(Node1.Parent)
            End If
        Next

    End Sub

    'Private Sub Compare_PrKey(ByVal Prefix As String, ByRef oSource As SQLDMO.Table2, ByRef Source_Table_Node As TreeNode, ByRef oDestination As SQLDMO.Table2, ByRef Destination_Table_Node As TreeNode)
    '    ' ÃÂÚÓ‰ Ò‡‚ÌË‚‡ÂÚ ÍÓÎÓÌÍË
    '    Dim sql As String, sql1 As String
    '    Dim Node As TreeNode, Node1 As TreeNode
    '    Dim n As Integer, n1 As Integer

    '    If (oSource.PrimaryKey Is Nothing) Then
    '        If (oDestination.PrimaryKey Is Nothing) Then
    '            'don't exists in both
    '            Exit Sub
    '        Else
    '            'exist in destination
    '            Node = New TreeNode(oDestination.PrimaryKey.Name)
    '            Node.ImageIndex = Me.imit_PrKey
    '            Node.SelectedImageIndex = Me.imit_PrKey
    '            n = Destination_Table_Node.Nodes.Add(Node)
    '            'Me.Print_Column(oDestination.Columns.Item(key), Node)

    '            Node1 = New TreeNode(oDestination.PrimaryKey.Name)
    '            Node1.ImageIndex = Me.imit_PrKey_not_exist
    '            Node1.SelectedImageIndex = Me.imit_PrKey_not_exist
    '            n1 = Source_Table_Node.Nodes.Add(Node1)
    '            'ALTER TABLE %TABLE_NAME% DROP CONSTRAINT  %CONSTRAINT_NAME%
    '            Me.Compare_Res.Add(Prefix & "_" & n, "ALTER TABLE " & oDestination.Name & " DROP CONSTRAINT " & oDestination.PrimaryKey.Name & vbCrLf & "GO")

    '            Set_Diff(Node.Parent)
    '            Set_Diff(Node1.Parent)
    '        End If
    '    Else
    '        If (oDestination.PrimaryKey Is Nothing) Then
    '            'exist in source
    '            Node = New TreeNode(oSource.PrimaryKey.Name)
    '            Node.ImageIndex = Me.imit_PrKey
    '            Node.SelectedImageIndex = Me.imit_PrKey
    '            n = Source_Table_Node.Nodes.Add(Node)
    '            oSource.PrimaryKey.Script()

    '            'Me.Print_Column(oColumn, Node)

    '            Node1 = New TreeNode(oSource.PrimaryKey.Name)
    '            Node1.ImageIndex = Me.imit_PrKey_not_exist
    '            Node1.SelectedImageIndex = Me.imit_PrKey_not_exist
    '            n1 = Destination_Table_Node.Nodes.Add(Node1)

    '            Me.Compare_Res.Add(Prefix & "_" & n, oSource.PrimaryKey.Script())

    '            Set_Diff(Node.Parent)
    '            Set_Diff(Node1.Parent)
    '        Else
    '            'exist in both
    '            sql = oSource.PrimaryKey.Script
    '            sql1 = oDestination.PrimaryKey.Script
    '            If sql = sql1 Then
    '                Node = New TreeNode(oSource.PrimaryKey.Name)
    '                Node.ImageIndex = Me.imit_PrKey
    '                Node.SelectedImageIndex = Me.imit_PrKey
    '                n = Source_Table_Node.Nodes.Add(Node)

    '                Node1 = New TreeNode(oSource.PrimaryKey.Name)
    '                Node1.ImageIndex = Me.imit_PrKey
    '                Node1.SelectedImageIndex = Me.imit_PrKey
    '                n1 = Destination_Table_Node.Nodes.Add(Node1)
    '            Else
    '                Node = New TreeNode(oSource.PrimaryKey.Name)
    '                Node.ImageIndex = Me.imit_PrKey_diff
    '                Node.SelectedImageIndex = Me.imit_PrKey_diff
    '                n = Source_Table_Node.Nodes.Add(Node)

    '                Node1 = New TreeNode(oSource.PrimaryKey.Name)
    '                Node1.ImageIndex = Me.imit_PrKey_diff
    '                Node1.SelectedImageIndex = Me.imit_PrKey_diff
    '                n1 = Destination_Table_Node.Nodes.Add(Node1)

    '                Me.Compare_Res.Add(Prefix & "_" & n, oSource.PrimaryKey.Script())
    '                Set_Diff(Node.Parent)
    '                Set_Diff(Node1.Parent)
    '            End If


    '            'Me.Compare_Res.Add(Prefix & "_" & n, "Add Prkey" & oSource.PrimaryKey.Name)
    '        End If
    '    End If


    'End Sub

    Private Sub Print_Key(ByRef oKey As SQLDMO.Key, ByVal node As TreeNode)
        Dim i As Integer
        Dim n As TreeNode, n1 As TreeNode

        n = New TreeNode("Key Columns")
        n.ImageIndex = Me.imit_Folder
        n.SelectedImageIndex = Me.imit_Folder
        node.Nodes.Add(n)
        For i = 1 To oKey.KeyColumns.Count
            n1 = New TreeNode(oKey.KeyColumns.Item(i))
            n1.ImageIndex = Me.imit_Column
            n1.SelectedImageIndex = Me.imit_Column
            n.Nodes.Add(n1)
        Next


        n = New TreeNode("Referenced Columns")
        n.ImageIndex = Me.imit_Folder
        n.SelectedImageIndex = Me.imit_Folder
        node.Nodes.Add(n)
        For i = 1 To oKey.ReferencedColumns.Count
            n1 = New TreeNode(oKey.ReferencedColumns.Item(i))
            n1.ImageIndex = Me.imit_Column
            n1.SelectedImageIndex = Me.imit_Column
            n.Nodes.Add(n1)
        Next

        For i = 1 To oKey.Properties.Count
            If oKey.Properties.Item(i).Get Then
                node.Nodes.Add(oKey.Properties.Item(i).Name & "=" & oKey.Properties.Item(i).Value)
            End If
        Next

        'node.Nodes.Add("AllowNulls=" & oColumn.AllowNulls)
        'node.Nodes.Add("Datatype=" & oColumn.Datatype)
        'node.Nodes.Add("PhysicalDatatype=" & oColumn.PhysicalDatatype)
        'If (oColumn.PhysicalDatatype = "numeric" Or oColumn.PhysicalDatatype = "decimal") Then
        '    node.Nodes.Add("NumericPrecision=" & oColumn.NumericPrecision)
        '    node.Nodes.Add("NumericScale=" & oColumn.NumericScale)
        '    node.Nodes.Add("NotForRepl=" & oColumn.NotForRepl)
        'Else
        '    If (oColumn.PhysicalDatatype.IndexOf("char") > -1) Then
        '        node.Nodes.Add("FullTextIndex=" & oColumn.FullTextIndex)
        '        node.Nodes.Add("Collation=" & oColumn.Collation)
        '    End If
        'End If
        'node.Nodes.Add("Length=" & oColumn.Length)
        'node.Nodes.Add("Default=" & oColumn.Default)
        'node.Nodes.Add("InPrimaryKey=" & oColumn.InPrimaryKey)

        'node.Nodes.Add("Identity=" & oColumn.Identity)
        'If (oColumn.Identity = True) Then
        '    node.Nodes.Add("IdentityIncrement=" & oColumn.IdentityIncrement)
        '    node.Nodes.Add("IdentitySeed=" & oColumn.IdentitySeed)
        'End If

        'node.Nodes.Add("IsComputed=" & oColumn.IsComputed)
        'If (oColumn.IsComputed = True) Then
        '    node.Nodes.Add("ComputedText=" & oColumn.ComputedText)
        'End If
        'node.Nodes.Add("Rule=" & oColumn.Rule)

    End Sub
    Private Sub Compare_Key(ByVal tabName As String, ByRef oSource As SQLDMO.Key, ByVal Source_Table_Node As TreeNode, ByRef oDestination As SQLDMO.Key, ByVal Destination_Table_Node As TreeNode)
        Dim e As Boolean
        Dim res As String, name As String
        Dim i As Integer
        Dim prop As SQLDMO.Property, prop1 As SQLDMO.Property
        Dim Destination As New Hashtable
        Dim Node As TreeNode
        e = True

        Dim FillFactor As Boolean = True
        FillFactor = act.Options("FillFactor")

        ' Compare properties
        For i = 1 To oSource.Properties.Count
            If oSource.Properties.Item(i).Get And (oSource.Properties.Item(i).Name = "FillFactor" And FillFactor) Then
                If oSource.Properties.Item(i).Value <> oDestination.Properties.Item(i).Value Then
                    e = False
                    res &= oSource.Properties.Item(i).Name & vbTab & oSource.Properties.Item(i).Value & vbTab & oDestination.Properties.Item(i).Value & vbCrLf
                    Source_Table_Node.Nodes.Add(oSource.Properties.Item(i).Name & "=" & oSource.Properties.Item(i).Value)
                    Destination_Table_Node.Nodes.Add(oSource.Properties.Item(i).Name & "=" & oDestination.Properties.Item(i).Value)
                End If
            End If
        Next

        'Compare KeyColumns
        For i = 1 To oDestination.KeyColumns.Count
            Destination.Add(oDestination.KeyColumns.Item(i), 0)
        Next

        For i = 1 To oSource.KeyColumns.Count
            name = oSource.KeyColumns.Item(i)
            If Destination.ContainsKey(name) Then
                'exists in both
                Destination.Item(name) = 1
            Else
                'exist in source
                e = False
                res &= "Column" & vbTab & name & vbTab & "Not" & vbCrLf
                Node = New TreeNode(name)
                Node.ImageIndex = Me.imit_Column_move
                Node.SelectedImageIndex = Me.imit_Column_move
                Source_Table_Node.Nodes.Add(Node)

                Node = New TreeNode(name)
                Node.ImageIndex = Me.imit_Column_not_exist
                Node.SelectedImageIndex = Me.imit_Column_not_exist
                Destination_Table_Node.Nodes.Add(Node)
            End If
        Next

        For Each name In Destination.Keys
            If Destination.Item(name) = 0 Then
                'Exists in destination base
                e = False
                res &= "Column" & vbTab & "Not" & vbTab & name & vbCrLf
                Node = New TreeNode(name)
                Node.ImageIndex = Me.imit_Column_del
                Node.SelectedImageIndex = Me.imit_Column_del
                Destination_Table_Node.Nodes.Add(Node)

                Node = New TreeNode(name)
                Node.ImageIndex = Me.imit_Column_not_exist
                Node.SelectedImageIndex = Me.imit_Column_not_exist
                Source_Table_Node.Nodes.Add(Node)
            End If
        Next

        'Compare ReferencedColumns
        Destination.Clear()

        For i = 1 To oDestination.ReferencedColumns.Count
            Destination.Add(oDestination.ReferencedColumns.Item(i), 0)
        Next

        For i = 1 To oSource.ReferencedColumns.Count
            name = oSource.ReferencedColumns.Item(i)
            If Destination.ContainsKey(name) Then
                'exists in both
                Destination.Item(name) = 1
            Else
                'exist in source
                e = False
                res &= "Column" & vbTab & name & vbTab & "Not" & vbCrLf
                Node = New TreeNode(name)
                Node.ImageIndex = Me.imit_Column_move
                Node.SelectedImageIndex = Me.imit_Column_move
                Source_Table_Node.Nodes.Add(Node)

                Node = New TreeNode(name)
                Node.ImageIndex = Me.imit_Column_not_exist
                Node.SelectedImageIndex = Me.imit_Column_not_exist
                Destination_Table_Node.Nodes.Add(Node)
            End If
        Next

        For Each name In Destination.Keys
            If Destination.Item(name) = 0 Then
                'Exists in destination base
                e = False
                res &= "Column" & vbTab & "Not" & vbTab & name & vbCrLf
                Node = New TreeNode(name)
                Node.ImageIndex = Me.imit_Column_del
                Node.SelectedImageIndex = Me.imit_Column_del
                Destination_Table_Node.Nodes.Add(Node)

                Node = New TreeNode(name)
                Node.ImageIndex = Me.imit_Column_not_exist
                Node.SelectedImageIndex = Me.imit_Column_not_exist
                Source_Table_Node.Nodes.Add(Node)
            End If
        Next

        If e = False Then
            Compare_Res.Add(Source_Table_Node.FullPath, "ALTER TABLE " & tabName & " DROP CONSTRAINT " & oSource.Name & vbCrLf & "GO" & vbCrLf & oSource.Script & vbCrLf & "GO")
            Set_Diff(Source_Table_Node)
            Set_Diff(Destination_Table_Node)
        End If
    End Sub
    Private Sub Compare_Keys(ByRef oSource As SQLDMO.Table2, ByVal Source_Table_Node As TreeNode, ByRef oDestination As SQLDMO.Table2, ByVal Destination_Table_Node As TreeNode)
        Dim oKey As SQLDMO.Key
        Dim oKey1 As SQLDMO.Key
        Dim Node As TreeNode, Node1 As TreeNode
        Dim n As Integer, n1 As Integer, sk_ind As Integer, dk_ind As Integer
        Dim key As String
        Dim Destination As New Hashtable

        ' ADD COLUMNS
        Node = New TreeNode("Keys")
        Node.ImageIndex = Me.imit_Folder
        Node.SelectedImageIndex = Me.imit_Folder
        sk_ind = Source_Table_Node.Nodes.Add(Node)

        Node = New TreeNode("Keys")
        Node.ImageIndex = Me.imit_Folder
        Node.SelectedImageIndex = Me.imit_Folder
        dk_ind = Destination_Table_Node.Nodes.Add(Node)



        For Each oKey In oDestination.Keys
            Destination.Add(oKey.Name, 0)
        Next

        For Each oKey In oSource.Keys
            If (Destination.ContainsKey(oKey.Name)) Then
                'Exists in both base
                Destination.Item(oKey.Name) = 1

                Node = New TreeNode(oKey.Name)
                Node.ImageIndex = Me.imit_Key
                Node.SelectedImageIndex = Me.imit_Key
                n = Source_Table_Node.Nodes(sk_ind).Nodes.Add(Node)

                Node1 = New TreeNode(oKey.Name)
                Node1.ImageIndex = Me.imit_Key
                Node1.SelectedImageIndex = Me.imit_Key
                n1 = Destination_Table_Node.Nodes(dk_ind).Nodes.Add(Node1)

                oKey1 = oDestination.Keys.Item(oKey.Name)
                Compare_Key(oSource.Name, oKey, Node, oKey1, Node1)
            Else
                'Exists in source base
                Node = New TreeNode(oKey.Name)
                Node.ImageIndex = Me.imit_Key_move
                Node.SelectedImageIndex = Me.imit_Key_move
                n = Source_Table_Node.Nodes(sk_ind).Nodes.Add(Node)
                Me.Print_Key(oKey, Node)

                Node1 = New TreeNode(oKey.Name)
                Node1.ImageIndex = Me.imit_Key_not_exist
                Node1.SelectedImageIndex = Me.imit_Key_not_exist
                n1 = Destination_Table_Node.Nodes(dk_ind).Nodes.Add(Node1)
                Me.Print_Key(oKey, Node1)

                Me.Compare_Res.Add(Node.FullPath, oKey.Script)

                Set_Diff(Node.Parent)
                Set_Diff(Node1.Parent)
            End If

        Next

        For Each key In Destination.Keys
            If Destination.Item(key) = 0 Then
                'Exists in destination base
                Node = New TreeNode(key)
                Node.ImageIndex = Me.imit_Key_del
                Node.SelectedImageIndex = Me.imit_Key_del
                n = Destination_Table_Node.Nodes(dk_ind).Nodes.Add(Node)
                Me.Print_Key(oDestination.Keys.Item(key), Node)

                Node1 = New TreeNode(key)
                Node1.ImageIndex = Me.imit_Key_not_exist
                Node1.SelectedImageIndex = Me.imit_Key_not_exist
                n1 = Source_Table_Node.Nodes(dk_ind).Nodes.Add(Node1)
                Me.Print_Key(oDestination.Keys.Item(key), Node1)

                Me.Compare_Res.Add(Node.FullPath, "ALTER TABLE [" & oDestination.Name & "] DROP CONSTRAINT [" & key & "]" & vbCrLf & "GO")

                Set_Diff(Node.Parent)
                Set_Diff(Node1.Parent)
            End If
        Next

    End Sub

    Private Sub Print_Index(ByRef oIndex As SQLDMO.Index2, ByRef node As TreeNode)
        Dim i As Integer
        Dim n As TreeNode, n1 As TreeNode

        'n = New TreeNode("Key Columns")
        'n.ImageIndex = Me.imit_Folder
        'n.SelectedImageIndex = Me.imit_Folder
        'node.Nodes.Add(n)
        'For i = 1 To oIndex.IndexedColumns.
        '    KeyColumns.Count()
        '    n1 = New TreeNode(oKey.KeyColumns.Item(i))
        '    n1.ImageIndex = Me.imit_Column
        '    n1.SelectedImageIndex = Me.imit_Column
        '    n.Nodes.Add(n1)
        'Next


        'n = New TreeNode("Referenced Columns")
        'n.ImageIndex = Me.imit_Folder
        'n.SelectedImageIndex = Me.imit_Folder
        'node.Nodes.Add(n)
        'For i = 1 To oKey.ReferencedColumns.Count
        '    n1 = New TreeNode(oKey.ReferencedColumns.Item(i))
        '    n1.ImageIndex = Me.imit_Column
        '    n1.SelectedImageIndex = Me.imit_Column
        '    n.Nodes.Add(n1)
        'Next

        For i = 1 To oIndex.Properties.Count
            If oIndex.Properties.Item(i).Get Then
                node.Nodes.Add(oIndex.Properties.Item(i).Name & "=" & oIndex.Properties.Item(i).Value)
            End If
        Next



    End Sub
    Private Sub Compare_Indexes(ByRef oSource As SQLDMO.Table2, ByVal Source_Table_Node As TreeNode, ByRef oDestination As SQLDMO.Table2, ByVal Destination_Table_Node As TreeNode)
        Dim oIndex As SQLDMO.Index2
        Dim oIndex1 As SQLDMO.Index2
        Dim Node As TreeNode, Node1 As TreeNode
        Dim n As Integer, n1 As Integer, sk_ind As Integer, dk_ind As Integer
        Dim key As String
        Dim Destination As New Hashtable

        ' ADD COLUMNS
        Node = New TreeNode("Indexes")
        Node.ImageIndex = Me.imit_Folder
        Node.SelectedImageIndex = Me.imit_Folder
        sk_ind = Source_Table_Node.Nodes.Add(Node)

        Node = New TreeNode("Indexes")
        Node.ImageIndex = Me.imit_Folder
        Node.SelectedImageIndex = Me.imit_Folder
        dk_ind = Destination_Table_Node.Nodes.Add(Node)



        For Each oIndex In oDestination.Indexes
            If oIndex.StatisticsIndex = False Then
                Destination.Add(oIndex.Name, 0)
            End If
        Next

        For Each oIndex In oSource.Indexes
            If oIndex.StatisticsIndex = False Then
                If (Destination.ContainsKey(oIndex.Name)) Then
                    'Exists in both base
                    Destination.Item(oIndex.Name) = 1

                    Node = New TreeNode(oIndex.Name)
                    Node.ImageIndex = Me.imit_Index
                    Node.SelectedImageIndex = Me.imit_Index
                    n = Source_Table_Node.Nodes(sk_ind).Nodes.Add(Node)

                    Node1 = New TreeNode(oIndex.Name)
                    Node1.ImageIndex = Me.imit_Index
                    Node1.SelectedImageIndex = Me.imit_Index
                    n1 = Destination_Table_Node.Nodes(dk_ind).Nodes.Add(Node1)

                    oIndex1 = oDestination.Indexes.Item(oIndex.Name)
                    'Compare_Index(oIndex, Node, oIndex1, Node1)
                    key = Compare_Properties(oIndex.Properties, Node, oIndex1.Properties, Node1)
                    If Len(key) > 0 Then
                        Me.Compare_Res.Add(Node.FullPath, "DROP INDEX [" & oSource.Name & "].[" & oIndex.Name & "]" & vbCrLf & " GO " & vbCrLf & oIndex.Script())
                    End If
                Else
                    'Exists in source base
                    Node = New TreeNode(oIndex.Name)
                    Node.SelectedImageIndex = Me.imit_Index_move
                    Node.ImageIndex = Me.imit_Index_move
                    n = Source_Table_Node.Nodes(sk_ind).Nodes.Add(Node)
                    Me.Print_Index(oIndex, Node)

                    Node1 = New TreeNode(oIndex.Name)
                    Node1.ImageIndex = Me.imit_Index_not_exist
                    Node1.SelectedImageIndex = Me.imit_Index_not_exist
                    n1 = Destination_Table_Node.Nodes(dk_ind).Nodes.Add(Node1)
                    Me.Print_Index(oIndex, Node1)

                    Me.Compare_Res.Add(Node.FullPath, oIndex.Script)

                    Set_Diff(Node.Parent)
                    Set_Diff(Node1.Parent)
                End If
            End If
        Next

        For Each key In Destination.Keys
            If Destination.Item(key) = 0 Then
                'Exists in destination base
                Node = New TreeNode(key)
                Node.ImageIndex = Me.imit_Index_del
                Node.SelectedImageIndex = Me.imit_Index_del
                n = Destination_Table_Node.Nodes(dk_ind).Nodes.Add(Node)
                Me.Print_Index(oDestination.Indexes.Item(key), Node)

                Node1 = New TreeNode(key)
                Node1.ImageIndex = Me.imit_Index_not_exist
                Node1.SelectedImageIndex = Me.imit_Index_not_exist
                n1 = Source_Table_Node.Nodes(dk_ind).Nodes.Add(Node1)
                Me.Print_Index(oDestination.Indexes.Item(key), Node1)

                Me.Compare_Res.Add(Node.FullPath, "ALTER TABLE " & oDestination.Name & " DROP CONSTRAINT " & key & vbCrLf & "GO")

                Set_Diff(Node.Parent)
                Set_Diff(Node1.Parent)
            End If
        Next

    End Sub

    Private Sub Print_Check(ByRef oCheck As SQLDMO.Check, ByVal node As TreeNode)
        Dim i As Integer
        Dim n As TreeNode, n1 As TreeNode

        For i = 1 To oCheck.Properties.Count
            If oCheck.Properties.Item(i).Get Then
                node.Nodes.Add(oCheck.Properties.Item(i).Name & "=" & oCheck.Properties.Item(i).Value)
            End If
        Next
    End Sub
    Private Sub Compare_Checks(ByRef oSource As SQLDMO.Table2, ByVal Source_Table_Node As TreeNode, ByRef oDestination As SQLDMO.Table2, ByVal Destination_Table_Node As TreeNode)
        Dim oCheck As SQLDMO.Check, oCheck1 As SQLDMO.Check
        Dim Node As TreeNode, Node1 As TreeNode
        Dim n As Integer, n1 As Integer, sk_ind As Integer, dk_ind As Integer
        Dim Destination As New Hashtable
        Dim key As String

        ' ADD COLUMNS
        Node = New TreeNode("Checks")
        Node.ImageIndex = Me.imit_Folder
        Node.SelectedImageIndex = Me.imit_Folder
        sk_ind = Source_Table_Node.Nodes.Add(Node)

        Node = New TreeNode("Checks")
        Node.ImageIndex = Me.imit_Folder
        Node.SelectedImageIndex = Me.imit_Folder
        dk_ind = Destination_Table_Node.Nodes.Add(Node)



        For Each oCheck In oDestination.Checks
            ' If oCheck.StatisticsIndex = False Then
            Destination.Add(oCheck.Name, 0)
            'End If
        Next

        For Each oCheck In oSource.Checks
            'If oIndex.StatisticsIndex = False Then
            If (Destination.ContainsKey(oCheck.Name)) Then
                'Exists in both base
                Destination.Item(oCheck.Name) = 1

                Node = New TreeNode(oCheck.Name)
                Node.ImageIndex = Me.imit_Check
                Node.SelectedImageIndex = Me.imit_Check
                n = Source_Table_Node.Nodes(sk_ind).Nodes.Add(Node)

                Node1 = New TreeNode(oCheck.Name)
                Node1.ImageIndex = Me.imit_Check
                Node1.SelectedImageIndex = Me.imit_Check
                n1 = Destination_Table_Node.Nodes(dk_ind).Nodes.Add(Node1)

                oCheck1 = oDestination.Checks.Item(oCheck.Name)
                'Compare_Check(Prefix & "_" & dk_ind & "_" & n1, oCheck, Node, oCheck1, Node1)
                key = Compare_Properties(oCheck.Properties, Node, oCheck1.Properties, Node1)
                If Len(key) > 0 Then
                    Me.Compare_Res.Add(Node.FullPath, oCheck.Script())
                End If
            Else
                'Exists in source base
                Node = New TreeNode(oCheck.Name)
                Node.SelectedImageIndex = Me.imit_Check_move
                Node.ImageIndex = Me.imit_Check_move
                n = Source_Table_Node.Nodes(sk_ind).Nodes.Add(Node)
                Me.Print_Check(oCheck, Node)

                Node1 = New TreeNode(oCheck.Name)
                Node1.ImageIndex = Me.imit_Check_not_exist
                Node1.SelectedImageIndex = Me.imit_Check_not_exist
                n1 = Destination_Table_Node.Nodes(dk_ind).Nodes.Add(Node1)
                Me.Print_Check(oCheck, Node1)

                Me.Compare_Res.Add(Node.FullPath, oCheck.Script)

                Set_Diff(Node.Parent)
                Set_Diff(Node1.Parent)
            End If
            '  End If
        Next

        For Each key In Destination.Keys
            If Destination.Item(key) = 0 Then
                'Exists in destination base
                Node = New TreeNode(key)
                Node.ImageIndex = Me.imit_Check_del
                Node.SelectedImageIndex = Me.imit_Check_del
                n = Destination_Table_Node.Nodes(dk_ind).Nodes.Add(Node)
                Me.Print_Check(oDestination.Checks.Item(key), Node)

                Node1 = New TreeNode(key)
                Node1.ImageIndex = Me.imit_Check_not_exist
                Node1.SelectedImageIndex = Me.imit_Check_not_exist
                n1 = Source_Table_Node.Nodes(dk_ind).Nodes.Add(Node1)
                Me.Print_Check(oDestination.Checks.Item(key), Node1)

                Me.Compare_Res.Add(Node.FullPath, "ALTER TABLE " & oDestination.Name & " DROP CONSTRAINT " & key & vbCrLf & "GO")

                Set_Diff(Node.Parent)
                Set_Diff(Node1.Parent)
            End If
        Next

    End Sub

    Private Sub Print_Permission(ByRef oCheck As SQLDMO.Check, ByVal node As TreeNode)
        Dim i As Integer
        Dim n As TreeNode, n1 As TreeNode

        For i = 1 To oCheck.Properties.Count
            If oCheck.Properties.Item(i).Get Then
                node.Nodes.Add(oCheck.Properties.Item(i).Name & "=" & oCheck.Properties.Item(i).Value)
            End If
        Next
    End Sub
    Private Sub Compare_Permission(ByRef oSource As SQLDMO.SQLObjectList, ByVal Source_Table_Node As TreeNode, ByRef oDestination As SQLDMO.SQLObjectList, ByVal Destination_Table_Node As TreeNode)
        'Dim key As Boolean
        Dim key As String
        Dim i As Integer, n As Integer
        Dim oPerm As SQLDMO.Permission2, oPerm1 As SQLDMO.Permission2
        'Dim Destination As New Hashtable
        Dim Node As TreeNode
        'e = True
        Dim Destination As New Hashtable

        For i = 1 To oDestination.Count
            oPerm = oDestination.Item(i)
            If act.IgnUsr.Contains(oPerm.Grantee) = False Then
                Destination.Add(oPerm.Grantee, 0)
            End If
        Next


        For i = 1 To oSource.Count
            oPerm = oSource.Item(i)
            If act.IgnUsr.Contains(oPerm.Grantee) = False Then
                If (Destination.ContainsKey(oPerm.Grantee)) Then
                    'Exists in both base
                    Destination.Item(oPerm.Grantee) = 1
                    For n = 1 To oDestination.Count
                        oPerm1 = oDestination.Item(n)
                        If (oPerm.Grantee = oPerm.Grantee) Then
                            Exit For
                        End If
                    Next
                    If oPerm.Granted <> oPerm1.Granted Then
                        Node = New TreeNode(oPerm.Grantee)
                        Node.ImageIndex = Me.imit_Role_diff
                        Node.SelectedImageIndex = Me.imit_Role_diff
                        Source_Table_Node.Nodes.Add(Node)
                        Set_Diff(Node.Parent)

                        Node = New TreeNode(oPerm.Grantee)
                        Node.ImageIndex = Me.imit_Role_diff
                        Node.SelectedImageIndex = Me.imit_Role_diff
                        Destination_Table_Node.Nodes.Add(Node)
                        Set_Diff(Node.Parent)

                        If oPerm.Granted Then
                            Me.Compare_Res.Add(Node.FullPath, "GRANT " & oPerm.PrivilegeTypeName & " ON " & oPerm.ObjectName & " TO " & oPerm.Grantee & vbCrLf & " GO")
                        Else
                            Me.Compare_Res.Add(Node.FullPath, "DENY " & oPerm.PrivilegeTypeName & " ON " & oPerm.ObjectName & " TO " & oPerm.Grantee & vbCrLf & " GO")
                        End If
                    Else
                        Node = New TreeNode(oPerm.Grantee)
                        Node.ImageIndex = Me.imit_Role
                        Node.SelectedImageIndex = Me.imit_Role
                        Source_Table_Node.Nodes.Add(Node)

                        Node = New TreeNode(oPerm.Grantee)
                        Node.ImageIndex = Me.imit_Role
                        Node.SelectedImageIndex = Me.imit_Role
                        Destination_Table_Node.Nodes.Add(Node)
                    End If

                Else
                    'Exists in source base
                    Node = New TreeNode(oPerm.Grantee)
                    Node.ImageIndex = Me.imit_Role_move
                    Node.SelectedImageIndex = Me.imit_Role_move
                    n = Source_Table_Node.Nodes.Add(Node)
                    Set_Diff(Node.Parent)

                    Node = New TreeNode(oPerm.Grantee)
                    Node.ImageIndex = Me.imit_Role_not_exist
                    Node.SelectedImageIndex = Me.imit_Role_not_exist
                    n = Destination_Table_Node.Nodes.Add(Node)
                    Set_Diff(Node.Parent)
                    'GRANT SELECT ON authors TO public GO
                    If oPerm.Granted Then
                        Me.Compare_Res.Add(Node.FullPath, "GRANT " & oPerm.PrivilegeTypeName & " ON " & oPerm.ObjectName & " TO " & oPerm.Grantee & vbCrLf & " GO")
                    Else
                        Me.Compare_Res.Add(Node.FullPath, "DENY " & oPerm.PrivilegeTypeName & " ON " & oPerm.ObjectName & " TO " & oPerm.Grantee & vbCrLf & " GO")
                    End If

                End If
            End If


        Next


        For Each key In Destination.Keys
            If Destination.Item(key) = 0 Then
                'Exists in destination base
                Node = New TreeNode(key)
                Node.ImageIndex = Me.imit_Role_del
                Node.SelectedImageIndex = Me.imit_Role_del
                n = Destination_Table_Node.Nodes.Add(Node)
                Set_Diff(Node.Parent)

                Node = New TreeNode(key)
                Node.ImageIndex = Me.imit_Role_not_exist
                Node.SelectedImageIndex = Me.imit_Role_not_exist
                n = Source_Table_Node.Nodes.Add(Node)
                Set_Diff(Node.Parent)

                Me.Compare_Res.Add(Node.FullPath, "REVOKE " & oPerm.PrivilegeTypeName & " ON " & oPerm.ObjectName & " TO " & key & vbCrLf & " GO")
            End If
        Next

    End Sub
    Private Sub Compare_Permissions(ByRef oSource As SQLDMO.Table2, ByVal Source_Table_Node As TreeNode, ByRef oDestination As SQLDMO.Table2, ByVal Destination_Table_Node As TreeNode)
        Dim oPerm As SQLDMO.Permission2
        Dim oPerm1 As SQLDMO.Permission2
        Dim Node As TreeNode, Node1 As TreeNode
        Dim ns As Integer, nd As Integer, sk_ind As Integer, dk_ind As Integer
        'Dim key As String
        Dim Destination As New Hashtable

        ' ADD COLUMNS
        Node = New TreeNode("Permissions")
        Node.ImageIndex = Me.imit_Folder
        Node.SelectedImageIndex = Me.imit_Folder
        sk_ind = Source_Table_Node.Nodes.Add(Node)

        Node1 = New TreeNode("Permissions")
        Node1.ImageIndex = Me.imit_Folder
        Node1.SelectedImageIndex = Me.imit_Folder
        dk_ind = Destination_Table_Node.Nodes.Add(Node1)


        ' SELECT
        Node = New TreeNode("Select")
        Node.ImageIndex = Me.imit_Folder
        Node.SelectedImageIndex = Me.imit_Folder
        ns = Source_Table_Node.Nodes(sk_ind).Nodes.Add(Node)

        Node1 = New TreeNode("Select")
        Node1.ImageIndex = Me.imit_Folder
        Node1.SelectedImageIndex = Me.imit_Folder
        nd = Destination_Table_Node.Nodes(dk_ind).Nodes.Add(Node1)

        Compare_Permission(oSource.ListPermissions(SQLDMO.SQLDMO_PRIVILEGE_TYPE.SQLDMOPriv_Select), Node, oDestination.ListPermissions(SQLDMO.SQLDMO_PRIVILEGE_TYPE.SQLDMOPriv_Select), Node1)

        ' Update
        Node = New TreeNode("Update")
        Node.ImageIndex = Me.imit_Folder
        Node.SelectedImageIndex = Me.imit_Folder
        ns = Source_Table_Node.Nodes(sk_ind).Nodes.Add(Node)

        Node1 = New TreeNode("Update")
        Node1.ImageIndex = Me.imit_Folder
        Node1.SelectedImageIndex = Me.imit_Folder
        nd = Destination_Table_Node.Nodes(dk_ind).Nodes.Add(Node1)

        Compare_Permission(oSource.ListPermissions(SQLDMO.SQLDMO_PRIVILEGE_TYPE.SQLDMOPriv_Update), Node, oDestination.ListPermissions(SQLDMO.SQLDMO_PRIVILEGE_TYPE.SQLDMOPriv_Update), Node1)

        ' Insert
        Node = New TreeNode("Insert")
        Node.ImageIndex = Me.imit_Folder
        Node.SelectedImageIndex = Me.imit_Folder
        ns = Source_Table_Node.Nodes(sk_ind).Nodes.Add(Node)

        Node1 = New TreeNode("Insert")
        Node1.ImageIndex = Me.imit_Folder
        Node1.SelectedImageIndex = Me.imit_Folder
        nd = Destination_Table_Node.Nodes(dk_ind).Nodes.Add(Node1)

        Compare_Permission(oSource.ListPermissions(SQLDMO.SQLDMO_PRIVILEGE_TYPE.SQLDMOPriv_Insert), Node, oDestination.ListPermissions(SQLDMO.SQLDMO_PRIVILEGE_TYPE.SQLDMOPriv_Insert), Node1)

        ' Delete
        Node = New TreeNode("Delete")
        Node.ImageIndex = Me.imit_Folder
        Node.SelectedImageIndex = Me.imit_Folder
        ns = Source_Table_Node.Nodes(sk_ind).Nodes.Add(Node)

        Node1 = New TreeNode("Delete")
        Node1.ImageIndex = Me.imit_Folder
        Node1.SelectedImageIndex = Me.imit_Folder
        nd = Destination_Table_Node.Nodes(dk_ind).Nodes.Add(Node1)

        Compare_Permission(oSource.ListPermissions(SQLDMO.SQLDMO_PRIVILEGE_TYPE.SQLDMOPriv_Delete), Node, oDestination.ListPermissions(SQLDMO.SQLDMO_PRIVILEGE_TYPE.SQLDMOPriv_Delete), Node1)

        ' References
        Node = New TreeNode("References")
        Node.ImageIndex = Me.imit_Folder
        Node.SelectedImageIndex = Me.imit_Folder
        ns = Source_Table_Node.Nodes(sk_ind).Nodes.Add(Node)

        Node1 = New TreeNode("References")
        Node1.ImageIndex = Me.imit_Folder
        Node1.SelectedImageIndex = Me.imit_Folder
        nd = Destination_Table_Node.Nodes(dk_ind).Nodes.Add(Node1)

        Compare_Permission(oSource.ListPermissions(SQLDMO.SQLDMO_PRIVILEGE_TYPE.SQLDMOPriv_References), Node, oDestination.ListPermissions(SQLDMO.SQLDMO_PRIVILEGE_TYPE.SQLDMOPriv_References), Node1)



    End Sub

    Private Sub Compare_Triggers(ByRef oSource As SQLDMO.Table2, ByVal Source_Table_Node As TreeNode, ByRef oDestination As SQLDMO.Table2, ByVal Destination_Table_Node As TreeNode)
        Dim oTrigger As SQLDMO.Trigger2
        Dim Node As TreeNode, Node1 As TreeNode
        Dim n As Integer, n1 As Integer, sk_ind As Integer, dk_ind As Integer
        Dim key As String, sql As String, sql1 As String

        Dim Destination As New Hashtable

        Node = New TreeNode("Triggers")
        Node.ImageIndex = Me.imit_Folder
        Node.SelectedImageIndex = Me.imit_Folder
        sk_ind = Source_Table_Node.Nodes.Add(Node)

        Node = New TreeNode("Triggers")
        Node.ImageIndex = Me.imit_Folder
        Node.SelectedImageIndex = Me.imit_Folder
        dk_ind = Destination_Table_Node.Nodes.Add(Node)

        For Each oTrigger In oDestination.Triggers
            If Not oTrigger.SystemObject Then
                Destination.Add(oTrigger.Name, 0)
            End If
        Next

        For Each oTrigger In oSource.Triggers
            If Not oTrigger.SystemObject Then
                Application.DoEvents()
                If (Destination.ContainsKey(oTrigger.Name)) Then
                    'Exists in both base
                    Destination.Item(oTrigger.Name) = 1

                    Node = New TreeNode(oTrigger.Name)
                    Node1 = New TreeNode(oTrigger.Name)

                    sql = oTrigger.Text
                    sql1 = oDestination.Triggers.Item(oTrigger.Name).Text
                    If String.Compare(Regex.Replace(sql, "\s", ""), Regex.Replace(sql1, "\s", ""), True) <> 0 Then
                        Node1.ImageIndex = Me.imit_Trigger_diff
                        Node1.SelectedImageIndex = Me.imit_Trigger_diff
                        Node.ImageIndex = Me.imit_Trigger_diff
                        Node.SelectedImageIndex = Me.imit_Trigger_diff

                        n = Source_Table_Node.Nodes(sk_ind).Nodes.Add(Node)
                        n1 = Destination_Table_Node.Nodes(dk_ind).Nodes.Add(Node1)


                        Set_Diff(Node.Parent)
                        Set_Diff(Node1.Parent)

                        Me.Compare_Res.Add(Node.FullPath, regexp_Alter.Replace(oTrigger.Script(), "ALTER"))
                    Else
                        Node1.ImageIndex = Me.imit_Trigger
                        Node1.SelectedImageIndex = Me.imit_Trigger
                        Node.ImageIndex = Me.imit_Trigger
                        Node.SelectedImageIndex = Me.imit_Trigger

                        n = Source_Table_Node.Nodes(sk_ind).Nodes.Add(Node)
                        n1 = Destination_Table_Node.Nodes(dk_ind).Nodes.Add(Node1)
                    End If


                Else
                    'Exists in source base
                    Node = New TreeNode(oTrigger.Name)
                    Node.ImageIndex = Me.imit_Trigger_move
                    Node.SelectedImageIndex = Me.imit_Trigger_move
                    n = Source_Table_Node.Nodes(sk_ind).Nodes.Add(Node)

                    Node1 = New TreeNode(oTrigger.Name)
                    Node1.ImageIndex = Me.imit_Trigger_not_exist
                    Node1.SelectedImageIndex = Me.imit_Trigger_not_exist
                    n1 = Destination_Table_Node.Nodes(dk_ind).Nodes.Add(Node1)

                    Set_Diff(Node.Parent)
                    Set_Diff(Node1.Parent)

                    Me.Compare_Res.Add(Node.FullPath, oTrigger.Script())
                End If
            End If
        Next

        For Each key In Destination.Keys
            If Destination.Item(key) = 0 Then
                'Exists in destination base
                Node1 = New TreeNode(key)
                Node1.ImageIndex = Me.imit_Trigger_not_exist
                Node1.SelectedImageIndex = Me.imit_Trigger_not_exist
                n1 = Source_Table_Node.Nodes(sk_ind).Nodes.Add(Node1)

                Node = New TreeNode(key)
                Node.ImageIndex = Me.imit_Trigger_del
                Node.SelectedImageIndex = Me.imit_Trigger_del
                n = Destination_Table_Node.Nodes(dk_ind).Nodes.Add(Node)

                Me.Compare_Res.Add(Node.FullPath, "DROP TRIGGER " & key & vbCrLf & "GO")


                Set_Diff(Node.Parent)
                Set_Diff(Node1.Parent)
            End If
        Next

    End Sub

    Private Sub Compare_Table(ByVal TableName As String, ByVal Source_Table_Node As TreeNode, ByVal Destination_Table_Node As TreeNode)
        Dim Source_Table As SQLDMO.Table2
        Dim Dest_Table As SQLDMO.Table2
        Source_Table = oDB_Source.Tables.Item(TableName)
        Dest_Table = oDB_Destination.Tables.Item(TableName)

        If mi_Columns.Checked = True And (Source_Table.Columns.Count > 0 Or Dest_Table.Columns.Count > 0) Then
            Compare_Columns(Source_Table, Source_Table_Node, Dest_Table, Destination_Table_Node)
        End If

        ''If mi_PrKeys.Checked = True Then
        ''    Compare_PrKey(Source_Prefix, Source_Table, Source_Table_Node, Dest_Table, Destination_Table_Node)
        ''End If

        If mi_Keys.Checked = True And (Source_Table.Keys.Count > 0 Or Dest_Table.Keys.Count > 0) Then
            Compare_Keys(Source_Table, Source_Table_Node, Dest_Table, Destination_Table_Node)
        End If

        If mi_Indexes.Checked = True And (Source_Table.Indexes.Count > 0 Or Dest_Table.Indexes.Count > 0) Then
            Compare_Indexes(Source_Table, Source_Table_Node, Dest_Table, Destination_Table_Node)
        End If

        If mi_Checks.Checked = True And (Source_Table.Checks.Count > 0 Or Dest_Table.Checks.Count > 0) Then
            Compare_Checks(Source_Table, Source_Table_Node, Dest_Table, Destination_Table_Node)
        End If

        If mi_Triggers.Checked = True And (Source_Table.Triggers.Count > 0 Or Dest_Table.Triggers.Count > 0) Then
            Compare_Triggers(Source_Table, Source_Table_Node, Dest_Table, Destination_Table_Node)
        End If

        If mi_tabPermissions.Checked = True Then
            Me.Compare_Permissions(Source_Table, Source_Table_Node, Dest_Table, Destination_Table_Node)
        End If
    End Sub
    Private Sub Compare_Tables()
        Dim node As TreeNode

        For Each node In trv_DB_Source.Nodes(ind_Table).Nodes
            'If node.Checked And node.ImageIndex = imit_Table And trv_DB_Destination.Nodes(ind_Table).Nodes(node.Index).ImageIndex = imit_Table Then
            If node.Checked And node.ImageIndex = imit_Table Then
                Compare_Table(node.Text, node, trv_DB_Destination.Nodes(ind_Table).Nodes(node.Index))
                tb_Compare_Res.Text = "Table: " & node.Text & vbCrLf & tb_Compare_Res.Text
                Application.DoEvents()
            End If
        Next
    End Sub
    Private Sub Init_Tables()
        Dim oTable As SQLDMO.Table2
        Dim Node As TreeNode, Node1 As TreeNode
        Dim n As Integer, n1 As Integer
        Dim key As String, sql As String

        Dim Destination As New Hashtable

        For Each oTable In oDB_Destination.Tables
            If Not oTable.SystemObject Then
                Destination.Add(oTable.Name, 0)
            End If
        Next

        For Each oTable In oDB_Source.Tables
            If Not oTable.SystemObject Then
                If (Destination.ContainsKey(oTable.Name)) Then
                    'Exists in both base
                    Destination.Item(oTable.Name) = 1

                    Node = New TreeNode(oTable.Name)
                    Node.ImageIndex = Me.imit_Table
                    Node.SelectedImageIndex = Me.imit_Table
                    n = Me.trv_DB_Source.Nodes(Me.ind_Table).Nodes.Add(Node)

                    Node1 = New TreeNode(oTable.Name)
                    Node1.ImageIndex = Me.imit_Table
                    Node1.SelectedImageIndex = Me.imit_Table
                    n1 = Me.trv_DB_Destination.Nodes(Me.ind_Table).Nodes.Add(Node1)


                    'Compare_Table(oTable.Name, Node, Me.ind_Table & "_" & n, Node1, Me.ind_Table & "_" & n1)
                Else
                    'Exists in source base
                    Node = New TreeNode(oTable.Name)
                    Node.ImageIndex = Me.imit_Table_move
                    Node.SelectedImageIndex = Me.imit_Table_move
                    n = Me.trv_DB_Source.Nodes(Me.ind_Table).Nodes.Add(Node)


                    Node1 = New TreeNode(oTable.Name)
                    Node1.ImageIndex = Me.imit_Table_not_exist
                    Node1.SelectedImageIndex = Me.imit_Table_not_exist
                    n1 = Me.trv_DB_Destination.Nodes(Me.ind_Table).Nodes.Add(Node1)

                    Set_Diff(Node.Parent)
                    Set_Diff(Node1.Parent)

                    Me.Compare_Res.Add(Node.FullPath, oTable.Script())
                End If
            End If
        Next

        For Each key In Destination.Keys
            If Destination.Item(key) = 0 Then
                'Exists in destination base
                Node1 = New TreeNode(key)
                Node1.ImageIndex = Me.imit_Table_not_exist
                Node1.SelectedImageIndex = Me.imit_Table_not_exist
                n1 = Me.trv_DB_Source.Nodes(Me.ind_Table).Nodes.Add(Node1)

                Node = New TreeNode(key)
                Node.ImageIndex = Me.imit_Table_del
                Node.SelectedImageIndex = Me.imit_Table_del
                n = Me.trv_DB_Destination.Nodes(Me.ind_Table).Nodes.Add(Node)


                Set_Diff(Node.Parent)
                Set_Diff(Node1.Parent)

                Me.Compare_Res.Add(Node.FullPath, "drop table " & key & vbCrLf & "GO")
            End If
        Next

    End Sub
#End Region
#Region "View"
    Private Sub Compare_View_Permissions(ByRef oSource As SQLDMO.View2, ByVal Source_Table_Node As TreeNode, ByRef oDestination As SQLDMO.View2, ByVal Destination_Table_Node As TreeNode)
        Dim oPerm As SQLDMO.Permission2
        Dim oPerm1 As SQLDMO.Permission2
        Dim Node As TreeNode, Node1 As TreeNode
        Dim ns As Integer, nd As Integer, sk_ind As Integer, dk_ind As Integer
        'Dim key As String
        Dim Destination As New Hashtable

        ' ADD COLUMNS
        Node = New TreeNode("Permissions")
        Node.ImageIndex = Me.imit_Folder
        Node.SelectedImageIndex = Me.imit_Folder
        sk_ind = Source_Table_Node.Nodes.Add(Node)

        Node1 = New TreeNode("Permissions")
        Node1.ImageIndex = Me.imit_Folder
        Node1.SelectedImageIndex = Me.imit_Folder
        dk_ind = Destination_Table_Node.Nodes.Add(Node1)


        ' SELECT
        Node = New TreeNode("Select")
        Node.ImageIndex = Me.imit_Folder
        Node.SelectedImageIndex = Me.imit_Folder
        ns = Source_Table_Node.Nodes(sk_ind).Nodes.Add(Node)

        Node1 = New TreeNode("Select")
        Node1.ImageIndex = Me.imit_Folder
        Node1.SelectedImageIndex = Me.imit_Folder
        nd = Destination_Table_Node.Nodes(dk_ind).Nodes.Add(Node1)

        Compare_Permission(oSource.ListPermissions(SQLDMO.SQLDMO_PRIVILEGE_TYPE.SQLDMOPriv_Select), Node, oDestination.ListPermissions(SQLDMO.SQLDMO_PRIVILEGE_TYPE.SQLDMOPriv_Select), Node1)

        ' Update
        Node = New TreeNode("Update")
        Node.ImageIndex = Me.imit_Folder
        Node.SelectedImageIndex = Me.imit_Folder
        ns = Source_Table_Node.Nodes(sk_ind).Nodes.Add(Node)

        Node1 = New TreeNode("Update")
        Node1.ImageIndex = Me.imit_Folder
        Node1.SelectedImageIndex = Me.imit_Folder
        nd = Destination_Table_Node.Nodes(dk_ind).Nodes.Add(Node1)

        Compare_Permission(oSource.ListPermissions(SQLDMO.SQLDMO_PRIVILEGE_TYPE.SQLDMOPriv_Update), Node, oDestination.ListPermissions(SQLDMO.SQLDMO_PRIVILEGE_TYPE.SQLDMOPriv_Update), Node1)

        ' Insert
        Node = New TreeNode("Insert")
        Node.ImageIndex = Me.imit_Folder
        Node.SelectedImageIndex = Me.imit_Folder
        ns = Source_Table_Node.Nodes(sk_ind).Nodes.Add(Node)

        Node1 = New TreeNode("Insert")
        Node1.ImageIndex = Me.imit_Folder
        Node1.SelectedImageIndex = Me.imit_Folder
        nd = Destination_Table_Node.Nodes(dk_ind).Nodes.Add(Node1)

        Compare_Permission(oSource.ListPermissions(SQLDMO.SQLDMO_PRIVILEGE_TYPE.SQLDMOPriv_Insert), Node, oDestination.ListPermissions(SQLDMO.SQLDMO_PRIVILEGE_TYPE.SQLDMOPriv_Insert), Node1)

        ' Delete
        Node = New TreeNode("Delete")
        Node.ImageIndex = Me.imit_Folder
        Node.SelectedImageIndex = Me.imit_Folder
        ns = Source_Table_Node.Nodes(sk_ind).Nodes.Add(Node)

        Node1 = New TreeNode("Delete")
        Node1.ImageIndex = Me.imit_Folder
        Node1.SelectedImageIndex = Me.imit_Folder
        nd = Destination_Table_Node.Nodes(dk_ind).Nodes.Add(Node1)

        Compare_Permission(oSource.ListPermissions(SQLDMO.SQLDMO_PRIVILEGE_TYPE.SQLDMOPriv_Delete), Node, oDestination.ListPermissions(SQLDMO.SQLDMO_PRIVILEGE_TYPE.SQLDMOPriv_Delete), Node1)

        ' References
        Node = New TreeNode("References")
        Node.ImageIndex = Me.imit_Folder
        Node.SelectedImageIndex = Me.imit_Folder
        ns = Source_Table_Node.Nodes(sk_ind).Nodes.Add(Node)

        Node1 = New TreeNode("References")
        Node1.ImageIndex = Me.imit_Folder
        Node1.SelectedImageIndex = Me.imit_Folder
        nd = Destination_Table_Node.Nodes(dk_ind).Nodes.Add(Node1)

        Compare_Permission(oSource.ListPermissions(SQLDMO.SQLDMO_PRIVILEGE_TYPE.SQLDMOPriv_References), Node, oDestination.ListPermissions(SQLDMO.SQLDMO_PRIVILEGE_TYPE.SQLDMOPriv_References), Node1)



    End Sub
    Private Sub Compare_Views()
        Dim node As TreeNode, dNode As TreeNode
        Dim oView As SQLDMO.View2, oView1 As SQLDMO.View2
        ' Dim sql As String, sql1 As String
        For Each node In trv_DB_Source.Nodes(ind_View).Nodes
            If node.Checked And node.ImageIndex = imit_View Then
                oView = oDB_Source.Views.Item(node.Text)
                oView1 = oDB_Destination.Views.Item(node.Text)
                dNode = FindNode(trv_DB_Destination.Nodes, node)
                If String.Compare(Regex.Replace(oView.Text, "\s", ""), Regex.Replace(oView1.Text, "\s", ""), True) <> 0 Then
                    node.ImageIndex = imit_View_diff
                    node.SelectedImageIndex = imit_View_diff
                    Compare_Res.Add(node.FullPath, regexp_Alter.Replace(oView.Script(), "ALTER"))
                End If
                If mi_viewPermissions.Checked = True Then
                    Me.Compare_View_Permissions(oView, node, oView1, dNode)
                End If
            End If
        Next


    End Sub
    Private Sub Init_Views()
        Dim oView As SQLDMO.View2
        Dim Node As TreeNode, Node1 As TreeNode
        Dim n As Integer, n1 As Integer
        Dim key As String, sql As String

        Dim Destination As New Hashtable

        For Each oView In oDB_Destination.Views
            If Not oView.SystemObject Then
                Destination.Add(oView.Name, 0)
            End If
        Next

        For Each oView In oDB_Source.Views
            If Not oView.SystemObject Then
                'tb_Compare_Res.Text = "compare view " & oView.Name & vbCrLf & tb_Compare_Res.Text
                'Application.DoEvents()
                If (Destination.ContainsKey(oView.Name)) Then
                    'Exists in both base
                    Destination.Item(oView.Name) = 1

                    Node = New TreeNode(oView.Name)
                    Node1 = New TreeNode(oView.Name)


                    Node1.ImageIndex = Me.imit_View
                    Node1.SelectedImageIndex = Me.imit_View
                    Node.ImageIndex = Me.imit_View
                    Node.SelectedImageIndex = Me.imit_View

                    n = Me.trv_DB_Source.Nodes(Me.ind_View).Nodes.Add(Node)
                    n1 = Me.trv_DB_Destination.Nodes(Me.ind_View).Nodes.Add(Node1)


                Else
                    'Exists in source base
                    Node = New TreeNode(oView.Name)
                    Node.ImageIndex = Me.imit_View_move
                    Node.SelectedImageIndex = Me.imit_View_move
                    n = Me.trv_DB_Source.Nodes(Me.ind_View).Nodes.Add(Node)

                    Node1 = New TreeNode(oView.Name)
                    Node1.ImageIndex = Me.imit_View_not_exist
                    Node1.SelectedImageIndex = Me.imit_View_not_exist
                    n1 = Me.trv_DB_Destination.Nodes(Me.ind_View).Nodes.Add(Node1)

                    Set_Diff(Node.Parent)
                    Set_Diff(Node1.Parent)

                    Compare_Res.Add(Node.FullPath, oView.Script())
                End If
            End If
        Next

        For Each key In Destination.Keys
            If Destination.Item(key) = 0 Then
                'Exists in destination base
                Node1 = New TreeNode(key)
                Node1.ImageIndex = Me.imit_View_not_exist
                Node1.SelectedImageIndex = Me.imit_View_not_exist
                n1 = Me.trv_DB_Source.Nodes(Me.ind_View).Nodes.Add(Node1)

                Node = New TreeNode(key)
                Node.ImageIndex = Me.imit_View_del
                Node.SelectedImageIndex = Me.imit_View_del
                n = Me.trv_DB_Destination.Nodes(Me.ind_View).Nodes.Add(Node)
                Compare_Res.Add(Node.FullPath, "drop view " & key & vbCrLf & "GO")

                Set_Diff(Node.Parent)
                Set_Diff(Node1.Parent)
            End If
        Next

    End Sub
#End Region
#Region "SP"
    Private Sub Compare_SP_Permissions(ByRef oSource As SQLDMO.StoredProcedure2, ByVal Source_Table_Node As TreeNode, ByRef oDestination As SQLDMO.StoredProcedure2, ByVal Destination_Table_Node As TreeNode)
        Dim oPerm As SQLDMO.Permission2
        Dim oPerm1 As SQLDMO.Permission2
        Dim Node As TreeNode, Node1 As TreeNode
        Dim ns As Integer, nd As Integer, sk_ind As Integer, dk_ind As Integer
        'Dim key As String
        Dim Destination As New Hashtable

        ' ADD COLUMNS
        Node = New TreeNode("Permissions")
        Node.ImageIndex = Me.imit_Folder
        Node.SelectedImageIndex = Me.imit_Folder
        sk_ind = Source_Table_Node.Nodes.Add(Node)

        Node1 = New TreeNode("Permissions")
        Node1.ImageIndex = Me.imit_Folder
        Node1.SelectedImageIndex = Me.imit_Folder
        dk_ind = Destination_Table_Node.Nodes.Add(Node1)


        ' EXEC
        Node = New TreeNode("EXEC")
        Node.ImageIndex = Me.imit_Folder
        Node.SelectedImageIndex = Me.imit_Folder
        ns = Source_Table_Node.Nodes(sk_ind).Nodes.Add(Node)

        Node1 = New TreeNode("EXEC")
        Node1.ImageIndex = Me.imit_Folder
        Node1.SelectedImageIndex = Me.imit_Folder
        nd = Destination_Table_Node.Nodes(dk_ind).Nodes.Add(Node1)

        Compare_Permission(oSource.ListPermissions(SQLDMO.SQLDMO_PRIVILEGE_TYPE.SQLDMOPriv_Execute), Node, oDestination.ListPermissions(SQLDMO.SQLDMO_PRIVILEGE_TYPE.SQLDMOPriv_Execute), Node1)

    End Sub
    Private Sub Compare_SPs()
        Dim node As TreeNode, dNode As TreeNode
        Dim sql As String, sql1 As String
        Dim oSP As SQLDMO.StoredProcedure2, oSP1 As SQLDMO.StoredProcedure2

        For Each node In trv_DB_Source.Nodes(ind_SP).Nodes
            If node.Checked And node.ImageIndex = imit_SP Then
                tb_Compare_Res.Text = "compare sp: " & node.Text & vbCrLf & tb_Compare_Res.Text
                Application.DoEvents()

                oSP = oDB_Source.StoredProcedures.Item(node.Text)
                oSP1 = oDB_Destination.StoredProcedures.Item(node.Text)

                sql = oSP.Text
                sql1 = oSP1.Text

                dNode = FindNode(trv_DB_Destination.Nodes, node)

                If String.Compare(Regex.Replace(sql, "\s", ""), Regex.Replace(sql1, "\s", ""), True) <> 0 Then
                    node.ImageIndex = imit_SP_diff
                    node.SelectedImageIndex = imit_SP_diff

                    Compare_Res.Add(node.FullPath, regexp_AlterSP.Replace(oSP.Script(), "ALTER PROCEDURE"))
                End If

                If mi_spPermissions.Checked Then
                    Compare_SP_Permissions(oSP, node, oSP1, dNode)
                End If
            End If
        Next


    End Sub
    Private Sub Init_SP()
        Dim oSP As SQLDMO.StoredProcedure2
        Dim Node As TreeNode, Node1 As TreeNode
        Dim key As String, sql As String

        Dim Destination As New Hashtable

        For Each oSP In oDB_Destination.StoredProcedures
            If Not oSP.SystemObject Then
                Destination.Add(oSP.Name, 0)
            End If
        Next

        For Each oSP In oDB_Source.StoredProcedures
            If Not oSP.SystemObject Then
                If (Destination.ContainsKey(oSP.Name)) Then
                    'Exists in both base
                    Destination.Item(oSP.Name) = 1

                    Node = New TreeNode(oSP.Name)
                    Node1 = New TreeNode(oSP.Name)


                    Node1.ImageIndex = Me.imit_SP
                    Node1.SelectedImageIndex = Me.imit_SP
                    Node.ImageIndex = Me.imit_SP
                    Node.SelectedImageIndex = Me.imit_SP

                    Me.trv_DB_Source.Nodes(Me.ind_SP).Nodes.Add(Node)
                    Me.trv_DB_Destination.Nodes(Me.ind_SP).Nodes.Add(Node1)


                Else
                    'Exists in source base
                    Node = New TreeNode(oSP.Name)
                    Node.ImageIndex = Me.imit_SP_move
                    Node.SelectedImageIndex = Me.imit_SP_move
                    Me.trv_DB_Source.Nodes(Me.ind_SP).Nodes.Add(Node)

                    Node1 = New TreeNode(oSP.Name)
                    Node1.ImageIndex = Me.imit_SP_not_exist
                    Node1.SelectedImageIndex = Me.imit_SP_not_exist
                    Me.trv_DB_Destination.Nodes(Me.ind_SP).Nodes.Add(Node1)

                    Set_Diff(Node.Parent)
                    Set_Diff(Node1.Parent)

                    Compare_Res.Add(Node.FullPath, oSP.Script())
                End If
            End If
        Next

        For Each key In Destination.Keys
            If Destination.Item(key) = 0 Then
                'Exists in destination base
                Node1 = New TreeNode(key)
                Node1.ImageIndex = Me.imit_SP_not_exist
                Node1.SelectedImageIndex = Me.imit_SP_not_exist
                Me.trv_DB_Source.Nodes(Me.ind_SP).Nodes.Add(Node1)

                Node = New TreeNode(key)
                Node.ImageIndex = Me.imit_SP_del
                Node.SelectedImageIndex = Me.imit_SP_del
                Me.trv_DB_Destination.Nodes(Me.ind_SP).Nodes.Add(Node)
                Compare_Res.Add(Node.FullPath, "DROP PROCEDURE  " & key & vbCrLf & "GO")

                Set_Diff(Node.Parent)
                Set_Diff(Node1.Parent)
            End If
        Next

    End Sub
#End Region
#Region "UDF"
    Private Sub Compare_UDF_Permissions(ByRef oSource As SQLDMO.UserDefinedFunction, ByVal Source_Table_Node As TreeNode, ByRef oDestination As SQLDMO.UserDefinedFunction, ByVal Destination_Table_Node As TreeNode)
        Dim oPerm As SQLDMO.Permission2
        Dim oPerm1 As SQLDMO.Permission2
        Dim Node As TreeNode, Node1 As TreeNode
        Dim ns As Integer, nd As Integer, sk_ind As Integer, dk_ind As Integer
        'Dim key As String
        Dim Destination As New Hashtable

        ' ADD COLUMNS
        Node = New TreeNode("Permissions")
        Node.ImageIndex = Me.imit_Folder
        Node.SelectedImageIndex = Me.imit_Folder
        sk_ind = Source_Table_Node.Nodes.Add(Node)

        Node1 = New TreeNode("Permissions")
        Node1.ImageIndex = Me.imit_Folder
        Node1.SelectedImageIndex = Me.imit_Folder
        dk_ind = Destination_Table_Node.Nodes.Add(Node1)


        ' EXEC
        Node = New TreeNode("EXEC")
        Node.ImageIndex = Me.imit_Folder
        Node.SelectedImageIndex = Me.imit_Folder
        ns = Source_Table_Node.Nodes(sk_ind).Nodes.Add(Node)

        Node1 = New TreeNode("EXEC")
        Node1.ImageIndex = Me.imit_Folder
        Node1.SelectedImageIndex = Me.imit_Folder
        nd = Destination_Table_Node.Nodes(dk_ind).Nodes.Add(Node1)

        Compare_Permission(oSource.ListPermissions(SQLDMO.SQLDMO_PRIVILEGE_TYPE.SQLDMOPriv_Execute), Node, oDestination.ListPermissions(SQLDMO.SQLDMO_PRIVILEGE_TYPE.SQLDMOPriv_Execute), Node1)

    End Sub
    Private Sub Compare_UDFs()
        Dim node As TreeNode, dNode As TreeNode
        Dim sql As String, sql1 As String
        Dim oUDF As SQLDMO.UserDefinedFunction, oUDF1 As SQLDMO.UserDefinedFunction

        For Each node In trv_DB_Source.Nodes(ind_UDF).Nodes
            If node.Checked And node.ImageIndex = imit_UDF Then
                tb_Compare_Res.Text = "compare sp: " & node.Text & vbCrLf & tb_Compare_Res.Text
                Application.DoEvents()

                oUDF = oDB_Source.UserDefinedFunctions.Item(node.Text)
                oUDF1 = oDB_Destination.UserDefinedFunctions.Item(node.Text)

                sql = oUDF.Text
                sql1 = oUDF1.Text

                dNode = FindNode(trv_DB_Destination.Nodes, node)

                If String.Compare(Regex.Replace(sql, "\s", ""), Regex.Replace(sql1, "\s", ""), True) <> 0 Then
                    node.ImageIndex = imit_UDF_diff
                    node.SelectedImageIndex = imit_UDF_diff

                    Compare_Res.Add(node.FullPath, regexp_Alter.Replace(oUDF.Script(), "ALTER"))
                End If

                If mi_udfPermissions.Checked Then
                    Compare_UDF_Permissions(oUDF, node, oUDF1, dNode)
                End If
            End If
        Next


    End Sub
    Private Sub Init_UDF()
        Dim oUDF As SQLDMO.UserDefinedFunction
        Dim Node As TreeNode, Node1 As TreeNode
        Dim key As String, sql As String

        Dim Destination As New Hashtable

        For Each oUDF In oDB_Destination.UserDefinedFunctions
            If Not oUDF.SystemObject Then
                Destination.Add(oUDF.Name, 0)
            End If
        Next

        For Each oUDF In oDB_Source.UserDefinedFunctions
            If Not oUDF.SystemObject Then
                'tb_Compare_Res.Text = "sp: " & oUDF.Name & vbCrLf & tb_Compare_Res.Text
                'Application.DoEvents()
                If (Destination.ContainsKey(oUDF.Name)) Then
                    'Exists in both base
                    Destination.Item(oUDF.Name) = 1

                    Node = New TreeNode(oUDF.Name)
                    Node1 = New TreeNode(oUDF.Name)


                    Node1.ImageIndex = Me.imit_UDF
                    Node1.SelectedImageIndex = Me.imit_UDF
                    Node.ImageIndex = Me.imit_UDF
                    Node.SelectedImageIndex = Me.imit_UDF

                    Me.trv_DB_Source.Nodes(Me.ind_UDF).Nodes.Add(Node)
                    Me.trv_DB_Destination.Nodes(Me.ind_UDF).Nodes.Add(Node1)


                Else
                    'Exists in source base
                    Node = New TreeNode(oUDF.Name)
                    Node.ImageIndex = Me.imit_UDF_move
                    Node.SelectedImageIndex = Me.imit_UDF_move
                    Me.trv_DB_Source.Nodes(Me.ind_UDF).Nodes.Add(Node)

                    Node1 = New TreeNode(oUDF.Name)
                    Node1.ImageIndex = Me.imit_UDF_not_exist
                    Node1.SelectedImageIndex = Me.imit_UDF_not_exist
                    Me.trv_DB_Destination.Nodes(Me.ind_UDF).Nodes.Add(Node1)

                    Set_Diff(Node.Parent)
                    Set_Diff(Node1.Parent)

                    Compare_Res.Add(Node.FullPath, oUDF.Script())
                End If
            End If
        Next

        For Each key In Destination.Keys
            If Destination.Item(key) = 0 Then
                'Exists in destination base
                Node1 = New TreeNode(key)
                Node1.ImageIndex = Me.imit_UDF_not_exist
                Node1.SelectedImageIndex = Me.imit_UDF_not_exist
                Me.trv_DB_Source.Nodes(Me.ind_UDF).Nodes.Add(Node1)

                Node = New TreeNode(key)
                Node.ImageIndex = Me.imit_UDF_del
                Node.SelectedImageIndex = Me.imit_UDF_del
                Me.trv_DB_Destination.Nodes(Me.ind_UDF).Nodes.Add(Node)
                Compare_Res.Add(Node.FullPath, "DROP FUNCTION " & key & vbCrLf & "GO")

                Set_Diff(Node.Parent)
                Set_Diff(Node1.Parent)
            End If
        Next

    End Sub
#End Region

    Private Sub Init_Defaults()
        Dim oDefault As SQLDMO.Default2
        Dim Node As TreeNode, Node1 As TreeNode
        Dim key As String, sql As String

        Dim Destination As New Hashtable

        For Each oDefault In oDB_Destination.Defaults
            Destination.Add(oDefault.Name, 0)
        Next

        For Each oDefault In oDB_Source.Defaults
            If (Destination.ContainsKey(oDefault.Name)) Then
                'Exists in both base
                Destination.Item(oDefault.Name) = 1

                Node = New TreeNode(oDefault.Name)
                Node1 = New TreeNode(oDefault.Name)


                Node1.ImageIndex = Me.imit_Default
                Node1.SelectedImageIndex = Me.imit_Default
                Node.ImageIndex = Me.imit_Default
                Node.SelectedImageIndex = Me.imit_Default

                Me.trv_DB_Source.Nodes(Me.ind_Default).Nodes.Add(Node)
                Me.trv_DB_Destination.Nodes(Me.ind_Default).Nodes.Add(Node1)


            Else
                'Exists in source base
                Node = New TreeNode(oDefault.Name)
                Node.ImageIndex = Me.imit_Default_move
                Node.SelectedImageIndex = Me.imit_Default_move
                Me.trv_DB_Source.Nodes(Me.ind_Default).Nodes.Add(Node)

                Node1 = New TreeNode(oDefault.Name)
                Node1.ImageIndex = Me.imit_Default_not_exist
                Node1.SelectedImageIndex = Me.imit_Default_not_exist
                Me.trv_DB_Destination.Nodes(Me.ind_Default).Nodes.Add(Node1)

                Set_Diff(Node.Parent)
                Set_Diff(Node1.Parent)

                Compare_Res.Add(Node.FullPath, oDefault.Script())
            End If
        Next

        For Each key In Destination.Keys
            If Destination.Item(key) = 0 Then
                'Exists in destination base
                Node1 = New TreeNode(key)
                Node1.ImageIndex = Me.imit_Default_not_exist
                Node1.SelectedImageIndex = Me.imit_Default_not_exist
                Me.trv_DB_Source.Nodes(Me.ind_Default).Nodes.Add(Node1)

                Node = New TreeNode(key)
                Node.ImageIndex = Me.imit_Default_del
                Node.SelectedImageIndex = Me.imit_Default_del
                Me.trv_DB_Destination.Nodes(Me.ind_Default).Nodes.Add(Node)
                Compare_Res.Add(Node.FullPath, "DROP DEFAULT " & key & vbCrLf & "GO")

                Set_Diff(Node.Parent)
                Set_Diff(Node1.Parent)
            End If
        Next

    End Sub
    Private Sub Init_Users()
        Dim oUser As SQLDMO.User
        Dim Node As TreeNode, Node1 As TreeNode
        Dim key As String, sql As String

        Dim Destination As New Hashtable

        For Each oUser In oDB_Destination.Users
            Destination.Add(oUser.Name, 0)
        Next

        For Each oUser In oDB_Source.Users
            If (Destination.ContainsKey(oUser.Name)) Then
                'Exists in both base
                Destination.Item(oUser.Name) = 1

                Node = New TreeNode(oUser.Name)
                Node1 = New TreeNode(oUser.Name)


                Node1.ImageIndex = Me.imit_Role
                Node1.SelectedImageIndex = Me.imit_Role
                Node.ImageIndex = Me.imit_Role
                Node.SelectedImageIndex = Me.imit_Role

                Me.trv_DB_Source.Nodes(Me.ind_User).Nodes.Add(Node)
                Me.trv_DB_Destination.Nodes(Me.ind_User).Nodes.Add(Node1)


            Else
                'Exists in source base
                Node = New TreeNode(oUser.Name)
                Node.ImageIndex = Me.imit_Role_move
                Node.SelectedImageIndex = Me.imit_Role_move
                Me.trv_DB_Source.Nodes(Me.ind_User).Nodes.Add(Node)

                Node1 = New TreeNode(oUser.Name)
                Node1.ImageIndex = Me.imit_Role_not_exist
                Node1.SelectedImageIndex = Me.imit_Role_not_exist
                Me.trv_DB_Destination.Nodes(Me.ind_User).Nodes.Add(Node1)

                Set_Diff(Node.Parent)
                Set_Diff(Node1.Parent)

                Compare_Res.Add(Node.FullPath, oUser.Script())
            End If
        Next

        For Each key In Destination.Keys
            If Destination.Item(key) = 0 Then
                'Exists in destination base
                Node1 = New TreeNode(key)
                Node1.ImageIndex = Me.imit_Role_not_exist
                Node1.SelectedImageIndex = Me.imit_Role_not_exist
                Me.trv_DB_Source.Nodes(Me.ind_User).Nodes.Add(Node1)

                Node = New TreeNode(key)
                Node.ImageIndex = Me.imit_Role_del
                Node.SelectedImageIndex = Me.imit_Role_del
                Me.trv_DB_Destination.Nodes(Me.ind_User).Nodes.Add(Node)
                Compare_Res.Add(Node.FullPath, "DROP DEFAULT " & key & vbCrLf & "GO")

                Set_Diff(Node.Parent)
                Set_Diff(Node1.Parent)
            End If
        Next

    End Sub

    Public Sub Compare_DB()
        Dim node As TreeNode

        tb_Compare_Res.Text = "Start compare" & vbCrLf
        For Each node In trv_DB_Source.Nodes
            If node.Checked Then
                Select Case node.Index
                    Case ind_Table
                        Compare_Tables()
                    Case ind_View
                        Compare_Views()
                    Case ind_SP
                        Compare_SPs()
                    Case ind_UDF
                        If mi_UDF.Checked Then
                            Compare_UDFs()
                        End If
                End Select
            End If
        Next

        tb_Compare_Res.Text = "End compare" & vbCrLf & tb_Compare_Res.Text
    End Sub


    Private Sub Init()
        '  Dim oTable As SQLDMO.Table2
        ' Dim DBObj As SQLDMO.DBObject

        If Compare_Res.Count Then
            Compare_Res.Clear()
        End If
        Dim treeNode As TreeNode

        For Each treeNode In trv_DB_Destination.Nodes
            If treeNode.Nodes.Count > 0 Then
                '   node = treeNode
                treeNode.Nodes.Clear()
                treeNode.ImageIndex = Me.imit_Folder
                treeNode.SelectedImageIndex = Me.imit_Folder
            End If
        Next


        For Each treeNode In Me.trv_DB_Source.Nodes
            If treeNode.Nodes.Count > 0 Then
                treeNode.Nodes.Clear()
                treeNode.ImageIndex = Me.imit_Folder
                treeNode.SelectedImageIndex = Me.imit_Folder
            End If
        Next

        trv_DB_Destination.Refresh()
        trv_DB_Source.Refresh()

        Init_Users()
        Init_Defaults()
        Init_Tables()
        Init_Views()
        Init_SP()
        Init_UDF()


    End Sub
#End Region

#Region "Component Messages"

    Private Sub b_Compare_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles b_Compare.Click
        Compare_DB()
    End Sub
    Private Sub b_Execute_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles b_Execute.Click
        Execute()
    End Sub
    Private Sub b_Exec_SQL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles b_Exec_SQL.Click
        Execute_SQL()
    End Sub
    Private Sub b_CheckAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles b_CheckAll.Click
        If Not trv_DB_Source.SelectedNode Is Nothing Then
            CheckAll(trv_DB_Source.SelectedNode)
        End If
    End Sub
    Private Sub b_UnCheckAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles b_UnCheckAll.Click
        If Not trv_DB_Source.SelectedNode Is Nothing Then
            UnCheckAll(trv_DB_Source.SelectedNode)
        End If
    End Sub
    Private Sub b_NextDiff_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles b_NextDiff.Click
        NextDiff_Select()
    End Sub
#Region "Sync Trees"
    Private Sub trv_DB_Source_AfterSelect(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles trv_DB_Source.AfterSelect
        If In_Selecting Then
            Exit Sub
        End If
        In_Selecting = True
        Show_Compare_Res(e.Node)
        trv_DB_Destination.SelectedNode = FindNode(trv_DB_Destination.Nodes, e.Node)
        In_Selecting = False
    End Sub
    Private Sub trv_DB_Destination_AfterSelect(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles trv_DB_Destination.AfterSelect
        If In_Selecting Then
            Exit Sub
        End If
        In_Selecting = True
        Show_Compare_Res(e.Node)
        trv_DB_Source.SelectedNode = FindNode(trv_DB_Source.Nodes, e.Node)
        In_Selecting = False
    End Sub


    Private Sub trv_DB_Source_AfterExpand(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles trv_DB_Source.AfterExpand
        Dim ar(10) As Integer
        Dim node As TreeNode
        Dim i As Integer
        node = e.Node
        i = 0
        ar(i) = node.Index
        While Not node.Parent Is Nothing
            i = i + 1
            node = node.Parent
            ar(i) = node.Index
        End While

        node = trv_DB_Destination.Nodes(ar(i))
        node.Expand()
        i = i - 1
        While (i >= 0)
            node = node.Nodes(ar(i))
            node.Expand()
            i = i - 1
        End While
        trv_DB_Destination.Refresh()

    End Sub
    Private Sub trv_DB_Destination_AfterExpand(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles trv_DB_Destination.AfterExpand
        Dim ar(10) As Integer
        Dim node As TreeNode
        Dim i As Integer
        node = e.Node
        i = 0
        ar(i) = node.Index
        While Not node.Parent Is Nothing
            i = i + 1
            node = node.Parent
            ar(i) = node.Index
        End While

        node = trv_DB_Source.Nodes(ar(i))
        node.Expand()
        i = i - 1
        While (i >= 0)
            node = node.Nodes(ar(i))
            node.Expand()
            i = i - 1
        End While
        node.Expand()
        trv_DB_Source.Refresh()
    End Sub

    Private Sub trv_DB_Source_AfterCollapse(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles trv_DB_Source.AfterCollapse
        If Me.Source_in_collapsing Then
            Exit Sub
        End If
        Me.Source_in_collapsing = True

        Dim ar(10) As Integer
        Dim node As TreeNode
        Dim i As Integer
        node = e.Node
        i = 0
        ar(i) = node.Index
        While Not node.Parent Is Nothing
            i = i + 1
            node = node.Parent
            ar(i) = node.Index
        End While

        node = trv_DB_Destination.Nodes(ar(i))
        'node.Collapse()
        i = i - 1
        While (i >= 0)
            node = node.Nodes(ar(i))
            i = i - 1
        End While
        node.Collapse()


        Me.Source_in_collapsing = False
    End Sub
    Private Sub trv_DB_Destination_AfterCollapse(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles trv_DB_Destination.AfterCollapse
        If Me.Destination_in_collapsing Then
            Exit Sub
        End If

        Dim ar(10) As Integer
        Dim node As TreeNode
        Dim i As Integer
        node = e.Node
        i = 0
        ar(i) = node.Index
        While Not node.Parent Is Nothing
            i = i + 1
            node = node.Parent
            ar(i) = node.Index
        End While

        node = trv_DB_Source.Nodes(ar(i))
        i = i - 1
        While (i >= 0)
            node = node.Nodes(ar(i))
            i = i - 1
        End While
        node.Collapse()


        Me.Destination_in_collapsing = False
    End Sub

    Private Sub trv_DB_Source_Scroll(ByRef m As System.Windows.Forms.Message) Handles trv_DB_Source.Scroll
        Dim msg As Message
        '  Dim lparam As IntPtr = New IntPtr(1)
        msg = m.Create(Me.trv_DB_Destination.Handle, m.Msg, m.WParam, New IntPtr(1))


        Me.trv_DB_Destination.Send_Msg(msg)
    End Sub
    Private Sub trv_DB_Destination_Scroll(ByRef m As System.Windows.Forms.Message) Handles trv_DB_Destination.Scroll
        Dim msg As Message
        msg = m.Create(Me.trv_DB_Source.Handle, m.Msg, m.WParam, New IntPtr(1))

        Me.trv_DB_Source.Send_Msg(msg)
    End Sub

    ' WM_MOUSEWEEL THEORY
    '    #define HANDLE_WM_MOUSEWHEEL(hwnd, wParam, lParam, fn) \
    '    ((fn)((hwnd), (int)(short)LOWORD(lParam), (int)(short)HIWORD(lParam), (int)(short)HIWORD(wParam), (UINT)(short)LOWORD(wParam)), 0L)
    '#define FORWARD_WM_MOUSEWHEEL(hwnd, xPos, yPos, zDelta, fwKeys, fn) \
    '    (void)(fn)((hwnd), WM_MOUSEWHEEL, MAKEWPARAM((fwKeys),(zDelta)), MAKELPARAM((x),(y)))

    '#define MAKEWPARAM(l, h)   
    ' \     ((LONG) (((WORD) (l)) | ((DWORD) ((WORD) (h))) << 16)) 
    Private Sub trv_DB_Destination_MouseWheel(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles trv_DB_Destination.MouseWheel
        If MouseWeel_from_source = True Then
            MouseWeel_from_source = False
            Exit Sub
        End If
        Dim wparam As Long
        Dim lparam As Long
        Dim msg As Message

        wparam = (e.Delta << 16) Or e.Button
        lparam = (e.Y << 16) Or e.X
        msg = msg.Create(Me.trv_DB_Source.Handle, WM_MOUSEWHEEL, New IntPtr(wparam), New IntPtr(lparam))


        MouseWeel_from_destination = True

        Me.trv_DB_Source.Send_Msg(msg)

    End Sub
    Private Sub trv_DB_Source_MouseWheel(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles trv_DB_Source.MouseWheel
        If MouseWeel_from_destination = True Then
            MouseWeel_from_destination = False
            Exit Sub
        End If

        Dim wparam As Long
        Dim lparam As Long
        Dim msg As Message

        wparam = (e.Delta << 16) Or e.Button
        lparam = (e.Y << 16) Or e.X
        msg = msg.Create(Me.trv_DB_Destination.Handle, WM_MOUSEWHEEL, New IntPtr(wparam), New IntPtr(lparam))

        MouseWeel_from_source = True
        Me.trv_DB_Destination.Send_Msg(msg)
    End Sub

#End Region
    Private Sub trv_DB_Source_AfterCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles trv_DB_Source.AfterCheck
        If e.Node Is Nothing Then
            Exit Sub
        End If
        Dim node As TreeNode = e.Node
        While Not node.Parent Is Nothing
            node = node.Parent
            node.Checked = True
        End While
    End Sub
#Region "Compare Settings"
    Private Sub mi_Columns_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mi_Columns.Click
        mi_Columns.Checked = Not mi_Columns.Checked
    End Sub
    Private Sub mi_Keys_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mi_Keys.Click
        mi_Keys.Checked = Not mi_Keys.Checked
    End Sub
    Private Sub mi_Indexes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mi_Indexes.Click
        mi_Indexes.Checked = Not mi_Indexes.Checked
    End Sub
    Private Sub mi_Checks_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mi_Checks.Click
        mi_Checks.Checked = Not mi_Checks.Checked
    End Sub
    Private Sub mi_Triggers_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mi_Triggers.Click
        mi_Triggers.Checked = Not mi_Triggers.Checked
    End Sub
    Private Sub mi_tabPermissions_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mi_tabPermissions.Click
        mi_tabPermissions.Checked = Not mi_tabPermissions.Checked
    End Sub
    Private Sub mi_spPermissions_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mi_spPermissions.Click
        mi_spPermissions.Checked = Not mi_spPermissions.Checked
    End Sub
    Private Sub mi_udfPermissions_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mi_udfPermissions.Click
        mi_udfPermissions.Checked = Not mi_udfPermissions.Checked
    End Sub
    Private Sub mi_viewPermissions_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mi_viewPermissions.Click
        mi_viewPermissions.Checked = Not mi_viewPermissions.Checked
    End Sub
    Private Sub mi_UDF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mi_UDF.Click
        mi_UDF.Checked = Not mi_UDF.Checked
    End Sub
#End Region

#End Region

#Region "SQLDMO Servers messages"
    Private Function oSQLServer_Dest_QueryTimeout(ByVal Message As String) As Boolean Handles oSQLServer_Dest.QueryTimeout
        'QueryTimeout event occurs when MicrosoftÆ SQL Serverô cannot complete execution of a Transact-SQL command batch within a user-defined period of time
        Dim sMsg As String
        sMsg = "QueryTimeout: " & vbCrLf & _
        Message

        MsgBox(sMsg, vbOKOnly, "Timeout SQLServer Object Event")
    End Function
    Private Function oSQLServer_Source_QueryTimeout(ByVal Message As String) As Boolean Handles oSQLServer_Source.QueryTimeout
        'QueryTimeout event occurs when MicrosoftÆ SQL Serverô cannot complete execution of a Transact-SQL command batch within a user-defined period of time
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
        'RemoteLoginFailed event occurs when an instance of MicrosoftÆ SQL Serverô attempts to connect to a remote server fails
        Dim sMsg As String
        sMsg = "RemoteLoginFailed: " & vbCrLf & _
        "Severity: " & Severity & vbCrLf & _
        "MessageNumber: " & MessageNumber & vbCrLf & _
        "MessageState: " & MessageState & vbCrLf & _
        "Message: " & Message

        MsgBox(sMsg, vbOKOnly, "RemoteLoginFailed SQLServer Object Event")
    End Sub
    Private Sub oSQLServer_Source_RemoteLoginFailed(ByVal Severity As Integer, ByVal MessageNumber As Integer, ByVal MessageState As Integer, ByVal Message As String) Handles oSQLServer_Source.RemoteLoginFailed
        'RemoteLoginFailed event occurs when an instance of MicrosoftÆ SQL Serverô attempts to connect to a remote server fails
        Dim sMsg As String
        sMsg = "RemoteLoginFailed: " & vbCrLf & _
        "Severity: " & Severity & vbCrLf & _
        "MessageNumber: " & MessageNumber & vbCrLf & _
        "MessageState: " & MessageState & vbCrLf & _
        "Message: " & Message

        MsgBox(sMsg, vbOKOnly, "RemoteLoginFailed SQLServer Object Event")
    End Sub

    'Private Sub oSQLServer_Dest_ServerMessage(ByVal Severity As Integer, ByVal MessageNumber As Integer, ByVal MessageState As Integer, ByVal Message As String) Handles oSQLServer_Dest.ServerMessage
    '    'ServerMessage event occurs when a MicrosoftÆ SQL Serverô success-with-information message is returned to the SQL-DMO application
    '    Dim sMsg As String
    '    sMsg = "ServerMessage: " & vbCrLf & _
    '    "Severity: " & Severity & vbCrLf & _
    '    "MessageNumber: " & MessageNumber & vbCrLf & _
    '    "MessageState: " & MessageState & vbCrLf & _
    '    "Message: " & Message

    '    MsgBox(sMsg, vbOKOnly, "ServerMessage SQLServer Object Event")
    'End Sub
    'Private Sub oSQLServer_Source_ServerMessage(ByVal Severity As Integer, ByVal MessageNumber As Integer, ByVal MessageState As Integer, ByVal Message As String) Handles oSQLServer_Source.ServerMessage
    '    'ServerMessage event occurs when a MicrosoftÆ SQL Serverô success-with-information message is returned to the SQL-DMO application
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

    Private Function FindNode(ByRef Nodes As TreeNodeCollection, ByRef n As TreeNode) As TreeNode
        Dim ar(10) As Integer
        Dim node As TreeNode
        Dim i As Integer
        node = n
        i = 0
        ar(i) = node.Index
        While Not node.Parent Is Nothing
            i = i + 1
            node = node.Parent
            ar(i) = node.Index
        End While

        FindNode = Nodes(ar(i))
        i = i - 1
        While (i >= 0)
            FindNode = FindNode.Nodes(ar(i))
            i = i - 1
        End While


    End Function

    Private Sub Column_sync(ByVal sColumn As SQLDMO.Column2, ByVal dColumn As SQLDMO.Column2)
        If (sColumn.Rule <> dColumn.Rule) Then
            dColumn.BindRule(sColumn.RuleOwner, sColumn.Rule, True)
        End If
        If (sColumn.Default <> dColumn.Default) Then
            dColumn.BindDefault(sColumn.DefaultOwner, sColumn.Default, True)
        End If

        If (sColumn.AllowNulls <> dColumn.AllowNulls) Then
            dColumn.AllowNulls = sColumn.AllowNulls
        End If

        If (sColumn.Collation <> dColumn.Collation) Then
            dColumn.Collation = sColumn.Collation
        End If

        ' Õ»«ﬂ 
        'If (sColumn.Datatype <> dColumn.Datatype) Then
        '    dColumn.Datatype = sColumn.Datatype
        'End If
    End Sub

    Private Sub Key_sync(ByVal s As SQLDMO.Key, ByVal d As SQLDMO.Key)
        If s.FillFactor <> d.FillFactor Then
            d.FillFactor = s.FillFactor
            d.RebuildIndex()
        End If
        If s.Checked <> d.Checked Then
            d.Checked = s.Checked
        End If

    End Sub
    Private Sub Index_sync(ByVal s As SQLDMO.Index2, ByVal d As SQLDMO.Index2)
        If s.FillFactor <> d.FillFactor Then
            d.FillFactor = s.FillFactor
            d.Rebuild()
        End If

    End Sub

    Public Sub Execute()
        Try
            If Not trv_DB_Source.SelectedNode Is Nothing Then

                Dim sNode As TreeNode = trv_DB_Source.SelectedNode
                Dim dNode As TreeNode = FindNode(trv_DB_Destination.Nodes, sNode)
                Select Case sNode.ImageIndex
                    Case imit_Column_diff
                        Column_sync(oDB_Source.Tables.Item(sNode.Parent.Parent.Text).Columns.Item(sNode.Text), oDB_Destination.Tables.Item(dNode.Parent.Parent.Text).Columns.Item(dNode.Text))
                    Case imit_Key_diff
                        Key_sync(oDB_Source.Tables.Item(sNode.Parent.Parent.Text).Keys.Item(sNode.Text), oDB_Destination.Tables.Item(dNode.Parent.Parent.Text).Keys.Item(dNode.Text))
                    Case imit_Index_diff
                        Index_sync(oDB_Source.Tables.Item(sNode.Parent.Parent.Text).Indexes.Item(sNode.Text), oDB_Destination.Tables.Item(dNode.Parent.Parent.Text).Indexes.Item(dNode.Text))
                    Case imit_Column_move
                        Dim d As SQLDMO.Table2 = oDB_Destination.Tables.Item(dNode.Parent.Parent.Text)
                        d.BeginAlter()
                        Dim dc As New SQLDMO.Column2
                        Dim sc As SQLDMO.Column2 = oDB_Source.Tables.Item(sNode.Parent.Parent.Text).Columns.Item(sNode.Text)
                        dc.Name = sc.Name
                        dc.AllowNulls = True
                        'dc.Collation = sc.Collation
                        dc.ComputedText = sc.ComputedText
                        dc.Datatype = sc.Datatype
                        dc.Identity = sc.Identity
                        dc.IdentityIncrement = sc.IdentityIncrement
                        dc.IdentitySeed = sc.IdentitySeed
                        dc.Length = sc.Length


                        d.Columns.Add(dc)
                        d.DoAlter()
                    Case Else
                        oDB_Destination.ExecuteImmediate(tb_Compare_Res.Text)

                End Select





                Compare_Res.Remove(sNode.FullPath)
                If dNode.ImageIndex = imit_Check_del Or _
                dNode.ImageIndex = imit_Column_del Or _
                dNode.ImageIndex = imit_Index_del Or _
                dNode.ImageIndex = imit_Key_del Or _
                dNode.ImageIndex = imit_Role_del Or _
                dNode.ImageIndex = imit_Table_del Or _
                dNode.ImageIndex = imit_Trigger_del Or _
                dNode.ImageIndex = imit_View_del Then 'Delete Object
                    sNode.Remove()
                    dNode.Remove()
                Else
                    sNode.ImageIndex = ImagToType(sNode.ImageIndex)
                    sNode.SelectedImageIndex = sNode.ImageIndex
                    dNode.ImageIndex = sNode.ImageIndex
                    dNode.SelectedImageIndex = sNode.ImageIndex

                End If


                NextDiff_Select()

            Else
                MsgBox("Please select element")
            End If
            '     node.ImageIndex = imit_SP_move Or _node.ImageIndex = imit_UDF_move Or _

        Catch ex As Exception
            tb_Compare_Res.Text = "Error" & ex.Source & ": " & ex.Message & ex.HelpLink
        End Try

    End Sub
    Public Sub Execute_SQL()
        Try
            If Not trv_DB_Source.SelectedNode Is Nothing Then

                Dim sNode As TreeNode = trv_DB_Source.SelectedNode
                Dim dNode As TreeNode = FindNode(trv_DB_Destination.Nodes, sNode)

                oDB_Destination.ExecuteImmediate(tb_Compare_Res.Text)





                Compare_Res.Remove(sNode.FullPath)
                If dNode.ImageIndex = imit_Check_del Or _
                dNode.ImageIndex = imit_Column_del Or _
                dNode.ImageIndex = imit_Index_del Or _
                dNode.ImageIndex = imit_Key_del Or _
                dNode.ImageIndex = imit_Role_del Or _
                dNode.ImageIndex = imit_Table_del Or _
                dNode.ImageIndex = imit_Trigger_del Or _
                dNode.ImageIndex = imit_View_del Then 'Delete Object
                    sNode.Remove()
                    dNode.Remove()
                Else
                    sNode.ImageIndex = ImagToType(sNode.ImageIndex)
                    sNode.SelectedImageIndex = sNode.ImageIndex
                    dNode.ImageIndex = sNode.ImageIndex
                    dNode.SelectedImageIndex = sNode.ImageIndex

                End If


                NextDiff_Select()

            Else
                MsgBox("Please select element")
            End If


        Catch ex As Exception
            tb_Compare_Res.Text = "Error" & ex.Source & ": " & ex.Message & ex.HelpLink
        End Try

    End Sub
    Public Sub CheckAllObj()
        Dim n As TreeNode
        For Each n In trv_DB_Source.Nodes
            CheckAll(n)
        Next
    End Sub
    Private Sub CheckAll(ByRef node As TreeNode)
        Dim n As TreeNode
        node.Checked = True
        For Each n In node.Nodes

            CheckAll(n)
        Next

    End Sub
    Private Sub UnCheckAll(ByRef node As TreeNode)
        Dim n As TreeNode
        node.Checked = False
        For Each n In node.Nodes
            UnCheckAll(n)
        Next

    End Sub

    Private Function IsDiff(ByVal node As TreeNode) As Boolean
        IsDiff = True

        If node.ImageIndex = imit_Check Or _
            node.ImageIndex = imit_Column Or _
            node.ImageIndex = imit_Folder Or _
            node.ImageIndex = imit_Index Or _
            node.ImageIndex = imit_Key Or _
            node.ImageIndex = imit_Role Or _
            node.ImageIndex = imit_SP Or _
            node.ImageIndex = imit_Table Or _
            node.ImageIndex = imit_Trigger Or _
            node.ImageIndex = imit_UDF Or _
            node.ImageIndex = imit_View Or _
            node.ImageIndex = imit_Empty Or _
            node.ImageIndex = -1 Then
            IsDiff = False

        End If
    End Function
    Private Function FindDiff(ByVal node As TreeNode) As TreeNode
        Dim n As TreeNode, n1 As TreeNode
        For Each n In node.Nodes
            If (IsDiff(n)) Then
                FindDiff = n
                Exit Function
            Else
                n1 = FindDiff(n)
                If (Not n1 Is Nothing) Then
                    FindDiff = n1
                    Exit Function
                End If
            End If
        Next
    End Function
    Public Sub NextDiff_Select()
        Dim node As TreeNode, n As TreeNode
        If trv_DB_Source.SelectedNode Is Nothing Then
            node = trv_DB_Source.TopNode
        Else
            node = trv_DB_Source.SelectedNode
        End If
search_again:
        ' ÕÓ‰ ËÏÂÂÚ ‡ÁÎË˜ËÂ
        If IsDiff(node) And node.Nodes.Count > 0 Then
            'Ë˘ÂÏ ‚ ÔÂ‰Í‡ı
            n = FindDiff(node)
        End If

        If n Is Nothing Then
            'Ë˘ÂÏ ‚ ÒÎÂ‰Û˛˘Ëı
search_in_nextNode:
            While Not node.NextNode Is Nothing
                node = node.NextNode
                If IsDiff(node) Then
                    n = node
                    Exit While
                End If
            End While

        End If
        'ÌÂ Ì‡¯ÎË ÔÓ‰˚Ï‡ÂÏÒˇ ‚ ‚Âı Ì‡ ÛÓ‚ÂÌ¸
        If (n Is Nothing) And (Not node.Parent Is Nothing) Then
            node = node.Parent
            GoTo search_in_nextNode
        End If

        If Not n Is Nothing Then
            trv_DB_Source.SelectedNode = n
            If n.ImageIndex = imit_Folder_diff Or n.ImageIndex = imit_Table_diff Or _
            Compare_Res(n.FullPath) Is Nothing Then
                node = n
                GoTo search_again
            End If
            trv_DB_Source.Focus()
        Else
            MsgBox("End Difference")
        End If
    End Sub



    Private Sub mi_IgnList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim f As New frm_IgnoreList
        f.con = Src_Con
        f.Ign_User = act.IgnUsr
        f.ShowDialog()
    End Sub

    Private Sub mi_SavesAsAct_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mi_SavesAsAct.Click
        SFD.InitialDirectory = Application.StartupPath
        If SFD.ShowDialog = DialogResult.OK Then
            Dim act As New CompareAction
            Dim s As SQLDMO.SQLServer2
            Try
                act.DstCon = New Connection
                act.SrcCon = New Connection

                act.SrcCon.DataBase = oDB_Source.Name
                s = oDB_Source.Parent
                act.SrcCon.DataBase = s.Login
                act.SrcCon.Server = s.Name
                act.SrcCon.Password = s.Password
                act.SrcCon.User = s.Login

                act.DstCon.DataBase = oDB_Destination.Name
                s = oDB_Destination.Parent
                act.DstCon.DataBase = s.Login
                act.DstCon.Server = s.Name
                act.DstCon.Password = s.Password
                act.DstCon.User = s.Login

                'act.IgnUsr = ar_Ign_User

                Dim myWriter As System.IO.StreamWriter


                Dim DBOptSerializer = New System.Xml.Serialization.XmlSerializer(GetType(CompareAction))
                myWriter = New System.IO.StreamWriter(SFD.FileName)
                DBOptSerializer.Serialize(myWriter, act)
                myWriter.Close()


            Catch ex As Exception
                MsgBox(" Error save action: " & ex.Source & vbCrLf & ex.Message & ex.HelpLink)
            End Try
        End If
    End Sub
    Private Sub mi_View_Action_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mi_View_Action.Click
        Dim f As New frm_Comp_Act
        f.con = Src_Con
        f.action = Me.act
        f.ShowDialog()

    End Sub


End Class


Public Class Connection
    Public Server As String
    Public User As String
    Public Password As String
    Public DataBase As String
End Class
Public Class CompareAction
    Public SrcCon As Connection
    Public DstCon As Connection
    <XmlArrayItem(ElementName:="User", Type:=GetType(String)), _
    XmlArray()> _
    Public IgnUsr As ArrayList

    <XmlArrayItem(ElementName:="Opt", Type:=GetType(String)), _
     XmlArray()> _
    Public Options As Hashtable

End Class
