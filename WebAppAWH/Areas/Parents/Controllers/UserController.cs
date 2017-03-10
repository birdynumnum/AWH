using Domain;
using Service;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppAWH.ViewModels;
using AutoMapper;
using WebAppAWH.Infrastructure.Alerts;
using Microsoft.Web.Mvc;

namespace WebAppAWH.Areas.Parents.Controllers
{
    [Authorize(Roles = "parent")]
    public class UserController : Controller
    {
        private IUserService userservice;
        private ICurrentUser currentuser { get; set; }

        public UserController(IUserService _userservice, ICurrentUser _currentuser)
        {
            userservice = _userservice;
            currentuser = _currentuser;
        }

        public ActionResult Index()
        {
            var currentuserid = currentuser.GetCurrentUser().Id;
            var listusers = userservice.GetAll().Where(i => i.Creator.Id == currentuserid);
            IEnumerable<UserVM> users = Mapper.Map<IEnumerable<ApplicationUser>, IEnumerable<UserVM>>(listusers);
            return View(users);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Create(UserVM uservm)
        {
            if (ModelState.IsValid)
            {
                userservice.CreateUser(new User(uservm.UserName, uservm.Email, string.Empty));

                return this.RedirectToAction<UserController>(c => c.Index()).WithSucces("Invitee created");
            }
            else
                return this.RedirectToAction<UserController>(c => c.Index()).WithError("Something went wrong");
        }

        [HttpGet]
        public ActionResult Edit(string id)
        {
            var result = Mapper.Map<ApplicationUser, UserVM>(userservice.GetUser(id));
            return View(result);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit(UserVM uservm)
        {
            ApplicationUser founduser = userservice.GetUser(uservm.Id);

            if (founduser != null)
            {
                founduser.UserName = uservm.UserName;
                founduser.Email = uservm.Email;

                userservice.UpdateUser(founduser);

                return this.RedirectToAction<UserController>(c => c.Index()).WithSucces("Invitee Edited");
            }
            else
                return this.RedirectToAction<UserController>(c => c.Index()).WithError("Something went wrong");
        }


        [HttpGet]
        public ActionResult Delete(string id)
        {
            var result = Mapper.Map<ApplicationUser, UserVM>(userservice.GetUser(id));
            return View(result);
        }

        [HttpPost]
        public ActionResult Delete(UserVM uservm)
        {
            ApplicationUser founduser = userservice.GetUser(uservm.Id);

            if (founduser != null)
            {
                userservice.DeleteUser(founduser);
                return this.RedirectToAction<UserController>(c => c.Index()).WithSucces("Invitee Deleted");
            }
            else
                return this.RedirectToAction<UserController>(c => c.Index()).WithError("Something went wrong");
        }


        [HttpGet]
        public ActionResult Details(string id)
        {
            var result = Mapper.Map<ApplicationUser, UserVM>(userservice.GetUser(id));
            return View(result);
        }

    }
}