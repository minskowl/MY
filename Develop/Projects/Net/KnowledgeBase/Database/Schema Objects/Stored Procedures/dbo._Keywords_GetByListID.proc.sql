CREATE PROCEDURE _Keywords_GetByListID
AS
DECLARE @tempExists INT;
EXEC _ExistsTempTable '#Int32',@tempExists OUTPUT;

IF @tempExists=0
BEGIN
	RETURN;
END
	
SELECT * FROM [Keywords] INNER JOIN  #Int32 ON #Int32.Value=[Keywords].[KeywordID]
DROP TABLE #Int32


