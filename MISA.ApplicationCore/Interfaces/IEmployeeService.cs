using MISA.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.ApplicationCore.Interfaces
{
    /// <summary>
    /// employee service
    /// createdBy: dtkien (23/12/2020)
    /// </summary>
    public interface IEmployeeService : IBaseService<Employee>
    {
        /// <summary>
        /// Phân trang
        /// </summary>
        /// <param name="pageSize">số bản ghi 1 trang</param>
        /// <param name="pageIndex">số thứ tự trang</param>
        /// <param name="searchKey">từ khoá tìm kiếm</param>
        /// <returns></returns>
        IEnumerable<Employee> GetEntityPaging(int pageSize, int pageIndex, string searchKey);
        /// <summary>
        /// số lượng bản ghi thoả mãn điều kiện
        /// </summary>
        /// <param name="searchKey">từ khoá tìm kiếm</param>
        /// <param name="customerGroupId">nhóm khách hàng</param>
        /// <returns></returns>
        int GetCountCondition(string searchKey);
    }
}
