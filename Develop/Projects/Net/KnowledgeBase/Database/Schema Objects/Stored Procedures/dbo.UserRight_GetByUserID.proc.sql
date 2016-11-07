-- =============================================
	-- Author:		Savchin Inc. 
	-- Create date: 13.10.2008 21:24:44
	-- Description:	Generated CodeRocket. Select by ForeygnKey FK_UserRights_Users
	-- =============================================

	CREATE PROCEDURE UserRight_GetByUserID( @UserID [int])
	AS
	BEGIN
		SELECT [CategoryID],[PermissionID],[UserID] 
		FROM [UserRights]
		WHERE  [UserID]=@UserID 
	END


