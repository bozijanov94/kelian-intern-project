using System;
using System.Web.Mvc;
using CoderaShopping.Business.Models;
using CoderaShopping.Business.Services;

namespace CoderaShopping.Controllers.Api
{
    public class CategoriesController : Controller
    {
        private ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public ActionResult GetAll()
        {
            var categories = _categoryService.GetAll();
            return Json(categories, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetById(Guid id)
        {
            var category = _categoryService.GetById(id);
            return Json(category, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create(CategoryCreateViewModel model)
        {
            var category = _categoryService.Create(model);
            return Json(category, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(Guid id)
        {
            var category = _categoryService.Delete(id);
            return Json(category, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Update(CategoryViewModel model)
        {
            var category = _categoryService.Update(model);
            return Json(category, JsonRequestBehavior.AllowGet);
        }
    }
}