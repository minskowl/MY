CREATE TABLE [dbo].[Knowledges]
(
[KnowledgeID] [int] NOT NULL IDENTITY(1, 1),
[CategoryID] [int] NOT NULL,
[Title] [nvarchar] (150) COLLATE Latin1_General_CI_AS NOT NULL,
[Summary] [ntext] COLLATE Cyrillic_General_CI_AS NULL,
[KnowledgeTypeID] [smallint] NOT NULL,
[CreationDate] [datetime] NOT NULL,
[CreatorID] [int] NOT NULL,
[ModificationDate] [datetime] NULL,
[ModificatorID] [int] NULL,
[PublicID] [uniqueidentifier] NOT NULL,
[KnowledgeStatusID] [tinyint] NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]


