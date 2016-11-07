Imports PGMRX120Lib

Namespace CodeParser
    Public Class vbCodeObject
        Implements iCodeObject



        Private _p As PgmrClass

        Private _text As String
        Public _type As String
        Public _id As Long

        Private Sub New(ByRef parser As PgmrClass, ByVal ID As Long)
            _p = parser
            _id = ID
            _type = _p.GetLabel(ID)
            _text = _p.GetValue(ID)
        End Sub

        Public Shared Function Create(ByRef parser As PgmrClass, ByVal ID As Long) As iCodeObject
            If parser Is Nothing Or ID = 0 Then Exit Function
            Create = New vbCodeObject(parser, ID)
        End Function




#Region " Properties "

        Public ReadOnly Property ID() As Long Implements iCodeObject.ID
            Get
                Return _id
            End Get

        End Property

        Public ReadOnly Property Text() As String Implements iCodeObject.Text
            Get
                Return _text
            End Get

        End Property

        Public ReadOnly Property Type() As String Implements iCodeObject.Type
            Get
                Return _type
            End Get
        End Property

        Public ReadOnly Property ChildCount() As Integer Implements iCodeObject.ChildCount
            Get
                Return _p.GetNumChildren(_id)
            End Get
        End Property
#End Region


        Public Function FirstChild() As iCodeObject Implements iCodeObject.FirstChild
            FirstChild = vbCodeObject.Create(_p, _p.GetChild(_id, 0))
        End Function
        Public Function LastChild() As iCodeObject Implements iCodeObject.LastChild
            LastChild = vbCodeObject.Create(_p, _p.GetChild(_id, _p.GetNumChildren(_id) - 1))
        End Function

        Public Function NextSibling() As iCodeObject Implements iCodeObject.NextSibling
            NextSibling = vbCodeObject.Create(_p, _p.GetNextSibling(_id))
        End Function

        Public Function PreviousSibling() As iCodeObject Implements iCodeObject.PreviousSibling
            PreviousSibling = vbCodeObject.Create(_p, _p.GetPrevSibling(_id))
        End Function



    End Class
End Namespace

