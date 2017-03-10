using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppAWH.Infrastructure.Alerts
{
    public class Alert
    {
        public string AlertClass { get; set; }
        public string Messages { get; set; }
        
        public Alert(string alertclass, string messages)
        {
            AlertClass = alertclass;
            Messages = messages;
        }
    }
}