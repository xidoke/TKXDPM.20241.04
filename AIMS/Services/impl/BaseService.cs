using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace AIMS.Services.impl
{
    public abstract class BaseService<T, R> where T : BaseService<T, R>, new() where R : new()
    {
        protected BaseService() { }

        protected static DatabaseConnect dbConnect = DatabaseConnect.gI();

        public static async Task<List<R>> SelectDataAsync(string where = null,
            Dictionary<string, object> parameters = null, string orderBy = null, bool ascending = true)
        {
            var resultList = new List<R>();
            await dbConnect.ConnectAsync();

            string sql = $"SELECT * FROM {GetTableName()}";
            if (!string.IsNullOrEmpty(where))
                sql += $" WHERE {where}";

            if (!string.IsNullOrEmpty(orderBy))
                sql += $" ORDER BY {orderBy} {(ascending ? "ASC" : "DESC")}";

            using (var command = new NpgsqlCommand(sql, dbConnect.Connection))
            {
                AddParametersToCommand(command, parameters);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var entity = new R();
                        (new T()).Map(reader, entity);
                        resultList.Add(entity);
                    }
                }
            }

            await dbConnect.CloseConnectionAsync();
            return resultList;
        }

        public static async Task<int> InsertDataAsync(Dictionary<string, object> values)
        {
            await dbConnect.ConnectAsync();
            string columns = string.Join(", ", values.Keys);
            string parameterNames = string.Join(", ", values.Keys.Select(k => "@" + k));
            string sql = $"INSERT INTO {GetTableName()} ({columns}) VALUES ({parameterNames})";

            using (var command = new NpgsqlCommand(sql, dbConnect.Connection))
            {
                AddParametersToCommand(command, values);
                int rowsAffected = await command.ExecuteNonQueryAsync();
                await dbConnect.CloseConnectionAsync();
                return rowsAffected;
            }
        }

        public async Task<int> SelectCountAsync(string where = null, Dictionary<string, object> parameters = null)
        {
            await dbConnect.ConnectAsync();
            int count = 0;

            string sql = $"SELECT COUNT(*) FROM {GetTableName()}";
            if (!string.IsNullOrEmpty(where))
            {
                sql += $" WHERE {where}";
            }

            using (NpgsqlCommand command = new NpgsqlCommand(sql, dbConnect.Connection))
            {
                command.CommandTimeout = 60;
                AddParametersToCommand(command, parameters);

                try
                {
                    object result = await command.ExecuteScalarAsync();
                    if (result != null && result != DBNull.Value)
                    {
                        count = Convert.ToInt32(result);
                    }
                }
                catch (NpgsqlException ex)
                {
                    Console.WriteLine($"Error executing COUNT query: {ex.Message}");
                }
            }
            await dbConnect.CloseConnectionAsync();
            return count;
        }

        public static async Task<int> UpdateDataAsync(Dictionary<string, object> setValues, string where = null, Dictionary<string, object> parameters = null)
        {
            await dbConnect.ConnectAsync();
            string setClause = string.Join(", ", setValues.Keys.Select(k => $"{k} = @set_{k}"));

            if (parameters == null)
                parameters = new Dictionary<string, object>();
            foreach (var kvp in setValues)
                parameters.Add($"set_{kvp.Key}", kvp.Value);

            string sql = $"UPDATE {GetTableName()} SET {setClause}";
            if (!string.IsNullOrEmpty(where))
                sql += $" WHERE {where}";

            using (var command = new NpgsqlCommand(sql, dbConnect.Connection))
            {
                AddParametersToCommand(command, parameters);
                int rowsAffected = await command.ExecuteNonQueryAsync();
                await dbConnect.CloseConnectionAsync();
                return rowsAffected;
            }
        }

        public static async Task<int> DeleteDataAsync(string where = null, Dictionary<string, object> parameters = null)
        {
            await dbConnect.ConnectAsync();
            string sql = $"DELETE FROM {GetTableName()}";
            if (!string.IsNullOrEmpty(where))
                sql += $" WHERE {where}";

            using (var command = new NpgsqlCommand(sql, dbConnect.Connection))
            {
                AddParametersToCommand(command, parameters);
                int rowsAffected = await command.ExecuteNonQueryAsync();
                await dbConnect.CloseConnectionAsync();
                return rowsAffected;
            }
        }

        private static void AddParametersToCommand(NpgsqlCommand command, Dictionary<string, object> parameters)
        {
            if (parameters != null)
            {
                foreach (var parameter in parameters)
                {
                    command.Parameters.AddWithValue(parameter.Key, parameter.Value ?? DBNull.Value);
                }
            }
        }

        protected static string GetTableName()
        {
            return typeof(R).Name.ToLower();
        }

        protected abstract void Map(NpgsqlDataReader reader, R entity);
    }
}