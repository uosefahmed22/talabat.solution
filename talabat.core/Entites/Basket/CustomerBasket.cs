using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace talabat.core.Entites.Basket
{
    public class CustomerBasket
    {
        public string Id { get; set; }
        public List<Basketitem> Items { get; set; }
        public string? PaymentIntentId {  get; set; }
        public string? ClientSecret {get; set; }
        public int? DeliveryMethodId {  get; set; }
        public decimal ShippingCost {  get; set; }
        public CustomerBasket(string id)
        {
            Id = id;
        }
    }
}
