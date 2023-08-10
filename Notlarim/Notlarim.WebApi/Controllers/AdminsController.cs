using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MyBlog.Entities.Concrete;
using Notlarim.Business.Abstract;
using Notlarim.Entities;
using Notlarim.WebApi.Dto;
using System.Net.Mail;
using System.Net;

namespace Notlarim.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminsController : ControllerBase
    {
        private INoteService _noteService;
        private ICategoryService _categoryService;
        private IMemberService _memberService;
        private IMessageService _messageService;
        public AdminsController(INoteService noteService, ICategoryService categoryService, IMemberService memberService, IMessageService messageService)
        {
            _noteService = noteService;
            _categoryService = categoryService;
            _memberService = memberService;
            _messageService = messageService;
        }

        #region Not İşlemleri
        [HttpGet("NoteList")]
        public async Task<IActionResult> Notelist()
        {
            var note = await _noteService.AdminNoteDetails();
            return Ok(note);
        }
        [HttpGet("NoteDetails/{noteId}")]
        public async Task<IActionResult> NoteDetails(int noteId)
        {
            if (noteId == null)
            {
                return BadRequest();
            }
            var note = await _noteService.GetById(noteId);
            return Ok(note);
        }
        [HttpPut("NoteUpdate/{noteId}")]
        public async Task<IActionResult> NoteUpdate(int noteId, NoteDto noteDto)
        {
            if (noteId != noteDto.NoteId)
            {
                return BadRequest();
            }
            var entity = await _noteService.GetById(noteId);
            if (entity == null)
            {
                return NotFound();
            }
            else if (entity != null)
            {
                entity.Title = noteDto.Title;
                entity.Content = noteDto.Content;
                entity.CategoryId = noteDto.CategoryId;
                entity.IsApproved = noteDto.IsApproved;
                entity.NoteImgUrl = noteDto.NoteImgUrl;
                entity.NotePdfUrl = noteDto.NotePdfUrl;

                _noteService.Update(entity);
                return NoContent();
            }
            return BadRequest();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNote(int id)
        {
            var note = await _noteService.GetById(id);
            if (note == null)
            {
                return NotFound();
            }
            await _noteService.DeleteAsync(note);
            return NoContent();
        }
        #endregion

        #region Kategori İşlemleri
        [HttpGet("CategoryList")]
        public async Task<IActionResult> CategoryList()
        {
            var categories = await _categoryService.GetAll();
            return Ok(categories);
        }
        [HttpGet("CategoryDetails/{categoryId}")]
        public async Task<IActionResult> CategoryDetails(int categoryId)
        {
            if (categoryId == null)
            {
                return BadRequest();
            }
            var note = await _categoryService.GetById(categoryId);
            return Ok(note);
        }
        [HttpPost("CategoryAdd")]
        public async Task<IActionResult> CategoryAdd(CategoryDto category)
        {
            if (category == null)
            {
                return BadRequest();
            }
            var model = new Category()
            {
                CategoryName = category.CategoryName
            };
            var categories = await _categoryService.AddAsync(model);
            return Ok(categories);
        }
        [HttpPut("CategoryUpdate/{categoryId}")]
        public async Task<IActionResult> CategoryUpdate(int categoryId, CategoryDto category)
        {
            if (categoryId != category.CategoryId)
            {
                return BadRequest();
            }
            var entity = await _categoryService.GetById(categoryId);
            if (entity == null)
            {
                return NotFound();
            }
            else if (entity != null)
            {
                entity.CategoryName = category.CategoryName;

                _categoryService.Update(entity);
                return NoContent();
            }
            return BadRequest();
        }
        [HttpDelete("CategoryDelete/{id}")]
        public async Task<IActionResult> CategoryDelete(int id)
        {
            var category = await _categoryService.GetById(id);
            if (category == null)
            {
                return NotFound();
            }
            _categoryService.Delete(category);
            return NoContent();
        }
        #endregion

        #region Üye İşlemleri
        [HttpGet("MemberList")]
        public async Task<IActionResult> MemberList()
        {
            var members = await _memberService.GetAll();
            return Ok(members);
        }
        [HttpGet("MemberDetails/{memberId}")]
        public async Task<IActionResult> MemberDetails(int memberId)
        {
            var member = await _memberService.GetById(memberId);
            return Ok(member);
        }
        [HttpPut("MemberUpdate/{memberId}")]
        public async Task<IActionResult> MemberUpdate(int memberId, MemberStatuDto memberStatuDto)
        {
            if (memberId != memberStatuDto.MemberId)
            {
                return BadRequest();
            }
            var entity = await _memberService.GetById(memberId);
            if (entity == null)
            {
                return BadRequest();
            }
            else if (entity != null && memberStatuDto.UserStatu == "Admin")
            {
                entity.UserStatu = memberStatuDto.UserStatu;
                _memberService.Update(entity);
                return NoContent();
            }
            else if (entity != null && memberStatuDto.UserStatu == "User")
            {

                entity.UserStatu = memberStatuDto.UserStatu;
                _memberService.Update(entity);

                return RedirectToAction("Login", "Logins");
            }
            return BadRequest();
        }
        [HttpDelete("MemberDelete/{memberId}")]
        public async Task<IActionResult> MemberDelete(int memberId)
        {
            var member = await _memberService.GetById(memberId);
            if (member == null)
            {
                return NotFound();
            }
            _memberService.Delete(member);
            return NoContent();
        }
        #endregion

        #region Mail İşlemeri
        [HttpGet("MailList")]
        public async Task<IActionResult> MailList()
        {
            var mails = await _messageService.GetAll();
            return Ok(mails);
        }
        [HttpGet("MailDetails/{mailId}")]
        public async Task<IActionResult> MailDetails(int mailId)
        {
            if (mailId == null)
            {
                return BadRequest();
            }
            var mail = _messageService.GetById(mailId);
            return Ok(mail);
        }
        #endregion

    }
}
