using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Topics.Core.Utilities;
using Topics.Web.Filters;
using Topics.Web.ViewModels;

namespace Topics.Web.Controllers
{
    [TopicsMvcAuthorization]
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            return View();
        }
    }
}
