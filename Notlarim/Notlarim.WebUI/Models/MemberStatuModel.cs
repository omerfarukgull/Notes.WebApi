using System.ComponentModel;

namespace Notlarim.WebUI.Models
{
    public class MemberStatuModel
    {
        public int MemberId { get; set; }
        [DisplayName("Kullanıcı Statu")]
        public string UserStatu { get; set; }
     
  
        public string? Name { get; set; }
   
        public string? SurName { get; set; }

        public string? Email { get; set; }

        public string? Password { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Gender { get; set; }
        public string? MemberImageUrl { get; set; }

        public string? University { get; set; }

        public string? Department { get; set; }

    }
}
