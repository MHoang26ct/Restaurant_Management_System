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
        public async Task<Customers?> GetCustomerByIdAsync(int id) {
            using (var connection = new SqlConnection(_connectionString)) {
                await connection.OpenAsync();
                var command = new SqlCommand("SELECT customerId, FullName, Email, PhoneNumber, LastVisitDate, TotalVisits, TotalSpent, CustomerRank FROM custumers WHERE customerId = @CustomerID", connection);
                command.Parameters.AddWithValue("@CustomerID", id);
                using (var reader = await command.ExecuteReaderAsync()) {
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
                    }
                    else {
                        return null;
                    }
                }
            }
        }

        // Thêm khách hàng mới và trả về ID khách hàng mới tạo (dùng cho đặt bàn)
        public async Task<int> AddCustomerAsync(Customers customer) {
            using (var connection = new SqlConnection(_connectionString)) {
                await connection.OpenAsync();
                using (var command = new SqlCommand("AddCustomer", connection)) {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@FullName", customer.FullName);
                    command.Parameters.AddWithValue("@Email", customer.Email);
                    command.Parameters.AddWithValue("@PhoneNumber", customer.PhoneNumber);
                    var outputIdParam = new SqlParameter("@NewCustomerID", System.Data.SqlDbType.Int) {
                        Direction = System.Data.ParameterDirection.Output
                    };
                    command.Parameters.Add(outputIdParam);
                    await command.ExecuteNonQueryAsync();
                    return (int)outputIdParam.Value;
                }
            }
        }

        // Cập nhật thông tin khách hàng
        public async Task UpdateCustomerInfoAsync(Customers customer) {
            using (var connection = new SqlConnection(_connectionString)) {
                await connection.OpenAsync();
                using (var command = new SqlCommand("UpdateCustomerInfo", connection)) {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@CustomerID", customer.Id);
                    command.Parameters.AddWithValue("@FullName", customer.FullName);
                    command.Parameters.AddWithValue("@Email", customer.Email);
                    command.Parameters.AddWithValue("@PhoneNumber", customer.PhoneNumber);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        // Lấy thông tin khách hàng theo tên và số điện thoại
        public async Task<Customers?> GetCustomerByNameAndPhoneAsync(string fullName, string phoneNumber) {
            using (var connection = new SqlConnection(_connectionString)) {
                await connection.OpenAsync();
                var command = new SqlCommand("SELECT customerId, FullName, Email, PhoneNumber, LastVisitDate, TotalVisits, TotalSpent, CustomerRank FROM Customers WHERE FullName = @FullName AND PhoneNumber = @PhoneNumber", connection);
                command.Parameters.AddWithValue("@FullName", fullName);
                command.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
                using (var reader = await command.ExecuteReaderAsync()) {
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
                    }
                    else {
                        return null;
                    }
                }
            }
        }

        // Lấy danh sách tất cả khách hàng
        public async Task<List<Customers>> GetAllCustomersAsync() {
            var customers = new List<Customers>();
            using (var connection = new SqlConnection(_connectionString)) {
                await connection.OpenAsync();
                using (var command = new SqlCommand("GetAllCustomers", connection)) {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    using (var reader = await command.ExecuteReaderAsync()) {
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
                    }
                }
            }
            return customers;
        }
    }
}
