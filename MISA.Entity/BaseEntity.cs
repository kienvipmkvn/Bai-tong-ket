﻿using MISA.Entity.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.Entity
{
    #region Khai báo custom attributes
    [AttributeUsage(AttributeTargets.Property)]
    public class Required : Attribute
    {

    }

    [AttributeUsage(AttributeTargets.Property)]
    public class CheckDuplicate : Attribute
    {

    }

    [AttributeUsage(AttributeTargets.Property)]
    public class DisplayName : Attribute
    {
        public string Name { get; set; }
        public DisplayName(string name = null)
        {
            this.Name = name;
        }

    }

    [AttributeUsage(AttributeTargets.Property)]
    public class PrimaryKey : Attribute
    {

    }

    [AttributeUsage(AttributeTargets.Property)]
    public class MaxLength : Attribute
    {
        public int Value { get; set; }
        public string ErrorMsg { get; set; }
        public MaxLength(int lengh, string erroMsg = null)
        {
            this.Value = lengh;
            this.ErrorMsg = erroMsg;
        }
    }
    #endregion

    /// <summary>
    /// Entity dùng chung
    /// </summary>
    /// createdBy: dtkien1 (14/12/2020)
    public class BaseEntity
    {
        //Trạng thái object
        public EntityState EntityState { get; set; } = EntityState.AddNew;
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
    }
}
