using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public class ManageController : Controller
    {
        private readonly ConfigManager _configManager = new();
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

            manageModel.EmailNotifications = _configManager.ReadValue("EmailNotifications") == "true" ? true : false;
            manageModel.NotifEmailAddress = _configManager.ReadValue("NotifEmailAddress");

            return View(manageModel);
        }

        public IActionResult SaveSettings(ManageModel model)
        {
            if(model.NotifEmailAddress != null)
            {
                _configManager.ChangeValue("EmailNotifications", model.EmailNotifications ? "true" : "false");
                _configManager.ChangeValue("NotifEmailAddress", model.NotifEmailAddress);
            }

            return LocalRedirect("~/Manage/Manage");
        }
    }
}
