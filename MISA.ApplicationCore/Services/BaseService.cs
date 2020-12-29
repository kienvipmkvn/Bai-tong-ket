using MISA.ApplicationCore.Enums;
using MISA.ApplicationCore.Interfaces;
using MISA.ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.ApplicationCore.Services
{
    /// <summary>
    /// service dùng chung
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="MISA.ApplicationCore.Interfaces.IBaseService{T}" />
    /// cretedBy: dtkien1 (14/12/2020)
    public class BaseService<T> : IBaseService<T> where T : BaseEntity
    {
        IBaseRepository<T> _baseRepository;
        ServiceResult _serviceResult;
        public BaseService(IBaseRepository<T> baseRepository)
        {
            _baseRepository = baseRepository;
            _serviceResult = new ServiceResult() { MISACode = MISACode.Success };
        }

        public virtual ServiceResult Add(T entity)
        {
            entity.EntityState = EntityState.AddNew;
            // Thực hiện validate
            var isValidate = Validate(entity);

            if (isValidate == true)
            {
                var rowEffected = _baseRepository.Add(entity);
                if (rowEffected == 0)
                {
                    throw new Exception($"Thêm mới không thành công :((");
                }
                _serviceResult.Data = rowEffected;
                _serviceResult.MISACode = MISACode.IsValid;
                return _serviceResult;
            }
            else
            {
                return _serviceResult;
            }

        }

        public ServiceResult Delete(object entityId)
        {
            var entityWithId = _baseRepository.GetById(entityId);
            if (entityWithId == null)
            {
                throw new Exception($"{typeof(T).Name}Id không có trong database");
            }

            var rowEffected = _baseRepository.Delete(entityId); 
            if (rowEffected == 0)
            {
                throw new Exception($"Xoá không thành công :((");
            }

            _serviceResult.Data = rowEffected;
            return _serviceResult;
        }

        public IEnumerable<T> GetEntities()
        {
            return _baseRepository.GetEntities();
        }

        public T GetById(Guid entityId)
        {
            return _baseRepository.GetById(entityId);
        }

        public ServiceResult Update(T entity)
        {
            entity.EntityState = EntityState.Update;
            var entityWithId = _baseRepository.GetById(entity.GetType().GetProperty($"{typeof(T).Name}Id").GetValue(entity));
            
            if (entityWithId == null)
            {
                throw new Exception($"{typeof(T).Name}Id không có trong database");
            }

            var isValidate = Validate(entity);
            if (isValidate == true)
            {
                var rowEffected = _baseRepository.Update(entity);

                if (rowEffected == 0)
                {
                    throw new Exception($"Cập nhật không thành công :((");
                }

                _serviceResult.Data = rowEffected;
                _serviceResult.MISACode = MISACode.IsValid;
                return _serviceResult;
            }
            else
            {
                return _serviceResult;
            }
        }

        /// <summary>
        /// kiểm tra dữ liệu có hợp lệ hay không
        /// </summary>
        /// <param name="entity">The entity.</param>
        private bool Validate(T entity)
        {
            var mesArrayError = new List<string>();
            var isValidate = true;
            // Đọc các Property
            var properties = entity.GetType().GetProperties();
            foreach (var property in properties)
            {
                var propertyValue = property.GetValue(entity);
                var displayName = string.Empty;
                var displayNameAttributes = property.GetCustomAttributes(typeof(DisplayName), true);
                if (displayNameAttributes.Length > 0)
                {
                    displayName = (displayNameAttributes[0] as DisplayName).Name;
                }
                // Kiểm tra xem có attribute cần phải validate không:
                if (property.IsDefined(typeof(Required), false))
                {
                    // Check bắt buộc nhập
                    if (propertyValue == null || propertyValue.ToString().Equals(string.Empty))
                    {
                        isValidate = false;
                        mesArrayError.Add(Properties.Resources.Msg_Required.Replace("{0}", displayName));
                        _serviceResult.MISACode = MISACode.NotValid;
                        _serviceResult.Messenger = Properties.Resources.Msg_IsNotValid;
                    }
                }

                if (property.IsDefined(typeof(CheckDuplicate), false))
                {
                    // Check trùng dữ liệu
                    if (propertyValue != null)
                    {
                        var propertyName = property.Name;
                        var entityDuplicate = _baseRepository.GetByProperty(entity, property);
                        if (entityDuplicate != null)
                        {
                            isValidate = false;
                            mesArrayError.Add(Properties.Resources.Msg_Duplicate.Replace("{0}", displayName));
                            _serviceResult.MISACode = MISACode.NotValid;
                            _serviceResult.Messenger = Properties.Resources.Msg_Duplicate.Replace("{0}", displayName);
                        }
                    }
                }

                if (property.IsDefined(typeof(MaxLength), false))
                {
                    // Lấy độ dài đã khai báo
                    var attributeMaxLength = property.GetCustomAttributes(typeof(MaxLength), true)[0];
                    var length = (attributeMaxLength as MaxLength).Value;
                    var msg = (attributeMaxLength as MaxLength).ErrorMsg;
                    if (propertyValue != null && propertyValue.ToString().Trim().Length > length)
                    {
                        isValidate = false;
                        mesArrayError.Add(msg ?? Properties.Resources.Msg_MaxLength.Replace("{0}", length.ToString()).Replace("{1}", displayName));
                        _serviceResult.MISACode = MISACode.NotValid;
                        _serviceResult.Messenger = Properties.Resources.Msg_IsNotValid;
                    }
                }
            }
            _serviceResult.Data = mesArrayError;
            if (isValidate == true)
                isValidate = ValidateCustom(entity);
            return isValidate;
        }

        /// <summary>
        /// Hàm thực hiện kiểm tra dữ liệu/ nghiệp vụ tùy chỉnh
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected virtual bool ValidateCustom(T entity)
        {
            return true;
        }
    }
}
