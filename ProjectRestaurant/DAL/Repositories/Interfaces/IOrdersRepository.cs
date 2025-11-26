using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectRestaurant.DAL.Models.Entities;

namespace ProjectRestaurant.DAL.Repositories.Interfaces {
    public interface IOrdersRepository {

        /// <summary>
        /// Thêm order mới, trả về OrderID vừa tạo để dùng cho việc thêm OrderDetail sau đó
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public Task<int> AddOrderAsync(Orders order);

        /// <summary>
        /// Truy xuất order bằng mã đặt bàn
        /// </summary>
        /// <param name="reservationId"></param>
        /// <returns></returns>
        public Task<List<Orders>> GetOrdersByReservationIdAsync(int reservationId);

        /// <summary>
        /// Truy xuất order bằng số bàn. Thuộc tính TotalAmount chỉ là tạm tính, nếu muốn tính chính xác cần truy xuất chi tiết order
        /// </summary>
        /// <param name="tableId"></param>
        /// <returns></returns>
        public Task<Orders?> GetOrdersByTableIdAsync(int tableId);

        /// <summary>
        /// Cập nhật thời gian thanh toán
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="checkoutTime"></param>
        /// <returns></returns>
        public Task UpdateCheckoutTimeAsync(int orderId, DateTime checkoutTime);

    }
}
