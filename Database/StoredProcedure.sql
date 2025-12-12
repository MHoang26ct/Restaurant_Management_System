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
    @Category VARCHAR(50) = NULL,
    @ImagePath VARCHAR(500) = NULL,
    @Description NVARCHAR(MAX) = NULL,
    @NewFoodID int OUTPUT 
AS 
BEGIN 
    INSERT INTO Foods (FoodName, Price, Category, ImagePath, Description)
    VALUES (@FoodName, @Price, @Category, @ImagePath, @Description)
    
    SET @NewFoodID = SCOPE_IDENTITY()
END
GO

-- 1.2. Cập nhật thông tin món ăn (Tên, Giá)
CREATE PROCEDURE UpdateFood
    @FoodID int,
    @FoodName varchar(100),
    @Price decimal(10, 2),
    @Category VARCHAR(50) = NULL,
    @ImagePath VARCHAR(500) = NULL,
    @Description NVARCHAR(MAX) = NULL
AS
BEGIN
    UPDATE Foods
    SET 
        -- Cập nhật bắt buộc (luôn luôn cập nhật FoodName và Price)
        FoodName = @FoodName, 
        Price = @Price,

        -- Cập nhật có điều kiện: Nếu tham số (@Category, @ImagePath, @Description) là NULL, 
        -- giữ nguyên giá trị hiện tại của cột. Nếu không NULL, lấy giá trị của tham số.
        Category = COALESCE(@Category, Category), 
        ImagePath = COALESCE(@ImagePath, ImagePath),
        Description = COALESCE(@Description, Description)
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

-- 1.4 Lấy danh sách các món ăn để hiển thị lên giao diện
CREATE PROCEDURE GetAllFoods
AS
BEGIN
    SELECT FoodID, FoodName, Price, Category, ImagePath, Description
    FROM Foods
    ORDER BY FoodName
END
GO

-- 1.5 Tìm kiếm món ăn theo tên (dùng cho chức năng tìm kiếm nhanh)
CREATE PROCEDURE SearchFoodsByName
    @SearchTerm VARCHAR(100)
AS
BEGIN
    SELECT FoodID, FoodName, Price, Category, ImagePath, Description
    FROM Foods
    WHERE FoodName LIKE '%' + @SearchTerm + '%'
    ORDER BY FoodName
END
GO

-- 1.6 Lấy món ăn theo ID
CREATE PROCEDURE GetFoodByID
    @FoodID INT
AS
BEGIN
    SELECT FoodID, FoodName, Price, Category, ImagePath, Description
    FROM Foods
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

-- 2.4. Lấy danh sách các bàn ăn để hiển thị lên giao diện
CREATE PROCEDURE GetAllTables
AS
BEGIN
    SELECT TableID, Capacity, TableStatus, OpenTime
    FROM Tables
    ORDER BY TableID
END
GO

-- 2.5 Lấy danh sách các bàn ăn trống
CREATE PROCEDURE GetAvailableTables
AS
BEGIN
    SELECT TableID, Capacity, TableStatus, OpenTime
    FROM Tables
    WHERE TableStatus = 'Available'
    ORDER BY TableID
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

-- 3.3 Lấy danh sách tất cả khách hàng 
CREATE PROCEDURE GetAllCustomers
AS
BEGIN
    SELECT CustomerID, FullName, Email, PhoneNumber, LastVisitDate, TotalVisits, TotalSpent, CustomerRank
    FROM Customers
    ORDER BY FullName
END
GO

-- 3.4 Tìm kiếm khách hàng theo tên và số điện thoại
CREATE PROCEDURE GetCustomerByNameAndPhone
    @Name VARCHAR(50) = NULL,
    @PhoneNumber VARCHAR(15) = NULL
AS
BEGIN
    SELECT CustomerID, FullName, Email, PhoneNumber, LastVisitDate, TotalVisits
    FROM Customers
    WHERE (@Name IS NULL OR FullName LIKE '%' + @Name + '%')
      AND (@PhoneNumber IS NULL OR PhoneNumber LIKE '%' + @PhoneNumber + '%')
    ORDER BY FullName
END
GO

-- 3.5 Lấy thông tin khách hàng theo Id
CREATE PROCEDURE GetCustomerByID
    @CustomerID INT
AS
BEGIN
    SELECT CustomerID, FullName, Email, PhoneNumber, LastVisitDate, TotalVisits, TotalSpent, CustomerRank
    FROM Customers
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

-- 4.2. Cập nhật thông tin phiếu đặt bàn
CREATE PROCEDURE UpdateReservation
    @ReservationID int,
    @TableID int,
    @ReservationTime datetime,
    @ComingTime datetime,
    @NumberOfGuests int,
    @Status varchar(20)
AS
BEGIN
    UPDATE Reservations
    SET TableID = @TableID,
        ReservationTime = @ReservationTime,
        ComingTime = @ComingTime,
        NumberOfGuests = @NumberOfGuests,
        Status = @Status
    WHERE ReservationID = @ReservationID
END
GO

-- 4.3. Lấy phiếu đặt bàn bằng số điện thoại khách hàng trong tương lai
CREATE PROCEDURE GetUpcomingReservationsByPhone
    @PhoneNumber VARCHAR(15)
AS
BEGIN
    SELECT r.ReservationID, r.CustomerID, r.TableID, r.ReservationTime, r.ComingTime, r.NumberOfGuests, r.Status
    FROM Reservations r
    JOIN Customers c ON r.CustomerID = c.CustomerID
    WHERE c.PhoneNumber = @PhoneNumber
      AND r.ComingTime >= GETDATE()
    ORDER BY r.ComingTime
END
GO

-- 4.4. Lấy danh sách tất cả các phiếu đặt bàn (ComingTime trong tương lai)
CREATE PROCEDURE GetAllUpcomingReservations
AS
BEGIN
    SELECT ReservationID, CustomerID, TableID, ReservationTime, ComingTime, NumberOfGuests
    FROM Reservations
    WHERE ComingTime >= GETDATE()
    ORDER BY ComingTime
END
GO

-- 4.5 Lấy phiếu đặt bàn theo ReservationID
CREATE PROCEDURE GetReservationByID
    @ReservationID INT
AS
BEGIN
    SELECT ReservationID, CustomerID, TableID, ReservationTime, ComingTime, NumberOfGuests, Status
    FROM Reservations
    WHERE ReservationID = @ReservationID
END
GO

-- 4.6 Lấy phiếu đặt bàn theo số điện thoại khách hàng
CREATE PROCEDURE GetReservationsByPhoneNumber
    @PhoneNumber VARCHAR(15)
AS
BEGIN
    SELECT r.ReservationID, r.CustomerID, r.TableID, r.ReservationTime, r.ComingTime, r.NumberOfGuests, r.Status
    FROM Reservations r
    JOIN Customers c ON r.CustomerID = c.CustomerID
    WHERE c.PhoneNumber = @PhoneNumber
    ORDER BY r.ComingTime
END
GO

-- 4.7 Lấy phiếu đặt bàn theo ngày
CREATE PROCEDURE GetReservationsByDate
    @Date DATE
AS
BEGIN
    SELECT ReservationID, CustomerID, TableID, ReservationTime, ComingTime, NumberOfGuests, Status
    FROM Reservations
    WHERE CAST(ComingTime AS DATE) = @Date
    ORDER BY ComingTime
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

-- 5.5. Lấy danh sách các order chưa hoàn thành (TimeCheckout IS NULL)
CREATE PROCEDURE GetAllPendingOrders
AS
BEGIN
    SELECT o.OrderID, o.ReservationID, o.TableID, o.OrderTime, o.TotalAmount, o.NumberOfGuests, o.CustomerID
    FROM Orders o
    WHERE o.TimeCheckout IS NULL
    ORDER BY o.OrderTime
END
GO

-- 5.6. Xóa Order cho trường hợp khách hủy đặt bàn
CREATE PROCEDURE DeleteOrder
    @OrderID INT
 AS
 BEGIN
    DELETE FROM Orders
    WHERE OrderID = @OrderID
END
GO

-- 5.7. Lấy danh sách order theo tableID và trạng thái chưa thanh toán
CREATE PROCEDURE GetOrdersByTableIDAndPendingStatus
    @TableID INT
AS
BEGIN
    SELECT o.OrderID, o.ReservationID, o.TableID, o.OrderTime, o.TotalAmount, o.NumberOfGuests, o.CustomerID
    FROM Orders o
    WHERE o.TableID = @TableID AND o.TimeCheckout IS NULL
    ORDER BY o.OrderTime
END
GO

-- 5.8 Lấy danh sách order theo ReservationID
CREATE PROCEDURE GetOrdersByReservationID
    @ReservationID INT
AS
BEGIN
    SELECT o.OrderID, o.ReservationID, o.TableID, o.OrderTime,
              o.TotalAmount, o.NumberOfGuests, o.CustomerID
    FROM Orders o
    WHERE o.ReservationID = @ReservationID
    ORDER BY o.OrderTime
END
GO

-- 5.9 Lấy chi tiết order theo OrderID( có tên món và giá)
CREATE PROCEDURE GetOrderDetailsWithFoodInfoByOrderID
    @OrderID INT
AS
BEGIN
    SELECT f.FoodName, od.Quantity, f.Price
    FROM OrderDetails od
    JOIN Foods f ON od.FoodID = f.FoodID
    WHERE od.OrderID = @OrderID
END
GO

-- 5.10 Lấy các chi tiết order đã hoàn thành theo OrderID
CREATE PROCEDURE GetCompletedOrderDetailsByOrderID
    @OrderID INT
AS
BEGIN
    SELECT OrderDetailID, OrderID, FoodID, Quantity, Notes, OrderStatus
    FROM OrderDetails
    WHERE OrderID = @OrderID AND OrderStatus = 'Completed'
    ORDER BY OrderDetailID
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

-- 6.4. Lấy danh sách tất cả nhân viên
CREATE PROCEDURE GetAllEmployees
AS
BEGIN
    SELECT FullName, PhoneNumber, Email, Position, HireDate
    FROM Employees
    ORDER BY FullName
END
GO

-- 6.5 Tìm kiếm nhân viên theo tên và số điện thoại
CREATE PROCEDURE GetEmployeesByNameAndPhone
    @FullName VARCHAR(50) = NULL,
    @PhoneNumber VARCHAR(15) = NULL
AS
BEGIN
    SELECT FullName, PhoneNumber, Email, Position, HireDate
    FROM Employees
    WHERE (@FullName IS NULL OR FullName LIKE '%' + @FullName + '%')
      AND (@PhoneNumber IS NULL OR PhoneNumber LIKE '%' + @PhoneNumber + '%')
    ORDER BY FullName
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

-- 7.4. Kiểm tra đăng nhập
CREATE PROCEDURE ValidateUserLogin
    @Username VARCHAR(50),
    @PasswordHash VARCHAR(255)
AS
BEGIN
    SELECT COUNT(*) AS UserCount
    FROM Users
    WHERE Username = @Username AND PasswordHash = @PasswordHash
END
GO

-- 7.6 Kiểm tra username đã tồn tại
CREATE PROCEDURE CheckUsernameExists
    @Username VARCHAR(50)
AS
BEGIN
    SELECT COUNT(*) AS UserCount
    FROM Users
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