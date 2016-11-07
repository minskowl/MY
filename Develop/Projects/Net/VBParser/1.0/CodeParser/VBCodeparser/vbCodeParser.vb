Imports PGMRX120Lib
Namespace CodeParser

    Public Class vbCodeParser
        Implements iCodeParser



        Private _p As PgmrClass

        Public Sub New()
            _p = New PgmrClass
            _p.SetGrammar("..\..\VBNET.GMR")

        End Sub
        Protected Overrides Sub Finalize()
            _p = Nothing
            MyBase.Finalize()
        End Sub

        Public Function GetCodeObjectByID(ByVal ID As Long) As iCodeObject
            GetCodeObjectByID = vbCodeObject.Create(_p, ID)
        End Function
        Public Function GetRoot() As iCodeObject Implements iCodeParser.GetRoot
            GetRoot = GetCodeObjectByID(_p.GetRoot())
        End Function


        Public Function ParseFile(ByVal fileName As String) As Boolean Implements iCodeParser.ParseFile
            _p.SetInputFilename(fileName)
            If _p.Parse() = PGStatus.pgStatusComplete Then ParseFile = True
        End Function

        Public Function ParseString(ByVal Text As String) As Boolean Implements iCodeParser.ParseString
            _p.SetInputString(Text)
            If _p.Parse() = PGStatus.pgStatusComplete Then ParseString = True
        End Function
    End Class


End Namespace

