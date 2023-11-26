using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using talabat.core.Entites.Product;

namespace talabat.core.Specifications
{
    public class ProductWithBrandAndTypeSpecifications : baseSpecification<Product>
    {
        // this constructor is used for get all products
        public ProductWithBrandAndTypeSpecifications(ProductSpecParams specParams)
             : base(P =>
            (!specParams.BrandId.HasValue || P.ProductBrandId == specParams.BrandId) &&
            (!specParams.TypeId.HasValue || P.ProductTypeId == specParams.TypeId) &&
            (string.IsNullOrEmpty(specParams.Search) || P.Name.ToLower().Contains(specParams.Search))
            )


        {
            if (!string.IsNullOrEmpty(specParams.sort))
            {
                switch (specParams.sort)
                {
                    case "priceAsc":
                        AddOrderBy(P => P.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDesc(P => P.Price);
                        break;
                    default:
                        AddOrderBy(P => P.Name);
                        break;
                }
            }

            /// page products = 100
            /// page size = 10
            /// page index = 3
            ///
            /// skip = 10 * ( 3 - 1 = 2 ) => 20
            /// take = 10
            ApplyPagenation(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);
            Iincludes.Add(P => P.ProductBrand);
            Iincludes.Add(P => P.ProductType);
        }


        // this constructor is used for get specific product 
        public ProductWithBrandAndTypeSpecifications( int id) :base(propa=>propa.id==id)
        {
            Iincludes.Add(P => P.ProductBrand);
            Iincludes.Add(P => P.ProductType);
        }
    }
}
