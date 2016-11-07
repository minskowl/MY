Imports System.CodeDom
Imports System.Drawing
Imports System.Runtime.InteropServices
Namespace CodeParser

    Public Class VBCodeBuilderMSCodeDom
        Implements Compiler.ICodeParser


#Region " Constants "

        Private tagProgram As String = "Program"
        Private tagOptionDirective As String = "OptionDirective"
        Private tagImportsDirective As String = "ImportsDirective"
        Private tagNamespaceBody As String = "NamespaceBody"
        Private tagModifiedAttributes As String = "ModifiedAttributes"
        Private tagTypeDeclaration As String = "TypeDeclaration"
        Private tagModuleDeclaration As String = "ModuleDeclaration"
        Private tagNonModuleDeclaration As String = "NonModuleDeclaration"
        Private tagInterfaceDeclaration As String = "InterfaceDeclaration"
        Private tagClassDeclaration As String = "ClassDeclaration"
        Private tagInterfaceName As String = "InterfaceName"
        Private tagInterfaceModifier As String = "InterfaceModifier"
        Private tagNamespaceDeclaration As String = "NamespaceDeclaration"
        Private tagMethodMemberDeclaration As String = "MethodMemberDeclaration"
        Private tagMethodDeclaration As String = "MethodDeclaration"

        Private _p As ICodeParser
        Private _defCodeNameSpace As CodeNamespace
#End Region

#Region " Main Methods "

        Private Sub New(ByRef CodeParser As ICodeParser)
            _p = CodeParser
        End Sub
        Public Shared Function Create(ByRef CodeParser As ICodeParser) As VBCodeBuilderMSCodeDom
            If CodeParser Is Nothing Then Return Nothing
            Return New VBCodeBuilderMSCodeDom(CodeParser)
        End Function
        Public Function Parse(ByVal program As String) As CodeCompileUnit
            Parse = New CodeCompileUnit
            _p.ParseString(program)

            Dim obj As iCodeObject = _p.GetRoot().FirstChild
            If obj Is Nothing Then Exit Function
            If obj.Type = tagProgram Then ParseProgram(Parse, obj)

        End Function

        Public Function Parse(ByVal codeStream As System.IO.TextReader) As CodeCompileUnit Implements System.CodeDom.Compiler.ICodeParser.Parse
            Dim res As New CodeCompileUnit

            _p.ParseString(codeStream.ReadToEnd)
            codeStream.Close()
            Dim obj As iCodeObject = _p.GetRoot().FirstChild
            If obj Is Nothing Then Return res

            If obj.Type = tagProgram Then ParseProgram(res, obj)
            Return res
        End Function

        Private Sub ParseProgram(ByRef unit As CodeCompileUnit, ByRef program As vbCodeObject)
            'Program ::= 
            '[{Source}];

            If program.ChildCount = 0 Then Exit Sub
            Dim obj As vbCodeObject = program.FirstChild
            If obj.ChildCount = 0 Then Exit Sub

            _defCodeNameSpace = New CodeNamespace
            unit.Namespaces.Add(_defCodeNameSpace)

            If obj.ChildCount = 0 Then Throw New Exception("ParseProgram: More sources then 1")
            obj = obj.FirstChild
            'Source ::=
            '	[{OptionDirective}]
            '	[{ImportsDirective}]
            '	[{ModifiedAttributes}]
            '	[NamespaceBody];

            While Not obj Is Nothing
                Select Case obj.Type      'Case tagOptionDirective   'Case tagModifiedAttributes

                    Case tagImportsDirective
                        ParseImportsDirective(_defCodeNameSpace, obj)
                    Case tagNamespaceBody
                        ParseNamespaceBody(unit, obj)
                    Case Else
                        Throw New exNotEmplemented(obj.Type)
                End Select
                obj = obj.NextSibling
            End While


        End Sub

        Private Sub ParseImportsDirective(ByRef ns As CodeNamespace, ByRef Directive As vbCodeObject)
            'ImportsDirective ::= 
            '	"Imports" ImportsClauses LineTerminator ;
            Dim obj As vbCodeObject = Directive.FirstChild
            While Not obj Is Nothing
                Select Case obj.Type
                    Case "ImportsClauses"
                        ns.Imports.AddRange(ParseImportsClauses(obj))
                    Case Else
                        Throw New exNotEmplemented(obj.Type)
                End Select
                obj = obj.NextSibling
            End While
        End Sub
        Private Function ParseImportsClauses(ByRef Directive As vbCodeObject) As CodeNamespaceImport()
            'ImportsClauses ::=
            '	{ImportsClause, ","} ;
            Dim impArr(Directive.ChildCount - 1) As CodeNamespaceImport
            Dim i As Integer

            Dim obj As vbCodeObject = Directive.FirstChild

            While Not obj Is Nothing
                Select Case obj.Type
                    Case "ImportsClause"
                        impArr(i) = ParseImportsClause(obj)
                    Case Else
                        Throw New exNotEmplemented(obj.Type)
                End Select
                obj = obj.NextSibling
                i += 1
            End While

            Return impArr
        End Function
        Private Function ParseImportsClause(ByRef Directive As vbCodeObject) As CodeNamespaceImport
            'ImportsClause ::= 
            '	  ImportsAliasClause 
            '	| RegularImportsClause ;
            Dim obj As vbCodeObject = Directive.FirstChild
            Select Case obj.Type
                Case "RegularImportsClause"
                    Return New CodeNamespaceImport(obj.Text)
                Case Else
                    Throw New exNotEmplemented(obj.Type)
            End Select
        End Function

#End Region


#Region " NameSpace "

        Private Sub ParseNamespaceBody(ByRef unit As CodeCompileUnit, ByRef body As vbCodeObject)
            'NamespaceBody ::=
            '	[{NamespaceMemberDeclaration}]  ;

            If body.ChildCount = 0 Then Exit Sub
            Dim obj As vbCodeObject = body.FirstChild

            While Not obj Is Nothing
                ParseNamespaceMemberDeclaration(unit, obj)
                obj = obj.NextSibling
            End While

        End Sub
        Private Sub ParseNamespaceMemberDeclaration(ByRef unit As CodeCompileUnit, ByRef decl As vbCodeObject)
            'NamespaceMemberDeclaration ::=
            '     NamespaceDeclaration
            '	| TypeDeclaration 
            '	| ModuleMemberDeclaration
            '	| Block ;
            Dim obj As vbCodeObject = decl.FirstChild
            Select Case obj.Type
                Case tagTypeDeclaration
                    ParseTypeDeclaration(_defCodeNameSpace, obj)
                Case tagNamespaceDeclaration
                    ParseNamespaceDeclaration(unit, obj)
                    'Case "ModuleMemberDeclaration"
                    '    ParseModuleMemberDeclaration()

                Case Else
                    Throw New exNotEmplemented(obj.Type)
            End Select
        End Sub
        Private Sub ParseNamespaceMemberDeclaration(ByRef ns As CodeNamespace, ByRef decl As vbCodeObject)
            'NamespaceMemberDeclaration ::=
            '     NamespaceDeclaration
            '	| TypeDeclaration 
            '	| ModuleMemberDeclaration
            '	| Block ;
            Dim obj As vbCodeObject = decl.FirstChild
            Select Case obj.Type
                Case tagTypeDeclaration
                    ParseTypeDeclaration(ns, obj)
                Case Else
                    Throw New exNotEmplemented(obj.Type)
            End Select
        End Sub
        Private Sub ParseNamespaceDeclaration(ByRef unit As CodeCompileUnit, ByRef decl As vbCodeObject)
            'NamespaceDeclaration ::=
            '	[Attributes]
            '	NamespaceTag NamespaceName LineTerminator
            '	[{ NamespaceMemberDeclaration }] 
            '	"End" "Namespace" LineTerminator ;

            Dim obj As vbCodeObject = decl.FirstChild

            Dim objNamespace As New CodeNamespace

            While Not obj Is Nothing
                Select Case obj.Type
                    Case "NamespaceTag"
                    Case "NamespaceName"
                        objNamespace.Name = obj.Text
                    Case "NamespaceMemberDeclaration"
                        ParseNamespaceMemberDeclaration(objNamespace, obj)
                    Case Else
                        Throw New exNotEmplemented(obj.Type)
                End Select
                obj = obj.NextSibling
            End While
            unit.Namespaces.Add(objNamespace)
        End Sub

#End Region

#Region " Interface "


        Private Sub ParseInterfaceDeclaration(ByRef ns As CodeNamespace, ByRef decl As vbCodeObject)
            'InterfaceDeclaration ::=
            '	[Attributes] [{InterfaceModifier}] 
            '	"Interface" InterfaceName LineTerminator
            '	[{InterfaceBase}]
            '	[{InterfaceMemberDeclaration}]
            '	"End" "Interface" LineTerminator ;


            Dim obj As vbCodeObject = decl.FirstChild

            Dim objInterface As New CodeTypeDeclaration
            objInterface.IsInterface = True
            objInterface.TypeAttributes = Reflection.TypeAttributes.Interface
            While Not obj Is Nothing
                Select Case obj.Type
                    Case tagInterfaceName
                        objInterface.Name = obj.Text
                    Case tagInterfaceModifier
                        ParseInterfaceModifier(objInterface, obj)
                    Case "InterfaceMemberDeclaration"
                        ParseInterfaceMemberDeclaration(objInterface, obj)
                    Case Else
                        Throw New exNotEmplemented(obj.Type)
                End Select
                obj = obj.NextSibling
            End While



            ns.Types.Add(objInterface)

        End Sub
        Private Function ParseInterfaceModifier(ByRef inter As CodeTypeDeclaration, ByRef modif As vbCodeObject) As MemberAttributes
            'InterfaceModifier ::= 
            '	  AccessModifier 
            '	| "Shadows" ;
            Dim obj As vbCodeObject = modif.FirstChild
            Select Case obj.Type

                Case "AccessModifier"
                    inter.Attributes = inter.Attributes Or ParseAccessModifier(obj)
                Case Else
                    Throw New exNotEmplemented(obj.Type)
            End Select
        End Function
        Private Sub ParseInterfaceMemberDeclaration(ByRef inter As CodeTypeDeclaration, ByRef decl As vbCodeObject)

            'InterfaceMemberDeclaration ::=
            '	  NonModuleDeclaration 
            '	| EventMemberDeclaration 
            '	| MethodMemberDeclaration 
            '	| PropertyMemberDeclaration ;
            If decl.ChildCount = 0 Then Exit Sub

            Dim obj As vbCodeObject = decl.FirstChild
            While Not obj Is Nothing
                Select Case obj.Type
                    Case tagMethodMemberDeclaration
                        inter.Members.Add(ParseMethodMemberDeclaration(obj))
                    Case "PropertyMemberDeclaration"
                        inter.Members.Add(ParsePropertyMemberDeclaration(obj))
                    Case Else
                        Throw New exNotEmplemented(obj.Type)
                End Select
                obj = obj.NextSibling
            End While

        End Sub


#End Region

#Region " Parse Method "

        Private Function ParseMethodMemberDeclaration(ByRef decl As vbCodeObject) As CodeTypeMember
            'MethodMemberDeclaration ::= 
            '	  MethodDeclaration 
            '	| ExternalMethodDeclaration ;

            Select Case decl.FirstChild.Type
                Case tagMethodDeclaration
                    Return ParseMethodDeclaration(decl.FirstChild)
                Case Else
                    Throw New exNotEmplemented(decl.FirstChild.Type)
            End Select

        End Function
        Private Function ParseMethodDeclaration(ByRef decl As vbCodeObject) As CodeMemberMethod
            'MethodDeclaration ::= 
            '	  SubDeclaration 
            '	| FunctionDeclaration ;
            Dim obj As vbCodeObject = decl.FirstChild

            Select Case obj.Type
                Case "SubDeclaration"
                    Return ParseSubDeclaration(obj)
                Case "FunctionDeclaration"
                    Return ParseFunctionDeclaration(obj)
                Case Else
                    Throw New exNotEmplemented(obj.Type)
            End Select

        End Function
        Private Function ParseSubDeclaration(ByRef decl As vbCodeObject) As CodeMemberMethod
            'SubDeclaration ::=
            '	[Attributes] [{ProcedureModifier}]
            '	"Sub" ProcedureName
            '	["(" [FormalParameterList] ")"]
            '	[HandlesOrImplements] 
            '	Body
            '	["End" "Sub" LineTerminator] ;

            Dim obj As vbCodeObject = decl.FirstChild
            Dim meth As New CodeMemberMethod
            'meth.Attributes = 0 'MemberAttributes.Overloaded
            'meth.
            While Not obj Is Nothing
                Select Case obj.Type
                    Case "ProcedureName"
                        meth.Name = obj.Text
                    Case "ProcedureModifier"
                        meth.Attributes = meth.Attributes Or ParseProcedureModifier(obj)
                    Case "Body"
                        meth.Statements.AddRange(ParseBody(obj))
                    Case "FormalParameterList"
                        meth.Parameters.AddRange(ParseFormalParameterList(obj))
                    Case Else
                        Throw New exNotEmplemented(obj.Type)
                End Select
                obj = obj.NextSibling
            End While
            Return meth
        End Function
        Private Function ParseProcedureModifier(ByRef modif As vbCodeObject) As MemberAttributes
            'ProcedureModifier ::=
            '	AccessModifier 
            '	| ("Shadows" 
            '	| "Shared" 
            '	| "Overridable" 
            '	| "NotOverridable" 
            '	| "MustOverride" 
            '	| "Overrides" 
            '	| "Overloads") ;
            Dim obj As vbCodeObject = modif.FirstChild
            Dim res As MemberAttributes = 0
            If obj Is Nothing Then
                Select Case modif.Text
                    Case "Overrides"
                        res = res Or MemberAttributes.Override
                    Case "Shared"
                        res = res Or MemberAttributes.Static
                    Case Else
                        Throw New exNotEmplemented(obj.Type)
                End Select
            Else
                Select Case obj.Type
                    Case "AccessModifier"
                        res = res Or ParseAccessModifier(obj)
                    Case Else
                        Throw New exNotEmplemented(obj.Type)
                End Select
            End If
            Return res
        End Function

        Private Function ParseFunctionDeclaration(ByRef decl As vbCodeObject) As CodeMemberMethod
            'FunctionDeclaration ::=
            '	[Attributes] [{ProcedureModifier}]
            '	"Function" ProcedureName
            '	["(" [FormalParameterList] ")"] 
            '	["As" [Attributes] TypeSpec]
            '	[HandlesOrImplements] 
            '	Body
            '	["End" "Function" LineTerminator] ;
            Dim meth As New CodeMemberMethod
            meth.Attributes = 0

            Dim obj As vbCodeObject = decl.FirstChild

            While Not obj Is Nothing
                Select Case obj.Type
                    Case "ProcedureName"
                        meth.Name = obj.Text
                    Case "ProcedureModifier"
                        meth.Attributes = meth.Attributes Or ParseProcedureModifier(obj)
                    Case "FormalParameterList"
                        meth.Parameters.AddRange(ParseFormalParameterList(obj))
                    Case "TypeSpec"
                        meth.ReturnType = ParseTypeSpec(obj)
                    Case "Body"
                        ParseBodyOfMethod(meth, obj)
                    Case "HandlesOrImplements"
                        ParseHandlesOrImplements(meth, obj)
                    Case Else
                        Throw New exNotEmplemented(obj.Type)
                End Select
                obj = obj.NextSibling
            End While
            Return meth
        End Function
        Private Sub ParseHandlesOrImplements(ByRef meth As CodeMemberMethod, ByRef decl As vbCodeObject)
            Dim obj As vbCodeObject = decl.FirstChild

            While Not obj Is Nothing
                Select Case obj.Type
                    Case "MethodImplementsClause"
                        ParseMethodImplementsClause(meth, obj)
                    Case Else
                        Throw New exNotEmplemented(obj.Type)
                End Select
                obj = obj.NextSibling
            End While
        End Sub
        Private Sub ParseMethodImplementsClause(ByRef meth As CodeMemberMethod, ByRef decl As vbCodeObject)
            Dim obj As vbCodeObject = decl.FirstChild

            While Not obj Is Nothing
                Select Case obj.Type
                    Case "ImplementsClause"
                        ParseImplementsClause(meth, obj)
                    Case Else
                        Throw New exNotEmplemented(obj.Type)
                End Select
                obj = obj.NextSibling
            End While
        End Sub
        Private Sub ParseImplementsClause(ByRef meth As CodeMemberMethod, ByRef decl As vbCodeObject)
            'ImplementsClause ::= 
            '	"Implements" ImplementsList ;
            Dim obj As vbCodeObject = decl.FirstChild

            While Not obj Is Nothing
                Select Case obj.Type
                    Case "ImplementsList"
                        meth.ImplementationTypes.AddRange(ParseImplementsList(obj))

                    Case Else
                        Throw New exNotEmplemented(obj.Type)
                End Select
                obj = obj.NextSibling
            End While
        End Sub

        Private Sub ParseBodyOfMethod(ByRef meth As CodeMemberMethod, ByRef decl As vbCodeObject)
            'Body ::=
            '	LineTerminator
            '	[Block];
            Dim obj As vbCodeObject = decl.FirstChild

            While Not obj Is Nothing
                Select Case obj.Type
                    Case "Block"
                        meth.Statements.AddRange(ParseBlock(obj))
                    Case Else
                        Throw New exNotEmplemented(obj.Type)
                End Select
                obj = obj.NextSibling
            End While

        End Sub

#Region " Params "

        Private Function ParseFormalParameterList(ByRef params As vbCodeObject) As CodeParameterDeclarationExpressionCollection
            'FormalParameterList ::=
            '	{FormalParameter, ","} ;
            Dim parsColl As New CodeParameterDeclarationExpressionCollection

            If params.ChildCount = 0 Then Return parsColl


            Dim obj As vbCodeObject = params.FirstChild
            Dim par As CodeParameterDeclarationExpression

            While Not obj Is Nothing
                par = New CodeParameterDeclarationExpression
                ParseFormalParameter(par, obj)
                parsColl.Add(par)

                obj = obj.NextSibling
            End While

            Return parsColl
        End Function

        Private Sub ParseFormalParameter(ByRef param As CodeParameterDeclarationExpression, ByRef decl As vbCodeObject)
            'FormalParameter ::=
            '	[Attributes] 
            '	[{ParameterModifier}] 
            '	ParameterName [ArrayNameModifier]
            '	["As" TypeSpec]
            '	["=" ConstantExpression] ;
            Dim obj As vbCodeObject = decl.FirstChild

            While Not obj Is Nothing
                Select Case obj.Type
                    Case "ParameterModifier"
                        ParseParameterModifier(param, obj)
                    Case "ParameterName"
                        param.Name = obj.Text
                    Case "TypeSpec"
                        param.Type = ParseTypeSpec(obj)

                    Case Else
                        Throw New exNotEmplemented(obj.Type)
                End Select
                obj = obj.NextSibling
            End While

        End Sub

        Private Sub ParseParameterModifier(ByRef param As CodeParameterDeclarationExpression, ByRef modif As vbCodeObject)
            'ParameterModifier ::= 
            '	  "ByRef" 
            '	| "ByVal" 
            '	| "Optional" 
            '	| "ParamArray" ;

            Select Case modif.Text
                Case "ByRef"
                    param.Direction = FieldDirection.Ref
                Case "ByVal"
                    param.Direction = FieldDirection.In
                Case Else
                    Throw New exNotEmplemented(modif.Text)
            End Select
        End Sub
#End Region

#Region " Body "
        Private Function ParseBody(ByRef decl As vbCodeObject) As CodeStatementCollection
            'statement_block_contents<TERMINAL> ::=
            '	check_for_statement
            '	[{
            '		  comment
            '		| StringLiteral
            '		| *(    comment 
            '			| StringLiteral 
            '			| "End" whitespace block_item_name
            '		   )
            '	}] ;
            Dim obj As vbCodeObject = decl.FirstChild
            Dim col As New CodeStatementCollection

            While Not obj Is Nothing
                Select Case obj.Type
                    Case "Block"
                        col.AddRange(ParseBlock(obj))
                    Case Else
                        Throw New exNotEmplemented(obj.Type)
                End Select
                obj = obj.NextSibling
            End While
            Return col
        End Function
        Private Function ParseBlock(ByRef decl As vbCodeObject) As CodeStatementCollection
            'Block ::= 
            '	[{LabeledLine}] ;
            Dim obj As vbCodeObject = decl.FirstChild
            Dim res As New CodeStatementCollection
            While Not obj Is Nothing
                Select Case obj.Type
                    Case "LabeledLine"
                        ParseLabeledLine(res, obj)
                    Case Else
                        Throw New exNotEmplemented(obj.Type)
                End Select
                obj = obj.NextSibling
            End While
            Return res
        End Function
        Private Sub ParseLabeledLine(ByRef col As CodeStatementCollection, ByRef decl As vbCodeObject)
            'LabeledLine ::= 
            '	[LabelName (":" | (? ^.LabelName.IntLiteral))]
            '	[Statements]
            '	[LineTerminator] ;
            Dim obj As vbCodeObject = decl.FirstChild
            While Not obj Is Nothing
                Select Case obj.Type
                    Case "Statements"
                        ParseStatements(col, obj)
                    Case Else
                        Throw New exNotEmplemented(obj.Type)
                End Select
                obj = obj.NextSibling
            End While
        End Sub


#End Region

#End Region

#Region " Class "

        Private Function ParseClassDeclaration(ByRef decl As vbCodeObject) As CodeTypeDeclaration
            'ClassDeclaration ::=
            '	[Attributes] [{ClassModifier}] 
            '	"Class" ClassName [LineTerminator]
            '	[ClassBase]
            '	[{TypeImplementsClause}]
            '	[{ClassMemberDeclaration}]
            '	"End" "Class" LineTerminator ;
            If decl.ChildCount = 0 Then Return Nothing
            Dim objClass As New CodeTypeDeclaration

            objClass.IsClass = True
            '  objClass.TypeAttributes = Reflection.TypeAttributes.Class


            Dim obj As vbCodeObject = decl.FirstChild

            While Not obj Is Nothing
                Select Case obj.Type
                    Case "ClassModifier"
                        objClass.Attributes = ParseClassModifier(obj)
                    Case "TypeImplementsClause"
                        ParseTypeImplementsClause(objClass, obj)
                    Case "ClassName"
                        objClass.Name = obj.Text
                    Case "ClassMemberDeclaration"
                        ParseClassMemberDeclaration(objClass, obj)
                    Case "Attributes"
                        objClass.CustomAttributes = ParseAttributes(obj)
                    Case Else
                        Throw New exNotEmplemented(obj.Type)
                End Select
                obj = obj.NextSibling
            End While

            Return objClass
        End Function


        Private Sub ParseImplements(ByRef typedecl As CodeTypeDeclaration, ByRef impl As vbCodeObject)
            'Implements ::=
            '	{TypeSpec, ","} ;

            Dim obj As vbCodeObject = impl.FirstChild

            While Not obj Is Nothing
                Select Case obj.Type
                    Case "TypeSpec"
                        typedecl.BaseTypes.Add(ParseTypeSpec(obj))
                    Case Else
                        Throw New exNotEmplemented(obj.Type)
                End Select
                obj = obj.NextSibling
            End While
        End Sub

        Private Sub ParseClassMemberDeclaration(ByRef cl As CodeTypeDeclaration, ByRef decl As vbCodeObject)
            'ClassMemberDeclaration ::=
            '	  NonModuleDeclaration 
            '	| EventMemberDeclaration 
            '	| VariableMemberDeclaration 
            '	| ConstantMemberDeclaration 
            '	| MethodMemberDeclaration 
            '	| PropertyMemberDeclaration 
            '	| ConstructorMemberDeclaration ;
            Dim obj As vbCodeObject = decl.FirstChild

            While Not obj Is Nothing
                Select Case obj.Type
                    Case "VariableMemberDeclaration"
                        cl.Members.AddRange(ParseVariableMemberDeclaration(obj))
                    Case "ConstructorMemberDeclaration"
                        cl.Members.Add(ParseConstructorMemberDeclaration(obj))
                    Case "MethodMemberDeclaration"
                        cl.Members.Add(ParseMethodMemberDeclaration(obj))
                    Case "PropertyMemberDeclaration"
                        cl.Members.Add(ParsePropertyMemberDeclaration(obj))
                    Case "ConstantMemberDeclaration"
                        cl.Members.AddRange(ParseConstantMemberDeclaration(obj))
                    Case "NonModuleDeclaration"
                        cl.Members.AddRange(ParseNonModuleDeclaration(obj))
                    Case Else
                        Throw New exNotEmplemented(obj.Type)
                End Select
                obj = obj.NextSibling
            End While

        End Sub
        Private Function ParseNonModuleDeclaration(ByRef decl As vbCodeObject) As CodeTypeMemberCollection
            'NonModuleDeclaration ::=
            '	  EnumDeclaration 
            '	| StructDeclaration 
            '	| InterfaceDeclaration 
            '	| ClassDeclaration 
            '	| DelegateTypeDeclaration ;
            Dim obj As vbCodeObject = decl.FirstChild
            Dim col As New CodeTypeMemberCollection

            While Not obj Is Nothing
                Select Case obj.Type
                    Case "EnumDeclaration"
                        col.Add(ParseEnumDeclaration(obj))
                    Case Else
                        Throw New exNotEmplemented(obj.Type)
                End Select
                obj = obj.NextSibling
            End While
            Return col
        End Function
        Private Function ParseEnumDeclaration(ByRef decl As vbCodeObject) As CodeTypeDeclaration
            'EnumDeclaration ::=
            '	[Attributes] 
            '	[{EnumModifier}] 
            '	"Enum" EnumName
            '	["As" IntegralTypeName] LineTerminator
            '	{EnumMemberDeclaration}
            '	"End" "Enum" LineTerminator ;

            Dim obj As vbCodeObject = decl.FirstChild
            Dim res As New CodeTypeDeclaration
            res.IsEnum = True

            While Not obj Is Nothing
                Select Case obj.Type
                    Case "EnumName"
                        res.Name = obj.Text
                    Case "EnumModifier"
                        res.Attributes = ParseEnumModifier(obj)
                    Case "EnumMemberDeclaration"
                        res.Members.Add(ParseEnumMemberDeclaration(obj))
                    Case Else
                        Throw New exNotEmplemented(obj.Type)
                End Select
                obj = obj.NextSibling
            End While
            Return res
        End Function

        Private Function ParseEnumMemberDeclaration(ByRef decl As vbCodeObject) As CodeTypeMember
            'EnumMemberDeclaration ::=
            '	[Attributes] EnumMemberName 
            '	["=" ConstantExpression] LineTerminator ;
            Dim obj As vbCodeObject = decl.FirstChild
            Dim res As New CodeTypeMember


            While Not obj Is Nothing
                Select Case obj.Type
                    Case "EnumMemberName"
                        res.Name = obj.Text
                    Case Else
                        Throw New exNotEmplemented(obj.Type)
                End Select
                obj = obj.NextSibling
            End While
            Return res
        End Function

        Private Function ParseEnumModifier(ByRef modif As vbCodeObject) As MemberAttributes
            'EnumModifier ::= 
            '	  AccessModifier 
            '	| "Shadows" ;;
            Dim obj As vbCodeObject = modif.FirstChild
            Dim res As MemberAttributes = 0
            If obj Is Nothing Then
                Select Case modif.Text

                    Case Else
                        Throw New exNotEmplemented(obj.Type)
                End Select
            Else
                Select Case obj.Type
                    Case "AccessModifier"
                        res = res Or ParseAccessModifier(obj)
                    Case Else
                        Throw New exNotEmplemented(obj.Type)
                End Select
            End If
            Return res
        End Function

        Private Function ParseConstantMemberDeclaration(ByRef decl As vbCodeObject) As CodeTypeMemberCollection
            'ConstantMemberDeclaration ::=
            '	[Attributes] 
            '	[{ConstantModifier}]
            '	"Const" 
            '	{ConstDeclaration, ","}
            '	LineTerminator ;
            Dim obj As vbCodeObject = decl.FirstChild
            Dim col As New CodeTypeMemberCollection
            Dim matr As MemberAttributes
            While Not obj Is Nothing
                Select Case obj.Type
                    Case "ConstDeclaration"
                        col.Add(ParseConstDeclaration(obj, matr))
                    Case "ConstantModifier"
                        matr = ParseConstantModifier(obj)
                    Case Else
                        Throw New exNotEmplemented(obj.Type)
                End Select
                obj = obj.NextSibling
            End While
            Return col
        End Function
        Private Function ParseConstDeclaration(ByRef decl As vbCodeObject, ByVal attr As MemberAttributes) As CodeMemberField
            'ConstDeclaration ::=
            '	ConstantName
            '	["As" TypeSpec] 
            '	"=" ConstantExpression ;
            Dim obj As vbCodeObject = decl.FirstChild
            Dim co As New CodeMemberField
            co.Attributes = attr
            While Not obj Is Nothing
                Select Case obj.Type
                    Case "ConstName"
                        co.Name = obj.Text
                    Case "TypeSpec"
                        co.Type = ParseTypeSpec(obj)
                    Case "ConstantExpression"
                        co.InitExpression = ParseConstantExpression(obj)
                    Case Else
                        Throw New exNotEmplemented(obj.Type)
                End Select
                obj = obj.NextSibling
            End While
            Return co
        End Function
        Private Function ParseConstantExpression(ByRef decl As vbCodeObject) As CodeExpression
            'ConstantExpression <TERMINAL> ::= 
            '	Expression ;
            Dim obj As vbCodeObject = decl.FirstChild
            While Not obj Is Nothing
                Select Case obj.Type
                    Case "Expression"
                        Return ParseExpression(obj)
                    Case Else
                        Throw New exNotEmplemented(obj.Type)
                End Select
                obj = obj.NextSibling
            End While
            Return Nothing
        End Function
        Private Function ParseConstantModifier(ByRef decl As vbCodeObject) As MemberAttributes
            'ConstantModifier ::= 
            '	  AccessModifiers 
            '	| "Shadows" ;
            Dim obj As vbCodeObject = decl.FirstChild
            Dim matr As MemberAttributes
            While Not obj Is Nothing
                Select Case obj.Type
                    Case "AccessModifiers"
                        matr = matr Or ParseAccessModifiers(obj)
                    Case Else
                        Throw New exNotEmplemented(obj.Type)
                End Select
                obj = obj.NextSibling
            End While
            Return matr
        End Function
        Private Function ParseAccessModifiers(ByRef decl As vbCodeObject) As MemberAttributes
            'AccessModifiers ::= 
            '	{AccessModifier};
            Dim obj As vbCodeObject = decl.FirstChild
            Dim matr As MemberAttributes
            While Not obj Is Nothing
                Select Case obj.Type
                    Case "AccessModifier"
                        matr = matr Or ParseAccessModifier(obj)
                    Case Else
                        Throw New exNotEmplemented(obj.Type)
                End Select
                obj = obj.NextSibling
            End While
            Return matr
        End Function
        Private Function ParsePropertyMemberDeclaration(ByRef decl As vbCodeObject) As CodeMemberProperty
            'PropertyMemberDeclaration ::=
            '	[Attributes] [{PropertyModifier}]
            '	"Property" PropertyName
            '	["(" [FormalParameterList] ")"]
            '	["As" TypeSpec] 
            '	[ImplementsClause] LineTerminator
            '	[{PropertyAccessorDeclaration}]
            '	["End" "Property" LineTerminator] ;
            Dim obj As vbCodeObject = decl.FirstChild

            Dim prop As New CodeMemberProperty

            While Not obj Is Nothing
                Select Case obj.Type
                    Case "Attributes"
                        prop.CustomAttributes.AddRange(ParseAttributes(obj))
                    Case "PropertyModifier"
                        ParsePropertyModifier(prop, obj)
                    Case "PropertyName"
                        prop.Name = obj.Text
                    Case "TypeSpec"
                        prop.Type = ParseTypeSpec(obj)
                    Case "PropertyAccessorDeclaration"
                        ParsePropertyAccessorDeclaration(prop, obj)
                    Case "MethodImplementsClause"
                        prop.ImplementationTypes.AddRange(ParseImplementsClause(obj.FirstChild))
                    Case Else
                        Throw New exNotEmplemented(obj.Type)
                End Select
                obj = obj.NextSibling
            End While
            Return prop
        End Function
        Private Function ParseImplementsClause(ByRef decl As vbCodeObject) As CodeTypeReferenceCollection
            'ImplementsClause ::= 
            '	"Implements" ImplementsList ;
            Dim obj As vbCodeObject = decl.FirstChild

            While Not obj Is Nothing
                Select Case obj.Type
                    Case "ImplementsList"
                        Return ParseImplementsList(obj)
                    Case Else
                        Throw New exNotEmplemented(obj.Type)
                End Select
                obj = obj.NextSibling
            End While
            Return Nothing
        End Function
        Private Function ParseImplementsList(ByRef decl As vbCodeObject) As CodeTypeReferenceCollection
            'ImplementsList ::=
            '	{InterfaceMemberSpecifier,","};
            Dim obj As vbCodeObject = decl.FirstChild
            Dim res As New CodeTypeReferenceCollection
            While Not obj Is Nothing
                Select Case obj.Type
                    Case "InterfaceMemberSpecifier"
                        res.Add(New CodeTypeReference(obj.Text))
                    Case Else
                        Throw New exNotEmplemented(obj.Type)
                End Select
                obj = obj.NextSibling
            End While
            Return res
        End Function

        Private Sub ParsePropertyAccessorDeclaration(ByRef prop As CodeMemberProperty, ByRef decl As vbCodeObject)
            'PropertyAccessorDeclaration ::= 
            '	  PropertyGetDeclaration 
            '	| PropertySetDeclaration ;
            Dim obj As vbCodeObject = decl.FirstChild

            While Not obj Is Nothing
                Select Case obj.Type
                    Case "PropertySetDeclaration"

                        ParsePropertySetDeclaration(prop, obj)
                    Case "PropertyGetDeclaration"
                        ParsePropertyGetDeclaration(prop, obj)

                    Case Else
                        Throw New exNotEmplemented(obj.Type)
                End Select
                obj = obj.NextSibling
            End While
        End Sub
        Private Sub ParsePropertySetDeclaration(ByRef prop As CodeMemberProperty, ByRef decl As vbCodeObject)
            'PropertySetDeclaration ::=
            '	[Attributes]
            '	"Set"	["(" [FormalParameterList] ")"]
            '	Property_Intermediate
            '	"End" "Set" LineTerminator;
            Dim obj As vbCodeObject = decl.FirstChild

            While Not obj Is Nothing
                Select Case obj.Type
                    Case "FormalParameterList"
                        prop.Parameters.AddRange(ParseFormalParameterList(obj))
                    Case "Body"
                        prop.SetStatements.AddRange(ParseBody(obj))
                    Case Else
                        Throw New exNotEmplemented(obj.Type)
                End Select
                obj = obj.NextSibling
            End While
        End Sub
        Private Sub ParsePropertyGetDeclaration(ByRef prop As CodeMemberProperty, ByRef decl As vbCodeObject)
            'PropertyGetDeclaration ::=
            '	[Attributes]
            '	"Get"
            '	Property_Intermediate
            '	"End" "Get" LineTerminator;

            Dim obj As vbCodeObject = decl.FirstChild

            While Not obj Is Nothing
                Select Case obj.Type
                    Case "Body"
                        prop.GetStatements.AddRange(ParseBody(obj))
                    Case Else
                        Throw New exNotEmplemented(obj.Type)
                End Select
                obj = obj.NextSibling
            End While
        End Sub
        Private Sub ParsePropertyModifier(ByRef prop As CodeMemberProperty, ByRef modif As vbCodeObject)
            'PropertyModifier ::= 
            '	  AccessModifier 
            '	| ("Shadows" 
            '	| "Shared" 
            '	| "Overridable" 
            '	| "NotOverridable" 
            '	| "MustOverride" 
            '	| "Overrides" 
            '	| "Overloads")
            '	| "Default" 
            '	| "ReadOnly" 
            '	| "WriteOnly" ;
            If modif.ChildCount > 0 Then
                Dim obj As vbCodeObject = modif.FirstChild
                While Not obj Is Nothing
                    Select Case obj.Type
                        Case "ProcedureModifier"
                            prop.Attributes = ParseProcedureModifier(obj)
                        Case Else
                            Throw New exNotEmplemented(obj.Type)
                    End Select
                    obj = obj.NextSibling
                End While
            Else
                Select Case modif.Text

                    Case "ReadOnly"
                        prop.HasGet = True
                        prop.HasSet = False
                    Case Else
                        Throw New exNotEmplemented(modif.Text)
                End Select
            End If

        End Sub
        Private Function ParseConstructorMemberDeclaration(ByRef modif As vbCodeObject) As CodeConstructor
            'ConstructorMemberDeclaration ::= 
            '	  InstanceConstructorDeclaration 
            '	| SharedConstructorDeclaration ;
            Dim obj As vbCodeObject = modif.FirstChild
            Select Case obj.Type
                Case "InstanceConstructorDeclaration"
                    Return ParseInstanceConstructorDeclaration(obj)
                Case Else
                    Throw New exNotEmplemented(obj.Type)
            End Select
        End Function
        Private Function ParseInstanceConstructorDeclaration(ByRef decl As vbCodeObject) As CodeConstructor
            'InstanceConstructorDeclaration ::=
            '	[Attributes]
            '	[{InstanceConstructorModifier}]
            '	"Sub" "New"
            '	["(" [FormalParameterList] ")"]
            '	SubBody_Intermediate
            '	"End" "Sub" LineTerminator ;
            Dim obj As vbCodeObject = decl.FirstChild

            Dim meth As New CodeConstructor
            meth.Name = "New"
            While Not obj Is Nothing
                Select Case obj.Type
                    Case "InstanceConstructorModifier"
                        ParseInstanceConstructorModifier(CType(meth, CodeMemberMethod), obj)
                    Case "Body"
                        meth.Statements.AddRange(ParseBody(obj))

                    Case "FormalParameterList"
                        meth.Parameters.AddRange(ParseFormalParameterList(obj))
                    Case Else
                        Throw New exNotEmplemented(obj.Type)
                End Select
                obj = obj.NextSibling
            End While
            Return meth
        End Function


        Private Sub ParseInstanceConstructorModifier(ByRef meth As CodeMemberMethod, ByRef modif As vbCodeObject)
            'InstanceConstructorModifier ::= 
            '	  AccessModifier 
            '	| "Overloads" ;
            Dim obj As vbCodeObject = modif.FirstChild
            Select Case obj.Type
                Case "AccessModifier"
                    meth.Attributes = meth.Attributes Or ParseAccessModifier(obj)
                Case Else
                    Throw New exNotEmplemented(obj.Type)
            End Select
        End Sub


        Private Function ParseClassModifier(ByRef modif As vbCodeObject) As MemberAttributes
            'ClassModifier ::= 
            '	  AccessModifier 
            '	| "Shadows" 
            '	| "MustInherit" 
            '	| "NotInheritable" ;
            Dim obj As vbCodeObject = modif.FirstChild
            Dim res As MemberAttributes = 0

            Select Case obj.Type

                Case "MustInherit"
                    res = MemberAttributes.Abstract
                Case "NotInheritable"
                    res = MemberAttributes.Final
                Case "AccessModifier"
                    res = ParseAccessModifier(obj)
                Case Else
                    Throw New exNotEmplemented(obj.Type)
            End Select
            Return res

        End Function

#End Region


#Region " Attributes "

        Private Function ParseAttributes(ByRef decl As vbCodeObject) As CodeAttributeDeclarationCollection
            'Attributes ::= 
            '	"<" AttributeList ">" ;
            Dim obj As vbCodeObject = decl.FirstChild

            While Not obj Is Nothing
                Select Case obj.Type
                    Case "AttributeList"
                        Return ParseAttributeList(obj)

                    Case Else
                        Throw New exNotEmplemented(obj.Type)
                End Select
                obj = obj.NextSibling
            End While
            Return Nothing
        End Function
        Private Function ParseAttributeList(ByRef decl As vbCodeObject) As CodeAttributeDeclarationCollection
            'AttributeList ::=
            '	{Attribute, ","};
            Dim obj As vbCodeObject = decl.FirstChild
            Dim attCol As New CodeAttributeDeclarationCollection
            While Not obj Is Nothing
                Select Case obj.Type
                    Case "Attribute"
                        attCol.Add(ParseAttribute(obj))
                    Case Else
                        Throw New exNotEmplemented(obj.Type)
                End Select
                obj = obj.NextSibling
            End While
            Return attCol
        End Function
        Private Function ParseAttribute(ByRef decl As vbCodeObject) As CodeAttributeDeclaration
            'Attribute ::= 
            '	[AttributeModifier ":"] 
            '	AttributeName
            '	["(" [ AttributeArguments ] ")"] ;
            Dim obj As vbCodeObject = decl.FirstChild
            Dim att As New CodeAttributeDeclaration
            While Not obj Is Nothing
                Select Case obj.Type
                    Case "AttributeName"
                        If (obj.Text.Substring(obj.Text.Length - 1) = ")") Then
                            att.Name = obj.Text.Substring(0, obj.Text.Length - 2)
                        Else
                            att.Name = obj.Text
                        End If

                    Case "AttributeArguments"
                        att.Arguments.AddRange(ParseAttributeArguments(obj))
                    Case Else
                        Throw New exNotEmplemented(obj.Type)
                End Select
                obj = obj.NextSibling
            End While
            Return att
        End Function

        Private Function ParseAttributeArguments(ByRef decl As vbCodeObject) As CodeAttributeArgumentCollection
            'AttributeArguments ::=
            '	  AttributePositionalArgumentList 
            '	  ["," VariablePropertyInitializerList] 
            '	| VariablePropertyInitializerList ;
            Dim obj As vbCodeObject = decl.FirstChild
            Dim attrarg As New CodeAttributeArgumentCollection

            While Not obj Is Nothing
                Select Case obj.Type
                    Case "VariablePropertyInitializerList"
                        attrarg.AddRange(ParseVariablePropertyInitializerList(obj))
                    Case Else
                        Throw New exNotEmplemented(obj.Type)
                End Select
                obj = obj.NextSibling
            End While

            Return attrarg
        End Function

        Private Function ParseVariablePropertyInitializerList(ByRef decl As vbCodeObject) As CodeAttributeArgumentCollection
            'VariablePropertyInitializerList ::=
            '	{VariablePropertyInitializer, ","} ;
            Dim obj As vbCodeObject = decl.FirstChild
            Dim attrarg As New CodeAttributeArgumentCollection
            While Not obj Is Nothing
                Select Case obj.Type
                    Case "VariablePropertyInitializer"
                        attrarg.Add(ParseVariablePropertyInitializer(obj))
                    Case Else
                        Throw New exNotEmplemented(obj.Type)
                End Select
                obj = obj.NextSibling
            End While
            Return attrarg
        End Function

        Private Function ParseVariablePropertyInitializer(ByRef decl As vbCodeObject) As CodeAttributeArgument
            'VariablePropertyInitializer ::= 
            '	(VariableName | NamespaceTag)
            '	":=" ConstantExpression ;
            Dim obj As vbCodeObject = decl.FirstChild
            Dim atrarg As New CodeAttributeArgument
            While Not obj Is Nothing
                Select Case obj.Type
                    Case "VariableName"
                        atrarg.Name = obj.Text
                    Case "ConstantExpression"
                        atrarg.Value = ParseConstantExpression(obj)
                    Case Else
                        Throw New exNotEmplemented(obj.Type)
                End Select
                obj = obj.NextSibling
            End While

            Return atrarg
        End Function
#End Region


#Region " Variables "

        Private Function ParseVariableMemberDeclaration(ByRef decl As vbCodeObject) As CodeTypeMemberCollection
            'VariableMemberDeclaration ::=
            '	[Attributes] 
            '	[{VariableModifier}]
            '	VariableDeclarators
            '	LineTerminator ;
            Dim var As New CodeMemberField
            Dim col As New CodeTypeMemberCollection

            Dim obj As vbCodeObject = decl.FirstChild

            While Not obj Is Nothing
                Select Case obj.Type
                    Case "VariableModifier"
                        var.Attributes = ParseVariableModifier(obj)
                    Case "VariableDeclarators"
                        col.AddRange(ParseVariableDeclarators(var, obj))
                    Case "Attributes"
                        var.CustomAttributes.AddRange(ParseAttributes(obj))
                    Case Else
                        Throw New exNotEmplemented(obj.Type)
                End Select
                obj = obj.NextSibling
            End While
            Return col
        End Function
        Private Function ParseVariableDeclarators(ByRef var As CodeMemberField, ByRef decl As vbCodeObject) As CodeTypeMemberCollection
            'VariableDeclarators ::=
            '	{VariableDeclarator, ","} ;
            Dim obj As vbCodeObject = decl.FirstChild
            Dim col As New CodeTypeMemberCollection
            While Not obj Is Nothing
                Select Case obj.Type
                    Case "VariableDeclarator"
                        col.Add(ParseVariableDeclarator(var, obj))
                    Case Else
                        Throw New exNotEmplemented(obj.Type)
                End Select
                obj = obj.NextSibling
            End While

            Return col
        End Function
        Private Function ParseVariableDeclarator(ByRef templ As CodeMemberField, ByRef decl As vbCodeObject) As CodeMemberField
            'VariableDeclarator ::=
            '	VariableIdentifier 
            '	["As" [NewTag] TypeSpec 
            '	[Arguments]] 
            '	["=" VariableInitializer] ;
            Dim obj As vbCodeObject = decl.FirstChild
            Dim var As New CodeMemberField

            var.CustomAttributes = templ.CustomAttributes
            var.Attributes = templ.Attributes


            While Not obj Is Nothing
                Select Case obj.Type
                    Case "VariableIdentifier"
                        var.Name = obj.Text
                    Case "TypeSpec"
                        var.Type = ParseTypeSpec(obj)
                    Case "VariableInitializer"
                        var.InitExpression = ParseVariableInitializer(obj)

                    Case "ArgumentList"

                        var.Type.TypeArguments.AddRange(ParseTypeArguments(obj))
                        '    var.CustomAttributes = ParseAttributeList(obj)
                    Case Else
                        Throw New exNotEmplemented(obj.Type)

                End Select
                obj = obj.NextSibling
            End While
            Return var
        End Function
        Private Function ParseTypeArguments(ByRef decl As vbCodeObject) As CodeTypeReferenceCollection
            If decl.ChildCount <> 1 Or decl.FirstChild.Type <> "PositionalArgumentList" Then
                Throw New exNotEmplemented(decl.FirstChild.Type)
            End If

            Dim obj As vbCodeObject = decl.FirstChild.FirstChild
            Dim col As New CodeTypeReferenceCollection
            While Not obj Is Nothing
                Select Case obj.Type

                    Case "ArgumentExpression"
                        col.Add(obj.Text.Substring(3))
                    Case Else
                        Throw New exNotEmplemented(obj.Type)


                End Select
                obj = obj.NextSibling
            End While
            Return col
        End Function

        Private Function ParseVariableInitializer(ByRef decl As vbCodeObject) As CodeExpression
            'VariableInitializer ::= 
            '	  RegularInitializer 
            '	| ArrayElementInitializer ;
            Dim obj As vbCodeObject = decl.FirstChild
            Select Case obj.Type
                Case "RegularInitializer"
                    Return ParseRegularInitializer(obj)
                Case Else
                    Throw New exNotEmplemented(obj.Type)
            End Select

        End Function
        Private Function ParseRegularInitializer(ByRef decl As vbCodeObject) As CodeExpression
            'RegularInitializer ::= 
            '	Expression ;
            Dim obj As vbCodeObject = decl.FirstChild
            Select Case obj.Type
                Case "Expression"
                    Return ParseExpression(obj)
                Case Else
                    Throw New exNotEmplemented(obj.Type)
            End Select
        End Function
        Private Function ParseVariableModifier(ByRef decl As vbCodeObject) As MemberAttributes

            'VariableModifier ::= 
            '	  AccessModifiers 
            '	| "Dim"
            '	| "Shadows" 
            '	| "Shared" 
            '	| "ReadOnly" 
            '	| "WithEvents" ;
            Dim obj As vbCodeObject = decl.FirstChild
            Dim res As MemberAttributes
            If obj Is Nothing Then
                Select Case decl.Text
                    Case "Shared"
                        res = MemberAttributes.Static
                    Case "WithEvents"
                        '      res=
                    Case Else
                        Throw New exNotEmplemented(decl.Text)
                End Select

            Else
                Select Case obj.Type
                    Case "AccessModifiers"
                        res = ParseAccessModifier(obj)
                    Case Else
                        Throw New exNotEmplemented(obj.Type)
                End Select
            End If

            Return res
        End Function

#End Region

#Region " Statements "
        Private Sub ParseStatements(ByRef col As CodeStatementCollection, ByRef decl As vbCodeObject)
            'Statements ::=
            '	[{Statement}] ;
            Dim obj As vbCodeObject = decl.FirstChild
            While Not obj Is Nothing
                Select Case obj.Type
                    Case "Statement"
                        col.AddRange(ParseStatement(obj))

                    Case Else
                        Throw New exNotEmplemented(obj.Type)
                End Select
                obj = obj.NextSibling
            End While
        End Sub
        Private Function ParseStatement(ByRef decl As vbCodeObject) As CodeStatementCollection
            'Statement ::= 
            '	  LocalDeclarationStatement 
            '	| WithStatement 
            '	| SyncLockStatement 
            '	| EventStatement 
            '	| AddHandlerStatement 
            '	| RemoveHandlerStatement 
            '	| AssignmentStatement 
            '	| InvocationStatement
            '	| ConditionalStatement 
            '	| LoopStatement 
            '	| ExceptionHandlingStatement 
            '	| ControlFlowStatement 
            '	| ArrayHandlingStatement 
            '	| InvocationStatement 
            '	;
            Dim obj As vbCodeObject = decl.FirstChild
            Dim col As New CodeStatementCollection
            Select Case obj.Type
                Case "AssignmentStatement"
                    col.Add(ParseAssignmentStatement(obj))
                Case "InvocationStatement"
                    col.Add(ParseInvocationStatement(obj))
                Case "ControlFlowStatement"
                    col.Add(ParseControlFlowStatement(obj))
                Case "ConditionalStatement"
                    col.AddRange(ParseConditionalStatement(obj))
                Case "LocalDeclarationStatement"
                    col.AddRange(ParseLocalDeclarationStatement(obj))
                Case "ExceptionHandlingStatement"
                    col.AddRange(ParseExceptionHandlingStatement(obj))
                Case Else
                    Throw New exNotEmplemented(obj.Type)
            End Select
            Return col
        End Function
        Private Function ParseExceptionHandlingStatement(ByRef decl As vbCodeObject) As CodeStatementCollection
            'ExceptionHandlingStatement ::=
            '	  StructuredExceptionStatement 
            '	| UnstructuredExceptionStatement ;
            Dim obj As vbCodeObject = decl.FirstChild


            Select Case obj.Type
                Case "StructuredExceptionStatement"
                    Return ParseStructuredExceptionStatement(obj)

                Case Else
                    Throw New exNotEmplemented(obj.Type)
            End Select
            Return New CodeStatementCollection
        End Function
        Private Function ParseStructuredExceptionStatement(ByRef decl As vbCodeObject) As CodeStatementCollection
            'StructuredExceptionStatement ::=
            '	  ThrowStatement 
            '	| TryStatement ;
            Dim obj As vbCodeObject = decl.FirstChild

            Dim res As New CodeStatementCollection
            Select Case obj.Type
                Case "TryStatement"

                    res.Add(ParseTryStatement(obj))
                Case Else
                    Throw New exNotEmplemented(obj.Type)
            End Select
            Return res
        End Function
        Private Function ParseTryStatement(ByRef decl As vbCodeObject) As CodeTryCatchFinallyStatement
            'TryStatement ::=
            '	"Try" StatementTerminator
            '	[Block]
            '	[{CatchStatement}]
            '	[FinallyStatement]
            '	"End" "Try" StatementTerminator ;

            Dim obj As vbCodeObject = decl.FirstChild
            Dim stat As New CodeTryCatchFinallyStatement

            While Not obj Is Nothing
                Select Case obj.Type
                    Case "Block"
                        stat.TryStatements.AddRange(ParseBlock(obj))
                    Case "CatchStatement"
                        stat.CatchClauses.Add(ParseCatchStatement(obj))
                    Case Else
                        Throw New exNotEmplemented(obj.Type)
                End Select
                obj = obj.NextSibling
            End While
            Return stat
        End Function
        Private Function ParseCatchStatement(ByRef decl As vbCodeObject) As CodeCatchClause
            'CatchStatement ::=
            '	"Catch" [vbnet_Identifier "As" TypeSpec]
            '	["When" BooleanExpression] StatementTerminator
            '	[Block] ;

            Dim obj As vbCodeObject = decl.FirstChild
            Dim res As CodeCatchClause = Nothing

            While Not obj Is Nothing
                Select Case obj.Type
                    Case "vbnet_Identifier"
                        res = New CodeCatchClause(obj.Text)
                    Case "TypeSpec"
                        res.CatchExceptionType = ParseTypeSpec(obj)
                    Case "Block"
                        res.Statements.AddRange(ParseBlock(obj))
                    Case Else
                        Throw New exNotEmplemented(obj.Type)
                End Select
                obj = obj.NextSibling
            End While
            Return res
        End Function
        Private Function ParseLocalDeclarationStatement(ByRef decl As vbCodeObject) As CodeStatementCollection
            '  Throw New NotImplementedException("ParseLocalDeclarationStatement")

            'LocalDeclarationStatement ::= 
            '	LocalModifier LocalDeclarator StatementTerminator ;
            Dim res As CodeStatementCollection = Nothing
            Dim obj As vbCodeObject = decl.FirstChild

            Dim LocalModifier As String
            While Not obj Is Nothing
                Select Case obj.Type
                    Case "LocalModifier"
                        LocalModifier = obj.Text
                    Case "LocalDeclarator"
                        res = ParseLocalDeclarator(obj)
                    Case Else
                        Throw New exNotEmplemented(obj.Type)
                End Select
                obj = obj.NextSibling
            End While

            Return res

        End Function
        Private Function ParseLocalDeclarator(ByRef decl As vbCodeObject) As CodeStatementCollection
            'LocalDeclarator ::=
            '	VariableDeclarators;
            Dim res As CodeStatementCollection = Nothing
            Dim obj As vbCodeObject = decl.FirstChild
            While Not obj Is Nothing
                Select Case obj.Type
                    Case "VariableDeclarators"
                        res = ParseVariableDeclarators(obj)
                    Case Else
                        Throw New exNotEmplemented(obj.Type)
                End Select
                obj = obj.NextSibling
            End While
            Return res
        End Function

        Private Function ParseVariableDeclarators(ByRef decl As vbCodeObject) As CodeStatementCollection
            'VariableDeclarators ::=
            '	{VariableDeclarator, ","} ;
            Dim obj As vbCodeObject = decl.FirstChild
            Dim col As New CodeStatementCollection
            While Not obj Is Nothing
                Select Case obj.Type
                    Case "VariableDeclarator"
                        col.Add(ParseVariableDeclarator(obj))
                    Case Else
                        Throw New exNotEmplemented(obj.Type)
                End Select
                obj = obj.NextSibling
            End While
            Return col
        End Function
        Private Function ParseVariableDeclarator(ByRef decl As vbCodeObject) As CodeVariableDeclarationStatement
            'VariableDeclarator ::=
            '	VariableIdentifier 
            '	["As" [NewTag] TypeSpec 
            '	["(" ArgumentList ")"]] 
            '	["=" VariableInitializer] ;

            Dim obj As vbCodeObject = decl.FirstChild
            Dim res As New CodeVariableDeclarationStatement
            Dim withNew As Boolean = False
            While Not obj Is Nothing
                Select Case obj.Type
                    Case "VariableIdentifier"
                        res.Name = obj.Text
                    Case "TypeSpec"
                        res.Type = ParseTypeSpec(obj)
                        If withNew Then res.InitExpression = New CodeObjectCreateExpression(res.Type)
                    Case "NewTag"
                        withNew = True
                    Case "ArgumentList"
                        CType(res.InitExpression, CodeObjectCreateExpression).Parameters.AddRange(ParseArgumentList(obj))
                    Case "VariableInitializer"
                        res.InitExpression = ParseVariableInitializer(obj)
                    Case Else
                        Throw New exNotEmplemented(obj.Type)
                End Select
                obj = obj.NextSibling
            End While


            Return res
        End Function
        Private Function ParseLocalModifier(ByRef decl As vbCodeObject) As CodeStatement
            'LocalModifier ::= 
            '	  "Dim" 
            '	| "Const"
            '	| "Static" ;



            Select Case decl.Text
                Case "Dim"
                    Dim obj As vbCodeObject = decl.NextSibling
                    Dim declVar As New CodeVariableDeclarationStatement

                    Return declVar
                Case Else
                    Throw New exNotEmplemented(decl.Text)
            End Select

        End Function

        Private Function ParseConditionalStatement(ByRef decl As vbCodeObject) As CodeStatementCollection
            'ConditionalStatement ::= 
            '	  IfStatement 
            '	| SelectStatement ;

            Dim obj As vbCodeObject = decl.FirstChild
            Dim col As New CodeStatementCollection
            Select Case obj.Type
                Case "IfStatement"
                    col.Add(ParseIfStatement(obj))
                Case "SelectStatement"
                    ParseSelectStatement(col, obj)
                Case Else
                    Throw New exNotEmplemented(obj.Type)
            End Select
            Return col
        End Function
        Private Sub ParseSelectStatement(ByRef stats As CodeStatementCollection, ByRef decl As vbCodeObject)
            'SelectStatement ::=
            '	"Select" ["Case"] 
            '	Expression StatementTerminator
            '
            '	[{CaseStatement}]
            '	[CaseElseStatement]
            '	"End" "Select" StatementTerminator ;
            Dim exp As CodeExpression = Nothing

            Dim obj As vbCodeObject = decl.FirstChild


            While Not obj Is Nothing
                Select Case obj.Type
                    Case "Expression"
                        exp = ParseExpression(obj)
                    Case "CaseStatement"
                        stats.Add(ParseCaseStatement(exp, obj))
                    Case Else
                        Throw New exNotEmplemented(obj.Type)
                End Select
                obj = obj.NextSibling
            End While
        End Sub
        Private Function ParseCaseStatement(ByRef exp As CodeExpression, ByRef decl As vbCodeObject) As CodeConditionStatement
            'CaseStatement ::=
            '	"Case" CaseClauses StatementTerminator
            '	[Block] ;

            Dim stat As New CodeConditionStatement

            Dim obj As vbCodeObject = decl.FirstChild
            While Not obj Is Nothing
                Select Case obj.Type
                    Case "CaseClauses"
                        stat.Condition = ParseCaseClauses(exp, obj)
                    Case "Block"
                        stat.TrueStatements.AddRange(ParseBlock(obj))
                    Case Else
                        Throw New exNotEmplemented(obj.Type)
                End Select
                obj = obj.NextSibling
            End While
            Return stat
        End Function
        Private Function ParseCaseClauses(ByRef exp As CodeExpression, ByRef decl As vbCodeObject) As CodeBinaryOperatorExpression
            'CaseClauses ::=
            '	{CaseClause, ","} ;
            Dim obj As vbCodeObject = decl.FirstChild

            If decl.ChildCount = 1 Then Return ParseCaseClause(exp, obj)


            Dim res As New CodeBinaryOperatorExpression
            While Not obj Is Nothing
                Select Case obj.Type
                    Case "CaseClause"
                        If res.Left Is Nothing Then
                            res.Left = ParseCaseClause(exp, obj)
                            res.[Operator] = CodeBinaryOperatorType.BooleanOr
                        ElseIf res.Right Is Nothing Then
                            res.Right = ParseCaseClause(exp, obj)
                        Else
                            Dim n As New CodeBinaryOperatorExpression
                            n.Left = res
                            n.[Operator] = CodeBinaryOperatorType.BooleanOr
                            res = n
                        End If

                    Case Else
                        Throw New exNotEmplemented(obj.Type)
                End Select
                obj = obj.NextSibling
            End While
            Return res
        End Function
        Private Function ParseCaseClause(ByRef exp As CodeExpression, ByRef decl As vbCodeObject) As CodeExpression
            'CaseClause ::=
            '	  ["Is"] ComparisonOperator Expression 
            '	| Expression ["To" Expression] ;
            Dim obj As vbCodeObject = decl.FirstChild
            While Not obj Is Nothing
                Select Case obj.Type
                    Case "Expression"
                        Dim result As New CodeBinaryOperatorExpression
                        result.Left = exp
                        result.[Operator] = CodeBinaryOperatorType.ValueEquality
                        result.Right = ParseExpression(obj)
                        Return result
                    Case Else
                        Throw New exNotEmplemented(obj.Type)
                End Select
                obj = obj.NextSibling
            End While
            Return Nothing
        End Function

        Private Function ParseIfStatement(ByRef decl As vbCodeObject) As CodeConditionStatement
            'IfStatement ::= 
            '	  BlockIfStatement 
            '	| LineIfThenStatement ;
            Dim obj As vbCodeObject = decl.FirstChild

            While Not obj Is Nothing
                Select Case obj.Type
                    Case "BlockIfStatement"
                        Return ParseBlockIfStatement(obj)
                    Case "LineIfThenStatement"
                        Return ParseLineIfThenStatement(obj)
                    Case Else
                        Throw New exNotEmplemented(obj.Type)
                End Select
                obj = obj.NextSibling
            End While
            Return Nothing
        End Function
        Private Function ParseLineIfThenStatement(ByRef decl As vbCodeObject) As CodeConditionStatement
            'LineIfThenStatement ::= 
            '	"If" BooleanExpression 
            '	"Then" [Statements]
            '	["Else" Statements] 
            '	[StatementTerminator];	
            Dim obj As vbCodeObject = decl.FirstChild
            Dim stat As New CodeConditionStatement

            While Not obj Is Nothing
                Select Case obj.Type
                    Case "BooleanExpression"
                        stat.Condition = ParseBooleanExpression(obj)
                    Case "Statements"
                        ParseStatements(stat.TrueStatements, obj)
                    Case Else
                        Throw New exNotEmplemented(obj.Type)
                End Select
                obj = obj.NextSibling
            End While
            Return stat
        End Function
        Private Function ParseBlockIfStatement(ByRef decl As vbCodeObject) As CodeConditionStatement
            'BlockIfStatement ::=
            '	"If" BooleanExpression 
            '	["Then"] StatementTerminator
            '	[Block]
            '	[{ElseIfStatement}]
            '	[ElseStatement]
            '	"End" "If" StatementTerminator ;
            Dim obj As vbCodeObject = decl.FirstChild
            Dim stat As New CodeConditionStatement

            While Not obj Is Nothing
                Select Case obj.Type
                    Case "BooleanExpression"
                        stat.Condition = ParseBooleanExpression(obj)
                    Case "ElseStatement"
                        ParseElseStatement(stat, obj)
                    Case "Block"
                        stat.TrueStatements.AddRange(ParseBlock(obj))
                    Case "ElseIfStatement"
                        ParseElseIfStatement(stat, obj)
                    Case Else
                        Throw New exNotEmplemented(obj.Type)
                End Select
                obj = obj.NextSibling
            End While
            Return stat
        End Function
        Private Sub ParseElseIfStatement(ByRef stat As CodeConditionStatement, ByRef decl As vbCodeObject)
            'ElseIfStatement ::=
            '	"ElseIf" BooleanExpression 
            '	["Then"] StatementTerminator
            '	[Block] ;
            Dim obj As vbCodeObject = decl.FirstChild
            Dim statN As New CodeConditionStatement
            stat.FalseStatements.Add(statN)

            While Not obj Is Nothing
                Select Case obj.Type
                    Case "BooleanExpression"
                        statN.Condition = ParseBooleanExpression(obj)
                    Case "Block"
                        stat.TrueStatements.AddRange(ParseBlock(obj))
                    Case Else
                        Throw New exNotEmplemented(obj.Type)
                End Select
                obj = obj.NextSibling
            End While
        End Sub
        Private Sub ParseElseStatement(ByRef stat As CodeConditionStatement, ByRef decl As vbCodeObject)
            'ElseStatement ::=
            '	"Else" StatementTerminator
            '	[Block] ;
            Dim obj As vbCodeObject = decl.FirstChild
            While Not obj Is Nothing
                Select Case obj.Type
                    Case "Block"
                        stat.FalseStatements.AddRange(ParseBlock(obj))
                    Case Else
                        Throw New exNotEmplemented(obj.Type)
                End Select
                obj = obj.NextSibling
            End While
        End Sub
        Private Function ParseControlFlowStatement(ByRef decl As vbCodeObject) As CodeStatement
            'ControlFlowStatement ::=
            '	  GotoStatement 
            '	| ExitStatement 
            '	| StopStatement 
            '	| EndStatement 
            '	| ReturnStatement ;
            Dim obj As vbCodeObject = decl.FirstChild
            Select Case obj.Type
                Case "ReturnStatement"
                    Return ParseReturnStatement(obj)
                Case "ExitStatement"
                    Return ParseExitStatement(obj)
                Case Else
                    Throw New exNotEmplemented(obj.Type)
            End Select
        End Function
        Private Function ParseExitStatement(ByRef decl As vbCodeObject) As CodeMethodReturnStatement

            'ExitStatement ::= 
            '	"Exit" ExitKind StatementTerminator ;

            Return New CodeMethodReturnStatement
        End Function
        Private Function ParseReturnStatement(ByRef decl As vbCodeObject) As CodeMethodReturnStatement
            'ReturnStatement ::= 
            '	"Return" [Expression] ;
            Dim obj As vbCodeObject = decl.FirstChild
            Dim stat As New CodeMethodReturnStatement
            While Not obj Is Nothing
                Select Case obj.Type
                    Case "Expression"
                        stat.Expression = ParseExpression(obj)
                    Case Else
                        Throw New exNotEmplemented(obj.Type)
                End Select
                obj = obj.NextSibling
            End While
            Return stat
        End Function
        Private Function ParseInvocationStatement(ByRef decl As vbCodeObject) As CodeExpressionStatement
            'InvocationStatement ::= 
            '	["Call"] InvocationExpression StatementTerminator ;
            Dim stat As New CodeExpressionStatement

            Dim obj As vbCodeObject = decl.FirstChild
            While Not obj Is Nothing
                Select Case obj.Type
                    Case "InvocationExpression"
                        stat.Expression = ParseInvocationExpression(obj)
                    Case Else
                        Throw New exNotEmplemented(obj.Type)
                End Select
                obj = obj.NextSibling
            End While
            Return stat
        End Function
        Private Function ParseAssignmentStatement(ByRef decl As vbCodeObject) As CodeAssignStatement
            'AssignmentStatement ::=
            '	AssignmentExpression
            '	[StatementTerminator] ;
            Dim obj As vbCodeObject = decl.FirstChild
            Dim stat As New CodeAssignStatement

            While Not obj Is Nothing
                Select Case obj.Type
                    Case "AssignmentExpression"
                        ParseAssignmentExpression(stat, obj)
                    Case Else
                        Throw New exNotEmplemented(obj.Type)
                End Select
                obj = obj.NextSibling
            End While
            Return stat
        End Function

        Private Sub ParseAssignmentExpression(ByRef stat As CodeAssignStatement, ByRef decl As vbCodeObject)
            'AssignmentExpression ::=
            '	  SimpleAssignment 
            '	| DelegateAssignment 
            '	| CompoundAssignment 
            '	| MidAssignment ;
            Dim obj As vbCodeObject = decl.FirstChild
            Select Case obj.Type
                Case "SimpleAssignment"
                    ParseSimpleAssignment(stat, obj)
                Case Else
                    Throw New exNotEmplemented(obj.Type)
            End Select
        End Sub

        Private Sub ParseSimpleAssignment(ByRef stat As CodeAssignStatement, ByRef decl As vbCodeObject)
            'SimpleAssignment ::= 
            '	VariableExpression "=" Expression ;
            Dim obj As vbCodeObject = decl.FirstChild
            While Not obj Is Nothing
                Select Case obj.Type
                    Case "VariableExpression"
                        stat.Left = ParseVariableExpression(obj)
                    Case "Expression"
                        stat.Right = ParseExpression(obj)
                    Case Else
                        Throw New exNotEmplemented(obj.Type)
                End Select
                obj = obj.NextSibling
            End While

        End Sub



#End Region

#Region " Expressions "
        Private Function ParseBooleanExpression(ByRef decl As vbCodeObject) As CodeExpression
            'BooleanExpression ::= 
            '	Expression ;
            Return ParseExpression(decl.FirstChild)
        End Function
        Private Function ParseInvocationExpression(ByRef decl As vbCodeObject) As CodeMethodInvokeExpression
            'InvocationExpression ::=
            '	InvocationTargetExpression
            '	[InvocationParams] ;
            Dim obj As vbCodeObject = decl.FirstChild


            While Not obj Is Nothing
                Select Case obj.Type
                    Case "InvocationTargetExpression"
                        Return ParseInvocationTargetExpression(obj)
                    Case Else
                        Throw New exNotEmplemented(obj.Type)
                End Select
                obj = obj.NextSibling
            End While
            Return Nothing
        End Function

        Private Function ParseInvocationTargetExpression(ByRef decl As vbCodeObject) As CodeMethodInvokeExpression
            'InvocationTargetExpression ::=
            '	  DelegateExpression
            '	| [[ Expression ] "."] vbnet_identifier
            '	| MyClassTag "." IdentifierOrKeyword
            '	| MyBaseTag "." IdentifierOrKeyword
            '	| MethodMemberName ;
            Dim obj As vbCodeObject = decl.FirstChild

            Select Case obj.Type
                Case "DelegateExpression"
                    Return ParseDelegateExpression(obj)
                Case Else
                    Throw New exNotEmplemented(obj.Type)
            End Select

        End Function
        Private Function ParseDelegateExpression(ByRef decl As vbCodeObject) As CodeMethodInvokeExpression
            'DelegateExpression ::= 
            '	QualRefExpression ;
            Dim obj As vbCodeObject = decl.FirstChild
            Select Case obj.Type
                Case "Expression"
                    Return DirectCast(ParseExpression(obj), CodeMethodInvokeExpression)
                Case Else
                    Throw New exNotEmplemented(obj.Type)
            End Select
        End Function


        Private Function ParseExpression(ByRef decl As vbCodeObject) As CodeExpression
            'Expression <SHOWDELIMITERS> ::= 
            '	InclusiveOrExpression;
            Dim obj As vbCodeObject = decl.FirstChild
            Select Case obj.Type
                Case "InclusiveOrExpression"
                    Return ParseInclusiveOrExpression(obj)
                Case Else
                    Throw New exNotEmplemented(obj.Type)
            End Select
        End Function
        Private Function ParseInclusiveOrExpression(ByRef decl As vbCodeObject) As CodeExpression
            'InclusiveOrExpression ::= 
            '	{ExclusiveOrExpression, InclusiveOrOperator}; 

            Dim obj As vbCodeObject = decl.FirstChild
            Select Case obj.Type
                Case "ExclusiveOrExpression"
                    Return ParseExclusiveOrExpression(obj)
                    'Case "InclusiveOrOperator"

                Case Else
                    Throw New exNotEmplemented(obj.Type)
            End Select
        End Function
        Private Function ParseExclusiveOrExpression(ByRef decl As vbCodeObject) As CodeExpression
            'ExclusiveOrExpression::= 
            '	{ConditionalAndExpression, ExclusiveOrOperator} ;
            Dim obj As vbCodeObject = decl.FirstChild
            Select Case obj.Type
                Case "ConditionalAndExpression"
                    Return ParseConditionalAndExpression(obj)
                Case Else
                    Throw New exNotEmplemented(obj.Type)
            End Select
        End Function

        Private Function ParseConditionalAndExpression(ByRef decl As vbCodeObject) As CodeExpression
            'ConditionalAndExpression ::=
            '	{LikeOperatorExpression, ConditionalAndOperator}; 
            Dim obj As vbCodeObject = decl.FirstChild
            Select Case obj.Type
                Case "LikeOperatorExpression"
                    Return ParseLikeOperatorExpression(obj)
                Case Else
                    Throw New exNotEmplemented(obj.Type)
            End Select
        End Function

        Private Function ParseLikeOperatorExpression(ByRef decl As vbCodeObject) As CodeExpression
            'LikeOperatorExpression ::= 
            '	{RelationalExpression, LikeOperator} ;
            Dim obj As vbCodeObject = decl.FirstChild
            Select Case obj.Type
                Case "RelationalExpression"
                    Return ParseRelationalExpression(obj)
                Case Else
                    Throw New exNotEmplemented(obj.Type)
            End Select
        End Function
        Private Function ParseRelationalExpression(ByRef decl As vbCodeObject) As CodeExpression
            'RelationalExpression ::=
            '	{ExponentLevelTerm, RelationalOperator} ;
            Dim obj As vbCodeObject = decl.FirstChild
            Select Case obj.Type
                Case "ExponentLevelTerm"
                    Return ParseExponentLevelTerm(obj)
                Case Else
                    Throw New exNotEmplemented(obj.Type)
            End Select
        End Function


        Private Function ParseConcatenationOperatorExpression(ByRef decl As vbCodeObject) As CodeExpression
            'ConcatenationOperatorExpression ::= 
            '	{PrimaryExpression, ConcatenationOperator} ;
            Dim obj As vbCodeObject = decl.FirstChild
            Select Case obj.Type
                Case "PrimaryExpression"
                    Return ParsePrimaryExpression(obj)
                Case Else
                    Throw New exNotEmplemented(obj.Type)
            End Select
        End Function
        Private Function ParseVariableExpression(ByRef decl As vbCodeObject) As CodeExpression
            'VariableExpression ::= 
            '	PrimaryExpression ;
            Dim obj As vbCodeObject = decl.FirstChild
            While Not obj Is Nothing
                Select Case obj.Type
                    Case "PrimaryExpression"
                        Return ParsePrimaryExpression(obj)
                    Case Else
                        Throw New exNotEmplemented(obj.Type)
                End Select
                obj = obj.NextSibling
            End While
            Return Nothing
        End Function


#Region " Primary Expression "

        Private Function ParsePrimaryExpression(ByRef decl As vbCodeObject) As CodeExpression
            'PrimaryExpression<HIDEDELIMITERS> ::=
            '	( 
            '	  MetaTypeExpression
            '	| QualRefExpression
            '	| ParenthesizedExpression
            '	| TypeOfIsOperatorExpression
            '	| NewExpression
            '	| TypeOfExpression
            '	| UnaryOperatorExpression 
            '	)
            '	[post_expression_operator_list];

            Dim obj As vbCodeObject = decl.FirstChild
            Dim params As CodeExpressionCollection = Nothing

            Dim objParams As vbCodeObject = obj.NextSibling
            If Not objParams Is Nothing AndAlso objParams.Type = "post_expression_operator_list" Then
                params = ParsePost_expression_operator_list(objParams)
            End If


            Select Case obj.Type
                Case "MetaTypeExpression"
                    If Not objParams Is Nothing Then Throw New NotImplementedException("MetaTypeExpression")
                    Return ParseMetaTypeExpression(obj)
                Case "QualifiedIdentifier"
                    Return ParseQualifiedIdentifier(obj, params)
                Case "NewExpression"
                    Return ParseNewExpression(obj)
                Case "LiteralExpression"
                    If Not objParams Is Nothing Then Throw New NotImplementedException("LiteralExpression")
                    Return ParseLiteralExpression(obj)
                Case "CastExpression"
                    If Not objParams Is Nothing Then Throw New NotImplementedException("CastExpression")
                    Return ParseCastExpression(obj)
                Case "UnaryOperatorExpression"
                    If Not objParams Is Nothing Then Throw New NotImplementedException("UnaryOperatorExpression")
                    Return ParseUnaryOperatorExpression(obj)
                Case "ParenthesizedExpression"
                    If Not objParams Is Nothing Then Throw New NotImplementedException("ParenthesizedExpression")
                    Return ParseParenthesizedExpression(obj)
                    'Case "TypeName"
                    '    Dim meth As New CodeMethodReferenceExpression()
                    '    Return meth
                Case Else
                    Throw New exNotEmplemented(obj.Type)
            End Select



        End Function

        Private Function ParseParenthesizedExpression(ByRef decl As vbCodeObject) As CodeExpression
            'ParenthesizedExpression ::= 
            '	"(" Expression ")" ;
            Dim obj As vbCodeObject = decl.FirstChild
            While Not obj Is Nothing
                Select Case obj.Type
                    Case "Expression"
                        Return ParseExpression(obj)
                    Case Else
                        Throw New exNotEmplemented(obj.Type)
                End Select
                obj = obj.NextSibling
            End While
            Return Nothing
        End Function

        Private Function ParseMetaTypeExpression(ByRef decl As vbCodeObject) As CodeTypeOfExpression
            'MetaTypeExpression ::= 
            '	"GetType" "(" TypeSpec ")" ;
            Dim obj As vbCodeObject = decl.FirstChild
            Dim expr As New CodeTypeOfExpression
            While Not obj Is Nothing
                Select Case obj.Type
                    Case "TypeSpec"
                        expr.Type = ParseTypeSpec(obj)
                    Case Else
                        Throw New exNotEmplemented(obj.Type)
                End Select
                obj = obj.NextSibling
            End While
            Return expr
        End Function

        Private Function ParseUnaryOperatorExpression(ByRef decl As vbCodeObject) As CodeBinaryOperatorExpression
            'UnaryOperatorExpression ::=
            '	UnaryPlusExpression |
            '	UnaryMinusExpression |
            '	UnaryLogicalNotExpression ;
            Dim obj As vbCodeObject = decl.FirstChild

            Select Case obj.Type
                Case "UnaryLogicalNotExpression"
                    Return ParseUnaryLogicalNotExpression(obj)
                Case Else
                    Throw New exNotEmplemented(obj.Type)
            End Select
        End Function
        Private Function ParseUnaryLogicalNotExpression(ByRef decl As vbCodeObject) As CodeBinaryOperatorExpression
            'UnaryLogicalNotExpression ::= 
            '	"Not" Expression ;
            Dim obj As vbCodeObject = decl.FirstChild

            Select Case obj.Type
                Case "Expression"
                    Return New CodeBinaryOperatorExpression(Nothing, CodeBinaryOperatorType.IdentityEquality, New CodePrimitiveExpression(False))
                Case Else
                    Throw New exNotEmplemented(obj.Type)
            End Select
        End Function
        Private Function ParseQualifiedIdentifier(ByRef decl As vbCodeObject, ByRef params As CodeExpressionCollection) As CodeExpression
            Dim res As CodeExpression = Nothing
            If (Not params Is Nothing) Then
                res = ParseQualifiedIdentifier1(decl, params)
            Else
                res = ParseCodeFieldReferenceExpression(decl)
            End If
            Return res
        End Function
        Private Function ParseQualifiedIdentifier1(ByRef decl As vbCodeObject, ByRef params As CodeExpressionCollection) As CodeExpression
            'QualifiedIdentifier ::=
            '	["."]
            '	( vbnet_Identifier 
            '	 | "[" IdentifierOrKeyword "]" 
            '	 | MeExpression )
            '	[{"." (IdentifierOrKeyword 
            '	 | "[" IdentifierOrKeyword "]") }] ;
            Dim res As CodeExpression = Nothing
            If params.Count = 2 AndAlso TypeOf params(0) Is CodeSnippetExpression _
              AndAlso CType(params(0), CodeSnippetExpression).Value = "Is" Then
                Dim binop As New CodeBinaryOperatorExpression
                binop.Operator = CodeBinaryOperatorType.ValueEquality
                binop.Right = New CodePrimitiveExpression(Nothing)
                binop.Left = ParseCodeFieldReferenceExpression(decl)
                res = binop
            Else
                res = ParseMethodInvokeExpression(decl, params)
            End If
            Return res


        End Function

        Private Function ParseMethodInvokeExpression(ByRef decl As vbCodeObject, ByRef params As CodeExpressionCollection) As CodeMethodInvokeExpression
            'QualifiedIdentifier ::=
            '	["."]
            '	( vbnet_Identifier 
            '	 | "[" IdentifierOrKeyword "]" 
            '	 | MeExpression )
            '	[{"." (IdentifierOrKeyword 
            '	 | "[" IdentifierOrKeyword "]") }] ;

            Dim obj As vbCodeObject = decl.FirstChild
            If obj Is Nothing Then Return Nothing

            Dim methReference As New CodeMethodReferenceExpression


            If Not obj.NextSibling Is Nothing Then
                Dim expr As CodeExpression = Nothing
                expr = New CodeVariableReferenceExpression(obj.Text)

                While Not obj.NextSibling.NextSibling Is Nothing
                    obj = obj.NextSibling
                    expr = New CodeFieldReferenceExpression(expr, obj.Text)
                End While
                obj = obj.NextSibling

            End If
            methReference.MethodName = obj.Text
            Dim res As New CodeMethodInvokeExpression
            res.Method = methReference
            res.Parameters.AddRange(params)
            Return res
        End Function

        Private Function ParseCodeFieldReferenceExpression(ByRef decl As vbCodeObject) As CodeExpression
            'QualifiedIdentifier ::=
            '	["."]
            '	( vbnet_Identifier 
            '	 | "[" IdentifierOrKeyword "]" 
            '	 | MeExpression )
            '	[{"." (IdentifierOrKeyword 
            '	 | "[" IdentifierOrKeyword "]") }] ;
            Dim res As CodeExpression = Nothing

            Dim obj As vbCodeObject = decl.FirstChild

            If obj.Type = "vbnet_Identifier" Then
                res = New CodeVariableReferenceExpression(obj.Text)
            ElseIf obj.Type = "MeExpression" Then
                res = New CodeThisReferenceExpression()
            Else
                Throw New NotSupportedException(obj.Type)
            End If

            obj = obj.NextSibling
            While Not obj Is Nothing
                Select Case obj.Type
                    Case "IdentifierOrKeyword"
                        Dim subObj As vbCodeObject = obj.FirstChild
                        If subObj.Type = "vbnet_Identifier" Then
                            res = New CodeFieldReferenceExpression(res, obj.Text)
                        Else
                            Throw New NotSupportedException(subObj.Type)
                        End If

                    Case Else
                        Throw New exNotEmplemented(obj.Type)
                End Select
                obj = obj.NextSibling
            End While



            Return res
        End Function

        'Private Sub ParseIdentifierOrKeyword(ByRef decl As vbCodeObject)
        '    'IdentifierOrKeyword ::= 
        '    '            vbnet_Identifier
        '    '	| Keyword ;
        '    Dim obj As vbCodeObject = decl.FirstChild
        'End Sub

        Private Function ParseLiteralExpression(ByRef decl As vbCodeObject) As CodePrimitiveExpression
            'LiteralExpression ::=
            '	Literal ;
            Dim obj As vbCodeObject = decl.FirstChild
            ParseLiteralExpression = New CodePrimitiveExpression
            ParseLiteral(ParseLiteralExpression, obj)

        End Function
        Private Sub ParseLiteral(ByRef primExpr As CodePrimitiveExpression, ByRef decl As vbCodeObject)
            'Literal ::= 
            '	  BooleanLiteral 
            '	| NumericLiteral 
            '	| StringLiteral 
            '	| CharacterLiteral 
            '	| DateLiteral 
            '	| Nothing ;
            Dim obj As vbCodeObject = decl.FirstChild

            Select Case obj.Type
                Case "CharacterLiteral"
                    primExpr.Value = CType(obj.Text, Boolean)
                Case "NumericLiteral"
                    ParseNumericLiteral(primExpr, obj)
                Case "CharacterLiteral"
                    primExpr.Value = obj.Text
                Case "StringLiteral"
                    primExpr.Value = obj.Text.Substring(1, obj.Text.Length - 2)
                Case "Nothing"
                    primExpr.Value = Nothing
                Case Else
                    Throw New exNotEmplemented(obj.Type)
            End Select

        End Sub
        Private Sub ParseNumericLiteral(ByRef primExpr As CodePrimitiveExpression, ByRef decl As vbCodeObject)
            'NumericLiteral ::= 
            '	  FloatingPointLiteral 
            '	| IntegerLiteral ;
            Dim obj As vbCodeObject = decl.FirstChild

            Select Case obj.Type
                Case "IntegerLiteral"
                    primExpr.Value = CType(obj.Text, Integer)
                Case "FloatingPointLiteral"
                    primExpr.Value = CType(obj.Text, Double)
                Case Else
                    Throw New exNotEmplemented(obj.Type)
            End Select

        End Sub

#End Region

        Private Function ParsePost_expression_operator_list(ByRef decl As vbCodeObject) As CodeExpressionCollection
            'post_expression_operator_list ::= 
            '	{post_expression_operator} ;
            Dim obj As vbCodeObject = decl.FirstChild
            Dim expcol As New CodeExpressionCollection

            While Not obj Is Nothing
                Select Case obj.Type
                    Case "post_expression_operator"
                        expcol.AddRange(ParsePost_expression_operator(obj))
                    Case Else
                        Throw New exNotEmplemented(obj.Type)
                End Select
                obj = obj.NextSibling
            End While
            Return expcol
        End Function

        Private Function ParsePost_expression_operator(ByRef decl As vbCodeObject) As CodeExpressionCollection
            'post_expression_operator ::=
            '	(? ^*TypeOfIsOperatorExpression == NULL)
            Dim res As New CodeExpressionCollection
            If decl.ChildCount = 0 Then Return res

            Dim obj As vbCodeObject = decl.FirstChild

            While Not obj Is Nothing
                Select Case obj.Type

                    Case "ArgumentList"
                        res.AddRange(ParseArgumentList(obj))
                    Case "IsOperator"
                        res.Add(New CodeSnippetExpression(obj.Text))
                    Case "Expression"
                        res.Add(ParseExpression(obj))
                    Case "InvocationParams"
                        res.AddRange(ParseInvocationParams(obj))

                        'Case "IdentifierOrKeyword"
                        '    Dim nextOper As vbCodeObject = obj.NextSibling
                        '    If nextOper Is Nothing Then
                        '        expr = New CodeFieldReferenceExpression(expr, subExpr.Text)
                        '    ElseIf nextOper.FirstChild.Type = "ArgumentList" Then
                        '        Dim params As CodeExpressionCollection = ParseArgumentList(nextOper.FirstChild)

                        '        Dim paramsArr(params.Count - 1) As CodeExpression
                        '        params.CopyTo(paramsArr, 0)

                        '        expr = New CodeMethodInvokeExpression(expr, subExpr.Text, paramsArr)
                        '        obj = subExpr
                        '    ElseIf nextOper.FirstChild.Type = "IdentifierOrKeyword" Then
                        '        expr = New CodeFieldReferenceExpression(expr, subExpr.Text)
                        '    Else
                        '        Throw New exNotEmplemented(nextOper.FirstChild.Type)
                        '    End If

                        'End Select


                        'Case "IdentifierOrKeyword"
                        '    expr = New CodeFieldReferenceExpression(expr, obj.Text)
                    Case Else
                        Throw New exNotEmplemented(obj.Type)
                End Select
                obj = obj.NextSibling
            End While
            Return res
        End Function
        Private Function ParseInvocationParams(ByRef decl As vbCodeObject) As CodeExpressionCollection
            'InvocationParams ::=
            '	"(" [ ArgumentList ] ")"
            '	| ^("," | "-")ArgumentList ;

            Dim res As New CodeExpressionCollection
            Dim obj As vbCodeObject = decl.FirstChild

            While Not obj Is Nothing
                Select Case obj.Type
                    Case "ArgumentList"
                        res.AddRange(ParseArgumentList(obj))
                    Case Else
                        Throw New exNotEmplemented(obj.Type)
                End Select
                obj = obj.NextSibling
            End While
            Return res
        End Function


        Private Function ParseArgumentList(ByRef decl As vbCodeObject) As CodeExpressionCollection
            'ArgumentList ::=
            '	  PositionalArgumentList ["," NamedArgumentList] 
            Dim obj As vbCodeObject = decl.FirstChild
            Dim pars As New CodeExpressionCollection
            While Not obj Is Nothing
                Select Case obj.Type
                    Case "NamedArgumentList"
                        pars.AddRange(ParseNamedArgumentList(obj))
                    Case "PositionalArgumentList"
                        pars.AddRange(ParsePositionalArgumentList(obj))
                    Case Else
                        Throw New exNotEmplemented(obj.Type)
                End Select
                obj = obj.NextSibling
            End While
            Return pars
        End Function
        Private Function ParseNamedArgumentList(ByRef decl As vbCodeObject) As CodeExpressionCollection
            'NamedArgumentList ::=
            '	{Identifier ":" "=" ArgumentExpression, ","} ;
            Dim obj As vbCodeObject = decl.FirstChild
            Dim pars As New CodeExpressionCollection


            While Not obj Is Nothing
                Select Case obj.Type
                    Case "Identifier"

                    Case "ArgumentExpression"
                        pars.Add(ParseArgumentExpression(obj))
                    Case Else
                        Throw New exNotEmplemented(obj.Type)
                End Select
                obj = obj.NextSibling
            End While
            Return pars
        End Function

        Private Function ParsePositionalArgumentList(ByRef decl As vbCodeObject) As CodeExpressionCollection
            'PositionalArgumentList ::=
            '	{[ArgumentExpression], ","} ;
            Dim obj As vbCodeObject = decl.FirstChild
            Dim pars As New CodeExpressionCollection
            While Not obj Is Nothing
                Select Case obj.Type
                    Case "ArgumentExpression"
                        pars.Add(ParseArgumentExpression(obj))
                    Case Else
                        Throw New exNotEmplemented(obj.Type)
                End Select
                obj = obj.NextSibling
            End While
            Return pars
        End Function

        Private Function ParseArgumentExpression(ByRef decl As vbCodeObject) As CodeExpression
            'ArgumentExpression ::= 
            '	  Expression
            '	| DelegateArgumentExpression ;
            Dim obj As vbCodeObject = decl.FirstChild


            While Not obj Is Nothing
                Select Case obj.Type
                    Case "Expression"
                        Return ParseExpression(obj)
                    Case Else
                        Throw New exNotEmplemented(obj.Type)
                End Select
                obj = obj.NextSibling
            End While
            Return Nothing
        End Function


        Private Function ParseVbnet_Identifier(ByRef decl As vbCodeObject) As CodeVariableReferenceExpression
            ParseVbnet_Identifier = New CodeVariableReferenceExpression(decl.Text)
        End Function

        Private Function ParseCastExpression(ByRef decl As vbCodeObject) As CodeCastExpression
            'CastExpression<SPACE=basic_space_symbol> ::=
            '	"CType" "(" Expression "," TypeSpec ")" |
            '	CastTarget "(" Expression ")" ;
            Dim expr As New CodeCastExpression
            Dim obj As vbCodeObject = decl.FirstChild
            While Not obj Is Nothing
                Select Case obj.Type
                    Case "Expression"
                        expr.Expression = ParseExpression(obj)
                    Case "TypeSpec"
                        expr.TargetType = ParseTypeSpec(obj)
                    Case Else
                        Throw New exNotEmplemented(obj.Type)
                End Select
                obj = obj.NextSibling
            End While
            Return expr
        End Function
        Private Function ParseNewExpression(ByRef decl As vbCodeObject) As CodeExpression
            'NewExpression ::=
            '	  ArrayCreationExpression 
            '	| ObjectCreationExpression 
            '	| DelegateCreationExpression ;
            Dim obj As vbCodeObject = decl.FirstChild
            Select Case obj.Type
                Case "ObjectCreationExpression"
                    Return ParseObjectCreationExpression(obj)
                Case "DelegateCreationExpression"
                    Return ParseDelegateCreationExpression(obj)
                Case Else
                    Throw New exNotEmplemented(obj.Type)
            End Select
        End Function

        Private Function ParseObjectCreationExpression(ByRef decl As vbCodeObject) As CodeObjectCreateExpression
            'ObjectCreationExpression ::= 
            '	NewTag TypeSpec [Arguments] ;

            Dim expr As New CodeObjectCreateExpression

            Dim obj As vbCodeObject = decl.FirstChild
            While Not obj Is Nothing
                Select Case obj.Type
                    Case "NewTag"
                    Case "TypeSpec"
                        expr.CreateType = ParseTypeSpec(obj)
                    Case "ArgumentList"
                        expr.Parameters.AddRange(ParseArgumentList(obj))
                    Case Else
                        Throw New exNotEmplemented(obj.Type)
                End Select
                obj = obj.NextSibling
            End While
            Return expr
        End Function

        Private Function ParseDelegateCreationExpression(ByRef decl As vbCodeObject) As CodeExpression
            ' Return New CodeVariableReferenceExpression(decl.Text)
            Dim obj As vbCodeObject = decl.FirstChild
            Select Case obj.Type
                Case Else
                    Throw New exNotEmplemented(obj.Type)
            End Select
        End Function
#End Region

#Region " Terms "

        Private Function ParseExponentLevelTerm(ByRef decl As vbCodeObject) As CodeExpression
            'ExponentLevelTerm ::= 
            '	{MultLevelTerm, ExponentOperator} ;
            Dim obj As vbCodeObject = decl.FirstChild
            Select Case obj.Type
                Case "MultLevelTerm"
                    Return ParseMultLevelTerm(obj)
                Case Else
                    Throw New exNotEmplemented(obj.Type)
            End Select
        End Function

        Private Function ParseMultLevelTerm(ByRef decl As vbCodeObject) As CodeExpression
            'MultLevelTerm ::=
            '	{AddLevelTerm, MultLevelOperator} ;
            Dim obj As vbCodeObject = decl.FirstChild
            Select Case obj.Type
                Case "AddLevelTerm"
                    Return ParseAddLevelTerm(obj)
                Case Else
                    Throw New exNotEmplemented(obj.Type)
            End Select
        End Function

        Private Function ParseAddLevelTerm(ByRef decl As vbCodeObject) As CodeExpression
            'AddLevelTerm ::= 
            '	{ConcatenationOperatorExpression, AddLevelOperator} ;
            Dim obj As vbCodeObject = decl.FirstChild
            Select Case obj.Type
                Case "ConcatenationOperatorExpression"
                    Return ParseConcatenationOperatorExpression(obj)
                Case Else
                    Throw New exNotEmplemented(obj.Type)
            End Select
        End Function

#End Region

#Region " Others "


        Private Function ParseTypeSpec(ByRef decltype As vbCodeObject) As CodeTypeReference
            'TypeSpec<TERMINAL> ::=
            '	TypeName [{ArrayTypeModifier}] ;
            Dim obj As vbCodeObject = decltype.FirstChild
            Dim typeref As CodeTypeReference
            While Not obj Is Nothing
                Select Case obj.Type
                    Case "TypeName"
                        'Select Case obj.Text
                        '    Case "String"
                        '        typeref = New CodeTypeReference(System.Type.GetType(obj.Text))
                        '    Case Else
                        typeref = New CodeTypeReference(obj.Text)
                        'End Select


                    Case Else
                        Throw New exNotEmplemented(obj.Type)
                End Select
                obj = obj.NextSibling
            End While
            Return New CodeTypeReference(decltype.Text)
        End Function

        Private Sub ParseTypeDeclaration(ByRef ns As CodeNamespace, ByRef decltype As vbCodeObject)
            'TypeDeclaration ::=
            '     ModuleDeclaration
            '	| NonModuleDeclaration ;

            Select Case decltype.FirstChild.Type
                Case tagNonModuleDeclaration
                    ParseNonModuleDeclaration(ns, decltype.FirstChild)
                Case Else
                    Throw New exNotEmplemented(decltype.FirstChild.Type)
            End Select
        End Sub
        Private Sub ParseTypeImplementsClause(ByRef cl As CodeTypeDeclaration, ByRef impl As vbCodeObject)
            'TypeImplementsClause ::= 
            '	[":"] "Implements" Implements LineTerminator ;
            Dim obj As vbCodeObject = impl.FirstChild

            While Not obj Is Nothing
                Select Case obj.Type
                    Case "Implements"
                        ParseImplements(cl, obj)
                    Case Else
                        Throw New exNotEmplemented(obj.Type)
                End Select
                obj = obj.NextSibling
            End While
        End Sub

        Private Sub ParseNonModuleDeclaration(ByRef ns As CodeNamespace, ByRef decl As vbCodeObject)
            'NonModuleDeclaration ::=
            '	  EnumDeclaration 
            '	| StructDeclaration 
            '	| InterfaceDeclaration 
            '	| ClassDeclaration 
            '	| DelegateTypeDeclaration ;


            Select Case decl.FirstChild.Type
                Case tagInterfaceDeclaration
                    ParseInterfaceDeclaration(ns, decl.FirstChild)
                Case tagClassDeclaration
                    ns.Types.Add(ParseClassDeclaration(decl.FirstChild))

                Case Else
                    Throw New exNotEmplemented(decl.FirstChild.Type)
            End Select
        End Sub


        Private Function ParseAccessModifier(ByRef modif As vbCodeObject) As MemberAttributes
            'AccessModifier ::= 
            '	  "Public" 
            '	| "Protected" 
            '	| "Friend" 
            '	| "Private" ;

            Select Case modif.Text.ToLower
                Case "public"
                    ParseAccessModifier = MemberAttributes.Public
                Case "private"
                    ParseAccessModifier = MemberAttributes.Private
                Case "protected"
                    ParseAccessModifier = MemberAttributes.Family
                Case "friend"
                    ParseAccessModifier = MemberAttributes.Assembly
                Case Else
                    Throw New exNotEmplemented(modif.Text)
            End Select
        End Function

#End Region

    End Class


End Namespace

