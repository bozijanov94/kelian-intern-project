using System;
using System.Web.Mvc;
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

        public ActionResult GetAll()
        {
            var products = _productService.GetAll();
            return Json(products, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetById(Guid id)
        {
            var product = _productService.GetById(id);
            return Json(product, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create(ProductCreateViewModel model)
        {
            var product = _productService.Create(model);
            return Json(product, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(Guid id)
        {
            var product = _productService.Delete(id);
            return Json(product, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Update(ProductViewModel model)
        {
            var product = _productService.Update(model);
            return Json(product, JsonRequestBehavior.AllowGet);
        }
    }
}