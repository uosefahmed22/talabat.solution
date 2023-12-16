using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using talabat.core;
using talabat.core.Entites;
using talabat.core.Entites.Order_Aggregate;
using talabat.core.Repositories;
using talabat.Repository.Data;

namespace talabat.Repository
{
    public class UnitOfWorke : IUnitOfWork
    {
        private readonly StoreContext _dbcontext;
        private Hashtable _repositories;
        #region Generic reposatory
        ///public iGenericRepository<Product> ProductRepo { get; set; }
        ///public iGenericRepository<ProductBrand> BrandRepo { get; set; }
        ///public iGenericRepository<ProductType> TypeRepo { get; set; }
        ///public iGenericRepository<Order> OrdersRepo { get; set; }
        ///public iGenericRepository<OrderItem> OrderItemsRepo { get; set; }
        ///public iGenericRepository<DeliveryMethod> DeliveryMethodsRepo { get; set; }
        #endregion
        public UnitOfWorke(StoreContext dbcontext)
        {
            _dbcontext = dbcontext;
            _repositories = new Hashtable();
            #region Generic reposatory

            ///ProductRepo = new GenericRepository<Product>(dbcontext);
            ///BrandRepo = new GenericRepository<ProductBrand>(dbcontext);
            ///TypeRepo = new GenericRepository<ProductType>(dbcontext);
            ///OrdersRepo = new GenericRepository<Order>(dbcontext);
            ///OrderItemsRepo = new GenericRepository<OrderItem>(dbcontext);
            ///DeliveryMethodsRepo = new GenericRepository<DeliveryMethod>(dbcontext);
            #endregion

        }
        public async Task<int> Complete()
          => await _dbcontext.SaveChangesAsync();
        

        public async ValueTask DisposeAsync()
        => await _dbcontext.DisposeAsync();
            
        

        public  iGenericRepository<TEntity> repository<TEntity>() where TEntity : BaseEntity
        {
            var type = typeof(TEntity).Name;  //product
            if (!_repositories.ContainsKey(type))
            {
                var repository = new GenericRepository<TEntity>(_dbcontext);
                _repositories.Add(type, repository);
            }
            return  _repositories[type] as iGenericRepository<TEntity>;
        }
    }
}
