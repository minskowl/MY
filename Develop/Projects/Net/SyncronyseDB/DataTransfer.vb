Imports System.Xml.Serialization
Public Class DataTransfer


    Private Inserted_Seq As New ArrayList
    Private Deleted_Seq As New ArrayList
    Private Tables As New ArrayList
    Private oDB_Destination As SQLDMO.Database2
    Private oDB_Source As SQLDMO.Database2

    <XmlArrayItem(ElementName:="Table", IsNullable:=True, Type:=GetType(String), Namespace:="http://www.semitech.by"), _
     XmlArray(ElementName:="Tables", Namespace:="http://www.semitech.by", IsNullable:=True)> _
    Public Property arTables() As ArrayList
        Get
            Return Tables
        End Get
        Set(ByVal tabs As ArrayList)
            Tables.Clear()
            Tables.AddRange(tabs)
        End Set
    End Property

    <XmlArrayItem(ElementName:="Table", IsNullable:=True, Type:=GetType(String), Namespace:="http://www.semitech.by"), _
     XmlArray(ElementName:="Inserted_Seq", Namespace:="http://www.semitech.by", IsNullable:=True)> _
    Public Property arInserted_Seq() As ArrayList
        Get
            Return Inserted_Seq
        End Get
        Set(ByVal tabs As ArrayList)
            Inserted_Seq.Clear()
            Inserted_Seq.AddRange(tabs)
        End Set
    End Property

    <XmlArrayItem(ElementName:="Table", IsNullable:=True, Type:=GetType(String), Namespace:="http://www.semitech.by"), _
     XmlArray(ElementName:="Deleted_Seq", Namespace:="http://www.semitech.by", IsNullable:=True)> _
    Public Property arDeleted_Seq() As ArrayList
        Get
            Return Deleted_Seq
        End Get
        Set(ByVal tabs As ArrayList)
            Deleted_Seq.Clear()
            Deleted_Seq.AddRange(tabs)
        End Set
    End Property
    Public Event Status(ByVal m As String)

    'Function getTables() As IEnumerator
    '    getTables = Tables.GetEnumerator
    'End Function

    'Sub setTables(ByRef tab As ArrayList)
    '    Tables.Clear()
    '    Tables.AddRange(tab)

    '    If Inserted_Seq.Count Then
    '        Inserted_Seq.Clear()
    '    End If
    '    If Deleted_Seq.Count Then
    '        Deleted_Seq.Clear()
    '    End If

    'End Sub

    Sub setDatabase(ByRef dbs As SQLDMO.Database2, ByRef dbd As SQLDMO.Database2)
        oDB_Destination = dbs
        oDB_Destination = dbd
    End Sub
    Private Function Ch_Tab_Ins(ByVal TabName As String, ByVal Level As Integer) As Boolean
        Dim par As SQLDMO.QueryResults2
        Dim i As Integer
        Ch_Tab_Ins = True
        Level += 1

        Application.DoEvents()

        If Me.Inserted_Seq.Contains(TabName) = True Then 'Alredy filled
            Exit Function
        End If
        par = oDB_Destination.Tables.Item(TabName).EnumDependencies(SQLDMO.SQLDMO_DEPENDENCY_TYPE.SQLDMODep_Parents)

        If par.Rows = 0 Then 'no parents
            Inserted_Seq.Add(TabName)
            Exit Function
        End If
        For i = 1 To par.Rows 'check parents
            If par.GetColumnBigInt(i, 1) = 8 And par.GetColumnBigInt(i, 4) = 1 Then 'Table
                If Inserted_Seq.Contains(par.GetColumnString(i, 2)) = False Then 'paren don't fill
                    If Ch_Tab_Ins(par.GetColumnString(i, 2), Level) = False Then ' Fill parent
                        MsgBox("Fuck") ' Not Filled
                        Ch_Tab_Ins = False
                        Exit Function
                    End If
                End If

            End If
        Next

        Inserted_Seq.Add(TabName)
    End Function

    Private Function Ch_Tab_Del(ByVal TabName As String, ByVal Level As Integer) As Boolean
        Dim par As SQLDMO.QueryResults2
        Dim i As Integer
        Ch_Tab_Del = True
        Level += 1

        Application.DoEvents()

        If Me.Deleted_Seq.Contains(TabName) = True Then 'Alredy filled
            Exit Function
        End If
        par = oDB_Destination.Tables.Item(TabName).EnumDependencies(SQLDMO.SQLDMO_DEPENDENCY_TYPE.SQLDMODep_Children)

        If par.Rows = 0 Then 'no parents
            Deleted_Seq.Add(TabName)
            Exit Function
        End If
        For i = 1 To par.Rows 'check parents
            If par.GetColumnBigInt(i, 1) = 8 And par.GetColumnBigInt(i, 4) = 1 Then 'Table
                If Deleted_Seq.Contains(par.GetColumnString(i, 2)) = False Then 'paren don't fill
                    If Ch_Tab_Del(par.GetColumnString(i, 2), Level) = False Then ' Fill parent
                        MsgBox("Fuck") ' Not Filled
                        Ch_Tab_Del = False
                        Exit Function
                    End If
                End If

            End If
        Next

        Deleted_Seq.Add(TabName)
    End Function

    Sub Init()
        Dim oTable As SQLDMO.Table2
        For Each oTable In oDB_Destination.Tables
            If oTable.SystemObject = False Then
                Tables.Add(oTable.Name)
            End If
        Next
    End Sub

    Sub Analyze()

        Dim tabName As String
        RaiseEvent Status("Analize start ")

        'i = 20
        Deleted_Seq.Clear()
        Inserted_Seq.Clear()

        For Each tabName In Tables
            RaiseEvent Status("table " & tabName)

            Application.DoEvents()

            Ch_Tab_Ins(tabName, 0)
            Ch_Tab_Del(tabName, 0)


        Next
        RaiseEvent Status("Analize end ")

    End Sub

    Sub Transf(ByVal clear_table As Boolean)
        Try
            Dim tabName As String
            Dim Transfer As New SQLDMO.Transfer2

            RaiseEvent Status("Transfer start")

            If clear_table = True Then
                For Each tabName In Me.Deleted_Seq
                    RaiseEvent Status("Clear table: " & tabName)
                    oDB_Destination.ExecuteImmediate("DELETE FROM " & tabName)
                Next
            End If


            Transfer.CopyData = SQLDMO.SQLDMO_COPYDATA_TYPE.SQLDMOCopyData_Append
            Transfer.CopySchema = False

            '  Transfer.DestServer = oSQLServer_Dest.Name
            ' Transfer.DestLogin = oSQLServer_Dest.Login
            'Transfer.DestPassword = oSQLServer_Dest.Password
            'Transfer.DestDatabase = oDB_Destination.Name




            'Me.trv_Tables.Nodes.
            For Each tabName In Me.Inserted_Seq

                'pb_Transfer.Value = 0 
                RaiseEvent Status("Transfer table: " & tabName)
                Application.DoEvents()
                Transfer.AddObjectByName(tabName, SQLDMO.SQLDMO_OBJECT_TYPE.SQLDMOObj_UserTable)
                oDB_Source.Transfer(Transfer)
                Transfer.RemoveAllObjects()

            Next
            RaiseEvent Status("Transfer end")

            Exit Sub
        Catch ex As Exception
            RaiseEvent Status("Transfer Error: " & ex.Source & vbCrLf & ex.Message & ex.HelpLink)
        End Try
    End Sub
End Class
