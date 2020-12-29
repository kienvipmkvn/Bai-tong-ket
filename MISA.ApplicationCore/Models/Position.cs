using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.ApplicationCore.Models
{
    /// <summary>
    /// Vị trí, chức vụ
    /// </summary>
    /// createdBy: dtkien1 (29/12/2020)
    public class Position : BaseEntity
    {
        #region Constructor
        public Position() { }
        #endregion

        #region Properties
        /// <summary>
        /// Khoá chính
        /// </summary>
        [PrimaryKey]
        public Guid PositionId { get; set; }

        /// <summary>
        /// Tên vị trí, chức vụ
        /// </summary>
        [Required]
        public string PositionName { get; set; }

        /// <summary>
        /// Mô tả
        /// </summary>
        public string Description { get; set; }
        #endregion
    }
}
