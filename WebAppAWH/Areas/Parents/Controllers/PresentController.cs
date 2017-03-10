using Service;
using Service.Presents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Domain;
using WebAppAWH.ViewModels;
using Microsoft.Web.Mvc;
using WebAppAWH.Infrastructure.Alerts;

namespace WebAppAWH.Areas.Parents.Controllers
{
    [Authorize(Roles = "parent")]
    public class PresentController : Controller
    {
        private IPresentService presentservice { get; set; }
        private ICurrentUser currentuser { get; set; }

        public PresentController(IPresentService _presentservice, ICurrentUser _currentuser)
        {
            presentservice = _presentservice;
            currentuser = _currentuser;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var currentuserid = currentuser.GetCurrentUser().Id;
            var listpresents = presentservice.GetAll().Where(i => i.Creator.Id == currentuserid);
            IEnumerable<PresentVM> presents = Mapper.Map<IEnumerable<Present>, IEnumerable<PresentVM>>(listpresents);
            return View(presents);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(PresentVM presentvm)
        {
            if (ModelState.IsValid)
            {
                presentservice.CreatePresent(new Present(presentvm.Name, presentvm.Description, presentvm.URL, presentvm.Brand, presentvm.Price, presentvm.Quantity, false));

                return this.RedirectToAction<PresentController>(c => c.Index()).WithSucces("Present added");
            }
            else
                return this.RedirectToAction<PresentController>(c => c.Index()).WithError("Something went wrong");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var foundpresent = Mapper.Map<Present, PresentVM>(presentservice.GetPresent(id));
            return View(foundpresent);
        }

        [HttpPost]
        public ActionResult Delete(PresentVM presentvm)
        {
            Present foundpresent = presentservice.GetPresent(presentvm.Id);

            if (foundpresent != null)
            {
                presentservice.DeletePresent(foundpresent);
                return this.RedirectToAction<PresentController>(c => c.Index()).WithSucces("Present deleted");
            }
            else
                return this.RedirectToAction<PresentController>(c => c.Index()).WithError("Something went wrong");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var foundpresent = Mapper.Map<Present, PresentVM>(presentservice.GetPresent(id));
            return View(foundpresent);
        }

        public ActionResult Edit(PresentVM presentvm)
        {
            var foundpresent = presentservice.GetPresent(presentvm.Id);
            if (foundpresent != null)
            {
                foundpresent.Name = presentvm.Name;
                foundpresent.Brand = presentvm.Brand;
                foundpresent.Description = presentvm.Description;
                foundpresent.Price = presentvm.Price;
                foundpresent.URL = presentvm.URL;
                foundpresent.Quantity = presentvm.Quantity;
                presentservice.EditPresent(foundpresent);
                return this.RedirectToAction<PresentController>(c => c.Index()).WithSucces("Present edited");
            }
            else
                return this.RedirectToAction<PresentController>(c => c.Index()).WithError("Something went wrong");
        }

        public ActionResult Details(int id)
        {
            var foundpresent = Mapper.Map<Present, PresentVM>(presentservice.GetPresent(id));

            if (foundpresent == null)
            {
                return this.RedirectToAction<PresentController>(c => c.Index()).WithError("Something went wrong");
            }

            return View(foundpresent);
        }
    }
}