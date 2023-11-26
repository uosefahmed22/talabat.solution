using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using talabat.core.Entites.Basket;

namespace talabat.core.Repositories
{
    public interface IBasketRepositries
    {
        Task<CustomerBasket?> GetBasketAsync(string basketid);
        Task<CustomerBasket?> UbdateBasketAsync(CustomerBasket basket);
        Task<bool> DeleteBasketAsync(string basketid);
    }
}
