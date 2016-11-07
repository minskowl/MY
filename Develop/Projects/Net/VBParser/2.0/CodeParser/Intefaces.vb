Namespace CodeParser
    Public Interface ICodeParser
        Function ParseFile(ByVal fileName As String) As Boolean
        Function ParseString(ByVal Text As String) As Boolean
        Function GetRoot() As ICodeObject

    End Interface

    Public Interface iCodeBuilder


    End Interface
    Public Interface ICodeObject
        ReadOnly Property Text() As String
        ReadOnly Property Type() As String
        ReadOnly Property ID() As Long

        ReadOnly Property ChildCount() As Integer
        ReadOnly Property NumDescendants() As Integer

        ReadOnly Property Parent() As ICodeObject
        ReadOnly Property FirstChild() As ICodeObject
        ReadOnly Property LastChild() As ICodeObject

        ReadOnly Property NextSibling() As ICodeObject
        ReadOnly Property PreviousSibling() As ICodeObject
    End Interface


End Namespace
