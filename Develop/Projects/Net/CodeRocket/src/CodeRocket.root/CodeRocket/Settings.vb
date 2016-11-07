Imports System.Xml.Serialization
Imports System.Collections.Generic
Imports System.IO
Imports Savchin.Controls.Browsers
Imports Savchin.Tools

Public Class Settings
    Private Const defFileName As String = "def.cfg"

    Public PathIcons As String
    Public WordWarp As Boolean


    <XmlArrayItem(ElementName:="Project", Type:=GetType(String)), XmlArray()> _
        Public RecentProjects As List(Of String)

    Public Sub New()
        RecentProjects = New List(Of String)
    End Sub

#Region "Load\Save"

    Public Shared Function Initialize() As Settings
        Try
            Return Load()
        Catch ex As System.Exception
            Return New Settings
        End Try
    End Function

    Public Shared Function Load() As Settings
        Return Load(Application.StartupPath & "\" & defFileName)
    End Function

    Public Shared Function Load(ByVal fileName As String) As Settings
        Dim result As Settings = TypeSerializer(Of Settings).Deserialize(fileName)

        If (result Is Nothing) Then
            result = New Settings
        End If

        Return result
    End Function

    Public Sub Save()
        Save(Application.StartupPath & "\" & defFileName)
    End Sub
    Public Sub Save(ByVal fileName As String)
        TypeSerializer(Of Settings).Serialize(fileName, Me)
    End Sub
#End Region



    Public Sub AddRecentProject(ByVal projectFileName As String)
        If (RecentProjects.Contains(projectFileName) = False) Then
            If RecentProjects.Count = 10 Then
                RecentProjects.RemoveAt(0)
            End If
            RecentProjects.Add(projectFileName)
        End If

    End Sub
End Class





