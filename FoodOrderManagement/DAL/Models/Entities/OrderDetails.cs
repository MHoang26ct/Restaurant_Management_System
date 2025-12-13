using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrderManagement.DAL.Models.Entities {
    public class orderDetail {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int FoodId { get; set; }
        public int Quantity { get; set; }

        /// <summary>
        /// Lưu ý đặc biệt cho món ăn trong đơn hàng.
        /// </summary>
        public string Notes { get; set; }
    }
}
