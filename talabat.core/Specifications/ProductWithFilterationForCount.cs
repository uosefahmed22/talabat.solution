using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using talabat.core.Entites.Product;

namespace talabat.core.Specifications
{
    public class ProductWithFilterationForCount : baseSpecification<Product>
    {
        public ProductWithFilterationForCount(ProductSpecParams specParams) : base(P =>
           (!specParams.BrandId.HasValue || P.ProductBrandId == specParams.BrandId) &&
           (!specParams.TypeId.HasValue || P.ProductTypeId == specParams.TypeId) &&
           (string.IsNullOrEmpty(specParams.Search) || P.Name.ToLower().Contains(specParams.Search))
           )

        {

        }
    }
}
