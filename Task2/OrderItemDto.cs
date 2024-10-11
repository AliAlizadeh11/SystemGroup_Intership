using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task2
{
    public class OrderItemDto
    {
        public long ID { get; set; }
        public long OrderId { get; set; }
        public long BookID { get; set; }
        public int Quantity { get; set; }
    }
}
