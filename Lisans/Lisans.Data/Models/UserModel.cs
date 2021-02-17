using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Lisans.Data.Models
{
    public class UserModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Bu alan boş geçilemez")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Bu alan boş geçilemez")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Bu alan boş geçilemez")]
        [RegularExpression(@"^[\w-]+(\.[\w-]+)*@([a-z0-9-]+(\.[a-z0-9-]+)*?\.[a-z]{2,6}|(\d{1,3}\.){3}\d{1,3})(:\d{4})?$", ErrorMessage = "Geçerli bir email adresi giriniz")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Bu alan boş geçilemez")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Bu alan boş geçilemez")]
        [DataType(DataType.PhoneNumber)]
        [MaxLength(11), MinLength(10, ErrorMessage = "En az 10 hane giriniz")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Uygun formatta giriniz")]
        public string Phone { get; set; }

        public bool isSuperAdmin { get; set; }


    }
}
