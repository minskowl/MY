Public Class SynTree
    Inherits System.Windows.Forms.TreeView

#Region " Windows Form Designer generated code "
    Private Const WM_ACTIVATEAPP As Integer = &H1C
    Private Const WM_VSCROLL As Integer = &H115
    Private Const WM_MOUSEWHEEL As Integer = &H20A

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
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        components = New System.ComponentModel.Container()
    End Sub

    <System.Security.Permissions.PermissionSetAttribute(System.Security.Permissions.SecurityAction.Demand, Name:="FullTrust")> _
       Protected Overrides Sub WndProc(ByRef m As Message)
        ' Listen for operating system messages
        Select Case (m.Msg)
            ' The WM_ACTIVATEAPP message occurs when the application
            ' becomes the active application or becomes inactive.
        Case WM_VSCROLL
                Dim lparam As Integer
                lparam = m.LParam.ToInt32
                If lparam = 0 Then
                    RaiseEvent Scroll(m)
                End If

        End Select
        MyBase.WndProc(m)
    End Sub
    Public Event Scroll(ByRef m As Message)
    ' Public Event MouseWheel(ByRef m As Message)
    Public Sub Send_Msg(ByRef m As Message)
        WndProc(m)
    End Sub



#End Region

End Class
