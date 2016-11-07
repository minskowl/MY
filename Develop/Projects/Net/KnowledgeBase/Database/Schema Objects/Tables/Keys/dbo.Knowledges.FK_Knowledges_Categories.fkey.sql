ALTER TABLE [dbo].[Knowledges] ADD
CONSTRAINT [FK_Knowledges_Categories] FOREIGN KEY ([CategoryID]) REFERENCES [dbo].[Categories] ([CategoryID])


