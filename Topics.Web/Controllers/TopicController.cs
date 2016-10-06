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
    [TopicsAPIAuthorization]
    [TopicsMvcAuthorization]
    public class TopicController : BaseController
    {
        private IPostService _postService;
        private ITopicService _topicService;
        private IUserService _userService;

        public TopicController(IPostService postServices, ITopicService topicService, IUserService userService)
        {
            _userService = userService;
            _topicService = topicService;
            _postService = postServices;
        }

        // GET: Topics/Create
        [TopicsMvcAuthorization(Roles = RolesConstants.ADMIN)]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Topics/Create
        [TopicsAPIAuthorization(Roles = RolesConstants.ADMIN)]
        [HttpPost]
        public ActionResult Create([Bind(Include = "TopicID,IsActive,Name")] TopicVM topic)
        {
            if (ModelState.IsValid)
            {
                _topicService.AddTopic(Mapper.Map<TopicDTO>(topic));

                return RedirectToAction("Index");
            }

            return View();
        }

        // GET: Topics/Delete/5
        [TopicsMvcAuthorization(Roles = RolesConstants.ADMIN)]
        public ActionResult Delete(int id)
        {
            TopicVM Topic = Mapper.Map<TopicVM>(_topicService.GetTopic(id));
            return View(Topic);
        }

        // POST: Topics/Delete/5
        [TopicsAPIAuthorization(Roles = RolesConstants.ADMIN)]
        [HttpPost]
        public ActionResult Delete(int id, FormCollection form)
        {
            _topicService.RemoveTopic(id);

            return RedirectToAction("Index");
        }

        // GET: Topics/Details/5
        public ActionResult Details(int id)
        {
            List<PostDTO> postsDTO = _postService.GetPosts().Where(p => p.TopicID == id).ToList();
            ViewBag.Posts = Mapper.Map<List<PostVM>>(postsDTO);
            TopicVM Topic = Mapper.Map<TopicVM>(_topicService.GetTopic(id));
            return View(Topic);
        }

        // GET: Topics/Edit/5
        [TopicsMvcAuthorization(Roles = RolesConstants.ADMIN)]
        public ActionResult Edit(int id)
        {
            TopicVM Topic = Mapper.Map<TopicVM>(_topicService.GetTopic(id));
            return View(Topic);
        }

        // POST: Topics/Edit/5
        [TopicsAPIAuthorization(Roles = RolesConstants.ADMIN)]
        [HttpPost]
        public ActionResult Edit([Bind(Include = "TopicID,IsActive,Name")] TopicVM topic)
        {
            if (ModelState.IsValid)
            {
                _topicService.ChangeTopic(topic.TopicID, Mapper.Map<TopicDTO>(topic));

                return RedirectToAction("Index");
            }

            return View();
        }

        // GET: Topics
        public ActionResult Index()
        {
            return View(Mapper.Map<ICollection<TopicVM>>(_topicService.GetTopics()));
        }
    }
}
