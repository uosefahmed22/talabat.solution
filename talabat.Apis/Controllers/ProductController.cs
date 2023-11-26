using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using talabat.Apis.Dtos.ProductDTO;
using talabat.Apis.Errors;
using talabat.Apis.Helpers;
using talabat.core;
using talabat.core.Entites.Product;
using talabat.core.Repositories;
using talabat.core.Specifications;
using talabat.Repository;

namespace talabat.Apis.Controllers
{

    public class ProductController : ApiBaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductController(IUnitOfWork unitOfWork,
                                 IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [CashehedAttribute(600)]
        [HttpGet]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]  
        public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> Getproducts([FromQuery] ProductSpecParams specParams)
        {
            var spec = new ProductWithBrandAndTypeSpecifications(specParams);
            var products = await _unitOfWork.repository<Product>().GetAllWithSpecAsync(spec);
            var Data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);
            var CountSpec = new ProductWithFilterationForCount(specParams);
            var Count = await _unitOfWork.repository<Product>().GetCountWithSpecAsync(CountSpec);
            return Ok(new Pagination<ProductToReturnDto>(specParams.PageIndex, specParams.PageSize, Count, Data));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProductToReturnDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]

        public async Task<ActionResult<ProductToReturnDto>> GetproductById(int id)
        {
            var spec = new ProductWithBrandAndTypeSpecifications(id);

            var product = await _unitOfWork.repository<Product>().GetEntityWithSpecAsync(spec);
            if (product is null) return NotFound(new ApiResponse(404));
            var mapproduct = _mapper.Map<Product, ProductToReturnDto>(product);
            return Ok(mapproduct);
        }


        [HttpGet("Brands")] //Get : /Api/Products/Brands
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrands()
        {
            var Brands = await _unitOfWork.repository<ProductBrand>().GetAllAsync();
            return Ok(Brands);
        }

        [HttpGet("Types")] //Get : /Api/Products/Types
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetTypes()
        {
            var Types = await _unitOfWork.repository<ProductType>().GetAllAsync();
            return Ok(Types);
        }
    }
}
