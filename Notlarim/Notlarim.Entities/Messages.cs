using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Entities.Concrete
{
    
    public class Messages
    {
  
        public int MessagesId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public bool? MessageStatus { get; set; }
        public DateTime CreatDate { get; set; } = DateTime.Now;
    }
}
