using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Seller : Person
    {
        public string PhoneNumber { get; set; }
        public string Fin { get; set; }
        public string SeriaNumber { get; set; }
        public ICollection<Product> Products { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
