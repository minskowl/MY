ALTER TABLE [dbo].[Knowledges] ADD
CONSTRAINT [FK_Knowledges_Creator] FOREIGN KEY ([CreatorID]) REFERENCES [dbo].[Users] ([UserID]) ON DELETE SET DEFAULT


