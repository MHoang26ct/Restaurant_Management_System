using FoodOrderManagement.DAL.Models.Entities;
using FoodOrderManagement.UI.Forms.FormDashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrderManagement.DAL.Repositories.Interfaces {
    public interface IStatisticsRepository {
        /// <summary>
        /// Thống kê kết quả kinh doanh theo khoảng thời gian (chia ra theo ngày). Có thể dùng chung cho thống kê theo ngày, tuần, tháng, năm
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public Task<List<Statistics>> GetBusinessStatsByDateAsync(DateTime startDate, DateTime endDate);

        /// <summary>
        /// Lấy thống kê tổng quan cho dashboard theo ngày
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        Task<DashboardStatModel> GetDashboardStatsByDateAsync(DateTime date);
    }
}
