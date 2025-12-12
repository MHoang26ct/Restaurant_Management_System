using FoodOrderManagement.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodOrderManagement.DAL.Models.Entities;
using Microsoft.Data.SqlClient;
using System.Configuration;
using FoodOrderManagement.DAL.Helper;

namespace FoodOrderManagement.DAL.Repositories.Implementations {
public class CustomersRepository : ICustomersRepository {
        private readonly DatabaseHelper _db = new DatabaseHelper();

        //
        private Customers Mapper(SqlDataReader reader) {
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

        // Lấy thông tin khách theo ID
        public async Task<Customers?> GetCustomerByIdAsync(int id) {
            var param = new SqlParameter("@CustomerID", id);
            return await _db.QuerySingleAsync<Customers>(
                "GetCustomerByID",
                Mapper,
                param
            );
        }

        // Thêm khách hàng mới và trả về ID khách hàng mới tạo (dùng cho đặt bàn)
        public async Task<int> AddCustomerAsync(Customers customer) {
            var outputIdParam = new SqlParameter("@NewCustomerID", System.Data.SqlDbType.Int) {
                Direction = System.Data.ParameterDirection.Output
            };
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@FullName", customer.FullName),
                new SqlParameter("@Email", customer.Email),
                new SqlParameter("@PhoneNumber", customer.PhoneNumber),
                outputIdParam
            };
            await _db.ExecuteNonQueryAsync("AddCustomer", parameters);
            return (int)outputIdParam.Value;
        }

        // Cập nhật thông tin khách hàng
        public async Task UpdateCustomerInfoAsync(Customers customer) {
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@CustomerID", customer.Id),
                new SqlParameter("@FullName", customer.FullName),
                new SqlParameter("@Email", customer.Email),
                new SqlParameter("@PhoneNumber", customer.PhoneNumber)
            };
            await _db.ExecuteNonQueryAsync("UpdateCustomerInfo", parameters);
        }

        // Lấy thông tin khách hàng theo tên và số điện thoại
        public async Task<Customers?> GetCustomerByNameAndPhoneAsync(string fullName, string phoneNumber) {
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@Name", string.IsNullOrEmpty(fullName) ? (object)DBNull.Value : fullName),
                new SqlParameter("@PhoneNumber", string.IsNullOrEmpty(phoneNumber) ? (object)DBNull.Value : phoneNumber)
            };
            return await _db.QuerySingleAsync<Customers>(
                "GetCustomerByNameAndPhone",
                Mapper,
                parameters
            );
        }

        // Lấy danh sách tất cả khách hàng
        public async Task<List<Customers>> GetAllCustomersAsync() {
            return await _db.GetListAsync("GetAllCustomers", Mapper);
        }
    }
}