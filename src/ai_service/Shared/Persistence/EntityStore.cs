using Dapper;
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
            this IDbConnection connection,
            T entity,
            IDbTransaction? transaction = null,
            CancellationToken cancellationToken = default) where T : class
        {
            cancellationToken.ThrowIfCancellationRequested();

            var sql = entity.GenerateInsertSql();
            await connection.ExecuteAsync(sql, entity, transaction);
        }

        public static async Task<TOut?> InsertAsync<T, TOut>(
            this IDbConnection connection,
            T entity,
            string returningField,
            IDbTransaction? transaction = null,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var sql = entity.GenerateInsertReturningSql(returningField);
            return await connection.ExecuteScalarAsync<TOut>(sql, entity, transaction);
        }

        public static async Task<IEnumerable<T>> RawSelectAsync<T>(
            this IDbConnection connection,
            string sql,
            object? parameters = null,
            IDbTransaction? transaction = null, 
            CancellationToken cancellationToken = default) where T : class
        {
            cancellationToken.ThrowIfCancellationRequested();

            return await connection.QueryAsync<T>(sql, parameters, transaction);
        }

        private static string GenerateInsertSql<T>(this T entity)
        {
            var type = typeof(T);

            if (_sqlCache.TryGetValue(type.GetCacheKey(TypeOfOperation.Insert), out string? sql))
                return sql;

            var tableName = type.GetTableName();
            var tableFields = type.GetTableFields();

            sql =
                $"INSERT INTO {tableName} ({string.Join(", ", tableFields.Select(f => $"{f}"))})" +
                $"VALUES ({string.Join(", ", tableFields.Select(f => $"@{f}"))})";

            _sqlCache.TryAdd(type.GetCacheKey(TypeOfOperation.Insert), sql);

            return sql;
        }

        private static string GenerateInsertReturningSql<T>(this T entity, string returningField)
        {
            return $"{entity.GenerateInsertSql()} RETURNING {returningField}";
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
