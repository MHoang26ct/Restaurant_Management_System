using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodOrderManagement.DAL.Models.Entities;
using FoodOrderManagement.DAL.Repositories.Interfaces;
using Microsoft.Data.SqlClient;
using System.Configuration;

namespace FoodOrderManagement.DAL.Repositories.Implementations {
    public class EmployeesRepository : IEmployeesRepository {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        // Lấy thông tin nhân viên theo tên và số điện thoại
        public async Task<Employee?> GetEmployeeByNameAndPhoneAsync(string fullName, string phoneNumber) {
            using SqlConnection connection = new SqlConnection(_connectionString);
            string query = "SELECT * FROM Employees WHERE FullName = @FullName AND PhoneNumber = @PhoneNumber";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@FullName", fullName);
            command.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
            await connection.OpenAsync();
            SqlDataReader reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync()) {
                Employee employee = new Employee {
                    FullName = reader["FullName"].ToString(),
                    PhoneNumber = reader["PhoneNumber"].ToString(),
                    Email = reader["Email"].ToString(),
                    Position = reader["Position"].ToString(),
                    HireDate = Convert.ToDateTime(reader["HireDate"])
                };
                return employee;
            }
            return null;
        }

        // Thêm nhân viên mới
        public async Task<bool> AddEmployeeAsync(Employee employee) {
            using SqlConnection connection = new SqlConnection(_connectionString);
            string query = "INSERT INTO Employees (FullName, PhoneNumber, Email, Position, HireDate) VALUES (@FullName, @PhoneNumber, @Email, @Position, @HireDate)";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@FullName", employee.FullName);
            command.Parameters.AddWithValue("@PhoneNumber", employee.PhoneNumber);
            command.Parameters.AddWithValue("@Email", employee.Email);
            command.Parameters.AddWithValue("@Position", employee.Position);
            command.Parameters.AddWithValue("@HireDate", employee.HireDate);
            await connection.OpenAsync();
            int result = await command.ExecuteNonQueryAsync();
            return result > 0;
        }

        // Cập nhật thông tin nhân viên
        public async Task<bool> UpdateEmployeeAsync(Employee employee) {
            using SqlConnection connection = new SqlConnection(_connectionString);
            SqlCommand command = new SqlCommand("UpdateEmployee", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@FullName", employee.FullName);
            command.Parameters.AddWithValue("@PhoneNumber", employee.PhoneNumber);
            command.Parameters.AddWithValue("@Email", employee.Email);
            command.Parameters.AddWithValue("@Position", employee.Position);
            command.Parameters.AddWithValue("@HireDate", employee.HireDate);
            await connection.OpenAsync();
            int result = await command.ExecuteNonQueryAsync();
            return result > 0;
        }

        // Xoá nhân viên theo tên và số điện thoại
        public async Task<bool> DeleteEmployeeByNameAndPhoneAsync(string fullName, string phoneNumber) {
            using SqlConnection connection = new SqlConnection(_connectionString);
            SqlCommand command = new SqlCommand("DeleteEmployee", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@FullName", fullName);
            command.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
            await connection.OpenAsync();
            int result = await command.ExecuteNonQueryAsync();
            return result > 0;
        }
        
        // Lấy tất cả nhân viên
        public async Task<List<Employee>> GetAllEmployeesAsync() {
            var employees = new List<Employee>();
            using (SqlConnection connection = new SqlConnection(_connectionString)) {
                await connection.OpenAsync();
                using (var command = new SqlCommand("GetAllEmployees", connection)) {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    using (var reader = await command.ExecuteReaderAsync()) {
                        while (await reader.ReadAsync()) {
                            var employee = new Employee {
                                FullName = reader.GetString(0),
                                PhoneNumber = reader.GetString(1),
                                Email = reader.GetString(2),
                                Position = reader.GetString(3),
                                HireDate = reader.GetDateTime(4)
                            };
                            employees.Add(employee);
                        }
                    }
                }
            }
            return employees;
        }
    }
}
