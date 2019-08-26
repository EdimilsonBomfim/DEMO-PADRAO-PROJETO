using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace WebShoes.Repository.Utilities
{
    public class ConnectionFactory
    {
        public static SqlConnection GetConnection(string connectionString)
        {
            var conn = new SqlConnection(connectionString);
            conn.Open();
            return conn;
        }
    }
}
