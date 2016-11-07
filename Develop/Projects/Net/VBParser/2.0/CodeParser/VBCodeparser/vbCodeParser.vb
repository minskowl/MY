Imports PGMRX120Lib
Namespace CodeParser

    Public Class vbCodeParser
        Implements ICodeParser



        Private _p As PgmrClass

        Public Sub New()
            _p = New PgmrClass
            _p.SetGrammar("..\..\VBNET.GMR")

        End Sub
        Protected Overrides Sub Finalize()
            _p = Nothing
            MyBase.Finalize()
        End Sub

        Public Function GetCodeObjectByID(ByVal ID As Long) As ICodeObject
            GetCodeObjectByID = vbCodeObject.Create(_p, ID)
        End Function
        Public Function GetRoot() As ICodeObject Implements ICodeParser.GetRoot
            GetRoot = GetCodeObjectByID(_p.GetRoot())
        End Function


        Public Function ParseFile(ByVal fileName As String) As Boolean Implements ICodeParser.ParseFile
            _p.SetInputFilename(fileName)
            If _p.Parse() = PGStatus.pgStatusComplete Then ParseFile = True
        End Function

        Public Function ParseString(ByVal Text As String) As Boolean Implements ICodeParser.ParseString

            _p.SetInputString(Text)
            If _p.Parse() = PGStatus.pgStatusComplete Then ParseString = True
        End Function
    End Class


End Namespace

