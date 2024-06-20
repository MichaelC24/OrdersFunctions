using OrdersLibrary;

namespace OrdersFunctions
{
    internal class Program
    {
        static void Main(string[] args)
        {

            var ConnStr = "Server = localhost\\sqlexpress01;database=SalesDb;trusted_connection=true;trustServerCertificate=true;";
            Connection connection = new Connection(ConnStr);
            connection.Open();



        }
    }
}
