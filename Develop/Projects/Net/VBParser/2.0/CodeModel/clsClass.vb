' ***********************************************************************
'  Module:  clsClass.vb
'  Author:  Savchin
'  Purpose: Definition of the Class clsClass
' ***********************************************************************
Option Strict Off

Imports Microsoft.VisualBasic
Imports System

Namespace CodeModel
   Public Class clsClass
      Public Name As String
      Private Methods As System.Collections.ArrayList
      
      '' <pdGenerated>default getter</pdGenerated>
      Public Function GetMethods() As System.Collections.ArrayList
         If Methods Is Nothing Then
            Methods = new System.Collections.ArrayList()
         End If
         return Methods
      End Function
      
      '' <pdGenerated>default setter</pdGenerated>
      Public Sub SetMethods(newMethods As System.Collections.ArrayList)
         RemoveAllMethods()
         Dim oclsMethod As clsMethod
         For Each oclsMethod in newMethods
            AddMethods(oclsMethod)
         Next
      End Sub
      
      '' <pdGenerated>default Add</pdGenerated>
      Public Sub AddMethods(newClsMethod As clsMethod)
         If newClsMethod Is Nothing Then
            return
         End If
         If Methods Is Nothing Then
            Methods = new System.Collections.ArrayList()
         End If
         If Not Methods.Contains(newClsMethod) Then
            Methods.Add(newClsMethod)
         End If
      End Sub
      
      '' <pdGenerated>default Remove</pdGenerated>
      Public Sub RemoveMethods(oldClsMethod As clsMethod)
         If oldClsMethod Is Nothing Then
            return
         End If
         If Methods Is Nothing Then
            return
         End If
         If Methods.Contains(oldClsMethod) Then
            Methods.Remove(oldClsMethod)
         End If
      End Sub
      
      '' <pdGenerated>default removeAll</pdGenerated>
      Public Sub RemoveAllMethods()
         If Not (Methods Is Nothing)
            Methods.Clear()
         End If
      End Sub
   End Class
End Namespace