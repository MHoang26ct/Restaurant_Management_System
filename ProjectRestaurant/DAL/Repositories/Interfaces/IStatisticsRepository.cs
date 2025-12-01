using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectRestaurant.DAL.Models.Entities;

namespace ProjectRestaurant.DAL.Repositories.Interfaces {
    public interface IStatisticsRepository {
        /// <summary>
        /// Thống kê kết quả kinh doanh theo khoảng thời gian (chia ra theo ngày). Có thể dùng chung cho thống kê theo ngày, tuần, tháng, năm
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public Task<Statistics> GetBusinessStatsByDateAsync(DateTime startDate, DateTime endDate);
    }
}
