using MISA.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.ApplicationCore.Interfaces
{
    /// <summary>
    /// employee repository
    /// createdBy: dtkien (29/12/2020)
    /// </summary>
    public interface IEmployeeRepository : IBaseRepository<Employee>
    {
        /// <summary>
        /// Phân trang
        /// </summary>
        /// <param name="pageSize">số bản ghi 1 trang</param>
        /// <param name="pageIndex">số thứ tự trang</param>
        /// <param name="searchKey">từ khoá tìm kiếm</param>
        /// <returns></returns>
        IEnumerable<Employee> GetEntityPaging(int limit, int offset, string searchKey, Guid? departmentId = null, Guid? positionId = null);
        /// <summary>
        /// số lượng bản ghi thoả mãn điều kiện
        /// </summary>
        /// <param name="searchKey">từ khoá tìm kiếm</param>
        /// <returns></returns>
        int GetCountCondition(string searchKey, Guid? departmentId = null, Guid? positionId = null);
    }
}
