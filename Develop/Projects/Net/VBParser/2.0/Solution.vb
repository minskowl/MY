' ***********************************************************************
'  Module:  Solution.vb
'  Author:  Savchin
'  Purpose: Definition of the Class Solution
' ***********************************************************************
Option Strict Off

Imports Microsoft.VisualBasic
Imports System

Public Class Solution
   Public Name As String
   Public FileName As String
   Private Projects As System.Collections.ArrayList
   
   '' <pdGenerated>default getter</pdGenerated>
   Public Function GetProjects() As System.Collections.ArrayList
      If Projects Is Nothing Then
         Projects = new System.Collections.ArrayList()
      End If
      return Projects
   End Function
   
   '' <pdGenerated>default setter</pdGenerated>
   Public Sub SetProjects(newProjects As System.Collections.ArrayList)
      RemoveAllProjects()
      Dim oProject As Project
      For Each oProject in newProjects
         AddProjects(oProject)
      Next
   End Sub
   
   '' <pdGenerated>default Add</pdGenerated>
   Public Sub AddProjects(newProject As Project)
      If newProject Is Nothing Then
         return
      End If
      If Projects Is Nothing Then
         Projects = new System.Collections.ArrayList()
      End If
      If Not Projects.Contains(newProject) Then
         Projects.Add(newProject)
      End If
   End Sub
   
   '' <pdGenerated>default Remove</pdGenerated>
   Public Sub RemoveProjects(oldProject As Project)
      If oldProject Is Nothing Then
         return
      End If
      If Projects Is Nothing Then
         return
      End If
      If Projects.Contains(oldProject) Then
         Projects.Remove(oldProject)
      End If
   End Sub
   
   '' <pdGenerated>default removeAll</pdGenerated>
   Public Sub RemoveAllProjects()
      If Not (Projects Is Nothing)
         Projects.Clear()
      End If
   End Sub
End Class