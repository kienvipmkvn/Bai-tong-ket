using MISA.ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.ApplicationCore.Interfaces
{
    /// <summary>
    /// customer repository
    /// createdBy: dtkien (20/12/2020)
    /// </summary>
    public interface ICustomerRepository : IBaseRepository<Customer>
    {
        /// <summary>
        /// Phân trang
        /// </summary>
        /// <param name="pageSize">số bản ghi 1 trang</param>
        /// <param name="pageIndex">số thứ tự trang</param>
        /// <param name="searchKey">từ khoá tìm kiếm</param>
        /// <param name="customerGroupId">nhóm khách hàng</param>
        /// <returns></returns>
        IEnumerable<Customer> GetEntityPaging(int limit, int offset, string searchKey, Guid? customerGroupId);
        /// <summary>
        /// số lượng bản ghi thoả mãn điều kiện
        /// </summary>
        /// <param name="searchKey">từ khoá tìm kiếm</param>
        /// <param name="customerGroupId">nhóm khách hàng</param>
        /// <returns></returns>
        int GetCountCondition(string searchKey, Guid? customerGroupId);
    }
}
