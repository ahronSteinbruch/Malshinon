using Malshinon.Helpers;
using Malshinon.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Malshinon.DAL
{
    internal class AlertRepository
    {
        private readonly ConnectionWrapper _connection = ConnectionWrapper.getInstance();

        public bool AddAlert(Alert alert)
        {
            const string query = @"
                INSERT INTO Alerts (TargetId, TriggerTime, Reason)
                VALUES (@targetId, @time, @reason);";

            var parameters = new Dictionary<string, object>
            {
                { "@targetId", alert.Id },
                { "@time", alert.TriggerTime },
                { "@reason", alert.Reason }
            };

            try
            {
                _connection.ExecuteNoneQuery(query, parameters);
                return true;
            }
            catch (Exception ex)
            {
                Logger.Log("Failed to add alert: " + ex.Message, Logger.LogLevel.Error);
                return false;
            }
        }
    }
}

