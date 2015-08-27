using Indepp.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.Net;
using System.Net.Mail;
using SendGrid;
using System.Configuration;
using Indepp.ViewModels;

namespace Indepp.Controllers
{
    public class HomeController : Controller
    {
        private PlaceContext db = new PlaceContext();

        // GET: Home
        public ActionResult Index(int? page)
        {
            ViewBag.PageTitle = "Home";

            var blogPosts = db.BlogPosts.OrderByDescending(pb => pb.ID);
            int pageSize = 3;
            int pageNumber = (page ?? 1);

            return View(blogPosts.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult About() 
        {
            ViewBag.PageTitle = "About";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.PageTitle = "Contact";
            return View();
        }

        [HttpPost]
        public ActionResult Contact(EmailMessage emailMessage)
        {
            ViewBag.PageTitle = "Contact";

            if (ModelState.IsValid)
            {
                // Add Recipients
                /*
                var recipients = new List<string>()
                { 
                    @"Artur Smulko <artursmulko@gmail.com>" 
                };

                // Using SendGrid to send emails
                var messageToIndepp = new SendGridMessage();
                messageToIndepp.From = new MailAddress(emailMessage.Email, emailMessage.Name);
                messageToIndepp.AddTo(recipients);
                messageToIndepp.Subject = emailMessage.Subject;
                messageToIndepp.Text = emailMessage.Message;

                // Credentials
                var username = ConfigurationManager.AppSettings["SENDGRID_USER"];
                var password = ConfigurationManager.AppSettings["SENDGRID_PASSWORD"];
                var credentials = new NetworkCredential(username, password);
                var transportWeb = new Web(credentials);

                // Send email
                transportWeb.DeliverAsync(messageToIndepp);
                */
                TempData.Add("MessageSent", "Your email has been sent");

                return RedirectToAction("Contact");
            }
            else
            {
                return View(emailMessage);
            }
        }

        [HttpGet]
        public ActionResult PlaceMap(string searchString)
        {
            //ViewBag.CurrentFilter = searchString;


            return View();
        }

        public JsonResult GetPlaceLocations(bool showCoffee, bool showFood, bool showFarm, bool showCraftShop)
        {
            var places = db.Places.Select(p => new PlaceMap
            {
                Name = p.Name,
                Category = p.Category,
                Longitude = p.Address.Longitude,
                Latitude = p.Address.Latitude
            });

            //var sortedPlaces = places.Where(p => p.Category != null);
            return Json(places, JsonRequestBehavior.AllowGet);
        }
    }
}