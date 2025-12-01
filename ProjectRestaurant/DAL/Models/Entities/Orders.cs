using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrderManagement.DAL.Models.Entities {
    public class Orders {
        public int Id { get; set; }
        public int ReservationId { get; set; }
        public int CustomerId { get; set; }
        public int TableId { get; set; }
        public DateTime OrderTime { get; set; }
        public decimal TotalAmount { get; set; }
        public int NumberOfGuests { get; set; }

        /// <summary>
        /// Thời gian thanh toán hóa đơn.
        /// </summary>
        public DateTime? CheckoutTime { get; set; }
    }
}
