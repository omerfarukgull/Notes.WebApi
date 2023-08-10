using System.ComponentModel;

namespace Notlarim.WebUI.Models
{
    public class CategoryModel
    {
        public int CategoryId { get; set; }
        [DisplayName("Kategori")]
        public string CategoryName { get; set; }

    }
}
