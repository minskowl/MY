Public Class Form1


    Shared Function ShowObject(ByVal obj As Object) As Form1
        Dim f As New Form1
        f.PropertyGrid1.SelectedObject = obj
        f.Show()
        Return f
    End Function
End Class