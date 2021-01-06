using MISA.ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Text;
using MISA.ApplicationCore.Enums;

namespace MISA.ApplicationCore.Entities
{
    /// <summary>
    /// Nhân viên
    /// </summary>
    /// createdBy: dtKien (29/12/2020)
    public class Employee : BaseEntity
    {
        /// <summary>
        /// Khoá chính
        /// </summary>
        [PrimaryKey]
        public Guid EmployeeId { get; set; }

        /// <summary>
        /// Mã nhân viên
        /// </summary>
        [Required]
        [CheckDuplicate]
        [DisplayName("Mã nhân viên")]
        [MaxLength(20, "Mã nhân viên đã vượt quá 20 ký tự cho phép")]
        public string EmployeeCode { get; set; }

        /// <summary>
        /// Họ và tên
        /// </summary>
        [Required]
        [DisplayName("Họ và tên")]
        public string FullName { get; set; }

        /// <summary>
        /// Ngày sinh
        /// </summary>
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        [Required]
        [DisplayName("Email")]
        [Pattern("^(([^<>()[\\]\\.,;:\\s@\\\"]+(\\.[^<>()[\\]\\.,;:\\s@\\\"]+)*)|(\\\".+\\\"))@(([^<>()[\\]\\.,;:\\s@\\\"]+\\.)+[^<>()[\\]\\.,;:\\s@\\\"]{2,})$")]
        public string Email { get; set; }

        /// <summary>
        /// Số điện thoại
        /// </summary>
        [CheckDuplicate]
        [Required]
        [DisplayName("Số điện thoại")]
        [Pattern("^(\\+)?([0-9]){8,12}$")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Giới tính
        /// </summary>
        public Gender? Gender { get; set; }

        /// <summary>
        /// Số CMT/CCCD
        /// </summary>
        [Required]
        [CheckDuplicate]
        [DisplayName("Số CMT/CCCD")]
        [Pattern("(^([0-9]){9}$)|(^([0-9]){12}$)")]
        public string IdentityNumber { get; set; }

        /// <summary>
        /// Ngày cấp CMT/CCCD
        /// </summary>
        public DateTime? IdentityDate { get; set; }

        /// <summary>
        /// Nơi cấp CMT/CCCD
        /// </summary>
        public string IdentityPlace { get; set; }

        /// <summary>
        /// Mã bộ phận
        /// </summary>
        public Guid? DepartmentId { get; set; }
        /// <summary>
        /// Tên bộ phận
        /// </summary>
        public string DepartmentName { get; set; }

        /// <summary>
        /// Mã vị trí
        /// </summary>
        public Guid? PositionId { get; set; }

        /// <summary>
        /// Tên vị trí
        /// </summary>
        public string PositionName { get; set; }

        /// <summary>
        /// Mã số thuế cá nhân
        /// </summary>
        public string PersonalTaxCode { get; set; }

        /// <summary>
        /// Lương
        /// </summary>
        public double? Salary { get; set; }

        /// <summary>
        /// Ngày gia nhập công ty
        /// </summary>
        public DateTime? JoinDate { get; set; }

        /// <summary>
        /// Trạng thái công việc
        /// </summary>
        public WorkStatus? WorkStatus { get; set; }
        public string WorkStatusName
        {
            get
            {
                var name = string.Empty;
                switch (WorkStatus)
                {
                    case Enums.WorkStatus.Resign:
                        name = Properties.Resources.Enum_WorkStatus_Resign;
                        break;
                    case Enums.WorkStatus.Working:
                        name = Properties.Resources.Enum_WorkStatus_Working;
                        break;
                    case Enums.WorkStatus.TrailWork:
                        name = Properties.Resources.Enum_WorkStatus_TrailWork;
                        break;
                    case Enums.WorkStatus.Retired:
                        name = Properties.Resources.Enum_WorkStatus_Retired;
                        break;
                    default:
                        break;
                }
                return name;
            }
        }
    }
}
