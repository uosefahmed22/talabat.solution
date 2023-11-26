using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using talabat.core.Entites;

namespace talabat.core.Specifications
{
    public interface ISpecification<T> where T : BaseEntity
    {
        public Expression<Func<T , bool>> Criteria { get; set; }
        public List<Expression<Func<T , object>>> Iincludes { get; set; }
        public Expression<Func<T , object>> orderBy { get; set; }
        public Expression<Func<T , object>> orderByDesc { get; set; }

        public int Skip { get; set; }
        public int Take { get; set; }
        public bool ISPagenationEnabled { get; set; } 
    }
}
