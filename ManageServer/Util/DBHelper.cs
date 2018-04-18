using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ManageServer.Util
{
    public class DBHelper
    {
        //public static string connectionStr = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["sqlConnectionString"].ToString();
        public static string connectionStr = "server=180.76.162.167;port=3306;user=root;password=654758634;database=gothing;";
        public DBHelper() { }

        public static int ExecuteNonQuery(string SQLString)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionStr))
            {
                using (MySqlCommand cmd = new MySqlCommand(SQLString, conn))
                {
                    try
                    {
                        int rows = cmd.ExecuteNonQuery();
                        return rows;
                    }
                    catch (MySqlException e)
                    {
                        throw e;
                    }
                }
            }
        }

        public static MySqlDataReader ExecuteReader(string SQLString)
        {
            MySqlConnection connection = new MySqlConnection(connectionStr);
            MySqlCommand cmd = new MySqlCommand(SQLString, connection);
            try
            {
                connection.Open();
                MySqlDataReader myReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                return myReader;
            }
            catch (MySqlException e)
            {
                throw e;
            }
        }

        public static DataSet ExecuteTable(string SQLString)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionStr))
            {
                DataSet ds = new DataSet();
                try
                {
                    connection.Open();
                    MySqlDataAdapter command = new MySqlDataAdapter(SQLString, connection);
                    command.Fill(ds, "ds");
                }
                catch (MySqlException ex)
                {
                    throw new Exception(ex.Message);
                }
                return ds;
            }
        }

        public static object ExecuteScalar(string SQLString)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionStr))
            {
                using (MySqlCommand cmd = new MySqlCommand(SQLString, connection))
                {
                    try
                    {
                        connection.Open();
                        object obj = cmd.ExecuteScalar();
                        if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                        {
                            return null;
                        }
                        else
                        {
                            return obj;
                        }
                    }
                    catch (MySqlException e)
                    {
                        connection.Close();
                        throw e;
                    }
                }
            }
        }
    }
}