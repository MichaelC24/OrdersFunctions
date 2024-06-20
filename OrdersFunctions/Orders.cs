using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersFunctions
{
    internal class Orders
    {
        public int Id { get; set; } = 0;
        public int CustomerId { get; set; } = 0;
        public DateTime Date { get; set; } = DateTime.MinValue;
        public string Description { get; set; } = string.Empty;

    }
}
