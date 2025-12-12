using FoodOrderManagement.DAL.Helper;
using FoodOrderManagement.DAL.Models.Entities;
using FoodOrderManagement.DAL.Repositories;
using FoodOrderManagement.DAL.Repositories.Interfaces;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrderManagement.DAL.Repositories.Implementations {
    internal class ReservationsRepository : IReservationsRepository
    {
        private readonly DatabaseHelper _db = new DatabaseHelper();

        //
        private Reservations Mapper(SqlDataReader reader)
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

        // Thêm đặt bàn mới và trả về ID đặt bàn mới tạo
        public async Task<int> AddReservationAsync(Reservations reservation)
        {
            var outputIdParam = new SqlParameter("@NewReservationID", System.Data.SqlDbType.Int)
            {
                Direction = System.Data.ParameterDirection.Output
            };
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@CustomerID", reservation.customerId),
                new SqlParameter("@TableID", reservation.TableId),
                new SqlParameter("@ReservationTime", reservation.ReservationTime),
                new SqlParameter("@ComingTime", reservation.ComingTime),
                new SqlParameter("@NumberOfGuests", reservation.NumberOfGuests),
                outputIdParam
            };
            await _db.ExecuteNonQueryAsync("AddReservation", parameters);
            return (int)outputIdParam.Value;
        }

        // Truy vấn bằng mã đặt bàn
        public async Task<Reservations?> GetReservationByReservationIdAsync(int reservationId)
        {
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@ReservationID", reservationId)
            };
            return await _db.QuerySingleAsync("GetReservationByReservationId", Mapper, parameters);
        }

        // Truy vấn theo ngày để tránh đặt trùng
        public async Task<List<Reservations>> GetReservationsByDateAsync(DateTime date)
        {
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@Date", date.Date)
            };
            return await _db.GetListAsync("GetReservationsByDate", Mapper, parameters);
        }

        // Truy xuất phiếu đặt bàn theo số điện thoại trong tương lai
        public async Task<List<Reservations>> GetUpcomingReservationsByPhoneNumberAsync(string phoneNumber)
        {
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@PhoneNumber", phoneNumber)
            };
            return await _db.GetListAsync("GetUpcomingReservationsByPhoneNumber", Mapper, parameters);
        }

        // Truy xuất phiếu đặt bàn theo số điện thoại (bao gồm cả quá khứ)
        public async Task<List<Reservations>> GetReservationsByPhoneNumberAsync(string phoneNumber)
        {
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@PhoneNumber", phoneNumber)
            };
            return await _db.GetListAsync("GetReservationsByPhoneNumber", Mapper, parameters);
        }

        // Lấy danh sách tất cả đặt bàn (thời gian đặt bàn trong tương lai)
        public async Task<List<Reservations>> GetAllUpcomingReservationsAsync()
        {
            return await _db.GetListAsync("GetAllUpcomingReservations", Mapper);
        }

        // Cập nhật thông tin đặt bàn (dùng luôn cho hủy đặt bàn - thay đổi trạng thái)
        public async Task<bool> UpdateReservationAsync(Reservations reservation)
        {
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@ReservationID", reservation.Id),
                new SqlParameter("@TableID", reservation.TableId),
                new SqlParameter("@ReservationTime", reservation.ReservationTime),
                new SqlParameter("@ComingTime", reservation.ComingTime),
                new SqlParameter("@NumberOfGuests", reservation.NumberOfGuests)
            };
            int rowsAffected = await _db.ExecuteNonQueryAsync("UpdateReservation", parameters);
            return rowsAffected > 0;
        }
    }
}
