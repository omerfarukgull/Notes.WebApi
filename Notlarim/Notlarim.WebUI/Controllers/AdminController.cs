using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyBlog.Entities.Concrete;
using Notlarim.Business.Abstract;
using Notlarim.Entities;
using Notlarim.WebUI.Models;
using System.Net.Mail;
using System.Net;

namespace Notlarim.WebUI.Controllers
{

    public class AdminController : Controller
    {
        private INoteService _noteService;
        private ICategoryService _categoryService;
        private IMemberService _memberService;
        private IMessageService _messageService;
        public AdminController(INoteService noteService, ICategoryService categoryService, IMemberService memberService, IMessageService messageService)
        {
            _noteService = noteService;
            _categoryService = categoryService;
            _memberService = memberService;
            _messageService = messageService;
        }
        public IActionResult Index()
        {
            var note = _noteService.AdminNoteDetails();
            return View(note);
        }

        #region Note İşlemeleri
        public IActionResult NoteList()
        {
            var noteList = _noteService.GetAll();
            return View(noteList);
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

            Note model =await _noteService.GetById(noteId);

            var entity = new NoteModel()
            {
                NoteId = model.NoteId,
                Title = model.Title,
                Content = model.Content,
                NoteImgUrl = model.NoteImgUrl,
                NotePdfUrl = model.NotePdfUrl,
                CategoryId = model.CategoryId,
                IsApproved = model.IsApproved,
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
                entity.IsApproved = note.IsApproved;
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
                return RedirectToAction(nameof(Index));
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

        #region Üye İşlemleri
        public IActionResult MemberList()
        {
            var members = _memberService.GetAll();
            return View(members);
        }
        [HttpGet]
        public async Task<IActionResult> MemberDetails(int memberId)
        {
            var member = await _memberService.GetById(memberId);
            var entity = new MemberStatuModel()
            {
                MemberId = member.MemberId,
                UserStatu = member.UserStatu,
                Name = member.Name,
                SurName = member.SurName,
                Department = member.Department,
                University = member.University,
                MemberImageUrl = member.MemberImageUrl,
                Gender = member.Gender,
                Email = member.Email,
                PhoneNumber = member.PhoneNumber,

            };
            return View(entity);
        }
        [HttpPost]
        public async Task<IActionResult> MemberDetails(MemberStatuModel memberStatuModel)
        {
            var entity = await _memberService.GetById(memberStatuModel.MemberId);
            if (entity == null)
            {
                return NotFound();
            }
            else if (entity != null && memberStatuModel.UserStatu == "Admin")
            {

                entity.UserStatu = memberStatuModel.UserStatu;
                _memberService.Update(entity);

                return RedirectToAction("MemberDetails", new RouteValueDictionary(
                                        new { controller = "Admin", action = "MemberDetails", memberId = memberStatuModel.MemberId }));
            }
            else if (entity != null && memberStatuModel.UserStatu == "User")
            {

                entity.UserStatu = memberStatuModel.UserStatu;
                _memberService.Update(entity);

                return RedirectToAction("Login", "Login");
            }
            return View(memberStatuModel);
        }
        public async Task<IActionResult> MemeberDelete(int memberId)
        {
            var member = await _memberService.GetById(memberId);
            _memberService.Delete(member);
            return RedirectToAction(nameof(MemberList));
        }
        #endregion

        #region Kategori İşlemeleri
        public IActionResult CategoryList()
        {
            var categoryList = _categoryService.GetAll();
            return View(categoryList);
        }

        [HttpGet]
        public IActionResult CategoryAdd()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CategoryAdd(CategoryModel categoryModel)
        {
            if (ModelState.IsValid)
            {
                var entity = new Category()
                {
                    CategoryName = categoryModel.CategoryName
                };
                _categoryService.Add(entity);
                return RedirectToAction(nameof(CategoryList));
            }
            return View(categoryModel);
        }

        [HttpGet]
        public async Task<IActionResult> CategoryDetails(int categoryId)
        {
            var entity = await _categoryService.GetById(categoryId);
            if (entity == null)
            {
                return NotFound();
            }
            var model = new CategoryModel
            {
                CategoryId = categoryId,
                CategoryName = entity.CategoryName
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> CategoryDetails(CategoryModel categoryModel)
        {

            var entity = await _categoryService.GetById(categoryModel.CategoryId);
            if (entity == null)
            {
                return NotFound();
            }
            else
            {
                entity.CategoryName = categoryModel.CategoryName;
                _categoryService.Update(entity);
                return RedirectToAction(nameof(CategoryList));
            }
        }
        [HttpPost]
        public async Task<IActionResult> CategoryDelete(int categoryId)
        {

            Category category = await _categoryService.GetById(categoryId);
            if (category == null)
            {
                return NotFound();
            }
            else
            {
                _categoryService.Delete(category);
                return RedirectToAction(nameof(CategoryList));
            }
        }
        #endregion

        #region Mail işlemeri
        public IActionResult MailList()
        {
            var messages = _messageService.GetAll();
            return View(messages);
        }
        [HttpGet]
        public async Task<IActionResult> MailSend(int mailId)
        {
            var messages = await _messageService.GetById(mailId);
            var model = new MailRequest()
            {
             MessagesId=messages.MessagesId, 
             Title=messages.Title,
             Email=messages.Email,
             Name=messages.Name,
             Message=messages.Message,
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult MailSend(MailRequest messages)
        {
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587); 
            client.Credentials = new NetworkCredential("myblogtest10@gmail.com", "zluzmiyidfhlfxzn"); 
            client.EnableSsl = true;
            MailMessage msj = new MailMessage();
            msj.From = new MailAddress("myblogtest10@gmail.com",messages.AnswerName); 
            msj.To.Add(messages.Email); 
            msj.Subject = messages.AnswerTitle; 
            msj.Body = messages.AnswerMessage; 
            client.Send(msj);
            _messageService.MessageStatusUpdate(messages.MessagesId);
            return RedirectToAction(nameof(MailList));
        }
        #endregion
    }
}
