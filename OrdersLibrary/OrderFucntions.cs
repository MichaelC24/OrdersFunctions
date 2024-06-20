using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace OrdersLibrary
{
    public class OrderFucntions
    {
        private SqlConnection connection { get; set; } = null;
        private string SqlGetAll = "Select * FROM Customers";
        public List<Orders> GetAll()
        {
            var sqlcmd = new SqlCommand(SqlGetAll,connection);
            var reader = sqlcmd.ExecuteReader();

            List<Orders> orders = new List<Orders>();

            while(reader.Read()) 
            {
                
            }
        }

        public OrderFucntions(Connection connection) {

            if (connection.GetConnection() != null)
            {
                connection.Open();
            }
        }
    }
}
