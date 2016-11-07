' ***********************************************************************
'  Module:  Class.vb
'  Author:  Savchin
'  Purpose: Definition of the Class Class
' ***********************************************************************
Option Strict Off

Imports Microsoft.VisualBasic
Imports System

Public Class Class
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
      Dim oMethod As Method
      For Each oMethod in newMethods
         AddMethods(oMethod)
      Next
   End Sub
   
   '' <pdGenerated>default Add</pdGenerated>
   Public Sub AddMethods(newMethod As Method)
      If newMethod Is Nothing Then
         return
      End If
      If Methods Is Nothing Then
         Methods = new System.Collections.ArrayList()
      End If
      If Not Methods.Contains(newMethod) Then
         Methods.Add(newMethod)
         newMethod.SetTest(Me)
      End If
   End Sub
   
   '' <pdGenerated>default Remove</pdGenerated>
   Public Sub RemoveMethods(oldMethod As Method)
      If oldMethod Is Nothing Then
         return
      End If
      If Methods Is Nothing Then
         return
      End If
      If Methods.Contains(oldMethod) Then
         Methods.Remove(oldMethod)
         oldMethod.SetTest(Nothing)
      End If
   End Sub
   
   '' <pdGenerated>default removeAll</pdGenerated>
   Public Sub RemoveAllMethods()
      If Not (Methods Is Nothing)
         Dim tmpMethods As System.Collections.ArrayList = new System.Collections.ArrayList()
         Dim oldMethod As Method
         For Each oldMethod in Methods
            tmpMethods.Add(oldMethod)
         Next
         Methods.Clear()
         For Each oldMethod in tmpMethods
            oldMethod.SetTest(Nothing)
         Next
         tmpMethods.Clear()
      End If
   End Sub
End Class