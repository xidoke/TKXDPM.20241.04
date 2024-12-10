using AIMS.Models.Entities;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace AIMS
{
    public class DatabaseConnect
    {
        private static DatabaseConnect _Instance;
        public static DatabaseConnect gI()
        {
            return _Instance == null ? _Instance = new DatabaseConnect() : _Instance;
        }

        public string ConnectionString = "User ID=postgres.dwsijitgwefuomejoime;Password=9Tb9eeaw1vsmClOd;Host=aws-0-ap-southeast-1.pooler.supabase.com;Port=6543;Database=postgres;Pooling=true;";
        public NpgsqlConnection vConnection;

        public void Connect()
        {
            vConnection = new NpgsqlConnection(ConnectionString);
            if (vConnection.State == ConnectionState.Closed)
            {
                vConnection.Open();
            }
        }

        public List<T> SelectData<T>(string table, Func<NpgsqlDataReader, T> mapFunction, string where = null, Dictionary<string, object> parameters = null, string orderBy = null, bool ascending = true)
        {
            Connect();
            List<T> resultList = new List<T>();

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
                AddParametersToCommand(command, parameters);

                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        resultList.Add(mapFunction(reader));
                    }
                }
            }
            vConnection.Close();
            return resultList;
        }

        public int SelectCount(string table, string where = null, Dictionary<string, object> parameters = null)
        {
            Connect();
            int count = 0;

            string sql = $"SELECT COUNT(*) FROM {table}";
            if (!string.IsNullOrEmpty(where))
            {
                sql += $" WHERE {where}";
            }

            using (NpgsqlCommand command = new NpgsqlCommand(sql, vConnection))
            {
                AddParametersToCommand(command, parameters);

                try
                {
                    object result = command.ExecuteScalar();
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
            vConnection.Close();
            return count;
        }

        public int UpdateData(string table, Dictionary<string, object> setValues, string where = null, Dictionary<string, object> parameters = null)
        {
            Connect();
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
                AddParametersToCommand(command, parameters);

                try
                {
                    rowsAffected = command.ExecuteNonQuery();
                }
                catch (NpgsqlException ex)
                {
                    Console.WriteLine($"Error updating data: {ex.Message}");
                }
            }
            vConnection.Close();
            return rowsAffected;
        }
        public int DeleteData(string table, string where = null, Dictionary<string, object> parameters = null)
        {
            Connect();
            int rowsAffected = 0;

            string sql = $"DELETE FROM {table}";
            if (!string.IsNullOrEmpty(where))
            {
                sql += $" WHERE {where}";
            }

            using (NpgsqlCommand command = new NpgsqlCommand(sql, vConnection))
            {
                AddParametersToCommand(command, parameters);

                try
                {
                    rowsAffected = command.ExecuteNonQuery();
                }
                catch (NpgsqlException ex)
                {
                    Console.WriteLine($"Error deleting data: {ex.Message}");
                }
            }
            vConnection.Close();
            return rowsAffected;
        }

        public int InsertData(string table, Dictionary<string, object> values)
        {
            Connect();
            int rowsAffected = 0;

            string columns = string.Join(", ", values.Keys);
            string parameterNames = string.Join(", ", values.Keys.Select(key => "@" + key));

            string sql = $"INSERT INTO {table} ({columns}) VALUES ({parameterNames})";

            using (NpgsqlCommand command = new NpgsqlCommand(sql, vConnection))
            {
                AddParametersToCommand(command, values);

                try
                {
                    rowsAffected = command.ExecuteNonQuery();
                }
                catch (NpgsqlException ex)
                {
                    Console.WriteLine($"Error inserting data: {ex.Message}");
                }
            }
            vConnection.Close();
            return rowsAffected;
        }

        // Helper function to add parameters to a command
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
    }
}