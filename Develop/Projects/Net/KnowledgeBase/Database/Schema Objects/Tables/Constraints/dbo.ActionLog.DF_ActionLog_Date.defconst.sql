ALTER TABLE [dbo].[ActionLog] ADD CONSTRAINT [DF_ActionLog_Date] DEFAULT (getdate()) FOR [Date]


