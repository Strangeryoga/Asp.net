CREATE TABLE Products (
    Id INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(100),
    Description NVARCHAR(255),
    Price DECIMAL(10, 2),
    Quantity INT
);


CREATE PROCEDURE CRUD_Products
    @Action NVARCHAR(10), -- Action type: INSERT, UPDATE, DELETE, SELECT
    @Id INT = NULL, -- Product ID (for UPDATE and DELETE)
    @Name NVARCHAR(100) = NULL, -- Product name (for INSERT and UPDATE)
    @Description NVARCHAR(255) = NULL, -- Product description (for INSERT and UPDATE)
    @Price DECIMAL(10, 2) = NULL, -- Product price (for INSERT and UPDATE)
    @Quantity INT = NULL -- Product quantity (for INSERT)
AS
BEGIN
    IF @Action = 'SELECT'
    BEGIN
        SELECT * FROM Products
    END
    ELSE IF @Action = 'INSERT'
    BEGIN
        INSERT INTO Products (Name, Description, Price, Quantity)
        VALUES (@Name, @Description, @Price, @Quantity)
    END
    ELSE IF @Action = 'UPDATE'
    BEGIN
        UPDATE Products
        SET Name = @Name, Description = @Description, Price = @Price
        WHERE Id = @Id
    END
    ELSE IF @Action = 'DELETE'
    BEGIN
        DELETE FROM Products WHERE Id = @Id
    END
END


select * from Products