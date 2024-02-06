using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Munch.Models;
using Microsoft.AspNetCore.Mvc;
using Munch.ViewModels.CartItems;
using Munch.Data;
using Munch.ViewModels.Order;
using Microsoft.EntityFrameworkCore;
using Humanizer;
using System.Net.Mail;
using System.Net;

namespace Munch.Controllers
{
    public class OrderController : Controller
    {
        private readonly MunchContext _context;
        private readonly SmtpClient _smtpClient;

        // Constructor
        public OrderController(MunchContext context)
        {
            _smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("abc@gmail.com", "password"),       // Add your Email address and password here
                EnableSsl = true
            };
            _context = context;
        }


        [HttpGet]
        public IActionResult Checkout()
        {
            var cartItems = _context.CartItems.Include(ci => ci.Item).ToList();

            decimal totalPrice = cartItems.Sum(item => item.Quantity * (item.Item?.Price ?? 0));

            var model = new CheckoutViewModel
            {
                TotalPrice = totalPrice
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Checkout(CheckoutViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model); // error.
            }
            
            return RedirectToAction("ConfirmMsg");
        }



        [HttpGet]
        public IActionResult ConfirmMsg()
        {
            var cartItems = _context.CartItems.ToList();
            _context.CartItems.RemoveRange(cartItems);
            _context.SaveChanges();

            return View();
        }

        [HttpPost]
        public IActionResult ConfirmMsg(CheckoutViewModel model)
        {
            try
            {
                var message = new MailMessage();
                message.From = new MailAddress("abc@gmail.com");        //sender address
                message.To.Add("abc@gmail.com");       // Add receivers's email address
                message.Subject = "Order Received";
                message.Body = $"Name: {model.Name}\n" +
                               $"Address: {model.Address}\n" +
                               $"Phone Number: {model.PhoneNumber}\n" +
                               $"Coupon Code: {model.CouponCode}\n" +
                               $"Total Price: {model.TotalPrice:C}";

                // Send the email
                _smtpClient.Send(message);


                return RedirectToAction("ConfirmMsg");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception during email sending: {ex}");
                ViewBag.ErrorMessage = "An error occurred while sending the email.";
                return View("ApplyForJob", model);
            }
        }
    }
}
