using FoodOrderManagement.DAL.Models.Entities;
using FoodOrderManagement.UI.Forms.ReservationManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrderManagement.DAL.Repositories.Interfaces {
    public interface IReservationsRepository {

        // Thêm đặt bàn mới và trả về ID đặt bàn mới tạo để thực hiện các order nếu cần
        public Task<int> AddReservationAsync(Reservations reservation);

        /// <summary>
        /// Truy xuất theo mã đặt bàn
        /// </summary>
        /// <param name="reservationId"></param>
        /// <returns></returns>
        public Task<Reservations?> GetReservationByReservationIdAsync(int reservationId);

        /// <summary>
        /// Truy xuất theo ngày để tránh đặt trùng
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public Task<List<Reservations>> GetReservationsByDateAsync(DateTime date);

        /// <summary>
        /// Truy xuât phiếu đặt bàn theo số điện thoại trong tương lai
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        public Task<List<Reservations>> GetUpcomingReservationsByPhoneNumberAsync(string phoneNumber);

        /// <summary>
        /// Truy xuất phiếu đặt bàn theo số điện thoại (bao gồm cả quá khứ)
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        public Task<List<Reservations>> GetReservationsByPhoneNumberAsync(string phoneNumber);

        /// <summary>
        /// Lấy danh sách tất cả đặt bàn (thời gian đặt bàn trong tương lai)
        /// </summary>
        /// <returns></returns>
        public Task<List<Reservations>> GetAllUpcomingReservationsAsync();

        /// <summary>
        /// Thay đổi thông tin đặt bàn (dùng luôn cho hủy đặt bàn)
        /// </summary>
        /// <param name="reservation"></param>
        /// <returns></returns>
        public Task<bool> UpdateReservationAsync(Reservations reservation);

        /// <summary>
        /// Lấy danh sách tất cả đặt bàn cho hiển thị
        /// </summary>
        /// <returns></returns>
        public Task<List<ReservationViewModel>> GetAllReservationsAsync();

        /// <summary>
        /// Xóa đặt bàn
        /// </summary>
        /// <param name="reservationId"></param>
        /// <returns></returns>
        public Task<bool> DeleteReservationAsync(int reservationId);

        /// <summary>
        /// Lấy đặt bàn sắp tới theo mã bàn
        /// </summary>
        /// <param name="tableId"></param>
        /// <returns></returns>
        public Task<Reservations> GetUpcomingReservationByTableIdAsync(int tableId);
    }
}
