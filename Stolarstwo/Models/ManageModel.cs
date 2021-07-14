using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stolarstwo.Models
{
    public class ManageModel
    {
        public List<FormModel> FormModels { get; set; } = new();
        public bool EmailNotifications { get; set; }
    }
}
