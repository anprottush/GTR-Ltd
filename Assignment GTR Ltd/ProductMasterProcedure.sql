/****** Script for SelectTopNRows command from SSMS  ******/
SELECT TOP (1000) [Id]
      ,[Name]
      ,[Barcode]
      ,[Type]
      ,[Quantity]
      ,[Price]
  FROM [UserData].[dbo].[ProductMasters]

  CREATE PROCEDURE sp_CreateProduct(
      @Name VARCHAR(100),
	  @Barcode VARCHAR(100),
	  @Type VARCHAR(100),
	  @Quantity INT,
	  @Price FLOAT)
AS
BEGIN
    INSERT INTO ProductMasters (Name, Barcode, Type, Quantity, Price)
    VALUES (@Name, @Barcode, @Type, @Quantity, @Price);
END

-- Update an existing product
CREATE PROCEDURE sp_EditProduct(
    @Id INT,
    @Name VARCHAR(100),
    @Barcode VARCHAR(100),
    @Type VARCHAR(100),
    @Quantity INT,
    @Price FLOAT)
AS
BEGIN
    UPDATE ProductMasters
    SET Name = @Name,
        Barcode = @Barcode, 
        Type = @Type,
        Quantity = @Quantity,
	Price = @Price
    WHERE Id = @Id
END

-- Delete a product by Id
CREATE PROCEDURE sp_RemoveProduct
    @Id INT
AS
BEGIN
    DELETE FROM ProductMasters
    WHERE Id = @Id
END

-- Get all products
CREATE PROCEDURE sp_FindAllProducts
AS
BEGIN
    SELECT * FROM ProductMasters
END

-- Get a product by Id
CREATE PROCEDURE sp_FindProduct(
    @Id INT)
AS
BEGIN
    SELECT * FROM ProductMasters
    WHERE Id = @Id
END