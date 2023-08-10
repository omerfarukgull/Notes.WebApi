using System.ComponentModel;

namespace Notlarim.WebUI.Models
{
    public class NoteModel
    {
        public int NoteId { get; set; }
        [DisplayName("Başlık")]
        public string Title { get; set; }
        [DisplayName("Kısa Açıklama")]
        public string Content { get; set; }
        [DisplayName("Fotoğraf")]
        public string NoteImgUrl { get; set; }
        [DisplayName("Note PDF")]
        public string NotePdfUrl { get; set; }

        [DisplayName("Kategori")]
        public int CategoryId { get; set; }
        public int MemberId { get; set; }
        public bool IsApproved { get; set; }
    }
}
