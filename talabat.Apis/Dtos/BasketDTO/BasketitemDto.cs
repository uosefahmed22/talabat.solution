using System.ComponentModel.DataAnnotations;

namespace talabat.Apis.Dtos.BasketDTO
{
    public class BasketitemDto
    {
        [Required]
        public int id { get; set; }

        [Required]
        public string name { get; set; }

        [Required]
        public string PictureUrl { get; set; }

        [Required]
        public string Brand { get; set; }

        [Required]
        public string Type { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Quantity Must Be At least One Item")]
        [Required]
        public int Quantity { get; set; }

        [Range(0.1, double.MaxValue, ErrorMessage = "Price Must Be More Than 0.1")]
        [Required]
        public decimal Price { get; set; }


    }
}