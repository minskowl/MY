CREATE VIEW dbo.KnowledgeInfo
AS
SELECT     dbo.Knowledges.KnowledgeID, dbo.Knowledges.Title, dbo.Knowledges.CreationDate, dbo.Knowledges.KnowledgeTypeID, 
                      dbo.Knowledges.KnowledgeStatusID, dbo.KnowledgeTypes.Name AS KnowledgeTypeName, dbo.Knowledges.PublicID, 
                      dbo.KnowledgeStatuses.Name AS KnowledgeStatusName, dbo.Knowledges.CategoryID, dbo.Knowledges.Summary
FROM         dbo.Knowledges INNER JOIN
                      dbo.KnowledgeTypes ON dbo.Knowledges.KnowledgeTypeID = dbo.KnowledgeTypes.KnowledgeTypeID INNER JOIN
                      dbo.KnowledgeStatuses ON dbo.KnowledgeStatuses.KnowledgeStatusID = dbo.Knowledges.KnowledgeStatusID


GO
EXEC sp_addextendedproperty N'MS_DiagramPane1', N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Knowledges"
            Begin Extent = 
               Top = 62
               Left = 395
               Bottom = 279
               Right = 569
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "KnowledgeTypes"
            Begin Extent = 
               Top = 57
               Left = 613
               Bottom = 135
               Right = 780
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "KnowledgeStatuses"
            Begin Extent = 
               Top = 200
               Left = 201
               Bottom = 278
               Right = 375
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
', 'SCHEMA', N'dbo', 'VIEW', N'KnowledgeInfo', NULL, NULL
GO
EXEC sp_addextendedproperty N'MS_DiagramPaneCount', 1, 'SCHEMA', N'dbo', 'VIEW', N'KnowledgeInfo', NULL, NULL

