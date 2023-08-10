using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Notlarim.Business.Abstract;
using Notlarim.Entities;
using Notlarim.WebUI.Models;
using System.Security.Claims;

namespace Notlarim.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private IMemberService _memberService;
        public AccountController(IMemberService memberService)
        {
            _memberService = memberService;
        }
       

   
        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.Remove("usermail");
            return RedirectToAction("NoteList", "Home");
        }

      
        [HttpGet]
        public async Task<IActionResult> UserProfile(int userId)
        {
            var entity = await _memberService.GetById(userId);

            var model = new MemberModel
            {
                MemberId = entity.MemberId,
                Email = entity.Email,
                Gender = entity.Gender,
                MemberImageUrl = entity.MemberImageUrl,
                Name = entity.Name,
                Password = entity.Password,
                PhoneNumber = entity.PhoneNumber,
                SurName = entity.SurName,
                University= entity.University,
                Department= entity.Department,

            };
            return View(model);

        }
       
        [HttpGet]
        public async  Task<IActionResult> UserDetails(int userId)
        {
            var entity = await _memberService.GetById(userId);
            var model = new MemberModel
            {
                MemberId = entity.MemberId,
                Email = entity.Email,
                Gender = entity.Gender,
                MemberImageUrl = entity.MemberImageUrl,
                Name = entity.Name,
                Password = entity.Password,
                PhoneNumber = entity.PhoneNumber,
                SurName = entity.SurName,
                Department= entity.Department,
                University= entity.University,
                UserStatu= entity.UserStatu,

            };
            return View(model);
        }
    
        [HttpPost]
        public async Task<IActionResult> UserDetails(MemberModel memberModel, IFormFile file)
        {
            var entity = await _memberService.GetById(memberModel.MemberId);
            if (entity == null)
            {
                return NotFound();
            }
            else if (entity != null)
            {
                entity.Name = memberModel.Name;
                entity.SurName = memberModel.SurName;
                entity.Gender = memberModel.Gender;
                entity.Email = memberModel.Email;
                entity.Password = memberModel.Password;
                entity.PhoneNumber = memberModel.PhoneNumber;
                entity.University= memberModel.University;
                entity.Department = memberModel.Department;
                entity.UserStatu = memberModel.UserStatu;
                if (file != null)
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\upload\\img", file.FileName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    entity.MemberImageUrl = file.FileName;
                }
                else
                {
                    entity.MemberImageUrl = memberModel.MemberImageUrl;
                }


                _memberService.Update(entity);


                HttpContext.Session.SetString("usermail", entity.Email);
                HttpContext.Session.SetString("usernamesurname", entity.Name + " " + entity.SurName);
                HttpContext.Session.SetString("username", entity.Name);
                HttpContext.Session.SetString("usersurname", entity.SurName);
                HttpContext.Session.SetString("userphoto", entity.MemberImageUrl);
                HttpContext.Session.SetString("userphonenumber", entity.PhoneNumber);
                HttpContext.Session.SetString("usergender", entity.Gender);
                HttpContext.Session.SetString("useruniversity", entity.University);
                HttpContext.Session.SetString("userdeparment", entity.Department);
                HttpContext.Session.SetString("useruserstatu", entity.UserStatu);

                return RedirectToAction("UserProfile", new RouteValueDictionary(
                                        new { controller = "Account", action = "UserProfile", userId = memberModel.MemberId }));
            }
            return View(memberModel);
        }
    }
}
