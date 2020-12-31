using MISA.ApplicationCore.Entities;
using MISA.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.ApplicationCore.Services
{
    public class EmployeeService : BaseService<Employee>, IEmployeeService
    {
        IEmployeeRepository _employeeRepository;
        #region Constructor
        public EmployeeService(IEmployeeRepository employeeRepository) : base(employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        #endregion
        protected override bool ValidateCustom(Employee entity)
        {
            return true;
        }

        public IEnumerable<Employee> GetEntityPaging(int pageSize, int pageIndex, string searchKey, Guid? departmentId = null, Guid? positionId = null)
        {
            return _employeeRepository.GetEntityPaging(pageSize, pageSize * pageIndex, searchKey, departmentId, positionId);
        }

        public int GetCountCondition(string searchKey, Guid? departmentId = null, Guid? positionId = null)
        {
            return _employeeRepository.GetCountCondition(searchKey, departmentId, positionId);
        }
    }
}
