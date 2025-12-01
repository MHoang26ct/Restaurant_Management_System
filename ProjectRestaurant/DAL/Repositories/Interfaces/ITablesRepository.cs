using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodOrderManagement.DAL.Models.Entities;
using FoodOrderManagement.DAL.Repositories.Interfaces;

namespace FoodOrderManagement.DAL.Repositories.Interfaces {
    public interface ITablesRepository {
        /// <summary>
        /// Lấy danh sách các bàn còn trống
        /// </summary>
        /// <returns></returns>
        Task<List<Tables>> GetAvailableTablesAsync();

        /// <summary>
        /// Cập nhật trạng thái và thời gian mở bàn, nếu chuyển trạng thái sang "Available" thì thời gian mở bàn sẽ là null
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="status"></param>
        /// <param name="openedAt"></param>
        /// <returns></returns>
        Task<bool> UpdateTableStatusAndOpenTimeAsync(int tableId, string status, DateTime? openedAt);

        /// <summary>
        /// Thêm bàn mới và trả về ID của bàn vừa thêm
        /// </summary>
        /// <param name="capacity"></param>
        /// <param name="tableStatus"></param>
        /// <returns></returns>
        Task<int> AddTableAsync(int capacity, string tableStatus);

        /// <summary>
        /// Xoá bàn theo ID
        /// </summary>
        /// <param name="tableId"></param>
        /// <returns></returns>
        Task<bool> DeleteTableByIdAsync(int tableId);
    }
}
