using Microsoft.AspNetCore.Mvc;
using Munch.Models;
using NuGet.Protocol.Plugins;
using System.Diagnostics;
using System.Net.Mail;
using System.Net;

namespace Munch.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SmtpClient _smtpClient;

        public HomeController(ILogger<HomeController> logger)
        {
            _smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("abc@gmail.com", "password"),       // Add your Email address and password here
                EnableSsl = true
            };
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ApplyForJob()
        {
            return View(new JobApplication());
        }

        [HttpPost]
        public IActionResult ApplyForJob(JobApplication model, IFormFile cvFile)
        {
            try
            {
                var message = new MailMessage();
                message.From = new MailAddress("abc@gmail.com");        //Sender address
                message.To.Add("abc@gmail.com");     // Destination Email Address
                message.Subject = "Job Application Received";
                message.Body = $"First Name: {model.FirstName}\nLast name: {model.LastName}\nAge: {model.Age}\nEmail: {model.Email}\nPhone Number: {model.PhoneNumber}\nJob Position: {model.PositionApplyingFor}\nAddress: {model.Address}";

                // Send the email
                _smtpClient.Send(message);

                return RedirectToAction("ThankYou");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception during email sending: {ex}");
                ViewBag.ErrorMessage = "An error occurred while sending the email.";
                return View("ApplyForJob", model);
            }

        }
        public IActionResult ThankYou()
        {
            return View();
        }
        public IActionResult Location()
        {
            return View();
        }
        public IActionResult AdminIndex()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Contact(Contact model)
        {
            try
            {
                var message = new MailMessage();
                message.From = new MailAddress("abc@gmail.com");        //Sender address
                message.To.Add("abc@gmail.com"); // Hard-coded destination email
                message.Subject = model.Subject;
                message.Body = $"Name: {model.Name}\nEmail: {model.Email}\nMessage: {model.Message}";
                // Send the email
                _smtpClient.Send(message);

                return RedirectToAction("ThankYou");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception during email sending: {ex}");
                ViewBag.ErrorMessage = "An error occurred while sending the email.";
                return View("ApplyForJob", model);
            }
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(Login model)
        {
            string validEmail = "user@example.com";     // Hardcoded Admin Credentials.
            string validPassword = "password123";

            if (model.Email == validEmail && model.Password == validPassword)
            {

                return RedirectToAction("AdminIndex","Home");
            }
            else
            {
                ViewBag.ErrorMessage = "Incorrect email or password. Please try again.";

                return View("Login", model);
            }
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _smtpClient.Dispose();
            }
            base.Dispose(disposing);
        }

        public IActionResult AddReview()
        {
            return View(new AddReview());
        }

        [HttpPost]
        public IActionResult AddReview(AddReview model, IFormFile cvFile)
        {
            return RedirectToAction("ThankYou");
        }
        
    }
}
