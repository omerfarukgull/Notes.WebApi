using Notlarim.Entities;
using System.ComponentModel;

namespace Notlarim.WebUI.Models
{
    public class MemberModel
    {
        public int MemberId { get; set; }
        [DisplayName("İsim")]
        public string Name { get; set; }
        [DisplayName("Soy İsim")]
        public string SurName { get; set; }
        [DisplayName("Email")]
        public string Email { get; set; }
        [DisplayName("Şifre")]
        public string Password { get; set; }
        [DisplayName("Telefon")]
        public string PhoneNumber { get; set; }
        [DisplayName("Cinsiyet")]
        public string Gender { get; set; }
        public string? MemberImageUrl { get; set; }
        [DisplayName("Üniversite")]
        public string University { get; set; }
        [DisplayName("Bölüm")]
        public string Department { get; set; }
        public string? UserStatu { get; set; }

    }
}
