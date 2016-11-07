-- =============================================
-- Author:		Savchin Inc. 
-- Create date: 13.10.2008 21:24:44
-- Description:	Generated CodeRocket
-- =============================================
CREATE PROCEDURE UserRight_GetAll
AS
BEGIN
	SELECT [CategoryID],[PermissionID],[UserID] 
	FROM [UserRights]
END


