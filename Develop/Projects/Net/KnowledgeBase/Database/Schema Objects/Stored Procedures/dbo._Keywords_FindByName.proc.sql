CREATE PROCEDURE _Keywords_FindByName	(	@Name NVARCHAR(200)	)
AS
SELECT * 
FROM [Keywords] 
WHERE [Name] LIKE @Name
ORDER BY [Name]


