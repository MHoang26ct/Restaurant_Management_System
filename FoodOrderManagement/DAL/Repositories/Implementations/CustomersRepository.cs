using FoodOrderManagement.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodOrderManagement.DAL.Models.Entities;
using Microsoft.Data.SqlClient;
using System.Configuration;

namespace FoodOrderManagement.DAL.Repositories.Implementations {
    public class CustomersRepository : ICustomersRepository {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        // Lấy thông tin khách theo ID
        public aysnc Task<Customers?> GetCustomerByIdAsync(int id) {
            using SqlConnection connection = new SqlConnection(_connectionString);
            SqlCommand command = new SqlCommand("GetCustomerByID", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@CustomerID", id);
            await connection.OpenAsync();
            using SqlDataReader reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync()) {
                return new Customers {
                    Id = reader.GetInt32(0),
                    FullName = reader.GetString(1),
                    Email = reader.GetString(2),
                    PhoneNumber = reader.GetString(3),
                    LastVisitDate = reader.IsDBNull(4) ? DateTime.MinValue : reader.GetDateTime(4),
                    TotalVisits = reader.GetInt32(5),
                    TotalSpent = (float)reader.GetDecimal(6),
                    CustomerRank = reader.GetString(7)
                };
            } else {
                return null;
            }
        }

// Thêm khách hàng mới và trả về ID khách hàng mới tạo (dùng cho đặt bàn)
        public async Task<int> AddCustomerAsync(Customers customer) {
            using SqlConnection connection = new SqlConnection(_connectionString);
            using SqlCommand command = new SqlCommand("AddCustomer", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            
            command.Parameters.AddWithValue("@FullName", customer.FullName);
            command.Parameters.AddWithValue("@Email", customer.Email);
            command.Parameters.AddWithValue("@PhoneNumber", customer.PhoneNumber);

            SqlParameter outputIdParam = new SqlParameter("@NewCustomerID", System.Data.SqlDbType.Int) {
                Direction = System.Data.ParameterDirection.Output
            };
            command.Parameters.Add(outputIdParam);

            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
            
            return (int)outputIdParam.Value;
        }

        // Cập nhật thông tin khách hàng
        public async Task UpdateCustomerInfoAsync(Customers customer) {
            using SqlConnection connection = new SqlConnection(_connectionString);
            using SqlCommand command = new SqlCommand("UpdateCustomerInfo", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@CustomerID", customer.Id);
            command.Parameters.AddWithValue("@FullName", customer.FullName);
            command.Parameters.AddWithValue("@Email", customer.Email);
            command.Parameters.AddWithValue("@PhoneNumber", customer.PhoneNumber);

            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
        }

        // Lấy thông tin khách hàng theo tên và số điện thoại
        public async Task<Customers?> GetCustomerByNameAndPhoneAsync(string fullName, string phoneNumber) {
            using SqlConnection connection = new SqlConnection(_connectionString);
            using SqlCommand command = new SqlCommand("GetCustomerByNameAndPhone", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@Name", string.IsNullOrEmpty(fullName) ? (object)DBNull.Value : fullName);
            command.Parameters.AddWithValue("@PhoneNumber", string.IsNullOrEmpty(phoneNumber) ? (object)DBNull.Value : phoneNumber);

            await connection.OpenAsync();
            using SqlDataReader reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync()) {
                return new Customers {
                    Id = reader.GetInt32(0),
                    FullName = reader.GetString(1),
                    Email = reader.GetString(2),
                    PhoneNumber = reader.GetString(3),
                    LastVisitDate = reader.IsDBNull(4) ? DateTime.MinValue : reader.GetDateTime(4),
                    TotalVisits = reader.GetInt32(5)
                };
            }
            
            return null;
        }

        // Lấy danh sách tất cả khách hàng
        public async Task<List<Customers>> GetAllCustomersAsync() {
            var customers = new List<Customers>();
            using SqlConnection connection = new SqlConnection(_connectionString);
            using SqlCommand command = new SqlCommand("GetAllCustomers", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;

            await connection.OpenAsync();
            using SqlDataReader reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync()) {
                customers.Add(new Customers {
                    Id = reader.GetInt32(0),
                    FullName = reader.GetString(1),
                    Email = reader.GetString(2),
                    PhoneNumber = reader.GetString(3),
                    LastVisitDate = reader.IsDBNull(4) ? DateTime.MinValue : reader.GetDateTime(4),
                    TotalVisits = reader.GetInt32(5),
                    TotalSpent = (float)reader.GetDecimal(6),
                    CustomerRank = reader.GetString(7)
                });
            }
            
            return customers;
        }