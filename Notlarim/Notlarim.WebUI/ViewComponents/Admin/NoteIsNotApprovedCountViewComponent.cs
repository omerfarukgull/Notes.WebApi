using Microsoft.AspNetCore.Mvc;
using Notlarim.Business.Abstract;

namespace Notlarim.WebUI.ViewComponents.Admin
{
    public class NoteIsNotApprovedCountViewComponent:ViewComponent
    {
        private INoteService _noteService;
        public NoteIsNotApprovedCountViewComponent(INoteService noteService)
        {
            _noteService = noteService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var notes = await _noteService.GetAllIsNotApproved();
            return View(notes);
        }
    }
}
