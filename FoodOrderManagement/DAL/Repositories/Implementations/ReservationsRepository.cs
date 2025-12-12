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

        // Thêm đặt bàn mới và trả về ID đặt bàn mới tạo
        public async Task<int> AddReservationAsync(Reservations reservation)
        {
            using SqlConnection connection = new SqlConnection(_connectionString);
            using SqlCommand command = new SqlCommand("AddReservation", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@CustomerID", reservation.customerId);
            command.Parameters.AddWithValue("@TableID", reservation.TableId);
            command.Parameters.AddWithValue("@ReservationTime", reservation.ReservationTime);
            command.Parameters.AddWithValue("@ComingTime", reservation.ComingTime);
            command.Parameters.AddWithValue("@NumberOfGuests", reservation.NumberOfGuests);

            SqlParameter outputIdParam = new SqlParameter("@NewReservationID", System.Data.SqlDbType.Int)
            {
                Direction = System.Data.ParameterDirection.Output
            };
            command.Parameters.Add(outputIdParam);

            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();

            return (int)outputIdParam.Value;
        }

        // Truy vấn bằng mã đặt bàn
        public async Task<Reservations?> GetReservationByReservationIdAsync(int reservationId)
        {
            using SqlConnection connection = new SqlConnection(_connectionString);
            using SqlCommand command = new SqlCommand("GetReservationByReservationId", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@ReservationID", reservationId);

            await connection.OpenAsync();
            using SqlDataReader reader = await command.ExecuteReaderAsync();

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

            return null;
        }

        // Truy vấn theo ngày để tránh đặt trùng
        public async Task<List<Reservations>> GetReservationsByDateAsync(DateTime date)
        {
            var reservations = new List<Reservations>();
            using SqlConnection connection = new SqlConnection(_connectionString);
            using SqlCommand command = new SqlCommand("GetReservationsByDate", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@Date", date.Date);

            await connection.OpenAsync();
            using SqlDataReader reader = await command.ExecuteReaderAsync();

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

            return reservations;
        }

        // Truy xuất phiếu đặt bàn theo số điện thoại trong tương lai
        public async Task<List<Reservations>> GetUpcomingReservationsByPhoneNumberAsync(string phoneNumber)
        {
            var reservations = new List<Reservations>();
            using SqlConnection connection = new SqlConnection(_connectionString);
            using SqlCommand command = new SqlCommand("GetUpcomingReservationsByPhoneNumber", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@PhoneNumber", phoneNumber);

            await connection.OpenAsync();
            using SqlDataReader reader = await command.ExecuteReaderAsync();

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

            return reservations;
        }

        // Truy xuất phiếu đặt bàn theo số điện thoại (bao gồm cả quá khứ)
        public async Task<List<Reservations>> GetReservationsByPhoneNumberAsync(string phoneNumber)
        {
            var reservations = new List<Reservations>();
            using SqlConnection connection = new SqlConnection(_connectionString);
            using SqlCommand command = new SqlCommand("GetReservationsByPhoneNumber", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@PhoneNumber", phoneNumber);

            await connection.OpenAsync();
            using SqlDataReader reader = await command.ExecuteReaderAsync();

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

            return reservations;
        }

        // Lấy danh sách tất cả đặt bàn (thời gian đặt bàn trong tương lai)
        public async Task<List<Reservations>> GetAllUpcomingReservationsAsync()
        {
            var reservations = new List<Reservations>();
            using SqlConnection connection = new SqlConnection(_connectionString);
            using SqlCommand command = new SqlCommand("GetAllUpcomingReservations", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;

            await connection.OpenAsync();
            using SqlDataReader reader = await command.ExecuteReaderAsync();

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

            return reservations;
        }

        // Cập nhật thông tin đặt bàn (dùng luôn cho hủy đặt bàn - thay đổi trạng thái)
        public async Task<bool> UpdateReservationAsync(Reservations reservation)
        {
            using SqlConnection connection = new SqlConnection(_connectionString);
            using SqlCommand command = new SqlCommand("UpdateReservation", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@ReservationID", reservation.Id);
            command.Parameters.AddWithValue("@TableID", reservation.TableId);
            command.Parameters.AddWithValue("@ReservationTime", reservation.ReservationTime);
            command.Parameters.AddWithValue("@ComingTime", reservation.ComingTime);
            command.Parameters.AddWithValue("@NumberOfGuests", reservation.NumberOfGuests);
            command.Parameters.AddWithValue("@Status", reservation.Status);

            await connection.OpenAsync();
            int rowsAffected = await command.ExecuteNonQueryAsync();

            return rowsAffected > 0;
        }
    }
}
