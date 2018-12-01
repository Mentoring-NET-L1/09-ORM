SELECT [R].[RegionID]
      ,[R].[RegionDescription]
      ,COUNT([ET].[EmployeeID]) AS [EmployeeCount]
FROM [dbo].[Regions] AS [R]
LEFT JOIN [dbo].[Territories] AS [T]
  ON [R].[RegionID] = [T].[RegionID]
LEFT JOIN [dbo].[EmployeeTerritories] AS [ET]
  ON [T].[TerritoryID] = [ET].[TerritoryID]
GROUP BY [R].[RegionID], [R].[RegionDescription];
