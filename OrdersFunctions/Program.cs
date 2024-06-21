using OrdersLibrary;

namespace OrdersFunctions
{
    internal class Program
    {
        static void Main(string[] args)
        {

            var connStr = "Server = localhost\\sqlexpress01;database=SalesDb;trusted_connection=true;trustServerCertificate=true;";
            Connection connection = new Connection(connStr);
            connection.Open();

            OrderController ordctrl = new OrderController(connection);

            var orders = ordctrl.GetAll();

            foreach (var o in orders)
            {
                Console.WriteLine(o.ToString());
            }

            var oneOrder = ordctrl.GetByPK(11111);
            if (oneOrder == null)
            {
                Console.WriteLine("order not found");
            }
            else
            {
                Console.WriteLine(oneOrder.ToString());
            }

            //********Create********//
            /*
            //OrdersLibrary.Orders newOrder = new OrdersLibrary.Orders
            //{
            //    Id = 0,
            //    CustomerId = 12,
            //    Description = "New Order",
            //    Date = new DateTime(2024, 6, 21)
            //};
            //var rc = ordctrl.Create(newOrder);
            //if (rc)
            //{
            //    Console.WriteLine("Create succeeded");
            //}
            //else
            //{
            //    Console.WriteLine("Create Failed");
            //}
            */

            //********CHange********//
            /*
            OrdersLibrary.Orders changeOrder = new OrdersLibrary.Orders();//{ Description = "Updated order", Id = 28, Date = new DateTime(2023,2,23) };

            changeOrder.Id = 29;
            changeOrder.Description = "updated";
            changeOrder.Date = new DateTime(2023, 5, 21);



            var rc = ordctrl.Change(changeOrder);
            if (rc)
            {
                Console.WriteLine("change succeeded");
            }
            else
            {
                Console.WriteLine("change Failed");
            }
            */

            //******Remove******//
            /*
            var rc = ordctrl.Remove(29);
            if (rc)
            {
                Console.WriteLine("Remove Succeeded");
            }
            else
            {
                Console.WriteLine("Change Failed");

                connection.Close();
            }
            */
        }
    }
}
