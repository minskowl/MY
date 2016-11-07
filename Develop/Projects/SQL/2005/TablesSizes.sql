CREATE TABLE #tableSize
(
	name nvarchar(128),-- ��� �������, ��� �������� ���� ��������� �������� �� ������������ ������������.
	rows char(11), -- ���������� ������������ ����� � �������. ���� ������ ������ ��� ������� ���������� Service Broker, ���� ������� ��������� ����� ��������� � �������.
	reserved varchar(18),-- ����� ����� ������������������ ������������ ��� ������� objname.
	data varchar(18),-- ����� ����� ������������, ������������ ������� ������� objname.
	index_size varchar(18),-- ����� ����� ������������, ������������ ��������� ������� objname.
	unused varchar(18) 
)

DECLARE @tabName NVARCHAR(256)

DECLARE tables CURSOR  FAST_FORWARD READ_ONLY FOR SELECT [name] FROM Sysobjects WHERE xtype='U'

OPEN tables

FETCH NEXT FROM tables INTO @tabName

WHILE @@FETCH_STATUS = 0
BEGIN
	INSERT INTO #tableSize EXEC sp_spaceused @objname=@tabName
	FETCH NEXT FROM tables INTO @tabName
END

CLOSE tables
DEALLOCATE tables

SELECT *, LEN(reserved) FROM #tableSize
ORDER BY LEN(reserved)

DROP TABLE #tableSize