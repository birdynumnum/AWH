using AutoMapper;
using Domain;
using Microsoft.Web.Mvc;
using Service;
using Service.Parents;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebAppAWH.Infrastructure.Alerts;
using WebAppAWH.ViewModels;

namespace WebAppAWH.Areas.Parents.Controllers
{
    [Authorize(Roles = "parent")]
    public class ParentController : Controller
    {
        private IParentService parentservice { get; set; }
        private ICurrentUser currentuser { get; set; }

        public ParentController(IParentService _parentservice, ICurrentUser _currentuser)
        {
            parentservice = _parentservice;
            currentuser = _currentuser;
        }

        public ActionResult Index()
        {
            var current_userid = currentuser.GetCurrentUser().Id;
            var listparents = parentservice.GetAll().ToList().Where(c => c.Creator.Id == current_userid);
            IEnumerable<ParentVM> parents = Mapper.Map<IEnumerable<Parent>, IEnumerable<ParentVM>>(listparents);
            return View(parents);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var foundparent = Mapper.Map<Parent, ParentVM>(parentservice.GetParent(id));

            if (foundparent == null)
            {
                return this.RedirectToAction<ParentController>(c => c.Index()).WithError("Something went wrong");
            }

            return View(foundparent);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Create(ParentVM parentVM)
        {
            if (ModelState.IsValid)
            {
                parentservice.CreateParent(new Parent(parentVM.FirstName, parentVM.LastName, parentVM.Email, parentVM.Role));

                return this.RedirectToAction<ParentController>(c => c.Index()).WithSucces("Parent added");
            }
            else
                return this.RedirectToAction<ParentController>(c => c.Index()).WithError("Something went wrong");
        }

        public ActionResult Edit(int id)
        {
            var result = Mapper.Map<Parent, ParentVM>(parentservice.GetParent(id));
            return View(result);
        }

        [HttpPost]
        public ActionResult Edit(int id, ParentVM parentvm)
        {
            var foundparent = parentservice.GetParent(id);
            if (foundparent != null)
            {
                //Thou shalt NOT reversemap - rewrite to extensionmethod
                foundparent.FirstName = parentvm.FirstName;
                foundparent.LastName = parentvm.LastName;
                foundparent.Email = parentvm.Email;
                foundparent.Role = parentvm.Role;
                parentservice.EditParent(foundparent);

                return this.RedirectToAction<ParentController>(c => c.Index()).WithSucces("Parent Edited");
            }
            else
                return this.RedirectToAction<ParentController>(c => c.Index()).WithError("Something went wrong");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var foundparent = Mapper.Map<Parent, ParentVM>(parentservice.GetParent(id));
            return View(foundparent);
        }

        [HttpPost]
        public ActionResult Delete(ParentVM parentvm)
        {
            Parent foundparent = parentservice.GetParent(parentvm.Id);

            if (foundparent != null)
            {
                parentservice.Delete(foundparent);

                return this.RedirectToAction<ParentController>(c => c.Index()).WithSucces("Parent deleted");
            }
            else
                return this.RedirectToAction<ParentController>(c => c.Index()).WithError("Something went wrong");
        }
    }
}
