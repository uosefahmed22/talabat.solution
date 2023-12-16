using AutoMapper;
using talabat.Apis.Dtos.BasketDTO;
using talabat.Apis.Dtos.OrderDto.OrderDTO;
using talabat.Apis.Dtos.ProductDTO;
using talabat.core.Entites.Basket;
using talabat.core.Entites.identity;
using talabat.core.Entites.Order_Aggregate;
using talabat.core.Entites.Product;

namespace talabat.Apis.Helpers
{
    public class MappingProfilles : Profile
    {
        public MappingProfilles()
        {
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(d=>d.ProductBrand , o=>o.MapFrom(s=>s.ProductBrand.Name))
                .ForMember(d => d.ProductType, o => o.MapFrom(s => s.ProductType.Name))
                .ForMember(d => d.PictureURL, o => o.MapFrom<productpictureurlresolver>());


            CreateMap<talabat.core.Entites.identity.Address, AddressDto>().ReverseMap();

            CreateMap<CustomerBasketDto, CustomerBasket>();
            CreateMap<BasketitemDto, Basketitem>();
            CreateMap<OrderAddressDto, core.Entites.Order_Aggregate.Address>();


        }
    }
}
