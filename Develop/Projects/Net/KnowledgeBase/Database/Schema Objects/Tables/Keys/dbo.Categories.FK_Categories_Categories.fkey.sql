ALTER TABLE [dbo].[Categories] ADD
CONSTRAINT [FK_Categories_Categories] FOREIGN KEY ([ParentCategoryID]) REFERENCES [dbo].[Categories] ([CategoryID])


