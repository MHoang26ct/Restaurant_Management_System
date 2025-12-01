using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrderManagement.DAL.Models.Entities {
    public class Tables {
        public int Id { get; set; }
        public int Capacity { get; set; }
        
        /// <summary>
        /// Thời gian khách ngồi vào bàn
        /// </summary>
        public DateTime OpenTime { get; set; } 

        /// <summary>
        /// Ví dụ: "Available", "Occupied", "Reserved"
        /// </summary>
        public string Status { get; set; } = "Available";
    }
}
