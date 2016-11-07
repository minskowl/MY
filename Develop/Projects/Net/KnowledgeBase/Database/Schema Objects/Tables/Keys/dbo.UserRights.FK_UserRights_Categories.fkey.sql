ALTER TABLE [dbo].[UserRights] ADD
CONSTRAINT [FK_UserRights_Categories] FOREIGN KEY ([CategoryID]) REFERENCES [dbo].[Categories] ([CategoryID]) ON DELETE CASCADE


