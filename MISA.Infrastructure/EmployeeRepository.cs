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
            var EmployeeDuplicate = _dbConnection.Query<Employee>($"SELECT * FROM View_Employee WHERE EmployeeCode = @EmployeeCode", new { employeeCode }, commandType: CommandType.Text).FirstOrDefault();
            return EmployeeDuplicate;
        }

        public IEnumerable<Employee> GetEntityPaging(int limit, int offset, string searchKey, Guid? departmentId = null, Guid? positionId = null)
        {
            var query = $"SELECT * FROM View_Employee";
            if (searchKey.Trim() != string.Empty)
            {
                query += $" where (FullName like '%{searchKey}%' or EmployeeCode like '{searchKey}%' or PhoneNumber like '%{searchKey}%')";
                if(departmentId!=null)
                {
                    query += $" and DepartmentId = '{departmentId}'";
                }
                if (positionId != null)
                {
                    query += $" and PositionId = '{positionId}'";
                }
            }
            else
            {
                if (departmentId != null)
                {
                    query += $" where DepartmentId = '{departmentId}'";
                    if (positionId != null)
                    {
                        query += $" and PositionId = '{positionId}'";
                    }
                }
                else
                {
                    if (positionId != null)
                    {
                        query += $" where PositionId = '{positionId}'";
                    }
                }
            }

            query += $" ORDER BY CreatedDate DESC";
            query += $" LIMIT {limit} OFFSET {offset}";
            var entities = _dbConnection.Query<Employee>(query, commandType: CommandType.Text);
            return entities;
        }

        public int GetCountCondition(string searchKey, Guid? departmentId = null, Guid? positionId = null)
        {
            var query = $"SELECT COUNT(*) FROM Employee";
            if (searchKey.Trim() != string.Empty)
            {
                query += $" where (FullName like '%{searchKey}%' or EmployeeCode like '{searchKey}%' or PhoneNumber like '%{searchKey}%')";
                if (departmentId != null)
                {
                    query += $" and DepartmentId = '{departmentId}'";
                }
                if (positionId != null)
                {
                    query += $" and PositionId = '{positionId}'";
                }
            }
            else
            {
                if (departmentId != null)
                {
                    query += $" where DepartmentId = '{departmentId}'";
                    if (positionId != null)
                    {
                        query += $" and PositionId = '{positionId}'";
                    }
                }
                else
                {
                    if (positionId != null)
                    {
                        query += $" where PositionId = '{positionId}'";
                    }
                }
            }

            var totalItem = _dbConnection.Query<int>(query, commandType: CommandType.Text).FirstOrDefault();
            return totalItem;
        }
    }
}
