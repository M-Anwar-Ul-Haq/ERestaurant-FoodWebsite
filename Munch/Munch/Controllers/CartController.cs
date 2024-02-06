using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Munch.Models;
using Microsoft.AspNetCore.Mvc;
using Munch.ViewModels.CartItems;
using Munch.ViewModels.Items;
using Munch.Data;

namespace Munch.Controllers
{
    public class CartController : Controller
    {
        private readonly MunchContext _context;
        private readonly IMapper mapper;

        public CartController(MunchContext context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        [HttpPost]
        public IActionResult AddToCart(int itemId, int quantity)
        {
            try
            {
                var item = _context.Items.Find(itemId);

                if (item == null)
                {
                    return NotFound("Item not found.");
                }

                var cartItem = new CartItem
                {
                    ItemId = item.Id,
                    Quantity = quantity,
                    Item = item,
                    // Add any other properties...
                };

                // Save cartItem to the database
                _context.CartItems.Add(cartItem);
                _context.SaveChanges();

                return RedirectToAction("All", "Items"); // Redirect to home or another page
            }
            catch (Exception ex)
            {
                // Log the exception, or return a more detailed error message
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        public IActionResult All()
        {
            var cartitems = this._context
                .CartItems
                .ProjectTo<OrderAllViewModel>
                (this.mapper.ConfigurationProvider)
                .ToList();

            return this.View(cartitems);
        }

        public IActionResult Delete(int cartItemId)
        {
            try
            {
                var cartItem = _context.CartItems.Find(cartItemId);

                if (cartItem == null)
                {
                    return NotFound("CartItem not found.");
                }

                _context.CartItems.Remove(cartItem);
                _context.SaveChanges();

                // Redirect to the "All" action to refresh the view
                return RedirectToAction("All");
            }
            catch (Exception ex)
            {
                // Log the exception, or return a more detailed error message
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }


    }
}
