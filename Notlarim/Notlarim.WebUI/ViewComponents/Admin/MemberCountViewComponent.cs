using Microsoft.AspNetCore.Mvc;
using Notlarim.Business.Abstract;

namespace Notlarim.WebUI.ViewComponents.Admin
{
    public class MemberCountViewComponent:ViewComponent
    {
        private IMemberService _memberService;
        public MemberCountViewComponent(IMemberService memberService)
        {
            _memberService = memberService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var memberCount = await _memberService.GetAll();
            return View(memberCount);
        }
    }
}
