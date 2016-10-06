using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Topics.Core.Enums;
using Topics.Core.Models;
using Topics.Services.Services;
using Topics.Services.Services.Interfaces;
using Topics.Web.Mappers;
using Topics.Web.ViewModels;

namespace Topics.Web.Controllers
{
    public class RegisterController : BaseController
    {
        private IRegisterService _RegisterService;
        private IRoleService _roleService;

        public RegisterController(IRegisterService RegisterService, IRoleService roleService)
        {
            _roleService = roleService;
            _RegisterService = RegisterService;
        }

        public void GetRoles()
        {
            ICollection<RoleDTO> dtos = _roleService.GetRoles();
            ICollection<SelectListItem> list = Mapper.Map<ICollection<SelectListItem>>(dtos);
            ViewBag.Roles = list;
        }

        // GET: Register
        public ActionResult Index()
        {
            if (TempData["errorMessage"] != null)
            {
                ViewBag.Error = TempData["errorMessage"].ToString();
            }
            GetRoles();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([System.Web.Http.FromBody]UserRef model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            ValidationMessageList messages = new ValidationMessageList();
            if (_RegisterService.RegisterUser(model, messages))
            {
                return RedirectToAction("Index", "Login");
            }
            string error = messages.Where(m => m.Type == MessageTypes.Error).Select(m => m.Text).FirstOrDefault();
            TempData["errorMessage"] = error;
            return RedirectToAction("Index");
        }
    }
}
