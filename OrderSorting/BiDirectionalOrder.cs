using System;

namespace OrderSorting
{
    public class BiDirectionalOrder
    {
        public BiDirectionalOrder(string street)
        {
            Street = street;
        }
        public string Street { get; set; }
        public BiDirectionalOrder Prev { get; set; }
        public BiDirectionalOrder Next { get; set; }
    }
}
