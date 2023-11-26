using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using talabat.core.Entites.Order_Aggregate;

namespace talabat.core.services
{
    public interface IOrderService
    {
        Task<Order?> CreateOrderAsync(string BuyerEmail, string BasketId, int DeliveryMethodId, Address ShippingAddress);

        Task<IReadOnlyList<Order>> CreateOrdersForUserAsync(string BuyerEmail);

        Task<Order> CreateOrderByIdForUserAsync(string BuyerEmail , int OrderId);
        Task<IReadOnlyList<DeliveryMethod>> GetDeliverymethods();
    }
}
