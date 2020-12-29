using MISA.ApplicationCore.Interfaces;
using MISA.ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.ApplicationCore.Services
{
    public class DepartmentService : BaseService<Department>, IDepartmentService
    {
        public DepartmentService(IDepartmentRepository departmentRepository) : base(departmentRepository)
        {

        }
    }
}
