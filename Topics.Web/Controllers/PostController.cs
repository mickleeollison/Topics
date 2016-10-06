using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Topics.Core.Constants;
using Topics.Core.Enums;
using Topics.Core.Models;
using Topics.Core.Utilities;
using Topics.Services.Services.Interfaces;
using Topics.Web.Filters;
using Topics.Web.ViewModels;

namespace Topics.Web.Controllers
{
    [TopicsMvcAuthorization]
    [TopicsAPIAuthorization]
    public class PostController : BaseController
    {
        private IPostService _postService;
        private ITopicService _topicService;
        private IUserService _userService;

        public PostController(IPostService postServices, ITopicService topicService, IUserService userService)
        {
            _userService = userService;
            _topicService = topicService;
            _postService = postServices;
        }

        // GET: Posts/Create
        public ActionResult Create()
        {
            ICollection<SelectListItem> userList = Mapper.Map<ICollection<SelectListItem>>(_userService.GetUsers());
            ViewBag.Users = userList;
            ICollection<SelectListItem> topicList = Mapper.Map<ICollection<SelectListItem>>(_topicService.GetTopics());
            ViewBag.Topics = topicList;
            return View();
        }

        // POST: Posts/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "TopicID,Description,Name")] PostVM post)
        {
            if (ModelState.IsValid)
            {
                _postService.AddPost(Mapper.Map<PostDTO>(post));

                return RedirectToAction("Index");
            }

            return View();
        }

        // GET: Posts/Delete/5
        public ActionResult Delete(int id)
        {
            PostVM post = Mapper.Map<PostVM>(_postService.GetPost(id));
            if (SessionManager.User.UserID == post.UserID || SessionManager.User.Role.Name == "Admin")
            {
                return View(post);
            }
            ValidationMessageList messages = new ValidationMessageList();
            messages.Add(new ValidationMessage(MessageTypes.Error, ErrorMessages.NO_POST_PERMISSION));
            string error = messages.Where(m => m.Type == MessageTypes.Error).Select(m => m.Text).FirstOrDefault();
            TempData["errorMessage"] = error;
            return RedirectToAction("Index");
        }

        // POST: Posts/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection form)
        {
            _postService.RemovePost(id);

            return RedirectToAction("Index");
        }

        // GET: Posts/Details/5
        public ActionResult Details(int id)
        {
            PostVM post = Mapper.Map<PostVM>(_postService.GetPost(id));
            if (post == null)
            {
                ValidationMessageList messages = new ValidationMessageList();
                messages.Add(new ValidationMessage(MessageTypes.Error, ErrorMessages.REMOVED_POST));
                string error = messages.Where(m => m.Type == MessageTypes.Error).Select(m => m.Text).FirstOrDefault();
                TempData["errorMessage"] = error;
            }
            return View(post);
        }

        // GET: Posts/Edit/5
        public ActionResult Edit(int id)
        {
            PostVM post = Mapper.Map<PostVM>(_postService.GetPost(id));
            if (SessionManager.User.UserID == post.UserID || SessionManager.User.Role.Name == "Admin")
            {
                ICollection<SelectListItem> userList = Mapper.Map<ICollection<SelectListItem>>(_userService.GetUsers());
                ViewBag.Users = userList;
                ICollection<SelectListItem> topicList = Mapper.Map<ICollection<SelectListItem>>(_topicService.GetTopics());
                ViewBag.Topics = topicList;
                return View(post);
            }
            ValidationMessageList messages = new ValidationMessageList();
            messages.Add(new ValidationMessage(MessageTypes.Error, ErrorMessages.NO_POST_PERMISSION));
            string error = messages.Where(m => m.Type == MessageTypes.Error).Select(m => m.Text).FirstOrDefault();
            TempData["errorMessage"] = error;
            return RedirectToAction("Index");
        }

        // POST: Posts/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "PostID,UserID,TopicID,DateCreated,Description,IsActive,Name,ShortDescription")] PostVM post)
        {
            if (ModelState.IsValid)
            {
                _postService.ChangePost(post.PostID, Mapper.Map<PostDTO>(post));

                return RedirectToAction("Index");
            }

            return View();
        }

        // GET: Posts
        public ActionResult Index()
        {
            if (TempData["errorMessage"] != null)
            {
                ViewBag.Error = TempData["errorMessage"].ToString();
            }
            return View(Mapper.Map<ICollection<PostVM>>(_postService.GetPosts()));
        }
    }
}
