using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notlarim.Business.Abstract;
using Notlarim.WebUI.Models;
using System.Security.Claims;
using Notlarim.Entities;

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
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(MemberModel loginModel)
        {
            var loginUser = _memberService.LoginUser(loginModel.Email, loginModel.Password);
            if (loginUser != null && loginUser.UserStatu == "User")
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
                HttpContext.Session.SetInt32("userıd", loginUser.MemberId);
                HttpContext.Session.SetString("userphonenumber", loginUser.PhoneNumber);
                HttpContext.Session.SetString("usergender", loginUser.Gender);
                HttpContext.Session.SetString("useruniversity", loginUser.University);
                HttpContext.Session.SetString("userdeparment", loginUser.Department);
                HttpContext.Session.SetString("useruserstatu", loginUser.UserStatu);
                return RedirectToAction("NoteList", "Home");
            }
            else if (loginUser != null && loginUser.UserStatu == "Admin")
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
                HttpContext.Session.SetInt32("userıd", loginUser.MemberId);
                HttpContext.Session.SetString("userphonenumber", loginUser.PhoneNumber);
                HttpContext.Session.SetString("usergender", loginUser.Gender);
                HttpContext.Session.SetString("useruniversity", loginUser.University);
                HttpContext.Session.SetString("userdeparment", loginUser.Department);
                HttpContext.Session.SetString("useruserstatu", loginUser.UserStatu);
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                ViewBag.Error = "Mail adresiniz veya Şifreniz hatalı";
            }
            return View(loginModel);
        }

        [HttpGet]
        public IActionResult MemberAdd()
        {
            return View();
        }
        [HttpPost]
        public IActionResult MemberAdd(MemberModel memberModel)
        {
            bool userCheck = _memberService.UserCheckMail(memberModel.Email);
            if (memberModel != null && userCheck == false)
            {
                if (memberModel.Gender == "kadın")
                {
                    memberModel.MemberImageUrl = "bayan-user.png";
                }
                else
                {
                    memberModel.MemberImageUrl = "erkek-user.jpeg";
                }
                var entity = new Member
                {
                    Name = memberModel.Name,
                    SurName = memberModel.SurName,
                    Email = memberModel.Email,
                    Password = memberModel.Password,
                    PhoneNumber = memberModel.PhoneNumber,
                    Gender = memberModel.Gender,
                    MemberImageUrl = memberModel.MemberImageUrl,
                    University = memberModel.University,
                    Department = memberModel.Department,
                    UserStatu = "User"
                };

                _memberService.Add(entity);
                return RedirectToAction(nameof(Login));
            }
            else
            {
                ViewBag.Error = "Bu e-posta adresi zaten kullanımda.";

            }
            return View(memberModel);
        }
    }
}
