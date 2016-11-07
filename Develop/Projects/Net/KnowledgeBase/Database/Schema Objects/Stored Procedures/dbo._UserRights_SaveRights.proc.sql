CREATE PROCEDURE _UserRights_SaveRights	(	@UserID int	)

AS
DECLARE @tempExists INT;

EXEC _ExistsTempTable '#CategoryPermission',@tempExists OUTPUT;

IF @tempExists=0
BEGIN
 PRINT '#CategoryPermission not exists'
 DELETE [UserRights] 
 FROM [UserRights] 
 WHERE [UserID]=@UserID 
 RETURN;
END
PRINT '#CategoryPermission exists'
BEGIN TRAN

DELETE [UserRights] 
FROM [UserRights] 
WHERE [UserID]=@UserID AND [CategoryID] NOT IN (SELECT CategoryID FROM #CategoryPermission )
IF @@ERROR<>0
BEGIN
   ROLLBACK TRAN
   DROP TABLE #CategoryPermission
   RETURN;
END
UPDATE [UserRights]
SET PermissionID=#CategoryPermission.PermissionID
FROM [UserRights] 
INNER JOIN #CategoryPermission ON [UserRights].[CategoryID]=#CategoryPermission.[CategoryID]
WHERE [UserRights].[UserID]=@UserID 
IF @@ERROR<>0
BEGIN
   ROLLBACK TRAN
   DROP TABLE #CategoryPermission
   RETURN;
END
INSERT INTO [UserRights] (UserID,[CategoryID],PermissionID)
SELECT @UserID, [CategoryID],PermissionID
FROM #CategoryPermission 
WHERE [CategoryID] NOT IN( SELECT [CategoryID] FROM [UserRights] WHERE [UserID]=@UserID)

IF @@ERROR<>0
BEGIN
   ROLLBACK TRAN
   DROP TABLE #CategoryPermission
   RETURN;
END

COMMIT TRAN 

DROP TABLE #CategoryPermission


