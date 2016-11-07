ALTER TABLE [dbo].[Knowledges] ADD CONSTRAINT [DF_Knowledges_CreationDate] DEFAULT (getutcdate()) FOR [CreationDate]


