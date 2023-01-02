using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiSkiPass.Models.Request
{
    public class AddServiceReq : Alarm
    {
        public int accountStockId { get; set; }
        public int categoryID { get; set; }
        public decimal amount { get; set; } = 1;
        /// <summary>
        /// ID скипасса или браслета
        /// </summary>
        public string key { get; set; }
        public long date_start { get; set; } = DateTime.Now.Ticks;
        public long date_end { get; set; } = DateTime.MaxValue.Ticks;
    }

}
