using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Stolarstwo.Models
{
    public class ManageModel
    {
        private readonly ConfigManager _configManager = new();

        public List<FormModel> FormModels { get; set; } = new();

        public bool EmailNotifications { get; set; }

        [EmailAddress]
        public string NotifEmailAddress { get; set; }
    }
}
