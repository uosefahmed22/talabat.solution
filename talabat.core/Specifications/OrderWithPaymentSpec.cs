using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using talabat.core.Entites.Order_Aggregate;

namespace talabat.core.Specifications
{
    public class OrderWithPaymentSpec:baseSpecification<Entites.Order_Aggregate.Order>
    {
        public OrderWithPaymentSpec(string paymentIntentId):base(O=>O.PaymentIntentId==paymentIntentId)
        {
            
        }

    }
}
