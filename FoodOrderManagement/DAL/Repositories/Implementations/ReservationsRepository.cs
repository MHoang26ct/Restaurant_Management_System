using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodOrderManagement.DAL.Models.Entities;
using FoodOrderManagement.DAL.Repositories;
using Microsoft.Data.SqlClient;
using System.Configuration;
using FoodOrderManagement.DAL.Repositories.Interfaces;

namespace FoodOrderManagement.DAL.Repositories.Implementations {
    internal class ReservationsRepository : IReservationsRepository
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        // Thêm order mới, trả về OrderID vừa tạo để dùng cho việc thêm OrderDetail sau đó
        public async Task<int> AddOrderAsync(Orders order) {
            using SqlConnection connection = new SqlConnection(_connectionString);
            using SqlCommand command = new SqlCommand("AddOrder", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@ReservationId", order.ReservationId);
            command.Parameters.AddWithValue("@TableId", order.TableId);
            command.Parameters.AddWithValue("@OrderTime", order.OrderTime);
            command.Parameters.AddWithValue("@CustomerID", order.CustomerId);
            command.Parameters.AddWithValue("@NumberOfGuests", order.NumberOfGuests);

            SqlParameter outputIdParam = new SqlParameter("@NewOrderID", System.Data.SqlDbType.Int) {
                Direction = System.Data.ParameterDirection.Output
            };
            command.Parameters.Add(outputIdParam);

            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
            
            return (int)outputIdParam.Value;
        }

        // Truy xuất order theo reservationID
        public async Task<List<Orders>> GetOrdersByReservationIdAsync(int reservationId) {
            var orders = new List<Orders>();
            using SqlConnection connection = new SqlConnection(_connectionString);
            using SqlCommand command = new SqlCommand("GetOrdersByReservationID", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@ReservationID", reservationId);

            await connection.OpenAsync();
            using SqlDataReader reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync()) {
                orders.Add(new Orders {
                    Id = reader.GetInt32(0),
                    ReservationId = reader.GetInt32(1),
                    TableId = reader.GetInt32(2),
                    OrderTime = reader.GetDateTime(3),
                    TotalAmount = reader.GetDecimal(4),
                    NumberOfGuests = reader.GetInt32(5),
                    CustomerId = reader.GetInt32(6)
                });
            }
            
            return orders;
        }

        // Truy xuất order theo số bàn (thường là order đang pending)
        public async Task<Orders?> GetOrdersByTableIdAsync(int tableId) {
            using SqlConnection connection = new SqlConnection(_connectionString);
            using SqlCommand command = new SqlCommand("GetOrdersByTableIDAndPendingStatus", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@TableID", tableId);

            await connection.OpenAsync();
            using SqlDataReader reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync()) {
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
            
            return null;
        }

        // Cập nhật thời gian thanh toán
        public async Task UpdateCheckoutTimeAsync(int orderId, DateTime checkoutTime) {
            using SqlConnection connection = new SqlConnection(_connectionString);
            using SqlCommand command = new SqlCommand("UpdateOrderCheckoutTime", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@OrderID", orderId);
            command.Parameters.AddWithValue("@CheckoutTime", checkoutTime);

            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
        }

        // Lấy danh sách order chưa thanh toán
        public async Task<List<Orders>> GetAllUnpaidOrdersAsync() {
            var orders = new List<Orders>();
            using SqlConnection connection = new SqlConnection(_connectionString);
            using SqlCommand command = new SqlCommand("GetAllPendingOrders", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;

            await connection.OpenAsync();
            using SqlDataReader reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync()) {
                orders.Add(new Orders {
                    Id = reader.GetInt32(0),
                    ReservationId = reader.GetInt32(1),
                    TableId = reader.GetInt32(2),
                    OrderTime = reader.GetDateTime(3),
                    TotalAmount = reader.GetDecimal(4),
                    NumberOfGuests = reader.GetInt32(5),
                    CustomerId = reader.GetInt32(6)
                });
            }
            
            return orders;
        }

        // Xóa order theo mã order (cho trường hợp khách hủy đặt bàn)
        public async Task DeleteOrderByIdAsync(int orderId) {
            using SqlConnection connection = new SqlConnection(_connectionString);
            using SqlCommand command = new SqlCommand("DeleteOrder", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@OrderID", orderId);

            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
        }
    }
}
