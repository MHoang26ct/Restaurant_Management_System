using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodOrderManagement.DAL.Repositories.Interfaces;
using FoodOrderManagement.DAL.Models.Entities;
using Microsoft.Data.SqlClient;
using System.Configuration;

namespace FoodOrderManagement.DAL.Repositories.Implementations {
    public class OrdersRepository : IOrdersRepository
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        // Thêm order mới, trả về OrderID vừa tạo để dùng cho việc thêm OrderDetail sau đó
        public async Task<int> AddOrderAsync(Orders order)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand("AddOrder", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ReservationId", order.ReservationId);
                    command.Parameters.AddWithValue("@TableId", order.TableId);
                    command.Parameters.AddWithValue("@OrderTime", order.OrderTime);
                    command.Parameters.AddWithValue("@CustomerID", order.CustomerId);
                    command.Parameters.AddWithValue("@NumberOfGuests", order.NumberOfGuests);
                    var outputIdParam = new SqlParameter("@NewOrderID", System.Data.SqlDbType.Int)
                    {
                        Direction = System.Data.ParameterDirection.Output
                    };
                    command.Parameters.Add(outputIdParam);
                    await command.ExecuteNonQueryAsync();
                    return (int)outputIdParam.Value;
                }
            }
        }

        // Truy xuất order theo reservationID
        public async Task<List<Orders>> GetOrdersByReservationIdAsync(int reservationId)
        {
            var orders = new List<Orders>();
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand("SELECT * FROM Orders WHERE ReservationId = @ReservationId", connection))
                {
                    command.Parameters.AddWithValue("@ReservationId", reservationId);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            orders.Add(new Orders
                            {
                                Id = reader.GetInt32(0),
                                ReservationId = reader.GetInt32(1),
                                TableId = reader.GetInt32(2),
                                OrderTime = reader.GetDateTime(3),
                                TotalAmount = reader.GetDecimal(4)
                            });
                        }
                    }
                }
            }
            return orders;
        }

        // Truy xuất order theo số bàn
        public async Task<Orders?> GetOrdersByTableIdAsync(int tableId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand("SELECT * FROM Orders WHERE TableId = @TableId AND TimeCheckout IS NULL", connection))
                {
                    command.Parameters.AddWithValue("@TableId", tableId);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new Orders
                            {
                                Id = reader.GetInt32(0),
                                ReservationId = reader.GetInt32(1),
                                TableId = reader.GetInt32(2),
                                CustomerId = reader.GetInt32(7),
                                OrderTime = reader.GetDateTime(3),
                                TotalAmount = reader.GetDecimal(4),
                                NumberOfGuests = reader.GetInt32(5),
                                CheckoutTime = reader.IsDBNull(6) ? null : reader.GetDateTime(6)
                            };
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
        }

        // Cập nhật thời gian thanh toán
        public async Task UpdateCheckoutTimeAsync(int orderId, DateTime checkoutTime)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand("UpdateOrderCheckoutTime", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@OrderID", orderId);
                    command.Parameters.AddWithValue("@CheckoutTime", checkoutTime);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        // Lấy danh sách order chưa thanh toán
        public async Task<List<Orders>> GetAllUnpaidOrdersAsync()
        {
            var orders = new List<Orders>();
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand("GetAllPendingOrders", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            orders.Add(new Orders
                            {
                                Id = reader.GetInt32(0),
                                ReservationId = reader.GetInt32(1),
                                TableId = reader.GetInt32(2),
                                OrderTime = reader.GetDateTime(3),
                                TotalAmount = reader.GetDecimal(4),
                                NumberOfGuests = reader.GetInt32(5),
                                CustomerId = reader.GetInt32(6)
                            });
                        }
                    }
                }
            }
            return orders;
        }
    }
}
