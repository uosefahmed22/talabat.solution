using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace talabat.core.Entites.Order_Aggregate
{
    public class OrderItem : BaseEntity
    {
        public OrderItem()
        {
                
        }

        public OrderItem(PrductItemOrder product, int quantity, decimal price)
        {
            Product = product;
            Quantity = quantity;
            Price = price;
        }

        public PrductItemOrder Product { get; set; }
        public int Quantity { get; set;  }
        public decimal Price { get; set; }
    }

}
