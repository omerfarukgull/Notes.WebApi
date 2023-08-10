using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Notlarim.Business.Abstract;
using Notlarim.Entities;
using Notlarim.WebUI.Models;
using System.Net.Mail;
using System.Net;
using MyBlog.Entities.Concrete;
using Microsoft.AspNetCore.Authorization;

namespace Notlarim.WebUI.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private INoteService _noteService;
        private  IMessageService _messageService;
        public HomeController(INoteService noteService, IMessageService messageService)
        {
            _noteService = noteService;
            _messageService = messageService;
        }

        // Anasayfa da Notları Listelem
        public async Task<IActionResult> NoteList()
        {
            var noteList = await _noteService.GetAllIsApproved();
            return View(noteList);
        }

        // Not' un detay sayfası 
        public async Task<IActionResult> NoteDetails(int noteId)
        {
            Note note = await _noteService.GetNoteDetails(noteId);
            return View(note);
        }

        // Üniversite Derst Notları Sayfasıda seilen kategoriye göre notları getirme getirme
        public async Task<IActionResult> CategoriesAndNotes(int categoryId)
        {
            var noteList = await _noteService.GetByCategory(categoryId);
            return View(noteList);
        }

        // Hakımızda Sayfası
        public IActionResult About()
        {
            return View();
        }

        // İletişim Sayfası
        [HttpGet]
        public IActionResult Contact()
        {
            return View();
        }

        //İletişim Mail gönderme
        [HttpPost]
        public IActionResult Contact(Messages model)
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
                ViewBag.Succes = "teşekkürler Mailniz başarı bir şekilde gönderildi"; //Bu kısımlarda ise kullanıcıya bilgi vermek amacı ile olur
                _messageService.Add(model); 
                return View();
            }
            catch (Exception)
            {
                ViewBag.Error = "Mesaj Gönderilirken hata oluştu"; //Bu kısımlarda ise kullanıcıya bilgi vermek amacı ile olur
                return View();
            }
        }

        //Sitedeki Nota adına göre arama cubuğu
        [HttpPost]
        public async Task<IActionResult> Search(string search)
        {
           List<SearchNote> searches= new List<SearchNote>();
            var searchNote = await _noteService.GetAll(search);

            foreach (var item in searchNote)
            {

                searches.Add(new SearchNote()
                {
                    SearchNoteId=item.NoteId,
                    SearchNoteTitle=item.Title,
                    SearchNoteImg=item.NoteImgUrl
                });
     

            }
            return View("Result", searches);
        }
    }
}
