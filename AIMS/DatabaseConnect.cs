using AIMS.Models.Entities;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace AIMS
{
    public class DatabaseConnect
    {
        private static DatabaseConnect _Instance;
        public static DatabaseConnect gI()
        {
            return _Instance == null ? _Instance = new DatabaseConnect() : _Instance;
        }
        public string ConnectionString = "User ID=postgres.dwsijitgwefuomejoime;Password=9Tb9eeaw1vsmClOd;Host=aws-0-ap-southeast-1.pooler.supabase.com;Port=6543;Database=postgres;Pooling=true;Timeout=60;CommandTimeout=60;Connection Lifetime=60;Multiplexing=true";
        public NpgsqlConnection vConnection;

        public async Task ConnectAsync()
        {
            vConnection = new NpgsqlConnection(ConnectionString);
            if (vConnection.State == ConnectionState.Closed)
            {
                await vConnection.OpenAsync();
            }
        }
        public async Task<List<T>> SelectDataAsync<T>(string table, Func<NpgsqlDataReader, T> mapFunction, string where = null, Dictionary<string, object> parameters = null, string orderBy = null, bool ascending = true)
        {
            List<T> resultList = new List<T>();
            try
            {
                await ConnectAsync();

                string sql = $"SELECT * FROM {table}";
                if (!string.IsNullOrEmpty(where))
                {
                    sql += $" WHERE {where}";
                }
                if (!string.IsNullOrEmpty(orderBy))
                {
                    string orderDirection = ascending ? "ASC" : "DESC";
                    sql += $" ORDER BY {orderBy} {orderDirection}";
                }

                using (NpgsqlCommand command = new NpgsqlCommand(sql, vConnection))
                {
                    command.CommandTimeout = 60;
                    AddParametersToCommand(command, parameters);

                    using (NpgsqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            resultList.Add(mapFunction(reader));
                        }
                    }
                }
                await CloseConnectionAsync();
            }
            catch
            {

            }
            return resultList;
        }

        public async Task<int> SelectCountAsync(string table, string where = null, Dictionary<string, object> parameters = null)
        {
            await ConnectAsync();
            int count = 0;

            string sql = $"SELECT COUNT(*) FROM {table}";
            if (!string.IsNullOrEmpty(where))
            {
                sql += $" WHERE {where}";
            }

            using (NpgsqlCommand command = new NpgsqlCommand(sql, vConnection))
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
            await CloseConnectionAsync();
            return count;
        }

        public async Task<int> UpdateDataAsync(string table, Dictionary<string, object> setValues, string where = null, Dictionary<string, object> parameters = null)
        {
            await ConnectAsync();
            int rowsAffected = 0;

            string sql = $"UPDATE {table} SET ";
            List<string> setClauses = new List<string>();

            foreach (KeyValuePair<string, object> setValue in setValues)
            {
                setClauses.Add($"{setValue.Key} = @set_{setValue.Key}");
                if (parameters == null)
                {
                    parameters = new Dictionary<string, object>();
                }
                parameters.Add($"set_{setValue.Key}", setValue.Value);
            }
            sql += string.Join(", ", setClauses);

            if (!string.IsNullOrEmpty(where))
            {
                sql += $" WHERE {where}";
            }

            using (NpgsqlCommand command = new NpgsqlCommand(sql, vConnection))
            {
                command.CommandTimeout = 60; 
                AddParametersToCommand(command, parameters);

                try
                {
                    rowsAffected = await command.ExecuteNonQueryAsync();
                }
                catch (NpgsqlException ex)
                {
                    Console.WriteLine($"Error updating data: {ex.Message}");
                }
            }
            await CloseConnectionAsync();
            return rowsAffected;
        }

        public async Task<int> DeleteDataAsync(string table, string where = null, Dictionary<string, object> parameters = null)
        {
            await ConnectAsync();
            int rowsAffected = 0;

            string sql = $"DELETE FROM {table}";
            if (!string.IsNullOrEmpty(where))
            {
                sql += $" WHERE {where}";
            }

            using (NpgsqlCommand command = new NpgsqlCommand(sql, vConnection))
            {
                command.CommandTimeout = 60;
                AddParametersToCommand(command, parameters);

                try
                {
                    rowsAffected = await command.ExecuteNonQueryAsync();
                }
                catch (NpgsqlException ex)
                {
                    Console.WriteLine($"Error deleting data: {ex.Message}");
                }
            }
            await CloseConnectionAsync();
            return rowsAffected;
        }

        public async Task<int> InsertDataAsync(string table, Dictionary<string, object> values)
        {
            await ConnectAsync();
            int rowsAffected = 0;

            string columns = string.Join(", ", values.Keys);
            string parameterNames = string.Join(", ", values.Keys.Select(key => "@" + key));

            string sql = $"INSERT INTO {table} ({columns}) VALUES ({parameterNames})";

            using (NpgsqlCommand command = new NpgsqlCommand(sql, vConnection))
            {
                command.CommandTimeout = 60; 
                AddParametersToCommand(command, values);

                try
                {
                    rowsAffected = await command.ExecuteNonQueryAsync(); 
                }
                catch (NpgsqlException ex)
                {
                    Console.WriteLine($"Error inserting data: {ex.Message}");
                }
            }
            await CloseConnectionAsync();
            return rowsAffected;
        }

        private void AddParametersToCommand(NpgsqlCommand command, Dictionary<string, object> parameters)
        {
            if (parameters != null)
            {
                foreach (KeyValuePair<string, object> parameter in parameters)
                {
                    command.Parameters.AddWithValue(parameter.Key, parameter.Value ?? DBNull.Value);
                }
            }
        }
        private async Task CloseConnectionAsync()
        {
            if (vConnection != null && vConnection.State == ConnectionState.Open)
            {
                await vConnection.CloseAsync();
            }
        }
    }
}