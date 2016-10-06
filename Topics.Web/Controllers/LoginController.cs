using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Topics.Core.Enums;
using Topics.Core.Models;
using Topics.Services.Services;
using Topics.Services.Services.Interfaces;
using Topics.Web.Filters;
using Topics.Web.Mappers;
using Topics.Web.ViewModels;

namespace Topics.Web.Controllers
{
    public class LoginController : BaseController
    {
        private ILoginService _loginService;
        private IRoleService _roleService;

        public LoginController(ILoginService loginService, IRoleService roleService)
        {
            _roleService = roleService;
            _loginService = loginService;
        }

        // GET: Login
        public ActionResult Index()
        {
            if (TempData["errorMessage"] != null)
            {
                ViewBag.Error = TempData["errorMessage"].ToString();
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login([System.Web.Http.FromBody]UserRef model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", "Login");
            }
            ValidationMessageList messages = new ValidationMessageList();
            if (_loginService.LoginUser(model, messages))
            {
                return RedirectToAction("Index", "Home");
            }
            string error = messages.Where(m => m.Type == MessageTypes.Error).Select(m => m.Text).FirstOrDefault();
            TempData["errorMessage"] = error;
            return RedirectToAction("Index");
        }

        public ActionResult Logoff()
        {
            _loginService.Logoff();
            return RedirectToAction("Index", "Home");
        }
    }
}
