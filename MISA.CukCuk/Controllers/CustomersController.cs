using System;
using Microsoft.AspNetCore.Mvc;
using MISA.ApplicationCore.Interfaces;
using MISA.CukCuk.Controllers;
using MISA.ApplicationCore.Models;
using MISA.ApplicationCore.Enum;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MISA.CukCuk.Web.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CustomersController : BaseEntityController<Customer>
    {
        ICustomerService _customerService;
        public CustomersController(ICustomerService customerService) : base(customerService)
        {
            _customerService = customerService;
        }

        /// <summary>
        /// Lấy dữ liệu phân trang
        /// </summary>
        /// <param name="pageSize">Số lượng bản ghi trong 1 trang.</param>
        /// <param name="pageIndex">Số thứ tự trang.</param>
        /// <returns></returns>
        [HttpGet("{pageSize}/{pageIndex}")]
        public IActionResult GetPageCustomer(int pageSize, int pageIndex, string searchKey = "", Guid? entityGroupId = null)
        {
            if (searchKey == null) searchKey = "";
            if (searchKey.Contains("'") || searchKey.Contains("\"")) throw new Exception("Không được dùng dấu nháy");
            var entities = _customerService.GetEntityPaging(pageSize, pageIndex, searchKey, entityGroupId);
            var totalItem = _customerService.GetCountCondition(searchKey, entityGroupId);
            var totalPage = (int) Math.Ceiling(1.0*totalItem / pageSize);
            if(entities != null)
            {
                var rs = new ServiceResult()
                {
                    Data = new PaginationModel<Customer>()
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
