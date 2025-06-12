using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace Malshinon.DAL
{
    internal static class TargetRepository
    {
        private static readonly ConnectionWrapper _connection = ConnectionWrapper.getInstance();

        public static Target? GetById(int id)
        {
            const string query = "SELECT * FROM Targets WHERE Id = @id";
            var parameters = new Dictionary<string, object> { { "@id", id } };
            using var reader = _connection.ExecuteSelect(query, parameters);

            if (reader != null && reader.Read())
            {
                return new Target
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Name = reader["Name"].ToString(),
                    SecretCode = reader["SecretCode"].ToString(),
                    Affiliation = reader["Affiliation"]?.ToString()
                };
            }
            return null;
        }
        // Insert target and return its ID
        public static int InsertAndReturnId(Target target)
        {
            const string query = @"INSERT INTO Targets (Name, SecretCode, Affiliation)
                                 VALUES (@name, @code, @affiliation);
                                 SELECT LAST_INSERT_ID();";

            var parameters = new Dictionary<string, object>
            {
                {"@name", target.Name},
                {"@code", target.SecretCode},
                {"@affiliation", target.Affiliation ?? string.Empty}
            };

            object? result = _connection.ExecuteScalar(query, parameters);
            return result != null ? Convert.ToInt32(result) : -1;
        }
        public static Target? GetByName(string name)
        {
            const string query = "SELECT * FROM Targets WHERE Name = @name";
            var parameters = new Dictionary<string, object> { { "@name", name } };
            using var reader = _connection.ExecuteSelect(query, parameters);

            if (reader != null && reader.Read())
            {
                return new Target
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Name = reader["Name"].ToString(),
                    SecretCode = reader["SecretCode"].ToString(),
                    Affiliation = reader["Affiliation"]?.ToString()
                };
            }
            return null;
        }

        public static Target? GetBySecretCode(string secretCode)
        {
            const string query = "SELECT * FROM Targets WHERE SecretCode = @code";
            var parameters = new Dictionary<string, object> { { "@code", secretCode } };
            using var reader = _connection.ExecuteSelect(query, parameters);

            if (reader != null && reader.Read())
            {
                return new Target
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Name = reader["Name"].ToString(),
                    SecretCode = reader["SecretCode"].ToString(),
                    Affiliation = reader["Affiliation"]?.ToString()
                };
            }
            return null;
        }
        public static int getHighestId()
        {
            const string query = "SELECT MAX(id) FROM Targets";
            using (var reader = _connection.ExecuteSelect(query))
            {
                if (reader != null && reader.Read())
                {
                    return Convert.ToInt32(reader[0]);
                }
            }
            return 0;
        }

        public static int GetIdByNameOrBySecretCode(string name)
        {
            const string query = "SELECT * FROM Tatgets WHERE Name = @name Or SecretCode = @name";
            var parameters = new Dictionary<string, object> { { "@name", name } };
            using var reader = _connection.ExecuteSelect(query, parameters);

            if (reader != null && reader.Read())
                return Convert.ToInt32(reader["Id"]);

            return -1;
        }


        public static List<Target> GetAll()
        {
            const string query = "SELECT * FROM Targets";
            var result = new List<Target>();
            using var reader = _connection.ExecuteSelect(query);
            while (reader != null && reader.Read())
            {
                result.Add(new Target
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Name = reader["Name"].ToString(),
                    SecretCode = reader["SecretCode"].ToString(),
                    Affiliation = reader["Affiliation"]?.ToString()
                });
            }
            return result;
        }

        public static int GetIdByName(string name)
        {
            const string query = "SELECT Id FROM Targets WHERE Name = @name";
            var parameters = new Dictionary<string, object> { { "@name", name } };
            using var reader = _connection.ExecuteSelect(query, parameters);
            if (reader != null && reader.Read()) return Convert.ToInt32(reader["Id"]);
            return -1;
        }

        public static int GetIdBySecretCode(string code)
        {
            const string query = "SELECT Id FROM Targets WHERE SecretCode = @code";
            var parameters = new Dictionary<string, object> { { "@code", code } };
            using var reader = _connection.ExecuteSelect(query, parameters);
            if (reader != null && reader.Read()) return Convert.ToInt32(reader["Id"]);
            return -1;
        }
    }
}
