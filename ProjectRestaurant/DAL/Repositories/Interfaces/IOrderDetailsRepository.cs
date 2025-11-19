using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectRestaurant.DAL.Models.Entities;

namespace ProjectRestaurant.DAL.Repositories.Interfaces {
    public interface IOrderDetailsRepository {
        // Thêm chi tiết order mới
        // OrderID sẽ được truy xuất theo số bàn bằng phương thức trong IOrdersRepository
        // nếu khách đã gọi món trước đó, chỉ cần thêm chi tiết món mới vào order hiện tại
        public Task AddOrderDetailAsync(orderDetail orderDetail);

        /// <summary>
        /// Lấy chi tiết order theo OrderID, nếu muốn tính bill thì kiểm tra trạng thái của từng chi tiết order
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public Task<List<orderDetail>> GetorderDetailByOrderIdAsync(int orderId);

        /// <summary>
        /// Thay đổi trạng thái của order
        /// </summary>
        /// <param name="orderDetailId"></param>
        /// <param name="newStatus"></param>
        /// <returns></returns>
        public Task UpdateOrderStatusAsync(int orderDetailId, string newStatus);
    }
}
