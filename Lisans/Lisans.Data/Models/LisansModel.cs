using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Lisans.Data.Models
{
    [Table("Lisans")]
    public class LisansModel
    {
        //Unique id
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LisansId { get; set; }

        //Okul adı
        [Required(ErrorMessage = "Lütfen bu alanı doldurunuz")]
        public string SchoolName { get; set; }

        //Okul kodu
        [Required(ErrorMessage = "Lütfen bu alanı doldurunuz")]
        [MaxLength(6,ErrorMessage ="Bu Alan 6 karakterden uzun olmamalıdır!")]
        public string SchoolCode { get; set; }

        //İşlemcinin hardware numarası
        [Required(ErrorMessage = "Lütfen bu alanı doldurunuz")]
        public string HardwareId { get; set; }

        //Guid ürün anahtarı
        public string OnlineProductKey { get; set; }

        //Üretilmiş ürün anahtarı
        public string OfflineProductKey { get; set; }

        //Gönderilecek e-mail adresi
        [Required(ErrorMessage = "Lütfen bu alanı doldurunuz")]
        [EmailAddress(ErrorMessage = "Lütfen geçerli bir adres giriniz.")]
        public string Email { get; set; }

        //Kullanım durumu
        public bool Status { get; set; }

        //Son giriş tarihi
        public DateTime LastOnlineDate { get; set; }

        //Oluşturulma tarihi
        public DateTime CreationDate { get; set; }

        //Son kullanma tarihi
        public DateTime ExpirationDate { get; set; }


        //Şehir
        [Required(ErrorMessage = "Lütfen bu alanı doldurunuz")]
        public int Province { get; set; }

        //Semt
        [Required(ErrorMessage = "Lütfen bu alanı doldurunuz")]
        public int District { get; set; }
    }
}
