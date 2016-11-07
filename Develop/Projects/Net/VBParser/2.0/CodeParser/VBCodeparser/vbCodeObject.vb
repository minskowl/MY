Imports PGMRX120Lib

Namespace CodeParser
    Public Class vbCodeObject
        Implements ICodeObject




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

        Public Shared Function Create(ByRef parser As PgmrClass, ByVal ID As Long) As ICodeObject
            If parser Is Nothing Or ID = 0 Then Return Nothing
            Create = New vbCodeObject(parser, ID)
        End Function




#Region " Properties "

        Public ReadOnly Property ID() As Long Implements ICodeObject.ID
            Get
                Return _id
            End Get

        End Property

        Public ReadOnly Property Text() As String Implements ICodeObject.Text
            Get
                Return _text
            End Get

        End Property

        Public ReadOnly Property Type() As String Implements ICodeObject.Type
            Get
                Return _type
            End Get
        End Property

        Public ReadOnly Property ChildCount() As Integer Implements ICodeObject.ChildCount
            Get
                Return _p.GetNumChildren(_id)
            End Get
        End Property

        Public ReadOnly Property NumDescendants() As Integer Implements ICodeObject.NumDescendants
            Get
                Return _p.GetNumDescendants(_id)
            End Get
        End Property

        Public ReadOnly Property NextSibling() As ICodeObject Implements ICodeObject.NextSibling
            Get
                Return vbCodeObject.Create(_p, _p.GetNextSibling(_id))
            End Get
        End Property
        Public ReadOnly Property FirstChild() As ICodeObject Implements ICodeObject.FirstChild
            Get
                Return vbCodeObject.Create(_p, _p.GetChild(_id, 0))
            End Get
        End Property
        Public ReadOnly Property LastChild() As ICodeObject Implements ICodeObject.LastChild
            Get
                Return vbCodeObject.Create(_p, _p.GetChild(_id, _p.GetNumChildren(_id) - 1))
            End Get
        End Property

        Public ReadOnly Property PreviousSibling() As ICodeObject Implements ICodeObject.PreviousSibling
            Get
                Return vbCodeObject.Create(_p, _p.GetPrevSibling(_id))
            End Get
        End Property


        Public ReadOnly Property Parent() As ICodeObject Implements ICodeObject.Parent
            Get
                Return vbCodeObject.Create(_p, _p.GetParent(_id))
            End Get
        End Property


#End Region





    End Class
End Namespace

