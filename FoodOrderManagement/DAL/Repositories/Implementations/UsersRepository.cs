using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using FoodOrderManagement.DAL.Models.Entities;
using FoodOrderManagement.DAL.Repositories.Interfaces;
using Microsoft.Data.SqlClient;
using System.Configuration;

namespace FoodOrderManagement.DAL.Repositories.Implementations {
    public class UsersRepository : IUsersRepository {
        string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        // Kiểm tra đăng nhập
        public async Task<bool> LoginAsync(Users user) {
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                user.PasswordHash = Convert.ToBase64String(SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(user.PasswordHash)));
                var command = new SqlCommand("SELECT COUNT(*) FROM Users WHERE Username = @Username AND PasswordHash = @PasswordHash", connection);
                command.Parameters.AddWithValue("@Username", user.Username);
                command.Parameters.AddWithValue("@PasswordHash", user.PasswordHash);
                var count = (int)await command.ExecuteScalarAsync();
                return count > 0;
            }
        }

        // Thêm tài khoản người dùng mới
        public async Task AddUserAsync(Users user) {
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                user.PasswordHash = Convert.ToBase64String(SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(user.PasswordHash)));
                var command = new SqlCommand("AddUser", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Username", user.Username);
                command.Parameters.AddWithValue("@PasswordHash", user.PasswordHash);
                command.Parameters.AddWithValue("@UserRole", user.Role);
                await command.ExecuteNonQueryAsync();
            }

        }

        // Đổi mật khẩu
        public async Task ChangePasswordAsync(string username, string newPassword) {
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                var newPasswordHash = Convert.ToBase64String(SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(newPassword)));
                var command = new SqlCommand("ChangeUserPassword", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@NewPasswordHash", newPasswordHash);
                await command.ExecuteNonQueryAsync();
            }
        }

        // Kiểm tra tên người dùng đã tồn tại
        public async Task<bool> IsUsernameExistsAsync(string username) {
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand("SELECT COUNT(*) FROM Users WHERE Username = @Username", connection);
                command.Parameters.AddWithValue("@Username", username);
                var count = (int)await command.ExecuteScalarAsync();
                return count > 0;
            }
        }

        // Xoá người dùng
        public async Task DeleteUserAsync(string username) {
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand("DeleteUser", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Username", username);
                await command.ExecuteNonQueryAsync();
            }
        }
    }
}
