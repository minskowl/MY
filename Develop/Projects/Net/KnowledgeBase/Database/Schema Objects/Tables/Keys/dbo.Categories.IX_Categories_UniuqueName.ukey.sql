ALTER TABLE [dbo].[Categories] ADD CONSTRAINT [IX_Categories_UniuqueName] UNIQUE NONCLUSTERED  ([ParentCategoryID], [Name]) ON [PRIMARY]


