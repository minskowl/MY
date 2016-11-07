' ***********************************************************************
'  Module:  Method.vb
'  Author:  Savchin
'  Purpose: Definition of the Class Method
' ***********************************************************************
Option Strict Off

Imports Microsoft.VisualBasic
Imports System

Public Class Method
   Public Name As String
   Public test As Class
   
   '' <pdGenerated>default parent getter</pdGenerated>
   Public Function GetTest() As Class
      return test
   End Function
   
   '' <pdGenerated>default parent setter</pdGenerated>
   '' <param>newClass</param>
   Public Sub SetTest(newClass As Class)
      If Not (Me.test Is newClass) Then
         If Not (Me.test Is Nothing) Then
            Dim oldClass As Class
            oldClass = Me.test
            Me.test = Nothing
            oldClass.RemoveMethods(Me)
         End If
         If Not (newClass Is Nothing) Then
            Me.test = newClass
            Me.test.AddMethods(Me)
         End If
      End If
   End Sub
End Class