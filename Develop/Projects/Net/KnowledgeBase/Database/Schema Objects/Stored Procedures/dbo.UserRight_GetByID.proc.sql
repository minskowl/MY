-- =============================================
-- Author:		Savchin Inc. 
-- Create date: 13.10.2008 21:24:44
-- Description:	Generated CodeRocket
-- =============================================
CREATE PROCEDURE UserRight_GetByID(@CategoryID [int],@UserID [int])
AS
BEGIN
	SELECT [CategoryID],[PermissionID],[UserID] 
	FROM [UserRights]
	WHERE [CategoryID]=@CategoryID AND [UserID]=@UserID  
END


