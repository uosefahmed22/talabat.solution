using Stripe;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace talabat.core.Entites.Order_Aggregate
{
    public class Order : BaseEntity
    {
        public Order()
        {
                
        }
        public Order(string buyerEmail, Address shipingAddress, DeliveryMethod deliveryMethod,
            ICollection<OrderItem> items, decimal subTotal,string paymentintentId)
        {
            BuyerEmail = buyerEmail;
            ShipingAddress = shipingAddress;
            DeliveryMethod = deliveryMethod;
            Items = items;
            SubTotal = subTotal;
            PaymentIntentId = paymentintentId;
        }

        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public OrderStatus status { get; set; } = OrderStatus.Pending;
        public Address ShipingAddress { get; set; }

        public DeliveryMethod DeliveryMethod { get; set; } // Navigational Property [ one ]
        public ICollection<OrderItem> Items { get; set; } = new HashSet<OrderItem>();
        public decimal SubTotal { get; set; } //Price Of Product * Quantity

        //[NotMapped]
        //public decimal Total { get => SubTotal + DeliveryMethod.Cost; }

        public decimal GetTotal()
            => SubTotal + DeliveryMethod.Cost;
        public string PaymentIntentId { get; set; } 


    }
}
