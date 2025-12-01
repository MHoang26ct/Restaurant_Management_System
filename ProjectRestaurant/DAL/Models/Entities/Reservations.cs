using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrderManagement.DAL.Models.Entities {
    public class Reservations {
        public int Id { get; set; }
        public int customerId { get; set; }
        public int TableId { get; set; }
        public DateTime ReservationTime { get; set; }

        /// <summary>
        /// Thời gian khách đến nhà hàng.
        /// </summary>
        public DateTime ComingTime { get; set; }
        public int NumberOfGuests { get; set; }
    }
}
