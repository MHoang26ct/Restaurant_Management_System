using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrderManagement.UI.Forms.FormDashboard
{
    public class DashboardStatModel
    {
        public decimal TotalRevenue { get; set; }
        public int TotalOrders { get; set; }
        public int TotalReservations { get; set; }
        public int TotalCustomers { get; set; }
    }
}
