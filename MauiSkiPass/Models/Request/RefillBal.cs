using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiSkiPass.Models.Request
{
    public class FillBalanceIn : Alarm
    {
        /// <summary>
        /// ID скипасса или браслета
        /// </summary>
        public string key { get; set; }
        public decimal add_sum { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string payment_id { get; set; } = "test";
        public string payment_system { get; set; } = "test";
        public string payment_source { get; set; } = "test";
        public string comment { get; set; } = "test";
        public int successed { get; set; } = 1;
    }
}
