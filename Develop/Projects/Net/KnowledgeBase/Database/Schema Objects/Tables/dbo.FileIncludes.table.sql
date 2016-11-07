CREATE TABLE [dbo].[FileIncludes]
(
[FileIncludeID] [uniqueidentifier] NOT NULL,
[KnowledgeID] [int] NOT NULL,
[FileName] [nvarchar] (100) COLLATE Cyrillic_General_CI_AS NOT NULL,
[Data] [image] NULL,
[Size] [int] NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]


GO
EXEC sp_addextendedproperty N'MS_Description', N'File Content', 'SCHEMA', N'dbo', 'TABLE', N'FileIncludes', 'COLUMN', N'Data'

