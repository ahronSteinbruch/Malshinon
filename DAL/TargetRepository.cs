using Malshinon.Helpers;
using Malshinon.models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace Malshinon.DAL
{
    internal class TargetRepository
    {
        private readonly ConnectionWrapper _connection = ConnectionWrapper.getInstance();

        public Target? GetById(int id)
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

        public Target? GetByName(string name)
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

        public Target? GetBySecretCode(string secretCode)
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

        public List<Target> GetAll()
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

        public int GetIdByName(string name)
        {
            const string query = "SELECT Id FROM Targets WHERE Name = @name";
            var parameters = new Dictionary<string, object> { { "@name", name } };
            using var reader = _connection.ExecuteSelect(query, parameters);
            if (reader != null && reader.Read()) return Convert.ToInt32(reader["Id"]);
            return -1;
        }

        public int GetIdBySecretCode(string code)
        {
            const string query = "SELECT Id FROM Targets WHERE SecretCode = @code";
            var parameters = new Dictionary<string, object> { { "@code", code } };
            using var reader = _connection.ExecuteSelect(query, parameters);
            if (reader != null && reader.Read()) return Convert.ToInt32(reader["Id"]);
            return -1;
        }
    }
}
