Imports System.IO
Public Class cFileBrowser
    Inherits System.Windows.Forms.UserControl

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() callOr NotifyFilters.DirectoryName 
        Me.FSW.NotifyFilter = NotifyFilters.CreationTime Or NotifyFilters.LastAccess Or NotifyFilters.LastWrite Or NotifyFilters.Attributes Or NotifyFilters.Size Or NotifyFilters.Security

        Me.liDrive.Drive = "c:\"
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
    Friend WithEvents liDrive As Microsoft.VisualBasic.Compatibility.VB6.DriveListBox
    Friend WithEvents imObj As System.Windows.Forms.ImageList
    Friend WithEvents tvObj As System.Windows.Forms.TreeView
    Friend WithEvents FSW As System.IO.FileSystemWatcher
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(cFileBrowser))
        Me.liDrive = New Microsoft.VisualBasic.Compatibility.VB6.DriveListBox()
        Me.tvObj = New System.Windows.Forms.TreeView()
        Me.imObj = New System.Windows.Forms.ImageList(Me.components)
        Me.FSW = New System.IO.FileSystemWatcher()
        CType(Me.FSW, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'liDrive
        '
        Me.liDrive.Dock = System.Windows.Forms.DockStyle.Top
        Me.liDrive.Name = "liDrive"
        Me.liDrive.Size = New System.Drawing.Size(192, 21)
        Me.liDrive.TabIndex = 0
        '
        'tvObj
        '
        Me.tvObj.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.tvObj.ImageList = Me.imObj
        Me.tvObj.Location = New System.Drawing.Point(0, 24)
        Me.tvObj.Name = "tvObj"
        Me.tvObj.Size = New System.Drawing.Size(192, 176)
        Me.tvObj.TabIndex = 1
        '
        'imObj
        '
        Me.imObj.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit
        Me.imObj.ImageSize = New System.Drawing.Size(16, 16)
        Me.imObj.ImageStream = CType(resources.GetObject("imObj.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imObj.TransparentColor = System.Drawing.Color.Transparent
        '
        'FSW
        '
        Me.FSW.EnableRaisingEvents = True
        Me.FSW.Filter = ""
        Me.FSW.IncludeSubdirectories = True
        Me.FSW.Path = "C:\"
        Me.FSW.SynchronizingObject = Me
        '
        'cFileBrowser
        '
        Me.Controls.AddRange(New System.Windows.Forms.Control() {Me.tvObj, Me.liDrive})
        Me.Name = "cFileBrowser"
        Me.Size = New System.Drawing.Size(192, 200)
        CType(Me.FSW, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region
    Private Sub LoadDir(ByRef nodes As TreeNodeCollection, ByVal path As String)

        'Dim fse As String() = Directory.GetFileSystemEntries(path)
        Try
            Dim fe As String
            Dim fse As String() = Directory.GetDirectories(path)
            Dim n As TreeNode
            Dim l As TreeNode
            For Each fe In fse
                n = New TreeNode()
                n.Text = System.IO.Path.GetFileName(fe)
                n.ImageIndex = 0
                n.SelectedImageIndex = 0
                l = New TreeNode()
                l.Text = "#"
                l.ImageIndex = 0
                l.SelectedImageIndex = 0
                n.Nodes.Add(l)
                nodes.Add(n)
            Next
            fse = Directory.GetFiles(path)
            For Each fe In fse
                n = New TreeNode()
                n.Text = System.IO.Path.GetFileName(fe)
                n.ImageIndex = 1
                n.SelectedImageIndex = 1
                nodes.Add(n)
            Next
        Catch ex As Exception
            MsgBox(ex.Message)

        End Try

    End Sub

    Private Sub liDrive_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles liDrive.SelectedValueChanged
        Me.tvObj.Nodes.Clear()
        LoadDir(Me.tvObj.Nodes, Me.liDrive.Drive.Substring(0, 2) & "\")
        Try
            Me.FSW.Path = Me.liDrive.Drive.Substring(0, 2) & "\"
        Catch ex As Exception
            MsgBox(ex.Message)

        End Try


    End Sub

    Private Sub tvObj_BeforeExpand(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewCancelEventArgs) Handles tvObj.BeforeExpand
        If e.Node.Nodes.Item(0).Text = "#" Then
            e.Node.Nodes.Item(0).Remove()
            Me.LoadDir(e.Node.Nodes, Me.liDrive.Drive.Substring(0, 2) & "\" & e.Node.FullPath)
        End If
    End Sub

    Private Sub FSW_Changed(ByVal sender As Object, ByVal e As System.IO.FileSystemEventArgs) Handles FSW.Changed
        Dim n As TreeNodeCollection = Me.GetTreeNodeColByPath(System.IO.Path.GetDirectoryName(e.FullPath))
        n.Clear()
        Me.LoadDir(n, System.IO.Path.GetDirectoryName(e.FullPath))
    End Sub

    Private Function GetTreeNodeColByPath(ByVal path As String) As TreeNodeCollection
        Dim s As String() = path.Split("\")
        Dim i As Integer
        Dim nodes As TreeNodeCollection
        nodes = Me.tvObj.Nodes
        Dim n As TreeNode

        For i = 1 To s.Length - 1
            For Each n In nodes
                If String.Compare(n.Text, s(i), True) = 0 Then
                    nodes = n.Nodes
                    Exit For
                End If
            Next
        Next
        GetTreeNodeColByPath = nodes
    End Function




End Class
