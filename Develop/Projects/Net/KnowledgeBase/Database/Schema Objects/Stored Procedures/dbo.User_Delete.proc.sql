-- =============================================
	-- Author:		Savchin Inc. 
	-- Create date: 19.10.2008 16:28:47
	-- Description:	Generated CodeRocket
	-- =============================================
	CREATE PROCEDURE User_Delete(@UserID [int])
	AS
	BEGIN
		DELETE FROM [Users]
		WHERE [UserID]=@UserID        
	END


