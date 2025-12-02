using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodOrderManagement.DAL.Repositories.Interfaces;
using FoodOrderManagement.DAL.Models.Entities;
using Microsoft.Data.SqlClient;
using System.Configuration;

namespace FoodOrderManagement.DAL.Repositories.Implementations {
    public class FoodsRepository : IFoodsRepository {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        // Lấy tất cả món ăn
        public async Task<List<Foods>> GetAllFoodsAsync() {
            var foods = new List<Foods>();
            using (SqlConnection connection = new SqlConnection(_connectionString)) {
                await connection.OpenAsync();
                using (var command = new SqlCommand("GetAllFoods", connection)) {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    using (var reader = await command.ExecuteReaderAsync()) {
                        while (await reader.ReadAsync()) {
                            var food = new Foods {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Price = reader.GetDecimal(2)
                            };
                            foods.Add(food);
                        }
                    }
                }
            }
            return foods;
        }

        // Truy xuất món ăn theo ID
        public async Task<Foods?> GetFoodByIdAsync(int foodId) {
            Foods food = null;
            using (SqlConnection connection = new SqlConnection(_connectionString)) {
                await connection.OpenAsync();
                using (var command = new SqlCommand("SELECT * FROM Foods WHERE FoodID = @FoodID", connection)) {
                    command.Parameters.AddWithValue("@FoodID", foodId);
                    using (var reader = await command.ExecuteReaderAsync()) {
                        if (await reader.ReadAsync()) {
                            food = new Foods {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Price = reader.GetDecimal(2),
                                Category = reader.GetString(3),
                                ImagePath = reader.GetString(4),
                                Description = reader.GetString(5)
                            };
                        }
                    }
                }
            }
            return food;
        }

        // Thêm món ăn mới và trả về ID của món ăn vừa thêm
        public async Task<int> AddFoodAsync(Foods food) {
            int newFoodId;
            using (SqlConnection connection = new SqlConnection(_connectionString)) {
                await connection.OpenAsync();
                using (var command = new SqlCommand("AddFood", connection)) {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@FoodName", food.Name);
                    command.Parameters.AddWithValue("@Price", food.Price);
                    command.Parameters.AddWithValue("@Category", food.Category);
                    command.Parameters.AddWithValue("@ImagePath", food.ImagePath);
                    command.Parameters.AddWithValue("@Description", food.Description);
                    var outputIdParam = new SqlParameter("@NewFoodID", System.Data.SqlDbType.Int) {
                        Direction = System.Data.ParameterDirection.Output
                    };
                    command.Parameters.Add(outputIdParam);
                    await command.ExecuteNonQueryAsync();
                    newFoodId = (int)outputIdParam.Value;
                }
            }
            return newFoodId;
        }

        // Cập nhật thông tin món ăn
        public async Task UpdateFoodAsync(Foods food) {
            using (SqlConnection connection = new SqlConnection(_connectionString)) {
                await connection.OpenAsync();
                using (var command = new SqlCommand("UpdateFood", connection)) {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@FoodID", food.Id);
                    command.Parameters.AddWithValue("@FoodName", food.Name);
                    command.Parameters.AddWithValue("@Price", food.Price);
                    command.Parameters.AddWithValue("@Category", food.Category);
                    command.Parameters.AddWithValue("@ImagePath", food.ImagePath);
                    command.Parameters.AddWithValue("@Description", food.Description);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        // Xoá món ăn 
        public async Task DeleteFoodAsync (int foodId) {
            using (var connection = new SqlConnection(_connectionString)) {
                await connection.OpenAsync();
                using (var command = new SqlCommand("DeleteFood", connection)) {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@FoodID", foodId);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
