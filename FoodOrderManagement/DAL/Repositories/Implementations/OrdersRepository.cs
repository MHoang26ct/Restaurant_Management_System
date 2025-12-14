using FoodOrderManagement.DAL.Helper;
using FoodOrderManagement.DAL.Models.Entities;
using FoodOrderManagement.DAL.Repositories.Interfaces;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrderManagement.DAL.Repositories.Implementations {
    public class OrdersRepository : IOrdersRepository
    {
        private readonly DatabaseHelper _db = new DatabaseHelper();

        //
        private Orders Mapper(SqlDataReader reader)
        {
            return new Orders
            {
                // Cột 0: ID (Luôn có)
                Id = reader.GetInt32(0),

                // 👇 SỬA CÁC DÒNG DƯỚI ĐÂY 👇

                // Cột 1: ReservationId (Nếu Null trả về 0)
                ReservationId = reader.IsDBNull(1) ? 0 : reader.GetInt32(1),

                // Cột 2: TableId
                TableId = reader.IsDBNull(2) ? 0 : reader.GetInt32(2),

                // Cột 3: OrderTime (Thường không Null)
                OrderTime = reader.GetDateTime(3),

                // ⚠️ Cột 4: TotalAmount (HAY BỊ LỖI NHẤT -> Nếu Null trả về 0)
                TotalAmount = reader.IsDBNull(4) ? 0 : reader.GetDecimal(4),

                // Cột 5: NumberOfGuests
                NumberOfGuests = reader.IsDBNull(5) ? 1 : reader.GetInt32(5),

                // Cột 6: CustomerId
                CustomerId = reader.IsDBNull(6) ? 0 : reader.GetInt32(6),

                // Cột 7: CheckoutTime (Nếu Null trả về null)
                CheckoutTime = reader.IsDBNull(7) ? (DateTime?)null : reader.GetDateTime(7)
            };
        }

        // Thêm order mới, trả về OrderID vừa tạo để dùng cho việc thêm OrderDetail sau đó
        public async Task<int> AddOrderAsync(Orders order)
        {
            var outputIdParam = new SqlParameter("@NewOrderID", System.Data.SqlDbType.Int)
            {
                Direction = System.Data.ParameterDirection.Output
            };
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@ReservationId", order.ReservationId == 0 ? (object)DBNull.Value : order.ReservationId),
                new SqlParameter("@TableId", order.TableId),
                new SqlParameter("@OrderTime", (order.OrderTime == DateTime.MinValue) ? DateTime.Now : order.OrderTime),
                new SqlParameter("@CustomerID", order.CustomerId),
                new SqlParameter("@NumberOfGuests", order.NumberOfGuests = 1),
                outputIdParam
            };
            await _db.ExecuteNonQueryAsync("AddOrder", parameters);
            return (int)outputIdParam.Value;
        }

        // Truy xuất order theo reservationID
        public async Task<List<Orders>> GetOrdersByReservationIdAsync(int reservationId) {
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@ReservationID", reservationId)
            };
            return await _db.GetListAsync("GetOrdersByReservationID", Mapper, parameters);
        }

        // Truy xuất order theo số bàn (thường là order đang pending)
        public async Task<Orders?> GetOrdersByTableIdAsync(int tableId) {
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@TableID", tableId)
            };
            return await _db.QuerySingleAsync("GetOrdersByTableIDAndPendingStatus", Mapper, parameters);
        }

        // Cập nhật thời gian thanh toán
        public async Task UpdateTimeCheckoutAsync(int orderId, DateTime? TimeCheckout) {
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@OrderID", orderId),
                new SqlParameter("@CheckoutTime", TimeCheckout.HasValue ? (object)TimeCheckout.Value : DBNull.Value)
            };
            await _db.ExecuteNonQueryAsync("UpdateOrderCheckoutTime", parameters);
        }

        // Lấy danh sách order chưa thanh toán
        public async Task<List<Orders>> GetAllUnpaidOrdersAsync() {
            return await _db.GetListAsync("GetAllPendingOrders", Mapper);
        }

        // Xóa order theo mã order (cho trường hợp khách hủy đặt bàn)
        public async Task DeleteOrderByIdAsync(int orderId) {
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@OrderID", orderId)
            };
            await _db.ExecuteNonQueryAsync("DeleteOrder", parameters);
        }

        // Lấy các order hoàn thành để hiển thị trong quản lý order
        public async Task<List<Orders>> GetAllCompletedOrdersAsync() {
            return await _db.GetListAsync("GetAllCompletedOrders", Mapper);
        }

        // Lấy tất cả order
        public async Task<List<Orders>> GetAllOrdersAsync() {
            return await _db.GetListAsync("GetAllOrders", Mapper);
        }
        public async Task UpdateOrderTotalAsync(int orderId, decimal total)
        {
            var parameters = new SqlParameter[]
            {
        new SqlParameter("@OrderID", orderId),
        new SqlParameter("@TotalAmount", total)
            };
            // Đảm bảo bạn có câu query hoặc SP tương ứng
            // Ví dụ Query: "UPDATE Orders SET TotalAmount = @TotalAmount WHERE Id = @OrderID"
            await _db.ExecuteNonQueryAsync("UpdateOrderTotal", parameters);
        }
    }
}
