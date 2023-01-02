using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MauiSkiPass.Models.Request
{
    public class GetBalanceIn : Alarm
    {
        /// <summary>
        /// ID скипасса или браслета
        /// </summary>
        [JsonRequired]
        public string key { get; set; }
    }
}
