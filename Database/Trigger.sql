/* =============================================
   FILE: DATABASE TRIGGERS
   MÔ TẢ: Các trigger tự động xử lý logic nghiệp vụ
   NGÀY CẬP NHẬT: 2025
   ============================================= */

-- =============================================
-- NHÓM 1: TRIGGERS TRÊN BẢNG ORDERS
-- =============================================

-- 1.1. Cập nhật lịch sử khách hàng khi có đơn hàng mới
-- Chức năng: Tăng tổng số lần ghé (TotalVisits) và cập nhật ngày ghé gần nhất (LastVisitDate)
CREATE TRIGGER trg_UpdateCustomerTotalVisits_and_LastVisitDate
ON Orders
AFTER INSERT
AS
BEGIN
    DECLARE @CustomerID int
    SELECT @CustomerID = CustomerID FROM inserted

    UPDATE Customers
    SET TotalVisits = TotalVisits + 1,
        LastVisitDate = GETDATE()
    WHERE CustomerID = @CustomerID
END
GO

-- 1.2. Cập nhật tổng chi tiêu của khách hàng khi thanh toán
-- Chức năng: Cộng dồn TotalSpent vào bảng Customers khi đơn hàng có TimeCheckout
CREATE TRIGGER trg_UpdateCustomerTotalSpent
ON Orders
AFTER UPDATE
AS
BEGIN
    DECLARE @CustomerID int
    DECLARE @TotalAmount decimal(10, 2)
    DECLARE @TimeCheckout datetime
    
    SELECT @CustomerID = CustomerID, 
           @TotalAmount = TotalAmount, 
           @TimeCheckout = TimeCheckout 
    FROM inserted
    
    -- Chỉ thực hiện nếu đơn hàng vừa được cập nhật TimeCheckout (tức là đã thanh toán)
    IF @TimeCheckout IS NOT NULL
    BEGIN
        UPDATE Customers
        SET TotalSpent = TotalSpent + @TotalAmount
        WHERE CustomerID = @CustomerID
    END
END
GO

-- 1.3. Tính lại tổng tiền đơn hàng (TotalAmount) khi thanh toán
-- Chức năng: Chốt sổ số tiền cuối cùng dựa trên các món đã có khi checkout
CREATE TRIGGER trg_orders_totalamount_on_checkout 
ON Orders
AFTER UPDATE
AS
BEGIN
    DECLARE @OrderID int
    DECLARE @TotalAmount decimal(10, 2)
    DECLARE @TimeCheckout datetime

    SELECT @OrderID = OrderID, @TimeCheckout = TimeCheckout FROM inserted

    IF @TimeCheckout IS NOT NULL
    BEGIN
        -- Chỉ tính tổng tiền các món có trạng thái 'Completed'
        SELECT @TotalAmount = SUM(F.Price * OD.Quantity)
        FROM OrderDetails OD
            JOIN Foods F ON OD.FoodID = F.FoodID
        WHERE OD.OrderID = @OrderID

        UPDATE Orders
        SET TotalAmount = ISNULL(@TotalAmount, 0)
        WHERE OrderID = @OrderID
    END
END 
GO

-- =============================================
-- NHÓM 2: TRIGGERS TRÊN BẢNG ORDER DETAILS
-- =============================================

-- 2.1. Tự động tính tổng tiền tạm tính của đơn hàng
-- Chức năng: Khi thêm/sửa chi tiết món, cập nhật lại TotalAmount của bảng Orders.
CREATE TRIGGER trg_orders_totalamount 
ON OrderDetails
AFTER INSERT, UPDATE
AS
BEGIN
    DECLARE @OrderID int
    DECLARE @TotalAmount decimal(10, 2)
    
    -- Lấy OrderID từ dòng dữ liệu vừa tác động
    SELECT @OrderID = OrderID FROM inserted
    -- Nếu là thao tác delete (inserted rỗng) thì lấy từ deleted (trường hợp mở rộng sau này)
    IF @OrderID IS NULL SELECT @OrderID = OrderID FROM deleted

    -- Tính tổng tiền
    SELECT @TotalAmount = SUM(F.Price * OD.Quantity)
    FROM OrderDetails OD
        JOIN Foods F ON OD.FoodID = F.FoodID
    WHERE OD.OrderID = @OrderID

    -- Cập nhật vào bảng cha Orders
    UPDATE Orders
    SET TotalAmount = ISNULL(@TotalAmount, 0)
    WHERE OrderID = @OrderID
END
GO

-- =============================================
-- NHÓM 3: TRIGGERS TRÊN BẢNG CUSTOMERS
-- =============================================

-- 3.1. Cập nhật hạng thành viên (Rank)
-- Chức năng: Tự động xét hạng (Silver, Gold, Platinum) dựa trên TotalSpent
-- Quy tắc: Platinum >= 50tr, Gold >= 10tr, Silver >= 2tr
CREATE TRIGGER trg_UpdateCustomerRank
ON Customers
AFTER UPDATE
AS
BEGIN
    -- Chỉ chạy logic nếu cột TotalSpent bị thay đổi
    IF UPDATE(TotalSpent)
    BEGIN
        DECLARE @CustomerID int
        DECLARE @TotalSpent decimal(15, 2)

        SELECT @CustomerID = CustomerID, @TotalSpent = TotalSpent FROM inserted

        DECLARE @NewRank varchar(20)
        SET @NewRank = 'Regular' -- Mặc định

        IF @TotalSpent >= 50000000 -- 50 triệu
            SET @NewRank = 'Platinum'
        ELSE IF @TotalSpent >= 10000000 -- 10 triệu
            SET @NewRank = 'Gold'
        ELSE IF @TotalSpent >= 2000000 -- 2 triệu
            SET @NewRank = 'Silver'

        UPDATE Customers
        SET CustomerRank = @NewRank
        WHERE CustomerID = @CustomerID
    END
END
GO