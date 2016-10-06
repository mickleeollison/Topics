using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Topics.Core.Constants;
using Topics.Core.Models;
using Topics.Services.Services.Interfaces;
using Topics.Web.Filters;
using Topics.Web.ViewModels;

namespace Topics.Web.Controllers
{
    [TopicsMvcAuthorization]
    [TopicsAPIAuthorization]
    public class UserController : BaseController
    {
        private IRoleService _roleService;
        private IUserService _userService;

        public UserController(IRoleService roleService, IUserService userService)
        {
            _userService = userService;
            _roleService = roleService;
        }

        // GET: Users/Delete/5
        [TopicsMvcAuthorization(Roles = RolesConstants.ADMIN)]
        public ActionResult Delete(int id)
        {
            UserVM user = Mapper.Map<UserVM>(_userService.GetUser(id));
            return View(user);
        }

        // POST: Users/Delete/5
        [TopicsAPIAuthorization(Roles = RolesConstants.ADMIN)]
        [HttpPost]
        public ActionResult Delete(int id, FormCollection form)
        {
            _userService.RemoveUser(id);
            return RedirectToAction("Index");
        }

        // GET: Users/Edit/5
        [TopicsMvcAuthorization(Roles = RolesConstants.ADMIN)]
        public ActionResult Edit(int id)
        {
            ICollection<SelectListItem> listRoles = Mapper.Map<ICollection<SelectListItem>>(_roleService.GetRoles());
            ViewBag.Roles = listRoles;
            UserVM user = Mapper.Map<UserVM>(_userService.GetUser(id));
            return View(user);
        }

        // POST: Users/Edit/5
        [TopicsAPIAuthorization(Roles = RolesConstants.ADMIN)]
        [HttpPost]
        public ActionResult Edit([Bind(Include = "UserID,RoleID,IsEnabled,UserName,Role")] UserVM user)
        {
            if (ModelState.IsValid)
            {
                _userService.ChangeUser(user.UserID, Mapper.Map<UserDTO>(user));

                return RedirectToAction("Index");
            }

            return View();
        }

        // GET: Users
        public ActionResult Index()
        {
            return View(Mapper.Map<ICollection<UserVM>>(_userService.GetUsers()));
        }
    }
}
