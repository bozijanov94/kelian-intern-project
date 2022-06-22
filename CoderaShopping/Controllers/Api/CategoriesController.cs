using System.Web.Mvc;
using CoderaShopping.Business.Services;

namespace CoderaShopping.Controllers.Api
{
    public class CaterogiesController : Controller
    {
        private ICategoryService _categoryService;

        public CaterogiesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public ActionResult GetAll()
        {
            var categories = _categoryService.GetAll();
            return Json(categories, JsonRequestBehavior.AllowGet);
        }
    }
}