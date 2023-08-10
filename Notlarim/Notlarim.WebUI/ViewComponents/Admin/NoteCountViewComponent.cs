using Microsoft.AspNetCore.Mvc;
using Notlarim.Business.Abstract;

namespace Notlarim.WebUI.ViewComponents.Admin
{
    public class NoteCountViewComponent:ViewComponent
    {
        private INoteService _noteService;
        public NoteCountViewComponent(INoteService noteService)
        {
            _noteService = noteService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var notes = await _noteService.GetAll();
            return View(notes);
        }
    }
}
