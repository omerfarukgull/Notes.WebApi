using Microsoft.AspNetCore.Mvc;
using Notlarim.Business.Abstract;
using Notlarim.Entities;
using Notlarim.WebUI.Models;
using System.Net.Mail;
using System.Net;
using MyBlog.Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using static Azure.Core.HttpHeader;
using NuGet.Packaging.Signing;
using System.Text;

namespace Notlarim.WebUI.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        //Tüm İşleler Api Üzerinde Gerçekleştirilmektedir.

        private INoteService _noteService;
        private IMessageService _messageService;
        public HomeController(INoteService noteService, IMessageService messageService)
        {
            _noteService = noteService;
            _messageService = messageService;
        }

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

        #region Web Api
        // Anasayfa da api üzerinden Notları Listelem
        public async Task<IActionResult> GetNoteFromRestApi()
        {
            var notes = new List<Note>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7034/api/homes"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    notes = JsonConvert.DeserializeObject<List<Note>>(apiResponse);
                }
            }
            return View(notes);
        }

        public async Task<IActionResult> GetNoteDetailFromRestApi(int noteId)
        {
            var note = new Note();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"https://localhost:7034/api/homes/notedetails/{noteId}"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        note = JsonConvert.DeserializeObject<Note>(apiResponse);
                    }
                }
            }
            return View(note);
        }
        public async Task<IActionResult> GetCategoriesAndNotesFromRestApi(int categoryId)
        {
            var notes = new List<Note>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"https://localhost:7034/api/homes/cat/{categoryId}"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        notes = JsonConvert.DeserializeObject<List<Note>>(apiResponse);
                    }
                }
            }
            return View(notes);
        }
        public async Task<IActionResult> MailSendFromRestApi(Messages model)
        {
            using (var httpClient = new HttpClient())
            {
                string apiUrl = "https://localhost:7034/api/homes/mailsend";
                var jsonContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync(apiUrl, jsonContent))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        ViewBag.Succes = "teşekkürler Mailniz başarı bir şekilde gönderildi";
                    }
                    else
                    {
                        ViewBag.Error = "Mesaj Gönderilirken hata oluştu";
                        return View();
                    }
                }
            }
            return View();

        }
        public async Task<IActionResult> SearchFromRestApi(string search)
        {
            using (var httpClient = new HttpClient())
            {
                var apiUrl = $"https://localhost:7034/api/homes/search/{search}";
                var jsonContent = new StringContent(JsonConvert.SerializeObject(search), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync(apiUrl, jsonContent))
                {
                    List<SearchNote> searches = new List<SearchNote>();
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    searches = JsonConvert.DeserializeObject<List<SearchNote>>(apiResponse);
                    return View("Result", searches);
                }
            
            }
            
        }
        #endregion






    }
}
