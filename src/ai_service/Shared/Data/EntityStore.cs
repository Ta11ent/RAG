﻿using Dapper;
using System.Collections.Concurrent;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Reflection;

namespace AI_service.Shared.Data
{
    public static class EntityStore
    {
        private static readonly ConcurrentDictionary<string, string> _sqlCache = new();
        private static readonly ConcurrentDictionary<Type, string[]> _fieldCache = new();

        public static async Task InsertAsync<T>(
            this IUnitOfWork uow,
            T entity,
            CancellationToken cancellationToken = default) where T : class
        {
            cancellationToken.ThrowIfCancellationRequested();

            try
            {
                var sql = entity.GenerateInsertSql();
                await uow.Connection.ExecuteAsync(sql, entity, uow.Transaction);
            }
            catch
            {
                throw;
            }
        }

        public static async Task<IEnumerable<T>> RawSelectAsync<T>(
            this IUnitOfWork uow,
            string sql,
            object? parameters = null,
            CancellationToken cancellationToken = default) where T : class
        {
            cancellationToken.ThrowIfCancellationRequested();

            try
            {
                return await uow.Connection.QueryAsync<T>(sql, parameters, uow.Transaction);
            }
            catch
            {
                throw;
            }
        }

        private static string GenerateInsertSql<T>(this T entity)
        {
            var type = typeof(T);

            if (_sqlCache.TryGetValue(type.GetCacheKey(TypeOfOperation.Insert), out string? sql))
                return sql;

            var tableName = type.GetTableName();
            var tableFields = type.GetTableFields();

            sql =
                $"INSERT INTO {tableName} ({string.Join(", ", tableFields)}) " +
                $"VALUES ({string.Join(", ", tableFields.Select(f => "@" + f))})";

            _sqlCache.TryAdd(type.GetCacheKey(TypeOfOperation.Insert), sql);

            return sql;
        }

        private static string[] GetTableFields(this Type type)
        {
            if (_fieldCache.TryGetValue(type, out var fields))
                return fields;

            fields = type
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.GetCustomAttribute<NotMappedAttribute>() == null)
                .Select(x => x.Name)
                .ToArray();

            _fieldCache.TryAdd(type, fields);

            return fields;
        }

        private static string GetTableName(this Type type)
        {
            return type.GetCustomAttribute<TableAttribute>()?.Name ?? type.Name; 
        }

        private static string GetCacheKey(this Type type, TypeOfOperation operation)
        {
            var name = type.GetTableName();

            return operation switch
            {
                TypeOfOperation.Insert => $"{name}_insert",
                _ => throw new NotImplementedException()
            };
        }

        private enum TypeOfOperation
        {
            Insert
        }
    }
}
