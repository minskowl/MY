' ***********************************************************************
'  Module:  Project.vb
'  Author:  Savchin
'  Purpose: Definition of the Class Project
' ***********************************************************************
Option Strict Off

Imports Microsoft.VisualBasic
Imports System

Public Class Project
   Public Name As String
   Public FileName As String
   Private NameSpaces As System.Collections.ArrayList
   
   '' <pdGenerated>default getter</pdGenerated>
   Public Function GetNameSpaces() As System.Collections.ArrayList
      If NameSpaces Is Nothing Then
         NameSpaces = new System.Collections.ArrayList()
      End If
      return NameSpaces
   End Function
   
   '' <pdGenerated>default setter</pdGenerated>
   Public Sub SetNameSpaces(newNameSpaces As System.Collections.ArrayList)
      RemoveAllNameSpaces()
      Dim oNameSpace As NameSpace
      For Each oNameSpace in newNameSpaces
         AddNameSpaces(oNameSpace)
      Next
   End Sub
   
   '' <pdGenerated>default Add</pdGenerated>
   Public Sub AddNameSpaces(newNameSpace As NameSpace)
      If newNameSpace Is Nothing Then
         return
      End If
      If NameSpaces Is Nothing Then
         NameSpaces = new System.Collections.ArrayList()
      End If
      If Not NameSpaces.Contains(newNameSpace) Then
         NameSpaces.Add(newNameSpace)
      End If
   End Sub
   
   '' <pdGenerated>default Remove</pdGenerated>
   Public Sub RemoveNameSpaces(oldNameSpace As NameSpace)
      If oldNameSpace Is Nothing Then
         return
      End If
      If NameSpaces Is Nothing Then
         return
      End If
      If NameSpaces.Contains(oldNameSpace) Then
         NameSpaces.Remove(oldNameSpace)
      End If
   End Sub
   
   '' <pdGenerated>default removeAll</pdGenerated>
   Public Sub RemoveAllNameSpaces()
      If Not (NameSpaces Is Nothing)
         NameSpaces.Clear()
      End If
   End Sub
End Class