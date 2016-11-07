ALTER TABLE [dbo].[Knowledges] ADD
CONSTRAINT [FK_Knowledges_Modificator] FOREIGN KEY ([ModificatorID]) REFERENCES [dbo].[Users] ([UserID])


