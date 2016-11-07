CREATE TABLE #tableSize
(
	name nvarchar(128),-- Имя объекта, для которого были запрошены сведения об используемом пространстве.
	rows char(11), -- Количество существующих строк в таблице. Если объект указан как очередь компонента Service Broker, этот столбец указывает число сообщений в очереди.
	reserved varchar(18),-- Общий объем зарезервированного пространства для объекта objname.
	data varchar(18),-- Общий объем пространства, используемый данными объекта objname.
	index_size varchar(18),-- Общий объем пространства, используемый индексами объекта objname.
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