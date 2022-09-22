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
    public class UsersController : Controller
    {
        private IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        public ActionResult GetAll(int currentPage, int itemsPerPage, UserFilterModel filter, bool orderAscend, string orderBy)
        {
            var users = _userService.GetAll(currentPage, itemsPerPage, filter, orderAscend, orderBy);
            return Json(users, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAllSearch(string search)
        {
            var users = _userService.GetAllSearch(search);
            return Json(users, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetById(Guid id)
        {
            var user = _userService.GetById(id);
            return Json(user, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create(UserCreateViewModel model)
        {
            _userService.Create(model);
            return Json(CustomMessages.User.SUCCESS_CREATE, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(Guid id)
        {
            _userService.Delete(id);
            return Json(CustomMessages.User.SUCCESS_DELETE, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Update(UserViewModel model)
        {
            _userService.Update(model);
            return Json(CustomMessages.User.SUCCESS_EDIT, JsonRequestBehavior.AllowGet);
        }
    }
}