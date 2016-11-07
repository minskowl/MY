ALTER TABLE [dbo].[Categories] ADD CONSTRAINT [DF_Categories_CreationDate] DEFAULT (getutcdate()) FOR [CreationDate]


