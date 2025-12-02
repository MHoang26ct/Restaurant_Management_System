using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodOrderManagement.DAL.Models.Entities;

namespace FoodOrderManagement.DAL.Repositories.Interfaces {
    public interface IFoodsRepository {
        /// <summary>
        /// Lấy tất cả món ăn
        /// </summary>
        /// <returns></returns>
        Task<List<Foods>> GetAllFoodsAsync();

        /// <summary>
        /// Lấy tất cả món ăn
        /// </summary>
        /// <param name="foodId"></param>
        /// <returns></returns>
        Task<Foods?> GetFoodByIdAsync(int foodId);

        /// <summary>
        /// Truy xuất món ăn theo ID
        /// </summary>
        /// <param name="food"></param>
        /// <returns></returns>
        Task<int> AddFoodAsync(Foods food);

        /// <summary>
        /// Thêm món ăn mới và trả về ID của món ăn vừa thêm
        /// </summary>
        /// <param name="food"></param>
        /// <returns></returns>
        Task UpdateFoodAsync(Foods food);

        /// <summary>
        /// Cập nhật thông tin món ăn

        /// </summary>
        /// <param name="foodId"></param>
        /// <returns></returns>
        Task DeleteFoodAsync(int foodId);
        /// <summary>
        /// Xóa món ăn
        /// </summary>
        /// <param name="food"></param>
        /// <returns></returns>
        /// >
    }
}
