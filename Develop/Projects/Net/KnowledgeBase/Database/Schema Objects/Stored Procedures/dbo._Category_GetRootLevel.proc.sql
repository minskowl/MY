CREATE PROCEDURE _Category_GetRootLevel

AS
SELECT * FROM Categories WHERE ParentCategoryID IS NULL


