/*
Delete Application from SqlMembershipProvider DataBase
*/
DECLARE @appid UNIQUEIDENTIFIER
SET @appid='11A0BBD1-1458-4564-B32C-7500A917430A'
DELETE [aspnet_UsersInRoles]  FROM [aspnet_UsersInRoles] 
INNER JOIN [aspnet_Users] ON [aspnet_UsersInRoles].[UserId] = [aspnet_Users].[UserId]
WHERE [ApplicationId]=@appid
DELETE FROM [aspnet_Users] WHERE [ApplicationId]=@appid
DELETE FROM [aspnet_Roles] WHERE [ApplicationId]=@appid
DELETE FROM [aspnet_Applications] WHERE [ApplicationId]=@appid