using System.ComponentModel.DataAnnotations;
using talabat.core.Entites;

namespace talabat.Apis.Dtos.BasketDTO
{
    public class CustomerBasketDto
    {
        [Required]
        public string Id { get; set; }
        public List<BasketitemDto> Items { get; set; }
        public string? PaymentIntentId { get; set; }
        public string? ClientSecret { get; set; }
    }
}
