CREATE PROCEDURE _Cagegory_GetShortInfoByParentCategoryID	(	@ParentCategoryID int 	)
AS
SELECT *
FROM CategoriesInfo 
WHERE ISNULL(@ParentCategoryID,0)=ISNULL(ParentCategoryID,0)


