﻿using MISA.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.ApplicationCore.Interfaces
{
    /// <summary>
    /// employee service
    /// createdBy: dtkien (29/12/2020)
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
        IEnumerable<Employee> GetEntityPaging(int pageSize, int pageIndex, string searchKey, Guid? departmentId = null, Guid? positionId = null);
        /// <summary>
        /// số lượng bản ghi thoả mãn điều kiện
        /// </summary>
        /// <param name="searchKey">từ khoá tìm kiếm</param>
        /// <param name="customerGroupId">nhóm khách hàng</param>
        /// <returns></returns>
        int GetCountCondition(string searchKey, Guid? departmentId = null, Guid? positionId = null);

        /// <summary>
        /// Lấy mã NV lớn nhất trong db
        /// </summary>
        /// <returns></returns>
        /// createdBy: dtkien (4/1/2021)
        string GetNextEmployeeCode();
    }
}
