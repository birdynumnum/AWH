using Domain;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Principal;
using Service;
using WebAppAWH.ViewModels;
using Service.Kids;
using AutoMapper;
using Microsoft.Web.Mvc;
using WebAppAWH.Infrastructure.Alerts;

namespace WebAppAWH.Areas.Parents.Controllers
{
    [Authorize(Roles = "parent")]
    public class KidController : Controller
    {
        private ApplicationDbContext context { get; set; }
        private UserManager<ApplicationUser> UserManager { get; set; }
        private ICurrentUser currentuser;
        private IKidService kidservice;

        public KidController(IKidService _kidservice, ICurrentUser _currentuser)
        {
            kidservice = _kidservice;
            currentuser = _currentuser;
        }

        public ActionResult Index()
        {
            var current_userid = currentuser.GetCurrentUser().Id;
            var listkids = kidservice.GetAllKids().ToList().Where(c => c.Creator.Id == current_userid);
            var result = Mapper.Map<IEnumerable<Kid>, IEnumerable<KidVM>>(listkids);
            return View(result);
        }

        public ActionResult Details(int id)
        {
            var result = Mapper.Map<Kid, KidVM>(kidservice.GetKid(id));
            return View(result);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(KidVM KidVM)
        {
            if (ModelState.IsValid)
            {
                kidservice.CreateKid(new Kid(KidVM.FirstName, KidVM.LastName, KidVM.DOB));
                return this.RedirectToAction<KidController>(c => c.Index()).WithSucces("Kid Added");
            }
            else return this.RedirectToAction<KidController>(c => c.Index()).WithError("Something went wrong");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var result = Mapper.Map<Kid, KidVM>(kidservice.GetKid(id));
            return View(result);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit(KidVM kidvm)
        {
            var foundkid = kidservice.GetKid(kidvm.Id);
            if (foundkid != null)
            {
                foundkid.FirstName = kidvm.FirstName;
                foundkid.LastName = kidvm.LastName;
                foundkid.DOB = kidvm.DOB;
                kidservice.EditKid(foundkid);

                return this.RedirectToAction<KidController>(c => c.Index()).WithSucces("Kid Edited");
            }
            else
                return this.RedirectToAction<KidController>(c => c.Index()).WithError("Something went wrong");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var result =  Mapper.Map<Kid, KidVM>(kidservice.GetKid(id));
            return View(result);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Delete(KidVM kidvm)
        {
            Kid foundkid = kidservice.GetKid(kidvm.Id);

            if (foundkid != null)
            { 
                kidservice.DeleteKid(foundkid.Id);
                return this.RedirectToAction< KidController>(c => c.Index()).WithSucces("Kid deleted"); 
            }else
                 return this.RedirectToAction< KidController>(c => c.Index()).WithError("Something went wrong"); 
        }
    }
}
