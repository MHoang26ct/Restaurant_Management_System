using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectRestaurant.DAL.Models.Entities;

namespace ProjectRestaurant.DAL.Repositories.Interfaces {
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
        /// Truy xuất theo mã khách hàng và thời gian đến (thời gian đến lớn hơn hoặc bằng thời gian hiện tại) 
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="ComingTime"></param>
        /// <returns></returns>
        public Task<List<Reservations>> GetReservationsByCustomerIdAndComingTimeAsync(int customerId, DateTime ComingTime);

        /// <summary>
        /// Truy xuất theo ngày để tránh đặt trùng
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public Task<List<Reservations>> GetReservationsByDateAsync(DateTime date);

        /// <summary>
        /// Truy vấn đặt bàn theo mã khách hàng và thời gian đặt
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="reservationTime"></param>
        /// <returns></returns>
        public Task<List<Reservations>> GetReservationsByCustomerIdAndReservationTimeAsync(int customerId, DateTime reservationTime);
    }
}
