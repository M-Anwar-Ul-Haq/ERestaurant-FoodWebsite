using Munch.Models;
using System.ComponentModel.DataAnnotations;

namespace Munch.ViewModels.CartItems
{
    public class OrderAllViewModel
    {
        public int CartItemId { get; set; }
        public int ItemId { get; set; }
        public int Quantity { get; set; }
        public Item Item { get; set; }
        public decimal TotalPrice { get; set; }

        public OrderAllViewModel()
        {
            // Ensure Item is not null before calculating TotalPrice
            if (Item != null)
            {
                TotalPrice = Quantity * Item.Price;
            }
            else
            {
                // Handle the case where Item is null, perhaps set TotalPrice to 0 or throw an exception
                TotalPrice = 0;
            }
        }
    }
}
