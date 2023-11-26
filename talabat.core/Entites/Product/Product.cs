using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace talabat.core.Entites.Product
{
    public class Product : BaseEntity
    {
        // [ string ] :: not allow null in => .net 6 ; but .net 5 => allow null
        public string Name { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }
        public int ProductBrandId { get; set; } // FK => not allow null
        // Navigational property
        public ProductBrand ProductBrand { get; set; }

        // Navigational property
        public ProductType ProductType { get; set; }
        //[ForeignKey("ProductType")]
        public int ProductTypeId { get; set; } // FK => not allow null



    }
}
