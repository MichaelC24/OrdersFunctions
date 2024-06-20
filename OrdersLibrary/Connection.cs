using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersLibrary
{
    public class Connection
    {

        public string _connectionString { get; set; } = string.Empty;
        private SqlConnection? _connection { get; set; } = null;

        public SqlConnection? GetConnection()
        {
            return _connection;
        }
        public void Open()
        {
            _connection = new SqlConnection(_connectionString);
            _connection.Open();
            if (_connection.State != System.Data.ConnectionState.Open)
            {
                throw new Exception("Unable to Connect!");
            }
        }

        public void Close()
        {
            _connection?.Close();
        }

        public Connection(string connectionString) 
        {
            connectionString = _connectionString;
        }
    }
}
