using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectRestaurant.DAL.Models.Entities;

namespace ProjectRestaurant.DAL.Repositories.Interfaces {
    public interface ICustomersRepository {
        /// <summary>
        /// Lấy thông tin khách theo ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Customers?> GetCustomerByIdAsync(int id);

        /// <summary>
        /// Thêm khách hàng mới và trả về ID khách hàng mới tạo (dùng cho đặt bàn)
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        Task<int> AddCustomerAsync(Customers customer);

        /// <summary>
        /// Cap nhật thông tin khách hàng
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        Task UpdateCustomerInfoAsync(Customers customer);

        /// <summary>
        /// Lấy thông tin khách hàng theo tên và số điện thoại
        /// </summary>
        /// <param name="fullName"></param>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        Task<Customers?> GetCustomerByNameAndPhoneAsync(string fullName, string phoneNumber);
    }
}
