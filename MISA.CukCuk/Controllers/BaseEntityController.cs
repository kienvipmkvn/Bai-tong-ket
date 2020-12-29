using Microsoft.AspNetCore.Mvc;
using MISA.ApplicationCore.Enums;
using MISA.ApplicationCore.Interfaces;
using MISA.ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.CukCuk.Controllers
{
    /// <summary>
    /// Controller dùng chung
    /// </summary>
    /// <typeparam name="T">The type of the entity.</typeparam>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    /// createdBy: dtkien1 (14/12/2020)
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BaseEntityController<T> : ControllerBase
    {
        IBaseService<T> _baseService;
        public BaseEntityController(IBaseService<T> baseService)
        {
            _baseService = baseService;
        }

        //Lấy toàn bộ dữ liệu
        [HttpGet]
        public IActionResult Get()
        {
            var entities = _baseService.GetEntities();
            if (entities != null)
            {
                var rs = new ServiceResult()
                {
                    Data = entities,
                    Messenger = "Thành công!",
                    MISACode = MISACode.Success
                };
                return Ok(rs);
            }

            else return NoContent();
        }

        //Lấy dữ liệu theo id
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var entity = _baseService.GetById(id);
            if (entity != null)
            {
                var rs = new ServiceResult()
                {
                    Data = entity,
                    Messenger = "Thành công!",
                    MISACode = MISACode.Success
                };
                return Ok(rs);
            }

            else return NoContent();
        }

        //Thêm dữ liệu
        [HttpPost]
        public IActionResult Post([FromBody] T entity)
        {
            var serviceResult = _baseService.Add(entity);
            if (serviceResult.MISACode == MISACode.NotValid)
                return BadRequest(serviceResult);
            else
                return Ok(serviceResult);
        }

        //Cập nhập dữ liệu
        [HttpPut("{id}")]
        public IActionResult Put([FromRoute] string id, [FromBody] T entity)
        {
            //map kiểu của khoá chính trong entity với tham số id
            var keyProperty = entity.GetType().GetProperty($"{typeof(T).Name}Id");
            if (keyProperty.PropertyType == typeof(Guid))
            {
                keyProperty.SetValue(entity, Guid.Parse(id));
            }
            else if (keyProperty.PropertyType == typeof(int))
            {
                keyProperty.SetValue(entity, int.Parse(id));
            }
            else
            {
                keyProperty.SetValue(entity, id);
            }

            var serviceResult = _baseService.Update(entity);
            if (serviceResult.MISACode == MISACode.NotValid)
                return BadRequest(serviceResult);
            else
                return Ok(serviceResult);
        }

        //Xoá dữ liệu
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var res = _baseService.Delete(id);
            return Ok(res);
        }
    }
}
