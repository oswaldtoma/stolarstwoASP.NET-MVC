using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Stolarstwo.Data;
using Stolarstwo.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;

namespace Stolarstwo.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStringLocalizer<HomeController> _localizer;
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly MyDbContext _myDbContext;
        private readonly ConfigManager _configManager = new();

        public HomeController(IStringLocalizer<HomeController> localizer,
            ILogger<HomeController> logger, IWebHostEnvironment webHostEnvironment, MyDbContext context)
        {
            _localizer = localizer;
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
            _myDbContext = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public IActionResult FormSubmit(FormModel model)
        {
            _myDbContext.FormModels.Add(model);
            try
            {
                _myDbContext.SaveChanges();
            }
            catch (Exception)
            {
                return View("FormSubmit", new FormSubmitModel() { Status = _localizer["Order failed!"] });
            }

            if(_configManager.ReadValue("EmailNotifications") == "true")
            {
                SendEmail(model);
            }

            return View("FormSubmit", new FormSubmitModel() { Status = _localizer["Order sent successfully!"] });
        }

        public IActionResult Gallery(string type)
        { 
            GalleryModel model = new(_webHostEnvironment);

            if (type == "interiordoors")
            {
                model.GalleryImagesPath = "doors\\interiordoors";
                model.Title = _localizer["Interior Doors"];
            }
            if (type == "exteriordoors")
            {
                model.GalleryImagesPath = "doors\\exteriordoors";
                model.Title = _localizer["Exterior Doors"];
            }
            if (type == "rustic")
            {
                model.GalleryImagesPath = "mirrors\\rustic";
                model.Title = _localizer["Rustic"];
            }
            if (type == "standardsmooth")
            {
                model.GalleryImagesPath = "mirrors\\standardsmooth";
                model.Title = _localizer["Standard (smooth)"];
            }

            return View("Gallery", model);
        }

        public IActionResult Manage()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private void SendEmail(FormModel model)
        {
            string toMail = _configManager.ReadValue("NotifEmailAddress");

            MailboxAddress from = new("stolarstwo", "stolarstwo@test.pl");
            MailboxAddress to = new("gmail", toMail);

            BodyBuilder body = new();
            body.TextBody = $"Nowe zamówienie {DateTime.Now}" +
                $"Typ lustra: {model.MirrorType.GetDisplayName()}\n" +
                $"Wysokość: {model.Height} Szerokość: {model.Width}\n" +
                $"Szerokość ramy: {model.FrameWidth.GetDisplayName()} \n" +
                $"Kolor: {model.ColorChoice.GetDisplayName()}\n" +
                $"Cena: {model.GetCalculatedPricePLN()}\n" +
                $"Adres dostawy:\n" +
                $"{model.FirstName} {model.LastName}\n" +
                $"{model.StreetAndNumber}\n" +
                $"{model.PostalCode}\n" +
                $"{model.City}\n" +
                $"{model.ShipmentType}\n";

            MimeMessage message = new();
            message.From.Add(from);
            message.To.Add(to);
            message.Subject = "Zamówienie";
            message.Body = body.ToMessageBody();

            SmtpClient client = new();
            client.ServerCertificateValidationCallback = (s, c, h, e) => true;
            client.Connect("smtp.mailtrap.io", 25, MailKit.Security.SecureSocketOptions.StartTls);
            client.Authenticate("b0477469d33e09", "425df7b7ee5afb");
            client.Send(message);
            client.Disconnect(true);
            client.Dispose();
        }
    }
}
