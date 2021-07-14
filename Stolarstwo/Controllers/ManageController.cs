using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stolarstwo.Data;
using Stolarstwo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stolarstwo.Controllers
{
    public class ManageController : Controller
    {
        private readonly MyDbContext _dbContext;
        public List<FormModel> Orders { get; set; }

        public IActionResult Manage()
        {
            //Orders = _dbContext.formModels.ToList();

            var manageModel = new ManageModel();
            manageModel.FormModels.Add(new FormModel()
            {
                City = "Krakow"
            });

            return View(manageModel);
        }
    }
}
