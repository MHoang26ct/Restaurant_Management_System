using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectRestaurant.DAL.Models.Entities;

namespace ProjectRestaurant.DAL.Repositories.Interfaces {
    public interface IUsersRepository {
        /// <summary>
        /// Kiểm tra đăng nhập
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<bool> LoginAsync(Users user);

        /// <summary>
        /// Thêm tài khoản người dùng mới
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task AddUserAsync(Users user);

        /// <summary>
        /// Đổi mật khẩu
        /// </summary>
        /// <param name="username"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        Task ChangePasswordAsync(string username, string newPassword);

        /// <summary>
        /// Kiểm tra tên người dùng đã tồn tại
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        Task<bool> IsUsernameExistsAsync(string username);

        /// <summary>
        /// Xoá người dùng
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        Task DeleteUserAsync(string username);

    }
}
