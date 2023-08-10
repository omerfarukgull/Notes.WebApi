using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Notlarim.Business.Abstract;
using Notlarim.Entities;
using Notlarim.WebUI.Models;

namespace Notlarim.WebUI.Controllers
{
    
    public class NoteController : Controller
    {
        private INoteService _noteService;
        private ICategoryService _categoryService;
        public NoteController(INoteService noteService, ICategoryService categoryService)
        {
            _noteService = noteService;
            _categoryService = categoryService;
    
        }

        #region Note İşlemeleri
        // Kullanıcıya ait noteları listeler
        public async Task<IActionResult> NoteList()
        {
            var userId = HttpContext.Session.GetInt32("userıd");
            var noteList =await _noteService.UserNotes((int)userId);
            return View(noteList);
        }

        [HttpGet]
        public async Task<IActionResult> NoteAdd()
        {
            var category = await _categoryService.GetAll();
            List<SelectListItem> categoryValues = (from c in category
                                                   select new SelectListItem
                                                   {
                                                       Text = c.CategoryName,
                                                       Value = c.CategoryId.ToString()
                                                   }).ToList();
            ViewBag.categories = categoryValues;
            return View();
        }
        [HttpPost]
        public IActionResult NoteAdd(NoteModel note, IFormFile file, IFormFile pdffile)
        {
            if (note != null)
            {
                var entity = new Note
                {
                    Title = note.Title,
                    Content = note.Content,
                    NoteImgUrl = file.FileName,
                    NotePdfUrl = pdffile.FileName,
                    CategoryId = note.CategoryId,
                    MemberId = note.MemberId,
                    IsApproved = false
                };
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\upload\\img", file.FileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                var pdfpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\upload\\pdf", pdffile.FileName);
                using (var stream = new FileStream(pdfpath, FileMode.Create))
                {
                    pdffile.CopyTo(stream);
                }
                _noteService.Add(entity);
                return RedirectToAction("NoteList");
            }
            return View(note);
        }
        [HttpGet]
        public async Task<IActionResult> NoteDetails(int noteId)
        {
            var category = await _categoryService.GetAll();
            List<SelectListItem> categoryValues = (from c in category
                                                   select new SelectListItem
                                                   {
                                                       Text = c.CategoryName,
                                                       Value = c.CategoryId.ToString()
                                                   }).ToList();
            ViewBag.categories = categoryValues;

            Note model = await _noteService.GetById(noteId);

            var entity = new NoteModel()
            {
                NoteId = noteId,
                Title = model.Title,
                Content = model.Content,
                NoteImgUrl = model.NoteImgUrl,
                NotePdfUrl = model.NotePdfUrl,
                CategoryId = model.CategoryId,
            };

            return View(entity);
        }
        [HttpPost]
        public async Task<IActionResult> NoteDetails(NoteModel note, IFormFile file, IFormFile pdffile)
        {
            var entity = await _noteService.GetById(note.NoteId);
            if (entity == null)
            {
                return NotFound();
            }
            else if (entity != null)
            {
                entity.Title = note.Title;
                entity.Content = note.Content;
                entity.CategoryId = note.CategoryId;
                if (file != null)
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\upload\\img", file.FileName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    entity.NoteImgUrl = file.FileName;
                }
                else
                {
                    entity.NoteImgUrl = entity.NoteImgUrl;
                }
                
                if (pdffile != null)
                {
                    var pdfpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\upload\\pdf", pdffile.FileName);
                    using (var stream = new FileStream(pdfpath, FileMode.Create))
                    {
                        pdffile.CopyTo(stream);
                    }
                    entity.NotePdfUrl = pdffile.FileName;
                }
                else
                {
                    entity.NotePdfUrl = entity.NotePdfUrl;
                }
                _noteService.Update(entity);
                return RedirectToAction("NoteList");
            }
            return View(note);
        }
        [HttpPost]
        public async Task<IActionResult> NoteDelete(int noteId)
        {
            Note note = await _noteService.GetById(noteId);
            if (note == null)
            {
                return NotFound();
            }
            _noteService.Delete(note);
            return RedirectToAction("NoteList");
        }
        #endregion

    }
}
