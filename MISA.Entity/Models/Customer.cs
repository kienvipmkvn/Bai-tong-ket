﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.Entity.Models
{
    /// <summary>
    /// Khách hàng
    /// </summary>
    /// CreatedBy: dtkien1 (10/12/2020)
    public class Customer : BaseEntity
    {
        #region Declare
        #endregion

        #region Constructor
        public Customer() { }
        #endregion

        #region Properties        
        /// <summary>
        /// Khoá chính
        /// </summary>
        [PrimaryKey]
        public Guid CustomerId { get; set; }
        /// <summary>
        /// Mã khách hàng
        /// </summary>
        [Required]
        [CheckDuplicate]
        [DisplayName("Mã khách hàng")]
        [MaxLength(20, "Mã khách hàng đã vượt quá 20 ký tự cho phép")]
        public string CustomerCode { get; set; }
        /// <summary>
        /// Họ tên khách hàng
        /// </summary>
        public string FullName { get; set; }
        /// <summary>
        /// Số thẻ thành viên
        /// </summary>
        public string MemberCardCode { get; set; }
        /// <summary>
        /// Ngày sinh
        /// </summary>
        public DateTime? DateOfBirth { get; set; }
        /// <summary>
        /// Giới tính
        /// </summary>
        public int? Gender { get; set; }
        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Điện thoại
        /// </summary>
        [CheckDuplicate]
        [DisplayName("Số điện thoại")]
        public string PhoneNumber { get; set; }
        /// <summary>
        /// Tên công ty
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// Mã số thuế của công ty
        /// </summary>
        public string CompanyTaxCode { get; set; }
        /// <summary>
        /// Địa chỉ
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// Mã nhóm khách hàng
        /// </summary>
        public Guid? CustomerGroupId { get; set; }
        #endregion

        #region Methods
        #endregion

    }
}
