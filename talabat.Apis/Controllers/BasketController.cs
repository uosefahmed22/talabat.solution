using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using talabat.Apis.Dtos.BasketDTO;
using talabat.Apis.Errors;
using talabat.core.Entites.Basket;
using talabat.core.Repositories;

namespace talabat.Apis.Controllers
{

    public class BasketController : ApiBaseController
    {
        private readonly IBasketRepositries _basketRepo;
        private readonly IMapper _mapper;

        public BasketController(IBasketRepositries basketRepo , IMapper mapper)
        {
            _basketRepo = basketRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<CustomerBasket>> getcustomerbasket(string basketId)
        {
            var basket = await _basketRepo.GetBasketAsync(basketId);
            return basket is null ? new CustomerBasket(basketId) : basket;
        }

        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> ubdateBasket(CustomerBasketDto basket)
        {
            var MappedBasket = _mapper.Map<CustomerBasketDto , CustomerBasket>(basket);
            var createdorubdatedcutomerbasket = await _basketRepo.UbdateBasketAsync(MappedBasket);
            if (createdorubdatedcutomerbasket is null)
                return BadRequest(new ApiResponse(400));
            return Ok(createdorubdatedcutomerbasket);
        }
        [HttpDelete] //Delet : Api/Baskets
        public async Task<ActionResult<bool>> DeletBasket(string basketId)
        {
            return await _basketRepo.DeleteBasketAsync(basketId);
        }
    }
}

