using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using talabat.core.Entites;

namespace talabat.core.Specifications
{
    public class baseSpecification<T> : ISpecification<T> where T : BaseEntity
    {
        public Expression<Func<T, bool>> Criteria { get; set; }
        public List<Expression<Func<T, object>>> Iincludes { get; set; } =  new List<Expression<Func<T, object>>>();
        public Expression<Func<T, object>> orderBy      { get; set; }
        public Expression<Func<T, object>> orderByDesc { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
        public bool ISPagenationEnabled { get; set; }

        public baseSpecification()
        {
            //Iincludes = new List<Expression<Func<T, object>>>();
        }
        public baseSpecification(Expression<Func<T, bool>>  criteriaExpresion)
        {
            Criteria = criteriaExpresion;
            //Iincludes = new List<Expression<Func<T, object>>>();
        }
        public void AddOrderBy(Expression<Func<T , object>>orderbyExp)
        {
            orderBy = orderbyExp;
        }
        public void AddOrderByDesc(Expression<Func<T, object>> orderbyDescExp)
        {
            orderByDesc = orderbyDescExp;
        }


        public void ApplyPagenation(int skip, int take)
        {
            ISPagenationEnabled = true;
            Skip = skip;
            Take = take;
        }
    }
}
