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
        private readonly MyDbContext _myDbContext;
        public List<FormModel> Orders { get; set; }

        public ManageController(MyDbContext context)
        {
            _myDbContext = context;
        }

        public IActionResult Manage()
        {
            Orders = _myDbContext.FormModels.ToList();

            var manageModel = new ManageModel();
            manageModel.FormModels = Orders;

            return View(manageModel);
        }
    }
}
