using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Lisans.Data.Models
{
    public class RegisterViewModel : UserModel
    {

        [DataType(DataType.Password), Compare(nameof(Password), ErrorMessage = "Şifreler uyuşmuyor")]
        public string ConfirmPassword { get; set; }

    }
}
