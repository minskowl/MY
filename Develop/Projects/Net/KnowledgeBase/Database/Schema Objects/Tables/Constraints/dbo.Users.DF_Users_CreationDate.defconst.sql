ALTER TABLE [dbo].[Users] ADD CONSTRAINT [DF_Users_CreationDate] DEFAULT (getdate()) FOR [CreationDate]


