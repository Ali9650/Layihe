using Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Order : BaseEntity
    {
        public int Count { get; set; }
        public decimal TotalAmount { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int SellerId { get; set; }
        public Seller Seller { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
