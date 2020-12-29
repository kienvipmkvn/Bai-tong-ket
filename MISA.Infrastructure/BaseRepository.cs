using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using System.Data;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Reflection;
using MISA.ApplicationCore.Models;
using MISA.ApplicationCore.Interfaces;
using MISA.ApplicationCore.Enum;

namespace MISA.Infrastructure
{
    /// <summary>
    /// Repository dùng chung
    /// </summary>
    /// <seealso cref="MISA.Infrastructure.Interfaces.IBaseRepository{T}" />
    /// <seealso cref="System.IDisposable" />
    /// createdBy: dtkien1 (14/12/2020)
    public class BaseRepository<T> : IBaseRepository<T>, IDisposable where T : BaseEntity
    {
        #region DECLARE
        IConfiguration _configuration;
        string _connectionString = string.Empty;
        protected IDbConnection _dbConnection = null;
        protected string _tableName;
        #endregion

        public BaseRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("MISACukCukConnectionString");
            _dbConnection = new MySqlConnection(_connectionString);
            _tableName = typeof(T).Name;
        }

        public int Add(T entity)
        {
            var rowAffects = 0;
            _dbConnection.Open();
            using (var transaction = _dbConnection.BeginTransaction())
            {
                var parameters = MappingDbType(entity);
                rowAffects = _dbConnection.Execute($"Proc_Insert{_tableName}", parameters, commandType: CommandType.StoredProcedure);
                transaction.Commit();
            }
            return rowAffects;
        }


        public int Delete(object entityId)
        {
            var res = 0;
            _dbConnection.Open();
            using (var transaction = _dbConnection.BeginTransaction())
            {
                res = _dbConnection.Execute($"DELETE FROM {_tableName} WHERE {_tableName}Id = '{entityId}'", commandType: CommandType.Text);
                transaction.Commit();
            }
            return res;
        }

        public T GetById(object entityId)
        {
            var entities = _dbConnection.Query<T>($"SELECT * FROM {_tableName} WHERE {_tableName}Id = @entityId", new { entityId }, commandType: CommandType.Text)
                .FirstOrDefault();
            return entities;
        }

        public IEnumerable<T> GetEntities()
        {
            var entities = _dbConnection.Query<T>($"SELECT * FROM {_tableName} Order by CreatedDate", commandType: CommandType.Text);
            return entities;
        }

        public IEnumerable<T> GetEntities(string storeName)
        {
            var entities = _dbConnection.Query<T>($"{storeName}", commandType: CommandType.StoredProcedure);
            return entities;
        }

        public int Update(T entity)
        {
            var parameters = MappingDbType(entity);
            var rowAffects = _dbConnection.Execute($"Proc_Update{_tableName}", parameters, commandType: CommandType.StoredProcedure);
            return rowAffects;
        }

        /// <summary>
        /// Lấy dữ liệu qua giá trị của property
        /// </summary>
        /// <returns>phần tử đầu tiên thoả mãn điều kiện</returns>
        public T GetByProperty(T entity, PropertyInfo property)
        {
            var propertyName = property.Name;
            var propertyValue = property.GetValue(entity);
            var keyValue = entity.GetType().GetProperty($"{_tableName}Id").GetValue(entity);
            string query;
            if (entity.EntityState == EntityState.AddNew)
                query = $"SELECT * FROM {_tableName} WHERE {propertyName} = '{propertyValue}'";
            else if (entity.EntityState == EntityState.Update)
                query = $"SELECT * FROM {_tableName} WHERE {propertyName} = '{propertyValue}' AND {_tableName}Id <> '{keyValue}'";
            else
                return null;
            var entityReturn = _dbConnection.Query<T>(query, commandType: CommandType.Text).FirstOrDefault();
            return entityReturn;
        }
        public void Dispose()
        {
            if (_dbConnection.State == ConnectionState.Open)
                _dbConnection.Close();
        }

        /// <summary>
        /// Mappings kiểu dữ liệu trong c# với database
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>parameter của store procedure</returns>
        private DynamicParameters MappingDbType(T entity)
        {
            var properties = entity.GetType().GetProperties();
            var parameters = new DynamicParameters();
            foreach (var property in properties)
            {
                var propertyName = property.Name;
                var propertyValue = property.GetValue(entity);
                var propertyType = property.PropertyType;
                if (propertyValue == null)
                {
                    parameters.Add($"@{propertyName}", null);
                    continue;
                }
                else
                {
                    if (propertyType == typeof(Guid) || propertyType == typeof(Guid?))
                    {
                        parameters.Add($"@{propertyName}", propertyValue, DbType.String);
                    }
                    else if (propertyType == typeof(bool) || propertyType == typeof(bool?))
                    {
                        var dbValue = ((bool)propertyValue == true ? 1 : 0);
                        parameters.Add($"@{propertyName}", dbValue, DbType.Int32);
                    }
                    else
                    {
                        parameters.Add($"@{propertyName}", propertyValue);
                    }
                }
            }
            return parameters;
        }
    }
}
