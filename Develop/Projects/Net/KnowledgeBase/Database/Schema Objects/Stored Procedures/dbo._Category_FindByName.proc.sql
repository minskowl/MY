CREATE PROCEDURE _Category_FindByName	(	@Name NVARCHAR(200)	)
AS
SELECT * FROM [Categories] WHERE [Name] LIKE @Name


