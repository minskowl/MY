ALTER TABLE [dbo].[Keywords] ADD CONSTRAINT [DF_Keywords_CreationDate] DEFAULT (getutcdate()) FOR [CreationDate]


