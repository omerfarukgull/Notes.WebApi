using Microsoft.AspNetCore.Mvc;
using Notlarim.Business.Abstract;

namespace Notlarim.WebUI.ViewComponents.Admin
{
    public class NoteListViewComponent : ViewComponent
    {
        private INoteService _noteService;
        public NoteListViewComponent(INoteService noteService)
        {
            _noteService = noteService;
        }
        public IViewComponentResult Invoke(int noteId)
        {
            var notes = _noteService.GetNoteDetails(noteId);
            return View(notes);
        }
    }
}
