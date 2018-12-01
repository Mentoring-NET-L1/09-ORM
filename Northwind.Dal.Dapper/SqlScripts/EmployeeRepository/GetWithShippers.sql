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

DECLARE @EmployeesShippers TABLE
(
    [EmployeeID] int,
    [ShipperID] int
);

INSERT INTO @EmployeesShippers
SELECT [E].[EmployeeID]
      ,[O].[ShipVia]
FROM [dbo].[Employees] AS [E]
LEFT JOIN [dbo].[Orders] AS [O]
  ON [E].[EmployeeID] = [O].[EmployeeID]
GROUP BY [E].[EmployeeID], [O].[ShipVia];

SELECT * FROM @Employees;

SELECT [EmployeeID] AS [PrimaryID]
      ,[ShipperID] AS [SecondaryID]
FROM @EmployeesShippers;

SELECT [ShipperID]
      ,[CompanyName]
      ,[Phone]
FROM [dbo].[Shippers]
WHERE [ShipperID] IN (SELECT [ShipperID] FROM @EmployeesShippers);
