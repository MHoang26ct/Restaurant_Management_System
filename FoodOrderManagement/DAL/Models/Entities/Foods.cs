using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrderManagement.DAL.Models.Entities {
    public class Foods {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ImagePath { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
    }
}
