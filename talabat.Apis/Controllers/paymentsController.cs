using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using talabat.Apis.Dtos.BasketDTO;
using talabat.Apis.Errors;
using talabat.core.Entites.Basket;
using talabat.core.services;

namespace talabat.Apis.Controllers
{
    public class paymentsController : ApiBaseController
    {
        private readonly IPaymentServices _paymentServices;

        public paymentsController(IPaymentServices paymentServices)
        {
            _paymentServices = paymentServices;
        }
        [Authorize]
        [ProducesResponseType(typeof(CustomerBasket), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [HttpPost("{basketId}")]
        public async Task<ActionResult<CustomerBasketDto>>CreateOrUpdatePaymentIntent(string basketId)
        {
            var basket =await _paymentServices.CreateOrUpdatedPaymentAsync(basketId);
            if (basket == null) return NotFound(new ApiResponse(404));
            return Ok(basket);
        }
    }
}
