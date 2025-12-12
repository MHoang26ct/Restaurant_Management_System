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
        private Orders Mapper(SqlDataReader reader) {
            return new Orders {
                Id = reader.GetInt32(0),
                ReservationId = reader.GetInt32(1),
                TableId = reader.GetInt32(2),
                OrderTime = reader.GetDateTime(3),
                TotalAmount = reader.GetDecimal(4),
                NumberOfGuests = reader.GetInt32(5),
                CustomerId = reader.GetInt32(6)
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
                new SqlParameter("@ReservationId", order.ReservationId),
                new SqlParameter("@TableId", order.TableId),
                new SqlParameter("@OrderTime", order.OrderTime),
                new SqlParameter("@CustomerID", order.CustomerId),
                new SqlParameter("@NumberOfGuests", order.NumberOfGuests),
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
        public async Task UpdateCheckoutTimeAsync(int orderId, DateTime checkoutTime) {
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@OrderID", orderId),
                new SqlParameter("@CheckoutTime", checkoutTime)
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
    }
}
