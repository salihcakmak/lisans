using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lisans.Data.RequestModels
{
    public class LisansControlModel
    {
        [JsonProperty("onlineproductkey")]
        public string OnlineProductKey { get; set; }
        [JsonProperty("hardwareid")]
        public string HardwareId { get; set; }
    }
}
