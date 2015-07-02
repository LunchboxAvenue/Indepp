using Indepp.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace Indepp.Controllers
{
    public class HomeController : Controller
    {
        private PlaceContext db = new PlaceContext();
        // GET: Home
        public ActionResult Index()
        {
            return View(db.Places.ToList());
        }

        public ActionResult Farms()
        {
            return View();
        }

        public ActionResult CraftShops()
        {
            return View();
        }

        public ActionResult Events()
        {
            return View();
        }

        public ActionResult About() 
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.MessageSent = false;
            return View();
        }

        [HttpPost]
        public ActionResult Contact(string name, string email, string message)
        {
            ViewBag.MessageSent = true;
            return View();
        }
    }
}