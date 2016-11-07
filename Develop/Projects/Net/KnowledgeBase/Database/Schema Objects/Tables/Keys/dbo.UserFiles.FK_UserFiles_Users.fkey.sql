ALTER TABLE [dbo].[UserFiles] ADD
CONSTRAINT [FK_UserFiles_Users] FOREIGN KEY ([UserID]) REFERENCES [dbo].[Users] ([UserID]) ON DELETE CASCADE


