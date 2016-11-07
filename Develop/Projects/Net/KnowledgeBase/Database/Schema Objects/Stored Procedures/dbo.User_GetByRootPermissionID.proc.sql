-- =============================================
	-- Author:		Savchin Inc. 
	-- Create date: 19.10.2008 16:28:47
	-- Description:	Generated CodeRocket. Select by ForeygnKey FK_Users_Permissions
	-- =============================================

	CREATE PROCEDURE User_GetByRootPermissionID( @RootPermissionID [smallint])
	AS
	BEGIN
		SELECT [CreationDate],[Email],[FirstName],[IsSystem],[IsUserAdmin],[LastName],[Login],[Password],[RootPermissionID],[SecurityAnswer],[SecurityQuestion],[UserID] 
		FROM [Users]
		WHERE  [RootPermissionID]=@RootPermissionID 
	END


