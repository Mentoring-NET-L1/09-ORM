DECLARE @Employees TABLE
(
    [EmployeeID] int,
    [LastName] nvarchar(20),
    [FirstName] nvarchar(10)
);

INSERT INTO @Employees
SELECT [EmployeeID]
      ,[LastName]
      ,[FirstName]
FROM [dbo].[Employees];

DECLARE @EmployeesRegions TABLE
(
    [EmployeeID] int,
    [RegionID] int
);

INSERT INTO @EmployeesRegions
SELECT [ET].[EmployeeID]
      ,[T].[RegionID]
FROM [dbo].[EmployeeTerritories] AS [ET]
JOIN [dbo].[Territories] AS [T]
  ON [ET].[TerritoryID] = [T].[TerritoryID]
WHERE [EmployeeID] IN (SELECT [EmployeeID] FROM @Employees)
GROUP BY [ET].[EmployeeID], [T].[RegionID];

SELECT * FROM @Employees;

SELECT [EmployeeID] AS [PrimaryID]
      ,[RegionID] AS [SecondaryID]
FROM @EmployeesRegions;

SELECT [RegionID]
      ,[RegionDescription]
FROM [dbo].[Regions]
WHERE [RegionID] IN (SELECT [RegionID] FROM @EmployeesRegions);
