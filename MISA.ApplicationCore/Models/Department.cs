using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.ApplicationCore.Models
{
    /// <summary>
    /// Phòng ban
    /// </summary>
    /// createdBy: dtkien1 (29/12/2020)
    public class Department : BaseEntity
    {
        #region Constructor
        public Department() { }
        #endregion

        #region Properties
        /// <summary>
        /// Khoá chính
        /// </summary>
        [PrimaryKey]
        public Guid DepartmentId { get; set; }

        /// <summary>
        /// Tên phòng ban
        /// </summary>
        [Required]
        public string DepartmentName { get; set; }

        /// <summary>
        /// Mô tả
        /// </summary>
        public string Description { get; set; }
        #endregion
    }
}
