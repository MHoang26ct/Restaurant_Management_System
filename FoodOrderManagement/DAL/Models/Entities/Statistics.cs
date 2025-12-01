using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrderManagement.DAL.Models.Entities {
    public class Statistics {
        /// <summary>
        /// Thời gian
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Tổng doanh thu
        /// </summary>
        public decimal TotalRevenue { get; set; }

        /// <summary>
        /// Tổng số đơn hàng
        /// </summary>
        public int TotalOrders { get; set; }

        /// <summary>
        /// Tổng số khách hàng
        /// </summary>
        public int TotalCustomers { get; set; }

        /// <summary>
        /// Tổng số lần đặt bàn
        /// </summary>
        public int TotalReservations { get; set; }
    }
}
