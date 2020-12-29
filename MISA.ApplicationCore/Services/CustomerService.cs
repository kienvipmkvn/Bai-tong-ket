using MISA.ApplicationCore.Interfaces;
using MISA.ApplicationCore.Models;
using System;
using System.Collections.Generic;

namespace MISA.ApplicationCore.Services
{
    public class CustomerService : BaseService<Customer>, ICustomerService
    {
        ICustomerRepository _customerRepository;
        #region Constructor
        public CustomerService(ICustomerRepository customerRepository) : base(customerRepository)
        {
            _customerRepository = customerRepository;
        }
        #endregion
        protected override bool ValidateCustom(Customer entity)
        {
            return true;
        }

        public IEnumerable<Customer> GetEntityPaging(int pageSize, int pageIndex, string searchKey, Guid? customerGroupId)
        {
            return _customerRepository.GetEntityPaging(pageSize, pageSize * pageIndex, searchKey, customerGroupId);
        }

        public int GetCountCondition(string searchKey, Guid? customerGroupId)
        {
            return _customerRepository.GetCountCondition(searchKey, customerGroupId);
        }
    }
}
