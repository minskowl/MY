' ***********************************************************************
'  Module:  clsSolution.vb
'  Author:  Savchin
'  Purpose: Definition of the Class clsSolution
' ***********************************************************************
Option Strict Off

Imports Microsoft.VisualBasic
Imports System

Namespace CodeModel
    Public Class clsSolutionObject

        Private _name As String
        Public Property Name() As String
            Get
                Return _name
            End Get
            Set(ByVal Value As String)
                _name = Value
            End Set
        End Property
        Private _fileName As String
        Public Property fileName() As String
            Get
                Return _fileName
            End Get
            Set(ByVal Value As String)
                _fileName = Value
            End Set
        End Property

        Public Sub New(ByVal Name As String, ByVal FileName As String)
            _name = Name
            _fileName = FileName
        End Sub
    End Class

    Public Class clsSolution
        Inherits clsSolutionObject

        Private Projects As System.Collections.ArrayList

        Public Sub New(ByVal Name As String, ByVal FileName As String)
            MyBase.New(Name, FileName)
        End Sub
        '' <pdGenerated>default getter</pdGenerated>
        Public Function GetProjects() As System.Collections.ArrayList
            If Projects Is Nothing Then
                Projects = New System.Collections.ArrayList
            End If
            Return Projects
        End Function

        '' <pdGenerated>default setter</pdGenerated>
        Public Sub SetProjects(ByVal newProjects As System.Collections.ArrayList)
            RemoveAllProjects()
            Dim oclsProject As clsProject
            For Each oclsProject In newProjects
                AddProjects(oclsProject)
            Next
        End Sub

        '' <pdGenerated>default Add</pdGenerated>
        Public Sub AddProjects(ByVal newClsProject As clsProject)
            If newClsProject Is Nothing Then
                Return
            End If
            If Projects Is Nothing Then
                Projects = New System.Collections.ArrayList
            End If
            If Not Projects.Contains(newClsProject) Then
                Projects.Add(newClsProject)
            End If
        End Sub

        '' <pdGenerated>default Remove</pdGenerated>
        Public Sub RemoveProjects(ByVal oldClsProject As clsProject)
            If oldClsProject Is Nothing Then
                Return
            End If
            If Projects Is Nothing Then
                Return
            End If
            If Projects.Contains(oldClsProject) Then
                Projects.Remove(oldClsProject)
            End If
        End Sub

        '' <pdGenerated>default removeAll</pdGenerated>
        Public Sub RemoveAllProjects()
            If Not (Projects Is Nothing) Then
                Projects.Clear()
            End If
        End Sub
    End Class
End Namespace