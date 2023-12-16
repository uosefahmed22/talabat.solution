using talabat.core.Entites;

namespace talabat.Apis.Dtos.ProductDTO
{
    public class ProductToReturnDto
    {

        public int id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PictureURL { get; set; }
        public decimal Price { get; set; }
        public int ProductBrandId { get; set; }
        public string ProductBrand { get; set; }
        public string ProductType { get; set; }
        public int ProductTypeId { get; set; }
    }
}
