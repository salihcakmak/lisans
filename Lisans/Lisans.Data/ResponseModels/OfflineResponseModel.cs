using System;
using System.Collections.Generic;
using System.Text;

namespace Lisans.Data.ResponseModels
{
    public class OfflineResponseModel
    {
        public string OfflineProductKey { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
