using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;
using System.Web.Mvc;
using Topics.Core.Utilities;
using Topics.Web.ViewModels;

namespace Topics.Web.Controllers
{
    public class BaseController : Controller
    {
        public BaseController()
        {
            ViewBag.Title = "Home Page";
            ViewBag.User = Mapper.Map<UserVM>(SessionManager.User);
        }
    }
}
