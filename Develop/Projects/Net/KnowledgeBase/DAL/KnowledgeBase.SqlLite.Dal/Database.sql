CREATE TABLE [Categories] (
[CategoryID] INTEGER  PRIMARY KEY AUTOINCREMENT NOT NULL,
[ParentCategoryID] INTEGER  NULL,
[Name] NVARCHAR(50)  NOT NULL,
[CreationDate] TIMESTAMP DEFAULT CURRENT_TIMESTAMP NOT NULL
);

CREATE TABLE [FileIncludes] (
[FileIncludeID] NVARCHAR(50)  UNIQUE NULL PRIMARY KEY,
[KnowledgeID] INTEGER  NOT NULL,
[FileName] NVARCHAR(100)  NOT NULL,
[Data] BLOB  NULL,
[Size] INTEGER  NOT NULL
);

CREATE TABLE [KeywordStatuses] (
[KeywordStatusID] INTEGER  NOT NULL PRIMARY KEY,
[Name] TEXT  UNIQUE NULL
);

CREATE TABLE [KeywordTypes] (
[KeywordTypeID] INTEGER  NOT NULL PRIMARY KEY,
[Name] TEXT  UNIQUE NOT NULL
);

CREATE TABLE [Keywords] (
[KeywordID] INTEGER  NOT NULL PRIMARY KEY AUTOINCREMENT,
[Name] NVARCHAR(50)  NOT NULL,
[KeywordTypeID] INTEGER  NOT NULL,
[CreationDate] TIMESTAMP DEFAULT CURRENT_TIMESTAMP NOT NULL,
[KeywordStatusID] INTEGER  NOT NULL
);

CREATE TABLE [KnowledgeKeywords] (
[KnowledgeID] INTEGER  NOT NULL,
[KeywordID] INTEGER  NOT NULL,
PRIMARY KEY ([KnowledgeID],[KeywordID])
);

CREATE TABLE [KnowledgeStatuses] (
[KnowledgeStatusID] INTEGER  NOT NULL PRIMARY KEY,
[Name] NVARCHAR(10)  NOT NULL
);

CREATE TABLE [KnowledgeTypes] (
[KnowledgeTypeID] INTEGER  NULL PRIMARY KEY,
[Name] NVARCHAR(50)  NOT NULL
);

CREATE TABLE [Knowledges] (
[KnowledgeID] INTEGER  NOT NULL PRIMARY KEY AUTOINCREMENT,
[CategoryID] INTEGER  NOT NULL,
[Title] NVARCHAR(150)  NOT NULL,
[Summary] TEXT  NOT NULL,
[KnowledgeTypeID] INTEGER  NOT NULL,
[CreationDate] TIMESTAMP DEFAULT CURRENT_TIMESTAMP NOT NULL,
[CreatorID] INTEGER  NULL,
[ModificationDate] TIMESTAMP DEFAULT CURRENT_TIMESTAMP NULL,
[ModificatorID] INTEGER  NULL,
[PublicID] BLOB  UNIQUE NOT NULL,
[KnowledgeStatusID] INTEGER  NOT NULL
);

CREATE TABLE [UserFiles] (
[UserFileID] INTEGER  NOT NULL PRIMARY KEY AUTOINCREMENT,
[FileName] NVARCHAR(100)  NOT NULL,
[Data] BLOB  NULL,
[Size] INTEGER  NOT NULL
);

CREATE VIEW [CategoriesInfo] AS 
SELECT     CategoryID, ParentCategoryID, Name, CreationDate,
ifnull ((SELECT     COUNT(*) AS Expr1 FROM  Knowledges WHERE     (c.CategoryID = CategoryID)), 0) AS cntKnowledges,
ifnull ((SELECT     COUNT(*) AS Expr2 FROM  Categories AS subCat  WHERE     (ParentCategoryID = c.CategoryID)), 0) AS cntSubCategories
FROM         Categories AS c;

CREATE VIEW [KnowledgeInfo] AS 
SELECT        Knowledges.KnowledgeID, Knowledges.Title, Knowledges.CreationDate, Knowledges.KnowledgeTypeID, Knowledges.KnowledgeStatusID,
                         KnowledgeTypes.Name AS KnowledgeTypeName, Knowledges.PublicID, KnowledgeStatuses.Name AS KnowledgeStatusName,
                         Knowledges.CategoryID, Knowledges.Summary, Knowledges.CreatorID
FROM            Knowledges INNER JOIN
                         KnowledgeTypes ON Knowledges.KnowledgeTypeID = KnowledgeTypes.KnowledgeTypeID INNER JOIN
                         KnowledgeStatuses ON KnowledgeStatuses.KnowledgeStatusID = Knowledges.KnowledgeStatusID;


INSERT INTO [KeywordStatuses] ([KeywordStatusID], [Name]) VALUES (0, 'New');
INSERT INTO [KeywordStatuses] ([KeywordStatusID], [Name]) VALUES (5, 'Approved');
INSERT INTO [KeywordTypes] ([KeywordTypeID], [Name]) VALUES (1, 'Other');
INSERT INTO [KeywordTypes] ([KeywordTypeID], [Name]) VALUES (2, 'Programming Languages');
INSERT INTO [KeywordTypes] ([KeywordTypeID], [Name]) VALUES (3, 'Databases');
INSERT INTO [KeywordTypes] ([KeywordTypeID], [Name]) VALUES (4, 'Technologies');
INSERT INTO [KeywordTypes] ([KeywordTypeID], [Name]) VALUES (5, 'Browsers');
INSERT INTO [KeywordTypes] ([KeywordTypeID], [Name]) VALUES (6, 'OS');
INSERT INTO [KeywordTypes] ([KeywordTypeID], [Name]) VALUES (7, 'Classes');
INSERT INTO [KnowledgeStatuses] ([KnowledgeStatusID], [Name]) VALUES (0, 'New');
INSERT INTO [KnowledgeStatuses] ([KnowledgeStatusID], [Name]) VALUES (1, 'Active');
INSERT INTO [KnowledgeStatuses] ([KnowledgeStatusID], [Name]) VALUES (2, 'Hide');
INSERT INTO [KnowledgeStatuses] ([KnowledgeStatusID], [Name]) VALUES (3, 'Deleted');
INSERT INTO [KnowledgeTypes] ([KnowledgeTypeID], [Name]) VALUES (1, 'Info');
INSERT INTO [KnowledgeTypes] ([KnowledgeTypeID], [Name]) VALUES (2, 'Error');
INSERT INTO [KnowledgeTypes] ([KnowledgeTypeID], [Name]) VALUES (3, 'Solution');
INSERT INTO [KnowledgeTypes] ([KnowledgeTypeID], [Name]) VALUES (4, 'Resource');


