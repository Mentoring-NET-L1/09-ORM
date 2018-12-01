DECLARE @CategoryID int;
IF @CategoryName IS NOT NULL
BEGIN
    SET @CategoryID = (SElECT [CategoryID] FROM [dbo].[Categories] WHERE [CategoryName] = @CategoryName);
    IF @CategoryID IS NULL
    BEGIN
        INSERT INTO [dbo].[Categories] ([CategoryName], [Description])
        VALUES (@CategoryName, @CategoryDescription);

        SET @CategoryID = SCOPE_IDENTITY();
    END;
END
ELSE
    SET @CategoryID = NULL;

DECLARE @SupplierID int;
IF @SupplierName IS NOT NULL
BEGIN
    SET @SupplierID = (SElECT [SupplierID] FROM [dbo].[Suppliers] WHERE [CompanyName] = @SupplierName);
    IF @SupplierID IS NULL
    BEGIN
        INSERT INTO [dbo].[Suppliers] ([CompanyName], [ContactName])
        VALUES (@SupplierName, @SupplierContactName);

        SET @SupplierID = SCOPE_IDENTITY();
    END;
END
ELSE
    SET @SupplierID = NULL;

INSERT INTO [dbo].[Products] ([ProductName], [UnitPrice], [Discontinued], [CategoryID], [SupplierID]) 
VALUES (@ProductName, @UnitPrice, @Discontinued, @CategoryID, @SupplierID)
