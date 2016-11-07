﻿-- =============================================
	-- Author:		Savchin Inc. 
	-- Create date: 19.10.2008 16:28:47
	-- Description:	Generated CodeRocket
	-- =============================================
	CREATE PROCEDURE User_GetByID(@UserID [int])
	AS
	BEGIN
		SELECT [CreationDate],[Email],[FirstName],[IsSystem],[IsUserAdmin],[LastName],[Login],[Password],[RootPermissionID],[SecurityAnswer],[SecurityQuestion],[UserID] 
		FROM [Users]
		WHERE [UserID]=@UserID  
	END


