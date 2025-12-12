using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodOrderManagement.DAL.Models.Entities;
using FoodOrderManagement.DAL.Repositories.Interfaces;
using Microsoft.Data.SqlClient;
using System.Configuration;
using FoodOrderManagement.DAL.Helper;

namespace FoodOrderManagement.DAL.Repositories.Implementations {
    public class EmployeesRepository : IEmployeesRepository {
        private readonly DatabaseHelper _db = new DatabaseHelper();

        //
        private Employee Mapper(SqlDataReader reader) {
            return new Employee {
                FullName = reader.GetString(0),
                PhoneNumber = reader.GetString(1),
                Email = reader.GetString(2),
                Position = reader.GetString(3),
                HireDate = reader.GetDateTime(4)
            };
        }

        // Lấy thông tin nhân viên theo tên và số điện thoại
        public async Task<Employee?> GetEmployeeByNameAndPhoneAsync(string fullName, string phoneNumber) {
            var parameters = new SqlParameter[] {
                new SqlParameter("@FullName", fullName),
                new SqlParameter("@PhoneNumber", phoneNumber)
            };
            return await _db.QuerySingleAsync("GetEmployeesByNameAndPhone", Mapper, parameters);
        }

        // Thêm nhân viên mới
        public async Task<bool> AddEmployeeAsync(Employee employee) {
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@FullName", employee.FullName),
                new SqlParameter("@PhoneNumber", employee.PhoneNumber),
                new SqlParameter("@Email", employee.Email),
                new SqlParameter("@Position", employee.Position),
                new SqlParameter("@HireDate", employee.HireDate)
            };
            int rowsAffected = await _db.ExecuteNonQueryAsync("AddEmployee", parameters);
            return rowsAffected > 0;
        }

        // Cập nhật thông tin nhân viên
        public async Task<bool> UpdateEmployeeAsync(Employee employee) {
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@FullName", employee.FullName),
                new SqlParameter("@PhoneNumber", employee.PhoneNumber),
                new SqlParameter("@Email", employee.Email),
                new SqlParameter("@Position", employee.Position),
                new SqlParameter("@HireDate", employee.HireDate)
            };
            int rowsAffected = await _db.ExecuteNonQueryAsync("UpdateEmployee", parameters);
            return rowsAffected > 0;
        }

        // Xoá nhân viên theo tên và số điện thoại
        public async Task<bool> DeleteEmployeeByNameAndPhoneAsync(string fullName, string phoneNumber) {
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@FullName", fullName),
                new SqlParameter("@PhoneNumber", phoneNumber)
            };
            int rowsAffected = await _db.ExecuteNonQueryAsync("DeleteEmployee", parameters);
            return rowsAffected > 0;
        }
        
        // Lấy tất cả nhân viên
        public async Task<List<Employee>> GetAllEmployeesAsync() {
            return await _db.GetListAsync("GetAllEmployees", Mapper);
        }
    }
}
