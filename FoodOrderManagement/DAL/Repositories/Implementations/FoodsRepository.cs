using FoodOrderManagement.DAL.Helper;
using FoodOrderManagement.DAL.Models.Entities;
using FoodOrderManagement.DAL.Repositories.Interfaces;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrderManagement.DAL.Repositories.Implementations {
    public class FoodsRepository : IFoodsRepository {
        private readonly DatabaseHelper _db = new DatabaseHelper();

        //
        private Foods Mapper(SqlDataReader reader) {
            return new Foods {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1),
                Price = reader.GetDecimal(2),
                Category = reader.GetString(3),
                ImagePath = reader.GetString(4),
                Description = reader.GetString(5)
            };
        }

        // Lấy tất cả món ăn
        public async Task<List<Foods>> GetAllFoodsAsync() {
            return await _db.QueryAsync("GetAllFoods", Mapper);
        }

        // Truy xuất món ăn theo ID
        public async Task<Foods?> GetFoodByIdAsync(int foodId) {
            var param = new SqlParameter("@FoodID", foodId);
            return await _db.QuerySingleAsync("GetFoodByID", Mapper, param);
        }

        // Lấy món ăn theo tên
        public async Task<Foods?> GetFoodByNameAsync(string foodName) { 
            var param = new SqlParameter("@FoodName", foodName);
            return await _db.QuerySingleAsync("SearchFoodsByName", Mapper, param);
        }

        // Thêm món ăn mới và trả về ID của món ăn vừa thêm
        public async Task<int> AddFoodAsync(Foods food) {
            var outputIdParam = new SqlParameter("@NewFoodID", System.Data.SqlDbType.Int) {
                Direction = System.Data.ParameterDirection.Output
            };
            var parameters = new SqlParameter[] {
                new SqlParameter("@FoodName", food.Name),
                new SqlParameter("@Price", food.Price),
                new SqlParameter("@Category", food.Category),
                new SqlParameter("@ImagePath", food.ImagePath),
                new SqlParameter("@Description", food.Description),
                outputIdParam
            };
            await _db.ExecuteNonQueryAsync("AddFood", parameters);
            return (int)outputIdParam.Value;
        }

        // Cập nhật thông tin món ăn
        public async Task UpdateFoodAsync(Foods food) {
            var parameters = new SqlParameter[] {
                new SqlParameter("@FoodID", food.Id),
                new SqlParameter("@FoodName", food.Name),
                new SqlParameter("@Price", food.Price),
                new SqlParameter("@Category", food.Category),
                new SqlParameter("@ImagePath", food.ImagePath),
                new SqlParameter("@Description", food.Description)
            };
            await _db.ExecuteNonQueryAsync("UpdateFood", parameters);
        }

        // Xoá món ăn 
        public async Task DeleteFoodAsync(int foodId) {
            var param = new SqlParameter("@FoodID", foodId);
            await _db.ExecuteNonQueryAsync("DeleteFood", param);
        }
    }
}