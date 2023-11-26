using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using talabat.core.Entites;
using talabat.core.Repositories;
using talabat.core.Specifications;

namespace talabat.Repository
{
    public class GenericRepository<T> : iGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreContext _dbcontext;

        public GenericRepository(StoreContext dbcontext) // ask clr to create object from Dbcontext implicitly 
        {
            _dbcontext = dbcontext;
        }
        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            //if (typeof(T)==typeof(Product))
            //{
            //    return (IEnumerable<T>) await _dbcontext.Set<Product>().Include(p => p.ProductBrand)
            //                                          .Include(p => p.ProductType).ToListAsync();
            //}
            return await _dbcontext.Set<T>().ToListAsync();
        }
        public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }
        public async Task<T> GetByIdAsync(int id)
        {
            //return await _dbcontext.Set<T>().Where(x => x.id == id).FirstOrDefaultAsync();
            return await _dbcontext.Set<T>().FindAsync(id);
        }
        public async Task<T> GetEntityWithSpecAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }
        private IQueryable<T> ApplySpecification (ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.getquery(_dbcontext.Set<T>(), spec); 
        }
        public async Task<int> GetCountWithSpecAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).CountAsync();
        }
        public async Task AddAsync(T item)
        {
            await _dbcontext.Set<T>().AddAsync(item);
        }
        public void Update(T item)
        {
            _dbcontext.Set<T>().Update(item);
        }
        public void Delete(T item)
        {
            _dbcontext.Set<T>().Remove(item);
        }

    }
}
