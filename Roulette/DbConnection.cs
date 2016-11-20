using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Configuration;

namespace Roulette
{
    public class DbConnection
    {
        private OleDbConnection connection = new OleDbConnection(@"Provider=Microsoft.Jet.OleDb.4.0;Data Source=" + ConfigurationManager.AppSettings["Database.Path"]);

        public OleDbDataReader ExecuteSqlQuery(string SqlCommand)
        {
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

        public void CloseConnection()
        {
            connection.Close();
        }
    }
}
