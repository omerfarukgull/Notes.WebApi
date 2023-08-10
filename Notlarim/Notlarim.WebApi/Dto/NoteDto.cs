using System.ComponentModel;

namespace Notlarim.WebApi.Dto
{
    public class NoteDto
    {
        public int NoteId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string NoteImgUrl { get; set; }
        public string NotePdfUrl { get; set; }
        public int CategoryId { get; set; }
        public bool IsApproved { get; set; }
    }
}
