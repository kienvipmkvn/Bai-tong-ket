using MISA.ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Text;
using MISA.ApplicationCore.Enum;

namespace MISA.ApplicationCore.Entities
{
    /// <summary>
    /// Nhân viên
    /// </summary>
    /// createdBy: dtKien (23/12/2020)
    public class Employee : BaseEntity
    {
        /// <summary>
        /// khoá chính
        /// </summary>
        [PrimaryKey]
        public Guid EmployeeId { get; set; }
        /// <summary>
        /// mã nhân viên
        /// </summary>
        [Required]
        [CheckDuplicate]
        [DisplayName("Mã nhân viên")]
        [MaxLength(20, "Mã nhân viên đã vượt quá 20 ký tự cho phép")]
        public string EmployeeCode { get; set; }
        /// <summary>
        /// tên
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// họ
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// họ và tên
        /// </summary>
        [Required]
        [DisplayName("Họ và tên")]
        public string FullName { get; set; }
        /// <summary>
        /// ngày sinh
        /// </summary>
        public DateTime? DateOfBirth { get; set; }
        /// <summary>
        /// email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// số điện thoại
        /// </summary>
        [CheckDuplicate]
        [Required]
        [DisplayName("Số điện thoại")]
        public string PhoneNumber { get; set; }
        /// <summary>
        /// giới tính
        /// </summary>
        public Gender? Gender { get; set; }
        /// <summary>
        /// địa chỉ
        /// </summary>
        public string Address { get; set; }
        public string IdentityNumber { get; set; }
        public DateTime? IdentityDate { get; set; }
        public string IdentityPlace { get; set; }
        public int? MaritalStatus { get; set; }
        /// <summary>
        /// trình độ học vấn
        /// </summary>
        public int? EducationalBackground { get; set; }
        public Guid? QualificationId { get; set; }
        /// <summary>
        /// mã bộ phận
        /// </summary>
        public Guid? DepartmentId { get; set; }
        /// <summary>
        /// mã vị trí
        /// </summary>
        public Guid? PositionId { get; set; }
        public int? WorkStatus { get; set; }
        /// <summary>
        /// mã số thuế cá nhân
        /// </summary>
        public string PersonalTaxCode { get; set; }
        /// <summary>
        /// lương
        /// </summary>
        public double? Salary { get; set; }
        /// <summary>
        /// ngày gia nhập công ty
        /// </summary>
        public DateTime? JoinDate { get; set; }
    }
}
