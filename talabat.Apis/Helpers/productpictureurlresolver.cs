using AutoMapper;
using talabat.Apis.Dtos.ProductDTO;
using talabat.core.Entites.Product;

namespace talabat.Apis.Helpers
{
    public class productpictureurlresolver : IValueResolver<Product, ProductToReturnDto, string>
    {
        private readonly IConfiguration _configuration;

        public productpictureurlresolver( IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Resolve(Product source, ProductToReturnDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))
                return $"{_configuration["ApiBaseUrl"]} {source.PictureUrl}";
            return string.Empty;
        }
    }
}
