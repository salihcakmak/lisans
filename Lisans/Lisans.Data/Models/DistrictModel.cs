using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Lisans.Data.Models
{
    [Table("District")]
    public class DistrictModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("il_id")]
        public int ProviceId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
