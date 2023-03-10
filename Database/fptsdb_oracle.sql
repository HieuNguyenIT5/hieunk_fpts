CREATE TABLE Buyer(
    Id NUMBER(10) PRIMARY KEY,
    Name VARCHAR2(30),
    PaymentMethod VARCHAR2(10)
);
GO
CREATE TABLE Orders(
    Id NUMBER(10) PRIMARY KEY,
    CreatDate DATE,
    Status NUMBER(1),
    BuyerId INT,
    Address VARCHAR2(500),
    CONSTRAINT fk_buyer FOREIGN KEY(BuyerId) REFERENCES Buyer(Id)
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
(1,N'Nguy?n Kh?c Hi?u', 'COD')

INSERT INTO buyer (id, name, paymentmethod) VALUES
(2,N'Nguy?n M?nh D?ng', 'Online')

INSERT INTO buyer (id, name, paymentmethod) VALUES
(3,N'Nguy?n Th? H?nh', 'COD');

INSERT INTO buyer (id, name, paymentmethod) VALUES
(4,N'?? Ng?c ??c', 'COD');

INSERT INTO buyer (id, name, paymentmethod) VALUES
(5,N'Nguy?n V?n Hi?u', 'COD');

INSERT INTO buyer (id, name, paymentmethod) VALUES
(6,N'Nguy?n Kh?c Phong', 'Online');

INSERT INTO buyer (id, name, paymentmethod) VALUES
(7,N'Nguy?n H?u Chi?n', 'Online');

INSERT INTO buyer (id, name, paymentmethod) VALUES
(8,N'Bùi Th? Trang', 'Online');

INSERT INTO buyer (id, name, paymentmethod) VALUES
(9,N'Tr?n M?nh Tu?n', 'Online');

INSERT INTO buyer (id, name, paymentmethod) VALUES
(10,N'Ph?m Bình Quý', 'Online');
Select * from buyer
GO
INSERT ALL 
  INTO orders (id, CreatDate, Status, BuyerId, Address) VALUES (1, '04-FEB-23', 1,1,N'Bình L?c')
  INTO orders (id, CreatDate, Status, BuyerId, Address) VALUES (2, '03-FEB-23', 0,1,N'L?c Long Quân')
  INTO orders (id, CreatDate, Status, BuyerId, Address) VALUES (3, '05-FEB-23', 0,2,N'Bình L?c')
  INTO orders (id, CreatDate, Status, BuyerId, Address) VALUES (4, '05-FEB-23', 1,3,N'L?c Long Quân')
  INTO orders (id, CreatDate, Status, BuyerId, Address) VALUES (5, '03-FEB-23', 1,4,N'Lê Chí Thanh')
  INTO orders (id, CreatDate, Status, BuyerId, Address) VALUES (6, '03-FEB-23', 0,4,N'L?c Long Quân')
  INTO orders (id, CreatDate, Status, BuyerId, Address) VALUES (7, '01-FEB-23', 0,7,N'Hoàng Qu?c Vi?t')
  INTO orders (id, CreatDate, Status, BuyerId, Address) VALUES (8, '03-FEB-23', 1,8,N'Âu c?')
  INTO orders (id, CreatDate, Status, BuyerId, Address) VALUES (9, '08-FEB-23', 0,8,N'Âu c?')
  INTO orders (id, CreatDate, Status, BuyerId, Address) VALUES (10,'03-FEB-23',1,9,N'Hoàng Qu?c Vi?t')
SELECT * FROM dual;
GO
INSERT ALL
INTO OrderItem(OrderId, ProductId, Units, UnitsPrice) VALUES (1, 2, 3, 2000)
INTO OrderItem(OrderId, ProductId, Units, UnitsPrice) VALUES (1, 5, 1, 200)
INTO OrderItem(OrderId, ProductId, Units, UnitsPrice) VALUES (1, 3, 1, 200)
INTO OrderItem(OrderId, ProductId, Units, UnitsPrice) VALUES (1, 4, 2, 500)
INTO OrderItem(OrderId, ProductId, Units, UnitsPrice) VALUES (4, 25, 1, 2200)
INTO OrderItem(OrderId, ProductId, Units, UnitsPrice) VALUES (5, 6, 1, 3000)
INTO OrderItem(OrderId, ProductId, Units, UnitsPrice) VALUES (5, 23, 2, 1500)
INTO OrderItem(OrderId, ProductId, Units, UnitsPrice) VALUES (7, 22, 3, 2500)
SELECT * FROM dual;
GO
SELECT * FROM Buyer
SELECT * FROM OrderItem
SELECT * FROM Orders
GO
--Read
-- L?y ra t?t c? nh?ng ng??i mua có ph??ng th?c thanh toán là COD
SELECT * FROM Buyer
WHERE PaymentMethod = 'COD'
GO
-- L?y ra các ??n hàng ?ã thành công(status = 1)
SELECT * FROM Orders
WHERE Status = 1
GO
-- L?y ra chi ti?t ??n hàng có orderid = 1
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
SET Address = 'L?c Long Quân'
WHERE Address = 'Âu C?'

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

--T?o store proceduce
--add Buyer
CREATE OR REPLACE PROCEDURE addBuyer(
    id IN NUMBER,
    name IN VARCHAR2,
    paymentMethod IN VARCHAR2
)
IS
BEGIN
    INSERT INTO Buyer (Id, Name, PaymentMethod)
    VALUES (id, name, paymentMethod);
    COMMIT;
END addBuyer;
GO
EXEC addBuyer 11,"Tr?nh Kh?c Hùng", "COD"
GO
SELECT * FROM Buyer
GO
--add Order

CREATE OR REPLACE PROCEDURE addOrder(
    id IN NUMBER,
    createdate IN DATE,
    status IN INT,
    Buyerid IN INT,
    address IN VARCHAR2
)
IS
BEGIN
    INSERT INTO Orders (Id, CreatDate, Status, BuyerId, Address)
    VALUES (id, createdate, status, Buyerid, address);
    COMMIT;
END addOrder;
GO
EXEC addOrder 11,"20230101", 1, 10, "L?c Long Quân"
GO
SELECT * FROM Orders
GO

--add OrderItem
CREATE OR REPLACE PROCEDURE addOrderItem(
    orderid IN INT,
    productid IN INT,
    units IN INT,
    unitsprice IN INT
)
IS
BEGIN
    INSERT INTO OrderItem (OrderId, ProductId, Units, UnitsPrice)VALUES
    (orderid, productid, units, unitsprice);
    COMMIT;
END addOrderItem;
GO
EXEC addOrderItem 10,25, 5, 1234
GO
SELECT * FROM OrderItem
GO
--update 

--update buyer
CREATE OR REPLACE PROCEDURE updateBuyer(
    id IN int,
    name IN varchar2,
    paymentMethod IN VARCHAR2
)
IS
BEGIN
    UPDATE Buyer
    SET
    Name = name, 
    Buyer.PaymentMethod =paymentMethod
    WHERE Buyer.Id = id;
    COMMIT;
END updateBuyer;
GO
EXEC updateBuyer(11, 'Tr?nh V?n Hùng', 'Online');
GO
SELECT * FROM Buyer
GO

--update Orders
CREATE OR REPLACE PROCEDURE updateOrder(
    id IN int,
    createdate IN date,
    status IN INT,
    Buyerid IN int,
    address IN VARCHAR2
)
IS
BEGIN
    UPDATE Orders SET 
    Orders.CreatDate = createdate, 
    Orders.Status = status, 
    Orders.BuyerId = Buyerid, 
    Orders.Address = address
    WHERE Orders.Id = id;
    COMMIT;
END updateOrder;
GO
CALL updateOrder(11, TO_DATE('20230101', 'YYYYMMDD'), 1, 10, 'Âu C?');
GO
SELECT * FROM Orders
GO

--update OrdersItem
--delete buyer
CREATE OR REPLACE PROCEDURE deleteBuyer(
    id in int)
IS
BEGIN
    DELETE Buyer
    WHERE Buyer.Id = id;
    commit;
END deleteBuyer;
GO
CREATE OR REPLACE PROCEDURE updateOrderItem(
    orderid in int,
    productid in int,
    units in int,
    unitsprice in int
    )
IS
BEGIN
    UPDATE OrderItem SET 
    ProductId = productid,
    Units = units, 
    UnitsPrice = unitsprice
    Where OrderId = orderid;
    commit;
END updateOrderItem;
GO
-- delete

--delete Orders
CREATE OR REPLACE PROCEDURE deleteOrder(
    id in int
    )
IS
BEGIN
    DELETE Orders
    WHERE Id = id;
END deleteOrder;

CREATE OR REPLACE PROCEDURE deleteOrderItem(
    orderid in int,
    productid in int
    )
is
BEGIN
    delete OrderItem
    Where OrderId = orderid
    AND ProductId = productid;
    commit;
END deleteOrderItem;


BEGIN
    addBuyer(11, 'L??ng', 'COD');
END;
select * from Buyer

