using Malshinon.Helpers;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace Malshinon.DAL
{
    public class ConnectionWrapper
    {
        private readonly string ConnectionStr = "server=localhost;" +
            "user=root;" +
            "database=Malshinon;" +
            "port=3306;";

        private static ConnectionWrapper Instance;

        private ConnectionWrapper() { }

        public static ConnectionWrapper getInstance()
        {
            if (Instance == null) Instance = new ConnectionWrapper();
            return Instance;
        }

        public MySqlDataReader? ExecuteSelect(string query, Dictionary<string, object> parameters = null)
        {
            MySqlConnection conn = null;
            try
            {
                conn = new MySqlConnection(ConnectionStr);
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(query, conn);
                if (parameters != null)
                {
                    foreach (var parameter in parameters)
                    {
                        cmd.Parameters.AddWithValue(parameter.Key, parameter.Value);
                    }
                }
                return cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message,Logger.LogLevel.Error);
                return null;
            }

        }

        public void ExecuteNoneQuery(string query, Dictionary<string, object> parameters = null)
        {
            MySqlConnection connection = null;
            MySqlCommand cmd = null;

            try
            {
                connection = new MySqlConnection(ConnectionStr);
                connection.Open();
                cmd = new MySqlCommand(query, connection);
                if (parameters != null)
                {
                    foreach (var parameter in parameters)
                    {
                        cmd.Parameters.AddWithValue(parameter.Key, parameter.Value);
                    }
                }
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, Logger.LogLevel.Error);
            }
/*            finally
            {
                if(cmd != null)
                    cmd.Dispose();
                if(connection != null)
                    connection.Close();
            }*/
        }

        public object? ExecuteScalar(string query, Dictionary<string, object> parameters = null)
        {
            MySqlConnection conn = null;
            try
            {
                conn = new MySqlConnection(ConnectionStr);
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(query, conn);
                if (parameters != null)
                {
                    foreach (var parameter in parameters)
                    {
                        cmd.Parameters.AddWithValue(parameter.Key, parameter.Value);
                    }
                }
                return cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, Logger.LogLevel.Error);
                return null;
            }
/*            finally
            {
                if (conn != null)
                    conn.Close();
            }*/
        }
    }
}
