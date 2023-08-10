using Microsoft.AspNetCore.Mvc;
using Notlarim.Business.Abstract;

namespace Notlarim.WebUI.ViewComponents.Admin
{
    public class CategoryCountViewComponent : ViewComponent
    {
        private ICategoryService _categoryService;

        public CategoryCountViewComponent(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = await _categoryService.GetAll();
            return View(model);
        }
    }
}
