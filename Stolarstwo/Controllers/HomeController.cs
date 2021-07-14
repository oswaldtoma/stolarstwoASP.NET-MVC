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

namespace Stolarstwo.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStringLocalizer<HomeController> _localizer;
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly MyDbContext _myDbContext;

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
                return View("FormSubmit", new FormSubmitModel().Status = _localizer["Order failed!"]);
            }

            return View("FormSubmit", new FormSubmitModel().Status = _localizer["Order sent successfully!"]);
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
    }
}
