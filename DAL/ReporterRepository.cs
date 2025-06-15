using Malshinon.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Malshinon.DAL
{
    internal static class ReporterRepository
    {
        private static readonly ConnectionWrapper _connection = ConnectionWrapper.getInstance();

        public static int idGenerator = getHighestId() + 1;

        public static Reporter? GetById(int id)
        {
            const string query = "SELECT * FROM Reporters WHERE Id = @id";
            var parameters = new Dictionary<string, object> { { "@id", id } };
            using var reader = _connection.ExecuteSelect(query, parameters);

            if (reader != null && reader.Read())
            {
                return new Reporter
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Name = reader["Name"].ToString(),
                    SecretCode = reader["SecretCode"].ToString(),
                    Rating = Convert.ToDouble(reader["Rating"]),
                    IsRecruit = Convert.ToBoolean(reader["IsRecruit"])
                };
            }
            return null;
        }
    
        public static Reporter? GetByNameOrBySecretCode(string name)
        {
            const string query = "SELECT * FROM Reporters WHERE Name = @name Or SecretCode = @name";
            var parameters = new Dictionary<string, object> { { "@name", name } };
            using var reader = _connection.ExecuteSelect(query, parameters);

            if (reader != null && reader.Read())
            {
                return new Reporter
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Name = reader["Name"].ToString(),
                    SecretCode = reader["SecretCode"].ToString(),
                    Rating = Convert.ToDouble(reader["Rating"]),
                    IsRecruit = Convert.ToBoolean(reader["IsRecruit"])
                };
            }
            return null;
        }

        public static List<Reporter> GetAll()
        {
            const string query = "SELECT * FROM Reporters";
            var result = new List<Reporter>();
            using var reader = _connection.ExecuteSelect(query);
            while (reader != null && reader.Read())
            {
                result.Add(new Reporter
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Name = reader["Name"].ToString(),
                    SecretCode = reader["SecretCode"].ToString(),
                    Rating = Convert.ToDouble(reader["Rating"]),
                    IsRecruit = Convert.ToBoolean(reader["IsRecruit"])
                });
            }
            return result;
        }
        public static List<Reporter> GetAllPotentialInformants()
        {
            const string query = "SELECT * FROM Reporters" +
                "WHERE IsRecruit";
            var result = new List<Reporter>();
            using var reader = _connection.ExecuteSelect(query);
            while (reader != null && reader.Read())
            {
                result.Add(new Reporter
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Name = reader["Name"].ToString(),
                    SecretCode = reader["SecretCode"].ToString(),
                    Rating = Convert.ToDouble(reader["Rating"]),
                    IsRecruit = Convert.ToBoolean(reader["IsRecruit"])
                });
            }
            return result;
        }

        public static double getAverageReportLength(int reporterId)
        {
            const string query = "SELECT AVG( LENGTH(Message)) FROM Reports WHERE ReporterId = @reporterId";
            var parameters = new Dictionary<string, object> { { "@reporterId", reporterId } };

            using (var reader = _connection.ExecuteSelect(query, parameters))
            {
                if (reader != null && reader.Read())
                {
                    return Convert.ToDouble(reader[0]);
                }
            }
            return 0;
        }

        //return the highest id
        public static int getHighestId()
        {
            const string query = "SELECT MAX(Id) FROM Reporters";
            using var reader = _connection.ExecuteSelect(query);

            if (reader != null && reader.Read())
            {
                return reader[0] != DBNull.Value ? Convert.ToInt32(reader[0]) : 0;
            }

            return 0;
        }


        public static bool AddReporter(Reporter reporter)
        {
            const string query = @"
                INSERT INTO Reporters (Name, SecretCode,Rating,IsRecruit)
                VALUES ( @Name, @SecretCode, @Rating, @IsRecruit);";

            var parameters = new Dictionary<string, object>
            {
                { "@Name", reporter.Name },
                { "@SecretCode", reporter.SecretCode },
                { "@Rating", reporter.Rating },
                { "@IsRecruit", reporter.IsRecruit }
            };

            try
            {
                _connection.ExecuteNoneQuery(query, parameters);
                return true;
            }
            catch (Exception ex)
            {
                Logger.Log("Failed to add reporter: " + ex.Message, Logger.LogLevel.Error);
                return false;
            }
        }
    }
}

