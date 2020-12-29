using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.Entity.Models
{
    public class PaginationModel<T>
    {
        public int TotalItem { get; set; }
        public int TotalPage { get; set; }
        public IEnumerable<T> Entities { get; set; }
    }
}
