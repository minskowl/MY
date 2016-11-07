' ***********************************************************************
'  Module:  clsProject.vb
'  Author:  Savchin
'  Purpose: Definition of the Class clsProject
' ***********************************************************************
Option Strict Off

Imports Microsoft.VisualBasic
Imports System

Namespace CodeModel
   Public Class clsProject
        Inherits clsSolutionObject

        Public Sub New(ByVal Name As String, ByVal FileName As String)
            MyBase.New(Name, FileName)
        End Sub

        Private NameSpaces As System.Collections.ArrayList

        '' <pdGenerated>default getter</pdGenerated>
        Public Function GetNameSpaces() As System.Collections.ArrayList
            If NameSpaces Is Nothing Then
                NameSpaces = New System.Collections.ArrayList
            End If
            Return NameSpaces
        End Function

        '' <pdGenerated>default setter</pdGenerated>
        Public Sub SetNameSpaces(ByVal newNameSpaces As System.Collections.ArrayList)
            RemoveAllNameSpaces()
            Dim oclsNameSpace As clsNameSpace
            For Each oclsNameSpace In newNameSpaces
                AddNameSpaces(oclsNameSpace)
            Next
        End Sub

        '' <pdGenerated>default Add</pdGenerated>
        Public Sub AddNameSpaces(ByVal newClsNameSpace As clsNameSpace)
            If newClsNameSpace Is Nothing Then
                Return
            End If
            If NameSpaces Is Nothing Then
                NameSpaces = New System.Collections.ArrayList
            End If
            If Not NameSpaces.Contains(newClsNameSpace) Then
                NameSpaces.Add(newClsNameSpace)
            End If
        End Sub

        '' <pdGenerated>default Remove</pdGenerated>
        Public Sub RemoveNameSpaces(ByVal oldClsNameSpace As clsNameSpace)
            If oldClsNameSpace Is Nothing Then
                Return
            End If
            If NameSpaces Is Nothing Then
                Return
            End If
            If NameSpaces.Contains(oldClsNameSpace) Then
                NameSpaces.Remove(oldClsNameSpace)
            End If
        End Sub

        '' <pdGenerated>default removeAll</pdGenerated>
        Public Sub RemoveAllNameSpaces()
            If Not (NameSpaces Is Nothing) Then
                NameSpaces.Clear()
            End If
        End Sub
    End Class
End Namespace