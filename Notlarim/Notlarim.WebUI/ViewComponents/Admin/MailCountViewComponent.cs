using Microsoft.AspNetCore.Mvc;
using Notlarim.Business.Abstract;

namespace Notlarim.WebUI.ViewComponents.Admin
{
    public class MailCountViewComponent: ViewComponent
    {
        private IMessageService _messageService;
        public MailCountViewComponent(IMessageService messageService)
        {
            _messageService = messageService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = await _messageService.GetAll();
            return View(model);
        }
    }
}
