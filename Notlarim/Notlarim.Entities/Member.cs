using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notlarim.Entities
{
    public class Member
    {

        public int MemberId { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Email { get; set;}
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
        public string MemberImageUrl { get; set; }
        public string University { get; set; }
        public string Department { get; set; }
        public DateTime CreatDate { get; set; } = DateTime.Now;

        public string UserStatu { get; set; }

        public ICollection<Note>? Notes { get; set; }

    }
}
