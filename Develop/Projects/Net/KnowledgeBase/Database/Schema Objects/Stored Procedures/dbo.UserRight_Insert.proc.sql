-- =============================================
-- Author:		Savchin Inc. 
-- Create date: 13.10.2008 21:24:44
-- Description:	Generated CodeRocket
-- =============================================
CREATE PROCEDURE UserRight_Insert(@CategoryID [int],@PermissionID [smallint],@UserID [int])
AS
BEGIN
	 INSERT [UserRights]
           ([CategoryID],[PermissionID],[UserID])
     VALUES
           (@CategoryID,@PermissionID,@UserID)
END


