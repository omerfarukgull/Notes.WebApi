using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Notlarim.Business.Abstract;
using System.Security.Claims;
using Notlarim.WebApi.Dto;
using Notlarim.Entities;
using Microsoft.AspNetCore.Authorization;

namespace Notlarim.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class LoginsController : ControllerBase
    {
        private IMemberService _memberService;
        public LoginsController(IMemberService memberService)
        {
            _memberService = memberService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto loginModel)
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
                return Ok(new { message = "Kullanıcı Giriş Başarılı" });
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
                return Ok(new { message = "Admin Giriş Başarılı" }); ;
            }
            else
            {
                return BadRequest(new { message = "Mail adresiniz veya Şifreniz hatal" });              
            }
           
        }
        [HttpPost]
        public async Task<IActionResult> MemberAdd(MemberDto memberDto)
        {
            bool userCheck = _memberService.UserCheckMail(memberDto.Email);
            if (memberDto != null && userCheck == false)
            {
                var entity = new Member
                {
                    Name = memberDto.Name,
                    SurName = memberDto.SurName,
                    Email = memberDto.Email,
                    Password = memberDto.Password,
                    PhoneNumber = memberDto.PhoneNumber,
                    Gender = memberDto.Gender,
                    MemberImageUrl = memberDto.MemberImageUrl,
                    University = memberDto.University,
                    Department = memberDto.Department,
                    UserStatu = "User"
                };
                _memberService.AddAsync(entity);
                return Ok(new { message = "Kayıt Başarılı" });
            }
            else
            {
                return BadRequest(new { message = "Bu e-posta adresi zaten kullanımda." });
            }
        }
    }
}


//{
//    "name": "Recep",
//  "surName": "Orkun",
//  "email": "recep@gmail.com",
//  "password": "123",
//  "phoneNumber": "5355050807",
//  "gender": "Erkek",
//  "memberImageUrl": "erkek-user.jpeg",
//  "university": "Sakarya Üniversitesi",
//  "department": "Biligisayar Mühendiliği",
//  "creatDate": "2023-08-08T08:13:03.284Z",
//  "userStatu": "User"
//  }