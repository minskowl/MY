CREATE PROCEDURE _Keywords_GetByName	(	@Name NVARCHAR(200)	)
AS
SELECT * 
FROM [Keywords] 
WHERE [Name] = @Name


