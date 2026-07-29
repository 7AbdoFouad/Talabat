using System.ComponentModel.DataAnnotations;

namespace Talabat.APIs.Dtos
{
    public class OrderDto
    {
        //[Required]
        //public string BuyerEmail { get; set; }
        [Required]
        public string BasketId { get; set; }
        [Required]
        // DeliveryMethodId !=0 data annotation 
        [Range(1, int.MaxValue, ErrorMessage = "The DeliveryMethodId field must be greater than 0")]
        public int DeliveryMethodId { get; set; }
        [Required]
        public AddressDto ShipToAddress { get; set; }
    }
}
