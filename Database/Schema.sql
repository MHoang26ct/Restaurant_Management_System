/* =============================================
   FILE: SCHEMA CREATION
   MÔ TẢ: Khởi tạo Database và các Bảng (Tables) cho hệ thống Quản lý Nhà hàng
   ============================================= */

-- 1. Tạo và Sử dụng Database
CREATE DATABASE RestaurantManagementDB
GO
USE RestaurantManagementDB
GO

-- 2. Tạo Bảng Users (Tài khoản người dùng)
CREATE TABLE Users (
    Username varchar(50) PRIMARY KEY,
    PasswordHash varchar(255) NOT NULL,
    UserRole bit NOT NULL -- 0: Nhân viên, 1: Quản lý (Hoặc định nghĩa theo chuẩn của hệ thống)
)
GO

-- 3. Tạo Bảng Employees (Thông tin nhân viên)
CREATE TABLE Employees (
    FullName varchar(50) NOT NULL,
    PhoneNumber varchar(15) NOT NULL UNIQUE,
    Email varchar(100) NOT NULL UNIQUE,
    HireDate datetime,
    Position varchar(50),
    PRIMARY KEY (FullName, PhoneNumber) -- Khóa chính kết hợp
)
GO

-- 4. Tạo Bảng Customers (Thông tin khách hàng)
CREATE TABLE Customers (
    CustomerID int IDENTITY(1,1) PRIMARY KEY,  
    FullName varchar(50) NOT NULL,
    Email varchar(100) NOT NULL UNIQUE,
    PhoneNumber varchar(15),
    LastVisitDate datetime NULL, -- Ngày ghé thăm gần nhất
    TotalVisits int DEFAULT 0,   -- Tổng số lần ghé
    TotalSpent decimal(15, 2) DEFAULT 0, -- Tổng chi tiêu
    CustomerRank varchar(20) DEFAULT 'Regular' -- Hạng thành viên
)
GO

-- 5. Tạo Bảng Tables (Thông tin bàn ăn)
CREATE TABLE Tables (
    TableID int IDENTITY(1,1) PRIMARY KEY,
    Capacity int NOT NULL,       -- Sức chứa tối đa
    TableStatus varchar(20) NOT NULL, -- Trạng thái (Available, Occupied, Reserved)
    OpenTime datetime              -- Thời điểm bàn bắt đầu được sử dụng
)
GO

-- 6. Tạo Bảng Foods (Thực đơn món ăn)
CREATE TABLE Foods (
    FoodID int IDENTITY(1,1) PRIMARY KEY,  
    FoodName varchar(100) NOT NULL,
    Price decimal(10, 2) NOT NULL
)
GO

-- 7. Tạo Bảng Reservations (Phiếu đặt bàn)
CREATE TABLE Reservations (
    ReservationID int IDENTITY(1,1) PRIMARY KEY,  
    CustomerID int NOT NULL,
    TableID int NOT NULL,
    ReservationTime datetime NOT NULL, -- Thời điểm khách đặt
    ComingTime datetime NOT NULL,      -- Thời điểm khách dự kiến đến
    NumberOfGuests int NOT NULL,
    status varchar(20) NOT NULL DEFAULT 'Pending',
    FOREIGN KEY (CustomerID) REFERENCES Customers(CustomerID),
    FOREIGN KEY (TableID) REFERENCES Tables(TableID)
)
GO

-- 8. Tạo Bảng Orders (Đơn hàng)
CREATE TABLE Orders (
    OrderID int IDENTITY(1,1) PRIMARY KEY,  
    ReservationID int NULL,    -- Có thể NULL nếu là khách vãng lai
    TableID int NOT NULL,
    CustomerID int NOT NULL,
    OrderTime datetime NOT NULL,
    TotalAmount decimal(10, 2) NOT NULL, -- Tổng tiền (sẽ được Trigger tự động tính toán)
    NumberOfGuests int NOT NULL,
    TimeCheckout datetime NULL,          -- Thời điểm thanh toán
    FOREIGN KEY (TableID) REFERENCES Tables(TableID),
    FOREIGN KEY (ReservationID) REFERENCES Reservations(ReservationID),
    FOREIGN KEY (CustomerID) REFERENCES Customers(CustomerID)
)
GO

-- 9. Tạo Bảng OrderDetails (Chi tiết đơn hàng)
CREATE TABLE OrderDetails (
    OrderDetailID int IDENTITY(1,1) PRIMARY KEY,
    OrderID int NOT NULL,
    FoodID int NOT NULL,
    Quantity int NOT NULL,
    Notes varchar(255),
    OrderStatus varchar(20) NOT NULL, -- Trạng thái món (Pending, In Progress, Completed, Cancelled)
    FOREIGN KEY (OrderID) REFERENCES Orders(OrderID),
    FOREIGN KEY (FoodID) REFERENCES Foods(FoodID)
)
GO

-- =============================================
-- 10. Các Ràng buộc (Constraints) Kiểm tra Dữ liệu
-- =============================================

-- Kiểm tra Trạng thái Bàn
ALTER TABLE Tables ADD CONSTRAINT CHK_TableStatus
    CHECK (TableStatus IN ('Available', 'Occupied', 'Reserved'))
GO

-- Kiểm tra Trạng thái Chi tiết Đơn hàng
ALTER TABLE OrderDetails ADD CONSTRAINT CHK_OrderStatus
    CHECK (OrderStatus IN ('Pending', 'In Progress', 'Completed', 'Cancelled'))
GO

-- Kiểm tra Vai trò Người dùng (0 hoặc 1)
ALTER TABLE Users ADD CONSTRAINT CHK_UserRole
    CHECK (UserRole IN (0, 1))
GO

-- Kiểm tra Giá Món ăn (Phải >= 0)
ALTER TABLE Foods ADD CONSTRAINT CHK_Food_Price
    CHECK (Price >= 0)
GO

-- Kiểm tra Tổng tiền Đơn hàng (Phải >= 0)
ALTER TABLE Orders ADD CONSTRAINT CHK_Orders_TotalAmount
    CHECK (TotalAmount >= 0)
GO

-- Kiểm tra Số lượng Khách đặt bàn (Phải > 0)
ALTER TABLE Reservations ADD CONSTRAINT CHK_NumberOfGuests
    CHECK (NumberOfGuests > 0)
GO

-- Kiểm tra Sức chứa Bàn (Phải > 0)
ALTER TABLE Tables ADD CONSTRAINT CK_Table_Capacity
    CHECK (Capacity > 0)
GO

-- Kiểm tra Số lượng Món ăn trong chi tiết đơn hàng (Phải > 0)
ALTER TABLE OrderDetails ADD CONSTRAINT CK_OrderDetails_Quantity
    CHECK (Quantity > 0)
GO

-- Kiểm tra Số lượng Khách trong Đơn hàng (Phải > 0)
ALTER TABLE Orders ADD CONSTRAINT CK_Orders_NumberOfGuests
    CHECK (NumberOfGuests > 0)
GO

-- Kiểm tra Hạng Khách hàng
ALTER TABLE Customers ADD CONSTRAINT CHK_CustomerRank
    CHECK (CustomerRank IN ('Regular', 'Silver', 'Gold', 'Platinum'))
GO

-- Kiểm tra Trạng thái Đặt bàn
ALTER TABLE Reservations ADD CONSTRAINT CHK_ReservationStatus
    CHECK (status IN ('Pending', 'Cancelled', 'Completed'))