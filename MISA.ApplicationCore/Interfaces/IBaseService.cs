using MISA.ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.ApplicationCore.Interfaces
{
    /// <summary>
    /// service dùng chung
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// createdBy: dtkien (20/12/2020)
    public interface IBaseService<T>
    {
        /// <summary>
        /// Lấy toàn bộ dữ liệu
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> GetEntities();
        /// <summary>
        /// Lấy dữ liệu theo id
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        T GetById(Guid entityId);

        /// <summary>
        /// Thêm dữ liệu
        /// </summary>
        /// <param name="entity">dữ liệu cần thêm</param>
        /// <returns></returns>
        ServiceResult Add(T entity);
        /// <summary>
        /// Cập nhật dữ liệu
        /// </summary>
        /// <param name="entity">dữ liệu cần cập nhật</param>
        /// <returns></returns>
        ServiceResult Update(T entity);
        /// <summary>
        /// Xoá dữ liệu
        /// </summary>
        /// <param name="entityId">id của dữ liệu cần xoá</param>
        /// <returns></returns>
        ServiceResult Delete(object entityId);
    }
}
