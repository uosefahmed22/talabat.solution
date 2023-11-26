using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace talabat.core.Entites.Order_Aggregate
{
    // Enum => Content Of Lables 
    public enum OrderStatus
    {
        [EnumMember (Value = "Pending")]
        Pending,
        [EnumMember (Value = "Payment Received")]
        PaymentReceived,
        [EnumMember (Value = "Payment Failed")]
        PaymentFailed
    }
}
