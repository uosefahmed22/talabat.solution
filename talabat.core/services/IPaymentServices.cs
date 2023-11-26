using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using talabat.core.Entites.Basket;

namespace talabat.core.services
{
    public interface IPaymentServices
    {
        Task<CustomerBasket> CreateOrUpdatedPaymentAsync(string BasketId);
    }
}
