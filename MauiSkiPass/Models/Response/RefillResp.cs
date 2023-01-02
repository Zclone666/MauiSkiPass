using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiSkiPass.Models.Response
{
    public class GetBalanceOut : Alarm
    {
        public int id { get; set; }
        /// <summary>
        /// ID скипасса или браслета
        /// </summary>
        public string key { get; set; }
        public decimal balance { get; set; }
    }
}
