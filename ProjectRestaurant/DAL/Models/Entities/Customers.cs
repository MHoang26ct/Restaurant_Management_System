using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectRestaurant.DAL.Models.Entities {
    public class Customers {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime LastVisitDate { get; set; } // Duoc tu dong cap nhap bang trigger thuc hien thay doi thong tin khach hang
        public int TotalVisits { get; set; } // Duoc tu dong cap nhap bang trigger thuc hien thay doi thong tin khach hang
        public float TotalSpent { get; set; } // Duoc tu dong cap nhap bang trigger moi khi khach hang co giao dich
        public string CustomerRank { get; set; } // Duoc tu dong cap nhap bang trigger thuc hien thay doi thong tin khach hang
    }
}
