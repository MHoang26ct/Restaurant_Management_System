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

        // Thêm đặt bàn mới và trả về ID đặt bàn mới tạo để thực hiện các order nếu cần
        public async Task<int> AddReservationAsync(Reservations reservation)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand("AddReservation", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@CustomerID", reservation.customerId);
                    command.Parameters.AddWithValue("@TableID", reservation.TableId);
                    command.Parameters.AddWithValue("@ReservationTime", reservation.ReservationTime);
                    command.Parameters.AddWithValue("@ComingTime", reservation.ComingTime);
                    command.Parameters.AddWithValue("@NumberOfGuests", reservation.NumberOfGuests);
                    var outputIdParam = new SqlParameter("@NewReservationID", System.Data.SqlDbType.Int)
                    {
                        Direction = System.Data.ParameterDirection.Output
                    };
                    command.Parameters.Add(outputIdParam);
                    await command.ExecuteNonQueryAsync();
                    return (int)outputIdParam.Value;
                }
            }
        }

        // Truy vấn bằng mã đặt bàn
        public async Task<Reservations?> GetReservationByReservationIdAsync(int reservationId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand("SELECT * FROM orderDetail WHERE reservationId = @reservationId", connection))
                {
                    command.Parameters.AddWithValue("@ReservationId", reservationId);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new Reservations
                            {
                                Id = reader.GetInt32(0),
                                customerId = reader.GetInt32(1),
                                TableId = reader.GetInt32(2),
                                ReservationTime = reader.GetDateTime(3),
                                ComingTime = reader.GetDateTime(4),
                                NumberOfGuests = reader.GetInt32(5)
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

        // Truy vấn bằng mã khách hàng và thời gian đến 
        public async Task<List<Reservations>> GetReservationsByCustomerIdAndComingTimeAsync(int customerId, DateTime currentTime)
        {
            var reservations = new List<Reservations>();
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand("SELECT * FROM Reservations WHERE CustomerId = @CustomerId AND ComingTime >= @CurrentTime", connection))
                {
                    command.Parameters.AddWithValue("@CustomerId", customerId);
                    command.Parameters.AddWithValue("@ComingTime", currentTime);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            reservations.Add(new Reservations
                            {
                                Id = reader.GetInt32(0),
                                customerId = reader.GetInt32(1),
                                TableId = reader.GetInt32(2),
                                ReservationTime = reader.GetDateTime(3),
                                ComingTime = reader.GetDateTime(4),
                                NumberOfGuests = reader.GetInt32(5)
                            });
                        }
                    }
                }
            }
            return reservations;
        }

        // Truy vấn theo ngày để tránh đặt trùng
        public async Task<List<Reservations>> GetReservationsByDateAsync(DateTime date)
        {
            var reservations = new List<Reservations>();
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand("SELECT * FROM Reservations WHERE CAST(ComingTime AS DATE) = @Date", connection))
                {
                    command.Parameters.AddWithValue("@Date", date.Date);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            reservations.Add(new Reservations
                            {
                                Id = reader.GetInt32(0),
                                customerId = reader.GetInt32(1),
                                TableId = reader.GetInt32(2),
                                ReservationTime = reader.GetDateTime(3),
                                ComingTime = reader.GetDateTime(4),
                                NumberOfGuests = reader.GetInt32(5)
                            });
                        }
                    }
                }
            }
            return reservations;
        }

        // Truy vấn đặt bàn theo mã khách hàng và thời gian đặt
        public async Task<List<Reservations>> GetReservationsByCustomerIdAndReservationTimeAsync(int customerId, DateTime reservationTime)
        {
            var reservations = new List<Reservations>();
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand("SELECT * FROM Reservations WHERE CustomerId = @CustomerId AND ReservationTime = @ReservationTime", connection))
                {
                    command.Parameters.AddWithValue("@CustomerId", customerId);
                    command.Parameters.AddWithValue("@ReservationTime", reservationTime);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            reservations.Add(new Reservations
                            {
                                Id = reader.GetInt32(0),
                                customerId = reader.GetInt32(1),
                                TableId = reader.GetInt32(2),
                                ReservationTime = reader.GetDateTime(3),
                                ComingTime = reader.GetDateTime(4),
                                NumberOfGuests = reader.GetInt32(5)
                            });
                        }
                    }
                }
            }
            return reservations;
        }

        // Lấy danh sách tất cả đặt bàn (thời gian đặt bàn trong tương lai)
        public async Task<List<Reservations>> GetAllUpcomingReservationsAsync()
        {
            var reservations = new List<Reservations>();
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand("GetAllUpcomingReservations", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            reservations.Add(new Reservations
                            {
                                Id = reader.GetInt32(0),
                                customerId = reader.GetInt32(1),
                                TableId = reader.GetInt32(2),
                                ReservationTime = reader.GetDateTime(3),
                                ComingTime = reader.GetDateTime(4),
                                NumberOfGuests = reader.GetInt32(5)
                            });
                        }
                    }
                }
            }
            return reservations;
        }
    }
}
