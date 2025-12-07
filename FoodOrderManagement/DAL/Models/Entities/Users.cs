using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrderManagement.DAL.Models.Entities {
    public class Users {
        public string Username { get; set; }
        public string PasswordHash { get; set; }

        /// <summary>
        /// 0: Employee, 1: Admin
        /// </summary>
        public int Role { get; set; } 
    }
}
