using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrderManagement.UI.Forms.ReservationManagement
{
    public class ReservationViewModel
    {
        public int Id { get; set; }
        public string CustomerName { get; set; } // Hiển thị tên thay vì ID
        public string PhoneNumber { get; set; }
        public int TableId { get; set; }
        public DateTime ReservationTime { get; set; }
        public int NumberOfGuests { get; set; }
        public string Status { get; set; }
    }
}
