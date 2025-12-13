using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodOrderManagement.DAL.Models.Entities;

namespace FoodOrderManagement.DAL.Repositories.Interfaces {
    public interface IOrdersRepository {

        /// <summary>
        /// Thêm order mới, trả về OrderID vừa tạo để dùng cho việc thêm OrderDetail sau đó
        /// không can thiet phai truyen total amount vi da co trigger tinh toan roi
        /// (trigger se tinh toan va cap nhat lai sau khi them/cap nhat chi tiet order)
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
        /// Có thể dùng để kiểm tra xem bàn đã có order chưa (chưa thanh toán) để tránh tạo thêm order mới trùng lặp
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
        public Task UpdateTimeCheckoutAsync(int orderId, DateTime TimeCheckout);

        /// <summary>
        /// Lấy danh sách order chưa thanh toán (tức là chưa có thời gian thanh toán)
        /// </summary>
        /// <returns></returns>
        public Task<List<Orders>> GetAllUnpaidOrdersAsync();

        // Xóa order theo mã order (cho trường hợp khách hủy đặt bàn)
        public Task DeleteOrderByIdAsync(int orderId);

        // Lấy các order hoàn thành để hiển thị trong quản lý order
        public Task<List<Orders>> GetAllCompletedOrdersAsync();

        // Lấy tất cả order
        public Task<List<Orders>> GetAllOrdersAsync();

    }
}
