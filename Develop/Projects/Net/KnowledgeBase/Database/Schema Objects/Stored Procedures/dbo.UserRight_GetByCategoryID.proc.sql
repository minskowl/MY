-- =============================================
	-- Author:		Savchin Inc. 
	-- Create date: 13.10.2008 21:24:44
	-- Description:	Generated CodeRocket. Select by ForeygnKey FK_UserRights_Categories
	-- =============================================

	CREATE PROCEDURE UserRight_GetByCategoryID( @CategoryID [int])
	AS
	BEGIN
		SELECT [CategoryID],[PermissionID],[UserID] 
		FROM [UserRights]
		WHERE  [CategoryID]=@CategoryID 
	END


