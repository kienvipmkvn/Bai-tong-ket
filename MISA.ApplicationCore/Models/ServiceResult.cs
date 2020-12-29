using MISA.ApplicationCore.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.ApplicationCore.Models
{
    /// <summary>
    /// Kiểu dữ liệu trả về
    /// </summary>
    /// createdBy: dtkien1 (14/12/2020)
    public class ServiceResult
    {
        /// <summary>
        /// Dữ liệu trả về
        /// </summary>
        public object Data { get; set; }
        /// <summary>
        /// thông báo
        /// </summary>
        public string Messenger { get; set; }
        public MISACode MISACode { get; set; }
    }
}
