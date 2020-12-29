using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.Entity.Models
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
        /// Mã nhóm khách hàng
        /// </summary>
        [Required]
        [DisplayName("Mã nhóm khách hàng")]
        [MaxLength(20, "Mã nhóm khách hàng đã vượt quá 20 ký tự cho phép")]
        public string CustomerGroupCode { get; set; }
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
