
Namespace CodeParser

    Public Class CodeBuilder
        Implements iCodeBuilder

        Private _p As ICodeParser
        Private Sub New(ByRef CodeParser As iCodeParser)
            _p = CodeParser
        End Sub
        Public Shared Function Create(ByRef CodeParser As iCodeParser) As CodeBuilder
            If CodeParser Is Nothing Then Return Nothing
            Create = New CodeBuilder(CodeParser)
        End Function

        Public Function ParseSolution(ByRef fileName As String) As CodeModel.clsSolution
            Dim dte As EnvDTE.DTE
            Try
                dte = Microsoft.VisualBasic.Interaction.CreateObject("VisualStudio.DTE.7.1")

                dte.Solution.Open(fileName)

                ParseSolution = ParseSolution(dte.Solution)

                dte.Quit()

            Catch ex As Exception
                Throw New Exception("Error Parse", ex)
            Finally
                dte = Nothing
            End Try
        End Function
        Public Function ParseSolution(ByRef sol As EnvDTE.Solution) As CodeModel.clsSolution
            Dim objSol As CodeModel.clsSolution
            objSol = New CodeModel.clsSolution(sol.Properties.Item("Name").Value, sol.Properties.Item("Path").Value)
            For Each prj As EnvDTE.Project In sol.Projects
                If Not prj.ProjectItems Is Nothing Then objSol.AddProjects(New CodeModel.clsProject(prj.Name, prj.FullName))
            Next

            Return objSol

        End Function


 
    End Class
End Namespace

