using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Notlarim.Business.Abstract;
using Notlarim.WebApi.Dto;

namespace Notlarim.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {

        private IMemberService _memberService;
        public AccountsController(IMemberService memberService)
        {
            _memberService = memberService;
        }

        [HttpGet("userprofile")]
        public async Task<IActionResult> UserProfile()
        {
            var userId = HttpContext.Session.GetInt32("userıd");
            var entity = await _memberService.GetById((int)userId);

            var model = new MemberDto
            {
                MemberId = entity.MemberId,
                Email = entity.Email,
                Gender = entity.Gender,
                MemberImageUrl = entity.MemberImageUrl,
                Name = entity.Name,
                Password = entity.Password,
                PhoneNumber = entity.PhoneNumber,
                SurName = entity.SurName,
                University = entity.University,
                Department = entity.Department,
                UserStatu=entity.UserStatu,

            };
            return Ok(model);
        }
        [HttpPut("userupdate")]
        public async Task<IActionResult> UserUpdate(MemberDto memberModel)// IFormFile file
        {
            var userId = HttpContext.Session.GetInt32("userıd");

            if (userId == null || userId != memberModel.MemberId)
            {
                return BadRequest();
            }
            var entity = await _memberService.GetById((int)userId);
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
                entity.University = memberModel.University;
                entity.Department = memberModel.Department;
                entity.UserStatu = memberModel.UserStatu;
                entity.MemberImageUrl = memberModel.MemberImageUrl;

                //if (file != null)
                //{
                //    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\upload\\img", file.FileName);
                //    using (var stream = new FileStream(path, FileMode.Create))
                //    {
                //        file.CopyTo(stream);
                //    }
                //    entity.MemberImageUrl = file.FileName;
                //}
                //else
                //{
                //    entity.MemberImageUrl = memberModel.MemberImageUrl;
                //}
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
                return Ok(entity);
            }

            return BadRequest();

        }
    }
}
