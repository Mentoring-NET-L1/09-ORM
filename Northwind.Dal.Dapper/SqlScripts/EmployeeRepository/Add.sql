INSERT [dbo].[Employees] ([LastName], [FirstName])
VALUES (@LastName, @FirstName)

SELECT [EmployeeID]
      ,[LastName]
      ,[FirstName]
FROM [dbo].[Employees]
WHERE [EmployeeID] IN (SELECT SCOPE_IDENTITY());
