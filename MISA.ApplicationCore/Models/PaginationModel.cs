using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.ApplicationCore.Models
{
    /// <summary>
    /// Lưu dữ liệu phân trang
    /// </summary>
    /// <typeparam name="T">Entity</typeparam>
    /// createdBy: dtkien1 (29/12/2020)
    public class PaginationModel<T>
    {
        /// <summary>
        /// tổng số bản ghi
        /// </summary>
        public int TotalItem { get; set; }
        /// <summary>
        /// tổng số trang
        /// </summary>
        public int TotalPage { get; set; }
        /// <summary>
        /// dữ liệu
        /// </summary>
        public IEnumerable<T> Entities { get; set; }
    }
}
