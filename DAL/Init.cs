using Google.Protobuf.Collections;
using Malshinon.Helpers;
using Malshinon.Services;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Relational;
using System;
using System.Collections.Generic;

namespace Malshinon.DAL
{
    public class Init
    {
        public static MySqlConnection Connection { get; private set; }
        public static void Initialise(string connectionString)
        {
            try
            {
                Connection = new MySqlConnection(connectionString);
                Connection.Open();

                EnsureTablesExist();
            }
            catch (MySqlException ex)
            {
                Logger.Log("Database connection or schema error: " + ex.Message, Logger.LogLevel.Error);
                throw new ApplicationException("Failed to connect to the database or initialize schema.", ex);
            }
            catch (Exception ex)
            {
                Logger.Log("Unexpected error during database initialization: " + ex.Message,Logger.LogLevel.Error);
                throw new ApplicationException("An unexpected error occurred during initialization.", ex);
            }
            finally
            {
                Close();
            }
        }

        private static void EnsureTablesExist()
        {
            var commands = new List<string>
            {
                @"CREATE TABLE IF NOT EXISTS Reporters (
                    Id INT PRIMARY KEY AUTO_INCREMENT,
                    Name VARCHAR(100),
                    SecretCode VARCHAR(100),
                    Rating DOUBLE DEFAULT 50.0,
                    IsRecruit BOOLEAN DEFAULT FALSE
                );",

                @"CREATE TABLE IF NOT EXISTS Targets (
                    Id INT PRIMARY KEY AUTO_INCREMENT,
                    Name VARCHAR(100),
                    SecretCode VARCHAR(100),
                    Affiliation VARCHAR(100)
                );",

                @"CREATE TABLE IF NOT EXISTS Reports (
                    Id INT PRIMARY KEY AUTO_INCREMENT,
                    ReporterId INT,
                    Message TEXT,
                    Timestamp DATETIME,
                    Tag VARCHAR(100),
                    FOREIGN KEY (ReporterId) REFERENCES Reporters(Id)
                );",

                @"CREATE TABLE IF NOT EXISTS Tags (
                Id INT PRIMARY KEY AUTO_INCREMENT,
                Name VARCHAR(100) NOT NULL UNIQUE
                );",

                @"CREATE TABLE IF NOT EXISTS ReportTargets (
                    ReportId INT,
                    TargetId INT,
                    PRIMARY KEY (ReportId, TargetId),
                    FOREIGN KEY (ReportId) REFERENCES Reports(Id),
                    FOREIGN KEY (TargetId) REFERENCES Targets(Id)
                );",


                @"CREATE TABLE IF NOT EXISTS TagRelations (
                TagId INT,
                RelatedTagId INT,
                PRIMARY KEY (TagId, RelatedTagId),
                FOREIGN KEY (TagId) REFERENCES Tags(Id),
                FOREIGN KEY (RelatedTagId) REFERENCES Tags(Id)
                );",
                

                @"CREATE TABLE IF NOT EXISTS ReportTags (
                    ReportId INT,
                    TagId INT,
                    PRIMARY KEY (ReportId, TagId),
                    FOREIGN KEY (ReportId) REFERENCES Reports(Id),
                    FOREIGN KEY (TagId) REFERENCES Tags(Id)
                );",

                @"CREATE TABLE IF NOT EXISTS Alerts (
                    Id INT PRIMARY KEY AUTO_INCREMENT,
                    TargetId INT,
                    TriggerTime DATETIME,
                    Reason TEXT,
                    FOREIGN KEY (TargetId) REFERENCES Targets(Id)
                );"
            };

            foreach (var sql in commands)
            {
                try
                {
                    using var cmd = new MySqlCommand(sql, Connection);
                    cmd.ExecuteNonQuery();
                }
                catch (MySqlException ex)
                {
                    Logger.Log($"Error executing SQL: {sql}. Error: {ex.Message}",Logger.LogLevel.Error);
                    throw new ApplicationException("Failed to create database schema.", ex);
                }
            }
        }

        public static void Close()
        {
            try
            {
                if (Connection != null && Connection.State == System.Data.ConnectionState.Open)
                {
                    Connection.Close();
                }
            }
            catch (MySqlException ex)
            {
                Logger.Log("Error closing database connection: " + ex.Message, Logger.LogLevel.Warning);
            }
        }
    }
}