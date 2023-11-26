using talabat.Apis.Dtos.OrderDto.OrderDTO;
using talabat.core.Entites.Order_Aggregate;

namespace talabat.Apis.Dtos.OrderDTO
{
    public class OrderDto
    {
        public string BasketId { get; set; }
        public int DeliveryMethod { get; set; }
        public OrderAddressDto ShipingAddress { get; set; }
    }
}
