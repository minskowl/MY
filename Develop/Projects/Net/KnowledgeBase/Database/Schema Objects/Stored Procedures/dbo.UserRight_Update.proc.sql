-- =============================================
-- Author:		Savchin Inc. 
-- Create date: 13.10.2008 21:24:44
-- Description:	Generated CodeRocket
-- =============================================
CREATE PROCEDURE UserRight_Update(@CategoryID [int],@PermissionID [smallint],@UserID [int])
AS
BEGIN
	UPDATE [UserRights]
	SET 
		[CategoryID]=@CategoryID,[PermissionID]=@PermissionID,[UserID]=@UserID
	WHERE [CategoryID]=@CategoryID AND [UserID]=@UserID        
END


