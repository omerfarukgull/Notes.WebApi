using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    [AllowAnonymous]
    public class HomesController : ControllerBase
    {
        private INoteService _noteService;
        private IMessageService _messageService;
        public HomesController(INoteService noteService, IMessageService messageService)
        {
            _noteService = noteService;
            _messageService = messageService;
        }
        [HttpGet]
        public async Task<IActionResult> NoteList()
        {
            var noteList = await _noteService.GetAllIsApproved();
            return Ok(noteList);
        }
    
        [HttpGet("notedetails/{noteId}")]
        public async Task<IActionResult> NoteDetails(int noteId)
        {
            var note = await _noteService.GetNoteDetails(noteId);
            return Ok(note);
        }
        [HttpGet("cat/{categoryId}")]
        public async Task<IActionResult> CategoriesAndNotes(int categoryId)
        {
            var noteList = await _noteService.GetByCategory(categoryId);
            return Ok(noteList);
        }
        [HttpPost("search/{search}")]
        public async Task<IActionResult> Search(string search)
        {
            List<SearchNoteDto> searches = new List<SearchNoteDto>();
            var searchNote = await _noteService.GetAll(search);
            if (searchNote.Count() ==0)
            {
                return Ok(new { message = "Öyle Bir Not bulunamadı" });
            }


            foreach (var item in searchNote)
            {

                searches.Add(new SearchNoteDto()
                {
                    SearchNoteId = item.NoteId,
                    SearchNoteTitle = item.Title,
                    SearchNoteImg = item.NoteImgUrl
                });

            }
            return Ok(searches);

        }
        [HttpPost("mailsend")]
        public async Task<IActionResult> Contact(Messages model)
        {
            try
            {
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587); //Burası aynı kalacak
                client.Credentials = new NetworkCredential("myblogtest10@gmail.com", "zluzmiyidfhlfxzn"); //Buraya Kendi gmail ve şifre yaz
                client.EnableSsl = true;
                MailMessage msj = new MailMessage();
                msj.From = new MailAddress(model.Email, model.Name); //iletişim kısmında girilecek mail buaraya gelecektir
                msj.To.Add("myblogtest10@gmail.com"); //Buraya kendi mail adresimizi yazıyoruz
                msj.Subject = model.Title + "" + model.Email; //Buraya iletişim sayfasında gelecek konu ve mail adresini mail içeriğine yazacaktır
                msj.Body = model.Message; //Mail içeriği burada aktarılacakır
                client.Send(msj); //Clien sent kısmında gönderme işlemi gerçeklecektir.
                //Bu kısımdan itibaren sizden kullanıcıya gidecek mail bilgisidir 
                MailMessage msj1 = new MailMessage();
                msj1.From = new MailAddress("myblogtest10@gmail.com", "Admin");
                msj1.To.Add(model.Email); //Buraua iletişim sayfasında gelecek mail adresi gelecktir.
                msj1.Subject = "Mail'inize Cevap";
                msj1.Body = "Size En kısa zamanda Döneceğiz. Teşekkür Ederiz Bizi tercih ettiğiniz için";
                client.Send(msj1);

                await _messageService.AddAsync(model);
                return Ok(new { message = "teşekkürler Mailniz başarı bir şekilde gönderildi" });
            }
            catch (Exception)
            {
                return Ok(new { message = "Mesaj Gönderilirken hata oluştu" });
            }
        }
    }
}
