using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.ApplicationCore.Models
{
    /// <summary>
    /// Nhóm khách hàng
    /// </summary>
    /// createdBy: dtkien1 (16/12/2020)
    public class CustomerGroup : BaseEntity
    {
        #region Constructor
        public CustomerGroup() { }
        #endregion

        #region Properties
        /// <summary>
        /// Khoá chính
        /// </summary>
        [PrimaryKey]
        public Guid CustomerGroupId { get; set; }
        /// <summary>
        /// Tên nhóm khách hàng
        /// </summary>
        [Required]
        public string CustomerGroupName { get; set; }
        /// <summary>
        /// Mô tả
        /// </summary>
        public string Description { get; set; }
        #endregion
    }
}
