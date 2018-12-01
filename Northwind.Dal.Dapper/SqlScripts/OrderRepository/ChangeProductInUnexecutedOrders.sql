UPDATE [dbo].[Order Details]
SET [ProductID] = @NewProductID
WHERE [ProductID] = @ProductID AND
      [OrderID] IN (
          SELECT [OrderID] 
          FROM [dbo].[Orders]
          WHERE [ShippedDate] IS NULL);
