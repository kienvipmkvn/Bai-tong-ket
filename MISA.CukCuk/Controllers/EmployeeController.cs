using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.ApplicationCore.Entities;
using MISA.ApplicationCore.Enums;
using MISA.ApplicationCore.Interfaces;
using MISA.ApplicationCore.Models;

namespace MISA.CukCuk.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmployeeController : BaseEntityController<Employee>
    {
        IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService) : base(employeeService)
        {
            _employeeService = employeeService;
        }

        /// <summary>
        /// Lấy dữ liệu phân trang
        /// </summary>
        /// <param name="pageSize">Số lượng bản ghi trong 1 trang.</param>
        /// <param name="pageIndex">Số thứ tự trang.</param>
        /// <returns></returns>
        [HttpGet("{pageSize}/{pageIndex}")]
        public IActionResult GetPageEmployee(int pageSize, int pageIndex, string searchKey = "", Guid? departmentId = null, Guid? positionId = null)
        {
            if (searchKey == null) searchKey = "";
            if (searchKey.Contains("'") || searchKey.Contains("\"")) throw new Exception("Không được dùng dấu nháy");
            var entities = _employeeService.GetEntityPaging(pageSize, pageIndex, searchKey, departmentId, positionId);
            var totalItem = _employeeService.GetCountCondition(searchKey, departmentId, positionId);
            var totalPage = (int)Math.Ceiling(1.0 * totalItem / pageSize);
            if (entities != null)
            {
                var rs = new ServiceResult()
                {
                    Data = new PaginationModel<Employee>()
                    {
                        Entities = entities,
                        TotalItem = totalItem,
                        TotalPage = totalPage
                    },
                    Messenger = "Thành công!",
                    MISACode = MISACode.Success
                };
                return Ok(rs);

            }
            else
            {
                return NoContent();
            }
        }
    }
}
