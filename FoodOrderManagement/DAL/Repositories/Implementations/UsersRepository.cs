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
            // Xử lý Hash mật khẩu
            using var sha256 = SHA256.Create();
            var passwordBytes = Encoding.UTF8.GetBytes(user.PasswordHash);
            var passwordHash = Convert.ToBase64String(sha256.ComputeHash(passwordBytes));

            using SqlConnection connection = new SqlConnection(connectionString);
            using SqlCommand command = new SqlCommand("ValidateUserLogin", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            
            command.Parameters.AddWithValue("@Username", user.Username);
            command.Parameters.AddWithValue("@PasswordHash", passwordHash);
            
            await connection.OpenAsync();
            var result = await command.ExecuteScalarAsync();
            
            // Kiểm tra null và convert an toàn
            return (result != null && (int)result > 0);
        }

        // Thêm tài khoản người dùng mới
        public async Task AddUserAsync(Users user) {
            // Xử lý Hash mật khẩu mới
            using var sha256 = SHA256.Create();
            var passwordBytes = Encoding.UTF8.GetBytes(user.PasswordHash);
            var passwordHash = Convert.ToBase64String(sha256.ComputeHash(passwordBytes));

            using SqlConnection connection = new SqlConnection(connectionString);
            using SqlCommand command = new SqlCommand("AddUser", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            
            command.Parameters.AddWithValue("@Username", user.Username);
            command.Parameters.AddWithValue("@PasswordHash", passwordHash);
            command.Parameters.AddWithValue("@UserRole", user.Role);
            
            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
        }

        // Đổi mật khẩu
        public async Task ChangePasswordAsync(string username, string newPassword) {
            // Xử lý Hash mật khẩu mới
            using var sha256 = SHA256.Create();
            var passwordBytes = Encoding.UTF8.GetBytes(newPassword);
            var newPasswordHash = Convert.ToBase64String(sha256.ComputeHash(passwordBytes));

            using SqlConnection connection = new SqlConnection(connectionString);
            using SqlCommand command = new SqlCommand("ChangeUserPassword", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            
            command.Parameters.AddWithValue("@Username", username);
            command.Parameters.AddWithValue("@NewPasswordHash", newPasswordHash);
            
            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
        }

        // Kiểm tra tên người dùng đã tồn tại
        public async Task<bool> IsUsernameExistsAsync(string username) {
            using SqlConnection connection = new SqlConnection(connectionString);
            using SqlCommand command = new SqlCommand("CheckUsernameExists", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            
            command.Parameters.AddWithValue("@Username", username);
            
            await connection.OpenAsync();
            var result = await command.ExecuteScalarAsync();
            
            return (result != null && (int)result > 0);
        }

        // Xoá người dùng
        public async Task DeleteUserAsync(string username) {
            using SqlConnection connection = new SqlConnection(connectionString);
            using SqlCommand command = new SqlCommand("DeleteUser", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            
            command.Parameters.AddWithValue("@Username", username);
            
            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
        }
    }
}
