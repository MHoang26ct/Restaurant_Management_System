using FoodOrderManagement.DAL.Helper;
using FoodOrderManagement.DAL.Models.Entities;
using FoodOrderManagement.DAL.Repositories.Interfaces;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrderManagement.DAL.Repositories.Implementations {
    public class UsersRepository : IUsersRepository {
        private readonly DatabaseHelper _db = new DatabaseHelper();

        //
        private Users Mapper(SqlDataReader reader) {
            return new Users {
                Username = reader.GetString(0),
                PasswordHash = reader.GetString(1),
                Role = reader.GetInt32(2)
            };
        }

        // Ham xu ly hash mat khau
        private string HashPassword(string password) {
            using var sha256 = SHA256.Create();
            var passwordBytes = Encoding.UTF8.GetBytes(password);
            return Convert.ToBase64String(sha256.ComputeHash(passwordBytes));
        }

        // Kiểm tra đăng nhập
        public async Task<bool> LoginAsync(Users user) {
            var passwordHash = HashPassword(user.PasswordHash);

            var parameters = new SqlParameter[]
            {
                new SqlParameter("@Username", user.Username),
                new SqlParameter("@PasswordHash", passwordHash)
            };
            var list = await _db.QueryAsync("ValidateUserLogin", Mapper, parameters);
            return list.Any();
        }

        // Thêm tài khoản người dùng mới
        public async Task AddUserAsync(Users user) {
            var passwordHash = HashPassword(user.PasswordHash);

            var parameters = new SqlParameter[]
            {
                new SqlParameter("@Username", user.Username),
                new SqlParameter("@PasswordHash", passwordHash),
                new SqlParameter("@UserRole", user.Role)
            };
            await _db.ExecuteNonQueryAsync("AddUser", parameters);  
        }

        // Đổi mật khẩu
        public async Task ChangePasswordAsync(string username, string newPassword) {
            var newPasswordHash = HashPassword(newPassword);
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@Username", username),
                new SqlParameter("@NewPasswordHash", newPasswordHash)
            };
            await _db.ExecuteNonQueryAsync("ChangeUserPassword", parameters);
        }

        // Kiểm tra tên người dùng đã tồn tại
        public async Task<bool> IsUsernameExistsAsync(string username) {
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@Username", username)
            };
            var list = await _db.QueryAsync("CheckUsernameExists", Mapper, parameters);
            return list.Any();
        }

        // Xoá người dùng
        public async Task DeleteUserAsync(string username) {
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@Username", username)
            };
            await _db.ExecuteNonQueryAsync("DeleteUser", parameters);
        }
    }
}
