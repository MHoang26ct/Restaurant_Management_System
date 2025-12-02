/* =============================================
   FILE: STORED PROCEDURES
   MÔ TẢ: Các thủ tục lưu trữ cho hệ thống quản lý nhà hàng
   NGÀY CẬP NHẬT: 2025
   ============================================= */

-- =============================================
-- NHÓM 1: QUẢN LÝ MÓN ĂN (FOODS)
-- =============================================

-- 1.1. Thêm món ăn mới vào thực đơn
-- Input: Tên món, Giá
-- Output: ID của món vừa tạo
CREATE PROCEDURE AddFood
    @FoodName varchar(100),
    @Price decimal(10, 2),
    @NewFoodID int OUTPUT 
AS 
BEGIN 
    INSERT INTO Foods (FoodName, Price)
    VALUES (@FoodName, @Price)
    
    SET @NewFoodID = SCOPE_IDENTITY()
END
GO

-- 1.2. Cập nhật thông tin món ăn (Tên, Giá)
CREATE PROCEDURE UpdateFood 
    @FoodID int,
    @FoodName varchar(100),
    @Price decimal(10, 2)
AS
BEGIN
    UPDATE Foods
    SET FoodName = @FoodName,
        Price = @Price
    WHERE FoodID = @FoodID
END
GO

-- 1.3. Xóa món ăn khỏi thực đơn
CREATE PROCEDURE DeleteFood
    @FoodID int
AS
BEGIN
    DELETE FROM Foods
    WHERE FoodID = @FoodID
END
GO

-- =============================================
-- NHÓM 2: QUẢN LÝ BÀN ĂN (TABLES)
-- =============================================

-- 2.1. Thêm bàn ăn mới
-- Input: Sức chứa, Trạng thái, Thời gian mở (nếu có)
-- Output: ID bàn vừa tạo
CREATE PROCEDURE AddTable
    @Capacity INT,
    @TableStatus VARCHAR(20),
    @OpenTime DATETIME NULL,
    @NewTableID INT OUTPUT
AS
BEGIN
    INSERT INTO Tables (Capacity, TableStatus, OpenTime)
    VALUES (@Capacity, @TableStatus, @OpenTime)

    SET @NewTableID = SCOPE_IDENTITY()
END
GO

-- 2.2. Cập nhật trạng thái bàn và thời gian mở bàn (Check-in/Check-out bàn)
CREATE PROCEDURE UpdateTableStatusAndOpenTime
    @TableID int,
    @NewStatus varchar(20),
    @OpenTime datetime null
AS
BEGIN
    UPDATE Tables
    SET TableStatus = @NewStatus,
        OpenTime = @OpenTime
    WHERE TableID = @TableID
END
GO

-- 2.3. Xóa bàn ăn
CREATE PROCEDURE DeleteTable
    @TableID INT
AS
BEGIN
    DELETE FROM Tables
    WHERE TableID = @TableID
END
GO

-- =============================================
-- NHÓM 3: QUẢN LÝ KHÁCH HÀNG (CUSTOMERS)
-- =============================================

-- 3.1. Thêm khách hàng mới
CREATE PROCEDURE AddCustomer
    @FullName varchar(50),
    @Email varchar(100),
    @PhoneNumber varchar(15),
    @NewCustomerID int OUTPUT
AS
BEGIN
    INSERT INTO Customers (FullName, Email, PhoneNumber)
    VALUES (@FullName, @Email, @PhoneNumber)

    SET @NewCustomerID = SCOPE_IDENTITY()
END
GO

-- 3.2. Cập nhật thông tin liên hệ của khách hàng
CREATE PROCEDURE UpdateCustomerInfo
    @CustomerID INT,
    @FullName VARCHAR(50),
    @Email VARCHAR(100),
    @PhoneNumber VARCHAR(15)
AS
BEGIN
    UPDATE Customers
    SET FullName = @FullName,
        Email = @Email,
        PhoneNumber = @PhoneNumber
    WHERE CustomerID = @CustomerID
END
GO

-- =============================================
-- NHÓM 4: QUẢN LÝ ĐẶT BÀN (RESERVATIONS)
-- =============================================

-- 4.1. Tạo phiếu đặt bàn mới
CREATE PROCEDURE AddReservation
    @CustomerID int,
    @TableID int,
    @ReservationTime datetime,
    @ComingTime datetime,
    @NumberOfGuests int,
    @NewReservationID int OUTPUT
AS
BEGIN
    INSERT INTO Reservations (CustomerID, TableID, ReservationTime, ComingTime, NumberOfGuests)
    VALUES (@CustomerID, @TableID, @ReservationTime, @ComingTime, @NumberOfGuests)

    SET @NewReservationID = SCOPE_IDENTITY()
END
GO

-- =============================================
-- NHÓM 5: QUẢN LÝ ĐƠN HÀNG & CHI TIẾT (ORDERS & ORDER DETAILS)
-- =============================================

-- 5.1. Tạo đơn hàng mới (Order)
-- Lưu ý: TotalAmount thường để 0 ban đầu, sẽ được tính lại khi thêm món
CREATE PROCEDURE AddOrder
    @ReservationID int,
    @TableID int,
    @OrderTime datetime,
    @TotalAmount decimal(10, 2) = 0,
    @NumberOfGuests int,
    @CustomerID int,
    @NewOrderID int OUTPUT
AS
BEGIN
    INSERT INTO Orders (ReservationID, TableID, OrderTime, TotalAmount, NumberOfGuests, CustomerID)
    VALUES (@ReservationID, @TableID, @OrderTime, @TotalAmount, @NumberOfGuests, @CustomerID)

    SET @NewOrderID = SCOPE_IDENTITY()
END
GO

-- 5.2. Thêm món vào đơn hàng (Order Detail)
CREATE PROCEDURE AddOrderDetail
    @OrderID int,
    @FoodID int,
    @Quantity int,
    @Notes varchar(255),
    @OrderStatus varchar(20)
AS
BEGIN
    INSERT INTO OrderDetails (OrderID, FoodID, Quantity, Notes, OrderStatus)
    VALUES (@OrderID, @FoodID, @Quantity, @Notes, @OrderStatus)
END
GO

-- 5.3. Cập nhật trạng thái món ăn (Ví dụ: Đang nấu -> Đã xong)
CREATE PROCEDURE UpdateOrderStatus
    @OrderDetailID int,
    @NewStatus varchar(20)
AS
BEGIN
    UPDATE OrderDetails
    SET OrderStatus = @NewStatus
    WHERE OrderDetailID = @OrderDetailID
END
GO

-- 5.4. Cập nhật thời gian thanh toán (Checkout) cho đơn hàng
-- Chỉ cập nhật nếu đơn hàng chưa thanh toán (TimeCheckout is NULL)
CREATE PROCEDURE UpdateOrderCheckoutTime
    @OrderID INT,
    @CheckoutTime DATETIME
AS
BEGIN
    UPDATE Orders
    SET TimeCheckout = @CheckoutTime
    WHERE OrderID = @OrderID 
    AND TimeCheckout IS NULL
END
GO

-- =============================================
-- NHÓM 6: QUẢN LÝ NHÂN VIÊN (EMPLOYEES)
-- =============================================

-- 6.1. Thêm nhân viên mới
CREATE PROCEDURE AddEmployee
    @FullName VARCHAR(50),
    @PhoneNumber VARCHAR(15),
    @Email VARCHAR(100),
    @Position VARCHAR(50),
    @HireDate DATETIME
AS
BEGIN
    INSERT INTO Employees (FullName, PhoneNumber, Email, Position, HireDate)
    VALUES (@FullName, @PhoneNumber, @Email, @Position, @HireDate)
END
GO

-- 6.2. Cập nhật thông tin nhân viên
CREATE PROCEDURE UpdateEmployee
    @FullName VARCHAR(50),
    @PhoneNumber VARCHAR(15),
    @Email VARCHAR(100),
    @Position VARCHAR(50),
    @HireDate DATETIME
AS
BEGIN
    UPDATE Employees
    SET Email = @Email,
        Position = @Position,
        HireDate = @HireDate
    WHERE FullName = @FullName AND PhoneNumber = @PhoneNumber
END
GO

-- 6.3. Xóa nhân viên
CREATE PROCEDURE DeleteEmployee
    @FullName VARCHAR(50),
    @PhoneNumber VARCHAR(15)
AS
BEGIN
    DELETE FROM Employees
    WHERE FullName = @FullName AND PhoneNumber = @PhoneNumber
END
GO

-- =============================================
-- NHÓM 7: QUẢN LÝ TÀI KHOẢN HỆ THỐNG (USERS)
-- =============================================

-- 7.1. Tạo tài khoản người dùng
CREATE PROCEDURE AddUser
    @Username VARCHAR(50),
    @PasswordHash VARCHAR(255),
    @UserRole INT
AS
BEGIN
    INSERT INTO Users (Username, PasswordHash, UserRole)
    VALUES (@Username, @PasswordHash, @UserRole)
END
GO

-- 7.2. Đổi mật khẩu
CREATE PROCEDURE ChangeUserPassword
    @Username VARCHAR(50),
    @NewPasswordHash VARCHAR(255)
AS
BEGIN
    UPDATE Users
    SET PasswordHash = @NewPasswordHash
    WHERE Username = @Username
END
GO

-- 7.3. Xóa tài khoản
CREATE PROCEDURE DeleteUser
    @Username VARCHAR(50)
AS
BEGIN
    DELETE FROM Users
    WHERE Username = @Username
END
GO

-- =============================================
-- NHÓM 8: BÁO CÁO & THỐNG KÊ (REPORTS)
-- =============================================

-- 8.1. Thống kê kinh doanh theo khoảng thời gian
-- Chỉ tính các đơn hàng đã thanh toán (TimeCheckout IS NOT NULL)
CREATE PROCEDURE GetBusinessStatsByDate
    @fromDate DATE,
    @toDate DATE
AS
BEGIN
    SELECT 
        CAST(OrderTime AS DATE) AS Date, 
        SUM(TotalAmount) AS TotalRevenue,
        COUNT(DISTINCT Orders.OrderID) AS TotalOrders,
        SUM(NumberOfGuests) AS TotalGuests,
        COUNT(DISTINCT CASE WHEN ReservationID IS NOT NULL THEN ReservationID END) AS TotalReservations
    FROM Orders
    WHERE 
        OrderTime >= @fromDate 
        AND OrderTime < DATEADD(day, 1, @toDate)
        AND TimeCheckout IS NOT NULL
    GROUP BY CAST(OrderTime AS DATE)
    ORDER BY Date
END
GO