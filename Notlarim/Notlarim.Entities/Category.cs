using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notlarim.Entities
{
    public class Category
    {
        
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public ICollection<Note> Notes { get; set; }
    }
}
