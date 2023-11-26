using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using talabat.core.Entites;
using talabat.core.Entites.Order_Aggregate;
using talabat.core.Repositories;

namespace talabat.core
{
    public interface IUnitOfWork : IAsyncDisposable 
    {
        ///public iGenericRepository<Product> ProductRepo { get; set; }
        ///public iGenericRepository<ProductBrand> BrandRepo { get; set; }
        ///public iGenericRepository<ProductType> TypeRepo { get; set; }
        ///public iGenericRepository<Order> OrdersRepo { get; set; }
        ///public iGenericRepository<OrderItem> OrderItemsRepo { get; set; }
        ///public iGenericRepository<DeliveryMethod> DeliveryMethodsRepo { get; set; }

        iGenericRepository<TEntity> repository<TEntity>() where TEntity : BaseEntity ;
        Task<int> Complete();

    }
}
