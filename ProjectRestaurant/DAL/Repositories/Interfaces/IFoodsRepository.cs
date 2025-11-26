using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectRestaurant.DAL.Models.Entities;

namespace ProjectRestaurant.DAL.Repositories.Interfaces {
    internal interface IFoodsRepository {
        /// <summary>
        /// Lấy tất cả món ăn
        /// </summary>
        /// <returns></returns>
        Task<List<Foods>> GetAllFoodsAsync();

        /// <summary>
        /// Truy xuất món ăn theo ID
        /// </summary>
        /// <param name="foodId"></param>
        /// <returns></returns>
        Task<Foods?> GetFoodByIdAsync(int foodId);

        /// <summary>
        /// Thêm món ăn mới và trả về ID của món ăn vừa thêm
        /// </summary>
        /// <param name="food"></param>
        /// <returns></returns>
        Task<int> AddFoodAsync(Foods food);

        /// <summary>
        /// Cập nhật thông tin món ăn
        /// </summary>
        /// <param name="food"></param>
        /// <returns></returns>
        Task UpdateFoodAsync(Foods food);

        /// <summary>
        /// Xóa món ăn
        /// </summary>
        /// <param name="foodId"></param>
        /// <returns></returns>
        Task DeleteFoodAsync(int foodId);
    }
}
