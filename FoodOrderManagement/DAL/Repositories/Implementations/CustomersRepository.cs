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
                FullName = reader.IsDBNull(1) ? null : reader.GetString(1),
                Email = reader.IsDBNull(2) ? null : reader.GetString(2),
                PhoneNumber = reader.IsDBNull(3) ? null : reader.GetString(3),
                LastVisitDate = reader.IsDBNull(4) ? DateTime.MinValue : reader.GetDateTime(4),
                TotalVisits = reader.GetInt32(5),
                TotalSpent = reader.IsDBNull(6) ? 0 : reader.GetDecimal(6),
                CustomerRank = reader.IsDBNull(7) ? null : reader.GetString(7)
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
        public async Task<int> AddCustomerAsync(Customers customer)
        {
            var outputIdParam = new SqlParameter("@NewCustomerID", System.Data.SqlDbType.Int)
            {
                Direction = System.Data.ParameterDirection.Output
            };

            var parameters = new SqlParameter[]
            {
                new SqlParameter("@FullName", customer.FullName),
                new SqlParameter("@Email", string.IsNullOrEmpty(customer.Email) ? (object)DBNull.Value : customer.Email),
                new SqlParameter("@PhoneNumber", customer.PhoneNumber),
                outputIdParam
            };

            await _db.ExecuteNonQueryAsync("AddCustomer", parameters);

            // Kiểm tra an toàn khi return
            return outputIdParam.Value != DBNull.Value ? (int)outputIdParam.Value : 0;
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
        public async Task DeleteCustomerAsync(int id)
        {
            string procedureName = "DeleteCustomer";

            var parameters = new SqlParameter[]
            {
            new SqlParameter("@CustomerID", id)
            };

            await _db.ExecuteNonQueryAsync(procedureName, parameters);
        }


        public async Task UpdateCustomerRankAsync(int customerId)
        {
            string querySum = @"
                SELECT ISNULL(SUM(TotalAmount), 0) 
                FROM Orders 
                WHERE CustomerID = @CusId AND CheckoutTime IS NOT NULL";

            decimal totalSpent = 0;
            using (var cmd = _db.CreateCommand(querySum))
            {
                cmd.Parameters.Add(new SqlParameter("@CusId", customerId));
                object result = await cmd.ExecuteScalarAsync();

                // Dùng Convert.ToDecimal an toàn cho mọi trường hợp
                totalSpent = result != null ? Convert.ToDecimal(result) : 0;
            }

            // Logic phân hạng (Mốc tiền khớp với UI)
            string newRank = "Regular"; // Hoặc "Member" tùy bạn chọn
            if (totalSpent >= 50000000) newRank = "Platinum";
            else if (totalSpent >= 10000000) newRank = "Gold";
            else if (totalSpent >= 2000000) newRank = "Silver";

            string queryUpdate = "UPDATE Customers SET CustomerRank = @Rank WHERE CustomerID = @CustomerID";

            var p = new SqlParameter[]
            {
                new SqlParameter("@Rank", newRank),
                new SqlParameter("@CustomerID", customerId)
            };

            await _db.ExecuteNonQueryAsync(queryUpdate, p);
        }
    }
}