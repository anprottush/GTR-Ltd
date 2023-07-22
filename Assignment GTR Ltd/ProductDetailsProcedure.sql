/****** Script for SelectTopNRows command from SSMS  ******/
SELECT TOP (1000) [Id]
      ,[PurchaseCustomer]
      ,[PurchaseDate]
      ,[SalesEmployee]
      ,[Description]
      ,[Stock]
      ,[Status]
      ,[ProductMasterId]
  FROM [UserData].[dbo].[ProductDetails]

   CREATE PROCEDURE sp_InsertProduct(
      @PurchaseCustomer VARCHAR(100),
	  @PurchaseDate VARCHAR(100),
	  @SalesEmployee VARCHAR(100),
	  @Description VARCHAR(100),
	  @Stock INT,
	  @Status VARCHAR(100),
	  @ProductMasterId INT
	  )
AS
BEGIN
    INSERT INTO ProductDetails (PurchaseCustomer, PurchaseDate, SalesEmployee, Description, Stock, Status, ProductMasterId)
    VALUES (@PurchaseCustomer, @PurchaseDate, @SalesEmployee, @Description, @Stock, @Status, @ProductMasterId);
END

-- Update an existing product
CREATE PROCEDURE sp_UpdateProduct(
    @Id INT,
   @PurchaseCustomer VARCHAR(100),
	  @PurchaseDate VARCHAR(100),
	  @SalesEmployee VARCHAR(100),
	  @Description VARCHAR(100),
	  @Stock INT,
	  @Status VARCHAR(100),
	  @ProductMasterId INT
	  )
AS
BEGIN
    UPDATE ProductDetails
    SET PurchaseCustomer = @PurchaseCustomer,
        PurchaseDate = @PurchaseDate, 
        SalesEmployee = @SalesEmployee,
        Description = @Description,
		Stock = @Stock,
		Status = @Status,
		ProductMasterId= @ProductMasterId
    WHERE Id = @Id
END

-- Delete a product by Id
CREATE PROCEDURE sp_DeleteProduct
    @Id INT
AS
BEGIN
    DELETE FROM ProductDetails
    WHERE Id = @Id
END

-- Get all products
CREATE PROCEDURE sp_GetAllProducts
AS
BEGIN
    SELECT * FROM ProductDetails
END

-- Get a product by Id
CREATE PROCEDURE sp_GetProduct(
    @Id INT)
AS
BEGIN
    SELECT * FROM ProductDetails
    WHERE Id = @Id
END