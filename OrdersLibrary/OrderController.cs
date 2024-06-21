using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;


namespace OrdersLibrary
{
    public class OrderController
    {
        private SqlConnection? _connection { get; set; } = null;
        private string SqlGetAll = "Select * FROM Orders";
        private string SqlGetByPK = "Select * FROM Orders WHERE Id = @id";
        private string SqlCreate = "INSERT Orders (CustomerId, Date, Description) Values (@CustomerId, @Date, @Description)";
        private string SqlChange = "Update Orders Set  Date = @Date, Description = @Description WHERE Id = @Id";
        private string SqlRemove = "DELETE Orders Where Id = @Id";
        public List<Orders> GetAll() //method that returns all rows and columns from orders
        {
            
            var sqlcmd = new SqlCommand(SqlGetAll, _connection); // takes the property SqlGetAll and excutes it using the _connection 
                                                                 // which is using the connection string to connect to the sql database
            
            var reader = sqlcmd.ExecuteReader(); 

            var orders = new List<Orders>();

            while (reader.Read())
            {
                var order = SqlConvertToClass(reader); //passes reader to the SqlConvertToClass it will then return an instance of Orders so you can then pass it to the list go to Line 59 for more info
                orders.Add(order); //adds the data to the list
            }
            reader.Close();
            return orders;
        }
        public Orders? GetByPK(int id) //the ? gives you the option of returning null
        {
            var sqlcmd = new SqlCommand(SqlGetByPK, _connection);

            sqlcmd.Parameters.AddWithValue("@id", id);

            var reader = sqlcmd.ExecuteReader();

            if (!reader.HasRows)
            {
                reader.Close();
                return null;
            }
            reader.Read();
            Orders orders = new Orders();

            orders.Id = Convert.ToInt32(reader["Id"]);
            orders.Date = Convert.ToDateTime(reader["Date"]);
            orders.CustomerId = Convert.ToInt32(reader["CustomerId"]);
            orders.Description = Convert.ToString(reader["Description"])!;

            return orders;

        }

        public bool Create(Orders order)
        {

            var sqlcmd = new SqlCommand(SqlCreate, _connection);

            ConvPara(sqlcmd,order);
            //sqlcmd.Parameters.AddWithValue("@CustomerId", order.CustomerId);
            //sqlcmd.Parameters.AddWithValue("@Date", order.Date);
            //sqlcmd.Parameters.AddWithValue("@Description", order.Description);

            var rowsAffected = sqlcmd.ExecuteNonQuery();

            return (rowsAffected == 1) ? true : false;
        }
        public bool Change(Orders order)
        {
            var  sqlcmd = new SqlCommand(SqlChange,_connection);

            sqlcmd.Parameters.AddWithValue("@Id", order.Id);
            
            ConvPara (sqlcmd,order); //access the ConvPara method which addes the order parameter to the matching parameter in the sql statement
            
            var rowsAffected = sqlcmd.ExecuteNonQuery(); // ExcuteNonQuery() returns a number for how many rows were affected
                                                         // this takes that number and saves it to a variable so you can see if it 
                                                         // succeeded or not in the program using an if statement or a Console.WriteLine
            return rowsAffected == 1 ? true : false;
        }
        public bool Remove(int id)
        {
            var sqlcmd = new SqlCommand(SqlRemove, _connection);

                sqlcmd.Parameters.AddWithValue("@Id", id);
            var rowsAffected = sqlcmd.ExecuteNonQuery();

            return rowsAffected == 1 ? true : false ;
        }

        private Orders SqlConvertToClass(SqlDataReader reader) //takes in a reader and returns an Orders type
        {
            var order = new Orders();

            order.Id = Convert.ToInt32(reader["Id"]);
            order.CustomerId = reader.IsDBNull(reader.GetOrdinal("CustomerId"))///checks to see if the value in the column you are looking at is a null value
                                                                               ///the GetOdinal function gets to postion of the column in realtion to the other columns in the table.
                ? (int?)null //sets customerID to null if the null checks results in true

                : Convert.ToInt32(reader["CustomerId"]); // converts from SQL format to int 

            order.Date = Convert.ToDateTime(reader["Date"]);
            order.Description = Convert.ToString(reader["Description"])!;

            return order;
        }
        private Orders ConvPara(SqlCommand sqlcmd,Orders order) // takes in sqlcmd and order to quickly add what ever value is in
            // the order parameter to the matching parameter in the sql statement
        {
            sqlcmd.Parameters.AddWithValue("@CustomerId", order.CustomerId);
            sqlcmd.Parameters.AddWithValue("@Date", order.Date);
            sqlcmd.Parameters.AddWithValue("@Description", order.Description);
            return order;
        }
        public OrderController(Connection connection) // Contructor to pass the connection string that was passed
                                                      // from program to the Connection class to the property _connection in this class OrderController

        {

            if (connection.GetConnection() != null)
            {
                _connection = connection.GetConnection()!; // need to make sure _connection is on the right side 
            }
        }
    }
}
