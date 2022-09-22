using System;
using System.Web.Mvc;
using CoderaShopping.Business;
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

        public ActionResult GetAll(int currentPage, int itemsPerPage, CategoryFilterModel filter, bool orderAscend, string orderBy)
        {
            var categories = _categoryService.GetAll(currentPage, itemsPerPage, filter, orderAscend, orderBy);
            return Json(categories, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAllAvailable(string search, bool? status = null)
        {
            var categories = _categoryService.GetAllAvailable(search, status);
            return Json(categories, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetById(Guid id)
        {
            var category = _categoryService.GetById(id);
            return Json(category, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create(CategoryCreateViewModel model)
        {
            _categoryService.Create(model);
            return Json(CustomMessages.Category.SUCCESS_CREATE, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(Guid id)
        {
            _categoryService.Delete(id);
            return Json(CustomMessages.Category.SUCCESS_DELETE, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Update(CategoryViewModel model)
        {
            _categoryService.Update(model);
            return Json(CustomMessages.Category.SUCCESS_EDIT, JsonRequestBehavior.AllowGet);
        }
    }
}