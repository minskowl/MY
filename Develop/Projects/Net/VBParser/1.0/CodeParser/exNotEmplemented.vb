Public Class exNotEmplemented
    Inherits Exception

    Public Sub New(ByVal Type As String)
        MyBase.New("Type '" & Type & "' not emplemented yet")
    End Sub
End Class
