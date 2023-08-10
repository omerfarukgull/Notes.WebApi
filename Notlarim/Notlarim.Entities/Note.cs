using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notlarim.Entities
{
 
    public class Note
    {
     
        public int NoteId { get; set; }
        public string Title { get; set; }
        public string Content { get; set;}
        public string NoteImgUrl { get; set;}
        public string NotePdfUrl { get; set;}
        public bool IsApproved { get; set; }
        public DateTime CreatDate { get; set; } = DateTime.Now;

 

        public int MemberId { get; set; }
        public Member? Member { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }


    }
}
