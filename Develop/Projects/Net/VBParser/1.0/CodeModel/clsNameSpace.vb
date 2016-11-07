' ***********************************************************************
'  Module:  clsNameSpace.vb
'  Author:  Savchin
'  Purpose: Definition of the Class clsNameSpace
' ***********************************************************************
Option Strict Off

Imports Microsoft.VisualBasic
Imports System

Namespace CodeModel
   Public Class clsNameSpace
      Public Name As String
      Private Clases As System.Collections.ArrayList
      
      '' <pdGenerated>default getter</pdGenerated>
      Public Function GetClases() As System.Collections.ArrayList
         If Clases Is Nothing Then
            Clases = new System.Collections.ArrayList()
         End If
         return Clases
      End Function
      
      '' <pdGenerated>default setter</pdGenerated>
      Public Sub SetClases(newClases As System.Collections.ArrayList)
         RemoveAllClases()
         Dim oclsClass As clsClass
         For Each oclsClass in newClases
            AddClases(oclsClass)
         Next
      End Sub
      
      '' <pdGenerated>default Add</pdGenerated>
      Public Sub AddClases(newClsClass As clsClass)
         If newClsClass Is Nothing Then
            return
         End If
         If Clases Is Nothing Then
            Clases = new System.Collections.ArrayList()
         End If
         If Not Clases.Contains(newClsClass) Then
            Clases.Add(newClsClass)
         End If
      End Sub
      
      '' <pdGenerated>default Remove</pdGenerated>
      Public Sub RemoveClases(oldClsClass As clsClass)
         If oldClsClass Is Nothing Then
            return
         End If
         If Clases Is Nothing Then
            return
         End If
         If Clases.Contains(oldClsClass) Then
            Clases.Remove(oldClsClass)
         End If
      End Sub
      
      '' <pdGenerated>default removeAll</pdGenerated>
      Public Sub RemoveAllClases()
         If Not (Clases Is Nothing)
            Clases.Clear()
         End If
      End Sub
   End Class
End Namespace