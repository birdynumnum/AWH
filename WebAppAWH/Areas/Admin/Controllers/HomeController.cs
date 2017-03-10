using AutoMapper;
using Domain;
using Microsoft.Web.Mvc;
using Service;
using System.Collections.Generic;
using System.Web.Mvc;
using WebAppAWH.Infrastructure.ActionFilters;
using WebAppAWH.Infrastructure.Alerts;
using WebAppAWH.ViewModels;

namespace WebAppAWH.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    public class HomeController : Controller
    {
        private IUserService userservice { get; set; }

        public HomeController(IUserService _userservice)
        {
            userservice = _userservice;
        }

        public ActionResult Index()
        {
            IEnumerable<UserVM> users = Mapper.Map<IEnumerable<ApplicationUser>, IEnumerable<UserVM>>(userservice.GetAll());
            return View(users);
        }

        [HttpGet]
        public ActionResult Details(string id)
        {
            var foundparent = Mapper.Map<ApplicationUser, UserVM>(userservice.GetUser(id));

            if (foundparent == null)
                return HttpNotFound();

            return View(foundparent);
        }

        public ActionResult Edit(string id)
        {
            var result = Mapper.Map<ApplicationUser, UserVM>(userservice.GetUser(id));
            return View(result);
        }

        [HttpPost]
        public ActionResult Edit(UserVM uservm)
        {
            var founduser = userservice.GetUser(uservm.Id);
            if (founduser != null)
            {
                founduser.UserName = uservm.UserName;
                founduser.Email = uservm.Email;
                userservice.UpdateUser(founduser);

                return this.RedirectToAction<HomeController>(c => c.Index()).WithSucces("User Edited");
            }
            else
                return this.RedirectToAction<HomeController>(c => c.Index()).WithError("Something went wrong");
        }

        [HttpGet]
        [Log("Deleted Parent")]
        public ActionResult Delete(string id)
        {
            var founduser = Mapper.Map<ApplicationUser, UserVM>(userservice.GetUser(id));
            return View(founduser);
        }

        [HttpPost]
        [Log("Deleted Parent")]
        public ActionResult Delete(UserVM uservm)
        {
            ApplicationUser founduser = userservice.GetUser(uservm.Id);

            if (founduser != null)
            {
                userservice.DeleteUser(founduser);

                return this.RedirectToAction<HomeController>(c => c.Index()).WithSucces("User deleted");
            }
            else
                return this.RedirectToAction<HomeController>(c => c.Index()).WithError("Something went wrong");
        }
    }
}