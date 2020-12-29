using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dapper;
using System.Linq;
using MISA.ApplicationCore.Models;
using MISA.ApplicationCore.Interfaces;

namespace MISA.Infrastructure
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(IConfiguration configuration) : base(configuration)
        {

        }
        public Customer GetCustomerByCode(string customerCode)
        {
            var customerDuplicate = _dbConnection.Query<Customer>($"SELECT * FROM Customer WHERE CustomerCode = @customerCode", new { customerCode }, commandType: CommandType.Text).FirstOrDefault();
            return customerDuplicate;
        }

        public IEnumerable<Customer> GetEntityPaging(int limit, int offset, string searchKey, Guid? customerGroupId)
        {
            var query = $"SELECT * FROM Customer";
            if (searchKey.Trim() != string.Empty)
            {
                query += $" where (FullName like '%{searchKey}%' or CustomerCode like '{searchKey}%' or PhoneNumber like '%{searchKey}%')";
                if (customerGroupId != null) query += $" and CustomerGroupId = '{customerGroupId}'";
            }
            else if (customerGroupId != null) query += $" where CustomerGroupId = '{customerGroupId}'";

            query += $" ORDER BY CreatedDate DESC";
            query += $" LIMIT {limit} OFFSET {offset}";
            var entities = _dbConnection.Query<Customer>(query, commandType: CommandType.Text);
            return entities;
        }

        public int GetCountCondition(string searchKey, Guid? customerGroupId)
        {
            var query = $"SELECT COUNT(*) FROM Customer";
            if (searchKey.Trim() != string.Empty)
            {
                query += $" where (FullName like '%{searchKey}%' or CustomerCode like '{searchKey}%' or PhoneNumber like '%{searchKey}%')";
                if (customerGroupId != null) query += $" and CustomerGroupId = '{customerGroupId}'";
            }
            else if (customerGroupId != null) query += $" where CustomerGroupId = '{customerGroupId}'";

            var totalItem = _dbConnection.Query<int>(query, commandType: CommandType.Text).FirstOrDefault();
            return totalItem;
        }
    }
}
