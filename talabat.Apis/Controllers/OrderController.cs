using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using talabat.Apis.Dtos.OrderDto;
using talabat.Apis.Dtos.OrderDto.OrderDTO;
using talabat.Apis.Dtos.OrderDTO;
using talabat.Apis.Errors;
using talabat.core.Entites.Order_Aggregate;
using talabat.core.services;

namespace talabat.Apis.Controllers
{
    [Authorize]
    public class OrderController : ApiBaseController
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrderController( IOrderService orderService , IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        // improving for swagger
        [ProducesResponseType(typeof(Order) , StatusCodes.Status200OK)]
        [ProducesResponseType(typeof (ApiResponse) , StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder(OrderDto orderDto)
        {
            var BuyerEmail = User.FindFirstValue(ClaimTypes.Email);

            var address = _mapper.Map<OrderAddressDto, Address>(orderDto.ShipingAddress);
            var order =  await _orderService.CreateOrderAsync(BuyerEmail, orderDto.BasketId, orderDto.DeliveryMethod , address );
            if(order is null)  return BadRequest(new ApiResponse(400));
            return Ok(order);
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Order>>> GetOrdersForUser()
        {
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var Orders = await  _orderService.CreateOrdersForUserAsync(buyerEmail);
            return Ok(Orders);
        }
        [ProducesResponseType(typeof(Order), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]

        [HttpGet("{id}")]
        public async Task<ActionResult<IReadOnlyList<Order>>> GetOrderForUser(int Id)
        {
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var Orders = await _orderService.CreateOrderByIdForUserAsync(buyerEmail, Id);
            if (Orders is null) return BadRequest(new ApiResponse(400));
            return Ok(Orders);
        }
        [HttpGet("DeliveryMethod")]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetAllDeliveyMethods()
        {
            var DeliveryMethods=await _orderService.GetDeliverymethods();
            return Ok(DeliveryMethods);
        }
    }
}
 