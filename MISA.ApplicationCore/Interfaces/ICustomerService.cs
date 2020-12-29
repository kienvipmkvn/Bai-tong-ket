using MISA.ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.ApplicationCore.Interfaces
{
    /// <summary>
    /// Customer service
    /// </summary>
    /// <seealso cref="MISA.Infrastructure.BaseRepository{MISA.Entity.Models.Customer}" />
    /// <seealso cref="MISA.Infrastructure.Interfaces.ICustomerRepository" />
    /// createdBy: dtkien1 (14/12/2020)
    public interface ICustomerService : IBaseService<Customer>
    {
        /// <summary>
        /// Phân trang
        /// </summary>
        /// <param name="pageSize">số bản ghi 1 trang</param>
        /// <param name="pageIndex">số thứ tự trang</param>
        /// <param name="searchKey">từ khoá tìm kiếm</param>
        /// <param name="customerGroupId">nhóm khách hàng</param>
        /// <returns></returns>
        IEnumerable<Customer> GetEntityPaging(int pageSize, int pageIndex, string searchKey, Guid? customerGroupId);
        /// <summary>
        /// số lượng bản ghi thoả mãn điều kiện
        /// </summary>
        /// <param name="searchKey">từ khoá tìm kiếm</param>
        /// <param name="customerGroupId">nhóm khách hàng</param>
        /// <returns></returns>
        int GetCountCondition(string searchKey, Guid? customerGroupId);
    }
}
