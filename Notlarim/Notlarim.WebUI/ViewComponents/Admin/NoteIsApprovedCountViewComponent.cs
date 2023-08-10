using Microsoft.AspNetCore.Mvc;
using Notlarim.Business.Abstract;

namespace Notlarim.WebUI.ViewComponents.Admin
{
    public class NoteIsApprovedCountViewComponent : ViewComponent
    {
        private INoteService _noteService;
        public NoteIsApprovedCountViewComponent(INoteService noteService)
        {
            _noteService = noteService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var notes = await _noteService.GetAllIsApproved();
            return View(notes);
        }
    }
}
