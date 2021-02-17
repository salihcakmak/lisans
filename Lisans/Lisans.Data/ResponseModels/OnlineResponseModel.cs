using System;
using System.Collections.Generic;
using System.Text;

namespace Lisans.Data.ResponseModels
{
    public class OnlineResponseModel
    {
        public string OnlineProductKey { get; set; }
        public DateTime LastOnlineDate { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
