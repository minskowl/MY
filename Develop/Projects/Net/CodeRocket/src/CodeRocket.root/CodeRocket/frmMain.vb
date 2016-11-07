Imports System.Collections.Generic
Imports System
Imports System.IO
Imports Savchin.Controls.Browsers
Imports Savchin.Controls.Common
Imports Savchin.CodeGeneration
Imports Savchin.CodeGeneration.Common

Public Class frmMain
    Inherits System.Windows.Forms.Form

#Region "Fields"




    Private Const fileFiler As String = "Generation projects|*" & GenerateProject.FileExtension & _
                "|XML|*.xml|ALL|*.*"

    Private ProjectTypeToPages As New Dictionary(Of ProjectType, List(Of Control))
    Private settingsInstance As Settings
    Private BookMarks As New Dictionary(Of String, BookMark)
    Private assemblyGenerator As New AssemblyGenerator





    Friend WithEvents TabControlBrowsers As System.Windows.Forms.TabControl
    Friend WithEvents TabPageAssembly As System.Windows.Forms.TabPage
    Friend WithEvents browserAssembly As AssemblyBrowser
    Friend WithEvents CPDBrowser1 As Savchin.Controls.Browsers.PDBrowser
    Friend WithEvents TabControl2 As FileTabControl
    Friend WithEvents TabPageProject As System.Windows.Forms.TabPage
    Friend WithEvents GeneratorProjectBrowser1 As Savchin.CodeGeneration.GeneratorProjectBrowser
    Friend WithEvents TabPageSchema As System.Windows.Forms.TabPage
    Friend WithEvents SchemaBrowser1 As Savchin.Data.Schema.Controls.SchemaBrowser
    Friend WithEvents TabPagePowerDesigner As System.Windows.Forms.TabPage

    Friend WithEvents VertMainContainer As System.Windows.Forms.SplitContainer
    Friend WithEvents VertContainer As System.Windows.Forms.SplitContainer
    Friend WithEvents HorContainer As System.Windows.Forms.SplitContainer


    Friend WithEvents TabFiles As FileTabControl
    Friend WithEvents MenuItemSave As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemSaveAs As System.Windows.Forms.MenuItem
    Friend WithEvents sfd As System.Windows.Forms.SaveFileDialog
    Friend WithEvents MenuItemClose As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem2 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem3 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemAbout As System.Windows.Forms.MenuItem
    Friend WithEvents PropertyGridControl As PropertyGrid

#End Region

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        ofd.Filter = fileFiler
        sfd.Filter = fileFiler


        VertContainer = New System.Windows.Forms.SplitContainer
        TabFiles = New Savchin.Controls.Common.FileTabControl
        HorContainer = New System.Windows.Forms.SplitContainer
        VertMainContainer = New System.Windows.Forms.SplitContainer
        PropertyGridControl = New PropertyGrid

        VertContainer.Panel1.SuspendLayout()
        VertContainer.Panel2.SuspendLayout()
        VertContainer.SuspendLayout()


        HorContainer.Panel1.SuspendLayout()
        HorContainer.SuspendLayout()
        VertMainContainer.SuspendLayout()
        PropertyGridControl.SuspendLayout()


        AddBrowserTabs()
        '
        'VertContainer
        '
        VertContainer.Dock = System.Windows.Forms.DockStyle.Fill
        'VertContainer.Location = New System.Drawing.Point(0, 100)
        VertContainer.Name = "VertContainer"
        '
        'VertContainer.Panel1
        '

        VertContainer.Panel1MinSize = 150
        '
        'VertContainer.Panel2
        '
        VertContainer.Panel2.Controls.Add(TabFiles)
        VertContainer.Panel2MinSize = 100
        VertContainer.Size = New System.Drawing.Size(724, 100)
        VertContainer.SplitterDistance = 240
        VertContainer.TabIndex = 1


        '
        'TabFiles
        '
        TabFiles.Dock = System.Windows.Forms.DockStyle.Fill
        TabFiles.Location = New System.Drawing.Point(0, 0)
        TabFiles.Name = "TabFiles"
        TabFiles.SelectedIndex = 0
        TabFiles.Size = New System.Drawing.Size(480, 100)
        TabFiles.TabIndex = 0
        TabFiles.WordWrap = False
        '
        'HorContainer
        '
        HorContainer.Location = New System.Drawing.Point(57, 49)
        HorContainer.Name = "HorContainer"
        HorContainer.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'HorContainer.Panel1
        '
        HorContainer.Panel1.Controls.Add(VertMainContainer)
        'HorContainer.Panel1.Controls.Add(VertContainer)
        HorContainer.Size = New System.Drawing.Size(724, 246)
        HorContainer.SplitterDistance = 200
        HorContainer.TabIndex = 2
        HorContainer.Dock = DockStyle.Fill

        '
        'PropertyGrid
        '
        PropertyGridControl.Dock = DockStyle.Fill
        '
        'VertMainContainer
        '
        VertMainContainer.Dock = System.Windows.Forms.DockStyle.Fill
        VertMainContainer.Location = New System.Drawing.Point(0, 0)
        VertMainContainer.Name = "SplitContainer1"
        VertMainContainer.Size = New System.Drawing.Size(724, 100)
        VertMainContainer.SplitterDistance = Me.Width - Convert.ToInt32(Me.Width / 3)


        VertMainContainer.TabIndex = 2

        VertMainContainer.Panel1.Controls.Add(VertContainer)
        VertMainContainer.Panel2.Controls.Add(PropertyGridControl)




        'Full

        Controls.Add(HorContainer)

        VertContainer.Panel1.ResumeLayout(False)
        VertContainer.Panel2.ResumeLayout(False)
        VertContainer.ResumeLayout(False)


        HorContainer.Panel1.ResumeLayout(False)
        HorContainer.ResumeLayout(False)
        VertMainContainer.ResumeLayout(False)
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
    Friend WithEvents mmMain As System.Windows.Forms.MainMenu
    Friend WithEvents miEdit As System.Windows.Forms.MenuItem
    Friend WithEvents miCut As System.Windows.Forms.MenuItem
    Friend WithEvents miCopy As System.Windows.Forms.MenuItem
    Friend WithEvents miPaste As System.Windows.Forms.MenuItem
    Friend WithEvents miDelete As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem6 As System.Windows.Forms.MenuItem
    Friend WithEvents miSelectAll As System.Windows.Forms.MenuItem
    Friend WithEvents miFile As System.Windows.Forms.MenuItem
    Friend WithEvents miAssemblyLoad As System.Windows.Forms.MenuItem
    Friend WithEvents ofd As System.Windows.Forms.OpenFileDialog
    Friend WithEvents miSettings As System.Windows.Forms.MenuItem
    Friend WithEvents fbd As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents miHelp As System.Windows.Forms.MenuItem
    Friend WithEvents miGenerateToOutPutDir As System.Windows.Forms.MenuItem
    Friend WithEvents miGenerateToSolutionDir As System.Windows.Forms.MenuItem
    Friend WithEvents miGenerateToSolution As System.Windows.Forms.MenuItem




    Friend WithEvents miGenerate As System.Windows.Forms.MenuItem
    Friend WithEvents miAssemblyBookmarks As System.Windows.Forms.MenuItem
    Friend WithEvents mProjectOpen As System.Windows.Forms.MenuItem
    Friend WithEvents miRecentProjects As System.Windows.Forms.MenuItem



    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemNewAssemblyProject As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemNewPowerDesignerProject As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemNewSchemaProject As System.Windows.Forms.MenuItem

    Friend WithEvents miWordWarp As System.Windows.Forms.MenuItem
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        Me.mmMain = New System.Windows.Forms.MainMenu(Me.components)
        Me.miFile = New System.Windows.Forms.MenuItem
        Me.MenuItem1 = New System.Windows.Forms.MenuItem
        Me.MenuItemNewAssemblyProject = New System.Windows.Forms.MenuItem
        Me.MenuItemNewPowerDesignerProject = New System.Windows.Forms.MenuItem
        Me.MenuItemNewSchemaProject = New System.Windows.Forms.MenuItem
        Me.mProjectOpen = New System.Windows.Forms.MenuItem
        Me.MenuItemClose = New System.Windows.Forms.MenuItem
        Me.MenuItem2 = New System.Windows.Forms.MenuItem
        Me.MenuItemSave = New System.Windows.Forms.MenuItem
        Me.MenuItemSaveAs = New System.Windows.Forms.MenuItem
        Me.MenuItem3 = New System.Windows.Forms.MenuItem
        Me.miAssemblyLoad = New System.Windows.Forms.MenuItem
        Me.miSettings = New System.Windows.Forms.MenuItem
        Me.miRecentProjects = New System.Windows.Forms.MenuItem
        Me.miEdit = New System.Windows.Forms.MenuItem
        Me.miCut = New System.Windows.Forms.MenuItem
        Me.miCopy = New System.Windows.Forms.MenuItem
        Me.miPaste = New System.Windows.Forms.MenuItem
        Me.miDelete = New System.Windows.Forms.MenuItem
        Me.MenuItem6 = New System.Windows.Forms.MenuItem
        Me.miSelectAll = New System.Windows.Forms.MenuItem
        Me.miWordWarp = New System.Windows.Forms.MenuItem
        Me.miAssemblyBookmarks = New System.Windows.Forms.MenuItem
        Me.miGenerate = New System.Windows.Forms.MenuItem
        Me.miGenerateToOutPutDir = New System.Windows.Forms.MenuItem
        Me.miGenerateToSolutionDir = New System.Windows.Forms.MenuItem
        Me.miGenerateToSolution = New System.Windows.Forms.MenuItem
        Me.miHelp = New System.Windows.Forms.MenuItem
        Me.MenuItemAbout = New System.Windows.Forms.MenuItem
        Me.ofd = New System.Windows.Forms.OpenFileDialog
        Me.fbd = New System.Windows.Forms.FolderBrowserDialog
        Me.sfd = New System.Windows.Forms.SaveFileDialog
        Me.SuspendLayout()
        '
        'mmMain
        '
        Me.mmMain.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.miFile, Me.miEdit, Me.miAssemblyBookmarks, Me.miGenerate, Me.miHelp})
        '
        'miFile
        '
        Me.miFile.Index = 0
        Me.miFile.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem1, Me.mProjectOpen, Me.MenuItemClose, Me.MenuItem2, Me.MenuItemSave, Me.MenuItemSaveAs, Me.MenuItem3, Me.miAssemblyLoad, Me.miSettings, Me.miRecentProjects})
        Me.miFile.Text = "&File"
        '
        'MenuItem1
        '
        Me.MenuItem1.Index = 0
        Me.MenuItem1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItemNewAssemblyProject, Me.MenuItemNewPowerDesignerProject, Me.MenuItemNewSchemaProject})
        Me.MenuItem1.Text = "New"
        '
        'MenuItemNewAssemblyProject
        '
        Me.MenuItemNewAssemblyProject.Index = 0
        Me.MenuItemNewAssemblyProject.Text = "Assebly Project"
        '
        'MenuItemNewPowerDesignerProject
        '
        Me.MenuItemNewPowerDesignerProject.Index = 1
        Me.MenuItemNewPowerDesignerProject.Text = "PowerDesigner Project"
        '
        'MenuItemNewSchemaProject
        '
        Me.MenuItemNewSchemaProject.Index = 2
        Me.MenuItemNewSchemaProject.Text = "Schema Project"
        '
        'mProjectOpen
        '
        Me.mProjectOpen.Index = 1
        Me.mProjectOpen.Text = "Open Project"
        '
        'MenuItemClose
        '
        Me.MenuItemClose.Index = 2
        Me.MenuItemClose.Text = "Close"
        '
        'MenuItem2
        '
        Me.MenuItem2.Index = 3
        Me.MenuItem2.Text = "-"
        '
        'MenuItemSave
        '
        Me.MenuItemSave.Index = 4
        Me.MenuItemSave.Shortcut = System.Windows.Forms.Shortcut.CtrlS
        Me.MenuItemSave.Text = "Save"
        '
        'MenuItemSaveAs
        '
        Me.MenuItemSaveAs.Index = 5
        Me.MenuItemSaveAs.Text = "Save As ..."
        '
        'MenuItem3
        '
        Me.MenuItem3.Index = 6
        Me.MenuItem3.Text = "-"
        '
        'miAssemblyLoad
        '
        Me.miAssemblyLoad.Index = 7
        Me.miAssemblyLoad.Text = "Load Assembly"
        '
        'miSettings
        '
        Me.miSettings.Index = 8
        Me.miSettings.Text = "Settings"
        '
        'miRecentProjects
        '
        Me.miRecentProjects.Index = 9
        Me.miRecentProjects.Text = "Recent Projects"
        '
        'miEdit
        '
        Me.miEdit.Index = 1
        Me.miEdit.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.miCut, Me.miCopy, Me.miPaste, Me.miDelete, Me.MenuItem6, Me.miSelectAll, Me.miWordWarp})
        Me.miEdit.Text = "&Edit"
        '
        'miCut
        '
        Me.miCut.Index = 0
        Me.miCut.Shortcut = System.Windows.Forms.Shortcut.CtrlX
        Me.miCut.Text = "Cu&t"
        '
        'miCopy
        '
        Me.miCopy.Index = 1
        Me.miCopy.Shortcut = System.Windows.Forms.Shortcut.CtrlC
        Me.miCopy.Text = "&Copy"
        '
        'miPaste
        '
        Me.miPaste.Index = 2
        Me.miPaste.Shortcut = System.Windows.Forms.Shortcut.CtrlV
        Me.miPaste.Text = "&Paste"
        '
        'miDelete
        '
        Me.miDelete.Index = 3
        Me.miDelete.Shortcut = System.Windows.Forms.Shortcut.Del
        Me.miDelete.Text = "&Delete"
        '
        'MenuItem6
        '
        Me.MenuItem6.Index = 4
        Me.MenuItem6.Text = "-"
        '
        'miSelectAll
        '
        Me.miSelectAll.Index = 5
        Me.miSelectAll.Shortcut = System.Windows.Forms.Shortcut.CtrlA
        Me.miSelectAll.Text = "Select &All"
        '
        'miWordWarp
        '
        Me.miWordWarp.Index = 6
        Me.miWordWarp.Text = "WordWarp"
        '
        'miAssemblyBookmarks
        '
        Me.miAssemblyBookmarks.Index = 2
        Me.miAssemblyBookmarks.Text = "BookMarks"
        '
        'miGenerate
        '
        Me.miGenerate.Enabled = False
        Me.miGenerate.Index = 3
        Me.miGenerate.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.miGenerateToOutPutDir, Me.miGenerateToSolutionDir, Me.miGenerateToSolution})
        Me.miGenerate.Text = "Generate"
        '
        'miGenerateToOutPutDir
        '
        Me.miGenerateToOutPutDir.Index = 0
        Me.miGenerateToOutPutDir.Shortcut = System.Windows.Forms.Shortcut.CtrlG
        Me.miGenerateToOutPutDir.Text = "To OuPutDir"
        '
        'miGenerateToSolutionDir
        '
        Me.miGenerateToSolutionDir.Index = 1
        Me.miGenerateToSolutionDir.Text = "To SolutionDir"
        '
        'miGenerateToSolution
        '
        Me.miGenerateToSolution.Index = 2
        Me.miGenerateToSolution.Text = "To Solution"
        '
        'miHelp
        '
        Me.miHelp.Index = 4
        Me.miHelp.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItemAbout})
        Me.miHelp.Text = "Help"
        '
        'MenuItemAbout
        '
        Me.MenuItemAbout.Index = 0
        Me.MenuItemAbout.Text = "About"
        '
        'ofd
        '
        Me.ofd.Filter = "DLL|*.dll|EXE|*.exe|All supported||ALL|*.*"
        '
        'frmMain
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(916, 533)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Menu = Me.mmMain
        Me.MinimumSize = New System.Drawing.Size(300, 200)
        Me.Name = "frmMain"
        Me.Text = "Code Rocket"
        Me.ResumeLayout(False)

    End Sub

#End Region





#Region " Event Handlers "

#Region " Form "

    Private Sub frmMain_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        AppCore.Current.Initialize(CPDBrowser1, _
                              browserAssembly, _
                              SchemaBrowser1, _
                              GeneratorProjectBrowser1, _
                              New FileTabWrapperControl(TabFiles), _
                              PropertyGridControl, Nothing)

        settingsInstance = Settings.Initialize()
        InitializeBySetting()

        Dim args As String() = Environment.GetCommandLineArgs()
        If args.Length > 1 And System.IO.File.Exists(args(1)) Then
            LoadProject(args(1))
        End If



    End Sub


    Private Sub frmMain_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        If settingsInstance Is Nothing Then Return

        settingsInstance.Save()

        Try
            AppCore.Current.SaveProject()
        Catch

        End Try
    End Sub
#End Region

#Region " Menu "

#Region " File "
    Private Sub MenuItemClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemClose.Click
        AppCore.Current.CloseProject()
    End Sub

    Private Sub MenuItemSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemSave.Click
        AppCore.Current.SaveProject()
    End Sub

    Private Sub MenuItemSaveAs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemSaveAs.Click

        If AppCore.Current.Project Is Nothing Then
            Return
        End If
        sfd.FileName = AppCore.Current.Project.ProjectFileName


        If sfd.ShowDialog() <> Windows.Forms.DialogResult.OK Then
            Return
        End If
        AppCore.Current.SaveProject(sfd.FileName)

    End Sub

    Private Sub MenuItemNewAssemblyProject_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemNewAssemblyProject.Click
        AppCore.Current.NewProject(ProjectType.Assembly)
    End Sub

    Private Sub MenuItemNewPowerDesignerProject_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemNewPowerDesignerProject.Click
        AppCore.Current.NewProject(ProjectType.PD)
    End Sub

    Private Sub MenuItemNewSchemaProject_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemNewSchemaProject.Click
        AppCore.Current.NewProject(ProjectType.Schema)
    End Sub

    Private Sub miSettings_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles miSettings.Click
        Dim f As New frmSettings
        f.setts = settingsInstance

        If (f.ShowDialog() = Windows.Forms.DialogResult.OK) Then
            InitializeBySetting()
        End If

    End Sub


    Private Sub mProjectOpen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mProjectOpen.Click


        If ofd.ShowDialog() = Windows.Forms.DialogResult.OK Then
            LoadProject(ofd.FileName)
        End If


    End Sub

    Private Sub miRecentProjects_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim menu As MenuItem = CType(sender, MenuItem)
        Dim filePath As String = menu.Tag.ToString



        If SafeLoadProject(filePath) Then
            miGenerate.Enabled = True
        Else

            settingsInstance.RecentProjects.Remove(filePath)
            miRecentProjects.MenuItems.Remove(menu)
        End If


    End Sub

#End Region

#Region " Edit "
    Private Sub miWordWarp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles miWordWarp.Click
        setWordWarp(Not miWordWarp.Checked)
    End Sub

    Private Sub miCut_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles miCut.Click
        TabFiles.CutText()
    End Sub

    Private Sub miCopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles miCopy.Click
        TabFiles.CopyText()
    End Sub

    Private Sub miPaste_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles miPaste.Click
        TabFiles.PasteText()
    End Sub

    Private Sub miDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles miDelete.Click
        'TabFiles.SelectedText = ""
    End Sub

    Private Sub miSelectAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles miSelectAll.Click
        TabFiles.SelectAllText()
    End Sub
#End Region

    Private Sub miAssemblyBookmarks_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim bm As BookMark = BookMarks.Item(CType(sender, MenuItem).Text)
        If bm Is Nothing Then Exit Sub
        browserAssembly.SelectNodeByFullPath(bm.FullPath)
    End Sub

#Region " Assembly "

    Private Sub miAssemblyGenerate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Dim tmp As Template = Templates.Item(CType(sender, MenuItem).Text)
        'If tmp Is Nothing Then Exit Sub
        'Dim teplateFileName As String = tmp.FileName
        'assemblyGenerator.GenerationProject = assemblyProject
        'Try
        '    For Each node As TreeNode In browserAssembly.CheckedNodes
        '        If node.ImageIndex = tmp.NodeType Then
        '            TabFiles.AddFileContent(node.Text, assemblyGenerator.GenerateForNode(node, teplateFileName))
        '        End If
        '    Next
        'Catch ex As CodeGenerationException
        '    ShowException(ex, "Error generate template " & tmp.FileName)
        'End Try

        'If browserAssembly.SelectedNode Is Nothing Then Exit Sub
        'If browserAssembly.SelectedNode.ImageIndex <> tmp.NodeType Then
        '    MsgBox("Incorrect Node selected")
        '    Exit Sub
        'End If



    End Sub
#End Region

#Region " Generate "


    Private Sub miGenerateToOutPutDir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles miGenerateToOutPutDir.Click
        Try
            AppCore.Current.GenerateToOutPutDir()
        Catch ex As CodeGenerationException
            ShowException(ex, "Error Generate To OutPut Dir", ex.Message)
        Catch ex As Exception
            ShowException(ex, "Error Generate To OutPut Dir")
        End Try

    End Sub

    Private Sub miGenerateToSolutionDir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles miGenerateToSolutionDir.Click
        Try
            AppCore.Current.GenerateToSolutionDir()
        Catch ex As CodeGenerationException
            ShowException(ex, "Error Generate To OutPut Dir", ex.Message)
        Catch ex As Exception
            ShowException(ex, "Error Generate To OutPut Dir")
        End Try


    End Sub
    Private Sub miGenerateToSolution_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles miGenerateToSolution.Click
        Try
            AppCore.Current.GenerateToSolution()
        Catch ex As CodeGenerationException
            ShowException(ex, "Error Generate To OutPut Dir", ex.Message)
        Catch ex As Exception
            ShowException(ex, "Error Generate To OutPut Dir")
        End Try
    End Sub


#End Region



#End Region

#Region " AssemblyBrowser "



    Private Sub browser_AfterSelect(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles browserAssembly.AfterSelect
        'If Not assemblyProject Is Nothing Then
        '    assemblyProject.LastView = node.FullPath
        'End If

    End Sub
#End Region

    Private Sub SchemaBrowser1_AfterSelect(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SchemaBrowser1.AfterSelect

        PropertyGridControl.SelectedObject = SchemaBrowser1.SelectedObject

    End Sub

    Private Sub GeneratorProjectBrowser1_AfterSelect(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GeneratorProjectBrowser1.AfterSelect

        'Dim selectedObject As Object = GeneratorProjectBrowser1.SelectedObject
        'If selectedObject Is Nothing Then
        '    Me.PropertyGridControl.SelectedObject = Nothing
        'Else
        '    Dim type As Type = selectedObject.GetType

        '    PropertyGridControl.SelectedObjects


        'End If

        PropertyGridControl.SelectedObject = GeneratorProjectBrowser1.SelectedObject


    End Sub
#End Region


#Region " helpers "
    Sub InitializeBySetting()
        For Each project As String In settingsInstance.RecentProjects
            AddRecentProjectMenuItem(project)
        Next
        setWordWarp(settingsInstance.WordWarp)

    End Sub

    Private Function LoadProject(ByVal projectFileName As String) As Boolean
        If SafeLoadProject(projectFileName) Then
            miGenerate.Enabled = True
            settingsInstance.AddRecentProject(ofd.FileName)
            AddRecentProjectMenuItem(ofd.FileName)
            TabControlBrowsers.Controls.Clear()

            Dim lstControls As List(Of Control) = Me.ProjectTypeToPages.Item(AppCore.Current.Project.ProjectType)

            For Each cnt As Control In lstControls
                TabControlBrowsers.Controls.Add(cnt)
            Next

        End If
    End Function

    Private Function SafeLoadProject(ByVal projectFileName As String) As Boolean
        Try
            AppCore.Current.LoadProject(projectFileName)
            Return True
        Catch ex As FileNotFoundException
            MessageBox.Show(ex.Message, "Error Open project", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            ShowException(ex, "Error Open project")
        End Try
        Return False
    End Function

    Private Sub setWordWarp(ByVal value As Boolean)
        miWordWarp.Checked = value
        TabFiles.WordWrap = value
        settingsInstance.WordWarp = value
    End Sub


    Private Sub AddBookMarkMenu(ByVal bm As BookMark)
        BookMarks.Add(bm.Name, bm)
        miAssemblyBookmarks.MenuItems.Add(bm.Name, New EventHandler(AddressOf miAssemblyBookmarks_Click))

    End Sub

    Private Sub AddRecentProjectMenuItem(ByVal project As String)
        Dim menuItemText As String
        If project.Length > 40 Then
            menuItemText = project.Substring(0, 3) & "..." & project.Substring(project.Length - 35, 35)
        Else
            menuItemText = project
        End If
        Dim menuItem As New MenuItem(menuItemText, New EventHandler(AddressOf miRecentProjects_Click))
        menuItem.Tag = project
        miRecentProjects.MenuItems.Add(menuItem)
    End Sub
    Private Sub ShowException(ByVal ex As Exception, ByVal title As String, Optional ByVal message As String = "")
        'MessageBox.Show(ex.Message & vbCrLf & message, title & ": " & ex.GetType().Name, MessageBoxButtons.OK, MessageBoxIcon.Error)
        ExceptionForm.ShowException(title, message, ex)
    End Sub

    Private Sub AddBrowserTabs()
        TabControlBrowsers = New System.Windows.Forms.TabControl
        TabPageAssembly = New System.Windows.Forms.TabPage
        browserAssembly = New AssemblyBrowser
        TabPagePowerDesigner = New System.Windows.Forms.TabPage
        CPDBrowser1 = New Savchin.Controls.Browsers.PDBrowser
        TabPageProject = New System.Windows.Forms.TabPage
        GeneratorProjectBrowser1 = New Savchin.CodeGeneration.GeneratorProjectBrowser
        TabPageSchema = New System.Windows.Forms.TabPage
        SchemaBrowser1 = New Savchin.Data.Schema.Controls.SchemaBrowser


        TabControlBrowsers.SuspendLayout()
        TabPageAssembly.SuspendLayout()
        TabPagePowerDesigner.SuspendLayout()
        TabPageProject.SuspendLayout()
        TabPageSchema.SuspendLayout()

        VertContainer.Panel1.Controls.Add(TabControlBrowsers)


        'browserAssembly

        browserAssembly.Dock = System.Windows.Forms.DockStyle.Fill
        browserAssembly.MinimumSize = New System.Drawing.Size(150, 100)
        browserAssembly.Name = "browserAssembly"
        browserAssembly.TabIndex = 0

        'CPDBrowser1

        CPDBrowser1.Dock = System.Windows.Forms.DockStyle.Fill
        CPDBrowser1.ModelFilePath = Nothing
        CPDBrowser1.Name = "CPDBrowser1"
        CPDBrowser1.ResourcePath = Nothing
        CPDBrowser1.ShowSearch = False
        CPDBrowser1.TabIndex = 0


        'GeneratorProjectBrowser1

        GeneratorProjectBrowser1.Dock = System.Windows.Forms.DockStyle.Fill
        GeneratorProjectBrowser1.Name = "GeneratorProjectBrowser1"
        GeneratorProjectBrowser1.TabIndex = 0



        'SchemaBrowser1

        SchemaBrowser1.CheckBoxes = True
        SchemaBrowser1.Dock = System.Windows.Forms.DockStyle.Fill
        SchemaBrowser1.Name = "SchemaBrowser1"
        SchemaBrowser1.TabIndex = 0



        'TabControl1()

        'TabControl1.Controls.Add(TabPageAssembly)
        'TabControl1.Controls.Add(TabPagePowerDesigner)
        'TabControl1.Controls.Add(TabPageProject)
        'TabControl1.Controls.Add(TabPageSchema)

        TabControlBrowsers.Dock = System.Windows.Forms.DockStyle.Fill

        TabControlBrowsers.MinimumSize = New System.Drawing.Size(100, 0)
        TabControlBrowsers.Name = "TabControl1"
        TabControlBrowsers.SelectedIndex = 0
        TabControlBrowsers.Size = New System.Drawing.Size(240, 100)



        'TabPageAssembly

        TabPageAssembly.Controls.Add(browserAssembly)
        TabPageAssembly.Name = "TabPageAssembly"
        TabPageAssembly.Padding = New System.Windows.Forms.Padding(3)
        TabPageAssembly.Size = New System.Drawing.Size(232, 74)
        TabPageAssembly.TabIndex = 0
        TabPageAssembly.Text = "Assembly"
        TabPageAssembly.UseVisualStyleBackColor = True


        'TabPagePowerDesigner

        TabPagePowerDesigner.Controls.Add(CPDBrowser1)
        TabPagePowerDesigner.Location = New System.Drawing.Point(4, 22)
        TabPagePowerDesigner.Name = "TabPagePowerDesigner"
        TabPagePowerDesigner.Padding = New System.Windows.Forms.Padding(3)
        TabPagePowerDesigner.Size = New System.Drawing.Size(229, 222)
        TabPagePowerDesigner.TabIndex = 1
        TabPagePowerDesigner.Text = "Power designer"
        TabPagePowerDesigner.UseVisualStyleBackColor = True


        'TabPageProject

        TabPageProject.Controls.Add(GeneratorProjectBrowser1)
        TabPageProject.Location = New System.Drawing.Point(4, 22)
        TabPageProject.Name = "TabPageProject"
        TabPageProject.Size = New System.Drawing.Size(229, 222)
        TabPageProject.TabIndex = 2
        TabPageProject.Text = "Project"
        TabPageProject.UseVisualStyleBackColor = True
        'TabPageSchema

        TabPageSchema.Controls.Add(SchemaBrowser1)
        TabPageSchema.Location = New System.Drawing.Point(4, 22)
        TabPageSchema.Name = "TabPageSchema"
        TabPageSchema.Padding = New System.Windows.Forms.Padding(3)
        TabPageSchema.Size = New System.Drawing.Size(229, 222)
        TabPageSchema.TabIndex = 3
        TabPageSchema.Text = "Schema"
        TabPageSchema.UseVisualStyleBackColor = True



        TabControlBrowsers.ResumeLayout(False)
        TabPageAssembly.ResumeLayout(False)
        TabPagePowerDesigner.ResumeLayout(False)
        TabPageProject.ResumeLayout(False)
        TabPageSchema.ResumeLayout(False)

        Dim assemblyList As New List(Of Control)
        assemblyList.Add(TabPageAssembly)
        assemblyList.Add(TabPageProject)
        ProjectTypeToPages.Add(ProjectType.Assembly, assemblyList)

        Dim pdList As New List(Of Control)
        pdList.Add(TabPageAssembly)
        pdList.Add(TabPageProject)
        ProjectTypeToPages.Add(ProjectType.PD, pdList)

        Dim SchemaList As New List(Of Control)
        SchemaList.Add(TabPageSchema)
        SchemaList.Add(TabPageProject)
        ProjectTypeToPages.Add(ProjectType.Schema, SchemaList)

    End Sub

#End Region





End Class
