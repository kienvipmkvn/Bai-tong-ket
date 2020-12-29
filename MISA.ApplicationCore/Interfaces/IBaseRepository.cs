using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace MISA.ApplicationCore.Interfaces
{
    /// <summary>
    /// createdBy: dtkien (29/12/2020)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBaseRepository<T>
    {
        /// <summary>
        /// Lấy toàn bộ dữ liệu
        /// </summary>
        /// <returns>entities</returns>
        IEnumerable<T> GetEntities();

        /// <summary>
        /// Lấy dữ liệu từ store procedure
        /// </summary>
        /// <param name="storeName">Tên store procedure.</param>
        /// <returns>entities</returns>
        IEnumerable<T> GetEntities(string storeName);

        /// <summary>
        /// Lấy dữ liệu theo id.
        /// </summary>
        /// <param name="entityId">id của bản ghi.</param>
        /// <returns>entity</returns>
        T GetById(object entityId);

        /// <summary>
        /// Thêm dữ liệu
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>Số hàng được thêm</returns>
        int Add(T entity);

        /// <summary>
        /// Cập nhật dữ liệu
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>Số bản ghi thay đổi</returns>
        int Update(T entity);

        /// <summary>
        /// Xoá 1 bản ghi 
        /// </summary>
        /// <param name="entityId">id của bản ghi</param>
        /// <returns>Số hàng bị xoá</returns>
        int Delete(object entityId);

        /// <summary>
        /// Lấy dữ liệu theo một trường cụ thể
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="property">The property.</param>
        /// <returns>entity</returns>
        T GetByProperty(T entity, PropertyInfo property);
    }
}
