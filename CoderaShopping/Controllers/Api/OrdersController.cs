using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using CoderaShopping.Business;
using CoderaShopping.Business.Models;
using CoderaShopping.Business.Services;

namespace CoderaShopping.Controllers.Api
{
    public class OrdersController : Controller
    {
        private IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public ActionResult GetAll(int currentPage, int itemsPerPage, OrderFilterModel filter, bool orderAscend, string orderBy)
        {
            var orders = _orderService.GetAll(currentPage, itemsPerPage, filter, orderAscend, orderBy);
            return Json(orders, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetById(Guid id)
        {
            var order = _orderService.GetById(id);
            return Json(order, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create(OrderCreateViewModel model)
        {
            _orderService.Create(model);
            return Json(CustomMessages.Order.SUCCESS_CREATE, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(Guid id)
        {
            _orderService.Delete(id);
            return Json(CustomMessages.Order.SUCCESS_DELETE, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Update(OrderViewModel model)
        {
            _orderService.Update(model);
            return Json(CustomMessages.Order.SUCCESS_EDIT, JsonRequestBehavior.AllowGet);
        }
    }
}