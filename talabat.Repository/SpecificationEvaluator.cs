using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using talabat.core.Entites;
using talabat.core.Specifications;

namespace talabat.Repository
{
    public static class SpecificationEvaluator <T> where T :BaseEntity
    {
        public static IQueryable<T> getquery (IQueryable<T> inputquery , ISpecification<T> spec )
        {
            var query = inputquery;
            //_dbContext.products
            if (spec .Criteria is not null) // P => P.id == 1
            {
                query = query.Where(spec.Criteria);
            }
            //_dbContext.products( P => P.id == 1 )
            if (spec.orderBy is not null) // P => P.price
            {
               query = query.OrderBy(spec.orderBy);
            }
            //_dbContext.products( P => P.id == 1 ).orderBy( P => P.price )
            if (spec.orderByDesc is not null) // P => P.Name
            {
                query = query.OrderByDescending(spec.orderByDesc);
            }


            if (spec.ISPagenationEnabled)
            {
                query = query.Skip(spec.Skip).Take(spec.Take);
            }

            //iclude ( P => P.ProductBrand) 
            //iclude ( P => P.ProductType)
            query = spec.Iincludes.Aggregate (query, (currentquery, includeExpresion) => currentquery.Include(includeExpresion));

            //_dbContext.products( P => P.id == 1 ).OrderByDescending(  P => P.Name).iclude ( P => P.ProductBrand) 
            return query;
        }
    }
}
