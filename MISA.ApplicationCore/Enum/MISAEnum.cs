﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.ApplicationCore.Enums
{
    /// <summary>
    /// MISACode để xác định trạng thái của việc validate
    /// </summary>
    /// createdBy: dtkien1 (29/12/2020)
    public enum MISACode
    {
        /// <summary>
        /// Dữ liệu hợp lệ
        /// </summary>
        IsValid = 100,

        /// <summary>
        /// Dữ liệu chưa hợp lệ
        /// </summary>
        NotValid = 900,

        /// <summary>
        /// Thành công
        /// </summary>
        Success = 200,
        Exception = 500
    }

    /// <summary>
    /// Xác định trạng thái của Object
    /// </summary>
    public enum EntityState
    {
        AddNew = 1,
        Update = 2,
        Delete = 3,
    }

    /// <summary>
    /// Thông tin giới tính
    /// </summary>
    public enum Gender
    {
        /// <summary>
        /// Nữ
        /// </summary>
        Female,

        /// <summary>
        /// Nam
        /// </summary>
        Male,

        /// <summary>
        /// Khác
        /// </summary>
        Other,

        /// <summary>
        /// Chưa xác định
        /// </summary>
        Unknow,
    }

    /// <summary>
    /// Enum tình trạng công việc
    /// </summary>
    public enum WorkStatus
    {
        /// <summary>
        /// Đã nghỉ việc
        /// </summary>
        Resign,

        /// <summary>
        /// Đang làm việc
        /// </summary>
        Working,

        /// <summary>
        /// Đang thử việc
        /// </summary>
        TrailWork,

        /// <summary>
        /// Đã nghỉ hưu
        /// </summary>
        Retired
    }
}
