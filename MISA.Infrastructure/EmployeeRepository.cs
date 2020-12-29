using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.ApplicationCore.Entities;
using MISA.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace MISA.Infrastructure
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(IConfiguration configuration) : base(configuration)
        {

        }
        public Employee GetEmployeeByCode(string employeeCode)
        {
            var EmployeeDuplicate = _dbConnection.Query<Employee>($"SELECT * FROM Employee WHERE EmployeeCode = @EmployeeCode", new { employeeCode }, commandType: CommandType.Text).FirstOrDefault();
            return EmployeeDuplicate;
        }

        public IEnumerable<Employee> GetEntityPaging(int limit, int offset, string searchKey)
        {
            var query = $"SELECT * FROM Employee";
            if (searchKey.Trim() != string.Empty)
            {
                query += $" where (FullName like '%{searchKey}%' or EmployeeCode like '{searchKey}%' or PhoneNumber like '%{searchKey}%')";
            }

            query += $" ORDER BY CreatedDate DESC";
            query += $" LIMIT {limit} OFFSET {offset}";
            var entities = _dbConnection.Query<Employee>(query, commandType: CommandType.Text);
            return entities;
        }

        public int GetCountCondition(string searchKey)
        {
            var query = $"SELECT COUNT(*) FROM Employee";
            if (searchKey.Trim() != string.Empty)
            {
                query += $" where (FullName like '%{searchKey}%' or EmployeeCode like '{searchKey}%' or PhoneNumber like '%{searchKey}%')";
            }

            var totalItem = _dbConnection.Query<int>(query, commandType: CommandType.Text).FirstOrDefault();
            return totalItem;
        }
    }
}
