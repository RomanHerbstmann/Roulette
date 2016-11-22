using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Configuration;
using System.Diagnostics;

namespace Roulette
{
    public class DbConnection
    {
        private OleDbConnection connection = new OleDbConnection(@"Provider=Microsoft.Jet.OleDb.4.0;Data Source=" + ConfigurationManager.AppSettings["Database.Path"]);

        public OleDbDataReader ExecuteSqlQuery(string SqlCommand)
        {
            if (connection.State == System.Data.ConnectionState.Open)
                CloseConnection();
            try
            {
                connection.Open();
                var cmd = connection.CreateCommand();
                cmd.CommandText = SqlCommand;
                var reader = cmd.ExecuteReader();
                return reader;
        }
            catch
            {
                return null;
            }
        } 

        public int? ExecuteSqlNonQuery(string SqlCommand)
        {
            if (connection.State == System.Data.ConnectionState.Open)
                CloseConnection();
            try
            {
                connection.Open();
                var cmd = connection.CreateCommand();
                cmd.CommandText = SqlCommand;
                var reader = cmd.ExecuteNonQuery();
                return reader;
            }
            catch
            {
                return null;
            }
        }

        public void CloseConnection()
        {
            connection.Close();
        }
    }
}
