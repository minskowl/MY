Imports System.Data.SqlClient
Public Class CompareDataControl
    Inherits System.Windows.Forms.UserControl
    Private srcCS As String
    Private destCS As String

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
    Friend WithEvents cb_Table As System.Windows.Forms.ComboBox
    Friend WithEvents b_Compare As System.Windows.Forms.Button
    Friend WithEvents dsData As System.Data.DataSet
    Friend WithEvents dt_tabs As System.Data.DataTable
    Friend WithEvents dt_src As System.Data.DataTable
    Friend WithEvents dt_dest As System.Data.DataTable
    Friend WithEvents fg_Data As C1.Win.C1FlexGrid.C1FlexGrid
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.cb_Table = New System.Windows.Forms.ComboBox
        Me.dt_tabs = New System.Data.DataTable
        Me.b_Compare = New System.Windows.Forms.Button
        Me.dsData = New System.Data.DataSet
        Me.dt_src = New System.Data.DataTable
        Me.dt_dest = New System.Data.DataTable
        Me.fg_Data = New C1.Win.C1FlexGrid.C1FlexGrid
        CType(Me.dt_tabs, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dsData, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dt_src, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dt_dest, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.fg_Data, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cb_Table
        '
        Me.cb_Table.DataSource = Me.dt_tabs
        Me.cb_Table.Location = New System.Drawing.Point(8, 8)
        Me.cb_Table.Name = "cb_Table"
        Me.cb_Table.Size = New System.Drawing.Size(121, 21)
        Me.cb_Table.TabIndex = 1
        '
        'dt_tabs
        '
        Me.dt_tabs.TableName = "tabs"
        '
        'b_Compare
        '
        Me.b_Compare.Location = New System.Drawing.Point(136, 8)
        Me.b_Compare.Name = "b_Compare"
        Me.b_Compare.TabIndex = 2
        Me.b_Compare.Text = "Compare"
        '
        'dsData
        '
        Me.dsData.DataSetName = "NewDataSet"
        Me.dsData.Locale = New System.Globalization.CultureInfo("ru-RU")
        Me.dsData.Tables.AddRange(New System.Data.DataTable() {Me.dt_tabs, Me.dt_src, Me.dt_dest})
        '
        'dt_src
        '
        Me.dt_src.TableName = "src"
        '
        'dt_dest
        '
        Me.dt_dest.TableName = "dest"
        '
        'fg_Data
        '
        Me.fg_Data.ColumnInfo = "10,1,0,0,0,85,Columns:"
        Me.fg_Data.Location = New System.Drawing.Point(16, 40)
        Me.fg_Data.Name = "fg_Data"
        Me.fg_Data.Size = New System.Drawing.Size(584, 256)
        Me.fg_Data.Styles = New C1.Win.C1FlexGrid.CellStyleCollection("Normal{Font:Microsoft Sans Serif, 8.25pt;}" & Microsoft.VisualBasic.ChrW(9) & "Fixed{BackColor:Control;ForeColor:Cont" & _
        "rolText;Border:Flat,1,ControlDark,Both;}" & Microsoft.VisualBasic.ChrW(9) & "Highlight{BackColor:Highlight;ForeColor" & _
        ":HighlightText;}" & Microsoft.VisualBasic.ChrW(9) & "Search{BackColor:Highlight;ForeColor:HighlightText;}" & Microsoft.VisualBasic.ChrW(9) & "Frozen{Bac" & _
        "kColor:Beige;}" & Microsoft.VisualBasic.ChrW(9) & "EmptyArea{BackColor:AppWorkspace;Border:Flat,1,ControlDarkDark,Bo" & _
        "th;}" & Microsoft.VisualBasic.ChrW(9) & "GrandTotal{BackColor:Black;ForeColor:White;}" & Microsoft.VisualBasic.ChrW(9) & "Subtotal0{BackColor:ControlDar" & _
        "kDark;ForeColor:White;}" & Microsoft.VisualBasic.ChrW(9) & "Subtotal1{BackColor:ControlDarkDark;ForeColor:White;}" & Microsoft.VisualBasic.ChrW(9) & "Su" & _
        "btotal2{BackColor:ControlDarkDark;ForeColor:White;}" & Microsoft.VisualBasic.ChrW(9) & "Subtotal3{BackColor:ControlD" & _
        "arkDark;ForeColor:White;}" & Microsoft.VisualBasic.ChrW(9) & "Subtotal4{BackColor:ControlDarkDark;ForeColor:White;}" & Microsoft.VisualBasic.ChrW(9) & _
        "Subtotal5{BackColor:ControlDarkDark;ForeColor:White;}" & Microsoft.VisualBasic.ChrW(9))
        Me.fg_Data.TabIndex = 3
        '
        'CompareDataControl
        '
        Me.Controls.Add(Me.fg_Data)
        Me.Controls.Add(Me.b_Compare)
        Me.Controls.Add(Me.cb_Table)
        Me.Name = "CompareDataControl"
        Me.Size = New System.Drawing.Size(640, 320)
        CType(Me.dt_tabs, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dsData, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dt_src, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dt_dest, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.fg_Data, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region
    Sub Init(ByVal SourceCS As String, ByVal DestCS As String)

        Dim sqlCon As New SqlConnection
        srcCS = SourceCS
        Me.destCS = DestCS
        sqlCon.ConnectionString = SourceCS
        sqlCon.Open()

        Dim sqlAdapter As New SqlDataAdapter("SELECT id,name FROM sysobjects where [xtype]='U' order by name", sqlCon)
        sqlAdapter.Fill(dsData.Tables.Item("tabs"))

        sqlCon.Close()
        cb_Table.ValueMember = "name"
        cb_Table.DisplayMember = "name"
    End Sub

    Private Sub b_Compare_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles b_Compare.Click
        Dim sqlCon As New SqlConnection
        sqlCon.ConnectionString = srcCS
        sqlCon.Open()

        Dim sqlAdapter As New SqlDataAdapter("SELECT * FROM " & cb_Table.Text, sqlCon)
        sqlAdapter.Fill(dsData.Tables.Item("src"))

        sqlCon.Close()

        sqlCon.ConnectionString = destCS
        sqlCon.Open()

        sqlAdapter.Fill(dsData.Tables.Item("dest"))

        sqlCon.Close()
        Dim colName As String
        Dim i As Integer
        While dsData.Tables.Item(1).Columns.Count > fg_Data.Cols.Count
            fg_Data.Cols.Add()
        End While

        For i = 0 To dsData.Tables.Item(1).Columns.Count - 1
            fg_Data(0, i) = dsData.Tables.Item(1).Columns.Item(i).ColumnName
        Next

    End Sub
End Class
