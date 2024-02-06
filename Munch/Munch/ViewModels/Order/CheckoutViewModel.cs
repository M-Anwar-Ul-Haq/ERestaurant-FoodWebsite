using Munch.Models;
using System.ComponentModel.DataAnnotations;

namespace Munch.ViewModels.Order
{
    public class CheckoutViewModel
    {
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        public string CouponCode { get; set; }

        [Required(ErrorMessage = "Please select a payment method.")]
        public string PaymentMethod { get; set; }

        // Additional fields for card payment
        [CreditCard(ErrorMessage = "Invalid card number.")]
        public string CardNumber { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
