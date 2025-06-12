using Malshinon.Helpers;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace Malshinon.DAL
{
    internal static class ReportRepository
    {
        private static readonly ConnectionWrapper _connection = ConnectionWrapper.getInstance();

        // Get all reports
        public static List<Report> GetAllReports()
        {
            const string query = "SELECT * FROM Reports";
            return GetReports(query);
        }

        // Get reports by target ID via ReportTargets table
        public static List<Report> GetReportsByTargetId(int targetId)
        {
            const string query = @"
                SELECT r.* 
                FROM Reports r
                INNER JOIN ReportTargets rt ON r.Id = rt.ReportId
                WHERE rt.TargetId = @targetId;";

            var parameters = new Dictionary<string, object>
            {
                { "@targetId", targetId }
            };

            return GetReports(query, parameters);
        }

        // Get reports by tag string (using the Tag column in Reports table)
        public static List<Report> GetReportsByTag(string tag)
        {
            const string query = @"SELECT * FROM Reports WHERE Tag = @tag";

            var parameters = new Dictionary<string, object>
            {
                { "@tag", tag }
            };

            return GetReports(query, parameters);
        }

        // Get reports by reporter ID
        public static List<Report> GetReportsByReporterId(int reporterId)
        {
            const string query = @"SELECT * FROM Reports WHERE ReporterId = @reporterId";

            var parameters = new Dictionary<string, object>
            {
                { "@reporterId", reporterId }
            };

            return GetReports(query, parameters);
        }

        // Get total reports by Target ID via ReportTargets
        public static int GetReportCountByTargetId(int targetId)
        {
            const string query = @"
                SELECT COUNT(*) 
                FROM Reports r
                INNER JOIN ReportTargets rt ON r.Id = rt.ReportId
                WHERE rt.TargetId = @targetId";

            var parameters = new Dictionary<string, object>
            {
                { "@targetId", targetId }
            };
            return GetReportsAmount(query, parameters);
        }

        // Get total reports by Reporter ID
        public static int GetReportCountByReporterId(int reporterId)
        {
            const string query = "SELECT COUNT(*) FROM Reports WHERE ReporterId = @reporterId";

            var parameters = new Dictionary<string, object>
            {
                { "@reporterId", reporterId }
            };
            return GetReportsAmount(query, parameters);
        }

        // Add a new report
        public static bool AddReport(Report report)
        {
            const string query = @"
                INSERT INTO Reports (ReporterId, Message, Timestamp, Tag)
                VALUES (@reporterId, @message, @timestamp, @tag);";

            var parameters = new Dictionary<string, object>
            {
                { "@reporterId", report.ReporterId },
                { "@message", report.Message },
                { "@timestamp", report.Timestamp },
                { "@tag", report.Tag }
            };

            try
            {
                _connection.ExecuteNoneQuery(query, parameters);
                return true;
            }
            catch (Exception ex)
            {
                Logger.Log("Failed to add report: " + ex.Message, Logger.LogLevel.Error);
                return false;
            }
        }

        // Helper method to map reader to Report objects
        private static List<Report> GetReports(string query, Dictionary<string, object>? parameters = null)
        {
            var reportList = new List<Report>();

            using (var reader = _connection.ExecuteSelect(query, parameters))
            {
                if (reader == null) return reportList;

                while (reader.Read())
                {
                    reportList.Add(new Report
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        ReporterId = Convert.ToInt32(reader["ReporterId"]),
                        Message = reader["Message"]?.ToString(),
                        Timestamp = Convert.ToDateTime(reader["Timestamp"]),
                        Tag = reader["Tag"]?.ToString()
                    });
                }
            }

            return reportList;
        }

        // Helper method to get count from any query that returns COUNT(*)
        private static int GetReportsAmount(string query, Dictionary<string, object>? parameters = null)
        {
            try
            {
                using (var reader = _connection.ExecuteSelect(query, parameters))
                {
                    if (reader != null && reader.Read())
                    {
                        return Convert.ToInt32(reader[0]);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log("Error fetching report count: " + ex.Message, Logger.LogLevel.Error);
            }

            return 0;
        }
    }
}
