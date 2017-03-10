using Domain;
using Service;
using Service.Presents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppAWH.ViewModels;
using AutoMapper;
using WebAppAWH.Infrastructure.Alerts;
using Microsoft.Web.Mvc;
using Microsoft.AspNet.Identity.EntityFramework;
using WebAppAWH.Controllers;
using System.Net;
using System.Web.Security;

namespace WebAppAWH.Areas.Invitee.Controllers
{
    [Authorize(Roles = "invitee")]
    public class HomeController : Controller
    {
        private IPresentService presentservice { get; set; }
        private IUserService userservice { get; set; }
        private ICurrentUser currentuser { get; set; }

        public HomeController(IPresentService _presentservice, IUserService _userservice, ICurrentUser _currentuser)
        {
            presentservice = _presentservice;
            userservice = _userservice;
            currentuser = _currentuser;
        }

        public ActionResult Index(InviteePresentVM inviteepresentVM)
        {
            List<ApplicationUser> users = userservice.GetAllInclude().Where(c => c.Id == currentuser.GetCurrentUser().Id).ToList();

            var creator_id = ((currentuser.GetCurrentUser().Creator) as IdentityUser).Id;

            if (users.Count != null)
            {
                inviteepresentVM.Presents = presentservice.GetAll().ToList().Where(c => c.Creator.Id == creator_id).ToList();
            }

            inviteepresentVM.HandleRequest();
            ModelState.Clear();
            return View(inviteepresentVM);
        }

        public ActionResult Details(int id)
        {
            var foundpresent = Mapper.Map<Present, PresentVM>(presentservice.GetPresent(id));

            InviteePresentVM fake = null;

            if (foundpresent == null)
            {
                return this.RedirectToAction<HomeController>(c => c.Index(fake)).WithError("Something went wrong");
            }

            return View(foundpresent);
        }

        public ActionResult AddToBasket(int id)
        {
            var present = presentservice.GetPresent(id);

            if (present != null)
            {
                GetBasket().AddPresent(present);
            }
            return this.RedirectToAction<HomeController>(c => c.BasketView()).WithSucces("Item added to Basket");
        }

        public void RemoveFromBasket(int id)
        {
            var present = presentservice.GetPresent(id);

            if (present != null)
            {
                GetBasket().RemovePresent(present);
            }
        }

        private PresentBasket GetBasket()
        {
            PresentBasket basket = (PresentBasket)Session["basket"];
            if (basket == null)
            {
                basket = new PresentBasket();
                Session["basket"] = basket;
            }
            return basket;
        }

        public ActionResult BasketView()
        {
            var model = GetBasket();

            return View(new PresentBasketVM { PresentBasket = GetBasket() });
        }

        public ActionResult CheckOut()
        {
            //TODO send mail...
            Session.Clear();
            Session.Abandon();

            //TODO - fix this!!
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home", new { area = "Admin" });
        }

        public ActionResult About()
        {
            return View();
        }
    }
}