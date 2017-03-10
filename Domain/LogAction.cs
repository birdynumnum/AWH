using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class LogAction
    {
        public int Id { get; set; }
        public ApplicationUser DoneBy { get; set; }
        public DateTime PerformedAt { get; set; }
        public string Action { get; set; }
        public string Controller { get; set; }
        public string Description { get; set; }


        public LogAction(ApplicationUser doneby, string action, string controller, string description)
        {
            DoneBy = doneby;
            PerformedAt = DateTime.Now;
            Action = action;
            Controller = controller;
            Description = description;
        }
    }
}
