Imports System
Imports System.Text
Imports System.IO
Imports EnvDTE
Imports EnvDTE80
Imports System.Diagnostics

Public Module Main


    Sub Revert()
        Dim separ() As Char = {"="}
        Dim tmp As String
        Dim line As String
        Dim parts() As String
        Dim result As StringBuilder = New StringBuilder
        Dim selection As TextSelection

        selection = DTE.ActiveDocument.Selection
        tmp = selection.Text

        Dim reader As StringReader = New StringReader(tmp)

        line = reader.ReadLine()

        While (Not line Is Nothing)

            parts = line.Split(separ)
            If (parts.Length = 1) Then
                result.AppendLine(parts(0))
            Else
                result.AppendLine(parts(1).Trim.TrimEnd(";") & "=" & parts(0).Trim & ";")
            End If
            line = reader.ReadLine()
        End While


        reader.Dispose()

        selection.Text = result.ToString




    End Sub
End Module

