CREATE TABLE [dbo].[Categories]
(
[CategoryID] [int] NOT NULL IDENTITY(1, 1),
[ParentCategoryID] [int] NULL,
[Name] [nvarchar] (50) COLLATE Cyrillic_General_CI_AS NOT NULL,
[CreationDate] [datetime] NOT NULL
) ON [PRIMARY]


GO
EXEC sp_addextendedproperty N'MS_Description', N'Date in UTC', 'SCHEMA', N'dbo', 'TABLE', N'Categories', 'COLUMN', N'CreationDate'

