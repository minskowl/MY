Namespace CodeParser
    Public Interface iCodeParser
        Function ParseFile(ByVal fileName As String) As Boolean
        Function ParseString(ByVal Text As String) As Boolean
        Function GetRoot() As iCodeObject

    End Interface

    Public Interface iCodeBuilder


    End Interface
    Public Interface iCodeObject
        ReadOnly Property Text() As String
        ReadOnly Property Type() As String
        ReadOnly Property ID() As Long

        ReadOnly Property ChildCount() As Integer

        Function FirstChild() As iCodeObject
        Function LastChild() As iCodeObject

        Function NextSibling() As iCodeObject
        Function PreviousSibling() As iCodeObject
    End Interface


End Namespace
