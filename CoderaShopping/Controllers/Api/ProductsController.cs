using System;
using System.Linq;
using System.Web.Mvc;
using CoderaShopping.Business;
using CoderaShopping.Business.Models;
using CoderaShopping.Business.Services;

namespace CoderaShopping.Controllers.Api
{
    public class ProductsController : Controller
    {
        private IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        public ActionResult GetAll(int currentPage, int itemsPerPage, ProductFilterModel filter, bool orderAscend, string orderBy)
        {
            var products = _productService.GetAll(currentPage, itemsPerPage, filter, orderAscend, orderBy);
            return Json(products, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAllSearch(string search)
        {
            var products = _productService.GetAllSearch(search);
            return Json(products, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetById(Guid id)
        {
            var product = _productService.GetById(id);
            return Json(product, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create(ProductCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var err = ModelState.Values.SelectMany(x => x.Errors.Select(y => y.ErrorMessage)).Where(z => !string.IsNullOrEmpty(z));
                return Json(new {Success = false, Message = err }, JsonRequestBehavior.AllowGet);
            }
            _productService.Create(model);
            return Json(new {Success = true, Message = CustomMessages.Product.SUCCESS_CREATE}, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(Guid id)
        {
            _productService.Delete(id);
            return Json(CustomMessages.Product.SUCCESS_DELETE, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Update(ProductViewModel model)
        {
            _productService.Update(model);
            return Json(CustomMessages.Product.SUCCESS_EDIT, JsonRequestBehavior.AllowGet);
        }
    }
}