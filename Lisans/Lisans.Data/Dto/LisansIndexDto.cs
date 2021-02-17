using Lisans.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lisans.Data.Dto
{
    public class LisansIndexDto
    {
        public string SchoolName { get; set; }
        public List <LisansModel> LisansList { get; set; }

    }
}
