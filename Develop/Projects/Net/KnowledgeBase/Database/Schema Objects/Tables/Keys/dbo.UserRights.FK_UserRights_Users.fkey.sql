ALTER TABLE [dbo].[UserRights] ADD
CONSTRAINT [FK_UserRights_Users] FOREIGN KEY ([UserID]) REFERENCES [dbo].[Users] ([UserID]) ON DELETE CASCADE


