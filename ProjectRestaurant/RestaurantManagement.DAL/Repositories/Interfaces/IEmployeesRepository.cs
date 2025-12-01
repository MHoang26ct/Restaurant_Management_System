using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectRestaurant.DAL.Models.Entities;

namespace ProjectRestaurant.DAL.Repositories.Interfaces {
    public interface IEmployeesRepository {
        /// <summary>
        /// Lấy thông tin nhân viên theo tên và số điện thoại
        /// </summary>
        /// <param name="fullName"></param>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        Task<Employee?> GetEmployeeByNameAndPhoneAsync(string fullName, string phoneNumber);

        /// <summary>
        /// Thêm nhân viên mới
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        Task<bool> AddEmployeeAsync(Employee employee);

        /// <summary>
        /// Cập nhật thông tin nhân viên
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        Task<bool> UpdateEmployeeAsync(Employee employee);

        /// <summary>
        /// Xoá nhân viên theo tên và số điện thoại
        /// </summary>
        /// <param name="fullName"></param>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        Task<bool> DeleteEmployeeByNameAndPhoneAsync(string fullName, string phoneNumber);
    }
}
