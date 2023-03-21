CREATE DATABASE dbfpts
GO
use dbfpts
GO
CREATE TABLE Buyer(
    Id INT PRIMARY KEY,
    Name NVARCHAR(30),
    PaymentMethod NVARCHAR(10)
);
GO
CREATE TABLE Orders(
    Id INT PRIMARY KEY,
    CreatDate DATE,
    Status BIT,
    BuyerId INT,
    Address NVARCHAR(500)
    CONSTRAINT fk_buyerr FOREIGN KEY(BuyerId) REFERENCES Buyer(Id)
);
GO
CREATE TABLE OrderItem(
    OrderId int,
    ProductId int,
    Units int,
    UnitsPrice Decimal,
    PRIMARY KEY(OrderId,ProductID),
    CONSTRAINT fk_order FOREIGN KEY(OrderId) REFERENCES Orders(Id)
)
GO
INSERT INTO buyer (id, name, paymentmethod) VALUES
(1,N'Nguyễn Khắc Hiếu', 'COD'),
(2,N'Nguyễn Mạnh Dũng', 'Online'),
(3,N'Nguyễn Thị Hạnh', 'COD'),
(4,N'Đỗ Ngọc Đức', 'COD'),
(5,N'Nguyễn Văn Hiếu', 'COD'),
(6,N'Nguyễn Khắc Phong', 'Online'),
(7,N'Nguyễn Hữu Chiến', 'Online'),
(8,N'Bùi Thị Trang', 'Online'),
(9,N'Trần Mạnh Tuấn', 'Online'),
(10,N'Phạm Bình Quý', 'Online')
GO
INSERT INTO orders (id, CreatDate, Status, BuyerId, Address) VALUES
(1, '20230203', 1,1,N'Bình Lục'),
(2, '20230205', 0,1,N'Lạc Long Quân'),
(3, '20230204', 0,2,N'Bình Lục'),
(4, '20230203', 1,3,N'Lạc Long Quân'),
(5, '20230204', 1,4,N'Lê Chí Thanh'),
(6, '20230202', 0,4,N'Lạc Long Quân'),
(7, '20230303', 0,7,N'Hoàng Quốc Việt'),
(8, '20230403', 1,8,N'Âu cơ'),
(9, '20230203', 0,8,N'Âu cơ'),
(10,'20230203',1,9,N'Hoàng Quốc Việt')
GO
INSERT INTO OrderItem(OrderId, ProductId, Units, UnitsPrice) VALUES
(1, 2, 3, 2000),
(1, 3, 1, 200),
(1, 4, 2, 500),
(4, 25, 1, 2200),
(5, 6, 1, 3000),
(5, 23, 2, 1500),
(7, 22, 3, 2500),
(8, 23, 4, 1250),
(9, 34, 5, 220),
(10, 15,2, 700)
GO
SELECT * FROM Buyer
SELECT * FROM OrderItem
SELECT * FROM Orders
GO
--Read
-- Lấy ra tất cả những người mua có phương thức thanh toán là COD
SELECT * FROM Buyer
WHERE PaymentMethod = 'COD'
GO
-- Lấy ra các đơn hàng đã thành công(status = 1)
SELECT * FROM Orders
WHERE Status = 1
GO
-- Lấy ra chi tiết đơn hàng có orderid = 1
SELECT * FROM orderitem
WHERE OrderId = 1

--Update
UPDATE buyer
SET PaymentMethod = CASE
	WHEN id=1 THEN 'Online'
    WHEN id=2 THEN 'COD'
    WHEN id=3 THEN 'COD'
    WHEN id=4 THEN 'Online'
    WHEN id=5 THEN 'Online'
    ELSE ''
    END
    WHERE id IN (1,2,3,4,5)

UPDATE orders
SET Address = 'Lạc Long Quân'
WHERE Address = 'Âu Cơ'

UPDATE orderitem 
SET UnitsPrice = CASE
	WHEN ProductId = 1 THEN 2500
	WHEN ProductId = 25 THEN 2300
	WHEN ProductId = 4 THEN 500
	WHEN ProductId = 6 THEN 2000
	WHEN ProductId = 22 THEN 2500
    WHEN ProductId = 23 THEN 2500
    ELSE ''
    END 
    WHERE ProductId IN (1,4,25,6,22,23)
 --Delete
DELETE FROM buyer WHERE id =10
DELETE FROM orders WHERE id =10
DELETE FROM OrderItem WHERE ProductId =1

--Tạo store proceduce
--add Buyer
CREATE PROCEDURE addBuyer
    @id int,
    @name nvarchar(50),
    @paymentMethod NVARCHAR(10)
AS
BEGIN
    INSERT INTO Buyer (Id, Name, PaymentMethod)VALUES
    (@id, @name, @paymentMethod)
END
GO
EXEC addBuyer 11,"Trịnh Khắc Hùng", "COD"
GO
SELECT * FROM Buyer
GO
--add Order

CREATE PROCEDURE addOrder
    @id int,
    @createdate date,
    @status BIT,
    @Buyerid int,
    @address NVARCHAR(500)
AS
BEGIN
    INSERT INTO Orders (Id, CreatDate, Status, BuyerId, Address)VALUES
    (@id, @createdate, @status, @Buyerid, @address)
END
GO
EXEC addOrder 11,"20230101", 1, 10, "Lạc Long Quân"
GO
SELECT * FROM Orders
GO

--add OrderItem
CREATE PROCEDURE addOrderItem
    @orderid int,
    @productid int,
    @units int,
    @unitsprice int
AS
BEGIN
    INSERT INTO OrderItem (OrderId, ProductId, Units, UnitsPrice)VALUES
    (@orderid, @productid, @units, @unitsprice)
END
GO
EXEC addOrderItem 10,25, 5, 1234
GO
SELECT * FROM OrderItem
GO
--update 

--update buyer
CREATE PROCEDURE updateBuyer
    @id int,
    @name nvarchar(50),
    @paymentMethod NVARCHAR(10)
AS
BEGIN
    UPDATE Buyer
    SET
    Name = @name, 
    PaymentMethod =@paymentMethod
    WHERE Id = @id
END
GO
EXEC updateBuyer 11,"Trịnh Văn Hùng", "Online"
GO
SELECT * FROM Buyer
GO

--update Orders
CREATE PROCEDURE updateOrder
    @id int,
    @createdate date,
    @status BIT,
    @Buyerid int,
    @address NVARCHAR(500)
AS
BEGIN
    UPDATE Orders SET 
    CreatDate = @createdate, 
    Status = @status, 
    BuyerId = @Buyerid, 
    Address = @address
    WHERE Id = @id
END
GO
EXEC updateOrder 11,"20230101", 1, 10, "Âu Cơ"
GO
SELECT * FROM Orders
GO

--update OrdersItem
--delete buyer
CREATE PROCEDURE deleteBuyer
    @id int
AS
BEGIN
    DELETE Buyer
    WHERE Id = @id
END
GO
EXEC deleteBuyer 11
GO
SELECT * FROM Buyer
GO
CREATE PROCEDURE updateOrderItem
    @orderid int,
    @productid int,
    @units int,
    @unitsprice int
AS
BEGIN
    UPDATE OrderItem SET 
    ProductId = @productid,
    Units = @units, 
    UnitsPrice = @unitsprice
    Where OrderId = @orderid
END
GO
EXEC updateOrderItem 4,25, 10, 1200
GO
SELECT * FROM OrderItem
GO
-- delete

--delete Orders
CREATE PROCEDURE deleteOrder
    @id int
AS
BEGIN
    DELETE Orders
    WHERE Id = @id
END
GO
EXEC deleteOrder 11
GO
SELECT * FROM Orders
GO
CREATE PROCEDURE deleteOrderItem
    @orderid int,
    @productid int
AS
BEGIN
    delete OrderItem
    Where OrderId = @orderid
    AND ProductId = @productid
END
GO
EXEC deleteOrderItem 4,25
GO
SELECT * FROM OrderItem
GO