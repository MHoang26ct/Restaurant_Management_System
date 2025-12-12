using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrderManagement.UI.Forms.TableManagement
{
    public class TableData
    {
        public int TableId { get; set; }
        public string TableName { get; set; } // Ví dụ: Bàn 5
        public string Status { get; set; } // Available, Occupied, Reserved;
        public string CustomerName { get; set; }
        public string OrderId { get; set; }
        public DateTime? ReservationTime { get; set; } // Nullable nếu không đặt
    }
}
