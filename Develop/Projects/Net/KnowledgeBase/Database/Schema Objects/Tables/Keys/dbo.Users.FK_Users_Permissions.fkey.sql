ALTER TABLE [dbo].[Users] ADD
CONSTRAINT [FK_Users_Permissions] FOREIGN KEY ([RootPermissionID]) REFERENCES [dbo].[Permissions] ([PermissionID])


