using FoodOrderManagement.DAL.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FoodOrderManagement.UI.Forms.OrderManagement.UserControlOfOrder.UC_ViewDetails;

namespace FoodOrderManagement.DAL.Repositories.Interfaces {
    public interface IOrderDetailsRepository {
        // Thêm chi tiết order mới
        // OrderID sẽ được truy xuất theo số bàn bằng phương thức trong IOrdersRepository
        // nếu khách đã gọi món trước đó, chỉ cần thêm chi tiết món mới vào order hiện tại
        public Task AddOrderDetailAsync(orderDetail orderDetail);

        /// <summary>
        /// Them danh sách chi tiết order mới
        /// </summary>
        /// <param name="orderDetails"></param>
        /// <returns></returns>
        public Task AddListOrderDetailAsync(List<orderDetail> orderDetails);

        /// <summary>
        /// Lấy chi tiết order theo OrderID
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public Task<List<orderDetail>> GetorderDetailByOrderIdAsync(int orderId);
        public Task<List<OrderDetailDisplay>> GetDetailsByOrderIdAsync(int orderId);

        /// <summary>
        /// Lấy các chi tiết order đã hoàn thành để tính tổng hóa đơn
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public Task<List<orderDetail>> GetCompletedOrderDetailsByOrderIdAsync(int orderId);

        /// <summary>
        /// Thay đổi trạng thái của order
        /// </summary>
        /// <param name="orderDetailId"></param>
        /// <param name="newStatus"></param>
        /// <returns></returns>
        public Task UpdateOrderStatusAsync(int orderDetailId, string newStatus);
        
    }
}
