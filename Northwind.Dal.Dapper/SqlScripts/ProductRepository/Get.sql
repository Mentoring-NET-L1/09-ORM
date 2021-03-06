﻿SELECT [p].[ProductID]
      ,[p].[ProductName]
      ,[p].[QuantityPerUnit]
      ,[p].[UnitPrice]
      ,[p].[UnitsInStock]
      ,[p].[UnitsOnOrder]
      ,[p].[ReorderLevel]
      ,[p].[Discontinued]
      ,[c].[CategoryID]
      ,[c].[CategoryName]
      ,[c].[Description]
      ,[s].[SupplierID]
      ,[s].[CompanyName]
      ,[s].[ContactName]
  FROM [dbo].[Products] AS [p]
  LEFT JOIN [dbo].[Categories] AS [c]
    ON [p].[CategoryID] = [c].[CategoryID]
  LEFT JOIN [dbo].[Suppliers] AS [s]
    ON [p].[SupplierID] = [s].[SupplierID];
