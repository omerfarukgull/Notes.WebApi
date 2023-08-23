using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notlarim.Business.Abstract;
using Notlarim.WebUI.Models;
using System.Security.Claims;
using Notlarim.Entities;
using Newtonsoft.Json;
using System.Text;

namespace Notlarim.WebUI.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        private IMemberService _memberService;
        public LoginController(IMemberService memberService)
        {
            _memberService = memberService;
        }

        #region Web Api İşlemleri
        public IActionResult Login()
        {
            return View();
        }
        public async Task<IActionResult> LoginFromRestApi(MemberModel loginModel)
        {

            using (var httpClient = new HttpClient())
            {
                var apiUrl = "https://localhost:7034/api/logins/Login";
                var jsonContent = new StringContent(JsonConvert.SerializeObject(loginModel), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync(apiUrl, jsonContent))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var loginUser = _memberService.LoginUser(loginModel.Email, loginModel.Password);
                        if (loginUser.UserStatu == "Admin")
                        {
                            var claims = new List<Claim>
                            {
                                  new Claim(ClaimTypes.Email,loginUser.Email),
                            };

                            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                            var authProperties = new AuthenticationProperties();
                            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                            HttpContext.Session.SetString("usermail", loginUser.Email);
                            HttpContext.Session.SetString("usernamesurname", loginUser.Name + " " + loginUser.SurName);
                            HttpContext.Session.SetString("username", loginUser.Name);
                            HttpContext.Session.SetString("usersurname", loginUser.SurName);
                            HttpContext.Session.SetString("userphoto", loginUser.MemberImageUrl);
                            HttpContext.Session.SetInt32("userid", loginUser.MemberId);
                            HttpContext.Session.SetString("userphonenumber", loginUser.PhoneNumber);
                            HttpContext.Session.SetString("usergender", loginUser.Gender);
                            HttpContext.Session.SetString("useruniversity", loginUser.University);
                            HttpContext.Session.SetString("userdeparment", loginUser.Department);
                            HttpContext.Session.SetString("useruserstatu", loginUser.UserStatu);

                            return RedirectToAction("Index", "Admin");
                        }
                        else if (loginUser.UserStatu == "User")
                        {
                            var claims = new List<Claim>
                            {
                                  new Claim(ClaimTypes.Email,loginUser.Email),
                            };

                            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                            var authProperties = new AuthenticationProperties();
                            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                            HttpContext.Session.SetString("usermail", loginUser.Email);
                            HttpContext.Session.SetString("usernamesurname", loginUser.Name + " " + loginUser.SurName);
                            HttpContext.Session.SetString("username", loginUser.Name);
                            HttpContext.Session.SetString("usersurname", loginUser.SurName);
                            HttpContext.Session.SetString("userphoto", loginUser.MemberImageUrl);
                            HttpContext.Session.SetInt32("userid", loginUser.MemberId);
                            HttpContext.Session.SetString("userphonenumber", loginUser.PhoneNumber);
                            HttpContext.Session.SetString("usergender", loginUser.Gender);
                            HttpContext.Session.SetString("useruniversity", loginUser.University);
                            HttpContext.Session.SetString("userdeparment", loginUser.Department);
                            HttpContext.Session.SetString("useruserstatu", loginUser.UserStatu);
                            return RedirectToAction("GetNoteFromRestApi", "Home");
                        }
                    }
                }
                ViewBag.Error = "Mail adresiniz veya Şifreniz hatalı";
                return View("Login", loginModel);
            }
        }

        public IActionResult MemberAdd()
        {
            return View();
        }
        public async Task<IActionResult> MemberAddFromRestApi(MemberModel model)
        {
            bool userCheck = _memberService.UserCheckMail(model.Email);

            if (userCheck)
            {
                ViewBag.Error = "Bu e-posta adresi zaten kullanımda.";
                return View("MemberAdd", model);
            }

            using (var httpClient = new HttpClient())
            {
                if (model.Gender == "kadın")
                {
                    model.MemberImageUrl = "bayan-user.png";
                    model.UserStatu = "User";
                }
                else
                {
                    model.MemberImageUrl = "erkek-user.jpeg";
                    model.UserStatu = "User";
                }
                var apiUrl = "https://localhost:7034/api/logins/MemberAdd";
                var jsonContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync(apiUrl, jsonContent))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Login));
                    }
                }

            }
            ViewBag.Error = "Bilinmeyen bir hata oluştu.";
            return View("MemberAdd", model);
        }
        #endregion

    }
}
