using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiSkiPass.Models.Response
{
    public class UserServicesResp : Alarm
    {
        public List<UserServices> services { get; set; } = new List<UserServices>();
    }

    public class UserServices
    {
        public string servName { get; set; }
        public bool isActive { get; set; }
        public double amount { get; set; }
        public string start { get; set; }
        public string end { get; set; }

    }
}
